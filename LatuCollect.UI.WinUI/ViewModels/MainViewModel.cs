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
using CoreFileNode = LatuCollect.Core.Models.FileNode;
using UiFileNode = LatuCollect.UI.WinUI.Models.FileNode;

namespace LatuCollect.UI.WinUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS - ÉTAT INTERNE DU VIEWMODEL
        // ═════════════════════════════════════════════════════════════════════
        //
        // Contient :
        // - État local (texte, recherche, sélection)
        // - Flags UI (feedback, simulation, batch)
        // - Services Core
        // - Outils techniques (async, debounce)
        // - Cache pour optimisation (preview)
        //

        // ─────────────────────────────────────────────
        // 📝 ÉTAT LOCAL (UI)
        // ─────────────────────────────────────────────

        private string _previewText = string.Empty;
        private string _currentFolderPath = string.Empty;
        private string _searchText = string.Empty;
        private string? _selectedFormat = null;
        // Cache du dernier état utilisé pour le preview
        private string _lastSelectionSignature = string.Empty;
        private bool _lastIsMarkdown = false;
        private bool _isInitializing = false; // Permet de différencier le chargement initial des changements utilisateur

        // ─────────────────────────────────────────────
        // 💬 FEEDBACK UTILISATEUR
        // ─────────────────────────────────────────────

        private string _feedbackMessage = "";
        private bool _isFeedbackVisible;

        // ─────────────────────────────────────────────
        // 🧪 SIMULATION
        // ─────────────────────────────────────────────

        private bool _isSimulationEnabled = false;
        private string _selectedSimulationScenario = "Aucun";

        // ─────────────────────────────────────────────
        // ⚙️ FLAGS TECHNIQUES
        // ─────────────────────────────────────────────

        // Évite les refresh multiples lors des sélections massives
        private bool _isBatchUpdating = false;

        // Permet d’annuler une recherche en cours (debounce)
        private CancellationTokenSource? _searchCts;

        // Évite plusieurs refresh preview simultanés
        private bool _isPreviewLoading = false;

        // 🔒 Verrou global UI (anti double clic)
        private bool _isBusy = false;

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        // ─────────────────────────────────────────────
        // 🔧 SERVICES CORE
        // ─────────────────────────────────────────────

        // Service de chargement de l’arborescence
        private readonly FileImportService _importService;
        // Service de collecte des fichiers sélectionnés
        private readonly FileCollectionService _collectionService;
        // Service de génération du contenu exporté
        private readonly FileExportService _exportService;
        // Service de logging (pour les erreurs, warnings, infos)
        private readonly ILogService _logger;
        // Configuration globale de l’application (ex: exclusions, limites)
        private readonly AppConfig _config;
        // Service de configuration utilisateur (JSON)
        private readonly IConfigurationService _configurationService;
        // Configuration utilisateur persistée
        private UserConfig _userConfig;

        // ═════════════════════════════════════════════════════════════════════
        // 2. CONSTANTES / LIMITES (PERFORMANCE & SÉCURITÉ)
        // ═════════════════════════════════════════════════════════════════════
        //
        // Objectif :
        // - Éviter les freezes UI sur gros projets
        // - Limiter la charge mémoire
        // - Contrôler la profondeur de l’arborescence
        //
        // IMPORTANT :
        // Ces valeurs sont utilisées lors du chargement des fichiers
        // (CreateNode / LoadTree)
        //
        // ─────────────────────────────────────────────

        // Nombre maximum de nodes (fichiers + dossiers)
        private const int MAX_NODES = 1000;

        // Profondeur maximale de l’arborescence
        private const int MAX_DEPTH = 10;


        // ═════════════════════════════════════════════════════════════════════
        // 3. ÉTATS UI (GLOBAL)
        // ═════════════════════════════════════════════════════════════════════
        //
        // Gestion de l’état global de l’application :
        // - Chargement (Loading)
        // - Prêt (Ready)
        // - Erreur (Error)
        //
        // Permet à l’UI de s’adapter automatiquement :
        // - Afficher un loader
        // - Afficher le contenu
        // - Afficher un message d’erreur
        //

        // ─────────────────────────────────────────────
        // 📊 ENUM DES ÉTATS
        // ─────────────────────────────────────────────

        public enum UiState
        {
            Loading,
            Ready,
            Empty,
            Error
        }

        // ─────────────────────────────────────────────
        // 🔄 ÉTAT ACTUEL
        // ─────────────────────────────────────────────

        private UiState _currentState = UiState.Ready;

        public UiState CurrentState
        {
            get => _currentState;
            set
            {
                if (SetProperty(ref _currentState, value))
                {
                    // 🔁 Notifie les propriétés dépendantes
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

        // ─────────────────────────────────────────────
        // 🔎 HELPERS POUR LE BINDING UI
        // ─────────────────────────────────────────────

        public bool IsLoading => CurrentState == UiState.Loading;
        public bool IsReady => CurrentState == UiState.Ready;
        public bool HasError => CurrentState == UiState.Error;
        public bool IsEmpty => CurrentState == UiState.Empty;

        // ─────────────────────────────────────────────
        // ❌ GESTION DES ERREURS
        // ─────────────────────────────────────────────

        private string _errorMessage = string.Empty;

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }


        // ═════════════════════════════════════════════════════════════════════
        // 4. PROPRIÉTÉS UI (BINDING)
        // ═════════════════════════════════════════════════════════════════════

        // ═════════════════════════════════════════════════════════════
        // 🔗 SETTINGS - BINDING UI (WRAPPERS)
        // ═════════════════════════════════════════════════════════════

        // 🧑‍💻 Mode développeur (alias UI)
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

        // 📂 Chargement automatique du dernier dossier
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

        // 📄 Format par défaut (différent du format courant)
        public string DefaultFormat
        {
            get => _config.DefaultFormat;
            set
            {
                if (_config.DefaultFormat != value)
                {
                    _config.DefaultFormat = value;

                    // Synchronise aussi le format actuel
                    SelectedFormat = value;

                    _ = SaveConfigurationAsync();
                }
            }
        }

        // 🔧 CONFIGURATION GLOBALE (exposée à la UI)
        public AppConfig Config => _config;


        // ─────────────────────────────────────────────
        // 📝 APERÇU & DOSSIER
        // ─────────────────────────────────────────────

        // Exposé à la UI pour affichage du preview
        public string PreviewText
        {
            get => _previewText;
            set => SetProperty(ref _previewText, value);
        }

        // Exposé à la UI pour affichage du chemin du dossier chargé
        public string CurrentFolderPath
        {
            get => _currentFolderPath;
            set => SetProperty(ref _currentFolderPath, value);
        }

        // ═════════════════════════════════════════════════════════════════════
        // 📊 EXPORT - VÉRIFICATIONS AVANT EXPORT
        // ═════════════════════════════════════════════════════════════════════

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

        // ─────────────────────────────────────────────
        // 📊 STATISTIQUES
        // ─────────────────────────────────────────────

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

        // ─────────────────────────────────────────────
        // 🌳 ARBORESCENCE
        // ─────────────────────────────────────────────

        public ObservableCollection<UiFileNode> Tree { get; } = new();

        // ─────────────────────────────────────────────
        // 🔍 RECHERCHE
        // ─────────────────────────────────────────────

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

        private bool _hasSearchResult = true;
        public bool HasSearchResult
        {
            get => _hasSearchResult;
            set => SetProperty(ref _hasSearchResult, value);
        }

        // ─────────────────────────────────────────────
        // 🔍 VISIBILITÉ UI (RECHERCHE)
        // ─────────────────────────────────────────────

        private bool _isSearchVisible;
        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set => SetProperty(ref _isSearchVisible, value);
        }

        // ─────────────────────────────────────────────
        // 💬 FEEDBACK UTILISATEUR
        // ─────────────────────────────────────────────

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

        // ─────────────────────────────────────────────
        // ⚠️ ÉTAT LIMITE PROJET (GROS DOSSIERS)
        // ─────────────────────────────────────────────

        private bool _isLimitReached;

        public bool IsLimitReached
        {
            get => _isLimitReached;
            set => SetProperty(ref _isLimitReached, value);
        }

        // ─────────────────────────────────────────────
        // 📄 FORMAT EXPORT
        // ─────────────────────────────────────────────

        public string SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                if (SetProperty(ref _selectedFormat, value))
                {
                    OnPropertyChanged(nameof(CanExport));

                    if (!_isInitializing) // 🔥 IMPORTANT
                    {
                        _ = SaveConfigurationAsync();
                    }
                }
            }
        }

        // ─────────────────────────────────────────────
        // ⚙️ ÉTATS DÉRIVÉS (LOGIQUE UI)
        // ─────────────────────────────────────────────

        public bool HasEmptyFiles => PreviewText.Contains("\n\n\n\n");

        public bool CanCopy => !string.IsNullOrWhiteSpace(PreviewText);

        public bool IsPreviewEmpty => CurrentState == UiState.Empty;

        public bool CanExport =>
            !IsPreviewEmpty &&
            SelectedFormat != null;

        // ─────────────────────────────────────────────
        // ☑ SÉLECTION GLOBALE (UI)
        // ─────────────────────────────────────────────

        private bool _isAllSelected;

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

        // ─────────────────────────────────────────────
        // 🧪 SIMULATION
        // ─────────────────────────────────────────────

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

        public string SimulationLabel
        {
            get
            {
                if (!IsSimulationEnabled)
                    return "🧪 Simulation : Désactivé";

                if (SelectedSimulationScenario == "Aucun")
                    return "🧪 Simulation : Activé";

                return $"🧪 Simulation : {SelectedSimulationScenario}";
            }
        }

        // ─────────────────────────────────────────────
        // 🧑🏻‍💻 MODE DÉVELOPPEUR
        // ─────────────────────────────────────────────

        private bool _isDeveloperMode;

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

        // visibilité UI
        public bool IsSimulationVisible => IsDeveloperMode;
      
        // message UI
        public string DeveloperWarningMessage =>
            "⚠ Mode simulation\n\n" +
            "Ce mode est destiné aux tests.\n" +
            "Il peut provoquer des comportements instables.";

        // ─────────────────────────────────────────────
        // 🧾 LOGS (DEBUG / SUIVI)
        // ─────────────────────────────────────────────

        public ReadOnlyObservableCollection<LogEntry> Logs
        {
            get
            {
                return (_logger as LogService)!.Logs;
            }
        }

        // ─────────────────────────────────────────────
        // 🧾 LOGS - INDICATEUR ERREUR
        // ─────────────────────────────────────────────

        // Permet d’afficher un indicateur visuel si des erreurs sont présentes dans les logs
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

        // ─────────────────────────────────────────────
        // 🧾 LOGS - COMPTEUR ERREURS
        // ─────────────────────────────────────────────
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

        // ─────────────────────────────────────────────
        // 🧾 FILTRE LOGS
        // ─────────────────────────────────────────────

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

        // ═════════════════════════════════════════════════════════════════════
        // 5. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════════════
        //
        // Initialisation du ViewModel :
        // - Configuration du logging
        // - Création des services Core
        // - Initialisation des valeurs par défaut
        // - Configuration de la simulation
        //

        public MainViewModel()
        {
            // ─────────────────────────────────────────────
            // 🧾 LOGGING
            // ─────────────────────────────────────────────
            _logger = new LogService();
            _logger.Info("MainViewModel initialisé");

            // 🔥 AJOUT ICI (TRÈS IMPORTANT)
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

            // ─────────────────────────────────────────────
            // 🔧 SERVICES CORE
            // ─────────────────────────────────────────────

            // Configuration globale (ex: exclusions, limites)
            _config = new AppConfig();
            _importService = new FileImportService(_config);
            // Configuration utilisateur (ex: préférences, derniers dossiers)
            _configurationService = new ConfigurationService();
            _userConfig = new UserConfig();
            _ = LoadConfigurationAsync();
            // Le FileCollectionService est léger et sans état, on peut l’instancier directement ici
            _collectionService = new FileCollectionService();
            // Le FileExportService est également léger, mais on peut le garder en champ pour une utilisation future (ex: export asynchrone)
            _exportService = new FileExportService();


            // ─────────────────────────────────────────────
            // ⚙️ ÉTAT INITIAL UI
            // ─────────────────────────────────────────────

            PreviewText = string.Empty;
            CurrentState = UiState.Empty;

            CurrentFolderPath = string.Empty;

            SelectedFormat = null;

            IsSimulationEnabled = false;

            SelectedSimulationScenario = "Aucun";
        }


        // ═════════════════════════════════════════════════════════════════════
        // 6. COMMANDES UI
        // ═════════════════════════════════════════════════════════════════════
        //
        // Actions déclenchées par l’utilisateur :
        // - Charger un dossier
        // - Exporter le contenu
        // - Copier le contenu (via Preview)
        // - Activer / désactiver la recherche
        //
        // 👉 Correspond aux interactions utilisateur (boutons UI)
        //

        // ─────────────────────────────────────────────
        // 📂 CHARGER UN DOSSIER
        // ─────────────────────────────────────────────

        // 🔥 VERSION CORE (ASYNCHRONE)
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

                _logger.Info("Arborescence projet chargée", $"Nodes: {coreNodes.Count}");

                // 🔥 AJOUT ICI
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
                IsBusy = false; // 🔓 libération obligatoire
            }
        }

        // ─────────────────────────────────────────────
        // 📦 EXPORTER LE CONTENU
        // ─────────────────────────────────────────────

        public string GetExportContent()
        {
            if (IsBusy) return string.Empty; // 🔒 protection

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
                IsBusy = false; // 🔓 libération obligatoire
            }
        }

        // ─────────────────────────────────────────────
        // 🔍 TOGGLE RECHERCHE
        // ─────────────────────────────────────────────

        public void ToggleSearch()
        {
            IsSearchVisible = !IsSearchVisible;
        }


        // ═════════════════════════════════════════════════════════════════════
        // 7. LOGIQUE UI (AUTORISÉE)
        // ═════════════════════════════════════════════════════════════════════
        //
        // Gestion de l’interface utilisateur :
        // - Mise à jour de l’aperçu
        // - Feedback utilisateur
        // - Réactions aux actions UI
        // - Synchronisation avec le Core
        //
        // ⚠️ IMPORTANT :
        // Ne doit PAS contenir de logique métier complexe
        //

        // ─────────────────────────────────────────────
        // 🔄 APERÇU DYNAMIQUE
        // ─────────────────────────────────────────────

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

        // ─────────────────────────────────────────────
        // 💬 FEEDBACK UTILISATEUR
        // ─────────────────────────────────────────────

        public async Task ShowFeedbackAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message) || message == "Aucun fichier sélectionné.")
                return;

            FeedbackMessage = message;
            IsFeedbackVisible = true;

            await Task.Delay(4500);

            IsFeedbackVisible = false;
        }

        // ─────────────────────────────────────────────
        // ☑ SÉLECTION GLOBALE (LOGIQUE)
        // ─────────────────────────────────────────────

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

        // ═════════════════════════════════════════════════════════════════════
        // 8. MÉTHODES PRIVÉES (UTILITAIRES)
        // ═════════════════════════════════════════════════════════════════════
        //
        // Méthodes internes au ViewModel :
        // - Filtrage de l’arborescence (recherche)
        // - Helpers récursifs
        // - Gestion du debounce (optimisation UI)
        //
        // ⚠️ Doivent rester simples (pas de logique métier complexe)
        //

        // ─────────────────────────────────────────────
        // 🧠 CACHE / SIGNATURE (OPTIMISATION PREVIEW)
        // ─────────────────────────────────────────────

        // Génère une signature unique basée sur les fichiers sélectionnés
        // Permet de détecter si l'état a changé
        private string BuildSelectionSignature(List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
                return string.Empty;

            // Ordre stable pour éviter faux changements
            var ordered = filePaths.OrderBy(p => p);

            return string.Join("|", ordered);
        }

        // ─────────────────────────────────────────────
        // 🔍 FILTRE DE RECHERCHE
        // ─────────────────────────────────────────────

        private void ApplyFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                foreach (var root in Tree)
                {
                    SetAllVisible(root);
                }

                HasSearchResult = true;
                return;
            }

            bool hasVisible = false;

            foreach (var root in Tree)
            {
                if (ApplyFilterToNode(root))
                    hasVisible = true;
            }

            HasSearchResult = hasVisible;
        }

        private bool ApplyFilterToNode(UiFileNode node)
        {
            bool match = node.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            bool hasChildMatch = false;

            foreach (var child in node.Children)
            {
                if (ApplyFilterToNode(child))
                    hasChildMatch = true;
            }

            node.IsVisible = match || hasChildMatch;

            return node.IsVisible;
        }

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

        private List<string> GetSelectedFiles()
        {
            var coreNodes = ConvertToCoreNodes(Tree);
            return _collectionService.GetSelectedFiles(coreNodes);
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

                coreNode.Children = ConvertToCoreNodes(uiNode.Children);

                result.Add(coreNode);
            }

            return result;
        }

        // ─────────────────────────────────────────────
        // ⚙️ CHARGEMENT CONFIGURATION UTILISATEUR
        // ─────────────────────────────────────────────

        private async Task LoadConfigurationAsync()
        {
            try
            {
                _isInitializing = true; // 🔥 BLOQUE SAVE

                _logger.Info("Chargement configuration utilisateur");

                _userConfig = await _configurationService.LoadAsync() ?? new UserConfig();

                // Sync Core
                _config.DefaultFormat = _userConfig.DefaultFormat;
                _config.IsDeveloperMode = _userConfig.IsDeveloperMode;
                _config.LastOpenedFolder = _userConfig.LastOpenedFolder;
                _config.AutoLoadLastFolder = _userConfig.AutoLoadLastFolder;
                _config.ExcludedFolders.Clear();

                foreach (var item in _userConfig.ExcludedFolders)
                {
                    _config.ExcludedFolders.Add(item);
                }

                // Sync UI
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
                _isInitializing = false; // 🔓 réactive save
            }
        }

        // ─────────────────────────────────────────────
        // 💾 SAUVEGARDE CONFIGURATION UTILISATEUR
        // ─────────────────────────────────────────────

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

        // ─────────────────────────────────────────────
        // 🔄 RESET CONFIGURATION UTILISATEUR
        // ─────────────────────────────────────────────

        public async Task ResetConfigurationAsync()
        {
            if (IsBusy) return;

            IsBusy = true;

            try
            {
                _logger.Info("Reset configuration utilisateur");

                // 🔥 Appel Core
                _userConfig = await _configurationService.ResetAsync();

                // 🔗 Synchronisation AppConfig
                _config.DefaultFormat = _userConfig.DefaultFormat;
                _config.IsDeveloperMode = _userConfig.IsDeveloperMode;
                _config.LastOpenedFolder = _userConfig.LastOpenedFolder;
                _config.AutoLoadLastFolder = _userConfig.AutoLoadLastFolder;

                // 🔗 Synchronisation UI
                SelectedFormat = _userConfig.DefaultFormat;
                IsDeveloperMode = _userConfig.IsDeveloperMode;
                CurrentFolderPath = _userConfig.LastOpenedFolder;

                // 🔄 Reset UI logique
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

        // ─────────────────────────────────────────────
        // 🌳 HELPER RÉCURSIF (VISIBILITÉ)
        // ─────────────────────────────────────────────

        private void SetAllVisible(UiFileNode node)
        {
            node.IsVisible = true;

            foreach (var child in node.Children)
            {
                SetAllVisible(child);
            }
        }

        // ─────────────────────────────────────────────
        // ⏱ DEBOUNCE RECHERCHE
        // ─────────────────────────────────────────────

        // Permet d’attendre que l’utilisateur ait fini de taper avant d’appliquer le filtre
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

        // ─────────────────────────────────────────────
        // 🧾 EXPORT LOGS
        // ─────────────────────────────────────────────

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
    }
}