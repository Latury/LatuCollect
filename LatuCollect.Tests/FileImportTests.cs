/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : FileImportTests.cs                                        ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le service FileImportService                                 ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier le chargement d’un dossier                               ║
║  - Vérifier la création des nodes                                    ║
║  - Vérifier les exclusions                                           ║
║  - Vérifier les limites                                              ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI                                                    ║
║  - Tests unitaires uniquement                                        ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration;
using LatuCollect.Core.Services.Import;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace LatuCollect.Tests
{
    public class FileImportTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. TEST — DOSSIER VALIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task LoadTreeAsync_ShouldLoadFiles_WhenFolderExists()
        {
            // ARRANGE
            var config = new AppConfig();
            var service = new FileImportService(config);

            string folder = "test_import";
            Directory.CreateDirectory(folder);

            File.WriteAllText(Path.Combine(folder, "a.txt"), "A");
            File.WriteAllText(Path.Combine(folder, "b.txt"), "B");

            // ACT
            var result = await service.LoadTreeAsync(folder);

            // ASSERT
            Assert.NotNull(result);
            Assert.NotEmpty(result.Nodes);
            Assert.True(result.TotalNodes >= 1);

            // CLEANUP
            Directory.Delete(folder, true);
        }

        // ═════════════════════════════════════════════════════════════
        // 2. TEST — DOSSIER INVALIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task LoadTreeAsync_ShouldReturnEmpty_WhenFolderInvalid()
        {
            // ARRANGE
            var config = new AppConfig();
            var service = new FileImportService(config);

            // ACT
            var result = await service.LoadTreeAsync("dossier_inexistant");

            // ASSERT
            Assert.Empty(result.Nodes);
            Assert.Equal(0, result.TotalNodes);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. TEST — EXCLUSION DOSSIER
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task LoadTreeAsync_ShouldExcludeFolders()
        {
            // ARRANGE
            var config = new AppConfig();
            config.ExcludedFolders.Add("ignore");

            var service = new FileImportService(config);

            string root = "test_import_exclude";
            string excluded = Path.Combine(root, "ignore");

            Directory.CreateDirectory(excluded);
            File.WriteAllText(Path.Combine(excluded, "a.txt"), "A");

            // ACT
            var result = await service.LoadTreeAsync(root);

            // ASSERT
            var rootNode = result.Nodes[0];

            Assert.DoesNotContain(rootNode.Children, n => n.Name == "ignore");

            // CLEANUP
            Directory.Delete(root, true);
        }

        // ═════════════════════════════════════════════════════════════
        // 4. TEST — LIMITE MAX_NODES
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task LoadTreeAsync_ShouldSetPartial_WhenLimitReached()
        {
            // ARRANGE
            var config = new AppConfig();
            var service = new FileImportService(config);

            string folder = "test_import_limit";
            Directory.CreateDirectory(folder);

            // créer beaucoup de fichiers
            for (int i = 0; i < 1200; i++)
            {
                File.WriteAllText(Path.Combine(folder, $"file_{i}.txt"), "X");
            }

            // ACT
            var result = await service.LoadTreeAsync(folder);

            // ASSERT
            Assert.True(result.IsPartial);
            Assert.Contains("affichage partiel", result.Message);

            // CLEANUP
            Directory.Delete(folder, true);
        }
    }
}