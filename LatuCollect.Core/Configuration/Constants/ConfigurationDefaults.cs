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

namespace LatuCollect.Core.Configuration.Constants
{
    public static class ConfigurationDefaults
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. CONFIGURATION PAR DÉFAUT
        // ═════════════════════════════════════════════════════════════════════
        //
        // Fournit une instance complète de configuration utilisateur
        //

        public static UserConfig Default => new()
        {
            DefaultFormat = ".txt",
            IsDeveloperMode = false,
            LastOpenedFolder = string.Empty,
            AutoLoadLastFolder = false,
            PreviewMaxFiles = 20,
            Theme = "Light",

            // 🔥 IMPORTANT
            ExcludedFolders = new()
    {
        "bin",
        "obj",
        ".git"
    }
        };
    }
}