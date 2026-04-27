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
║  - Fournir une configuration par défaut                              ║
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
        //
        // Toutes les valeurs par défaut sont centralisées ici
        //

        public const string DEFAULT_FORMAT = ".txt";
        public const bool DEFAULT_DEV_MODE = false;
        public const bool DEFAULT_AUTO_LOAD = false;
        public const int DEFAULT_PREVIEW_MAX_FILES = 20;
        public const string DEFAULT_THEME = "Light";


        // ═════════════════════════════════════════════════════════════
        // 2. DOSSIERS EXCLUS PAR DÉFAUT
        // ═════════════════════════════════════════════════════════════

        public static List<string> DefaultExcludedFolders => new()
        {
            "bin",
            "obj",
            ".git"
        };


        // ═════════════════════════════════════════════════════════════
        // 3. CONFIGURATION COMPLÈTE
        // ═════════════════════════════════════════════════════════════
        //
        // Crée une instance complète avec toutes les valeurs par défaut
        //

        public static UserConfig Default => new()
        {
            DefaultFormat = DEFAULT_FORMAT,
            IsDeveloperMode = DEFAULT_DEV_MODE,
            LastOpenedFolder = string.Empty,
            AutoLoadLastFolder = DEFAULT_AUTO_LOAD,
            PreviewMaxFiles = DEFAULT_PREVIEW_MAX_FILES,
            Theme = DEFAULT_THEME,
            ExcludedFolders = new List<string>(DefaultExcludedFolders)
        };
    }
}