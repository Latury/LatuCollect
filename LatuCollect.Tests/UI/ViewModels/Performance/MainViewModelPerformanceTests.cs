/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : MainViewModelPerformanceTests.cs                          ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester la stabilité du MainViewModel sur gros arbres                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier stabilité ApplyFilter                                    ║
║  - Vérifier récursion profonde                                       ║
║  - Vérifier gestion gros volumes                                     ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun benchmark précis                                            ║
║  - Validation stabilité uniquement                                   ║
║  - Aucun contrôle WinUI réel                                         ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Tests.Helpers;
using LatuCollect.UI.WinUI.ViewModels;
using LatuCollect.UI.WinUI.Models;

namespace LatuCollect.Tests.UI.ViewModels.Performance
{
    public class MainViewModelPerformanceTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — GROS ARBRE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void ApplyFilter_ShouldHandleLargeTree()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            // 🔥 Génération arbre massif
            for (int i = 0; i < 5000; i++)
            {
                var file = TestTreeFactory.CreateFile(
                    $"File_{i}.cs");

                TestTreeFactory.AddChild(root, file);
            }

            vm.Tree.Add(root);

            // ACT
            vm.SearchText = "File_4999";

            vm.ApplyFilter();

            // ASSERT
            Assert.True(vm.HasSearchResult);

            Assert.Single(vm.FilteredTree);

            Assert.True(
                root.Children.Any(c =>
                    c.Name == "File_4999.cs"
                    && c.IsVisible));
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — ARBRE PROFOND
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void ApplyFilter_ShouldHandleDeepTree()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            var current = root;

            // 🔥 Génération récursion profonde
            for (int i = 0; i < 1000; i++)
            {
                var folder = TestTreeFactory.CreateFolder(
                    $"Folder_{i}");

                TestTreeFactory.AddChild(current, folder);

                current = folder;
            }

            // 🔥 fichier final
            var targetFile = TestTreeFactory.CreateFile(
                "TargetFile.cs");

            TestTreeFactory.AddChild(current, targetFile);

            vm.Tree.Add(root);

            // ACT
            vm.SearchText = "TargetFile";

            vm.ApplyFilter();

            // ASSERT
            Assert.True(vm.HasSearchResult);

            Assert.True(targetFile.IsVisible);

            Assert.True(current.IsExpanded);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — SÉLECTION MASSIVE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task OnNodeSelectionChanged_ShouldHandleMassiveSelection()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            // 🔥 Génération arbre massif
            for (int i = 0; i < 10000; i++)
            {
                var file = TestTreeFactory.CreateFile(
                    $"File_{i}.cs");

                TestTreeFactory.AddChild(root, file);
            }

            vm.Tree.Add(root);

            // ACT
            await vm.OnNodeSelectionChanged(root, true);

            // ASSERT
            Assert.True(root.IsSelected);

            Assert.Equal(
                10000,
                root.Children.Count(c => c.IsSelected));
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — RESTAURATION VISIBILITÉ APRÈS RECHERCHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void Search_ShouldRestoreHiddenNodes_AfterSearchClear()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root"
            };

            var visibleFile = new FileNode
            {
                Name = "VisibleFile.cs"
            };

            var hiddenFile = new FileNode
            {
                Name = "HiddenFile.cs"
            };

            root.Children.Add(visibleFile);
            visibleFile.Parent = root;

            root.Children.Add(hiddenFile);
            hiddenFile.Parent = root;

            vm.Tree.Add(root);

            // ACT — recherche ciblée
            vm.SearchText = "Visible";
            vm.ApplyFilter();

            // ASSERT — hidden masqué
            Assert.True(visibleFile.IsVisible);

            Assert.False(hiddenFile.IsVisible);

            // ACT — reset recherche
            vm.SearchText = string.Empty;
            vm.ApplyFilter();

            // ASSERT — visibilité restaurée
            Assert.True(visibleFile.IsVisible);

            Assert.True(hiddenFile.IsVisible);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — CONSERVATION SÉLECTION APRÈS RESET FILTRE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void Search_ShouldKeepSelection_AfterFilterReset()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            var visibleFolder =
                TestTreeFactory.CreateFolder("Export_50");

            var hiddenFolder =
                TestTreeFactory.CreateFolder("Hidden");

            var selectedFile =
                TestTreeFactory.CreateFile("Target.cs");

            var hiddenFile =
                TestTreeFactory.CreateFile("HiddenFile.cs");

            TestTreeFactory.AddChild(
                visibleFolder,
                selectedFile);

            TestTreeFactory.AddChild(
                hiddenFolder,
                hiddenFile);

            TestTreeFactory.AddChild(
                root,
                visibleFolder);

            TestTreeFactory.AddChild(
                root,
                hiddenFolder);

            vm.Tree.Add(root);

            // ACT — recherche
            vm.SearchText = "Export_50";

            vm.ApplyFilter();

            // ASSERT — hidden masqué
            Assert.True(visibleFolder.IsVisible);

            Assert.False(hiddenFolder.IsVisible);

            // ACT — sélection visible
            selectedFile.IsSelected = true;

            // ACT — reset filtre
            vm.SearchText = string.Empty;

            vm.ApplyFilter();

            // ASSERT — visibilité restaurée
            Assert.True(hiddenFolder.IsVisible);

            // ASSERT — sélection conservée
            Assert.True(selectedFile.IsSelected);

            // ASSERT — hidden jamais sélectionné
            Assert.False(hiddenFile.IsSelected);
        }
    }
}