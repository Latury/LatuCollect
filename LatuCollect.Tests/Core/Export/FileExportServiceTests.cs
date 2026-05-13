/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : FileExportServiceTests.cs                                 ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le service FileExportService                                 ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier l’assemblage du contenu                                  ║
║  - Vérifier la présence des fichiers dans l’export                   ║
║  - Vérifier les statistiques                                         ║
║  - Vérifier les limites mode IA                                      ║
║  - Vérifier les exports sync / async                                 ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI                                                    ║
║  - Tests unitaires uniquement                                        ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Services.Export;

namespace LatuCollect.Tests.Core.Export
{
    public class FileExportServiceTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. TEST — EXPORT SIMPLE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldIncludeFilesContent()
        {
            // ARRANGE
            var service = new FileExportService();

            string file1 = "test1.txt";
            string file2 = "test2.txt";

            File.WriteAllText(file1, "Content A");
            File.WriteAllText(file2, "Content B");

            var files = new List<string> { file1, file2 };

            // ACT
            var result = service.BuildContentWithStats(files, false);

            // ASSERT
            Assert.Contains("Content A", result.Content);
            Assert.Contains("Content B", result.Content);

            Assert.Equal(2, result.Stats.FileCount);
            Assert.True(result.Stats.TotalCharacters > 0);

            // CLEANUP
            File.Delete(file1);
            File.Delete(file2);
        }

        // ═════════════════════════════════════════════════════════════
        // 2. TEST — FORMAT MARKDOWN
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldFormatMarkdown_WhenEnabled()
        {
            // ARRANGE
            var service = new FileExportService();

            string file = "test_md.txt";

            File.WriteAllText(file, "Markdown Test");

            var files = new List<string> { file };

            // ACT
            var result = service.BuildContentWithStats(files, true);

            // ASSERT
            Assert.Contains("```", result.Content);
            Assert.Contains("Markdown Test", result.Content);

            // CLEANUP
            File.Delete(file);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. TEST — FICHIER INEXISTANT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldHandleMissingFile()
        {
            // ARRANGE
            var service = new FileExportService();

            string missingFile = "not_found.txt";

            var files = new List<string> { missingFile };

            // ACT
            var result = service.BuildContentWithStats(files, false);

            // ASSERT
            Assert.Contains("Erreur", result.Content);
        }

        // ═════════════════════════════════════════════════════════════
        // 4. TEST — STATISTIQUES MULTI FICHIERS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldCalculateCorrectStats()
        {
            // ARRANGE
            var service = new FileExportService();

            string file1 = "stat1.txt";
            string file2 = "stat2.txt";

            File.WriteAllText(file1, "Line1\nLine2");
            File.WriteAllText(file2, "Line3");

            var files = new List<string> { file1, file2 };

            // ACT
            var result = service.BuildContentWithStats(files, false);

            // ASSERT
            Assert.Equal(2, result.Stats.FileCount);
            Assert.True(result.Stats.TotalLines >= 3);

            // CLEANUP
            File.Delete(file1);
            File.Delete(file2);
        }

        // ═════════════════════════════════════════════════════════════
        // 5. TEST — FICHIER VIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldHandleEmptyFile()
        {
            // ARRANGE
            var service = new FileExportService();

            string file = "empty.txt";

            File.WriteAllText(file, "");

            var files = new List<string> { file };

            // ACT
            var result = service.BuildContentWithStats(files, false);

            // ASSERT
            Assert.Equal(1, result.Stats.FileCount);
            Assert.Equal(0, result.Stats.TotalCharacters);

            // CLEANUP
            File.Delete(file);
        }

        // ═════════════════════════════════════════════════════════════
        // 6. TEST — COLLECTION VIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldHandleEmptyFileCollection()
        {
            // ARRANGE
            var service = new FileExportService();

            var files = new List<string>();

            // ACT
            var result = service.BuildContentWithStats(files, false);

            // ASSERT
            Assert.NotNull(result);

            Assert.Equal(string.Empty, result.Content);

            Assert.Equal(0, result.Stats.FileCount);
            Assert.Equal(0, result.Stats.TotalLines);
            Assert.Equal(0, result.Stats.TotalCharacters);
            Assert.Equal(0, result.Stats.TotalSizeBytes);
        }

        // ═════════════════════════════════════════════════════════════
        // 7. TEST — EXPORT ASYNC
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task BuildContentWithStatsAsync_ShouldIncludeFilesContent()
        {
            // ARRANGE
            var service = new FileExportService();

            string file = "async_test.txt";

            File.WriteAllText(file, "Async Content");

            var files = new List<string> { file };

            // ACT
            var result = await service.BuildContentWithStatsAsync(
                files,
                false,
                "normal"
            );

            // ASSERT
            Assert.Contains("Async Content", result.Content);
            Assert.Equal(1, result.Stats.FileCount);

            // CLEANUP
            File.Delete(file);
        }

        // ═════════════════════════════════════════════════════════════
        // 8. TEST — LIMITE MODE IA
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task BuildContentWithStatsAsync_ShouldSetPartial_WhenAiLimitReached()
        {
            // ARRANGE
            var service = new FileExportService();

            var files = new List<string>();

            // 🔥 crée 25 fichiers (> limite 20)
            for (int i = 0; i < 25; i++)
            {
                string file = $"ai_limit_{i}.txt";

                File.WriteAllText(file, $"Content {i}");

                files.Add(file);
            }

            // ACT
            var result = await service.BuildContentWithStatsAsync(
                files,
                false,
                "ai"
            );

            // ASSERT
            Assert.True(result.IsPartial);

            Assert.Contains("Limite atteinte", result.PartialMessage);

            // ⚠ IMPORTANT
            Assert.True(result.Stats.FileCount <= 20);

            // CLEANUP
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 9. TEST — EXPORT FICHIER RÉEL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ExportAsync_ShouldWriteFile()
        {
            // ARRANGE
            var service = new FileExportService();

            string exportPath = "export_test.txt";
            string content = "Export Content";

            // ACT
            var result = await service.ExportAsync(exportPath, content);

            // ASSERT
            Assert.True(result.IsSuccess);

            Assert.True(File.Exists(exportPath));

            string exportedContent = await File.ReadAllTextAsync(exportPath);

            Assert.Equal(content, exportedContent);

            // CLEANUP
            File.Delete(exportPath);
        }

        // ═════════════════════════════════════════════════════════════
        // 10. TEST — CHEMIN INVALIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ExportAsync_ShouldHandleInvalidPath()
        {
            // ARRANGE
            var service = new FileExportService();

            string invalidPath = "<>:/invalid/export.txt";

            // ACT
            var result = await service.ExportAsync(
                invalidPath,
                "Test"
            );

            // ASSERT
            Assert.False(result.IsSuccess);

            Assert.NotEmpty(result.Message);
        }

        // ═════════════════════════════════════════════════════════════
        // 11. TEST — EXPORT CONTENU VIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ExportAsync_ShouldHandleEmptyContent()
        {
            // ARRANGE
            var service = new FileExportService();

            string exportPath = "empty_export.txt";

            // ACT
            var result = await service.ExportAsync(
                exportPath,
                string.Empty
            );

            // ASSERT
            Assert.True(result.IsSuccess);

            Assert.True(File.Exists(exportPath));

            string exportedContent =
                await File.ReadAllTextAsync(exportPath);

            Assert.Equal(string.Empty, exportedContent);

            // CLEANUP
            File.Delete(exportPath);
        }

        // ═════════════════════════════════════════════════════════════
        // 12. TEST — DOSSIER INEXISTANT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public async Task ExportAsync_ShouldHandleMissingDirectory()
        {
            // ARRANGE
            var service = new FileExportService();

            string exportPath =
                @"MissingFolder\SubFolder\export.txt";

            // ACT
            var result = await service.ExportAsync(
                exportPath,
                "Test"
            );

            // ASSERT
            Assert.False(result.IsSuccess);

            Assert.NotEmpty(result.Message);
        }

        [Fact]
        public void BuildContentWithStats_ShouldHandleNullCollection()
        {
            // ARRANGE
            var service = new FileExportService();

            // ACT
            var result = service.BuildContentWithStats(
                null,
                false
            );

            // ASSERT
            Assert.NotNull(result);

            Assert.Equal(string.Empty, result.Content);

            Assert.Equal(0, result.Stats.FileCount);
        }

        [Fact]
        public async Task ExportAsync_ShouldHandleNullContent()
        {
            // ARRANGE
            var service = new FileExportService();

            string exportPath = "null_content.txt";

            // ACT
            var result = await service.ExportAsync(
                exportPath,
                null
            );

            // ASSERT
            Assert.True(result.IsSuccess);

            Assert.True(File.Exists(exportPath));

            string exportedContent =
                await File.ReadAllTextAsync(exportPath);

            Assert.Equal(string.Empty, exportedContent);

            // CLEANUP
            File.Delete(exportPath);
        }

        [Fact]
        public async Task ExportAsync_ShouldHandleNullPath()
        {
            // ARRANGE
            var service = new FileExportService();

            // ACT
            var result = await service.ExportAsync(
                null,
                "Test"
            );

            // ASSERT
            Assert.False(result.IsSuccess);

            Assert.Equal(
                "Chemin invalide",
                result.Message
            );
        }
    }
}
