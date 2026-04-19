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

    public class FileExportService
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. EXPORT FICHIER
        // ═════════════════════════════════════════════════════════════════════
        //
        // Écrit le contenu dans un fichier
        //

        public ExportResult Export(string path, string content)
        {
            try
            {
                // 🧪 Simulation
                SimulationService.SimulateExport();

                // 📄 Écriture fichier
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


        // ═════════════════════════════════════════════════════════════════════
        // 2. CONSTRUCTION CONTENU + STATISTIQUES
        // ═════════════════════════════════════════════════════════════════════
        //
        // Assemble :
        // - contenu des fichiers
        // - statistiques
        //

        public ExportData BuildContentWithStats(IEnumerable<string> filePaths, bool isMarkdown)
        {
            var result = new StringBuilder();
            var stats = new StatisticsResult();

            foreach (var path in filePaths)
            {
                if (!File.Exists(path))
                    continue;

                // 📄 Lecture fichier
                string content = FileReaderService.ReadFile(path);

                if (string.IsNullOrWhiteSpace(content))
                {
                    content = "[Fichier vide ou erreur de lecture]";
                }

                // 📊 Statistiques (APRÈS avoir le contenu)
                FileStatisticsService.UpdateStatistics(stats, content, path);

                // 📦 Formatage
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