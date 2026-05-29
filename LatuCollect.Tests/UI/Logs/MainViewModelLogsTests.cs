/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.UI.ViewModels.Logs                                   ║
║  Fichier : MainViewModelLogsTests.cs                                 ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le système de logs du MainViewModel                          ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier HasLogErrors                                             ║
║  - Vérifier LogErrorCount                                            ║
║  - Vérifier FilteredLogs                                             ║
║  - Vérifier les filtres runtime                                      ║
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

using LatuCollect.Core.Logging.Interfaces;
using LatuCollect.UI.WinUI.Models.Logs;
using LatuCollect.UI.WinUI.ViewModels;
using LatuCollect.UI.WinUI.ViewModels.Logs;

namespace LatuCollect.Tests.UI.ViewModels.Logs
{
    public class MainViewModelLogsTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — HAS LOG ERRORS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task HasLogErrors_ShouldReturnFalse_WhenNoErrorExists()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await vm.WaitForInitializationAsync();

            // ASSERT
            Assert.False(vm.HasLogErrors);
        }

        [Fact]
        public async Task HasLogErrors_ShouldReturnTrue_WhenErrorExists()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await vm.WaitForInitializationAsync();

            var logger =
                vm.GetLoggerForTests();

            logger.Error("Erreur test");

            // ASSERT
            Assert.True(vm.HasLogErrors);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — LOG ERROR COUNT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task LogErrorCount_ShouldReturnZero_WhenNoErrorExists()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await vm.WaitForInitializationAsync();

            // ASSERT
            Assert.Equal(0, vm.LogErrorCount);
        }

        [Fact]
        public async Task LogErrorCount_ShouldReturnCorrectCount_WhenErrorsExist()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await vm.WaitForInitializationAsync();

            var logger =
                vm.GetLoggerForTests();

            logger.Error("Erreur 1");
            logger.Error("Erreur 2");
            logger.Error("Erreur 3");

            // ASSERT
            Assert.Equal(3, vm.LogErrorCount);
        }

        // ═════════════════════════════════════════════════════════════
        // TEST — FILTERED LOGS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task FilteredLogs_ShouldReturnOnlyInfoLogs()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await vm.WaitForInitializationAsync();

            var logger =
                vm.GetLoggerForTests();

            logger.Info("Info");
            logger.Warning("Warning");
            logger.Error("Error");

            vm.SelectedLogFilter =
                LogFilter.Info;

            // ACT
            var logs =
                vm.FilteredLogs.ToList();

            // ASSERT
            Assert.NotEmpty(logs);

            Assert.All(
                logs,
                log => Assert.Equal(
                    "Info",
                    log.Level.ToString()));
        }

        [Fact]
        public async Task FilteredLogs_ShouldReturnOnlyWarningLogs()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await vm.WaitForInitializationAsync();

            var logger =
                vm.GetLoggerForTests();

            logger.Info("Info");
            logger.Warning("Warning");
            logger.Error("Error");

            vm.SelectedLogFilter =
                LogFilter.Warning;

            // ACT
            var logs =
                vm.FilteredLogs.ToList();

            // ASSERT
            Assert.NotEmpty(logs);

            Assert.All(
                logs,
                log => Assert.Equal(
                    "Warning",
                    log.Level.ToString()));
        }

        [Fact]
        public async Task FilteredLogs_ShouldReturnOnlyErrorLogs()
        {
            // ARRANGE
            var vm = new MainViewModel();

            await vm.WaitForInitializationAsync();

            var logger =
                vm.GetLoggerForTests();

            logger.Info("Info");
            logger.Warning("Warning");
            logger.Error("Error");

            vm.SelectedLogFilter =
                LogFilter.Error;

            // ACT
            var logs =
                vm.FilteredLogs.ToList();

            // ASSERT
            Assert.NotEmpty(logs);

            Assert.All(
                logs,
                log => Assert.Equal(
                    "Error",
                    log.Level.ToString()));
        }
    }
}