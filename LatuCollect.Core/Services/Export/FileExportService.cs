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
    // 1. MODÈLES (À EXTRAIRE PLUS TARD)
    // ═════════════════════════════════════════════════════════════
    //
    // ➜ FUTUR :
    // Core/Models/Export/
    //

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

        public bool IsPartial { get; set; }
        public string PartialMessage { get; set; } = "";
    }


    // ═════════════════════════════════════════════════════════════
    // 2. SERVICE EXPORT
    // ═════════════════════════════════════════════════════════════

    public class FileExportService
    {
        // ═════════════════════════════════════════════════════════════
        // 2.1 CONSTANTES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_FILES_AI = 20;
        private const int MAX_CHARACTERS_AI = 20000;
        private const int MAX_CHARACTERS_NORMAL = 1_000_000;


        // ═════════════════════════════════════════════════════════════
        // 2.2 EXPORT FICHIER
        // ═════════════════════════════════════════════════════════════

        public ExportResult Export(string path, string content)
        {
            return ExecuteExport(() =>
            {
                File.WriteAllText(path, content, Encoding.UTF8);
            });
        }

        public async Task<ExportResult> ExportAsync(string path, string content)
        {
            return await ExecuteExportAsync(async () =>
            {
                await File.WriteAllTextAsync(path, content, Encoding.UTF8);
            });
        }


        // ═════════════════════════════════════════════════════════════
        // 2.3 BUILD CONTENU (SYNC)
        // ═════════════════════════════════════════════════════════════

        public ExportData BuildContentWithStats(IEnumerable<string> filePaths, bool isMarkdown)
        {
            var builder = new StringBuilder();
            var stats = new StatisticsResult();

            foreach (var path in filePaths)
            {
                ProcessFileSync(path, builder, stats, isMarkdown);
            }

            return new ExportData
            {
                Content = builder.ToString(),
                Stats = stats
            };
        }


        // ═════════════════════════════════════════════════════════════
        // 2.4 BUILD CONTENU (ASYNC)
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

            foreach (var path in filePaths)
            {
                if (IsFileLimitReached(exportMode, fileCount))
                {
                    SetPartial(ref isPartial, ref partialMessage, builder,
                        $"⚠ Limite atteinte : {MAX_FILES_AI} fichiers maximum (mode IA)");
                    break;
                }

                await ProcessFileAsync(path, builder, stats, isMarkdown);

                fileCount++;

                if (IsCharLimitReached(exportMode, builder))
                {
                    SetPartial(ref isPartial, ref partialMessage, builder,
                        exportMode?.ToLower() == "ai"
                            ? $"⚠ Limite atteinte : {MAX_CHARACTERS_AI:N0} caractères (mode IA)"
                            : $"⚠ Limite atteinte : {MAX_CHARACTERS_NORMAL:N0} caractères");
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
        // 2.5 TRAITEMENT FICHIER
        // ═════════════════════════════════════════════════════════════

        private void ProcessFileSync(string path, StringBuilder builder, StatisticsResult stats, bool isMarkdown)
        {
            if (!File.Exists(path))
            {
                AppendFormatted(builder, path, "[Erreur : fichier introuvable]", isMarkdown);
                return;
            }

            var result = FileReaderService.ReadFileAsync(path).GetAwaiter().GetResult();

            if (!result.IsSuccess)
            {
                AppendFormatted(builder, path, $"[Erreur : {result.ErrorMessage}]", isMarkdown);
                return;
            }

            FileStatisticsService.UpdateStatistics(stats, result.Content, result.FileSize);
            AppendFormatted(builder, path, result.Content, isMarkdown);
        }

        private async Task ProcessFileAsync(string path, StringBuilder builder, StatisticsResult stats, bool isMarkdown)
        {
            if (!File.Exists(path))
            {
                AppendFormatted(builder, path, "[Erreur : fichier introuvable]", isMarkdown);
                return;
            }

            var result = await FileReaderService.ReadFileAsync(path);

            if (!result.IsSuccess)
            {
                AppendFormatted(builder, path, $"[Erreur : {result.ErrorMessage}]", isMarkdown);
                return;
            }

            FileStatisticsService.UpdateStatistics(stats, result.Content, result.FileSize);
            AppendFormatted(builder, path, result.Content, isMarkdown);
        }


        // ═════════════════════════════════════════════════════════════
        // 2.6 LIMITES
        // ═════════════════════════════════════════════════════════════

        private bool IsFileLimitReached(string exportMode, int fileCount)
        {
            return exportMode?.ToLower() == "ai" && fileCount >= MAX_FILES_AI;
        }

        private bool IsCharLimitReached(string exportMode, StringBuilder builder)
        {
            return exportMode?.ToLower() == "ai"
                ? builder.Length > MAX_CHARACTERS_AI
                : builder.Length > MAX_CHARACTERS_NORMAL;
        }

        private void SetPartial(ref bool isPartial, ref string message, StringBuilder builder, string text)
        {
            isPartial = true;

            message = text + "\nLe contenu a été tronqué.";

            builder.AppendLine();
            builder.AppendLine("----------------------------------------");
            builder.AppendLine(message);
        }


        // ═════════════════════════════════════════════════════════════
        // 2.7 FORMAT
        // ═════════════════════════════════════════════════════════════

        private void AppendFormatted(StringBuilder builder, string path, string content, bool isMarkdown)
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
        // 2.8 HELPERS EXPORT
        // ═════════════════════════════════════════════════════════════

        private ExportResult ExecuteExport(Action action)
        {
            try
            {
                SimulationService.SimulateExport();
                action();
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

        private async Task<ExportResult> ExecuteExportAsync(Func<Task> action)
        {
            try
            {
                SimulationService.SimulateExport();
                await action();
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

        private ExportResult Success(string message)
            => new() { IsSuccess = true, Message = message };

        private ExportResult Fail(string message)
            => new() { IsSuccess = false, Message = message };
    }
}