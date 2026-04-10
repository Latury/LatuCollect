/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI                                                         ║
║  Fichier : MainViewModel.cs                                          ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer l’état et la logique de l’interface principale (MVVM)         ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gérer la sélection des fichiers                                   ║
║  - Générer l’aperçu dynamique                                        ║
║  - Construire l’arborescence projet                                  ║
║  - Gérer la recherche (filtrage temps réel)                          ║
║  - Centraliser la logique d’export (respect ALC)                     ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Contient la logique métier liée à l’UI                            ║
║  - Aucun accès direct à l’UI                                         ║
║  - Aucun accès direct aux fichiers depuis l’UI                       ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - CommunityToolkit.Mvvm                                             ║
║  - System.IO                                                         ║
║  - FileReaderService                                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using CommunityToolkit.Mvvm.ComponentModel;
using LatuCollect.Core.Services;
using LatuCollect.Core.Simulation;
using LatuCollect.UI.WinUI.Models;
using LatuCollect.UI.WinUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace LatuCollect.UI.WinUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // ======================================================
        // 🧠 CHAMPS PRIVÉS - ÉTAT INTERNE DE LA VUE
        // ======================================================

        private string _previewText = "Aucun fichier sélectionné...";
        private string _currentFolderPath = string.Empty;
        private string _searchText = string.Empty;

        private string _feedbackMessage = "";
        private bool _isFeedbackVisible;

        private string? _selectedFormat = null;

        private bool _isSimulationEnabled = false;
        private string _selectedSimulationScenario = "Aucun";
        // Config de simulation partagée
        private bool _isBatchUpdating = false;

        // ======================================================
        // ⚡ LIMITES PERFORMANCE (ANTI FREEZE)
        // ======================================================

        private const int MAX_NODES = 1000;     // nombre total max
        private const int MAX_DEPTH = 10;       // profondeur max

        // ======================================================
        // ⚠️ ÉTAT VOLUME PROJET
        // ======================================================

        private bool _isLimitReached;
        public bool IsLimitReached
        {
            get => _isLimitReached;
            set => SetProperty(ref _isLimitReached, value);
        }

        // ======================================================
        // 🔄 ÉTATS UI (GLOBAL)
        // ======================================================

        public enum UiState
        {
            Loading,
            Ready,
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
                    // 🔥 IMPORTANT : notifier les propriétés liées
                    OnPropertyChanged(nameof(IsLoading));
                    OnPropertyChanged(nameof(IsReady));
                    OnPropertyChanged(nameof(HasError));
                    OnPropertyChanged(nameof(IsPreviewEmpty));
                }
            }
        }

        // 🔎 Helpers (facilitent le binding UI plus tard)
        public bool IsLoading => CurrentState == UiState.Loading;
        public bool IsReady => CurrentState == UiState.Ready;
        public bool HasError => CurrentState == UiState.Error;

        // Message d'erreur éventuel
        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        // ======================================================
        // 📦 PROPRIÉTÉS PUBLIQUES - LIÉES À L’INTERFACE
        // ======================================================

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

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                    ApplyFilter();
            }
        }
        public ObservableCollection<FileNode> Tree { get; } = new();

        // ======================================================
        // 🔍 VISIBILITÉ BARRE DE RECHERCHE
        // ======================================================

        private bool _isSearchVisible;
        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set => SetProperty(ref _isSearchVisible, value);
        }

        // ======================================================
        // 🔍 ETAT DE L’APERÇU & EXPORT 
        // ======================================================

        public bool HasEmptyFiles => PreviewText.Contains("\n\n\n\n");
        public bool CanCopy => PreviewText != "Aucun fichier sélectionné...";
        public bool IsPreviewEmpty =>
    CurrentState == UiState.Ready &&
    PreviewText == "Aucun fichier sélectionné...";

        public bool CanExport =>
            !IsPreviewEmpty &&
            SelectedFormat != null;

        // ======================================================
        // ☑ SÉLECTION GLOBALE (CHECKBOX)
        // ======================================================

        private bool _isAllSelected;
        public bool IsAllSelected
        {
            get => _isAllSelected;
            set
            {
                if (SetProperty(ref _isAllSelected, value))
                {
                    SetAllSelection(value);
                }
            }
        }

        // ======================================================
        // 💬 RETOUR UTILISATEUR (FEEDBACK)
        // ======================================================

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

        public async void ShowFeedback(string message)
        {
            // ❌ Bloque ce message (déjà affiché au centre)
            if (string.IsNullOrWhiteSpace(message) || message == "Aucun fichier sélectionné.")
                return;

            FeedbackMessage = message;
            IsFeedbackVisible = true;

            await Task.Delay(4500);

            IsFeedbackVisible = false;
        }

        // ========================================================
        // 📄 FORMAT D’EXPORT - GESTION DE LA SÉLECTION DU FORMAT 
        // ========================================================

        public string SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                if (SetProperty(ref _selectedFormat, value))
                    OnPropertyChanged(nameof(CanExport));
            }
        }

        // ======================================================
        // 📊 EXOPORT - VÉRIFICATIONS AVANT EXPORT
        // ======================================================

        public ExportCheckResult CheckExportState()
        {
            if (IsPreviewEmpty)
                return ExportCheckResult.NoSelection;

            if (HasEmptyFiles)
                return ExportCheckResult.EmptyFiles;

            return ExportCheckResult.Ok;
        }

        public enum ExportCheckResult
        {
            Ok,
            NoSelection,
            EmptyFiles
        }

        // ======================================================
        // 🧪 SIMULATION - GESTION DE L’ÉTAT DE LA SIMULATION 
        // ======================================================

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

        // ======================================================
        // 🧩 UTILITAIRES - GESTION DES FICHIERS SÉLECTIONNÉS
        // ======================================================

        private List<string> GetSelectedFiles()
        {
            List<string> files = new();

            void ProcessNode(FileNode node)
            {
                if (node.IsSelected && File.Exists(node.Path))
                    files.Add(node.Path);

                foreach (var child in node.Children)
                    ProcessNode(child);
            }

            foreach (var root in Tree)
                ProcessNode(root);

            return files;
        }

        // ======================================================
        // ☑ SÉLECTION GLOBALE (LOGIQUE)
        // ======================================================

        private void SetAllSelection(bool isSelected)
        {
            _isBatchUpdating = true;

            foreach (var root in Tree)
            {
                SetNodeSelection(root, isSelected);
            }

            _isBatchUpdating = false;

            RefreshPreview(); // 🔥 UN SEUL refresh
        }

        private void SetNodeSelection(FileNode node, bool isSelected)
        {
            node.IsSelected = isSelected;

            foreach (var child in node.Children)
            {
                SetNodeSelection(child, isSelected);
            }
        }

        // ===========================================================
        // 🌳 ARBORESCENCE - CHARGEMENT DE L’ARBORESCENCE DE FICHIERS
        // ===========================================================

        public async void LoadTree(string path)
        {
            try
            {
                // =========================
                // 🔄 ÉTAT : LOADING
                // =========================
                CurrentState = UiState.Loading;

                // 👇 Laisse le temps à l’UI de s’afficher
                await Task.Delay(100);

                // ===========================================================================
                // 🧪 SIMULATION (APPLIQUÉE AVANT LE CHARGEMENT POUR INFLUENCER LE PROCESSUS)
                // ===========================================================================
                if (!UiSimulationService.ApplyUiSimulation(this))
                    return;

                // ====================================================
                // 🔄 CHARGEMENT DE L’ARBORESCENCE (THREAD BACKGROUND)
                // ====================================================
                var rootNode = await Task.Run(() =>
                {
                    if (!Directory.Exists(path))
                        return null;

                    int count = 0;
                    return CreateNode(path, 0, ref count);
                });

                // =====================================
                // 🔄 MISE À JOUR DE L’UI (MAIN THREAD)  
                // =====================================
                Tree.Clear();

                if (rootNode == null)
                {
                    CurrentState = UiState.Error;
                    ErrorMessage = "Dossier introuvable.";
                    return;
                }

                Tree.Add(rootNode);

                // ⚠️ Message si projet trop volumineux
                if (IsLimitReached)
                {
                    ShowFeedback("⚠ Projet volumineux — affichage partiel pour éviter les ralentissements");
                }

                // =========================
                // 🔄 ÉTAT : READY
                // =========================
                CurrentState = UiState.Ready;
            }
            catch (Exception ex)
            {
                CurrentState = UiState.Error;
                ErrorMessage = ex.Message;
            }
        }

        private FileNode CreateNode(string path, int depth, ref int count)
        {
            // 🔒 Protection profondeur + volume
            if (depth > MAX_DEPTH || count > MAX_NODES)
            {
                IsLimitReached = true;
                return null;
            }

            FileNode node = new()
            {
                Name = Path.GetFileName(path),
                Path = path
            };

            node.SelectionChanged += OnNodeSelectionChanged;

            count++; // 🔢 on compte ce node

            if (Directory.Exists(path))
            {
                // =========================
                // 📁 DOSSIERS
                // =========================
                foreach (var dir in Directory.GetDirectories(path))
                {
                    if (count > MAX_NODES)
                    {
                        IsLimitReached = true;
                        break;
                    }

                    try
                    {
                        var child = CreateNode(dir, depth + 1, ref count);
                        if (child != null)
                            node.Children.Add(child);
                    }
                    catch { }
                }

                // =========================
                // 📄 FICHIERS
                // =========================
                foreach (var file in Directory.GetFiles(path))
                {
                    if (count > MAX_NODES)
                    {
                        IsLimitReached = true;
                        break;
                    }

                    try
                    {
                        var child = new FileNode
                        {
                            Name = Path.GetFileName(file),
                            Path = file
                        };

                        child.SelectionChanged += OnNodeSelectionChanged;

                        node.Children.Add(child);
                        count++; // 🔢 on compte le fichier
                    }
                    catch { }
                }
            }

            return node;
        }

        // ======================================================
        // 🔄 APERÇU DYNAMIQUE
        // ======================================================

        private void OnNodeSelectionChanged(FileNode node)
        {
            if (_isBatchUpdating)
                return;

            RefreshPreview();
        }

        private void RefreshPreview()
        {
            var files = GetSelectedFiles();

            if (files.Count == 0)
            {
                PreviewText = "Aucun fichier sélectionné...";
            }
            else
            {
                const int MAX_FILES_PREVIEW = 20;

                var previewFiles = files.Count > MAX_FILES_PREVIEW
                    ? files.GetRange(0, MAX_FILES_PREVIEW)
                    : files;

                var content = FileExportService.BuildContent(previewFiles);

                if (files.Count > MAX_FILES_PREVIEW)
                {
                    content += "\n\n----------------------------------------\n";
                    content += "⚠ Aperçu limité à 20 fichiers...\n";
                }

                PreviewText = content;
            }

            OnPropertyChanged(nameof(CanCopy));
            OnPropertyChanged(nameof(IsPreviewEmpty));
            OnPropertyChanged(nameof(CanExport));
            OnPropertyChanged(nameof(HasEmptyFiles));
        }

        // ======================================================
        // 🔍 FILTRE DE RECHERCHE (SEARCH)
        // ======================================================

        private void ApplyFilter()
        {
            foreach (var root in Tree)
            {
                ApplyFilterToNode(root);
            }
        }

        private bool ApplyFilterToNode(FileNode node)
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

        // Toggle de la visibilité de la barre de recherche
        public void ToggleSearch()
        {
            IsSearchVisible = !IsSearchVisible;
        }

        // ======================================================
        // 📦 EXPORT - CONTENU À EXPORTER
        // ======================================================

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

            return FileExportService.BuildContent(files);
        }
    }
}