/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.ViewModels.TreeView                               ║
║  Fichier : TreeViewViewModel.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer l’état et les interactions du TreeView                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gestion de la recherche                                           ║
║  - Gestion de la visibilité des nodes                                ║
║  - Gestion de l’expansion                                            ║
║  - Gestion de la sélection                                           ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Logique UI uniquement                                             ║
║  - Aucune logique métier                                             ║
║  - Aucun accès disque                                                ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiFileNode = LatuCollect.UI.WinUI.Models.FileNode;

namespace LatuCollect.UI.WinUI.ViewModels.TreeView
{
    public partial class TreeViewViewModel : ObservableObject
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════

        private string _searchText = string.Empty;

        private bool _hasSearchResult = true;

        private bool _isSearchVisible;

        private CancellationTokenSource? _searchCts;

        private readonly HashSet<string> _expandedPaths = new();

        // ═════════════════════════════════════════════════════════════
        // 2. PROPRIÉTÉS PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        // Dossiers actuellement développés
        public IReadOnlyCollection<string> ExpandedPaths =>
            _expandedPaths;

        // 🌳 Arborescence complète
        public ObservableCollection<UiFileNode> Tree { get; } = new();

        // 🔍 Arborescence filtrée
        public ObservableCollection<UiFileNode> FilteredTree { get; } = new();

        // Texte de recherche
        public string SearchText
        {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        // Indique si la recherche retourne des résultats
        public bool HasSearchResult
        {
            get => _hasSearchResult;
            set => SetProperty(ref _hasSearchResult, value);
        }

        // Indique si la zone de recherche est affichée
        public bool IsSearchVisible
        {
            get => _isSearchVisible;
            set => SetProperty(ref _isSearchVisible, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public TreeViewViewModel()
        {
        }

        // ═════════════════════════════════════════════════════════════
        // 4. MÉTHODES PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        // Ajoute un dossier à la liste des dossiers ouverts
        public void AddExpandedPath(
            string path)
        {
            _expandedPaths.Add(path);
        }

        // Supprime un dossier de la liste des dossiers ouverts
        public void RemoveExpandedPath(
            string path)
        {
            _expandedPaths.Remove(path);
        }

        // Vide la liste des dossiers ouverts
        public void ClearExpandedPaths()
        {
            _expandedPaths.Clear();
        }

        // Sauvegarde les dossiers ouverts
        public void SaveExpandedNodes()
        {
            _expandedPaths.Clear();

            foreach (var node in Tree)
            {
                SaveExpandedNodesRecursive(node);
            }
        }

        // Sauvegarde récursive des paths ouverts
        private void SaveExpandedNodesRecursive(
            UiFileNode node)
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

        // Applique le filtre de recherche de manière récursive sur les nodes
        public bool ApplyFilterRecursive(
            UiFileNode node)
        {
            bool match =
                node.Name.Contains(
                    SearchText,
                    StringComparison.OrdinalIgnoreCase);

            bool hasVisibleChild = false;

            foreach (var child in node.Children)
            {
                if (ApplyFilterRecursive(child))
                {
                    hasVisibleChild = true;
                }
            }

            bool isVisible =
                match || hasVisibleChild;

            if (node.IsVisible != isVisible)
            {
                node.IsVisible = isVisible;
            }

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                if (hasVisibleChild &&
                    !node.IsExpanded)
                {
                    node.IsExpanded = true;
                }
            }

            return isVisible;
        }

        // Applique la visibilité à tous les nodes de manière récursive
        public void SetVisibilityRecursive(
            UiFileNode node,
            bool visible)
        {
            node.IsVisible = visible;

            foreach (var child in node.Children)
            {
                SetVisibilityRecursive(
                    child,
                    visible);
            }
        }

        // Synchronisation runtime expansion TreeView
        public void OnNodeExpandedChanged(
            UiFileNode node)
        {
            if (node == null)
                return;

            if (node.IsExpanded)
            {
                AddExpandedPath(node.Path);
            }
            else
            {
                RemoveExpandedPath(node.Path);
            }
        }

        // Récupère la liste des paths actuellement ouverts
        public List<string> GetExpandedPaths()
        {
            return _expandedPaths.ToList();
        }

        // Applique la sélection récursive sur un node et ses enfants
        public void SetNodeSelection(
            UiFileNode node,
            bool isSelected)
        {
            if (node == null)
                return;

            node.IsSelected = isSelected;

            foreach (var child in node.Children)
            {
                SetNodeSelection(child, isSelected);
            }
        }

        // Récupère la liste des fichiers sélectionnés
        public List<string> GetSelectedFiles()
        {
            return GetSelectedFilesOptimized();
        }

        // Récupère la liste des paths sélectionnés de manière récursive
        private List<string> GetSelectedFilesOptimized()
        {
            var result = new List<string>();

            foreach (var node in Tree)
            {
                CollectSelectedFilesRecursive(node, result);
            }

            return result;
        }

        // Indique si la recherche retourne des résultats
        public void UpdateSearchResult(
            bool hasResult)
        {
            HasSearchResult = hasResult;
        }

        // Affiche / masque la zone de recherche
        public void ToggleSearchVisibility()
        {
            IsSearchVisible = !IsSearchVisible;
        }

        // ═════════════════════════════════════════════════════════════
        // 5. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════

        // Extraction progressive depuis MainViewModel

        private void CollectSelectedFilesRecursive(
            UiFileNode node,
            List<string> result)
        {
            // ✔ uniquement fichiers sélectionnés
            if (!node.IsDirectory &&
                node.IsSelected)
            {
                result.Add(node.Path);
            }

            foreach (var child in node.Children)
            {
                CollectSelectedFilesRecursive(child, result);
            }
        }
    }
}