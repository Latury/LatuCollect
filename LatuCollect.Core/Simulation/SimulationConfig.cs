/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration                                         ║
║  Fichier : SimulationConfig.cs                                       ║
║                                                                      ║
║  Rôle :                                                              ║
║  Stocker l’état global de simulation                                 ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Activer / désactiver la simulation                                ║
║  - Définir le scénario actif                                         ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Utilisé uniquement par le Core                                    ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Simulation
{
    public static class SimulationConfig
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. ÉTAT GLOBAL SIMULATION
        // ═════════════════════════════════════════════════════════════════════
        //
        // Active ou non les comportements simulés
        //

        public static bool IsEnabled { get; set; } = false;


        // ═════════════════════════════════════════════════════════════════════
        // 2. SCÉNARIO ACTIF
        // ═════════════════════════════════════════════════════════════════════
        //
        // Définit quel type de simulation est utilisé
        // (ex: erreurs, fichiers vides, etc.)
        //

        public static string Scenario { get; set; } = "Aucun";
    }
}