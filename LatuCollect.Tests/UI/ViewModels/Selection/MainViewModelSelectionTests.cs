/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : MainViewModelSelectionTests.cs                            ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester la cohérence de sélection du TreeView                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier propagation parent → enfants                             ║
║  - Vérifier propagation enfants → parent                             ║
║  - Vérifier stabilité sélection                                       ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Tests ViewModel uniquement                                        ║
║  - Aucun contrôle WinUI réel                                         ║
║  - Aucune logique XAML                                               ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.Models;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.Tests.UI.ViewModels.Selection
{
    public class MainViewModelSelectionTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — PARENT → ENFANTS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void HandleNodeClick_ShouldSelectAllChildren_WhenParentSelected()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent"
            };

            var child1 = new FileNode
            {
                Name = "Child1"
            };

            var child2 = new FileNode
            {
                Name = "Child2"
            };

            parent.Children.Add(child1);
            parent.Children.Add(child2);

            // simulate checkbox checked
            parent.IsSelected = true;

            // ACT
            vm.HandleNodeClick(parent);

            // ASSERT
            Assert.True(parent.IsSelected);
            Assert.True(child1.IsSelected);
            Assert.True(child2.IsSelected);
        }

        [Fact]
        public void HandleNodeClick_ShouldUnselectParent_WhenChildUnchecked()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var parent = new FileNode
            {
                Name = "Parent",
                IsSelected = true
            };

            var child1 = new FileNode
            {
                Name = "Child1",
                IsSelected = true
            };

            var child2 = new FileNode
            {
                Name = "Child2",
                IsSelected = true
            };

            parent.Children.Add(child1);
            parent.Children.Add(child2);

            // ⚠ simulation arbre réel
            typeof(FileNode)
                .GetProperty(nameof(FileNode.Parent))!
                .SetValue(child1, parent);

            typeof(FileNode)
                .GetProperty(nameof(FileNode.Parent))!
                .SetValue(child2, parent);

            // simulate uncheck child
            child1.IsSelected = false;

            // ACT
            vm.HandleNodeClick(child1);

            // ASSERT
            Assert.Null(parent.IsSelected);
        }
    }
}