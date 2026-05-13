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

namespace LatuCollect.Tests.UI.ViewModels.Search
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

        // ═════════════════════════════════════════════════════════════
        // TEST — RESET RECHERCHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void Search_ShouldRestoreVisibility_WhenSearchIsCleared()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root"
            };

            var file1 = new FileNode
            {
                Name = "App.xaml"
            };

            var file2 = new FileNode
            {
                Name = "Hello.cs"
            };

            root.Children.Add(file1);
            root.Children.Add(file2);

            vm.Tree.Add(root);

            // ACT — recherche
            vm.SearchText = "App";
            vm.ApplyFilter();

            // ASSERT — pendant recherche
            Assert.True(file1.IsVisible);
            Assert.False(file2.IsVisible);

            // ACT — reset recherche
            vm.SearchText = string.Empty;
            vm.ApplyFilter();

            // ASSERT — tout redevient visible
            Assert.True(root.IsVisible);
            Assert.True(file1.IsVisible);
            Assert.True(file2.IsVisible);

        }

        // ═════════════════════════════════════════════════════════════
        // TEST — RESET EXPANSION APRÈS RECHERCHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void Search_ShouldExpandParent_WhenSearching()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root"
            };

            var folder = new FileNode
            {
                Name = "Components"
            };

            var file = new FileNode
            {
                Name = "App.xaml"
            };

            root.Children.Add(folder);

            folder.Parent = root;

            folder.Children.Add(file);

            file.Parent = folder;

            vm.Tree.Add(root);

            // ASSERT INITIAL
            Assert.False(folder.IsExpanded);

            // ACT
            vm.SearchText = "App";
            vm.ApplyFilter();

            // ASSERT
            Assert.True(folder.IsExpanded);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — SUPPRESSION NODE ENFANT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void RemoveNodeFromTree_ShouldRemoveChildNode()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root"
            };

            var child1 = new FileNode
            {
                Name = "Keep.cs"
            };

            var child2 = new FileNode
            {
                Name = "Remove.cs"
            };

            root.Children.Add(child1);
            root.Children.Add(child2);

            child1.Parent = root;
            child2.Parent = root;

            vm.Tree.Add(root);

            // IMPORTANT
            vm.ApplyFilter();

            // ACT
            vm.RemoveNodeFromTree(child2);

            // ASSERT
            Assert.Single(root.Children);

            Assert.Contains(
                root.Children,
                n => n.Name == "Keep.cs");

            Assert.DoesNotContain(
                root.Children,
                n => n.Name == "Remove.cs");
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — SUPPRESSION NODE RACINE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void RemoveNodeFromTree_ShouldRemoveRootNode()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root1 = new FileNode
            {
                Name = "Root1"
            };

            var root2 = new FileNode
            {
                Name = "Root2"
            };

            vm.Tree.Add(root1);
            vm.Tree.Add(root2);

            vm.ApplyFilter();

            // ACT
            vm.RemoveNodeFromTree(root1);

            // ASSERT
            Assert.Single(vm.Tree);

            Assert.Contains(
                vm.Tree,
                n => n.Name == "Root2");

            Assert.DoesNotContain(
                vm.Tree,
                n => n.Name == "Root1");
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — RESET EXPANSION APRÈS CLEAR RECHERCHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void Search_ShouldKeepExpandedNodes_WhenSearchIsCleared()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root"
            };

            var folder = new FileNode
            {
                Name = "Folder"
            };

            var file = new FileNode
            {
                Name = "TargetFile.cs"
            };

            root.Children.Add(folder);
            folder.Parent = root;

            folder.Children.Add(file);
            file.Parent = folder;

            vm.Tree.Add(root);

            // ACT — recherche
            vm.SearchText = "Target";
            vm.ApplyFilter();

            // ASSERT — expansion auto
            Assert.True(folder.IsExpanded);

            // ACT — reset recherche
            vm.SearchText = string.Empty;
            vm.ApplyFilter();

            // ASSERT — expansion conservée
            Assert.True(folder.IsExpanded);
        }
    }
}