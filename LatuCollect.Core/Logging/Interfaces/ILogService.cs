/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core.Logging.Interfaces                                    ║
║  Fichier : ILogService.cs                                            ║
║                                                                      ║
║  Rôle :                                                              ║
║  Définit le contrat du service de journalisation                     ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Exposer les méthodes de logging (Info, Warning, Error)            ║
║  - Permettre une implémentation interchangeable (DI)                 ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Interface Core pure                                               ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Logging.Interfaces
{
    // ═════════════════════════════════════════════════════════════
    // 1. CONTRAT LOGGING
    // ═════════════════════════════════════════════════════════════
    //
    // Définit les opérations disponibles pour le système de logs
    //

    public interface ILogService
    {
        // ═════════════════════════════════════════════════════════════
        // 2. MÉTHODES DE LOG — API PUBLIQUE
        // ═════════════════════════════════════════════════════════════
        //
        // Utilisées dans toute l’application pour tracer :
        // - Informations
        // - Avertissements
        // - Erreurs
        //

        // Log une information générale
        void Info(string message, string? context = null);

        // Log un avertissement
        void Warning(string message, string? context = null);

        // Log une erreur
        void Error(string message, string? context = null);
    }
}