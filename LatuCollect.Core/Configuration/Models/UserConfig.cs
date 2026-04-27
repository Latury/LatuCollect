/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration.Models                                  ║
║  Fichier : UserConfig.cs                                             ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter la configuration utilisateur persistée                  ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker les préférences utilisateur                               ║
║  - Être sérialisée en JSON                                           ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Données uniquement                                                ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Collections.Generic;

namespace LatuCollect.Core.Configuration.Models
{
    public class UserConfig
    {
        // ═════════════════════════════════════════════════════════════
        // 1. FORMAT EXPORT
        // ═════════════════════════════════════════════════════════════
        //
        // Format utilisé pour l’export par défaut
        //

        public string DefaultFormat { get; set; } = ".txt";


        // ═════════════════════════════════════════════════════════════
        // 2. MODE DÉVELOPPEUR
        // ═════════════════════════════════════════════════════════════
        //
        // Active les fonctionnalités avancées (simulation, debug)
        //

        public bool IsDeveloperMode { get; set; } = false;


        // ═════════════════════════════════════════════════════════════
        // 3. DOSSIERS EXCLUS
        // ═════════════════════════════════════════════════════════════
        //
        // Liste des dossiers ignorés lors de l’import
        //

        public List<string> ExcludedFolders { get; set; } = new();


        // ═════════════════════════════════════════════════════════════
        // 4. DERNIER DOSSIER
        // ═════════════════════════════════════════════════════════════
        //
        // Permet de restaurer l’état utilisateur au démarrage
        //

        public string LastOpenedFolder { get; set; } = string.Empty;

        public bool AutoLoadLastFolder { get; set; } = false;


        // ═════════════════════════════════════════════════════════════
        // 5. CONFIGURATION APERÇU
        // ═════════════════════════════════════════════════════════════
        //
        // Limites pour éviter surcharge UI
        //

        public int PreviewMaxFiles { get; set; } = 20;


        // ═════════════════════════════════════════════════════════════
        // 6. THÈME (FUTUR)
        // ═════════════════════════════════════════════════════════════
        //
        // Thème UI (Light / Dark)
        //

        public string Theme { get; set; } = "Light";
    }
}