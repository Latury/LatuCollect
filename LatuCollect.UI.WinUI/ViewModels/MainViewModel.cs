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
using LatuCollect.Core.Services.Collection;
using LatuCollect.Core.Services.Export;
using LatuCollect.Core.Services.Import;
using LatuCollect.Core.Simulation;
using LatuCollect.UI.WinUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        //

        // ─────────────────────────────────────────────
        // 📝 ÉTAT LOCAL (UI)
        // ─────────────────────────────────────────────

        private string _previewText = "Aucun fichier sélectionné...";
        private string _currentFolderPath = string.Empty;
        private string _searchText = string.Empty;
        private string? _selectedFormat = null;

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

        // ─────────────────────────────────────────────
        // 🔧 SERVICES CORE
        // ─────────────────────────────────────────────

        // Service de chargement de l’arborescence
        private readonly FileImportService _importService;
        // Service de collecte des fichiers sélectionnés
        private readonly FileCollectionService _collectionService;
        // Service de génération du contenu exporté
        private readonly FileExportService _exportService;

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
                }
            }
        }

        // ─────────────────────────────────────────────
        // 🔎 HELPERS POUR LE BINDING UI
        // ─────────────────────────────────────────────

        public bool IsLoading => CurrentState == UiState.Loading;
        public bool IsReady => CurrentState == UiState.Ready;
        public bool HasError => CurrentState == UiState.Error;

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
        //
        // Données exposées à l’interface (XAML)
        //
        // ─────────────────────────────────────────────
        // 📝 APERÇU & DOSSIER
        // ─────────────────────────────────────────────

        public string PreviewText
        {
            get => _previewText;
            set => SetProperty(ref _previewText, value);
        }

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
                    OnPropertyChanged(nameof(CanExport));
            }
        }

        // ─────────────────────────────────────────────
        // ⚙️ ÉTATS DÉRIVÉS (LOGIQUE UI)
        // ─────────────────────────────────────────────

        public bool HasEmptyFiles => PreviewText.Contains("\n\n\n\n");

        public bool CanCopy => PreviewText != "Aucun fichier sélectionné...";

        public bool IsPreviewEmpty =>
            CurrentState == UiState.Ready &&
            PreviewText == "Aucun fichier sélectionné...";

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
                    RefreshPreview();
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
                    RefreshPreview();
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


        // ═════════════════════════════════════════════════════════════════════
        // 5. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════════════
        //
        // Initialisation du ViewModel :
        // - Création des services Core
        // - Initialisation des valeurs par défaut
        //

        public MainViewModel()
        {
            // ─────────────────────────────────────────────
            // 🔧 SERVICES CORE
            // ─────────────────────────────────────────────

            // Initialisation des services Core
            _importService = new FileImportService();
            // Le FileCollectionService est léger et sans état, on peut l’instancier directement ici
            _collectionService = new FileCollectionService();
            // Le FileExportService est également léger, mais on peut le garder en champ pour une utilisation future (ex: export asynchrone)
            _exportService = new FileExportService();

            // ─────────────────────────────────────────────
            // ⚙️ ÉTAT INITIAL UI
            // ─────────────────────────────────────────────

            CurrentState = UiState.Ready;

            PreviewText = "Aucun fichier sélectionné...";

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
        public async void LoadTree(string path)
        {
            try
            {
                CurrentState = UiState.Loading;

                await Task.Delay(100);

                if (!UiSimulationService.ApplyUiSimulation(this))
                    return;

                // 🔥 UTILISATION DU CORE
                var coreNodes = await _importService.LoadTreeAsync(path);

                Tree.Clear();

                if (coreNodes.Count == 0)
                {
                    CurrentState = UiState.Error;
                    ErrorMessage = "Dossier introuvable.";
                    return;
                }

                // 🔁 CONVERSION CORE → UI
                foreach (var coreNode in coreNodes)
                {
                    Tree.Add(ConvertToUiNode(coreNode));
                }

                CurrentState = UiState.Ready;
            }
            catch (Exception ex)
            {
                CurrentState = UiState.Error;
                ErrorMessage = ex.Message;
            }
        }

        // ─────────────────────────────────────────────
        // 📦 EXPORTER LE CONTENU
        // ─────────────────────────────────────────────

        public string GetExportContent()
        {
            var files = GetSelectedFiles();

            if (files.Count == 0)
                return string.Empty;

            const int MAX_FILES_EXPORT = 200;

            if (files.Count > MAX_FILES_EXPORT)
            {
                return $"⚠ Trop de fichiers sélectionnés ({files.Count}).\n" +
                       $"Limite actuelle : {MAX_FILES_EXPORT} fichiers.\n\n" +
                       $"Réduis la sélection.";
            }

            bool isMarkdown = SelectedFormat == ".md";

            var data = _exportService.BuildContentWithStats(files, isMarkdown);
            return data.Content;
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

            RefreshPreview();
        }

        private async void RefreshPreview()
        {
            var files = GetSelectedFiles();

            if (files.Count == 0)
            {
                PreviewText = "Aucun fichier sélectionné...";
                return;
            }

            CurrentState = UiState.Loading;

            const int MAX_FILES_PREVIEW = 20;

            var previewFiles = files.Count > MAX_FILES_PREVIEW
                ? files.GetRange(0, MAX_FILES_PREVIEW)
                : files;

            bool isMarkdown = SelectedFormat == ".md";

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

            CurrentState = UiState.Ready;

            OnPropertyChanged(nameof(CanCopy));
            OnPropertyChanged(nameof(IsPreviewEmpty));
            OnPropertyChanged(nameof(CanExport));
            OnPropertyChanged(nameof(HasEmptyFiles));
        }

        // ─────────────────────────────────────────────
        // 💬 FEEDBACK UTILISATEUR
        // ─────────────────────────────────────────────

        public async void ShowFeedback(string message)
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

            RefreshPreview();
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
            var coreNodes = _collectionService.ConvertFromUiTree(Tree);
            return _collectionService.GetSelectedFiles(coreNodes);
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
    }
}