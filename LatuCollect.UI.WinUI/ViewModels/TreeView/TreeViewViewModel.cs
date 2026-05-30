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
using LatuCollect.UI.WinUI.Models;
using System.Collections.ObjectModel;

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

        // ═════════════════════════════════════════════════════════════
        // 2. PROPRIÉTÉS PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        // 🌳 Arborescence complète
        public ObservableCollection<FileNode> Tree { get; } = new();

        // 🔍 Arborescence filtrée
        public ObservableCollection<FileNode> FilteredTree { get; } = new();

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
        // 4. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════

        // Extraction progressive depuis MainViewModel
    }
}