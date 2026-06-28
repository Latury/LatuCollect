/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                  ║
║  Module : Tests                                                     ║
║  Fichier : MainViewModelExportTests.cs                              ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester les états export du MainViewModel                           ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI réel                                               ║
║  - Validation logique ViewModel                                      ║
║  - Validation état export                                            ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using LatuCollect.UI.WinUI.ViewModels.Export;
using Xunit;

namespace LatuCollect.Tests.UI.ViewModels.Export
{
    public class MainViewModelExportTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — AUCUNE SÉLECTION
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CheckExportState_ShouldReturnNoSelection_WhenPreviewEmpty()
        {
            // ARRANGE
            var vm = new MainViewModel();

            vm.PreviewText = string.Empty;
            vm.CurrentState = MainViewModel.UiState.Empty;

            // ACT
            var result = vm.CheckExportState();

            // ASSERT
            Assert.Equal(
                ExportViewModel.ExportCheckResult.NoSelection,
                result
            );
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — FICHIERS VIDES
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CheckExportState_ShouldReturnEmptyFiles_WhenPreviewContainsEmptyFiles()
        {
            // ARRANGE
            var vm = new MainViewModel();

            vm.PreviewText = "Test\n\n\n\n";
            vm.CurrentState = MainViewModel.UiState.Ready;

            // ACT
            var result = vm.CheckExportState();

            // ASSERT
            Assert.Equal(
                ExportViewModel.ExportCheckResult.EmptyFiles,
                result
            );
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — EXPORT OK
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CheckExportState_ShouldReturnOk_WhenPreviewValid()
        {
            // ARRANGE
            var vm = new MainViewModel();

            vm.PreviewText = "Contenu valide";
            vm.CurrentState = MainViewModel.UiState.Ready;

            // ACT
            var result = vm.CheckExportState();

            // ASSERT
            Assert.Equal(
                ExportViewModel.ExportCheckResult.Ok,
                result
            );
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — CAN EXPORT FALSE SI FORMAT NULL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CanExport_ShouldBeFalse_WhenFormatNull()
        {
            // ARRANGE
            var vm = new MainViewModel();

            vm.PreviewText = "Contenu";
            vm.CurrentState = MainViewModel.UiState.Ready;

            vm.SelectedFormat = null;

            // ASSERT
            Assert.False(vm.CanExport);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — CAN EXPORT TRUE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CanExport_ShouldBeTrue_WhenPreviewAndFormatValid()
        {
            // ARRANGE
            var vm = new MainViewModel();

            vm.PreviewText = "Contenu";

            // 🔥 IMPORTANT
            // SelectedFormat déclenche un refresh async
            vm.SelectedFormat = ".txt";

            // 🔥 IMPORTANT
            // remettre Ready après le format
            vm.CurrentState = MainViewModel.UiState.Ready;

            // ASSERT
            Assert.True(vm.CanExport);
        }

        [Fact]
        public void CanExport_ShouldReturnFalse_WhenFormatIsEmpty()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.CurrentState = MainViewModel.UiState.Ready;
            vm.SelectedFormat = "";

            // ASSERT
            Assert.False(vm.CanExport);
        }
    }
}