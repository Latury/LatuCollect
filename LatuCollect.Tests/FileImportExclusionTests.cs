/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                  ║
║  Module : Tests                                                     ║
║  Fichier : FileImportExclusionTests.cs                              ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester les exclusions du FileImportService                         ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Validation pipeline réel                                          ║
║  - Vérifie exclusion fichiers                                        ║
║  - Vérifie exclusion dossiers                                        ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration;
using LatuCollect.Core.Configuration.Models;
using LatuCollect.Core.Services.Import;

namespace LatuCollect.Tests
{
    public class FileImportExclusionTests
    {
        [Fact]
        public async Task LoadTreeAsync_ShouldExclude_File_ByFullPath()
        {
            // ARRANGE
            string root = "test_exclusion_file";

            Directory.CreateDirectory(root);

            string includedFile =
                Path.Combine(root, "keep.txt");

            string excludedFile =
                Path.Combine(root, "hidden.md");

            File.WriteAllText(includedFile, "KEEP");
            File.WriteAllText(excludedFile, "HIDDEN");

            var config = new AppConfig();

            config.ExcludedFolders.Add(
                new ExclusionItem(excludedFile)
            );

            var service = new FileImportService(config);

            // ACT
            var result = await service.LoadTreeAsync(root);

            var rootNode = result.Nodes.First();

            // ASSERT
            Assert.DoesNotContain(
                rootNode.Children,
                n => n.Path == excludedFile
            );

            Assert.Contains(
                rootNode.Children,
                n => n.Path == includedFile
            );

            // CLEANUP
            Directory.Delete(root, true);
        }

        [Fact]
        public async Task LoadTreeAsync_ShouldExclude_Directory_ByFullPath()
        {
            // ARRANGE
            string root = "test_exclusion_directory";

            Directory.CreateDirectory(root);

            string includedFolder =
                Path.Combine(root, "keep");

            string excludedFolder =
                Path.Combine(root, "hidden");

            Directory.CreateDirectory(includedFolder);
            Directory.CreateDirectory(excludedFolder);

            File.WriteAllText(
                Path.Combine(includedFolder, "a.txt"),
                "KEEP"
            );

            File.WriteAllText(
                Path.Combine(excludedFolder, "b.txt"),
                "HIDDEN"
            );

            var config = new AppConfig();

            config.ExcludedFolders.Add(
                new ExclusionItem(excludedFolder)
            );

            var service = new FileImportService(config);

            // ACT
            var result = await service.LoadTreeAsync(root);

            var rootNode = result.Nodes.First();

            // ASSERT
            Assert.DoesNotContain(
                rootNode.Children,
                n => n.Path == excludedFolder
            );

            Assert.Contains(
                rootNode.Children,
                n => n.Path == includedFolder
            );

            // CLEANUP
            Directory.Delete(root, true);
        }

        [Fact]
        public async Task LoadTreeAsync_ShouldRemove_AllChildren_WhenParentDirectoryExcluded()
        {
            // ARRANGE
            string root = "test_exclusion_parent";

            string parentFolder =
                Path.Combine(root, "Parent");

            string childFolder =
                Path.Combine(parentFolder, "Child");

            Directory.CreateDirectory(childFolder);

            File.WriteAllText(
                Path.Combine(parentFolder, "a.txt"),
                "A"
            );

            File.WriteAllText(
                Path.Combine(childFolder, "b.txt"),
                "B"
            );

            var config = new AppConfig();

            config.ExcludedFolders.Add(
                new ExclusionItem(
                    parentFolder,
                    false,
                    true
                )
            );

            var service = new FileImportService(config);

            // ACT
            var result = await service.LoadTreeAsync(root);

            var rootNode = result.Nodes.First();

            // ASSERT
            Assert.DoesNotContain(
                rootNode.Children,
                n => n.Path == parentFolder
            );

            // CLEANUP
            Directory.Delete(root, true);
        }
    }
}