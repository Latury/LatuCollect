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
using LatuCollect.Core.Services.Statistics;
using Xunit;

namespace LatuCollect.Tests
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

            string content = "Hello\nWorld"; // 2 lignes
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
            Assert.Equal(3, stats.TotalLines); // 2 + 1
            Assert.Equal(4, stats.TotalCharacters); // "A\nB" (3) + "C" (1) = 4 ⚠

            // ⚠ correction importante :
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
    }
}