/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Services.Import                                       ║
║  Fichier : IFileImportService.cs                                     ║
║                                                                      ║
║  Rôle :                                                              ║
║  Définir le contrat du service d’import                              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Charger l’arborescence des fichiers                               ║
║  - Retourner un résultat structuré                                   ║
║  - Exposer les opérations async d’import                             ║
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

using LatuCollect.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace LatuCollect.Core.Services.Import
{
    public interface IFileImportService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. IMPORT ARBORESCENCE
        // ═════════════════════════════════════════════════════════════

        Task<ImportResult> LoadTreeAsync(
            string rootPath,
            CancellationToken cancellationToken = default);
    }
}