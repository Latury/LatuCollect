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
using System.Collections.Generic;
using System.Linq;

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

        // ═════════════════════════════════════════════════════════════
        // 4. ÉTATS PREVIEW
        // ═════════════════════════════════════════════════════════════

        private string _lastSelectionSignature = string.Empty;

        public string LastSelectionSignature
        {
            get => _lastSelectionSignature;
            set => SetProperty(ref _lastSelectionSignature, value);
        }

        private bool _lastIsMarkdown;

        public bool LastIsMarkdown
        {
            get => _lastIsMarkdown;
            set => SetProperty(ref _lastIsMarkdown, value);
        }

        private bool _isPreviewLoading;

        public bool IsPreviewLoading
        {
            get => _isPreviewLoading;
            set => SetProperty(ref _isPreviewLoading, value);
        }

        private int _previewRequestId;

        public int PreviewRequestId
        {
            get => _previewRequestId;
            set => SetProperty(ref _previewRequestId, value);
        }

        private int _lastCompletedPreviewId;

        public int LastCompletedPreviewId
        {
            get => _lastCompletedPreviewId;
            set => SetProperty(ref _lastCompletedPreviewId, value);
        }

        private bool _hasShownPartialWarning;

        public bool HasShownPartialWarning
        {
            get => _hasShownPartialWarning;
            set => SetProperty(ref _hasShownPartialWarning, value);
        }

        private bool _isPartial;

        public bool IsPartial
        {
            get => _isPartial;
            set => SetProperty(ref _isPartial, value);
        }

        private string _partialMessage = string.Empty;

        public string PartialMessage
        {
            get => _partialMessage;
            set => SetProperty(ref _partialMessage, value);
        }

        private bool _isPreviewTruncated;

        public bool IsPreviewTruncated
        {
            get => _isPreviewTruncated;
            set => SetProperty(ref _isPreviewTruncated, value);
        }

        // ═════════════════════════════════════════════════════════════
        // 5. MÉTHODES PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        public void ResetPreview()
        {
            FileCount = 0;
            TotalLines = 0;
            TotalCharacters = 0;
            TotalSize = 0;

            IsPartial = false;
            PartialMessage = string.Empty;
            IsPreviewTruncated = false;
        }

        public void ApplyStatistics(
            int fileCount,
            long totalLines,
            long totalCharacters,
            long totalSize)
        {
            FileCount = fileCount;
            TotalLines = totalLines;
            TotalCharacters = totalCharacters;
            TotalSize = totalSize;
        }

        public void ApplyPreviewContent(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                PreviewText = string.Empty;
                IsPreviewTruncated = false;
                return;
            }

            if (content.Length > MAX_PREVIEW_LENGTH)
            {
                PreviewText = content.Substring(0, MAX_PREVIEW_LENGTH);
                IsPreviewTruncated = true;
            }
            else
            {
                PreviewText = content;
                IsPreviewTruncated = false;
            }
        }

        public string BuildSelectionSignature(
            List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0)
                return string.Empty;

            var ordered = filePaths.OrderBy(p => p);

            return string.Join("|", ordered);
        }

        public bool IsPreviewUpToDate(
            string currentSignature,
            bool isMarkdown)
        {
            return currentSignature ==
                   LastSelectionSignature
                   &&
                   isMarkdown ==
                   LastIsMarkdown;
        }

        public void UpdatePreviewState(
            string currentSignature,
            bool isMarkdown)
        {
            LastSelectionSignature = currentSignature;
            LastIsMarkdown = isMarkdown;
        }

        public void CompletePreviewRequest(
            int requestId)
        {
            LastCompletedPreviewId = requestId;
        }

        // ═════════════════════════════════════════════════════════════
        // 6. CONSTANTES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_PREVIEW_LENGTH = 100_000;

    }
}