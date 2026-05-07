/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                  ║
║  Module : Tests                                                     ║
║  Fichier : MainViewModelSearchTests.cs                              ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le système de recherche TreeView                            ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Validation logique visibilité                                    ║
║  - Validation expansion automatique                                 ║
║  - Aucun accès UI réel                                               ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.Models;
using LatuCollect.UI.WinUI.ViewModels;
using System.Linq;

namespace LatuCollect.Tests
{
    public class MainViewModelSearchTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — FICHIER TROUVÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task Search_ShouldShowMatchingFile()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root"
            };

            var file = new FileNode
            {
                Name = "TestFile.cs"
            };

            root.Children.Add(file);

            vm.Tree.Add(root);

            // ACT
            vm.SearchText = "Test";

            vm.ApplyFilter();

            // ASSERT
            Assert.True(file.IsVisible);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — FICHIER MASQUÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task Search_ShouldHideNonMatchingFile()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root"
            };

            var file = new FileNode
            {
                Name = "Hello.cs"
            };

            root.Children.Add(file);

            vm.Tree.Add(root);

            // ACT
            vm.SearchText = "ZZZ";

            vm.ApplyFilter();

            // ASSERT
            Assert.False(file.IsVisible);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — PARENT VISIBLE SI ENFANT MATCH
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task Search_ShouldKeepParentVisible_WhenChildMatches()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Folder"
            };

            var child = new FileNode
            {
                Name = "ImportantFile.cs"
            };

            root.Children.Add(child);

            vm.Tree.Add(root);

            // ACT
            vm.SearchText = "Important";

            vm.ApplyFilter();

            // ASSERT
            Assert.True(root.IsVisible);
            Assert.True(child.IsVisible);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — EXPANSION AUTO
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task Search_ShouldExpandParent_WhenChildMatches()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Folder"
            };

            var child = new FileNode
            {
                Name = "Target.cs"
            };

            root.Children.Add(child);

            vm.Tree.Add(root);

            // ACT
            vm.SearchText = "Target";

            vm.ApplyFilter();

            // ASSERT
            Assert.True(root.IsExpanded);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — VisibleChildren ne retourne que les visibles
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void VisibleChildren_ShouldReturnOnlyVisibleNodes()
        {
            // ARRANGE
            var parent = new FileNode
            {
                Name = "Parent"
            };

            var visibleChild = new FileNode
            {
                Name = "Visible.cs",
                IsVisible = true
            };

            var hiddenChild = new FileNode
            {
                Name = "Hidden.cs",
                IsVisible = false
            };

            parent.Children.Add(visibleChild);
            parent.Children.Add(hiddenChild);

            // ACT
            var result = parent.VisibleChildren.ToList();

            // ASSERT
            Assert.Single(result);

            Assert.Contains(
                result,
                n => n.Name == "Visible.cs");

            Assert.DoesNotContain(
                result,
                n => n.Name == "Hidden.cs");
        }
    }
}
