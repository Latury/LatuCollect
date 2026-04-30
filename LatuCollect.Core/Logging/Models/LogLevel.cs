/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core.Logging.Models                                        ║
║  Fichier : LogLevel.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Définit les niveaux de gravité des logs                             ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Identifier le type de log (Info, Warning, Error)                  ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Modèle Core pur                                                   ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Logging.Models
{
    // ═════════════════════════════════════════════════════════════
    // 1. NIVEAUX DE LOG
    // ═════════════════════════════════════════════════════════════
    //
    // Définit les niveaux de gravité utilisés dans l’application
    //

    public enum LogLevel
    {
        // Information générale (fonctionnement normal)
        Info,

        // Avertissement (problème non bloquant)
        Warning,

        // Erreur (échec ou comportement inattendu)
        Error
    }
}