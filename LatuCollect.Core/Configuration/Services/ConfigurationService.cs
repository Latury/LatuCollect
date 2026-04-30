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
using System.IO;
using System.Text.Json;
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


        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public ConfigurationService()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var folder = Path.Combine(appData, "LatuCollect");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

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
                var json = Serialize(config);

                await File.WriteAllTextAsync(_configPath, json);
            }
            catch (Exception ex)
            {
                // Non bloquant mais visible en debug
                Console.WriteLine($"Erreur sauvegarde config : {ex.Message}");
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

        // Sécurise les données chargées depuis le JSON
        // pour éviter les valeurs null ou invalides.
        private static UserConfig Sanitize(UserConfig config)
        {
            return new UserConfig
            {
                DefaultFormat = string.IsNullOrWhiteSpace(config.DefaultFormat) ? ".txt" : config.DefaultFormat,
                IsDeveloperMode = config.IsDeveloperMode,
                ExcludedFolders = config.ExcludedFolders ?? new(),
                LastOpenedFolder = config.LastOpenedFolder ?? string.Empty,
                AutoLoadLastFolder = config.AutoLoadLastFolder,
                PreviewMaxFiles = config.PreviewMaxFiles <= 0 ? 20 : config.PreviewMaxFiles,
                ExportMode = string.IsNullOrWhiteSpace(config.ExportMode) ? "normal" : config.ExportMode,
                LogLevel = string.IsNullOrWhiteSpace(config.LogLevel) ? "Info" : config.LogLevel,
                Theme = string.IsNullOrWhiteSpace(config.Theme) ? "Light" : config.Theme
            };
        }
    }
}