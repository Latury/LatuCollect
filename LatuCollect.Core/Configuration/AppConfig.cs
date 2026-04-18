/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration                                         ║
║  Fichier : AppConfig.cs                                              ║
║                                                                      ║
║  Rôle :                                                              ║
║  Stocker la configuration globale de l’application                   ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gérer les dossiers exclus                                         ║
║  - Fournir des paramètres globaux                                    ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Données uniquement                                                ║
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

        // ═════════════════════════════════════════════════════════════════════
        // 1. CONFIGURATION GLOBALE
        // ═════════════════════════════════════════════════════════════════════
        //
        // Contient :
        // - Paramètres globaux
        // - Données modifiables à chaud
        //

        // ─────────────────────────────────────────────
        // 📁 DOSSIERS EXCLUS
        // ─────────────────────────────────────────────
        //
        // Liste des dossiers ignorés lors de la lecture du projet
        //

        public static List<string> ExcludedFolders { get; } = new()
        {
            "bin",
            "obj",
            ".git"
        };

    }
}