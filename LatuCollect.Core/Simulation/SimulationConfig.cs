/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core                                                       ║
║  Fichier : SimulationConfig.cs                                       ║
║                                                                      ║
║  Rôle :                                                              ║
║  Configurer le système de simulation                                 ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Activer / désactiver la simulation                                ║
║  - Définir le scénario de simulation                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Simulation
{
    public static class SimulationConfig
    {
        /// <summary>
        // Active ou désactive la simulation
        /// </summary>
        public static bool IsEnabled { get; set; } = true; // Par défaut, la simulation est désactivée

        /// <summary>
        // Nom du scénario de simulation
        /// </summary>
        public static string Scenario { get; set; } = "FichiersVolumineux"; // Par défaut, aucun scénario n'est défini
    }
}