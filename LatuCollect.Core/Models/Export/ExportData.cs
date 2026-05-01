/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Models.Export                                         ║
║  Fichier : ExportData.cs                                             ║
║                                                                      ║
║  Rôle :                                                              ║
║  Contenir les données générées pour l’export                         ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Modèle Core pur                                                   ║
║  - Aucune dépendance UI                                              ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Models;

namespace LatuCollect.Core.Models.Export
{
    public class ExportData
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONTENU
        // ═════════════════════════════════════════════════════════════

        public string Content { get; set; } = "";

        public StatisticsResult Stats { get; set; } = new();


        // ═════════════════════════════════════════════════════════════
        // 2. ÉTAT PARTIEL
        // ═════════════════════════════════════════════════════════════

        public bool IsPartial { get; set; }

        public string PartialMessage { get; set; } = "";
    }
}