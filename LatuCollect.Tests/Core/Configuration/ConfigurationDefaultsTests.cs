/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : ConfigurationDefaultsTests.cs                             ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester la normalisation des exclusions système                      ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI                                                    ║
║  - Tests configuration uniquement                                    ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration.Models;
using LatuCollect.Core.Configuration.Services;

namespace LatuCollect.Tests.Core.Configuration
{
    public class ConfigurationDefaultsTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — EXCLUSIONS SYSTÈME AJOUTÉES AUTOMATIQUEMENT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task LoadAsync_ShouldAddProtectedSystemExclusions()
        {
            // ARRANGE
            var tempPath = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid() + ".json");

            var service = new ConfigurationService(tempPath);

            var config = new UserConfig
            {
                ExcludedFolders = new List<ExclusionItem>()
            };

            await service.SaveAsync(config);

            // ACT
            var loaded = await service.LoadAsync();

            // ASSERT
            Assert.NotNull(loaded);

            Assert.Contains(
                loaded.ExcludedFolders,
                e =>
                    e.Name == ".git"
                    && e.IsProtected
                    && e.IsDirectory);

            Assert.Contains(
                loaded.ExcludedFolders,
                e =>
                    e.Name == "bin"
                    && e.IsProtected
                    && e.IsDirectory);

            Assert.Contains(
                loaded.ExcludedFolders,
                e =>
                    e.Name == "obj"
                    && e.IsProtected
                    && e.IsDirectory);
        }


        // ═════════════════════════════════════════════════════════════
        // TEST — CONSERVATION EXCLUSION EXISTANTE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task LoadAsync_ShouldPreserveExistingSystemExclusion()
        {
            // ARRANGE
            var tempPath = Path.Combine(
                Path.GetTempPath(),
                Guid.NewGuid() + ".json");

            var service = new ConfigurationService(tempPath);

            var config = new UserConfig
            {
                ExcludedFolders = new List<ExclusionItem>
        {
            new ExclusionItem(
                ".git",
                false,
                false)
        }
            };

            await service.SaveAsync(config);

            // ACT
            var loaded = await service.LoadAsync();

            // ASSERT
            Assert.NotNull(loaded);

            var git = loaded.ExcludedFolders
                .FirstOrDefault(e => e.Name == ".git");

            Assert.NotNull(git);

            // ✔ Les valeurs utilisateur doivent être conservées
            Assert.False(git!.IsProtected);

            Assert.False(git.IsDirectory);
        }
    }
}
