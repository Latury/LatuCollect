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
using LatuCollect.Core.Services.Export;
using LatuCollect.Core.Services.Import;
using LatuCollect.UI.WinUI.Models.Logs;
using LatuCollect.UI.WinUI.ViewModels.Logs;
using LatuCollect.UI.WinUI.ViewModels.Settings;
using LatuCollect.UI.WinUI.ViewModels.TreeView;
using LatuCollect.UI.WinUI.ViewModels.Preview;
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

        private string _currentFolderPath = string.Empty;
        private string? _selectedFormat = null;

        private bool _isInitializing = false;

        private string _feedbackMessage = "";
        private bool _isFeedbackVisible;

        private CancellationTokenSource? _searchCts;

        private bool _isBusy = false;

        private bool _isLimitReached;

        private bool _isDeveloperMode;
        private bool _hasShownPartialWarning = false;

        private string _selectedLogLevel = "Info";
        private string _selectedTheme = "Dark";

        private bool _isRefreshingExclusions = false;

        // 🔥 Protection sélection massive
        private bool _isBulkSelectionUpdating = false;

        public bool IsBulkSelectionUpdating =>
            _isBulkSelectionUpdating;

        // 🔥 Conservation état ouvert TreeView
        private readonly HashSet<string> _expandedPaths = new();

        // ═════════════════════════════════════════════════════════════
        // 2. SERVICES & CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        private readonly IFileImportService _importService;
        private readonly IFileExportService _exportService;
        private readonly ILogService _logger;

        private readonly AppConfig _config;
        private readonly IConfigurationService _configurationService;

        private UserConfig _userConfig;

        private readonly LogsViewModel _logsViewModel;

        private readonly TreeViewViewModel _treeViewViewModel;

        private readonly SettingsViewModel _settingsViewModel;

        private readonly PreviewViewModel _previewViewModel;

        // ═════════════════════════════════════════════════════════════
        // 3. CONSTANTES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_NODES = 5000;
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
            _logger.MinimumLevel = level switch
            {
                "Warning" => LogLevel.Warning,
                "Error" => LogLevel.Error,
                _ => LogLevel.Info
            };
        }

        // Récupère le niveau de log actuel
        private string GetCurrentLogLevel()
        {
            return _logger.MinimumLevel switch
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
                        RequestPreviewRefresh();
                    }
                }
            }
        }


        private ExclusionItem? _selectedExclusion;
        // Exclusion sélectionnée dans la liste (pour suppression)
        public ExclusionItem? SelectedExclusion
        {
            get => _selectedExclusion;
            set
            {
                if (SetProperty(ref _selectedExclusion, value))
                {
                    OnPropertyChanged(nameof(CanRemoveExclusion));
                }
            }
        }

        // Autorisation suppression exclusion
        public bool CanRemoveExclusion
        {
            get
            {
                // Rien sélectionné
                if (SelectedExclusion == null)
                    return false;

                // ✔ Exclusion normale
                if (!SelectedExclusion.IsProtected)
                    return true;

                // 🔒 Protégée → mode développeur requis
                return IsDeveloperMode;
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
                    OnPropertyChanged(nameof(IsDeveloperModeEnabled));

                    // 🔥 IMPORTANT
                    OnPropertyChanged(nameof(CanRemoveExclusion));
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
            get => _previewViewModel.PreviewText;
            set => _previewViewModel.PreviewText = value;
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
        // 11. STATISTIQUES
        // ═════════════════════════════════════════════════════════════

        public int FileCount
        {
            get => _previewViewModel.FileCount;
            set => _previewViewModel.FileCount = value;
        }

        public long TotalLines
        {
            get => _previewViewModel.TotalLines;
            set => _previewViewModel.TotalLines = value;
        }

        public long TotalCharacters
        {
            get => _previewViewModel.TotalCharacters;
            set => _previewViewModel.TotalCharacters = value;
        }

        public long TotalSize
        {
            get => _previewViewModel.TotalSize;
            set => _previewViewModel.TotalSize = value;
        }

        // ═════════════════════════════════════════════════════════════
        // 12. ARBORESCENCE & RECHERCHE
        // ═════════════════════════════════════════════════════════════

        public ObservableCollection<UiFileNode> Tree
        {
            get => _treeViewViewModel.Tree;
        }

        public ObservableCollection<UiFileNode> FilteredTree
        {
            get => _treeViewViewModel.FilteredTree;
        }

        private readonly ObservableCollection<object>
            _groupedExclusions = new();

        public string SearchText
        {
            get => _treeViewViewModel.SearchText;
            set
            {
                if (_treeViewViewModel.SearchText != value)
                {
                    _treeViewViewModel.SearchText = value;

                    OnPropertyChanged();

                    _ = DebounceFilterAsync();
                }
            }
        }

        public bool HasSearchResult
        {
            get => _treeViewViewModel.HasSearchResult;
            set
            {
                if (_treeViewViewModel.HasSearchResult != value)
                {
                    _treeViewViewModel.HasSearchResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsSearchVisible
        {
            get => _treeViewViewModel.IsSearchVisible;
            set
            {
                if (_treeViewViewModel.IsSearchVisible != value)
                {
                    _treeViewViewModel.IsSearchVisible = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsLimitReached
        {
            get => _isLimitReached;
            set => SetProperty(ref _isLimitReached, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 13. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public MainViewModel()
        {
            // 🔧 Logger
            _logger = new LogService();
            _logsViewModel = new LogsViewModel(_logger);
            _logger.Info("MainViewModel initialisé");

            // 🔥 Rafraîchissement automatique UI lors de mise à jour logs
            _logger.LogsUpdated += (s, e) =>
            {
                RefreshFilteredLogs();
            };

            // ⚙️ Configuration
            _config = new AppConfig();
            RebuildGroupedExclusions();
            _configurationService = new ConfigurationService();
            _userConfig = new UserConfig();

            // 🖥️ ViewModels spécialisés
            _treeViewViewModel = new TreeViewViewModel();

            _previewViewModel = new PreviewViewModel();

            _settingsViewModel =
                new SettingsViewModel(
                    _userConfig,
                    _config,
                    _logger);

            // 📂 Services Core
            _importService = new FileImportService(_config);
            _exportService = new FileExportService();

            // 🔄 Chargement config async
            _ = LoadConfigurationAsync();

            // 🎯 État initial UI
            PreviewText = string.Empty;
            CurrentState = UiState.Empty;
            CurrentFolderPath = string.Empty;
            SelectedFormat = null;
        }

        // ═════════════════════════════════════════════════════════════
        // 14. COMMANDES UI
        // ═════════════════════════════════════════════════════════════

        // Chargement dossier
        public async Task LoadTreeAsync(string path)
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                _previewViewModel.HasShownPartialWarning = false;

                _logger.Info("Chargement du dossier lancé");
                _logger.Info("Dossier sélectionné", path);

                CurrentState = UiState.Loading;

                // 🔥 Import
                var importResult = await _importService.LoadTreeAsync(path);

                // 🔥 Sauvegarde état ouvert avant rebuild
                SaveExpandedNodes();

                // 🔥 Reset propre
                Tree.Clear();
                FilteredTree.Clear();

                // ❌ Aucun résultat
                if (importResult.Nodes.Count == 0)
                {
                    _logger.Warning("Aucun fichier trouvé dans le dossier", path);

                    CurrentState = UiState.Error;
                    ErrorMessage = "Dossier introuvable ou vide.";

                    OnPropertyChanged(nameof(IsTreeEmpty));
                    return;
                }

                // ✔ Conversion vers UI progressive
                int batchCount = 0;

                foreach (var coreNode in importResult.Nodes)
                {
                    Tree.Add(ConvertToUiNode(coreNode));

                    batchCount++;

                    // 🔥 Laisse respirer WinUI régulièrement
                    if (batchCount >= 25)
                    {
                        batchCount = 0;

                        await Task.Yield();
                    }
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
                await RefreshPreviewAsync(
                     _previewViewModel.PreviewRequestId);
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
        // 15. EXPORT
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

        public bool CanCopy =>
    CurrentState == UiState.Ready &&
    !string.IsNullOrWhiteSpace(PreviewText);

        public bool IsPreviewEmpty =>
    CurrentState == UiState.Empty;

        public bool CanExport =>
    !IsPreviewEmpty &&
    !string.IsNullOrWhiteSpace(SelectedFormat);

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

                var files = _treeViewViewModel.GetSelectedFiles();

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

                var files = _treeViewViewModel.GetSelectedFiles();

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

                if (data.IsPartial &&
                    !_previewViewModel.HasShownPartialWarning)
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
        // 16. PREVIEW
        // ═════════════════════════════════════════════════════════════

        private async Task RefreshPreviewAsync(int requestId)
        {


            _previewViewModel.IsPreviewLoading = true;

            try
            {
                var files = _treeViewViewModel.GetSelectedFiles();

                // 🟡 Aucun fichier sélectionné
                if (files.Count == 0)
                {
                    _logger.Warning("Aucun fichier sélectionné pour le preview");

                    _previewViewModel.LastSelectionSignature = string.Empty;
                    _previewViewModel.LastIsMarkdown = false;

                    // ✅ Reset preview
                    PreviewText = "Aucun fichier sélectionné...";

                    _previewViewModel.ResetPreview();

                    CurrentState = UiState.Empty;

                    OnPropertyChanged(nameof(CanCopy));
                    OnPropertyChanged(nameof(IsPreviewEmpty));
                    OnPropertyChanged(nameof(CanExport));
                    OnPropertyChanged(nameof(HasEmptyFiles));

                    return;
                }

                bool isMarkdown = SelectedFormat == ".md";
                string currentSignature = BuildSelectionSignature(files);

                // 🔥 IMPORTANT : optimisation pour éviter de régénérer si même sélection + même format
                if (currentSignature ==
                        _previewViewModel.LastSelectionSignature &&
                    isMarkdown ==
                        _previewViewModel.LastIsMarkdown)
                {
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

                _previewViewModel.LastSelectionSignature =
                    currentSignature;

                _previewViewModel.LastIsMarkdown =
                    isMarkdown;

                // 🔥 RESET message partiel si nouvelle sélection
                _previewViewModel.HasShownPartialWarning = false;

                CurrentState = UiState.Loading;

                var data = await _exportService.BuildContentWithStatsAsync(
                    files,
                    isMarkdown,
                    ExportMode
                );

                // 🔥 Vérification versioning async
                if (requestId != _previewViewModel.PreviewRequestId)
                {
                    _logger.Info(
                        "Preview ignoré",
                        $"RequestId obsolète: {requestId}");

                    return;
                }

                // 🔥 Avertissement partiel (une seule fois par session)
                if (data.IsPartial &&
                    !_previewViewModel.HasShownPartialWarning)
                {
                    _hasShownPartialWarning = true;
                    await ShowFeedbackAsync(data.PartialMessage);
                }

                // 🔥 Limitation de l’aperçu pour éviter les problèmes de performance
                const int MAX_PREVIEW_LENGTH = 100_000;

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

                _previewViewModel.ApplyStatistics(
                    stats.FileCount,
                    stats.TotalLines,
                    stats.TotalCharacters,
                    stats.TotalSizeBytes);

                _logger.Info(
                    "Preview généré avec succès",
                    $"Fichiers: {files.Count}");

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
                _previewViewModel.IsPreviewLoading = false;
            }
        }

        // Méthode d’accès aux fichiers sélectionnés
        public async Task RefreshPreviewForTestsAsync()
        {
            await RefreshPreviewAsync(
                _previewViewModel.PreviewRequestId);
        }

        // 🔥 TESTS — attente fin initialisation async
        internal async Task WaitForInitializationAsync()
        {
            while (_isInitializing)
            {
                await Task.Delay(10);
            }
        }

        // 🔥 TESTS — accès fichiers sélectionnés
        internal void SaveExpandedNodesForTests()
        {
            SaveExpandedNodes();
        }

        // 🔥 TESTS — conversion UI
        internal UiFileNode ConvertToUiNodeForTests(
            CoreFileNode coreNode)
        {
            return ConvertToUiNode(coreNode);
        }

        // 🔥 TESTS — état expansion runtime
        internal List<string> GetExpandedPathsForTests()
        {
            return _expandedPaths.ToList();
        }

        // 🔥 TESTS — config utilisateur
        internal UserConfig GetUserConfigForTests()
        {
            return _userConfig;
        }

        // 🔥 TESTS — exclusions groupées
        internal List<object> GetGroupedExclusionsForTests()
        {
            return _groupedExclusions.ToList();
        }

        // 🔥 TESTS — accès logger
        internal ILogService GetLoggerForTests()
        {
            return _logger;
        }

        private string BuildSelectionSignature(List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
                return string.Empty;

            var ordered = filePaths.OrderBy(p => p);

            return string.Join("|", ordered);
        }

        // ═════════════════════════════════════════════════════════════
        // 17. FEEDBACK
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
        // 18. SÉLECTION & ARBORESCENCE
        // ═════════════════════════════════════════════════════════════

        public bool IsTreeEmpty =>
            string.IsNullOrWhiteSpace(CurrentFolderPath) || Tree.Count == 0;

        public Task OnNodeSelectionChanged(
    UiFileNode node,
    bool isChecked)
        {
            // 🔥 Ignore événements pendant propagation massive
            if (_isBulkSelectionUpdating)
                return Task.CompletedTask;

            if (node == null)
                return Task.CompletedTask;

            // 🔥 IMPORTANT
            // verrou global sélection massive
            _isBulkSelectionUpdating = true;

            try
            {
                // 📁 propagation récursive
                SetNodeSelection(node, isChecked);
            }
            finally
            {
                // 🔥 libération IMMÉDIATE après propagation
                _isBulkSelectionUpdating = false;
            }

            // 🔥 Preview découplé du pipeline sélection
            RequestPreviewRefresh();

            return Task.CompletedTask;
        }

        // Propagation récursive sélection/désélection
        private void SetNodeSelection(
            UiFileNode node,
            bool isSelected)
        {
            _treeViewViewModel.SetNodeSelection(
                node,
                isSelected);
        }

        // Ajout d’un node de l’arbre (exclusion)
        public async Task AddExclusionFromNode(UiFileNode node)
        {
            if (node == null)
                return;

            try
            {
                _logger.Info("Ajout exclusion depuis TreeView", node.Path);

                string path = node.Path;
                var displayName = System.IO.Path.GetFileName(path);

                if (string.IsNullOrWhiteSpace(path))
                    return;

                // 🔍 Vérifie si déjà présent
                if (_config.ExcludedFolders.Any(e =>
                    string.Equals(e.Name, path, StringComparison.OrdinalIgnoreCase)))
                {
                    await ShowFeedbackAsync($"⚠ Déjà exclu : {displayName}");
                    return;
                }
                // ✔ Ajout exclusion
                var item = new ExclusionItem(
    path,
    false,
    node.IsDirectory
);

                _config.ExcludedFolders.Add(item);

                if (!_userConfig.ExcludedFolders.Any(e =>
                    string.Equals(e.Name, path, StringComparison.OrdinalIgnoreCase)))
                {
                    _userConfig.ExcludedFolders.Add(item);
                }

                OnPropertyChanged(nameof(Config));

                RefreshExclusionsUi();

                // 🔥 IMPORTANT : suppression immédiate dans l’arbre
                RemoveNodeFromTree(node);

                // 💾 Sauvegarde
                await SaveConfigurationAsync();

                // ❌ SUPPRIMÉ → plus de reload complet
                // await LoadTreeAsync(CurrentFolderPath);

                await ShowFeedbackAsync($"✔ Exclu : {displayName}");
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur exclusion depuis TreeView", ex.Message);
                await ShowFeedbackAsync("❌ Erreur lors de l'exclusion");
            }
        }

        // Ajout d’un node de l’arbre (exclusion protégée)
        public async Task AddProtectedExclusionFromNode(UiFileNode node)
        {
            if (node == null)
                return;

            try
            {
                _logger.Info("Ajout exclusion PROTÉGÉE", node.Path);

                string path = node.Path;
                var displayName = System.IO.Path.GetFileName(path);

                if (string.IsNullOrWhiteSpace(path))
                    return;

                // 🔍 Vérifie si déjà présent
                if (_config.ExcludedFolders.Any(e =>
                    string.Equals(e.Name, path, StringComparison.OrdinalIgnoreCase)))
                {
                    await ShowFeedbackAsync($"⚠ Déjà exclu : {displayName}");
                    return;
                }

                // ✔ Ajout exclusion PROTÉGÉE
                var item = new ExclusionItem(
    path,
    true,
    Directory.Exists(path)
);

                _config.ExcludedFolders.Add(item);

                // 🔥 Évite doublon config utilisateur
                if (!_userConfig.ExcludedFolders.Any(e =>
                    string.Equals(e.Name, path, StringComparison.OrdinalIgnoreCase)))
                {
                    _userConfig.ExcludedFolders.Add(item);
                }

                // 🔄 Refresh UI
                OnPropertyChanged(nameof(Config));

                RefreshExclusionsUi();

                // 🔥 Suppression immédiate dans l’arbre
                RemoveNodeFromTree(node);

                // 💾 Sauvegarde
                await SaveConfigurationAsync();

                await ShowFeedbackAsync($"🔒 Exclusion protégée : {displayName}");
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur exclusion protégée", ex.Message);

                await ShowFeedbackAsync("❌ Erreur");
            }
        }

        // Suppression d’un node de l’arbre (exclusion)
        public async Task RemoveExclusionFromNode(UiFileNode node)
        {
            if (node == null)
                return;

            try
            {
                _logger.Info("Suppression exclusion", node.Path);

                string path = node.Path;
                var displayName = System.IO.Path.GetFileName(path);

                var existing = _config.ExcludedFolders
                    .FirstOrDefault(e =>
                        string.Equals(e.Name, path, StringComparison.OrdinalIgnoreCase));

                if (existing == null)
                {
                    await ShowFeedbackAsync($"⚠ Non trouvé : {displayName}");
                    return;
                }

                // 🔒 Protégé → mode développeur requis
                if (existing.IsProtected && !IsDeveloperMode)
                {
                    await ShowFeedbackAsync($"🔒 Mode développeur requis : {displayName}");
                    return;
                }

                var configItemsToRemove = _config.ExcludedFolders
                    .Where(e =>
                        string.Equals(e.Name, existing.Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var item in configItemsToRemove)
                {
                    _config.ExcludedFolders.Remove(item);
                }

                var userConfigItemsToRemove = _userConfig.ExcludedFolders
                    .Where(e =>
                        string.Equals(e.Name, existing.Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var item in userConfigItemsToRemove)
                {
                    _userConfig.ExcludedFolders.Remove(item);
                }

                OnPropertyChanged(nameof(Config));

                await SaveConfigurationAsync();

                await ShowFeedbackAsync($"♻️ Inclus : {displayName}");
            }
            catch (Exception ex)
            {
                _logger.Error("Erreur suppression exclusion", ex.Message);
                await ShowFeedbackAsync("❌ Erreur suppression");
            }
        }

        // Suppression d’un node de l’arbre (exclusion)
        internal void RemoveNodeFromTree(UiFileNode node)
        {
            if (node == null)
                return;

            // 🔹 1. Suppression directe
            if (node.Parent == null)
            {
                Tree.Remove(node);
            }
            else
            {
                node.Parent.Children.Remove(node);
            }

            // 🔹 2. Réapplique filtre uniquement
            ApplyFilter();

            OnPropertyChanged(nameof(IsTreeEmpty));
        }

        // Sauvegarde les dossiers ouverts
        private void SaveExpandedNodes()
        {
            _expandedPaths.Clear();

            foreach (var node in Tree)
            {
                SaveExpandedNodesRecursive(node);
            }
        }

        // Sauvegarde récursive des paths ouverts
        private void SaveExpandedNodesRecursive(UiFileNode node)
        {
            if (node.IsExpanded)
            {
                _expandedPaths.Add(node.Path);
            }

            foreach (var child in node.Children)
            {
                SaveExpandedNodesRecursive(child);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 19. FILTRAGE & RECHERCHE
        // ═════════════════════════════════════════════════════════════

        internal void ApplyFilter()
        {
            _logger.Info(
                "ApplyFilter",
                $"Search: '{SearchText}' | Tree: {Tree.Count} | FilteredBefore: {FilteredTree.Count}");

            FilteredTree.Clear();

            // 🔥 RESET EXPANSION SI PAS DE RECHERCHE
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var node in Tree)
                {
                    _treeViewViewModel.SetVisibilityRecursive(node, true);
                    FilteredTree.Add(node);
                }

                HasSearchResult = true;

                _logger.Info(
                    "ApplyFilter terminé",
                    $"FilteredAfter: {FilteredTree.Count}");

                return;
            }

            bool hasResult = false;

            foreach (var node in Tree)
            {
                bool visible = _treeViewViewModel.ApplyFilterRecursive(node);

                if (visible)
                {
                    FilteredTree.Add(node);
                    hasResult = true;
                }
            }

            HasSearchResult = hasResult;

            _logger.Info(
                "ApplyFilter terminé",
                $"FilteredAfter: {FilteredTree.Count}");
        }

        // Debounce recherche
        private async Task DebounceFilterAsync()
        {
            _searchCts?.Cancel();
            _searchCts?.Dispose();

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

        // Demande un refresh preview non bloquant
        private void RequestPreviewRefresh()
        {
            int requestId = ++_previewViewModel.PreviewRequestId;

            _ = DebouncePreviewAsync(requestId);
        }

        // Debounce preview pour éviter refresh massifs
        private async Task DebouncePreviewAsync(int requestId)
        {
            // 🔥 Attente courte stabilité utilisateur
            await Task.Delay(300);

            // 🔥 Vérification versioning async pour éviter refresh obsolète
            if (requestId != _previewViewModel.PreviewRequestId)
                return;

            await RefreshPreviewAsync(requestId);

            _previewViewModel.LastCompletedPreviewId = requestId;
        }

        // ═════════════════════════════════════════════════════════════
        // 20. CONVERSION UI ↔ CORE
        // ═════════════════════════════════════════════════════════════

        private UiFileNode ConvertToUiNode(CoreFileNode coreNode)
        {
            var uiNode = new UiFileNode
            {
                Name = coreNode.Name,
                Path = coreNode.Path,

                // 🔥 IMPORTANT
                // Conserve le vrai type du node
                IsDirectory = coreNode.IsDirectory,

                // 🔥 Restaure état ouvert après rebuild
                IsExpanded = _expandedPaths.Contains(coreNode.Path)
            };

            foreach (var child in coreNode.Children)
            {
                var childNode = ConvertToUiNode(child);

                childNode.Parent = uiNode; // 🔥 IMPORTANT

                uiNode.Children.Add(childNode);
            }

            // 🔥 IMPORTANT
            // Synchronisation runtime expansion
            uiNode.ExpandedChanged += OnNodeExpandedChanged;

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

                    // 🔥 IMPORTANT
                    // Conserve le vrai type du node
                    IsDirectory = uiNode.IsDirectory,

                    // 🔥 IMPORTANT
                    IsSelected = uiNode.IsSelected && !uiNode.IsDirectory
                };

                foreach (var child in ConvertToCoreNodes(uiNode.Children))
                {
                    coreNode.Children.Add(child);
                }

                result.Add(coreNode);
            }

            return result;
        }

        // Synchronisation runtime expansion
        private void OnNodeExpandedChanged(UiFileNode node)
        {
            _treeViewViewModel.OnNodeExpandedChanged(node);
        }

        // Reset expansion runtime
        private void ClearExpandedPaths()
        {
            _expandedPaths.Clear();
        }

        // ═════════════════════════════════════════════════════════════
        // 21. CONFIGURATION UTILISATEUR
        // ═════════════════════════════════════════════════════════════

        // Rebuild de la liste groupée des exclusions pour l’UI
        private void RebuildGroupedExclusions()
        {
            _groupedExclusions.Clear();

            var list = Config.ExcludedFolders;

            if (list == null || list.Count == 0)
                return;

            var protectedItems =
                list.Where(e => e.IsProtected).ToList();

            var normalItems =
                list.Where(e => !e.IsProtected).ToList();

            // 🔒 Protégés
            if (protectedItems.Count > 0)
            {
                _groupedExclusions.Add("🔒 Protégés");

                foreach (var item in protectedItems)
                {
                    _groupedExclusions.Add(item);
                }
            }

            // 📁 Normaux
            if (normalItems.Count > 0)
            {
                _groupedExclusions.Add("📁 Normaux");

                foreach (var item in normalItems)
                {
                    _groupedExclusions.Add(item);
                }
            }
        }

        // Rafraîchit l’UI des exclusions (après ajout/suppression)
        public void RefreshExclusionsUi()
        {
            RebuildGroupedExclusions();

            OnPropertyChanged(nameof(CanRemoveExclusion));
        }

        // Chargement de la configuration utilisateur au lancement
        private async Task LoadConfigurationAsync()
        {
            try
            {
                _isInitializing = true;
                _logger.Info("Chargement configuration utilisateur");

                _userConfig = await _configurationService.LoadAsync() ?? new UserConfig();

                // 🔥 IMPORTANT : restauration état ouvert TreeView
                _expandedPaths.Clear();

                foreach (var path in _userConfig.ExpandedPaths)
                {
                    _expandedPaths.Add(path);
                }

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
                        _logger.Warning(
                            "Dossier introuvable au lancement",
                            _userConfig.LastOpenedFolder);

                        // Reset propre
                        _userConfig.LastOpenedFolder = string.Empty;

                        await ShowFeedbackAsync(
                            "⚠ Le dernier dossier n'existe plus");

                        CurrentState = UiState.Empty;

                        // 🔥 Sauvegarde config nettoyée
                        await SaveConfigurationAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(
                    "Erreur chargement configuration",
                    ex.Message);
            }
            finally
            {
                _isInitializing = false;
            }
        }

        // Sauvegarde de la configuration utilisateur (lancée à la fermeture et après chaque modification importante)
        public async Task SaveConfigurationAsync()
        {
            try
            {
                if (_userConfig == null)
                    _userConfig = new UserConfig();

                _userConfig.DefaultFormat = SelectedFormat ?? ".txt";

                _userConfig.IsDeveloperMode = IsDeveloperMode;

                _userConfig.LastOpenedFolder = CurrentFolderPath;

                _userConfig.AutoLoadLastFolder =
                    _userConfig.AutoLoadLastFolder;

                _userConfig.ExportMode = ExportMode;

                SaveExpandedNodes();

                _userConfig.ExcludedFolders =
                    _config.ExcludedFolders.ToList();

                _userConfig.LogLevel =
                    GetCurrentLogLevel();

                // 🔥 IMPORTANT : sauvegarde des paths ouverts
                _userConfig.ExpandedPaths =
                _expandedPaths.ToList();

                await _configurationService.SaveAsync(_userConfig);

                _logger.Info("Configuration sauvegardée");
            }
            catch (Exception ex)
            {
                _logger.Error(
                    "Erreur sauvegarde configuration",
                    ex.Message);
            }
        }

        // Reset runtime dossier chargé
        private void ResetLoadedFolderState()
        {
            // 🔥 Incrémente pour invalider tous les previews en cours
            _previewViewModel.PreviewRequestId++;

            _previewViewModel.LastCompletedPreviewId = 0;

            _previewViewModel.IsPreviewLoading = false;
            _isBulkSelectionUpdating = false;

            // 🔥 Reset signature sélection pour forcer refresh preview
            _previewViewModel.LastSelectionSignature =
                string.Empty;

            _previewViewModel.LastIsMarkdown =
                false;

            // 🔥 Reset avertissement partiel
            _previewViewModel.HasShownPartialWarning = false;

            // 🔥 Reset expansion TreeView
            ClearExpandedPaths();

            // 🔥 Reset recherche
            SearchText = string.Empty;
            HasSearchResult = true;

            // 🔥 Reset dossier
            CurrentFolderPath = string.Empty;

            // 🔥 Reset arbre
            Tree.Clear();
            FilteredTree.Clear();

            // 🔥 Reset preview
            PreviewText = string.Empty;

            // 🔥 Reset statistiques
            FileCount = 0;
            TotalLines = 0;
            TotalCharacters = 0;
            TotalSize = 0;

            // 🔥 Reset état UI
            CurrentState = UiState.Empty;

            // 🔥 Refresh bindings
            OnPropertyChanged(nameof(IsTreeEmpty));
            OnPropertyChanged(nameof(CanCopy));
            OnPropertyChanged(nameof(IsPreviewEmpty));
            OnPropertyChanged(nameof(CanExport));
            OnPropertyChanged(nameof(HasEmptyFiles));
        }

        public async Task ResetConfigurationAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                _previewViewModel.HasShownPartialWarning = false;
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

                // 🔥 Reset runtime UI
                ResetLoadedFolderState();

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

        public async Task ResetSettingsAsync()
        {
            await ResetConfigurationAsync();
        }

        // ═════════════════════════════════════════════════════════════
        // 22. LOGS
        // ═════════════════════════════════════════════════════════════

        // Rafraîchit la liste filtrée des logs (après changement de filtre ou nouveau log)
        private void RefreshFilteredLogs()
        {
            OnPropertyChanged(nameof(FilteredLogs));
        }

        // Génère le contenu à exporter en filtrant les logs selon le filtre sélectionné
        public string GetLogsExportContent()
        {
            return _logsViewModel.GetLogsExportContent();
        }

        // Collection complète de logs (non filtrée) pour affichage dans UI ou export complet
        public ReadOnlyObservableCollection<LogEntry> Logs =>
            _logsViewModel.Logs;

        // Indicateur d’erreurs présentes dans les logs (pour affichage badge ou alerte)
        public bool HasLogErrors =>
            _logsViewModel.HasLogErrors;
        
        // Nombre d’erreurs présentes dans les logs (pour affichage badge ou alerte)
        public int LogErrorCount =>
            _logsViewModel.LogErrorCount;

        // Collection de logs filtrée selon le filtre sélectionné (pour affichage dans UI)
        public LogFilter SelectedLogFilter
        {
            get => _logsViewModel.SelectedLogFilter;
            set => _logsViewModel.SelectedLogFilter = value;
        }

        // Collection de logs filtrée selon le filtre sélectionné (pour affichage dans UI)
        public IEnumerable<LogEntry> FilteredLogs =>
            _logsViewModel.FilteredLogs;
    }
}