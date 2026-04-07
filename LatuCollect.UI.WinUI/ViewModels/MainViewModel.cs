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
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace LatuCollect.UI.WinUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // ======================================================
        // 🧠 CHAMPS PRIVÉS
        // ======================================================

        private string _previewText = "Aucun fichier sélectionné...";
        private string _currentFolderPath = string.Empty;
        private string _searchText = string.Empty;

        // ======================================================
        // 📦 PROPRIÉTÉS PUBLIQUES
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
        // 🔍 ÉTATS UI
        // ======================================================

        public bool HasEmptyFiles => PreviewText.Contains("\n\n\n\n");
        public bool CanCopy => PreviewText != "Aucun fichier sélectionné...";
        public bool IsPreviewEmpty => PreviewText == "Aucun fichier sélectionné...";

        // ======================================================
        // 📄 FORMAT EXPORT
        // ======================================================

        private string? _selectedFormat = null;

        public string SelectedFormat
        {
            get => _selectedFormat;
            set
            {
                if (SetProperty(ref _selectedFormat, value))
                    OnPropertyChanged(nameof(CanExport));
            }
        }

        public bool CanExport =>
            !IsPreviewEmpty &&
            SelectedFormat != null;

        // ======================================================
        // 📊 EXPORT CHECK
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
        // 🧪 SIMULATION (UI STATE)
        // ======================================================

        private bool _isSimulationEnabled = false;
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

        private string _selectedSimulationScenario = "Aucun";
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
        // 🌳 ARBORESCENCE (SIMPLIFIÉE)
        // ======================================================

        public void LoadTree(string path)
        {
            Tree.Clear();

            if (!Directory.Exists(path))
                return;

            Tree.Add(CreateNode(path));
        }

        private FileNode CreateNode(string path)
        {
            FileNode node = new()
            {
                Name = Path.GetFileName(path),
                Path = path
            };

            node.SelectionChanged += OnNodeSelectionChanged;

            if (Directory.Exists(path))
            {
                // 📁 DOSSIERS
                foreach (var dir in Directory.GetDirectories(path))
                {
                    try
                    {
                        node.Children.Add(CreateNode(dir));
                    }
                    catch (Exception)
                    {
                        // ignore → stabilité
                    }
                }

                // 📄 FICHIERS
                foreach (var file in Directory.GetFiles(path))
                {
                    try
                    {
                        var child = new FileNode
                        {
                            Name = Path.GetFileName(file),
                            Path = file
                        };

                        child.SelectionChanged += OnNodeSelectionChanged;
                        node.Children.Add(child);
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                }
            }

            return node;
        }

        // ======================================================
        // 🔄 SÉLECTION / APERÇU
        // ======================================================

        private void OnNodeSelectionChanged(FileNode node)
        {
            RefreshPreview();
        }

        private void RefreshPreview()
        {
            PreviewText = "";
            bool hasSelection = false;

            void ProcessNode(FileNode node)
            {
                if (node.IsSelected && File.Exists(node.Path))
                {
                    hasSelection = true;

                    string content = FileReaderService.ReadFile(node.Path);

                    PreviewText +=
                        $"{node.Path}\n\n\n" +
                        $"{content}\n\n\n" +
                        $"----------------------------------------\n\n\n";
                }

                foreach (var child in node.Children)
                    ProcessNode(child);
            }

            foreach (var root in Tree)
                ProcessNode(root);

            if (!hasSelection)
                PreviewText = "Aucun fichier sélectionné...";

            OnPropertyChanged(nameof(CanCopy));
            OnPropertyChanged(nameof(IsPreviewEmpty));
            OnPropertyChanged(nameof(CanExport));
            OnPropertyChanged(nameof(HasEmptyFiles));
        }

        // ======================================================
        // 🔍 FILTRAGE
        // ======================================================

        private void ApplyFilter()
        {
            foreach (var root in Tree)
                FilterNode(root);
        }

        private bool FilterNode(FileNode node)
        {
            bool match = node.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            bool hasChild = false;

            foreach (var child in node.Children)
                if (FilterNode(child))
                    hasChild = true;

            node.IsVisible = match || hasChild;

            return node.IsVisible;
        }

        // ======================================================
        // 📦 EXPORT
        // ======================================================

        public string GetExportContent()
        {
            return BuildExportContent();
        }

        public string BuildExportContent()
        {
            string result = "";

            void ProcessNode(FileNode node)
            {
                if (node.IsSelected && File.Exists(node.Path))
                {
                    string content = FileReaderService.ReadFile(node.Path);

                    result +=
                        $"{node.Path}\n\n\n" +
                        $"{content}\n\n\n" +
                        $"----------------------------------------\n\n\n";
                }

                foreach (var child in node.Children)
                    ProcessNode(child);
            }

            foreach (var root in Tree)
                ProcessNode(root);

            return result;
        }
    }
}