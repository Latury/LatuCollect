/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.UI.MainViewModel                                     ║
║  Fichier : MainViewModelPreviewTests.cs                              ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le système de preview du MainViewModel                       ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier les états du preview                                     ║
║  - Vérifier les messages affichés                                    ║
║  - Vérifier CanCopy                                                  ║
║  - Vérifier les statistiques                                         ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI réel                                               ║
║  - Tests unitaires uniquement                                        ║
║  - Tests stables uniquement                                          ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using Xunit;

namespace LatuCollect.Tests.UI.ViewModels.Preview
{
    public class MainViewModelPreviewTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. AUCUN FICHIER SÉLECTIONNÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RefreshPreview_ShouldSetEmptyState_WhenNoFilesSelected()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            await vm.RefreshPreviewForTestsAsync();

            // ASSERT
            Assert.Equal(
                MainViewModel.UiState.Empty,
                vm.CurrentState);

            Assert.Equal(
                "Aucun fichier sélectionné...",
                vm.PreviewText);
        }

        // ═════════════════════════════════════════════════════════════
        // 2. CAN COPY
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RefreshPreview_ShouldDisableCopy_WhenPreviewIsEmpty()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            await vm.RefreshPreviewForTestsAsync();

            // ASSERT
            Assert.False(vm.CanCopy);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. ÉTAT UI
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RefreshPreview_ShouldSetEmptyState_WhenTreeIsEmpty()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            await vm.RefreshPreviewForTestsAsync();

            // ASSERT
            Assert.True(vm.IsEmpty);

            Assert.False(vm.IsReady);
            Assert.False(vm.IsLoading);
            Assert.False(vm.HasError);
        }

        // ═════════════════════════════════════════════════════════════
        // 4. STATISTIQUES
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RefreshPreview_ShouldResetStatistics_WhenNoFilesSelected()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            await vm.RefreshPreviewForTestsAsync();

            // ASSERT
            Assert.Equal(0, vm.FileCount);
            Assert.Equal(0, vm.TotalLines);
            Assert.Equal(0, vm.TotalCharacters);
            Assert.Equal(0, vm.TotalSize);
        }

        // ═════════════════════════════════════════════════════════════
        // 5. EXPORT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RefreshPreview_ShouldDisableExport_WhenNoFilesSelected()
        {
            // ARRANGE
            var vm = new MainViewModel();

            vm.SelectedFormat = ".txt";

            // ACT
            await vm.RefreshPreviewForTestsAsync();

            // ASSERT
            Assert.False(vm.CanExport);
        }

        [Fact]
        public async Task RefreshPreview_ShouldReturnNoSelection_WhenNoFilesSelected()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            await vm.RefreshPreviewForTestsAsync();

            // ASSERT
            Assert.Equal(
                MainViewModel.ExportCheckResult.NoSelection,
                vm.CheckExportState());
        }
    }
}