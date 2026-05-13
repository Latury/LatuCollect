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
║  - Gérer les exclusions (avec protection)                            ║
║  - Assurer la compatibilité avec anciens formats                     ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Données uniquement                                                ║
║  - Nettoyage autorisé                                                ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Collections.Generic;
using System.Linq;

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
        // 3. DOSSIERS EXCLUS (NOUVEAU MODÈLE)
        // ═════════════════════════════════════════════════════════════

        private List<ExclusionItem> _excludedFolders = new();

        public List<ExclusionItem> ExcludedFolders
        {
            get => _excludedFolders;

            set
            {
                if (value == null)
                {
                    _excludedFolders = new List<ExclusionItem>();
                    return;
                }

                _excludedFolders = value
                    .Where(v => v != null && !string.IsNullOrWhiteSpace(v.Name))
                    .Select(v => new ExclusionItem(
                        v.Name.Trim(),
                        v.IsProtected,
                        v.IsDirectory
                    ))
                    .Where(v => v.Name.Length >= 2)
                    .GroupBy(v => v.Name, System.StringComparer.OrdinalIgnoreCase)
                    .Select(g => g.First())
                    .ToList();
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 4. MIGRATION ANCIEN FORMAT (STRING → OBJET)
        // ═════════════════════════════════════════════════════════════
        //
        // Permet de charger :
        // ["bin", "obj"]
        //

        public List<string>? LegacyExcludedFolders
        {
            get => null;
            set
            {
                if (value == null || value.Count == 0)
                    return;

                ExcludedFolders = value
                    .Where(v => !string.IsNullOrWhiteSpace(v))
                    .Select(v => new ExclusionItem(v.Trim(), true))
                    .ToList();
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 5. DERNIER DOSSIER
        // ═════════════════════════════════════════════════════════════

        public string LastOpenedFolder { get; set; } = string.Empty;

        public bool AutoLoadLastFolder { get; set; } = false;


        // ═════════════════════════════════════════════════════════════
        // 6. MODE D’EXPORT
        // ═════════════════════════════════════════════════════════════

        public int PreviewMaxFiles { get; set; } = 20;

        public string ExportMode { get; set; } = "normal";


        // ═════════════════════════════════════════════════════════════
        // 7. LOGS
        // ═════════════════════════════════════════════════════════════

        public string LogLevel { get; set; } = "Info";


        // ═════════════════════════════════════════════════════════════
        // 8. THÈME
        // ═════════════════════════════════════════════════════════════

        public string Theme { get; set; } = "Light";
    }
}