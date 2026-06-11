/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.ViewModels.Preview                                ║
║  Fichier : PreviewViewModel.cs                                       ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer l’état et les interactions de l’aperçu                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gestion du contenu Preview                                        ║
║  - Gestion des états Preview                                         ║
║  - Gestion du rafraîchissement Preview                               ║
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

namespace LatuCollect.UI.WinUI.ViewModels.Preview
{
    public partial class PreviewViewModel : ObservableObject
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public PreviewViewModel()
        {
        }

        // ═════════════════════════════════════════════════════════════
        // 2. CONTENU PREVIEW
        // ═════════════════════════════════════════════════════════════

        private string _previewText = string.Empty;

        public string PreviewText
        {
            get => _previewText;
            set => SetProperty(ref _previewText, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. STATISTIQUES
        // ═════════════════════════════════════════════════════════════

        private int _fileCount;
        public int FileCount
        {
            get => _fileCount;
            set => SetProperty(ref _fileCount, value);
        }

        private long _totalLines;
        public long TotalLines
        {
            get => _totalLines;
            set => SetProperty(ref _totalLines, value);
        }

        private long _totalCharacters;
        public long TotalCharacters
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
    }
}