/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Logging.Models                                             ║
║  Fichier : LogEntry.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représente une entrée de log dans l'application                     ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker les informations d’un log                                 ║
║  - Fournir une structure claire et exploitable                       ║
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
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 📌 PROPRIÉTÉS
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        public DateTime Timestamp { get; }

        public string Date { get; }

        public LogLevel Level { get; }

        public string Message { get; }

        public string? Context { get; }


        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // ⚙️ CONSTRUCTEUR
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        public LogEntry(LogLevel level, string message, string? context = null)
        {
            Timestamp = DateTime.Now;

            // Format lisible : jour/mois/année + heure
            Date = Timestamp.ToString("dd/MM/yyyy HH:mm:ss");

            Level = level;
            Message = message;
            Context = context;
        }
    }
}