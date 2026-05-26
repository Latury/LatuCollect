/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration.Services                                ║
║  Fichier : ConfigurationService.cs                                   ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer la lecture et l’écriture de la configuration utilisateur      ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Charger la configuration depuis un fichier JSON                   ║
║  - Sauvegarder la configuration                                      ║
║  - Fournir une configuration par défaut si nécessaire                ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Accès fichier uniquement                                          ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration.Constants;
using LatuCollect.Core.Configuration.Interfaces;
using LatuCollect.Core.Configuration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LatuCollect.Core.Configuration.Services
{
    public class ConfigurationService : IConfigurationService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTANTES
        // ═════════════════════════════════════════════════════════════

        private const string CONFIG_FILE_NAME = "config.json";


        // ═════════════════════════════════════════════════════════════
        // 2. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════

        private readonly string _configPath;

        private readonly SemaphoreSlim _configLock = new(1, 1);

        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public ConfigurationService(string? customConfigPath = null)
        {
            // 🔥 Tests → chemin custom
            if (!string.IsNullOrWhiteSpace(customConfigPath))
            {
                var directory = Path.GetDirectoryName(customConfigPath);

                if (!string.IsNullOrWhiteSpace(directory) &&
                    !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                _configPath = customConfigPath;

                return;
            }

            // ✔ Application réelle
            var appData = Environment.GetFolderPath(
                Environment.SpecialFolder.ApplicationData);

            var folder = Path.Combine(appData, "LatuCollect");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            _configPath = Path.Combine(folder, CONFIG_FILE_NAME);
        }


        // ═════════════════════════════════════════════════════════════
        // 4. CHARGEMENT CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        public async Task<UserConfig> LoadAsync()
        {
            try
            {
                if (!File.Exists(_configPath))
                {
                    var defaultConfig = GetDefaultConfig();

                    await SaveAsync(defaultConfig);

                    return defaultConfig;
                }

                var json = await File.ReadAllTextAsync(_configPath);

                var config = JsonSerializer.Deserialize<UserConfig>(json);

                if (config == null)
                    return GetDefaultConfig();

                return Sanitize(config);
            }
            catch
            {
                return GetDefaultConfig();
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 5. SAUVEGARDE CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        public async Task SaveAsync(UserConfig config)
        {
            try
            {
                await _configLock.WaitAsync();

                var json = Serialize(config);

                // 🔥 IMPORTANT
                // Écriture atomique pour éviter pollution / race condition
                var tempPath = _configPath + ".tmp";

                await File.WriteAllTextAsync(tempPath, json);

                if (File.Exists(_configPath))
                {
                    File.Delete(_configPath);
                }

                File.Move(tempPath, _configPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Erreur sauvegarde config : {ex.Message}");
            }
            finally
            {
                _configLock.Release();
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 6. RESET CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        public async Task<UserConfig> ResetAsync()
        {
            try
            {
                var defaultConfig = GetDefaultConfig();

                await SaveAsync(defaultConfig);

                return defaultConfig;
            }
            catch
            {
                return GetDefaultConfig();
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 7. MÉTHODES UTILITAIRES
        // ═════════════════════════════════════════════════════════════

        private static string Serialize(UserConfig config)
        {
            return JsonSerializer.Serialize(config, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        private static UserConfig GetDefaultConfig()
        {
            return ConfigurationDefaults.CreateDefault();
        }

        // Valide et normalise la configuration (valeurs par défaut si nécessaire)
        private static UserConfig Sanitize(UserConfig config)
        {
            var sanitized = new UserConfig
            {
                DefaultFormat = string.IsNullOrWhiteSpace(config.DefaultFormat)
                    ? ".txt"
                    : config.DefaultFormat,

                IsDeveloperMode = config.IsDeveloperMode,

                ExcludedFolders = config.ExcludedFolders != null
                    ? new List<ExclusionItem>(config.ExcludedFolders)
                    : new List<ExclusionItem>(),

                LastOpenedFolder = config.LastOpenedFolder ?? string.Empty,

                AutoLoadLastFolder = config.AutoLoadLastFolder,

                PreviewMaxFiles = config.PreviewMaxFiles <= 0
                    ? 20
                    : config.PreviewMaxFiles,

                ExportMode = string.IsNullOrWhiteSpace(config.ExportMode)
                    ? "normal"
                    : config.ExportMode,

                LogLevel = string.IsNullOrWhiteSpace(config.LogLevel)
                    ? "Info"
                    : config.LogLevel,

                Theme = string.IsNullOrWhiteSpace(config.Theme)
                    ? "Light"
                    : config.Theme
            };

            // 🔒 Garantit les exclusions système protégées
            foreach (var defaultExclusion in ConfigurationDefaults.DefaultExcludedFolders)
            {
                var existing = sanitized.ExcludedFolders
                    .FirstOrDefault(e =>
                        string.Equals(
                            e.Name,
                            defaultExclusion.Name,
                            StringComparison.OrdinalIgnoreCase));

                // ➕ Ajout si absent
                if (existing == null)
                {
                    sanitized.ExcludedFolders.Add(
                        new ExclusionItem(
                            defaultExclusion.Name,
                            true,
                            true));
                }
            }

            return sanitized;
        }
    }
}