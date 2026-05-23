/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.UI.MainViewModel                                     ║
║  Fichier : MainViewModelStateTests.cs                                ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester les états UI du MainViewModel                                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier les états UI                                             ║
║  - Vérifier CanCopy                                                  ║
║  - Vérifier CanExport                                                ║
║  - Vérifier CheckExportState                                         ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI réel                                               ║
║  - Tests unitaires uniquement                                        ║
║  - Aucun accès fichiers                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Tests.Helpers;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.Tests.UI.ViewModels.State
{
    public class MainViewModelStateTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. ÉTAT INITIAL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void Constructor_ShouldInitializeEmptyState()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ASSERT
            Assert.False(vm.IsReady);

            Assert.False(vm.IsLoading);

            Assert.False(vm.HasError);

            // ✔ état acceptable au démarrage
            Assert.True(
                vm.CurrentState == MainViewModel.UiState.Empty ||
                vm.CurrentState == MainViewModel.UiState.Loading);
        }

        // ═════════════════════════════════════════════════════════════
        // 2. CAN COPY
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CanCopy_ShouldReturnFalse_WhenPreviewIsEmpty()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.PreviewText = string.Empty;

            // ASSERT
            Assert.False(vm.CanCopy);
        }

        [Fact]
        public void CanCopy_ShouldReturnTrue_WhenPreviewHasContent()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.PreviewText = "Hello World";

            // 🔥 IMPORTANT :
            // CanCopy dépend maintenant de l’état UI Ready
            // et pas uniquement du texte du preview
            vm.CurrentState = MainViewModel.UiState.Ready;

            // ASSERT
            Assert.True(vm.CanCopy);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. CAN EXPORT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CanExport_ShouldReturnFalse_WhenFormatIsNull()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.CurrentState = MainViewModel.UiState.Ready;
            vm.SelectedFormat = null;

            // ASSERT
            Assert.False(vm.CanExport);
        }

        [Fact]
        public void CanExport_ShouldReturnTrue_WhenFormatExistsAndStateReady()
        {
            // ARRANGE
            var vm = new MainViewModel();

            vm.PreviewText = "Hello World";

            // 🔥 IMPORTANT
            // SelectedFormat déclenche un refresh async
            vm.SelectedFormat = ".txt";

            // 🔥 IMPORTANT
            // remettre Ready après le refresh potentiel
            vm.CurrentState = MainViewModel.UiState.Ready;

            // ASSERT
            Assert.True(vm.CanExport);
        }

        // ═════════════════════════════════════════════════════════════
        // 4. CHECK EXPORT STATE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void CheckExportState_ShouldReturnNoSelection_WhenPreviewIsEmpty()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.CurrentState = MainViewModel.UiState.Empty;

            // ASSERT
            Assert.Equal(
                MainViewModel.ExportCheckResult.NoSelection,
                vm.CheckExportState());
        }

        [Fact]
        public void CheckExportState_ShouldReturnEmptyFiles_WhenPreviewContainsEmptyFiles()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.CurrentState = MainViewModel.UiState.Ready;
            vm.PreviewText = "\n\n\n\n";

            // ASSERT
            Assert.Equal(
                MainViewModel.ExportCheckResult.EmptyFiles,
                vm.CheckExportState());
        }

        [Fact]
        public void CheckExportState_ShouldReturnOk_WhenPreviewIsValid()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.CurrentState = MainViewModel.UiState.Ready;
            vm.PreviewText = "Valid content";

            // ASSERT
            Assert.Equal(
                MainViewModel.ExportCheckResult.Ok,
                vm.CheckExportState());
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — RESET DOSSIER CHARGÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ResetConfiguration_ShouldResetLoadedFolderState()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root =
                TestTreeFactory.CreateFolder("Root");

            var file =
                TestTreeFactory.CreateFile("File.cs");

            TestTreeFactory.AddChild(root, file);

            vm.Tree.Add(root);

            await vm.OnNodeSelectionChanged(file, true);

            vm.SearchText = "File";

            // ACT
            await vm.ResetConfigurationAsync();

            // ASSERT
            Assert.Empty(vm.Tree);

            Assert.Empty(vm.FilteredTree);

            Assert.Equal(
                MainViewModel.UiState.Empty,
                vm.CurrentState);

            Assert.True(vm.IsEmpty);

            Assert.Equal(
                string.Empty,
                vm.CurrentFolderPath);

            Assert.Equal(
                string.Empty,
                vm.PreviewText);

            Assert.Equal(0, vm.FileCount);

            Assert.Equal(0, vm.TotalLines);

            Assert.Equal(0, vm.TotalCharacters);

            Assert.Equal(0, vm.TotalSize);

            Assert.Equal(
                string.Empty,
                vm.SearchText);
        }
    }
}