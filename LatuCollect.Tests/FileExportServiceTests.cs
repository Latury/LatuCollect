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
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Aucun accès UI                                                    ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Services.Export;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace LatuCollect.Tests
{
    public class FileExportServiceTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. TEST — EXPORT SIMPLE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldIncludeFilesContent()
        {
            // 1. ARRANGE
            var service = new FileExportService();

            string file1 = "test1.txt";
            string file2 = "test2.txt";

            File.WriteAllText(file1, "Content A");
            File.WriteAllText(file2, "Content B");

            var files = new List<string> { file1, file2 };

            // 2. ACT
            var result = service.BuildContentWithStats(files, false);

            // 3. ASSERT
            Assert.Contains("Content A", result.Content);
            Assert.Contains("Content B", result.Content);

            Assert.Equal(2, result.Stats.FileCount);
            Assert.True(result.Stats.TotalCharacters > 0);

            // 4. CLEANUP
            File.Delete(file1);
            File.Delete(file2);
        }

        // ═════════════════════════════════════════════════════════════
        // 2. TEST — FORMAT MARKDOWN
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldFormatMarkdown_WhenEnabled()
        {
            // 1. ARRANGE
            var service = new FileExportService();

            string file = "test_md.txt";
            File.WriteAllText(file, "Markdown Test");

            var files = new List<string> { file };

            // 2. ACT
            var result = service.BuildContentWithStats(files, true);

            // 3. ASSERT
            Assert.Contains("```", result.Content);
            Assert.Contains("Markdown Test", result.Content);

            // 4. CLEANUP
            File.Delete(file);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. TEST — FICHIER INEXISTANT DANS EXPORT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldHandleMissingFile()
        {
            // 1. ARRANGE
            var service = new FileExportService();

            string missingFile = "not_found.txt";

            var files = new List<string> { missingFile };

            // 2. ACT
            var result = service.BuildContentWithStats(files, false);

            // 3. ASSERT
            Assert.Contains("Erreur", result.Content);
        }

        // ═════════════════════════════════════════════════════════════
        // 4. TEST — STATISTIQUES MULTI FICHIERS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void BuildContentWithStats_ShouldCalculateCorrectStats()
        {
            // 1. ARRANGE
            var service = new FileExportService();

            string file1 = "stat1.txt";
            string file2 = "stat2.txt";

            File.WriteAllText(file1, "Line1\nLine2");
            File.WriteAllText(file2, "Line3");

            var files = new List<string> { file1, file2 };

            // 2. ACT
            var result = service.BuildContentWithStats(files, false);

            // 3. ASSERT
            Assert.Equal(2, result.Stats.FileCount);
            Assert.True(result.Stats.TotalLines >= 3);

            // 4. CLEANUP
            File.Delete(file1);
            File.Delete(file2);
        }
    }
}