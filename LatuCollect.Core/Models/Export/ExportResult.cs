/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Models.Export                                         ║
║  Fichier : ExportResult.cs                                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter le résultat d’un export                                 ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Modèle Core pur                                                   ║
║  - Aucune dépendance UI                                              ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Models.Export
{
    public class ExportResult
    {
        // ═════════════════════════════════════════════════════════════
        // 1. PROPRIÉTÉS
        // ═════════════════════════════════════════════════════════════

        public bool IsSuccess { get; set; }

        public string Message { get; set; } = "";
    }
}