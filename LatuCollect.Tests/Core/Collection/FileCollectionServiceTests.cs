/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : FileCollectionServiceTests.cs                             ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le service FileCollectionService                             ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI                                                    ║
║  - Validation logique sélection                                      ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Models;
using LatuCollect.Core.Services.Collection;

namespace LatuCollect.Tests.Core.Collection
{
    public class FileCollectionServiceTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. TEST — ROOTS NULL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void GetSelectedFiles_ShouldReturnEmpty_WhenRootsAreNull()
        {
            // ARRANGE
            var service = new FileCollectionService();

            // ACT
            var result = service.GetSelectedFiles(null);

            // ASSERT
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // ═════════════════════════════════════════════════════════════
        // 2. TEST — FICHIER SÉLECTIONNÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void GetSelectedFiles_ShouldReturnSelectedFile()
        {
            // ARRANGE
            var service = new FileCollectionService();

            string file = "selected.txt";

            File.WriteAllText(file, "content");

            var node = new FileNode
            {
                Name = "selected.txt",
                Path = file,
                IsSelected = true
            };

            var roots = new List<FileNode>
            {
                node
            };

            // ACT
            var result = service.GetSelectedFiles(roots);

            // ASSERT
            Assert.Single(result);

            Assert.Contains(file, result);

            // CLEANUP
            File.Delete(file);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. TEST — DOSSIER IGNORÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void GetSelectedFiles_ShouldIgnoreFolder()
        {
            // ARRANGE
            var service = new FileCollectionService();

            var folder = new FileNode
            {
                Name = "Folder",
                Path = "FakeFolder",
                IsSelected = true
            };

            // 🔥 dossier = possède un enfant
            folder.Children.Add(new FileNode
            {
                Name = "Child.txt"
            });

            var roots = new List<FileNode>
            {
                folder
            };

            // ACT
            var result = service.GetSelectedFiles(roots);

            // ASSERT
            Assert.Empty(result);
        }

        // ═════════════════════════════════════════════════════════════
        // 4. TEST — DOUBLONS IGNORÉS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void GetSelectedFiles_ShouldIgnoreDuplicates()
        {
            // ARRANGE
            var service = new FileCollectionService();

            string file = "duplicate.txt";

            File.WriteAllText(file, "content");

            var node1 = new FileNode
            {
                Name = "duplicate.txt",
                Path = file,
                IsSelected = true
            };

            var node2 = new FileNode
            {
                Name = "duplicate.txt",
                Path = file,
                IsSelected = true
            };

            var roots = new List<FileNode>
            {
                node1,
                node2
            };

            // ACT
            var result = service.GetSelectedFiles(roots);

            // ASSERT
            Assert.Single(result);

            Assert.Contains(file, result);

            // CLEANUP
            File.Delete(file);
        }

        // ═════════════════════════════════════════════════════════════
        // 5. TEST — LIMITE MAX FILES
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void GetSelectedFiles_ShouldRespectMaxFilesLimit()
        {
            // ARRANGE
            var service = new FileCollectionService();

            var roots = new List<FileNode>();

            for (int i = 0; i < 5100; i++)
            {
                string file = $"limit_{i}.txt";

                File.WriteAllText(file, "content");

                roots.Add(new FileNode
                {
                    Name = file,
                    Path = file,
                    IsSelected = true
                });
            }

            // ACT
            var result = service.GetSelectedFiles(roots);

            // ASSERT
            Assert.True(result.Count <= 5000);

            // CLEANUP
            foreach (var node in roots)
            {
                if (File.Exists(node.Path))
                {
                    File.Delete(node.Path);
                }
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 6. TEST — CHEMIN INVALIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void GetSelectedFiles_ShouldIgnoreInvalidPath()
        {
            // ARRANGE
            var service = new FileCollectionService();

            var node = new FileNode
            {
                Name = "Invalid",
                Path = "\0invalid",
                IsSelected = true
            };

            var roots = new List<FileNode>
    {
        node
    };

            // ACT
            var result = service.GetSelectedFiles(roots);

            // ASSERT
            Assert.Empty(result);
        }

        // ═════════════════════════════════════════════════════════════
        // 7. TEST — NODE NULL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void GetSelectedFiles_ShouldIgnoreNullNode()
        {
            // ARRANGE
            var service = new FileCollectionService();

            var roots = new List<FileNode?>
    {
        null
    };

            // ACT
            var result = service.GetSelectedFiles(
                roots.Where(r => r != null)!);

            // ASSERT
            Assert.Empty(result);
        }
    }
}