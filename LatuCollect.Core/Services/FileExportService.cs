/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core                                                       ║
║  Fichier : FileExportService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Écrire le contenu final dans un fichier                             ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Écrire du texte dans un fichier                                   ║
║  - Construire le contenu exporté + statistiques                      ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - System.IO                                                         ║
║  - SimulationService                                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Simulation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LatuCollect.Core.Services
{
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

    public static class FileExportService
    {
        public static ExportResult Export(string path, string content)
        {
            try
            {
                SimulationService.SimulateExport();
                File.WriteAllText(path, content);

                return new ExportResult
                {
                    IsSuccess = true,
                    Message = "Export réussi"
                };
            }
            catch (UnauthorizedAccessException)
            {
                return new ExportResult
                {
                    IsSuccess = false,
                    Message = "⛔ Accès refusé. Vérifie les permissions du dossier."
                };
            }
            catch (IOException)
            {
                return new ExportResult
                {
                    IsSuccess = false,
                    Message = "📁 Impossible d'écrire le fichier. Il est peut-être ouvert dans un autre programme."
                };
            }
            catch (ArgumentException)
            {
                return new ExportResult
                {
                    IsSuccess = false,
                    Message = "⚠ Chemin de fichier invalide."
                };
            }
            catch (Exception ex)
            {
                return new ExportResult
                {
                    IsSuccess = false,
                    Message = $"Erreur inattendue : {ex.Message}"
                };
            }
        }

        public static ExportData BuildContentWithStats(IEnumerable<string> filePaths, bool isMarkdown)
        {
            StringBuilder result = new StringBuilder();
            var stats = new StatisticsResult();

            foreach (var path in filePaths)
            {
                if (!File.Exists(path))
                    continue;

                stats.FileCount++;

                string content = FileReaderService.ReadFile(path);

                if (string.IsNullOrWhiteSpace(content))
                {
                    content = "[Fichier vide ou erreur de lecture]";
                }

                // 📊 Stats
                stats.TotalCharacters += content.Length;
                stats.TotalLines += content.Split('\n').Length;

                var fileInfo = new FileInfo(path);
                stats.TotalSizeBytes += fileInfo.Length;

                // 📄 Contenu
                if (isMarkdown)
                {
                    result.AppendLine($"## 📄 {path}");
                    result.AppendLine();
                    result.AppendLine("```");
                    result.AppendLine(content);
                    result.AppendLine("```");
                    result.AppendLine();
                    result.AppendLine("---");
                    result.AppendLine();
                }
                else
                {
                    result.AppendLine($"📄 {path}");
                    result.AppendLine();
                    result.AppendLine();
                    result.AppendLine(content);
                    result.AppendLine();
                    result.AppendLine();
                    result.AppendLine("----------------------------------------");
                    result.AppendLine();
                    result.AppendLine();
                }
            }

            return new ExportData
            {
                Content = result.ToString(),
                Stats = stats
            };
        }
    }
}