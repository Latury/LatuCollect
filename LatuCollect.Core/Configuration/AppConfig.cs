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
    }
}