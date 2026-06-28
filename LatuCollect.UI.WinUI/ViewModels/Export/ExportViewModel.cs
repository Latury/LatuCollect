/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.ViewModels.Export                                 ║
║  Fichier : ExportViewModel.cs                                        ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer les paramètres UI liés à l’export                             ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Gestion du mode d’export                                          ║
║  - Stockage des préférences d’export UI                              ║
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
using static LatuCollect.UI.WinUI.ViewModels.MainViewModel;

namespace LatuCollect.UI.WinUI.ViewModels.Export
{
    public partial class ExportViewModel : ObservableObject
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════

        private string _exportMode = "normal";

        private bool _hasEmptyFiles;

        private bool _isPreviewEmpty;

        // ═════════════════════════════════════════════════════════════
        // 2. PROPRIÉTÉS PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        public string ExportMode
        {
            get => _exportMode;
            set => SetProperty(ref _exportMode, value);
        }

        public bool HasEmptyFiles
        {
            get => _hasEmptyFiles;
            set => SetProperty(ref _hasEmptyFiles, value);
        }

        public bool IsPreviewEmpty
        {
            get => _isPreviewEmpty;
            set => SetProperty(ref _isPreviewEmpty, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public ExportViewModel()
        {
        }

        // ═════════════════════════════════════════════════════════════
        // 4. MÉTHODES PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        public ExportCheckResult CheckExportState()
        {
            if (IsPreviewEmpty)
                return ExportCheckResult.NoSelection;

            if (HasEmptyFiles)
                return ExportCheckResult.EmptyFiles;

            return ExportCheckResult.Ok;
        }

        // ═════════════════════════════════════════════════════════════
        // 5. ENUMS
        // ═════════════════════════════════════════════════════════════

        public enum ExportCheckResult
        {
            Ok,
            NoSelection,
            EmptyFiles
        }
    }
}