/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Models                                                ║
║  Fichier : StatisticsResult.cs                                       ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter les statistiques calculées sur les fichiers             ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Modèle Core pur                                                   ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Models.Export
{
    public class StatisticsResult
    {
        // ═════════════════════════════════════════════════════════════
        // 1. PROPRIÉTÉS
        // ═════════════════════════════════════════════════════════════

        public int FileCount { get; set; }

        public long TotalLines { get; set; }

        public long TotalCharacters { get; set; }

        public long TotalSizeBytes { get; set; }
    }
}