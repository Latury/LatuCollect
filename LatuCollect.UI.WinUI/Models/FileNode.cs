/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI                                                         ║
║  Fichier : FileNode.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter un élément de l’arborescence                            ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Représenter un fichier ou dossier                                 ║
║  - Gérer la sélection                                                ║
║  - Gérer la visibilité (filtrage)                                    ║
║  - Contenir des enfants                                              ║
║  - Notifier les changements de sélection                             ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - CommunityToolkit.Mvvm                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.ObjectModel;

namespace LatuCollect.UI.WinUI.Models
{
    public partial class FileNode : ObservableObject
    {
        // =========================
        // CHAMPS PRIVÉS
        // =========================

        private string _name = string.Empty;
        private string _path = string.Empty;
        private bool _isSelected;
        private bool _isVisible = true;

        // =========================
        // EVENEMENTS
        // =========================

        public event Action<FileNode>? SelectionChanged;

        // =========================
        // PROPRIÉTÉS PUBLIQUES
        // =========================

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (SetProperty(ref _isSelected, value))
                {
                    SelectionChanged?.Invoke(this);
                }
            }
        }

        /// <summary>
        /// Indique si le node est visible (filtrage)
        /// </summary>
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }

        public ObservableCollection<FileNode> Children { get; } = new();

        public bool IsFolder => Children.Count > 0;
    }
}