/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                  ║
║  Module : Tests                                                     ║
║  Fichier : MainViewModelExclusionTests.cs                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester les exclusions TreeView                                     ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI réel                                               ║
║  - Validation logique arbre                                          ║
║  - Validation suppression node                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.Models;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.Tests.UI.ViewModels.Exclusions
{
    public class MainViewModelExclusionTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — SUPPRESSION NODE RACINE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void RemoveNodeFromTree_ShouldRemoveRootNode()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Root",
                Path = @"C:\Root"
            };

            vm.Tree.Add(root);

            // ACT
            vm.RemoveNodeFromTree(root);

            // ASSERT
            Assert.Empty(vm.Tree);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — SUPPRESSION ENFANT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void RemoveNodeFromTree_ShouldRemoveChildNode()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent",
                Path = @"C:\Parent"
            };

            var child = new FileNode
            {
                Name = "Child.cs",
                Path = @"C:\Parent\Child.cs",
                Parent = parent
            };

            parent.Children.Add(child);

            vm.Tree.Add(parent);

            // ACT
            vm.RemoveNodeFromTree(child);

            // ASSERT
            Assert.Empty(parent.Children);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — FILTRE RESTE COHÉRENT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void RemoveNodeFromTree_ShouldRefreshFilter()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root = new FileNode
            {
                Name = "Folder",
                Path = @"C:\Folder"
            };

            var file1 = new FileNode
            {
                Name = "Test1.cs",
                Path = @"C:\Folder\Test1.cs",
                Parent = root
            };

            var file2 = new FileNode
            {
                Name = "Test2.cs",
                Path = @"C:\Folder\Test2.cs",
                Parent = root
            };

            root.Children.Add(file1);
            root.Children.Add(file2);

            vm.Tree.Add(root);

            // 🔥 active recherche
            vm.SearchText = "Test";

            vm.ApplyFilter();

            // ACT
            vm.RemoveNodeFromTree(file1);

            // ASSERT
            Assert.Single(root.Children);

            Assert.Contains(
                root.Children,
                n => n.Name == "Test2.cs"
            );
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — NODE NULL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void RemoveNodeFromTree_ShouldIgnoreNull()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // ACT
            vm.RemoveNodeFromTree(null);

            // ASSERT
            Assert.Empty(vm.Tree);
        }
    }
}