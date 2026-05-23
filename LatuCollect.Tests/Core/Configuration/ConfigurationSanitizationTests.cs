/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.Core.Configuration                                   ║
║  Fichier : ConfigurationSanitizationTests.cs                         ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le nettoyage automatique de UserConfig                       ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès fichier                                               ║
║  - Validation données uniquement                                     ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration.Models;

namespace LatuCollect.Tests.Core.Configuration
{
    public class ConfigurationSanitizationTests
    {
        // ═════════════════════════════════════════════════════════════
        // TEST — NETTOYAGE EXPANDED PATHS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void ExpandedPaths_ShouldSanitize_InvalidValues()
        {
            // ARRANGE
            var config = new UserConfig();

            // ACT
            config.ExpandedPaths = new()
            {
                "",
                "   ",
                @"C:\Projet\Test",
                @"C:\Projet\Test",
                @"  C:\Projet\Folder  "
            };

            // ASSERT
            Assert.Equal(2, config.ExpandedPaths.Count);

            Assert.Contains(
                @"C:\Projet\Test",
                config.ExpandedPaths);

            Assert.Contains(
                @"C:\Projet\Folder",
                config.ExpandedPaths);
        }
    }
}