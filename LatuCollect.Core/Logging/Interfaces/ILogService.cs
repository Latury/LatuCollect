/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Logging                                                    ║
║  Fichier : ILogService.cs                                            ║
║                                                                      ║
║  Rôle :                                                              ║
║  Définit le contrat du service de journalisation.                    ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Exposer les méthodes de logging (Info, Warning, Error)            ║
║  - Permettre une implémentation interchangeable (DI)                 ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Aucune                                                            ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Logging.Interfaces
{
    public interface ILogService
    {
        void Info(string message, string? context = null);

        void Warning(string message, string? context = null);

        void Error(string message, string? context = null);
    }
}