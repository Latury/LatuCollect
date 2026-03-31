/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI                                                         ║
║  Fichier : MainViewModel.cs                                          ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer l’état et la logique de l’interface principale                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gérer la sélection des fichiers                                   ║
║  - Générer l’aperçu dynamique                                        ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - CommunityToolkit.Mvvm                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using CommunityToolkit.Mvvm.ComponentModel;

namespace LatuCollect.UI.WinUI.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        // =========================
        // CHAMPS PRIVÉS
        // =========================

        private string _previewText = "Aucun fichier sélectionné...";
        private bool _isFileImportChecked;
        private bool _isFileExportChecked;
        private bool _isMainWindowChecked;
        private bool _isReadmeChecked;

        // =========================
        // PROPRIÉTÉS
        // =========================

        public string PreviewText
        {
            get => _previewText;
            set => SetProperty(ref _previewText, value);
        }

        public bool IsFileImportChecked
        {
            get => _isFileImportChecked;
            set
            {
                if (SetProperty(ref _isFileImportChecked, value))
                    RefreshPreview();
            }
        }

        public bool IsFileExportChecked
        {
            get => _isFileExportChecked;
            set
            {
                if (SetProperty(ref _isFileExportChecked, value))
                    RefreshPreview();
            }
        }

        public bool IsMainWindowChecked
        {
            get => _isMainWindowChecked;
            set
            {
                if (SetProperty(ref _isMainWindowChecked, value))
                    RefreshPreview();
            }
        }

        public bool IsReadmeChecked
        {
            get => _isReadmeChecked;
            set
            {
                if (SetProperty(ref _isReadmeChecked, value))
                    RefreshPreview();
            }
        }

        // =========================
        // LOGIQUE
        // =========================

        private void RefreshPreview()
        {
            PreviewText = "";

            bool hasSelection = false;

            void AddFile(string name)
            {
                hasSelection = true;

                PreviewText +=
                    $"{name}\n\n\n" +
                    $"Contenu simulé du fichier...\n\n\n" +
                    $"----------------------------------------\n\n\n";
            }

            if (IsFileImportChecked) AddFile("FileImportService.cs");
            if (IsFileExportChecked) AddFile("FileExportService.cs");
            if (IsMainWindowChecked) AddFile("MainWindow.xaml");
            if (IsReadmeChecked) AddFile("README.md");

            if (!hasSelection)
            {
                PreviewText = "Aucun fichier sélectionné...";
            }
        }
    }
}