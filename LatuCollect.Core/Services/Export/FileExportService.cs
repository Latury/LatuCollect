/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Services.Export                                       ║
║  Fichier : FileExportService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Construire et exporter le contenu des fichiers                      ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Construire le contenu final                                       ║
║  - Calculer les statistiques                                         ║
║  - Écrire le fichier exporté (sync + async)                          ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Services.Reader;
using LatuCollect.Core.Services.Statistics;
using LatuCollect.Core.Simulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LatuCollect.Core.Services.Export
{
    // ═════════════════════════════════════════════════════════════
    // 1. MODÈLES
    // ═════════════════════════════════════════════════════════════

    public class ExportResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    public class StatisticsResult
    {
        public int FileCount { get; set; }
        public int TotalLines { get; set; }
        public int TotalCharacters { get; set; }
        public long TotalSizeBytes { get; set; }
    }

    public class ExportData
    {
        public string Content { get; set; } = "";
        public StatisticsResult Stats { get; set; } = new();

        // Indique si le contenu est partiel (limite atteinte)
        public bool IsPartial { get; set; }
        public string PartialMessage { get; set; } = "";
    }

    // ═════════════════════════════════════════════════════════════
    // 2. SERVICE EXPORT
    // ═════════════════════════════════════════════════════════════

    public class FileExportService
    {
        // ═════════════════════════════════════════════════════════════
        // 2.1 EXPORT SYNC
        // ═════════════════════════════════════════════════════════════

        public ExportResult Export(string path, string content)
        {
            if (string.IsNullOrWhiteSpace(path))
                return Fail("Chemin invalide");

            try
            {
                SimulationService.SimulateExport();
                File.WriteAllText(path, content, Encoding.UTF8);

                return Success("Export réussi");
            }
            catch (UnauthorizedAccessException)
            {
                return Fail("Accès refusé");
            }
            catch (IOException)
            {
                return Fail("Fichier utilisé ou inaccessible");
            }
            catch (ArgumentException)
            {
                return Fail("Chemin invalide");
            }
            catch (Exception ex)
            {
                return Fail($"Erreur : {ex.Message}");
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 2.2 EXPORT ASYNC
        // ═════════════════════════════════════════════════════════════

        public async Task<ExportResult> ExportAsync(string path, string content)
        {
            if (string.IsNullOrWhiteSpace(path))
                return Fail("Chemin invalide");

            try
            {
                SimulationService.SimulateExport();
                await File.WriteAllTextAsync(path, content, Encoding.UTF8);

                return Success("Export réussi");
            }
            catch (UnauthorizedAccessException)
            {
                return Fail("Accès refusé");
            }
            catch (IOException)
            {
                return Fail("Fichier utilisé ou inaccessible");
            }
            catch (ArgumentException)
            {
                return Fail("Chemin invalide");
            }
            catch (Exception ex)
            {
                return Fail($"Erreur : {ex.Message}");
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 2.3 BUILD CONTENU + STATS (SYNC)
        // ═════════════════════════════════════════════════════════════

        public ExportData BuildContentWithStats(IEnumerable<string> filePaths, bool isMarkdown)
        {
            var builder = new StringBuilder();
            var stats = new StatisticsResult();

            foreach (var path in filePaths)
            {
                if (!File.Exists(path))
                {
                    AppendFormatted(builder, path, "[Erreur : fichier introuvable]", isMarkdown);
                    continue;
                }

                var result = FileReaderService.ReadFileAsync(path).GetAwaiter().GetResult();

                if (!result.IsSuccess)
                {
                    AppendFormatted(builder, path, $"[Erreur : {result.ErrorMessage}]", isMarkdown);
                    continue;
                }

                FileStatisticsService.UpdateStatistics(stats, result.Content, result.FileSize);
                AppendFormatted(builder, path, result.Content, isMarkdown);
            }

            return new ExportData
            {
                Content = builder.ToString(),
                Stats = stats
            };
        }


        // ═════════════════════════════════════════════════════════════
        // 2.4 BUILD CONTENU + STATS (ASYNC)
        // ═════════════════════════════════════════════════════════════

        public async Task<ExportData> BuildContentWithStatsAsync(
    IEnumerable<string> filePaths,
    bool isMarkdown,
    string exportMode)
        {
            var builder = new StringBuilder();
            var stats = new StatisticsResult();

            bool isPartial = false;
            string partialMessage = "";

            int fileCount = 0;

            const int MAX_FILES_AI = 20;
            const int MAX_CHARACTERS_AI = 20000;
            const int MAX_CHARACTERS_NORMAL = 1_000_000;

            foreach (var path in filePaths)
            {
                // 🔥 LIMITE FICHIERS (MODE IA)
                if (exportMode?.ToLower() == "ai" && fileCount >= MAX_FILES_AI)
                {
                    isPartial = true;
                    partialMessage = $"⚠ Limite atteinte : {MAX_FILES_AI} fichiers maximum (mode IA)";
                    partialMessage += "\nLe contenu a été tronqué.";

                    builder.AppendLine();
                    builder.AppendLine("----------------------------------------");
                    builder.AppendLine(partialMessage);

                    break;
                }

                // ❌ Fichier introuvable
                if (!File.Exists(path))
                {
                    AppendFormatted(builder, path, "[Erreur : fichier introuvable]", isMarkdown);
                    continue;
                }

                var result = await FileReaderService.ReadFileAsync(path);

                // ❌ Erreur lecture
                if (!result.IsSuccess)
                {
                    AppendFormatted(builder, path, $"[Erreur : {result.ErrorMessage}]", isMarkdown);
                    continue;
                }

                FileStatisticsService.UpdateStatistics(stats, result.Content, result.FileSize);
                AppendFormatted(builder, path, result.Content, isMarkdown);

                fileCount++;

                // 🔥 LIMITE CARACTÈRES (MODE IA)
                if (exportMode?.ToLower() == "ai" && builder.Length > MAX_CHARACTERS_AI)
                {
                    isPartial = true;

                    partialMessage = $"⚠ Limite atteinte : {MAX_CHARACTERS_AI:N0} caractères (mode IA)";
                    partialMessage += "\nLe contenu a été tronqué.";

                    builder.Length = MAX_CHARACTERS_AI;

                    builder.AppendLine();
                    builder.AppendLine("----------------------------------------");
                    builder.AppendLine(partialMessage);

                    break;
                }

                // 🔥 LIMITE MODE NORMAL
                if (exportMode?.ToLower() != "ai" && builder.Length > MAX_CHARACTERS_NORMAL)
                {
                    isPartial = true;

                    partialMessage = $"⚠ Limite atteinte : {MAX_CHARACTERS_NORMAL:N0} caractères";
                    partialMessage += "\nLe contenu a été tronqué.";

                    builder.Length = MAX_CHARACTERS_NORMAL;

                    builder.AppendLine();
                    builder.AppendLine("----------------------------------------");
                    builder.AppendLine(partialMessage);

                    break;
                }
            }

            return new ExportData
            {
                Content = builder.ToString(),
                Stats = stats,
                IsPartial = isPartial,
                PartialMessage = partialMessage
            };
        }

        // ═════════════════════════════════════════════════════════════
        // 3. FORMAT
        // ═════════════════════════════════════════════════════════════

        private void AppendFormatted(
            StringBuilder builder,
            string path,
            string content,
            bool isMarkdown)
        {
            if (isMarkdown)
            {
                builder.AppendLine($"## 📄 {path}");
                builder.AppendLine();
                builder.AppendLine("```");
                builder.AppendLine(content ?? "");
                builder.AppendLine("```");
                builder.AppendLine();
                builder.AppendLine("---");
                builder.AppendLine();
            }
            else
            {
                builder.AppendLine($"📄 {path}");
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine(content ?? "");
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine("----------------------------------------");
                builder.AppendLine();
                builder.AppendLine();
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 4. HELPERS
        // ═════════════════════════════════════════════════════════════

        private ExportResult Success(string message)
        {
            return new ExportResult
            {
                IsSuccess = true,
                Message = message
            };
        }

        private ExportResult Fail(string message)
        {
            return new ExportResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}