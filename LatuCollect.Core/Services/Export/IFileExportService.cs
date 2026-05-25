/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Services.Export                                       ║
║  Fichier : IFileExportService.cs                                     ║
║                                                                      ║
║  Rôle :                                                              ║
║  Définir le contrat du service d’export                              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Construire le contenu exporté                                     ║
║  - Exporter le contenu vers un fichier                               ║
║  - Exposer les opérations sync et async                              ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Contrat uniquement                                                ║
║  - Aucune logique métier                                             ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Models.Export;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LatuCollect.Core.Services.Export
{
    public interface IFileExportService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. EXPORT FICHIER
        // ═════════════════════════════════════════════════════════════

        ExportResult Export(
            string path,
            string content);

        Task<ExportResult> ExportAsync(
            string path,
            string content);


        // ═════════════════════════════════════════════════════════════
        // 2. BUILD CONTENU
        // ═════════════════════════════════════════════════════════════

        ExportData BuildContentWithStats(
            IEnumerable<string> filePaths,
            bool isMarkdown);

        Task<ExportData> BuildContentWithStatsAsync(
            IEnumerable<string> filePaths,
            bool isMarkdown,
            string exportMode);
    }
}