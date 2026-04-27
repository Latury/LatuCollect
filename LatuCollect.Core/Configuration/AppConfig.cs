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

using System.Collections.ObjectModel;

namespace LatuCollect.Core.Configuration
{
    public class AppConfig
    {
        // ═════════════════════════════════════════════════════════════
        // 1. DOSSIERS EXCLUS
        // ═════════════════════════════════════════════════════════════
        //
        // Liste des dossiers ignorés lors de l’import
        //
        // Exemple :
        // - bin
        // - obj
        // - .git
        //

        public ObservableCollection<string> ExcludedFolders { get; set; } = new()
        {
            "bin",
            "obj",
            ".git"
        };


        // ═════════════════════════════════════════════════════════════
        // 2. FORMAT PAR DÉFAUT
        // ═════════════════════════════════════════════════════════════
        //
        // Format utilisé pour l’export si non défini par l’utilisateur
        //

        public string DefaultFormat { get; set; } = ".txt";


        // ═════════════════════════════════════════════════════════════
        // 3. MODE DÉVELOPPEUR
        // ═════════════════════════════════════════════════════════════
        //
        // Active les fonctionnalités avancées :
        // - Simulation
        // - Debug UI
        //

        public bool IsDeveloperMode { get; set; } = false;


        // ═════════════════════════════════════════════════════════════
        // 4. DERNIER DOSSIER OUVERT
        // ═════════════════════════════════════════════════════════════
        //
        // Utilisé pour recharger automatiquement le dernier projet
        //

        public string LastOpenedFolder { get; set; } = string.Empty;


        // ═════════════════════════════════════════════════════════════
        // 5. AUTO-CHARGEMENT
        // ═════════════════════════════════════════════════════════════
        //
        // Si activé :
        // → recharge automatiquement LastOpenedFolder au démarrage
        //

        public bool AutoLoadLastFolder { get; set; } = false;
    }
}