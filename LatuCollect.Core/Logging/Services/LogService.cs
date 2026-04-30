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
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS — STOCKAGE
        // ═════════════════════════════════════════════════════════════
        //
        // Contient tous les logs en mémoire
        //

        private readonly ObservableCollection<LogEntry> _logs = new();

        private LogLevel _minimumLevel = LogLevel.Info;

        // ═════════════════════════════════════════════════════════════
        // 2. PROPRIÉTÉS PUBLIQUES — EXPOSITION
        // ═════════════════════════════════════════════════════════════
        //
        // Version lecture seule pour l’UI / autres services
        //

        public ReadOnlyObservableCollection<LogEntry> Logs { get; }

        public LogLevel MinimumLevel
        {
            get => _minimumLevel;
            set => _minimumLevel = value;
        }

        // ═════════════════════════════════════════════════════════════
        // 3. ÉVÉNEMENTS
        // ═════════════════════════════════════════════════════════════
        //
        // Notifie quand un log est ajouté
        //

        public event EventHandler? LogsUpdated;


        // ═════════════════════════════════════════════════════════════
        // 4. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public LogService()
        {
            Logs = new ReadOnlyObservableCollection<LogEntry>(_logs);
        }


        // ═════════════════════════════════════════════════════════════
        // 5. MÉTHODES PUBLIQUES — API LOGGING
        // ═════════════════════════════════════════════════════════════
        //
        // Points d’entrée principaux
        //

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
        // 6. MÉTHODE INTERNE — AJOUT LOG
        // ═════════════════════════════════════════════════════════════
        //
        // Création + stockage + notification
        //

        private void Add(LogLevel level, string message, string? context)
        {
            // 🔴 FILTRAGE
            if (level < _minimumLevel)
                return;

            var log = new LogEntry(level, message, context);

            _logs.Add(log);

            LogsUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}