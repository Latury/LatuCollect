/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : FileReaderTests.cs                                        ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le service FileReaderService                                 ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier la lecture d’un fichier existant                         ║
║  - Vérifier le contenu retourné                                      ║
║  - Vérifier la taille du fichier                                     ║
║  - Vérifier la gestion des erreurs                                   ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI                                                    ║
║  - Tests unitaires uniquement                                        ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Services.Reader;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace LatuCollect.Tests
{
    public class FileReaderTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. TEST — LECTURE FICHIER EXISTANT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldReturnSuccess_WhenFileExists()
        {
            // 1. ARRANGE
            string path = "test_file.txt";
            string expectedContent = "Hello LatuCollect";

            File.WriteAllText(path, expectedContent);

            // 2. ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // 3. ASSERT
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedContent, result.Content);
            Assert.True(result.FileSize > 0);

            // 4. CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 2. TEST — FICHIER INEXISTANT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldFail_WhenFileDoesNotExist()
        {
            // 1. ARRANGE
            string path = "file_not_exists.txt";

            // 2. ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // 3. ASSERT
            Assert.False(result.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        // ═════════════════════════════════════════════════════════════
        // 3. TEST — CACHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldUseCache_WhenFileAlreadyRead()
        {
            // 1. ARRANGE
            string path = "test_cache.txt";
            string initialContent = "Version 1";
            string modifiedContent = "Version 2";

            File.WriteAllText(path, initialContent);

            // 2. ACT — première lecture (remplit le cache)
            var firstRead = await FileReaderService.ReadFileAsync(path);

            // 🔥 modification du fichier sur disque
            File.WriteAllText(path, modifiedContent);

            // 2. ACT — deuxième lecture (doit utiliser le cache)
            var secondRead = await FileReaderService.ReadFileAsync(path);

            // 3. ASSERT
            Assert.True(firstRead.IsSuccess);
            Assert.True(secondRead.IsSuccess);

            // ⚠ IMPORTANT : doit rester "Version 1"
            Assert.Equal(initialContent, secondRead.Content);

            // 4. CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileReaderService.ClearCache();
        }
    }
}