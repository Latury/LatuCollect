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
using System.Collections.ObjectModel;

namespace LatuCollect.Core.Configuration
{
    public class AppConfig
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

        public ObservableCollection<string> ExcludedFolders { get; set; } = new()
    {
        "bin",
        "obj",
        ".git"
    };

        // ─────────────────────────────────────────────
        // 📁 PARAMÈTRES GLOBAUX
        // ─────────────────────────────────────────────
        //
        // Paramètres globaux de l’application
        //
        // - Format de fichier par défaut
        // - Mode développeur
        // - Dernier dossier ouvert
        // - Chargement automatique du dernier dossier

        public string DefaultFormat { get; set; } = ".txt";

        public bool IsDeveloperMode { get; set; } = false;

        public string LastOpenedFolder { get; set; } = string.Empty;

        public bool AutoLoadLastFolder { get; set; } = false;
    }
}
