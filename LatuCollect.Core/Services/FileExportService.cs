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
║  - Construire le contenu exporté                                     ║
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
using System.Collections.Generic;
using System.IO;
using System;

namespace LatuCollect.Core.Services
{
    // ✅ Résultat d'export (simple et clair)
    public class ExportResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
    }

    public static class FileExportService
    {
        public static ExportResult Export(string path, string content)
        {
            try
            {
                // 🔥 Simulation
                SimulationService.SimulateExport();

                // ✔ Export réel
                File.WriteAllText(path, content);

                return new ExportResult
                {
                    IsSuccess = true,
                    Message = "Export réussi"
                };
            }
            catch (Exception ex)
            {
                return new ExportResult
                {
                    IsSuccess = false,
                    Message = $"Erreur export : {ex.Message}"
                };
            }
        }

        // 🧠 Construction du contenu (SOURCE UNIQUE)
        public static string BuildContent(IEnumerable<string> filePaths)
        {
            string result = "";

            foreach (var path in filePaths)
            {
                if (!File.Exists(path))
                    continue;

                string content = FileReaderService.ReadFile(path);

                result +=
                    $"{path}\n\n\n" +
                    $"{content}\n\n\n" +
                    $"----------------------------------------\n\n\n";
            }

            return result;
        }
    }
}