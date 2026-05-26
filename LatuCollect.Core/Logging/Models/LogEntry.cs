/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core.Logging.Models                                        ║
║  Fichier : LogEntry.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représente une entrée de log dans l'application                     ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker les informations d’un log                                 ║
║  - Fournir une structure claire et exploitable                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Modèle Core pur                                                   ║
║  - Aucune dépendance UI                                              ║
║  - Aucun formatage UI                                                ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - LogLevel                                                          ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System;

namespace LatuCollect.Core.Logging.Models
{
    public class LogEntry
    {
        // ═════════════════════════════════════════════════════════════
        // 1. DONNÉES BRUTES (CORE)
        // ═════════════════════════════════════════════════════════════
        //
        // Informations fondamentales du log
        //

        public DateTime Timestamp { get; }

        public LogLevel Level { get; }

        public string Message { get; }

        public string? Context { get; }

        // ═════════════════════════════════════════════════════════════
        // 2. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public LogEntry(
            LogLevel level,
            string message,
            string? context = null)
        {
            Timestamp = DateTime.Now;

            Level = level;
            Message = message;
            Context = context;
        }
    }
}