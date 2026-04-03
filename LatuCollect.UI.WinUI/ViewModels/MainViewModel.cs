/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI                                                         ║
║  Fichier : MainViewModel.cs                                          ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer l’état et la logique de l’interface principale                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gérer la sélection des fichiers                                   ║
║  - Générer l’aperçu dynamique                                        ║
║  - Construire l’arborescence                                         ║
║  - Gérer la recherche (filtrage)                                     ║
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
using LatuCollect.UI.WinUI.Models;
using System.Collections.ObjectModel;
using System.IO;
using System;

namespace LatuCollect.UI.WinUI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        // =========================
        // CHAMPS PRIVÉS (données internes)
        // =========================

        private string _previewText = "Aucun fichier sélectionné...";
        private string _currentFolderPath = string.Empty;
        private string _searchText = string.Empty;

        // =========================
        // PROPRIÉTÉS PUBLIQUES (Binding UI)
        // =========================

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
                {
                    ApplyFilter();
                }
            }
        }

        public ObservableCollection<FileNode> Tree { get; } = new();

        // =========================
        // PROPRIÉTÉS CALCULÉES (UI)
        // =========================

        // Permet de savoir si le bouton Copier doit être activé
        public bool CanCopy => PreviewText != "Aucun fichier sélectionné...";

        // Permet de savoir si l’aperçu est vide
        public bool IsPreviewEmpty => PreviewText == "Aucun fichier sélectionné...";

        // =========================
        // FORMAT EXPORT
        // =========================

        // true = md, false = txt
        private bool _isMarkdownSelected = false;

        public bool IsMarkdownSelected
        {
            get => _isMarkdownSelected;
            set => SetProperty(ref _isMarkdownSelected, value);
        }

        // =========================
        // MÉTHODES PUBLIQUES (API UI)
        // =========================

        public void LoadTree(string path)
        {
            Tree.Clear();

            if (!Directory.Exists(path))
                return;

            Tree.Add(CreateNode(path));
        }

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

                foreach (FileNode child in node.Children)
                {
                    ProcessNode(child);
                }
            }

            foreach (FileNode root in Tree)
            {
                ProcessNode(root);
            }

            return result;
        }

        // =========================
        // MÉTHODES PRIVÉES (LOGIQUE INTERNE)
        // =========================

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
                foreach (string dir in Directory.GetDirectories(path))
                {
                    node.Children.Add(CreateNode(dir));
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    FileNode child = new()
                    {
                        Name = Path.GetFileName(file),
                        Path = file
                    };

                    child.SelectionChanged += OnNodeSelectionChanged;
                    node.Children.Add(child);
                }
            }

            return node;
        }

        private void OnNodeSelectionChanged(FileNode node)
        {
            RefreshPreview();
        }

        private void ApplyFilter()
        {
            foreach (FileNode root in Tree)
            {
                FilterNode(root);
            }
        }

        private bool FilterNode(FileNode node)
        {
            bool match = node.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);

            bool hasVisibleChild = false;

            foreach (FileNode child in node.Children)
            {
                if (FilterNode(child))
                    hasVisibleChild = true;
            }

            node.IsVisible = match || hasVisibleChild;

            return node.IsVisible;
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

                foreach (FileNode child in node.Children)
                {
                    ProcessNode(child);
                }
            }

            foreach (FileNode root in Tree)
            {
                ProcessNode(root);
            }

            if (!hasSelection)
                PreviewText = "Aucun fichier sélectionné...";

            // Met à jour le bouton Copier
            OnPropertyChanged(nameof(CanCopy));
            
            // Met à jour l’état de l’aperçu
            OnPropertyChanged(nameof(IsPreviewEmpty));
        }
    }
}