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
using LatuCollect.Core.Configuration.Services;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LatuCollect.Core.Configuration.Services
{
    public class ConfigurationService : IConfigurationService
    {
        // ═════════════════════════════════════════════════════════════════════
        // 1. CONSTANTES
        // ═════════════════════════════════════════════════════════════════════

        private const string CONFIG_FILE_NAME = "config.json";

        // ═════════════════════════════════════════════════════════════════════
        // 2. CHEMIN CONFIGURATION
        // ═════════════════════════════════════════════════════════════════════

        private readonly string _configPath;

        public ConfigurationService()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var folder = Path.Combine(appData, "LatuCollect");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            _configPath = Path.Combine(folder, CONFIG_FILE_NAME);
        }

        // ═════════════════════════════════════════════════════════════════════
        // 3. CHARGEMENT
        // ═════════════════════════════════════════════════════════════════════

        public async Task<UserConfig> LoadAsync()
        {
            try
            {
                if (!File.Exists(_configPath))
                {
                    var defaultConfig = ConfigurationDefaults.Default;

                    // 🔥 CRÉE le fichier au premier lancement
                    await SaveAsync(defaultConfig);

                    return defaultConfig;
                }

                var json = await File.ReadAllTextAsync(_configPath);

                var config = JsonSerializer.Deserialize<UserConfig>(json);

                return config ?? ConfigurationDefaults.Default;
            }
            catch
            {
                return ConfigurationDefaults.Default;
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // 4. SAUVEGARDE
        // ═════════════════════════════════════════════════════════════════════

        public async Task SaveAsync(UserConfig config)
        {
            try
            {
                var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                await File.WriteAllTextAsync(_configPath, json);
            }
            catch
            {
                // volontairement silencieux (logs plus tard si besoin)
            }
        }

        // ═════════════════════════════════════════════════════════════════════
        // 5. RESET CONFIGURATION
        // ═════════════════════════════════════════════════════════════════════
        //
        // Remet la configuration à zéro (valeurs par défaut)
        //

        public async Task<UserConfig> ResetAsync()
        {
            try
            {
                var defaultConfig = ConfigurationDefaults.Default;

                await SaveAsync(defaultConfig);

                return defaultConfig;
            }
            catch
            {
                return ConfigurationDefaults.Default;
            }
        }
    }
}