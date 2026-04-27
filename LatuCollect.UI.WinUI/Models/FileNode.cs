/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Models                                            ║
║  Fichier : FileNode.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter un fichier ou dossier pour l’interface utilisateur      ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gérer l’affichage (Name, Path)                                    ║
║  - Gérer la sélection utilisateur                                    ║
║  - Gérer la visibilité (recherche)                                   ║
║  - Notifier l’UI (binding)                                           ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Contient de la logique UI (autorisé)                              ║
║  - Ne doit PAS contenir de logique métier                            ║
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
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════

        private string _name = "";
        private string _path = "";
        private bool _isSelected;
        private bool _isVisible = true;


        // ═════════════════════════════════════════════════════════════
        // 2. INFORMATIONS FICHIER / DOSSIER
        // ═════════════════════════════════════════════════════════════

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


        // ═════════════════════════════════════════════════════════════
        // 3. ÉTAT DE SÉLECTION (UI)
        // ═════════════════════════════════════════════════════════════

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

        public event Action<FileNode>? SelectionChanged;


        // ═════════════════════════════════════════════════════════════
        // 4. VISIBILITÉ (RECHERCHE)
        // ═════════════════════════════════════════════════════════════

        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }


        // ═════════════════════════════════════════════════════════════
        // 5. STRUCTURE ARBORESCENTE
        // ═════════════════════════════════════════════════════════════

        public ObservableCollection<FileNode> Children { get; } = new();
    }
}