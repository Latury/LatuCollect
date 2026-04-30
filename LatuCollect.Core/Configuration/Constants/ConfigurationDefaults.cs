/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration.Constants                               ║
║  Fichier : ConfigurationDefaults.cs                                  ║
║                                                                      ║
║  Rôle :                                                              ║
║  Centraliser les valeurs par défaut de la configuration              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Fournir des constantes de configuration par défaut                ║
║  - Fournir une instance UserConfig initialisée                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Données uniquement                                                ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration.Models;
using System.Collections.Generic;

namespace LatuCollect.Core.Configuration.Constants
{
    public static class ConfigurationDefaults
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTANTES PAR DÉFAUT
        // ═════════════════════════════════════════════════════════════

        public const string DEFAULT_FORMAT = ".txt";
        public const bool DEFAULT_DEV_MODE = false;
        public const bool DEFAULT_AUTO_LOAD = false;
        public const int DEFAULT_PREVIEW_MAX_FILES = 20;
        public const string DEFAULT_THEME = "Light";


        // ═════════════════════════════════════════════════════════════
        // 2. DOSSIERS EXCLUS PAR DÉFAUT (IMMUTABLES)
        // ═════════════════════════════════════════════════════════════
        //
        // ⚠️ Retourne une nouvelle instance à chaque appel
        // pour éviter toute modification globale
        //

        public static IReadOnlyList<string> DefaultExcludedFolders => new List<string>
        {
            "bin",
            "obj",
            ".git"
        };


        // ═════════════════════════════════════════════════════════════
        // 3. CONFIGURATION COMPLÈTE PAR DÉFAUT
        // ═════════════════════════════════════════════════════════════

        public static UserConfig CreateDefault()
        {
            return new UserConfig
            {
                DefaultFormat = DEFAULT_FORMAT,
                IsDeveloperMode = DEFAULT_DEV_MODE,
                LastOpenedFolder = string.Empty,
                AutoLoadLastFolder = DEFAULT_AUTO_LOAD,
                PreviewMaxFiles = DEFAULT_PREVIEW_MAX_FILES,
                Theme = DEFAULT_THEME,

                // Copie défensive pour éviter toute mutation externe
                ExcludedFolders = new List<string>(DefaultExcludedFolders)
            };
        }
    }
}