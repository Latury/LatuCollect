/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core                                                       ║
║  Fichier : SimulationService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Simuler des comportements pour les tests                            ║
║                                                                      ║
║  VERSION SAFE :                                                      ║
║  - NE LANCE PLUS D’EXCEPTION                                         ║
║  - Retourne uniquement des valeurs contrôlées                        ║
║                                                                      ║
║  Avantages :                                                         ║
║  - Aucun crash                                                       ║
║  - Simulation stable                                                 ║
║  - Respect ALC                                                       ║
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
        // ======================================================
        // 📥 SIMULATION LECTURE (SAFE)
        // ======================================================

        public static string SimulateRead(string path)
        {
            if (!SimulationConfig.IsEnabled)
                return null;

            switch (SimulationConfig.Scenario)
            {
                case "FichiersVides":
                    return "";

                case "ErreursLecture":
                    return "[Erreur de lecture]";

                case "CheminsLongs":
                    return "[Chemin trop long]";

                default:
                    return null;
            }
        }

        // ======================================================
        // 📤 SIMULATION EXPORT
        // ======================================================

        public static void SimulateExport()
        {
            if (!SimulationConfig.IsEnabled)
                return;

            if (SimulationConfig.Scenario == "ErreursExport")
            {
                // ⚠️ Ici on garde le throw volontaire
                // car il est géré côté UI (try/catch export)
                throw new Exception("Erreur lors de l'export (simulation)");
            }
        }
    }
}