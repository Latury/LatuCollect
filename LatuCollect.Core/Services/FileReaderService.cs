/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core                                                       ║
║  Fichier : FileReaderService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Lire le contenu des fichiers                                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Lire un fichier texte                                             ║
║  - Retourner son contenu brut                                        ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - System.IO                                                         ║
║  - SimulationService                                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.IO;
using LatuCollect.Core.Simulation;

namespace LatuCollect.Core.Services
{
    public static class FileReaderService
    {
        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
                return "[Fichier introuvable]";

            // 🔥 Simulation
            var simulated = SimulationService.SimulateRead(path);
            if (simulated != null)
                return simulated;

            // ✔ Lecture normale
            return File.ReadAllText(path);
        }
    }
}