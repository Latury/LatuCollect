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
║  - Écrire le fichier exporté                                         ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - FileReaderService                                                 ║
║  - SimulationService                                                 ║
║  - System.IO                                                         ║
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
    }


    // ═════════════════════════════════════════════════════════════
    // 2. SERVICE EXPORT
    // ═════════════════════════════════════════════════════════════

    public class FileExportService
    {
        // ═════════════════════════════════════════════════════════════
        // 2.1 EXPORT FICHIER
        // ═════════════════════════════════════════════════════════════

        public ExportResult Export(string path, string content)
        {
            if (string.IsNullOrWhiteSpace(path))
                return new ExportResult { IsSuccess = false, Message = "Chemin invalide" };

            try
            {
                // 🧪 Simulation
                SimulationService.SimulateExport();

                // 📄 Écriture
                File.WriteAllText(path, content, Encoding.UTF8);

                return new ExportResult
                {
                    IsSuccess = true,
                    Message = "Export réussi"
                };
            }
            catch (UnauthorizedAccessException)
            {
                return Fail("⛔ Accès refusé. Vérifie les permissions.");
            }
            catch (IOException)
            {
                return Fail("📁 Fichier utilisé ou inaccessible.");
            }
            catch (ArgumentException)
            {
                return Fail("⚠ Chemin invalide.");
            }
            catch (Exception ex)
            {
                return Fail($"Erreur inattendue : {ex.Message}");
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 2.2 BUILD CONTENU + STATS
        // ═════════════════════════════════════════════════════════════

        public ExportData BuildContentWithStats(IEnumerable<string> filePaths, bool isMarkdown)
        {
            var builder = new StringBuilder();
            var stats = new StatisticsResult();

            foreach (var path in filePaths)
            {
                if (!File.Exists(path))
                    continue;

                var content = ReadSafe(path);

                FileStatisticsService.UpdateStatistics(stats, content, path);

                AppendFormatted(builder, path, content, isMarkdown);
            }

            return new ExportData
            {
                Content = builder.ToString(),
                Stats = stats
            };
        }


        // ═════════════════════════════════════════════════════════════
        // 3. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════

        private string ReadSafe(string path)
        {
            var content = FileReaderService.ReadFile(path);

            if (string.IsNullOrWhiteSpace(content))
                return "[Fichier vide ou erreur de lecture]";

            return content;
        }


        private void AppendFormatted(StringBuilder builder, string path, string content, bool isMarkdown)
        {
            if (isMarkdown)
            {
                builder.AppendLine($"## 📄 {path}");
                builder.AppendLine();
                builder.AppendLine("```");
                builder.AppendLine(content);
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
                builder.AppendLine(content);
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine("----------------------------------------");
                builder.AppendLine();
                builder.AppendLine();
            }
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