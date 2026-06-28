/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.UI.ViewModels.Selection                              ║
║  Fichier : MainViewModelSelectionTests.cs                            ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le comportement de sélection du MainViewModel                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier propagation sélection                                    ║
║  - Vérifier stabilité sélection massive                              ║
║  - Vérifier cohérence sélection ↔ visibilité                         ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI réel                                               ║
║  - Aucun test WinUI                                                  ║
║  - Validation logique uniquement                                     ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Tests.Helpers;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.Tests.UI.ViewModels.Selection
{
    public class MainViewModelSelectionTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — PROPAGATION SÉLECTION DOSSIER
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task OnNodeSelectionChanged_ShouldSelectAllChildren()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            var file1 = TestTreeFactory.CreateFile("File1.cs");
            var file2 = TestTreeFactory.CreateFile("File2.cs");
            var file3 = TestTreeFactory.CreateFile("File3.cs");

            TestTreeFactory.AddChild(root, file1);
            TestTreeFactory.AddChild(root, file2);
            TestTreeFactory.AddChild(root, file3);

            vm.Tree.Add(root);

            // ACT
            await vm.OnNodeSelectionChanged(root, true);

            // ASSERT
            Assert.True(root.IsSelected);

            Assert.True(file1.IsSelected);
            Assert.True(file2.IsSelected);
            Assert.True(file3.IsSelected);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — PROPAGATION DÉSÉLECTION DOSSIER
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task OnNodeSelectionChanged_ShouldUnselectAllChildren()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            var file1 = TestTreeFactory.CreateFile("File1.cs");
            var file2 = TestTreeFactory.CreateFile("File2.cs");
            var file3 = TestTreeFactory.CreateFile("File3.cs");

            TestTreeFactory.AddChild(root, file1);
            TestTreeFactory.AddChild(root, file2);
            TestTreeFactory.AddChild(root, file3);

            vm.Tree.Add(root);

            // 🔥 Pré-sélection
            await vm.OnNodeSelectionChanged(root, true);

            // ACT
            await vm.OnNodeSelectionChanged(root, false);

            // ASSERT
            Assert.False(root.IsSelected);

            Assert.False(file1.IsSelected);
            Assert.False(file2.IsSelected);
            Assert.False(file3.IsSelected);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — CONSERVATION SÉLECTION APRÈS RECHERCHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void Search_ShouldKeepSelection_AfterSearchReset()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            var visibleFolder =
                TestTreeFactory.CreateFolder("VisibleFolder");

            var hiddenFolder =
                TestTreeFactory.CreateFolder("HiddenFolder");

            var selectedFile =
                TestTreeFactory.CreateFile("Target.cs");

            var hiddenFile =
                TestTreeFactory.CreateFile("Hidden.cs");

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

            // 🔥 sélection fichier visible
            selectedFile.IsSelected = true;

            // ACT — recherche
            vm.SearchText = "VisibleFolder";

            vm.ApplyFilter();

            // ASSERT — hidden masqué
            Assert.True(visibleFolder.IsVisible);

            Assert.False(hiddenFolder.IsVisible);

            // ACT — reset recherche
            vm.SearchText = string.Empty;

            vm.ApplyFilter();

            // ASSERT — visibilité restaurée
            Assert.True(hiddenFolder.IsVisible);

            // ASSERT — sélection conservée
            Assert.True(selectedFile.IsSelected);

            // ASSERT — hidden jamais sélectionné
            Assert.False(hiddenFile.IsSelected);
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

            // 🔥 arbre massif
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
        // TEST — MULTI-CLIC RAPIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task OnNodeSelectionChanged_ShouldRemainStable_AfterRapidChanges()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            for (int i = 0; i < 1000; i++)
            {
                var file = TestTreeFactory.CreateFile(
                    $"File_{i}.cs");

                TestTreeFactory.AddChild(root, file);
            }

            vm.Tree.Add(root);

            // ACT — changements rapides
            for (int i = 0; i < 20; i++)
            {
                await vm.OnNodeSelectionChanged(root, true);

                await vm.OnNodeSelectionChanged(root, false);
            }

            // ASSERT
            Assert.False(root.IsSelected);

            Assert.All(
                root.Children,
                child => Assert.False(child.IsSelected));
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — VISIBILITÉ ET SÉLECTION
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void HiddenNode_ShouldKeepSelectionState()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            var visibleFile =
                TestTreeFactory.CreateFile("Visible.cs");

            var hiddenFile =
                TestTreeFactory.CreateFile("Hidden.cs");

            TestTreeFactory.AddChild(root, visibleFile);
            TestTreeFactory.AddChild(root, hiddenFile);

            vm.Tree.Add(root);

            // 🔥 sélection initiale
            visibleFile.IsSelected = true;
            hiddenFile.IsSelected = true;

            // ACT — simulation invisibilité
            hiddenFile.IsVisible = false;

            // ASSERT
            Assert.True(visibleFile.IsSelected);

            // ASSERT — invisible mais sélection conservée
            Assert.True(hiddenFile.IsSelected);

            // ASSERT — visibilité correcte
            Assert.False(hiddenFile.IsVisible);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — REFRESH PREVIEW RAPIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RefreshPreview_ShouldKeepLatestSelection()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = TestTreeFactory.CreateFolder("Root");

            var file1 =
                TestTreeFactory.CreateFile("File1.cs");

            var file2 =
                TestTreeFactory.CreateFile("File2.cs");

            TestTreeFactory.AddChild(root, file1);
            TestTreeFactory.AddChild(root, file2);

            vm.Tree.Add(root);

            // ACT — sélection rapide
            await vm.OnNodeSelectionChanged(file1, true);

            await vm.OnNodeSelectionChanged(file2, true);

            // 🔥 IMPORTANT
            // On attend que le preview soit généré après la sélection
            await vm.WaitForPreviewAsync();

            // ASSERT — état sélection cohérent
            Assert.True(file1.IsSelected);

            Assert.True(file2.IsSelected);

            // ASSERT — preview généré
            Assert.False(string.IsNullOrWhiteSpace(vm.PreviewText));
        }
    }
}