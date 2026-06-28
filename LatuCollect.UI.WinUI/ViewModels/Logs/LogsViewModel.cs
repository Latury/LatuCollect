/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.ViewModels.Logs                                   ║
║  Fichier : LogsViewModel.cs                                          ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer l'affichage et le filtrage des logs                           ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Exposer les logs                                                  ║
║  - Gérer le filtrage                                                 ║
║  - Fournir les indicateurs d'erreurs                                 ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Ne contient aucune logique métier                                 ║
║  - Dépend uniquement de ILogService                                  ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using CommunityToolkit.Mvvm.ComponentModel;
using LatuCollect.Core.Logging.Interfaces;
using LatuCollect.Core.Logging.Models;
using LatuCollect.UI.WinUI.Models.Logs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LatuCollect.UI.WinUI.ViewModels.Logs
{
    public partial class LogsViewModel : ObservableObject
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════

        private readonly ILogService _logger;

        private LogFilter _selectedLogFilter = LogFilter.All;

        // ═════════════════════════════════════════════════════════════
        // 2. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public LogsViewModel(ILogService logger)
        {
            _logger = logger;

            _logger.LogsUpdated += (_, _) =>
            {
                OnPropertyChanged(nameof(Logs));
                OnPropertyChanged(nameof(HasLogErrors));
                OnPropertyChanged(nameof(LogErrorCount));
                OnPropertyChanged(nameof(FilteredLogs));
            };
        }

        // ═════════════════════════════════════════════════════════════
        // 3. PROPRIÉTÉS PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        public ReadOnlyObservableCollection<LogEntry> Logs =>
            _logger.Logs;

        public bool HasLogErrors =>
            _logger.Logs.ToList()
        .Any(l => l.Level == LogLevel.Error);

        public int LogErrorCount =>
           _logger.Logs.ToList()
        .Count(l => l.Level == LogLevel.Error);

        public LogFilter SelectedLogFilter
        {
            get => _selectedLogFilter;
            set
            {
                if (SetProperty(ref _selectedLogFilter, value))
                {
                    OnPropertyChanged(nameof(FilteredLogs));
                }
            }
        }

        public IEnumerable<LogEntry> FilteredLogs =>
            GetFilteredLogs();

        // ═════════════════════════════════════════════════════════════
        // 4. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════

        private IEnumerable<LogEntry> GetFilteredLogs()
        {
            var logs = _logger.Logs.ToList();

            return SelectedLogFilter switch
            {
                LogFilter.Info =>
                    logs.Where(l => l.Level == LogLevel.Info),

                LogFilter.Warning =>
                    logs.Where(l => l.Level == LogLevel.Warning),

                LogFilter.Error =>
                    logs.Where(l => l.Level == LogLevel.Error),

                _ => logs
            };
        }

        // ═════════════════════════════════════════════════════════════
        // 5. EXPORT LOGS
        // ═════════════════════════════════════════════════════════════

        // Méthode d'export des logs au format texte
        private string FormatLogEntry(LogEntry log)
        {
            string formattedDate =
                log.Timestamp.ToString("dd/MM/yyyy HH:mm:ss");

            return $"[{formattedDate}] [{log.Level}] {log.Message}" +
                   (string.IsNullOrWhiteSpace(log.Context)
                       ? ""
                       : $" ({log.Context})");
        }

        // Génère le contenu à exporter en filtrant les logs selon le filtre sélectionné
        public string GetLogsExportContent()
        {
            var logs = FilteredLogs.ToList();

            if (logs.Count == 0)
                return string.Empty;

            var lines = logs.Select(FormatLogEntry);

            return string.Join(
                Environment.NewLine
                + "----------------------------------------"
                + Environment.NewLine,
                lines
            );
        }
    }
}