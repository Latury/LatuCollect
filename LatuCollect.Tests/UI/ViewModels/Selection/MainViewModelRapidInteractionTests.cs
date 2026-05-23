/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : MainViewModelRapidInteractionTests.cs                     ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester les interactions rapides utilisateur                         ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Validation stabilité runtime async                                ║
║  - Validation sélection massive                                      ║
║  - Validation preview obsolète                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Tests.Helpers;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.Tests.UI.ViewModels.Selection
{
    public class MainViewModelRapidInteractionTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — SPAM SÉLECTION RAPIDE PARENT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RapidSelection_ShouldKeepConsistentState()
        {
            // ARRANGE
            var vm = new MainViewModel();

            var root =
                TestTreeFactory.CreateFolder("Root");

            // 🔥 100 fichiers
            for (int i = 0; i < 100; i++)
            {
                var file =
                    TestTreeFactory.CreateFile($"File{i}.cs");

                TestTreeFactory.AddChild(root, file);
            }

            vm.Tree.Add(root);

            // ACT — spam rapide
            for (int i = 0; i < 10; i++)
            {
                await vm.OnNodeSelectionChanged(root, true);

                await vm.OnNodeSelectionChanged(root, false);
            }

            // 🔥 Attend stabilisation preview async
            await Task.Delay(1000);

            // ASSERT — aucun enfant sélectionné
            Assert.All(
                root.Children,
                child => Assert.False(child.IsSelected));

            // ASSERT — parent décoché
            Assert.False(root.IsSelected);

            // ASSERT — état UI stable
            Assert.False(vm.IsLoading);

            Assert.True(
                vm.CurrentState ==
                MainViewModel.UiState.Empty);
        }
    }
}