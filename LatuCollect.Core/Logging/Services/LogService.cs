/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Logging.Services                                           ║
║  Fichier : LogService.cs                                             ║
║                                                                      ║
║  Rôle :                                                              ║
║  Implémente le service de journalisation                             ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Créer des logs (Info, Warning, Error)                             ║
║  - Stocker les logs en mémoire                                       ║
║  - Fournir les logs au reste de l’application                        ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - ILogService                                                       ║
║  - LogEntry                                                          ║
║  - LogLevel                                                          ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Logging.Interfaces;
using LatuCollect.Core.Logging.Models;
using System.Collections.Generic;

namespace LatuCollect.Core.Logging.Services
{
    public class LogService : ILogService
    {
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 📌 CHAMPS PRIVÉS
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        private readonly List<LogEntry> _logs = new();


        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 📌 PROPRIÉTÉS
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        public IReadOnlyList<LogEntry> Logs => _logs;


        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // ⚙️ MÉTHODES PUBLIQUES
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        public void Info(string message, string? context = null)
        {
            Add(LogLevel.Info, message, context);
        }

        public void Warning(string message, string? context = null)
        {
            Add(LogLevel.Warning, message, context);
        }

        public void Error(string message, string? context = null)
        {
            Add(LogLevel.Error, message, context);
        }


        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 🔧 MÉTHODES PRIVÉES
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        private void Add(LogLevel level, string message, string? context)
        {
            var log = new LogEntry(level, message, context);
            _logs.Add(log);
        }
    }
}