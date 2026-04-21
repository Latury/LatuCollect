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
║  - Notifier les changements                                          ║
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
using System;
using System.Collections.ObjectModel;

namespace LatuCollect.Core.Logging.Services
{
    public class LogService : ILogService
    {
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 📌 COLLECTION OBSERVABLE
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        private readonly ObservableCollection<LogEntry> _logs = new();

        public ReadOnlyObservableCollection<LogEntry> Logs { get; }

        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // 🔔 ÉVÉNEMENT
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        public event EventHandler? LogsUpdated;

        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
        // ⚙️ CONSTRUCTEUR
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        public LogService()
        {
            Logs = new ReadOnlyObservableCollection<LogEntry>(_logs);
        }

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
        // 🔧 MÉTHODE PRIVÉE
        //━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

        private void Add(LogLevel level, string message, string? context)
        {
            var log = new LogEntry(level, message, context);

            _logs.Add(log);

            // 🔥 NOTIFICATION
            LogsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}