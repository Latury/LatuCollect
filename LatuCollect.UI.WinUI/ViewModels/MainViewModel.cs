/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI.WinUI.ViewModels                                        ║
║  Fichier : MainViewModel.cs                                          ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer l’état de l’interface utilisateur principale                  ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gestion de l’arborescence affichée                                ║
║  - Gestion des sélections utilisateur                                ║
║  - Gestion des commandes UI (charger, exporter, copier)              ║
║  - Interaction avec les services Core                                ║
║                                                                      ║
║    NOTE IMPORTANTE (ALC) :                                           ║
║  Ce ViewModel contient encore de la logique métier (temporaire)      ║
║  → Doit être déplacée progressivement vers le Core                   ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - FileImportService (Core)                                          ║
║  - FileExportService (Core)                                          ║
║  - FileNode (UI Model)                                               ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using CommunityToolkit.Mvvm.ComponentModel;
using LatuCollect.Core.Configuration;
using LatuCollect.Core.Configuration.Interfaces;
using LatuCollect.Core.Configuration.Models;
using LatuCollect.Core.Configuration.Services;
using LatuCollect.Core.Logging.Interfaces;
using LatuCollect.Core.Logging.Models;
using LatuCollect.Core.Logging.Services;
using LatuCollect.Core.Services.Collection;
using LatuCollect.Core.Services.Export;
using LatuCollect.Core.Services.Import;
using LatuCollect.Core.Simulation;
using LatuCollect.UI.WinUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreFileNode = LatuCollect.Core.Models.FileNode;
using UiFileNode = LatuCollect.UI.WinUI.Models.FileNode;

namespace LatuCollect.UI.WinUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS — ÉTAT UI
        // ═════════════════════════════════════════════════════════════

        private string _previewText = string.Empty;
        private string _currentFolderPath = string.Empty;
        private string _searchText = string.Empty;
        private string? _selectedFormat = null;

        private string _lastSelectionSignature = string.Empty;
        private bool _lastIsMarkdown = false;
        private bool _isInitializing = false;

        private string _feedbackMessage = "";
        private bool _isFeedbackVisible;

        private bool _isSimulationEnabled = false;
        private string _selectedSimulationScenario = "Aucun";

        private bool _isBatchUpdating = false;
        private CancellationTokenSource? _searchCts;
        private bool _isPreviewLoading = false;
        private bool _isBusy = false;

        private bool _isSearchVisible;
        private bool _hasSearchResult = true;
        private bool _isLimitReached;

        private bool _isDeveloperMode;
        private bool _hasShownPartialWarning = false;

        private string _selectedLogLevel = "Info";
        private string _selectedTheme = "Dark";

        // ═════════════════════════════════════════════════════════════
        // 2. SERVICES & CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        private readonly FileImportService _importService;
        private readonly FileCollectionService _collectionService;
        private readonly FileExportService _exportService;
        private readonly ILogService _logger;

        private readonly AppConfig _config;
        private readonly IConfigurationService _configurationService;

        private UserConfig _userConfig;

        // ═════════════════════════════════════════════════════════════
        // 3. CONSTANTES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_NODES = 1000;
        private const int MAX_DEPTH = 10;

        // ═════════════════════════════════════════════════════════════
        // 4. ÉTAT GLOBAL UI
        // ═════════════════════════════════════════════════════════════

        public enum UiState
        {
            Loading,
            Ready,
            Empty,
            Error
        }

        private UiState _currentState = UiState.Ready;

        private string _errorMessage = string.Empty;

        // ═════════════════════════════════════════════════════════════
        // 5. ÉVÉNEMENTS
        // ═════════════════════════════════════════════════════════════

        public event Action<string>? ThemeChanged;

        // ═════════════════════════════════════════════════════════════
        // 6. LISTES UI (STATIC DATA)
        // ═════════════════════════════════════════════════════════════

        public List<string> LogLevels { get; } = new()
    {
        "Info",
        "Warning",
        "Error"
    };

        public List<string> Themes { get; } = new()
    {
        "Dark",
        "Light"
    };

        // ═════════════════════════════════════════════════════════════
        // 7. MÉTHODES PRIVÉES — LOG LEVEL
        // ═════════════════════════════════════════════════════════════

        // Applique le niveau de log dans le service
        private void ApplyLogLevel(string level)
        {
            if (_logger is not LogService logService)
                return;

            logService.MinimumLevel = level switch
            {
                "Warning" => LogLevel.Warning,
                "Error" => LogLevel.Error,
                _ => LogLevel.Info
            };
        }

        // Récupère le niveau de log actuel
        private string GetCurrentLogLevel()
        {
            if (_logger is not LogService logService)
                return "Info";

            return logService.MinimumLevel switch
            {
                LogLevel.Warning => "Warning",
                LogLevel.Error => "Error",
                _ => "Info"
            };
        }

        // ═════════════════════════════════════════════════════════════
        // 8. PROPRIÉTÉS UI — ÉTAT GLOBAL
        // ═════════════════════════════════════════════════════════════

        public UiState CurrentState
        {
            get => _currentState;
            set
            {
                if (SetProperty(ref _currentState, value))
                {
                    OnPropertyChanged(nameof(IsLoading));
                    OnPropertyChanged(nameof(IsReady));
                    OnPropertyChanged(nameof(HasError));
                    OnPropertyChanged(nameof(IsPreviewEmpty));
                    OnPropertyChanged(nameof(IsEmpty));
                    OnPropertyChanged(nameof(CanExport));
                    OnPropertyChanged(nameof(CanCopy));
                }
            }
        }

        public bool IsLoading => CurrentState == UiState.Loading;
        public bool IsReady => CurrentState == UiState.Ready;
        public bool HasError => CurrentState == UiState.Error;
        public bool IsEmpty => CurrentState == UiState.Empty;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 9. PROPRIÉTÉS UI — SETTINGS
        // ═════════════════════════════════════════════════════════════

        public AppConfig Config => _config;


        public string? SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                if (SetProperty(ref _selectedFormat, value))
                {
                    // 🔁 UI
                    OnPropertyChanged(nameof(CanExport));

                    if (!_isInitializing)
                    {
                        // 💾 config
                        _userConfig.DefaultFormat = value ?? ".txt";
                        _ = SaveConfigurationAsync();

                        // 🔄 refresh preview
                        _ = RefreshPreviewAsync();
                    }
                }
            }
        }

        public bool AutoLoadLastFolder
        {
            get => _userConfig.AutoLoadLastFolder;
            set
            {
                if (_userConfig.AutoLoadLastFolder != value)
                {
                    _userConfig.AutoLoadLastFolder = value;
                    _ = SaveConfigurationAsync();
                }
            }
        }

        public bool IsDeveloperMode
        {
            get => _isDeveloperMode;
            set
            {
                if (SetProperty(ref _isDeveloperMode, value))
                {
                    OnPropertyChanged(nameof(IsSimulationVisible));
                    OnPropertyChanged(nameof(IsDeveloperModeEnabled));
                }
            }
        }

        public bool IsDeveloperModeEnabled
        {
            get => IsDeveloperMode;
            set
            {
                if (IsDeveloperMode != value)
                {
                    IsDeveloperMode = value;
                    _ = SaveConfigurationAsync();
                }
            }
        }

        public string SelectedLogLevel
        {
            get => _selectedLogLevel;
            set
            {
                if (SetProperty(ref _selectedLogLevel, value))
                {
                    ApplyLogLevel(value);
                    _userConfig.LogLevel = value;
                    _ = SaveConfigurationAsync();
                }
            }
        }

        public string SelectedTheme
        {
            get => _selectedTheme;
            set
            {
                if (SetProperty(ref _selectedTheme, value))
                {
                    _userConfig.Theme = value;


                    ThemeChanged?.Invoke(value);

                    _ = SaveConfigurationAsync();
                }
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 10. PROPRIÉTÉS UI — DONNÉES
        // ═════════════════════════════════════════════════════════════

        public string PreviewText
        {
            get => _previewText;
            set => SetProperty(ref _previewText, value);
        }

        public string CurrentFolderPath
        {
            get => _currentFolderPath;
            set
            {
                if (SetProperty(ref _currentFolderPath, value))
                {
                    OnPropertyChanged(nameof(CurrentFolderPathDisplay));
                }
            }
        }

        public string CurrentFolderPathDisplay =>
            string.IsNullOrWhiteSpace(CurrentFolderPath)
                ? "Aucun dossier"
                : CurrentFolderPath;

        // ═════════════════════════════════════════════════════════════
        // 11. MODE DEV & SIMULATION
        // ═════════════════════════════════════════════════════════════

        public bool IsSimulationVisible => IsDeveloperMode;

        public bool IsSimulationEnabled
        {
            get => _isSimulationEnabled;
            set
            {
                if (SetProperty(ref _isSimulationEnabled, value))
                {
                    SimulationConfig.IsEnabled = value;
                    OnPropertyChanged(nameof(SimulationLabel));
                    _ = RefreshPreviewAsync();
                }
            }
        }

        public string SelectedSimulationScenario
        {
            get => _selectedSimulationScenario;
            set
            {
                if (SetProperty(ref _selectedSimulationScenario, value))
                {
                    SimulationConfig.Scenario = value;
                    OnPropertyChanged(nameof(SimulationLabel));
                    _ = RefreshPreviewAsync();
                }
            }
        }

        public string SimulationLabel =>
            !IsSimulationEnabled
                ? "🧪 Simulation : Désactivé"
                : SelectedSimulationScenario == "Aucun"
                    ? "🧪 Simulation : Activé"
                    : $"🧪 Simulation : {SelectedSimulationScenario}";

        public string DeveloperWarningMessage =>
            "⚠ Mode simulation\n\n" +
            "Ce mode est destiné aux tests.\n" +
            "Il peut provoquer des comportements instables.";


        // ═════════════════════════════════════════════════════════════
        // 12. STATISTIQUES
        // ═════════════════════════════════════════════════════════════

        private int _fileCount;
        public int FileCount
        {
            get => _fileCount;
            set => SetProperty(ref _fileCount, value);
        }

        private int _totalLines;
        public int TotalLines
        {
            get => _totalLines;
            set => SetProperty(ref _totalLines, value);
        }

        private int _totalCharacters;
        public int TotalCharacters
        {
            get => _totalCharacters;
            set => SetProperty(ref _totalCharacters, value);
        }

        private long _totalSize;
        public long TotalSize
        {
            get => _totalSize;
            set => SetProperty(ref _totalSize, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 13. ARBORESCENCE & RECHERCHE
        // ═════════════════════════════════════════════════════════════

        public ObservableCollection<UiFileNode> Tree { get; } = new();
        public ObservableCollection<UiFileNode> FilteredTree { get; } = new();

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    DebounceFilter();
                }
            }
        }

        public bool HasSearchResult
        {
            get => _hasSearchResult;
            set => SetProperty(ref _hasSearchResult, value);
        }

        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set => SetProperty(ref _isSearchVisible, value);
        }

        public bool IsLimitReached
        {
            get => _isLimitReached;
            set => SetProperty(ref _isLimitReached, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 14. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public MainViewModel()
        {
            // 🔧 Logger
            _logger = new LogService();
            _logger.Info("MainViewModel initialisé");

            // 🔁 Mise à jour UI logs
            if (_logger is LogService logService)
            {
                logService.LogsUpdated += (s, e) =>
                {
                    OnPropertyChanged(nameof(HasLogErrors));
                    OnPropertyChanged(nameof(Logs));
                    OnPropertyChanged(nameof(LogErrorCount));
                    OnPropertyChanged(nameof(FilteredLogs));
                };
            }

            // ⚙️ Configuration
            _config = new AppConfig();
            _configurationService = new ConfigurationService();
            _userConfig = new UserConfig();

            // 📂 Services Core
            _importService = new FileImportService(_config);
            _collectionService = new FileCollectionService();
            _exportService = new FileExportService();

            // 🔄 Chargement config async
            _ = LoadConfigurationAsync();

            // 🎯 État initial UI
            PreviewText = string.Empty;
            CurrentState = UiState.Empty;
            CurrentFolderPath = string.Empty;
            SelectedFormat = null;

            // 🧪 Simulation
            IsSimulationEnabled = false;
            SelectedSimulationScenario = "Aucun";
        }

        // ═════════════════════════════════════════════════════════════
        // 15. COMMANDES UI
        // ═════════════════════════════════════════════════════════════

        // Chargement dossier
        public async Task LoadTreeAsync(string path)
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                _hasShownPartialWarning = false;

                _logger.Info("Chargement du dossier lancé");
                _logger.Info("Dossier sélectionné", path);

                CurrentState = UiState.Loading;

                await Task.Delay(100);

                if (!await UiSimulationService.ApplyUiSimulationAsync(this))
                {
                    await RefreshPreviewAsync();
                    return;
                }

                // 🔥 Import
                var importResult = await _importService.LoadTreeAsync(path);

                // 🔥 Reset propre
                Tree.Clear();
                FilteredTree.Clear();
                OnPropertyChanged(nameof(IsTreeEmpty));

                // ❌ Aucun résultat
                if (importResult.Nodes.Count == 0)
                {
                    _logger.Warning("Aucun fichier trouvé dans le dossier", path);

                    CurrentState = UiState.Error;
                    ErrorMessage = "Dossier introuvable ou vide.";

                    OnPropertyChanged(nameof(IsTreeEmpty));
                    return;
                }

                // ✔ Conversion vers UI
                foreach (var coreNode in importResult.Nodes)
                {
                    Tree.Add(ConvertToUiNode(coreNode));
                }

                OnPropertyChanged(nameof(IsTreeEmpty));

                ApplyFilter();

                _logger.Info("Arborescence projet chargée", $"Nodes: {importResult.TotalNodes}");

                if (importResult.IsPartial)
                {
                    _logger.Warning("Import partiel", importResult.Message);
                    _ = ShowFeedbackAsync(importResult.Message);
                }

                CurrentFolderPath = path;
                OnPropertyChanged(nameof(IsTreeEmpty));

                _ = SaveConfigurationAsync();

                // ✅ ICI LA BONNE LOGIQUE
                await RefreshPreviewAsync();
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur lors du chargement du dossier", ex.Message);

                CurrentState = UiState.Error;
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Toggle recherche UI
        public void ToggleSearch()
        {
            IsSearchVisible = !IsSearchVisible;
        }

        // ═════════════════════════════════════════════════════════════
        // 16. EXPORT
        // ═════════════════════════════════════════════════════════════

        private string _exportMode = "normal";

        public string ExportMode
        {
            get => _exportMode;
            set
            {
                if (SetProperty(ref _exportMode, value))
                {
                    _userConfig.ExportMode = value;
                    _ = SaveConfigurationAsync();
                }
            }
        }

        public bool HasEmptyFiles => PreviewText.Contains("\n\n\n\n");

        public bool CanCopy => !string.IsNullOrWhiteSpace(PreviewText);

        public bool IsPreviewEmpty => CurrentState == UiState.Empty;

        public bool CanExport =>
            !IsPreviewEmpty &&
            SelectedFormat != null;

        public enum ExportCheckResult
        {
            Ok,
            NoSelection,
            EmptyFiles
        }

        public ExportCheckResult CheckExportState()
        {
            if (IsPreviewEmpty)
                return ExportCheckResult.NoSelection;

            if (HasEmptyFiles)
                return ExportCheckResult.EmptyFiles;

            return ExportCheckResult.Ok;
        }

        // Export sync
        public string GetExportContent()
        {
            if (IsBusy) return string.Empty;

            try
            {
                IsBusy = true;

                _logger.Info("Export lancé");

                var files = GetSelectedFiles();

                if (files.Count == 0)
                {
                    _logger.Warning("Export annulé : aucun fichier sélectionné");
                    return string.Empty;
                }

                const int MAX_FILES_EXPORT = 200;

                if (files.Count > MAX_FILES_EXPORT)
                {
                    _logger.Warning("Export annulé : trop de fichiers", files.Count.ToString());

                    return $"⚠ Trop de fichiers sélectionnés ({files.Count}).\n" +
                           $"Limite actuelle : {MAX_FILES_EXPORT} fichiers.\n\n" +
                           $"Réduis la sélection.";
                }

                bool isMarkdown = SelectedFormat == ".md";

                var data = _exportService.BuildContentWithStats(files, isMarkdown);

                _logger.Info("Export généré avec succès");

                return data.Content;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Export async
        public async Task<string> GetExportContentAsync()
        {
            if (IsBusy) return string.Empty;

            try
            {
                IsBusy = true;

                _logger.Info("Export async lancé");

                var files = GetSelectedFiles();

                if (files.Count == 0)
                {
                    _logger.Warning("Export annulé : aucun fichier sélectionné");
                    return string.Empty;
                }

                const int MAX_FILES_EXPORT = 200;

                if (files.Count > MAX_FILES_EXPORT)
                {
                    _logger.Warning("Export annulé : trop de fichiers", files.Count.ToString());

                    return $"⚠ Trop de fichiers sélectionnés ({files.Count}).\n" +
                           $"Limite actuelle : {MAX_FILES_EXPORT} fichiers.\n\n" +
                           $"Réduis la sélection.";
                }

                bool isMarkdown = SelectedFormat == ".md";

                var data = await _exportService.BuildContentWithStatsAsync(
    files,
    isMarkdown,
    ExportMode
);
                if (data.IsPartial && !_hasShownPartialWarning)
                {
                    _hasShownPartialWarning = true;
                    await ShowFeedbackAsync(data.PartialMessage);
                }

                _logger.Info("Export async généré avec succès");

                return data.Content;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 17. PREVIEW
        // ═════════════════════════════════════════════════════════════

        private async Task RefreshPreviewAsync()
        {
            // 🔒 Protection anti multi-appel
            if (_isPreviewLoading || _isBatchUpdating)
                return;

            _logger.Info("Génération du preview");

            _isPreviewLoading = true;

            try
            {
                var files = GetSelectedFiles();

                // 🟡 Aucun fichier sélectionné
                if (files.Count == 0)
                {
                    _logger.Warning("Aucun fichier sélectionné pour le preview");

                    _lastSelectionSignature = string.Empty;
                    _lastIsMarkdown = false;

                    // ✅ IMPORTANT : on affiche le message
                    PreviewText = "Aucun fichier sélectionné...";
                    CurrentState = UiState.Empty;

                    return;
                }

                bool isMarkdown = SelectedFormat == ".md";
                string currentSignature = BuildSelectionSignature(files);

                // 🔥 IMPORTANT : optimisation pour éviter de régénérer si même sélection + même format
                if (currentSignature == _lastSelectionSignature && isMarkdown == _lastIsMarkdown)
                {
                    // 🔥 IMPORTANT : ne pas forcer Ready si aucun fichier
                    if (files.Count == 0)
                    {
                        CurrentState = UiState.Empty;
                    }
                    else
                    {
                        CurrentState = UiState.Ready;
                    }

                    return;
                }

                _lastSelectionSignature = currentSignature;
                _lastIsMarkdown = isMarkdown;

                // 🔥 RESET message partiel si nouvelle sélection
                _hasShownPartialWarning = false;

                CurrentState = UiState.Loading;

                var data = await _exportService.BuildContentWithStatsAsync(
                    files,
                    isMarkdown,
                    ExportMode
                );

                // 🔥 message si export partiel (une seule fois)
                if (data.IsPartial && !_hasShownPartialWarning)
                {
                    _hasShownPartialWarning = true;
                    await ShowFeedbackAsync(data.PartialMessage);
                }

                // 🔥 Limitation de l’aperçu pour éviter les problèmes de performance
                const int MAX_PREVIEW_LENGTH = 100_000; // 100k caractères

                if (data.Content.Length > MAX_PREVIEW_LENGTH)
                {
                    PreviewText = data.Content.Substring(0, MAX_PREVIEW_LENGTH)
                        + "\n\n----------------------------------------\n"
                        + "⚠ Aperçu limité (contenu trop volumineux)";
                }
                else
                {
                    PreviewText = data.Content;
                }

                var stats = data.Stats;

                FileCount = stats.FileCount;
                TotalLines = stats.TotalLines;
                TotalCharacters = stats.TotalCharacters;
                TotalSize = stats.TotalSizeBytes;

                _logger.Info("Preview généré avec succès", $"Fichiers: {files.Count}");

                CurrentState = UiState.Ready;

                OnPropertyChanged(nameof(CanCopy));
                OnPropertyChanged(nameof(IsPreviewEmpty));
                OnPropertyChanged(nameof(CanExport));
                OnPropertyChanged(nameof(HasEmptyFiles));
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur preview", ex.Message);

                CurrentState = UiState.Error;
                ErrorMessage = ex.Message;
            }
            finally
            {
                _isPreviewLoading = false;
            }
        }

        private string BuildSelectionSignature(List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
                return string.Empty;

            var ordered = filePaths.OrderBy(p => p);

            return string.Join("|", ordered);
        }

        // ═════════════════════════════════════════════════════════════
        // 18. FEEDBACK
        // ═════════════════════════════════════════════════════════════

        public bool IsFeedbackVisible
        {
            get => _isFeedbackVisible;
            set => SetProperty(ref _isFeedbackVisible, value);
        }

        public string FeedbackMessage
        {
            get => _feedbackMessage;
            set => SetProperty(ref _feedbackMessage, value);
        }

        public async Task ShowFeedbackAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message) || message == "Aucun fichier sélectionné.")
                return;

            FeedbackMessage = message;
            IsFeedbackVisible = true;

            await Task.Delay(4500);

            IsFeedbackVisible = false;
        }

        // ═════════════════════════════════════════════════════════════
        // 19. SÉLECTION & ARBORESCENCE
        // ═════════════════════════════════════════════════════════════

        public bool IsTreeEmpty =>
            string.IsNullOrWhiteSpace(CurrentFolderPath) || Tree.Count == 0;

        // Click sur un node
        public async void HandleNodeClick(UiFileNode node)
        {
            // 🔒 BLOQUE tout si en cours
            if (_isBatchUpdating || _isPreviewLoading)
                return;

            try
            {
                _isBatchUpdating = true;

                bool newValue = node.IsSelected;

                // 🔽 descente
                SetNodeSelection(node, newValue);

                // 🔼 remontée COMPLÈTE
                UpdateParentSelectionRecursive(node);
            }
            finally
            {
                _isBatchUpdating = false;
            }

            // 🔒 attendre preview AVANT nouvelle action
            await RefreshPreviewAsync();
        }

        // Met à jour les parents
        private void UpdateParentSelectionRecursive(UiFileNode node)
        {
            var parent = node.Parent;

            while (parent != null)
            {
                bool anySelected = parent.Children.Any(c => c.IsSelected);

                // 🔥 règle : au moins un enfant = parent coché
                parent.IsSelected = anySelected;

                parent = parent.Parent;
            }
        }

        // Applique sélection récursive
        private void SetNodeSelection(UiFileNode node, bool isSelected)
        {
            node.IsSelected = isSelected;

            foreach (var child in node.Children)
            {
                SetNodeSelection(child, isSelected);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 20. FILTRAGE & RECHERCHE
        // ═════════════════════════════════════════════════════════════

        private void ApplyFilter()
        {
            // 🔁 Toujours travailler sur le vrai Tree
            FilteredTree.Clear();

            // 🔹 Pas de recherche → tout visible
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var node in Tree)
                {
                    SetVisibilityRecursive(node, true);
                    FilteredTree.Add(node);
                }

                HasSearchResult = true;
                return;
            }

            bool hasResult = false;

            foreach (var node in Tree)
            {
                bool visible = ApplyFilterRecursive(node);

                if (visible)
                {
                    FilteredTree.Add(node);
                    hasResult = true;
                }
            }

            HasSearchResult = hasResult;
        }

        // Applique visibilité récursive
        private bool ApplyFilterRecursive(UiFileNode node)
        {
            bool match = node.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            bool hasVisibleChild = false;

            foreach (var child in node.Children)
            {
                if (ApplyFilterRecursive(child))
                {
                    hasVisibleChild = true;
                }
            }

            bool isVisible = match || hasVisibleChild;

            node.IsVisible = isVisible;

            return isVisible;
        }

        // Applique visibilité récursive (sans logique de recherche, pour reset)
        private void SetVisibilityRecursive(UiFileNode node, bool visible)
        {
            node.IsVisible = visible;

            foreach (var child in node.Children)
            {
                SetVisibilityRecursive(child, visible);
            }
        }

        // Debounce recherche
        private async void DebounceFilter()
        {
            _searchCts?.Cancel();

            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            try
            {
                await Task.Delay(300, token);

                if (!token.IsCancellationRequested)
                {
                    ApplyFilter();
                }
            }
            catch (TaskCanceledException)
            {
                // Normal → ignoré
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 21. CONVERSION UI ↔ CORE
        // ═════════════════════════════════════════════════════════════

        private UiFileNode ConvertToUiNode(CoreFileNode coreNode)
        {
            var uiNode = new UiFileNode
            {
                Name = coreNode.Name,
                Path = coreNode.Path
            };

            foreach (var child in coreNode.Children)
            {
                var childNode = ConvertToUiNode(child);

                childNode.Parent = uiNode; // 🔥 IMPORTANT

                uiNode.Children.Add(childNode);
            }

            return uiNode;
        }

        private List<CoreFileNode> ConvertToCoreNodes(IEnumerable<UiFileNode> uiNodes)
        {
            var result = new List<CoreFileNode>();

            foreach (var uiNode in uiNodes)
            {
                var coreNode = new CoreFileNode
                {
                    Name = uiNode.Name,
                    Path = uiNode.Path,
                    IsSelected = uiNode.IsSelected && uiNode.Children.Count == 0
                };

                foreach (var child in ConvertToCoreNodes(uiNode.Children))
                {
                    coreNode.Children.Add(child);
                }

                result.Add(coreNode);
            }

            return result;
        }

        private List<string> GetSelectedFiles()
        {
            var coreNodes = ConvertToCoreNodes(Tree);
            return _collectionService.GetSelectedFiles(coreNodes);
        }

        // ═════════════════════════════════════════════════════════════
        // 22. CONFIGURATION UTILISATEUR
        // ═════════════════════════════════════════════════════════════

        private async Task LoadConfigurationAsync()
        {
            try
            {
                _isInitializing = true;
                _logger.Info("Chargement configuration utilisateur");

                _userConfig = await _configurationService.LoadAsync() ?? new UserConfig();
                SelectedTheme = _userConfig.Theme ?? "Dark";
                ThemeChanged?.Invoke(SelectedTheme);
                ApplyLogLevel(_userConfig.LogLevel);
                SelectedLogLevel = _userConfig.LogLevel;
                _userConfig.DefaultFormat = _userConfig.DefaultFormat;
                _userConfig.IsDeveloperMode = _userConfig.IsDeveloperMode;
                _userConfig.LastOpenedFolder = _userConfig.LastOpenedFolder;
                _userConfig.AutoLoadLastFolder = _userConfig.AutoLoadLastFolder;
                ExportMode = _userConfig.ExportMode ?? "normal";
                _config.ExcludedFolders.Clear();

                foreach (var item in _userConfig.ExcludedFolders)
                {
                    _config.ExcludedFolders.Add(item);
                }

                SelectedFormat = _userConfig.DefaultFormat;
                IsDeveloperMode = _userConfig.IsDeveloperMode;

                _logger.Info("Configuration chargée avec succès");

                if (_userConfig.AutoLoadLastFolder &&
    !string.IsNullOrWhiteSpace(_userConfig.LastOpenedFolder))
                {
                    if (Directory.Exists(_userConfig.LastOpenedFolder))
                    {
                        await LoadTreeAsync(_userConfig.LastOpenedFolder);
                    }
                    else
                    {
                        _logger.Warning("Dossier introuvable au lancement", _userConfig.LastOpenedFolder);

                        // Reset propre
                        _userConfig.LastOpenedFolder = string.Empty;

                        await ShowFeedbackAsync("⚠ Le dernier dossier n'existe plus");

                        CurrentState = UiState.Empty;

                        // 🔥 AJOUT ICI
                        await SaveConfigurationAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur chargement configuration", ex.Message);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        public async Task SaveConfigurationAsync()
        {
            try
            {
                if (_userConfig == null)
                    _userConfig = new UserConfig();

                _userConfig.DefaultFormat = SelectedFormat ?? ".txt";
                _userConfig.IsDeveloperMode = IsDeveloperMode;
                _userConfig.LastOpenedFolder = CurrentFolderPath;
                _userConfig.AutoLoadLastFolder = _userConfig.AutoLoadLastFolder;
                _userConfig.ExportMode = ExportMode;
                _userConfig.ExcludedFolders = _config.ExcludedFolders.ToList();
                _userConfig.LogLevel = GetCurrentLogLevel();

                await _configurationService.SaveAsync(_userConfig);

                _logger.Info("Configuration sauvegardée");
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur sauvegarde configuration", ex.Message);
            }
        }

        public async Task ResetConfigurationAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                _hasShownPartialWarning = false;
                _logger.Info("Reset configuration utilisateur");

                _userConfig = await _configurationService.ResetAsync();

                // 🔧 CONFIG
                _userConfig.DefaultFormat = _userConfig.DefaultFormat;
                _userConfig.IsDeveloperMode = _userConfig.IsDeveloperMode;

                // 🔥 IMPORTANT : on vide complètement
                _userConfig.LastOpenedFolder = string.Empty;
                _userConfig.AutoLoadLastFolder = false;

                _config.ExcludedFolders.Clear();

                foreach (var item in _userConfig.ExcludedFolders)
                {
                    _config.ExcludedFolders.Add(item);
                }

                // 🔧 SETTINGS
                SelectedFormat = _userConfig.DefaultFormat;
                IsDeveloperMode = _userConfig.IsDeveloperMode;
                ExportMode = _userConfig.ExportMode ?? "normal";

                // 🔥 RESET UI COMPLET

                CurrentFolderPath = string.Empty;

                Tree.Clear();
                FilteredTree.Clear();

                SearchText = string.Empty;
                HasSearchResult = true;

                PreviewText = string.Empty;

                CurrentState = UiState.Empty;

                // reset flags internes
                _hasShownPartialWarning = false;

                await ShowFeedbackAsync("Configuration réinitialisée");

                _logger.Info("Reset configuration terminé");
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur reset configuration", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async void ResetSettings()
        {
            await ResetConfigurationAsync();
        }

        // ═════════════════════════════════════════════════════════════
        // 23. LOGS
        // ═════════════════════════════════════════════════════════════

        public string GetLogsExportContent()
        {
            if (_logger is not LogService logService)
                return string.Empty;

            var logs = FilteredLogs.ToList();

            if (logs.Count == 0)
                return string.Empty;

            var lines = logs.Select(log =>
                $"[{log.Date}] [{log.Level}] {log.Message}" +
                (string.IsNullOrWhiteSpace(log.Context) ? "" : $" ({log.Context})")
            );

            return string.Join(
                Environment.NewLine + "----------------------------------------" + Environment.NewLine,
                lines
            );
        }

        public ReadOnlyObservableCollection<LogEntry> Logs
        {
            get
            {
                return (_logger as LogService)!.Logs;
            }
        }

        public bool HasLogErrors
        {
            get
            {
                if (_logger is LogService logService)
                {
                    return logService.Logs.Any(l => l.Level == LogLevel.Error);
                }

                return false;
            }
        }

        public int LogErrorCount
        {
            get
            {
                if (_logger is LogService logService)
                {
                    return logService.Logs.Count(l => l.Level == LogLevel.Error);
                }

                return 0;
            }
        }

        public enum LogFilter
        {
            All,
            Info,
            Warning,
            Error
        }

        private LogFilter _selectedLogFilter = LogFilter.All;

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

        public IEnumerable<LogEntry> FilteredLogs
        {
            get
            {
                if (_logger is not LogService logService)
                    return Enumerable.Empty<LogEntry>();

                return SelectedLogFilter switch
                {
                    LogFilter.Info => logService.Logs.Where(l => l.Level == LogLevel.Info),
                    LogFilter.Warning => logService.Logs.Where(l => l.Level == LogLevel.Warning),
                    LogFilter.Error => logService.Logs.Where(l => l.Level == LogLevel.Error),
                    _ => logService.Logs
                };
            }
        }
    }
}

