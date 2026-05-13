/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                  ║
║  Module : Tests                                                     ║
║  Fichier : MainViewModelSelectionLogicTests.cs                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester la logique de sélection TreeView                            ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI réel                                               ║
║  - Validation sélection récursive                                    ║
║  - Validation état parent/enfant                                     ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.Models;
using LatuCollect.UI.WinUI.ViewModels;
using Xunit;

namespace LatuCollect.Tests.UI.ViewModels.Selection
{
    public class MainViewModelSelectionLogicTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — SÉLECTION PARENT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void HandleNodeClick_ShouldSelectAllChildren()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent"
            };

            var child1 = new FileNode
            {
                Name = "Child1",
                Parent = parent
            };

            var child2 = new FileNode
            {
                Name = "Child2",
                Parent = parent
            };

            parent.Children.Add(child1);
            parent.Children.Add(child2);

            parent.IsSelected = true;

            // ACT
            vm.HandleNodeClick(parent);

            // ASSERT
            Assert.True(child1.IsSelected);
            Assert.True(child2.IsSelected);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — DÉSÉLECTION PARENT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void HandleNodeClick_ShouldUnselectAllChildren()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent",
                IsSelected = false
            };

            var child = new FileNode
            {
                Name = "Child",
                Parent = parent,
                IsSelected = true
            };

            parent.Children.Add(child);

            // ACT
            vm.HandleNodeClick(parent);

            // ASSERT
            Assert.False(child.IsSelected);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — PARENT PARTIEL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void HandleNodeClick_ShouldSetParentPartial()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent"
            };

            var child1 = new FileNode
            {
                Name = "Child1",
                Parent = parent,
                IsSelected = true
            };

            var child2 = new FileNode
            {
                Name = "Child2",
                Parent = parent,
                IsSelected = false
            };

            parent.Children.Add(child1);
            parent.Children.Add(child2);

            // ACT
            vm.HandleNodeClick(child1);

            // ASSERT
            Assert.Null(parent.IsSelected);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — TOUS ENFANTS SÉLECTIONNÉS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void HandleNodeClick_ShouldSetParentSelected()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent"
            };

            var child1 = new FileNode
            {
                Name = "Child1",
                Parent = parent,
                IsSelected = true
            };

            var child2 = new FileNode
            {
                Name = "Child2",
                Parent = parent,
                IsSelected = true
            };

            parent.Children.Add(child1);
            parent.Children.Add(child2);

            // ACT
            vm.HandleNodeClick(child1);

            // ASSERT
            Assert.True(parent.IsSelected);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — AUCUN ENFANT SÉLECTIONNÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void HandleNodeClick_ShouldSetParentUnselected()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent"
            };

            var child1 = new FileNode
            {
                Name = "Child1",
                Parent = parent,
                IsSelected = false
            };

            var child2 = new FileNode
            {
                Name = "Child2",
                Parent = parent,
                IsSelected = false
            };

            parent.Children.Add(child1);
            parent.Children.Add(child2);

            // ACT
            vm.HandleNodeClick(child1);

            // ASSERT
            Assert.False(parent.IsSelected);
        }
    }
}