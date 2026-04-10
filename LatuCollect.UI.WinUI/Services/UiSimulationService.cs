/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI                                                         ║
║  Fichier : UiSimulationService.cs                                    ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer les simulations liées à l’interface utilisateur               ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Déterminer l’état UI simulé (Loading / Error)                     ║
║  - Centraliser la logique de simulation côté UI                      ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - NE modifie JAMAIS le ViewModel directement                        ║
║  - NE contient AUCUNE logique métier                                 ║
║  - Retourne uniquement des états                                     ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - SimulationConfig (Core)                                           ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Simulation;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.UI.WinUI.Services
{
    public static class UiSimulationService
    {
        // ======================================================
        // 🔍 ÉTAT SIMULATION UI
        // ======================================================

        public static UiSimulationResult GetState()
        {
            if (!SimulationConfig.IsEnabled)
                return UiSimulationResult.None;

            return SimulationConfig.Scenario switch
            {
                "UI_Loader" => UiSimulationResult.Loading,
                "UI_Error" => UiSimulationResult.Error,
                _ => UiSimulationResult.None
            };
        }

        // ======================================================
        // 🧪 APPLICATION SIMULATION UI
        // ======================================================

        public static bool ApplyUiSimulation(MainViewModel viewModel)
        {
            var sim = GetState();

            if (sim == UiSimulationResult.Error)
            {
                viewModel.CurrentState = MainViewModel.UiState.Error;
                viewModel.ErrorMessage = "Erreur simulée (UI)";
                return false;
            }

            if (sim == UiSimulationResult.Loading)
            {
                System.Threading.Thread.Sleep(2000); // temporaire
            }

            return true;
        }
    }

    // ======================================================
    // 📊 RESULTAT SIMULATION
    // ======================================================

    public enum UiSimulationResult
    {
        None,
        Loading,
        Error
    }
}