/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Settings.Pages                                    ║
║  Fichier : SettingsGeneralPage.xaml.cs                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Page des paramètres généraux                                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Initialisation UI                                                 ║
║  - Réception du ViewModel (navigation)                               ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Aucun accès aux services                                          ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace LatuCollect.UI.WinUI.Settings.Pages
{
    public sealed partial class SettingsGeneralPage : Page
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════
        //
        // (aucun pour l’instant, mais section prête pour évolution)
        //


        // ═════════════════════════════════════════════════════════════
        // 2. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        
        public SettingsGeneralPage()
        {
            this.InitializeComponent();
        }


        // ═════════════════════════════════════════════════════════════
        // 3. NAVIGATION → RÉCEPTION DU VIEWMODEL
        // ═════════════════════════════════════════════════════════════

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e); // 🔥 IMPORTANT

            if (e.Parameter is MainViewModel vm)
            {
                DataContext = vm;
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 4. ÉVÉNEMENTS UI
        // ═════════════════════════════════════════════════════════════

        
        private async void OnResetClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            var dialog = new ContentDialog
            {
                Title = "Confirmation",
                Content = "Réinitialiser tous les paramètres ?",
                PrimaryButtonText = "Oui",
                CloseButtonText = "Annuler",
                XamlRoot = this.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                await vm.ResetConfigurationAsync();
            }
        }
    }
}