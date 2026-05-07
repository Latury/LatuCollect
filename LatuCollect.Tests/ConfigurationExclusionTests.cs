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
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LatuCollect.Tests
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
            var service = new ConfigurationService();

            var config = new UserConfig();

            config.ExcludedFolders.Add(
                new ExclusionItem(
                    @"C:\Projet\Test\fichier.txt",
                    false
                )
            );

            // ACT
            await service.SaveAsync(config);

            var loaded = await service.LoadAsync();

            // ASSERT
            var exclusion = loaded.ExcludedFolders
                .FirstOrDefault(e =>
                    e.Name == @"C:\Projet\Test\fichier.txt");

            Assert.NotNull(exclusion);

            Assert.False(exclusion!.IsProtected);
        }

        [Fact]
        public async Task SaveAsync_ShouldPreserve_FileExtension()
        {
            // ARRANGE
            var service = new ConfigurationService();

            var config = new UserConfig();

            config.ExcludedFolders.Add(
                new ExclusionItem(
                    @"C:\Projet\Test\document.md",
                    false
                )
            );

            // ACT
            await service.SaveAsync(config);

            var loaded = await service.LoadAsync();

            Assert.Contains(
    loaded.ExcludedFolders,
    e => e.Name.Contains("document.md")
);

            foreach (var exclusion in loaded.ExcludedFolders)
            {
                System.Diagnostics.Debug.WriteLine(exclusion.Name);
            }
        }
    }
}