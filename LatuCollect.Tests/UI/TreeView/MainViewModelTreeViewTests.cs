/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.UI.ViewModels.TreeView                               ║
║  Fichier : MainViewModelTreeViewTests.cs                             ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester les comportements TreeView du MainViewModel                  ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier conservation état ouvert                                 ║
║  - Vérifier stabilité expansion                                      ║
║  - Vérifier rebuild arbre UI                                         ║
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

using LatuCollect.Core.Models;
using LatuCollect.Tests.Helpers;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.Tests.UI.ViewModels.TreeView
{
    public class MainViewModelTreeViewTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — CONSERVATION ÉTAT OUVERT APRÈS RELOAD
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void ExpandedNodes_ShouldRemainExpanded_AfterReload()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root =
                TestTreeFactory.CreateFolder("Root");

            var folder =
                TestTreeFactory.CreateFolder("Folder");

            TestTreeFactory.AddChild(root, folder);

            vm.Tree.Add(root);

            // ACT — ouverture dossier
            folder.IsExpanded = true;

            vm.SaveExpandedNodesForTests();

            var coreRoot = new FileNode
            {
                Name = root.Name,
                Path = root.Path,
                IsDirectory = true
            };

            var coreFolder = new FileNode
            {
                Name = folder.Name,
                Path = folder.Path,
                IsDirectory = true
            };

            coreRoot.Children.Add(coreFolder);

            var rebuilt =
                vm.ConvertToUiNodeForTests(coreRoot);

            // ASSERT
            Assert.True(
                rebuilt.Children[0].IsExpanded);
        }
    }
}