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
║  - Gérer les exclusions (avec protection)                            ║
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

using LatuCollect.Core.Configuration.Models;
using System.Collections.ObjectModel;

namespace LatuCollect.Core.Configuration
{
    public class AppConfig
    {
        // ═════════════════════════════════════════════════════════════
        // 1. DOSSIERS EXCLUS
        // ═════════════════════════════════════════════════════════════
        //
        // Liste des exclusions actives utilisées par le Core
        //
        // Exemple :
        // - bin (protégé)
        // - obj (protégé)
        // - node_modules (non protégé)
        //

        public ObservableCollection<ExclusionItem> ExcludedFolders { get; set; } = new();
    }
}