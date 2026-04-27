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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiFileNode = LatuCollect.UI.WinUI.Models.FileNode;
using CoreFileNode = LatuCollect.Core.Models.FileNode;

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

        private bool _isAllSelected;
        private bool _isSearchVisible;
        private bool _hasSearchResult = true;
        private bool _isLimitReached;

        private bool _isDeveloperMode;


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

        private string _errorMessage = string.Empty;

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
        // 5. PROPRIÉTÉS UI — SETTINGS
        // ═════════════════════════════════════════════════════════════

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

        public bool AutoLoadLastFolder
        {
            get => _config.AutoLoadLastFolder;
            set
            {
                if (_config.AutoLoadLastFolder != value)
                {
                    _config.AutoLoadLastFolder = value;
                    _ = SaveConfigurationAsync();
                }
            }
        }

        public string DefaultFormat
        {
            get => _config.DefaultFormat;
            set
            {
                if (_config.DefaultFormat != value)
                {
                    _config.DefaultFormat = value;
                    SelectedFormat = value;
                    _ = SaveConfigurationAsync();
                }
            }
        }

        public AppConfig Config => _config;


        // ═════════════════════════════════════════════════════════════
        // 6. PROPRIÉTÉS UI — DONNÉES
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
        // 7. STATISTIQUES
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
        // 8. ARBORESCENCE & RECHERCHE
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
        // 9. FEEDBACK
        // ═════════════════════════════════════════════════════════════

        public string FeedbackMessage
        {
            get => _feedbackMessage;
            set => SetProperty(ref _feedbackMessage, value);
        }

        public bool IsFeedbackVisible
        {
            get => _isFeedbackVisible;
            set => SetProperty(ref _isFeedbackVisible, value);
        }


        // ═════════════════════════════════════════════════════════════
        // 10. EXPORT & FORMAT
        // ═════════════════════════════════════════════════════════════

        public string SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                if (SetProperty(ref _selectedFormat, value))
                {
                    OnPropertyChanged(nameof(CanExport));

                    if (!_isInitializing)
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

        // ═════════════════════════════════════════════════════════════
        // 11. MODE DEV & SIMULATION
        // ═════════════════════════════════════════════════════════════

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
        // 12. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public MainViewModel()
        {
            _logger = new LogService();
            _logger.Info("MainViewModel initialisé");

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

            _config = new AppConfig();
            _importService = new FileImportService(_config);

            _configurationService = new ConfigurationService();
            _userConfig = new UserConfig();
            _ = LoadConfigurationAsync();

            _collectionService = new FileCollectionService();
            _exportService = new FileExportService();

            PreviewText = string.Empty;
            CurrentState = UiState.Empty;
            CurrentFolderPath = string.Empty;
            SelectedFormat = null;
            IsSimulationEnabled = false;
            SelectedSimulationScenario = "Aucun";
        }


        // ═════════════════════════════════════════════════════════════
        // 13. COMMANDES UI
        // ═════════════════════════════════════════════════════════════
        //
        // Actions déclenchées par l’utilisateur :
        // - Charger un dossier
        // - Exporter
        // - Copier
        // - Recherche

        // 👉 COLLE ICI : LoadTreeAsync, GetExportContent, ToggleSearch
        public async Task LoadTreeAsync(string path)
        {
            if (IsBusy) return; // 🔒 anti double clic

            IsBusy = true;

            try
            {
                _logger.Info("Chargement du dossier lancé");
                _logger.Info("Dossier sélectionné", path);

                CurrentState = UiState.Loading;

                await Task.Delay(100);

                if (!await UiSimulationService.ApplyUiSimulationAsync(this))
                {
                    CurrentState = UiState.Ready;
                    return;
                }

                var coreNodes = await _importService.LoadTreeAsync(path);

                Tree.Clear();

                if (coreNodes.Count == 0)
                {
                    _logger.Warning("Aucun fichier trouvé dans le dossier", path);

                    CurrentState = UiState.Error;
                    ErrorMessage = "Dossier introuvable.";
                    return;
                }

                foreach (var coreNode in coreNodes)
                {
                    Tree.Add(ConvertToUiNode(coreNode));
                }

                ApplyFilter();

                _logger.Info("Arborescence projet chargée", $"Nodes: {coreNodes.Count}");

                CurrentFolderPath = path;
                _ = SaveConfigurationAsync();

                CurrentState = UiState.Ready;
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

        public void ToggleSearch()
        {
            IsSearchVisible = !IsSearchVisible;
        }

        // ═════════════════════════════════════════════════════════════
        // 14. LOGIQUE UI
        // ═════════════════════════════════════════════════════════════
        //
        // Gestion dynamique de l’interface :
        // - Preview
        // - Sélection
        // - Feedback

        // 👉 COLLE ICI : RefreshPreviewAsync, OnNodeSelectionChanged, ShowFeedbackAsync

        private async Task RefreshPreviewAsync()
        {
            // 🔒 Protection anti multi-appel
            if (_isPreviewLoading)
                return;
            // 🔥 sécurité cohérence
            if (string.IsNullOrWhiteSpace(PreviewText))
            {
                CurrentState = UiState.Empty;
            }

            // 🟢 LOG — DÉBUT PREVIEW
            _logger.Info("Génération du preview");

            _isPreviewLoading = true;

            try
            {
                var files = GetSelectedFiles();

                // 🟡 LOG — AUCUN FICHIER
                if (files.Count == 0)
                {
                    _logger.Warning("Aucun fichier sélectionné pour le preview");

                    _lastSelectionSignature = string.Empty;
                    _lastIsMarkdown = false;

                    PreviewText = string.Empty;
                    CurrentState = UiState.Empty;
                    return;
                }

                bool isMarkdown = SelectedFormat == ".md";
                string currentSignature = BuildSelectionSignature(files);

                if (currentSignature == _lastSelectionSignature && isMarkdown == _lastIsMarkdown)
                {
                    // 🔥 IMPORTANT : garantir cohérence UI
                    if (string.IsNullOrWhiteSpace(PreviewText) ||
                        PreviewText == "Aucun fichier sélectionné...")
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

                CurrentState = UiState.Loading;

                const int MAX_FILES_PREVIEW = 20;

                var previewFiles = files.Count > MAX_FILES_PREVIEW
                    ? files.GetRange(0, MAX_FILES_PREVIEW)
                    : files;

                var data = await Task.Run(() =>
                {
                    return _exportService.BuildContentWithStats(previewFiles, isMarkdown);
                });

                var content = data.Content;

                const int MAX_CHARACTERS = 20000;

                if (content.Length > MAX_CHARACTERS)
                {
                    content = content.Substring(0, MAX_CHARACTERS);

                    content += "\n\n----------------------------------------\n";
                    content += "⚠ Aperçu limité à 20 000 caractères...";
                }

                var stats = data.Stats;

                if (files.Count > MAX_FILES_PREVIEW)
                {
                    content += "\n\n----------------------------------------\n";
                    content += "⚠ Aperçu limité à 20 fichiers...\n";
                }

                PreviewText = content;

                FileCount = stats.FileCount;
                TotalLines = stats.TotalLines;
                TotalCharacters = stats.TotalCharacters;
                TotalSize = stats.TotalSizeBytes;

                // 🟢 LOG — SUCCÈS PREVIEW
                _logger.Info("Preview généré avec succès", $"Fichiers: {files.Count}");

                CurrentState = UiState.Ready;

                OnPropertyChanged(nameof(CanCopy));
                OnPropertyChanged(nameof(IsPreviewEmpty));
                OnPropertyChanged(nameof(CanExport));
                OnPropertyChanged(nameof(HasEmptyFiles));
            }
            catch (Exception ex) // 🔥 AJOUT ICI (IMPORTANT)
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

        private void OnNodeSelectionChanged(UiFileNode node)
        {
            if (_isBatchUpdating)
                return;

            _isBatchUpdating = true;

            // 🔥 PROPAGATION AUX ENFANTS
            foreach (var child in node.Children)
            {
                SetNodeSelection(child, node.IsSelected);
            }

            _isBatchUpdating = false;

            _ = RefreshPreviewAsync();
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

        private string BuildSelectionSignature(List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
                return string.Empty;

            var ordered = filePaths.OrderBy(p => p);

            return string.Join("|", ordered);
        }

        // ═════════════════════════════════════════════════════════════
        // 15. SÉLECTION & ARBORESCENCE
        // ═════════════════════════════════════════════════════════════
        //
        // Gestion des sélections utilisateur :
        // - Select all
        // - Propagation enfants

        // 👉 COLLE ICI : SetAllSelection, SetNodeSelection

        private void SetAllSelection(bool isSelected)
        {
            _isBatchUpdating = true;

            foreach (var root in Tree)
            {
                SetNodeSelection(root, isSelected);
            }

            _isBatchUpdating = false;

            _ = RefreshPreviewAsync();
        }

        private void SetNodeSelection(UiFileNode node, bool isSelected)
        {
            node.IsSelected = isSelected;

            foreach (var child in node.Children)
            {
                SetNodeSelection(child, isSelected);
            }
        }

        public bool IsAllSelected
        {
            get => _isAllSelected;
            set
            {
                if (SetProperty(ref _isAllSelected, value))
                {
                    OnSelectAllBlocked?.Invoke();

                    _isAllSelected = false;
                    OnPropertyChanged(nameof(IsAllSelected));
                }
            }
        }

        public event Action? OnSelectAllBlocked;

        // ═════════════════════════════════════════════════════════════
        // 16. FILTRAGE & RECHERCHE
        // ═════════════════════════════════════════════════════════════
        //
        // Recherche dynamique dans l’arborescence

        // 👉 COLLE ICI : ApplyFilter, FilterNode, DebounceFilter

        private void ApplyFilter()
        {
            FilteredTree.Clear();

            // 🔹 Pas de recherche → copie complète
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var node in Tree)
                {
                    FilteredTree.Add(node);
                }

                HasSearchResult = true;
                return;
            }

            bool hasResult = false;

            foreach (var node in Tree)
            {
                var filtered = FilterNode(node);

                if (filtered != null)
                {
                    FilteredTree.Add(filtered);
                    hasResult = true;
                }
            }

            HasSearchResult = hasResult;
        }

        private UiFileNode? FilterNode(UiFileNode node)
        {
            bool match = node.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            var newNode = new UiFileNode
            {
                Name = node.Name,
                Path = node.Path,
                IsSelected = node.IsSelected
            };

            foreach (var child in node.Children)
            {
                var filteredChild = FilterNode(child);

                if (filteredChild != null)
                {
                    newNode.Children.Add(filteredChild);
                }
            }

            if (match || newNode.Children.Count > 0)
                return newNode;

            return null;
        }

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
        // 17. CONVERSION UI ↔ CORE
        // ═════════════════════════════════════════════════════════════
        //
        // Transformation des données UI vers Core

        // 👉 COLLE ICI : ConvertToUiNode, ConvertToCoreNodes, GetSelectedFiles

        private UiFileNode ConvertToUiNode(CoreFileNode coreNode)
        {
            var uiNode = new UiFileNode
            {
                Name = coreNode.Name,
                Path = coreNode.Path
            };

            uiNode.SelectionChanged += OnNodeSelectionChanged;

            foreach (var child in coreNode.Children)
            {
                uiNode.Children.Add(ConvertToUiNode(child));
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
                    IsSelected = uiNode.IsSelected
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
        // 18. CONFIGURATION UTILISATEUR
        // ═════════════════════════════════════════════════════════════
        //
        // Gestion JSON :
        // - Load
        // - Save
        // - Reset

        // 👉 COLLE ICI : LoadConfigurationAsync, SaveConfigurationAsync, ResetConfigurationAsync

        private async Task LoadConfigurationAsync()
        {
            try
            {
                _isInitializing = true;
                _logger.Info("Chargement configuration utilisateur");

                _userConfig = await _configurationService.LoadAsync() ?? new UserConfig();

                _config.DefaultFormat = _userConfig.DefaultFormat;
                _config.IsDeveloperMode = _userConfig.IsDeveloperMode;
                _config.LastOpenedFolder = _userConfig.LastOpenedFolder;
                _config.AutoLoadLastFolder = _userConfig.AutoLoadLastFolder;
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
                    await LoadTreeAsync(_userConfig.LastOpenedFolder);
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
                _userConfig.AutoLoadLastFolder = _config.AutoLoadLastFolder;
                _userConfig.ExcludedFolders = _config.ExcludedFolders.ToList();

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
                _logger.Info("Reset configuration utilisateur");

                _userConfig = await _configurationService.ResetAsync();

                _config.DefaultFormat = _userConfig.DefaultFormat;
                _config.IsDeveloperMode = _userConfig.IsDeveloperMode;
                _config.LastOpenedFolder = _userConfig.LastOpenedFolder;
                _config.AutoLoadLastFolder = _userConfig.AutoLoadLastFolder;

                _config.ExcludedFolders.Clear();

                foreach (var item in _userConfig.ExcludedFolders)
                {
                    _config.ExcludedFolders.Add(item);
                }

                SelectedFormat = _userConfig.DefaultFormat;
                IsDeveloperMode = _userConfig.IsDeveloperMode;
                CurrentFolderPath = _userConfig.LastOpenedFolder;

                Tree.Clear();
                PreviewText = string.Empty;
                CurrentState = UiState.Empty;

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
        // 19. LOGS
        // ═════════════════════════════════════════════════════════════
        //
        // Export / filtrage des logs

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