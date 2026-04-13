/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core                                                       ║
║  Fichier : AppConfig.cs                                              ║
║                                                                      ║
║  Rôle :                                                              ║
║  Centraliser la configuration globale de l’application               ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker les paramètres globaux                                    ║
║  - Fournir les exclusions de dossiers                                ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Appartient au Core                                                ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Collections.Generic;

namespace LatuCollect.Core.Configuration
{
    public static class AppConfig
    {
        // ======================================================
        // 📁 DOSSIERS EXCLUS (CONFIG GLOBALE)
        // ======================================================

        public static List<string> ExcludedFolders { get; set; } = new()
{
    "bin",
    "obj",
    ".git"
};
    }
}