/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests                                                      ║
║  Fichier : FileStatisticsTests.cs                                    ║
║                                                                      ║
║  Rôle :                                                              ║
║  Tester le service FileStatisticsService                             ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier le calcul des statistiques                               ║
║  - Vérifier le comptage des lignes                                   ║
║  - Vérifier les caractères                                           ║
║  - Vérifier la taille totale                                         ║
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

using LatuCollect.Core.Models;
using LatuCollect.Core.Services.Statistics;
using Xunit;

namespace LatuCollect.Tests.Core.Statistics
{
    public class FileStatisticsTests
    {
        // ═════════════════════════════════════════════════════════════
        // 1. TEST — STATISTIQUES SIMPLES
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldUpdateAllFields()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            string content = "Hello\nWorld";
            long size = 100;

            // ACT
            FileStatisticsService.UpdateStatistics(stats, content, size);

            // ASSERT
            Assert.Equal(1, stats.FileCount);
            Assert.Equal(2, stats.TotalLines);
            Assert.Equal(content.Length, stats.TotalCharacters);
            Assert.Equal(size, stats.TotalSizeBytes);
        }

        // ═════════════════════════════════════════════════════════════
        // 2. TEST — MULTIPLE FICHIERS
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldAccumulateValues()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            // ACT
            FileStatisticsService.UpdateStatistics(stats, "A\nB", 50);
            FileStatisticsService.UpdateStatistics(stats, "C", 25);

            // ASSERT
            Assert.Equal(2, stats.FileCount);
            Assert.Equal(3, stats.TotalLines);
            Assert.Equal(4, stats.TotalCharacters);
            Assert.Equal(75, stats.TotalSizeBytes);
        }

        // ═════════════════════════════════════════════════════════════
        // 3. TEST — CONTENU NULL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldHandleNullContent()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            // ACT
            FileStatisticsService.UpdateStatistics(stats, null, 0);

            // ASSERT
            Assert.Equal(1, stats.FileCount);
            Assert.Equal(0, stats.TotalLines);
            Assert.Equal(0, stats.TotalCharacters);
            Assert.Equal(0, stats.TotalSizeBytes);
        }

        // ═════════════════════════════════════════════════════════════
        // 4. TEST — CONTENU VIDE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldHandleEmptyContent()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            // ACT
            FileStatisticsService.UpdateStatistics(stats, "", 10);

            // ASSERT
            Assert.Equal(1, stats.FileCount);
            Assert.Equal(0, stats.TotalLines);
            Assert.Equal(0, stats.TotalCharacters);
            Assert.Equal(10, stats.TotalSizeBytes);
        }

        // ═════════════════════════════════════════════════════════════
        // 5. TEST — MULTI LIGNES
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldCountMultipleLines()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            string content = "Line1\nLine2\nLine3";

            // ACT
            FileStatisticsService.UpdateStatistics(stats, content, 30);

            // ASSERT
            Assert.Equal(3, stats.TotalLines);
        }

        // ═════════════════════════════════════════════════════════════
        // 6. TEST — SAUTS DE LIGNE UNIQUEMENT
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldHandleOnlyNewLines()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            string content = "\n\n\n";

            // ACT
            FileStatisticsService.UpdateStatistics(stats, content, 3);

            // ASSERT
            Assert.Equal(4, stats.TotalLines);
        }

        // ═════════════════════════════════════════════════════════════
        // 7. TEST — TAILLE NÉGATIVE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldIgnoreNegativeFileSize()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            // ACT
            FileStatisticsService.UpdateStatistics(
                stats,
                "Test",
                -1
            );

            // ASSERT
            Assert.Equal(0, stats.TotalSizeBytes);
        }

        // ═════════════════════════════════════════════════════════════
        // 8. TEST — CONTENU UNICODE
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldHandleUnicodeContent()
        {
            // ARRANGE
            var stats = new StatisticsResult();

            string content = "Bonjour 👋\nこんにちは\néàç";

            // ACT
            FileStatisticsService.UpdateStatistics(
                stats,
                content,
                100
            );

            // ASSERT
            Assert.Equal(1, stats.FileCount);

            Assert.Equal(content.Length, stats.TotalCharacters);

            Assert.Equal(3, stats.TotalLines);

            Assert.Equal(100, stats.TotalSizeBytes);
        }

        // ═════════════════════════════════════════════════════════════
        // 9. TEST — STATS NULL
        // ═════════════════════════════════════════════════════════════

        [Fact]
        public void UpdateStatistics_ShouldIgnoreNullStats()
        {
            // ACT
            FileStatisticsService.UpdateStatistics(
                null,
                "Hello",
                10
            );

            // ASSERT
            // ✔ aucun crash attendu
        }
    }
}