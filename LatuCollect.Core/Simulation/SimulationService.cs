/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core                                                       ║
║  Fichier : SimulationService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Simuler des comportements pour les tests                            ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Simuler la lecture des fichiers                                   ║
║  - Simuler les erreurs d’export                                      ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System;

namespace LatuCollect.Core.Simulation
{
    public static class SimulationService
    {
        // 📥 Simulation lecture
        public static string SimulateRead(string path)
        {
            if (!SimulationConfig.IsEnabled)
                return null;

            switch (SimulationConfig.Scenario)
            {
                case "FichiersVides":
                    return "";

                case "FichiersCorrompus":
                    return "### ERREUR : FICHIER CORROMPU ###";

                case "ErreursAcces":
                    throw new UnauthorizedAccessException("Accès refusé (simulation)");

                case "FichiersVolumineux":
                    return GenerateLargeContent();

                default:
                    return null;
            }
        }

        // 📤 Simulation export
        public static void SimulateExport()
        {
            if (!SimulationConfig.IsEnabled)
                return;

            if (SimulationConfig.Scenario == "ExportErrors")
            {
                throw new Exception("Erreur lors de l'export (simulation)");
            }
        }

        // 🔧 Génération gros contenu
        private static string GenerateLargeContent()
        {
            return new string('A', 100000); // 100k caractères
        }
    }
}