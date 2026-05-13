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
║  - Vérifier le cache                                                 ║
║  - Vérifier les cas limites                                          ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI                                                    ║
║  - Tests unitaires uniquement                                        ║
║  - Tests stables uniquement                                          ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Services.Reader;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LatuCollect.Tests.Core.Reader
{
    public class FileReaderTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. TEST — LECTURE FICHIER EXISTANT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldReturnSuccess_WhenFileExists()
        {
            // ARRANGE
            string path = "test_file.txt";
            string expectedContent = "Hello LatuCollect";

            File.WriteAllText(path, expectedContent);

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(result.IsSuccess);
            Assert.Equal(expectedContent, result.Content);
            Assert.True(result.FileSize > 0);

            // CLEANUP
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
            // ARRANGE
            string path = "file_not_exists.txt";

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.False(result.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(result.ErrorMessage));
        }

        // ═════════════════════════════════════════════════════════════
        // 3. TEST — CACHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldUseCache_WhenFileAlreadyRead()
        {
            // ARRANGE
            string path = "test_cache.txt";
            string initialContent = "Version 1";
            string modifiedContent = "Version 2";

            File.WriteAllText(path, initialContent);

            // ACT — première lecture
            var firstRead = await FileReaderService.ReadFileAsync(path);

            // modification fichier
            File.WriteAllText(path, modifiedContent);

            // ACT — deuxième lecture
            var secondRead = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(firstRead.IsSuccess);
            Assert.True(secondRead.IsSuccess);

            // doit utiliser le cache
            Assert.Equal(initialContent, secondRead.Content);

            // CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileReaderService.ClearCache();
        }

        // ═════════════════════════════════════════════════════════════
        // 4. TEST — CHEMIN NULL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldFail_WhenPathIsNull()
        {
            // ACT
            var result = await FileReaderService.ReadFileAsync(null);

            // ASSERT
            Assert.False(result.IsSuccess);
            Assert.Equal("Chemin invalide", result.ErrorMessage);
        }

        // ═════════════════════════════════════════════════════════════
        // 5. TEST — CHEMIN VIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldFail_WhenPathIsEmpty()
        {
            // ACT
            var result = await FileReaderService.ReadFileAsync("");

            // ASSERT
            Assert.False(result.IsSuccess);
            Assert.Equal("Chemin invalide", result.ErrorMessage);
        }

        // ═════════════════════════════════════════════════════════════
        // 6. TEST — FICHIER VIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldHandleEmptyFile()
        {
            // ARRANGE
            string path = "empty_file.txt";

            File.WriteAllText(path, string.Empty);

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(result.IsSuccess);
            Assert.Equal(string.Empty, result.Content);
            Assert.Equal(0, result.FileSize);

            // CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 7. TEST — GROS FICHIER
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldTruncateLargeFile()
        {
            // ARRANGE
            string path = "large_file.txt";

            // 🔥 génération gros contenu (> 2 MB)
            string largeContent = new string('A', 3 * 1024 * 1024);

            await File.WriteAllTextAsync(path, largeContent, Encoding.UTF8);

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(result.IsSuccess);

            Assert.Contains(
                "⚠ Fichier tronqué",
                result.Content);

            Assert.True(result.FileSize > 2 * 1024 * 1024);

            // CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 8. TEST — CLEAR CACHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ClearCache_ShouldForceNewRead()
        {
            // ARRANGE
            string path = "clear_cache_test.txt";

            File.WriteAllText(path, "Version 1");

            // première lecture
            await FileReaderService.ReadFileAsync(path);

            // vider cache
            FileReaderService.ClearCache();

            // modifier fichier
            File.WriteAllText(path, "Version 2");

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(result.IsSuccess);
            Assert.Equal("Version 2", result.Content);

            // CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileReaderService.ClearCache();
        }

        // ═════════════════════════════════════════════════════════════
        // 9. TEST — FICHIER SUPPRIMÉ PENDANT LA LECTURE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldFail_WhenFileDeletedDuringRead()
        {
            // ARRANGE
            string path = "deleted_test.txt";

            File.WriteAllText(path, "Hello");

            // suppression immédiate
            File.Delete(path);

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.False(result.IsSuccess);

            Assert.Equal(
                "Fichier introuvable",
                result.ErrorMessage
            );
        }

        // ═════════════════════════════════════════════════════════════
        // 10. TEST — REMOVE FROM CACHE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task RemoveFromCache_ShouldForceNewRead()
        {
            // ARRANGE
            string path = "remove_cache_test.txt";

            File.WriteAllText(path, "Version 1");

            // première lecture
            await FileReaderService.ReadFileAsync(path);

            // suppression cache
            FileReaderService.RemoveFromCache(path);

            // modification fichier
            File.WriteAllText(path, "Version 2");

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(result.IsSuccess);

            Assert.Equal(
                "Version 2",
                result.Content);

            // CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileReaderService.ClearCache();
        }

        // ═════════════════════════════════════════════════════════════
        // 11. TEST — FICHIER VERROUILLÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldFail_WhenFileIsLocked()
        {
            // ARRANGE
            string path = "locked_file.txt";

            await File.WriteAllTextAsync(path, "Locked");

            using var stream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.ReadWrite,
                FileShare.None);

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.False(result.IsSuccess);

            Assert.Equal(
                "Erreur de lecture",
                result.ErrorMessage);

            // CLEANUP
            stream.Close();

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 12. TEST — CONTENU UNICODE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldHandleUnicodeContent()
        {
            // ARRANGE
            string path = "unicode_test.txt";

            string expected =
                "Bonjour 👋\nこんにちは\néàç";

            await File.WriteAllTextAsync(
                path,
                expected,
                Encoding.UTF8);

            // ACT
            var result = await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(result.IsSuccess);

            Assert.Equal(
                expected,
                result.Content);

            // CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 13. TEST — GROS CONTENU ASYNC
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldHandleLargeAsyncContent()
        {
            // ARRANGE
            string path = "large_async.txt";

            string content =
                new string('X', 500_000);

            await File.WriteAllTextAsync(
                path,
                content,
                Encoding.UTF8);

            // ACT
            var result =
                await FileReaderService.ReadFileAsync(path);

            // ASSERT
            Assert.True(result.IsSuccess);

            Assert.StartsWith(
                "X",
                result.Content);

            Assert.True(
                result.Content.Length > 100_000);

            // CLEANUP
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 14. TEST — CHEMIN INVALIDE AVANCÉ
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ReadFileAsync_ShouldFail_WhenPathContainsInvalidCharacters()
        {
            // ARRANGE
            string invalidPath = "\0invalid.txt";

            // ACT
            var result =
                await FileReaderService.ReadFileAsync(invalidPath);

            // ASSERT
            Assert.False(result.IsSuccess);

            Assert.False(
                string.IsNullOrWhiteSpace(
                    result.ErrorMessage));
        }
    }
}