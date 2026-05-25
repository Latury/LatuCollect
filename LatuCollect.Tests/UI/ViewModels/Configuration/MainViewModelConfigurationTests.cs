/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.UI.ViewModels.Configuration                          ║
║  Fichier : MainViewModelConfigurationTests.cs                        ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester la configuration utilisateur du MainViewModel                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier la sauvegarde configuration                              ║
║  - Vérifier la persistance utilisateur                               ║
║  - Vérifier les états runtime sauvegardés                            ║
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

using LatuCollect.UI.WinUI.Models;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.Tests.UI.ViewModels.Configuration
{
    public class MainViewModelConfigurationTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — SAUVEGARDE EXPANDED PATHS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task SaveConfiguration_ShouldPersist_ExpandedPaths()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await Task.Delay(100);

            var folder = new FileNode
            {
                Name = "Folder",
                Path = @"C:\Root\Folder",
                IsDirectory = true,
                IsExpanded = true
            };

            vm.Tree.Add(folder);

            // 🔥 Synchronisation runtime expansion
            vm.SaveExpandedNodesForTests();

            // ACT
            await vm.SaveConfigurationAsync();

            var config =
                vm.GetUserConfigForTests();

            // ASSERT
            Assert.Contains(
                folder.Path,
                config.ExpandedPaths);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — SYNCHRONISATION RUNTIME EXPANDED PATHS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task SaveExpandedNodes_ShouldUpdate_RuntimeExpandedPaths()
        {
            // ARRANGE
            var vm = new MainViewModel();

            // 🔥 Laisse finir l'initialisation async
            await Task.Delay(100);

            var folder = new FileNode
            {
                Name = "Folder",
                Path = @"C:\Root\Folder",
                IsDirectory = true,
                IsExpanded = true
            };

            vm.Tree.Add(folder);

            // ACT
            vm.SaveExpandedNodesForTests();

            // ASSERT
            Assert.Contains(
                folder.Path,
                vm.GetExpandedPathsForTests());
        }
    }
}