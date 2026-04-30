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

        public string DefaultFormat { get; set; } = ".txt";


        // ═════════════════════════════════════════════════════════════
        // 2. MODE DÉVELOPPEUR
        // ═════════════════════════════════════════════════════════════

        public bool IsDeveloperMode { get; set; } = false;


        // ═════════════════════════════════════════════════════════════
        // 3. DOSSIERS EXCLUS
        // ═════════════════════════════════════════════════════════════

        private List<string>? _excludedFolders;

        public List<string> ExcludedFolders
        {
            get => _excludedFolders ??= new List<string>();
            set => _excludedFolders = value ?? new List<string>();
        }


        // ═════════════════════════════════════════════════════════════
        // 4. DERNIER DOSSIER
        // ═════════════════════════════════════════════════════════════

        public string LastOpenedFolder { get; set; } = string.Empty;

        public bool AutoLoadLastFolder { get; set; } = false;


        // ═════════════════════════════════════════════════════════════
        // 5. MODE D’EXPORT
        // ═════════════════════════════════════════════════════════════

        public int PreviewMaxFiles { get; set; } = 20;

        public string ExportMode { get; set; } = "normal";


        // ═════════════════════════════════════════════════════════════
        // 6. LOGS
        // ═════════════════════════════════════════════════════════════

        public string LogLevel { get; set; } = "Info";


        // ═════════════════════════════════════════════════════════════
        // 7. THÈME
        // ═════════════════════════════════════════════════════════════

        public string Theme { get; set; } = "Light";
    }
}