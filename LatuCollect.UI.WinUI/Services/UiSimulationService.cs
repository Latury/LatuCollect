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
║  - Appliquer les effets de simulation sur le ViewModel               ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Logique UI uniquement                                             ║
║  - Peut modifier le ViewModel (UI uniquement)                        ║
║  - Aucune logique métier                                             ║
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
using System.Threading.Tasks;

namespace LatuCollect.UI.WinUI.Services
{
    public static class UiSimulationService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════
        //
        // (Aucun champ ici pour l’instant)
        // Classe statique → pas d’état interne
        //


        // ═════════════════════════════════════════════════════════════
        // 2. ÉTAT SIMULATION UI
        // ═════════════════════════════════════════════════════════════
        //
        // Retourne l’état simulé actuel
        // (aucune modification du ViewModel ici)
        //

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


        // ═════════════════════════════════════════════════════════════
        // 3. APPLICATION SIMULATION UI
        // ═════════════════════════════════════════════════════════════
        //
        // Applique les effets visuels sur le ViewModel
        //

        public static async Task<bool> ApplyUiSimulationAsync(MainViewModel viewModel)
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
                await Task.Delay(2000); // ✅ non bloquant
            }

            return true;
        }


        // ═════════════════════════════════════════════════════════════
        // 4. RÉSULTAT SIMULATION
        // ═════════════════════════════════════════════════════════════
        //
        // États possibles de simulation UI
        //

        public enum UiSimulationResult
        {
            None,
            Loading,
            Error
        }
    }
}