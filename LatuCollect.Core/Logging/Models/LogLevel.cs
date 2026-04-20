/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Logging                                                    ║
║  Fichier : LogLevel.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Définit les niveaux de gravité des logs.                            ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Identifier le type de log (Info, Warning, Error)                  ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Aucune                                                            ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Logging.Models
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }
}