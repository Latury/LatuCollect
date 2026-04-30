/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core.Logging.Services                                      ║
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
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS — STOCKAGE
        // ═════════════════════════════════════════════════════════════

        private readonly ObservableCollection<LogEntry> _logs = new();

        private LogLevel _minimumLevel = LogLevel.Info;


        // ═════════════════════════════════════════════════════════════
        // 2. PROPRIÉTÉS PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        public ReadOnlyObservableCollection<LogEntry> Logs { get; }

        public LogLevel MinimumLevel
        {
            get => _minimumLevel;
            set => _minimumLevel = value;
        }


        // ═════════════════════════════════════════════════════════════
        // 3. ÉVÉNEMENTS
        // ═════════════════════════════════════════════════════════════

        public event EventHandler? LogsUpdated;


        // ═════════════════════════════════════════════════════════════
        // 4. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public LogService()
        {
            Logs = new ReadOnlyObservableCollection<LogEntry>(_logs);
        }


        // ═════════════════════════════════════════════════════════════
        // 5. API PUBLIQUE — LOGGING
        // ═════════════════════════════════════════════════════════════

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


        // ═════════════════════════════════════════════════════════════
        // 6. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════

        // Ajoute un log après filtrage
        private void Add(LogLevel level, string message, string? context)
        {
            if (!ShouldLog(level))
                return;

            var log = CreateEntry(level, message, context);

            _logs.Add(log);

            NotifyUpdated();
        }


        // Vérifie si le niveau est autorisé
        private bool ShouldLog(LogLevel level)
        {
            return level >= _minimumLevel;
        }


        // Crée une entrée de log
        private LogEntry CreateEntry(LogLevel level, string message, string? context)
        {
            return new LogEntry(level, message, context);
        }


        // Notifie les abonnés
        private void NotifyUpdated()
        {
            LogsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}