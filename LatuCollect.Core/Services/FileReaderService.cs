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

using LatuCollect.Core.Simulation;
using System;
using System.IO;

namespace LatuCollect.Core.Services
{
    public static class FileReaderService
    {
        public static string ReadFile(string path)
        {
            try
            {
                if (!File.Exists(path))
                    return "[Fichier introuvable]";

                // 🔥 Simulation
                string simulated = SimulationService.SimulateRead(path);
                if (simulated != null)
                    return simulated;

                // ✔ Lecture normale
                return File.ReadAllText(path);
            }
            catch (PathTooLongException)
            {
                return "[Chemin trop long]";
            }
            catch (IOException)
            {
                return "[Erreur de lecture]";
            }
            catch (Exception)
            {
                return "[Erreur inconnue]";
            }
        }
    }
}