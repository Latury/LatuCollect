/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Simulation                                            ║
║  Fichier : SimulationService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Simuler des comportements pour tests et debug                       ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Simuler lecture de fichiers                                       ║
║  - Simuler erreurs d’export                                          ║
║  - Simuler cas spécifiques (UI, fichiers, erreurs)                   ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune dépendance UI                                              ║
║  - Utilise SimulationConfig                                          ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration;

namespace LatuCollect.Core.Simulation
{
    public static class SimulationService
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. LECTURE FICHIER
        // ═════════════════════════════════════════════════════════════════════

        public static string SimulateRead(string path)
        {
            if (!SimulationConfig.IsEnabled)
                return null;

            switch (SimulationConfig.Scenario)
            {
                case "FichiersVides":
                    return string.Empty;

                case "ErreursLecture":
                    return "[Erreur simulée de lecture]";

                case "CheminsLongs":
                    return "[Chemin trop long simulé]";
            }

            return null;
        }


        // ═════════════════════════════════════════════════════════════════════
        // 2. EXPORT
        // ═════════════════════════════════════════════════════════════════════

        public static void SimulateExport()
        {
            if (!SimulationConfig.IsEnabled)
                return;

            if (SimulationConfig.Scenario == "ErreursExport")
            {
                throw new System.Exception("Erreur simulée lors de l’export");
            }
        }


        // ═════════════════════════════════════════════════════════════════════
        // 3. HELPERS (UI / ÉTAT)
        // ═════════════════════════════════════════════════════════════════════

        public static bool IsLoaderSimulation()
        {
            return SimulationConfig.IsEnabled &&
                   SimulationConfig.Scenario == "UI_Loader";
        }

        public static bool IsErrorSimulation()
        {
            return SimulationConfig.IsEnabled &&
                   SimulationConfig.Scenario == "UI_Error";
        }
    }
}