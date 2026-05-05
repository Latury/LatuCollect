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
║  - Représenter une structure arborescente                            ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Logique UI uniquement                                             ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace LatuCollect.UI.WinUI.Models
{
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Représente un node UI (fichier ou dossier)


    public partial class FileNode : ObservableObject
    {
        // ==========================================
        // 🔒 CHAMPS PRIVÉS
        // ==========================================

        private string _name = "";
        private string _path = "";
        private bool _isSelected;
        private bool _isVisible = true;
        private bool _isExpanded;

        // ==========================================
        // 🌐 PROPRIÉTÉS
        // ==========================================

        // Nom affiché
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        // Chemin complet
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        // Sélection utilisateur
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        // Visibilité (recherche)
        public bool IsVisible
        {
            get => _isVisible;
            set => SetProperty(ref _isVisible, value);
        }


        // État d’ouverture du dossier
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        // ==========================================
        // 🌳 STRUCTURE ARBORESCENTE
        // ==========================================

        public ObservableCollection<FileNode> Children { get; } = new();

        // Parent (assigné uniquement lors de la construction)
        public FileNode? Parent { get; internal set; }


        // ==========================================
        // ⚙️ PROPRIÉTÉS CALCULÉES
        // ==========================================

        // Indique si c'est un dossier
        public bool IsFolder => Children.Count > 0;


        // ==========================================
        // 🔧 MÉTHODES PRIVÉES
        // ==========================================
        // Aucune
    }
}