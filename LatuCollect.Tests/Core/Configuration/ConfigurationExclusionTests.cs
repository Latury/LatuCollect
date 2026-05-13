/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                  ║
║  Module : Tests                                                     ║
║  Fichier : ConfigurationExclusionTests.cs                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester la persistance des exclusions utilisateur                    ║
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
    public class ConfigurationExclusionTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — PERSISTANCE EXCLUSION FICHIER
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task SaveAsync_ShouldPreserve_FileExclusion()
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
                @"C:\Projet\Test\fichier.txt",
                false,
                false)
        }
            };

            // ACT
            await service.SaveAsync(config);

            var loaded = await service.LoadAsync();

            // ASSERT
            Assert.NotNull(loaded);

            var exclusion = loaded.ExcludedFolders.FirstOrDefault();

            Assert.NotNull(exclusion);

            Assert.Equal(
                @"C:\Projet\Test\fichier.txt",
                exclusion!.Name);

            Assert.False(exclusion.IsProtected);

            Assert.False(exclusion.IsDirectory);
        }

        [Fact]
        public async Task SaveAsync_ShouldPreserve_FileExtension()
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
                true,
                true),

            new ExclusionItem(
                "bin",
                true,
                true),

            new ExclusionItem(
                "obj",
                true,
                true)
        }
            };

            // ACT
            await service.SaveAsync(config);

            var loaded = await service.LoadAsync();

            // ASSERT
            Assert.NotNull(loaded);

            Assert.Contains(
                loaded.ExcludedFolders,
                e => e.Name == ".git");

            Assert.Contains(
                loaded.ExcludedFolders,
                e => e.Name == "bin");

            Assert.Contains(
                loaded.ExcludedFolders,
                e => e.Name == "obj");
        }
    }
}