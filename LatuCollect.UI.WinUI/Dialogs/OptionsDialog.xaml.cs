/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI.WinUI.Dialogs                                           ║
║  Fichier : OptionsDialog.xaml.cs                                     ║
║                                                                      ║
║  Rôle :                                                              ║
║  Dialogue des paramètres utilisateur                                 ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Afficher les options (UI uniquement)                              ║
║  - Se binder au ViewModel (MainViewModel)                            ║
║  - Gérer les interactions UI simples                                 ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - AUCUNE logique métier ici                                         ║
║  - AUCUN accès direct aux services                                   ║
║  - Utilise uniquement le DataContext (MVVM)                          ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - WinUI (ContentDialog)                                             ║
║  - MainViewModel (via DataContext)                                   ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace LatuCollect.UI.WinUI.Dialogs
{
    public sealed partial class OptionsDialog : ContentDialog
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════
        //
        // Initialise le dialogue :
        // - Charge le XAML
        // - Aucun traitement métier
        //
        public OptionsDialog()
        {
            this.InitializeComponent();
        }

        // ═════════════════════════════════════════════════════════════
        // 2. ÉVÉNEMENTS UI (OPTIONNEL)
        // ═════════════════════════════════════════════════════════════
        //
        // ⚠️ À utiliser uniquement si nécessaire
        // ⚠️ Ne jamais mettre de logique métier ici
        //
        // Exemple : fermeture du dialog, animation, etc.
        //

        private async void OnExclusionsClicked(object sender, RoutedEventArgs e)
        {
            // temporaire (on fera mieux après)
            var dialog = new ContentDialog
            {
                Title = "Dossiers exclus",
                Content = new TextBlock { Text = "À implémenter" },
                CloseButtonText = "OK",
                XamlRoot = this.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private async void OnPrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (this.DataContext is MainViewModel vm)
            {
                args.Cancel = true; // 🔥 BLOQUE la fermeture

                await vm.SaveConfigurationAsync();

                this.Hide(); // 🔥 ferme manuellement après save
            }
        }

        private async void OnResetClicked(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel vm)
            {
                var confirm = new ContentDialog
                {
                    Title = "Confirmation",
                    Content = "Réinitialiser la configuration ?",
                    PrimaryButtonText = "Oui",
                    CloseButtonText = "Non",
                    XamlRoot = this.XamlRoot
                };

                if (await confirm.ShowAsync() == ContentDialogResult.Primary)
                {
                    await vm.ResetConfigurationAsync();
                }
            }
        }
    }
}