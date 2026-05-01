/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Settings.Panels                                   ║
║  Fichier : SettingsPanel.xaml.cs                                     ║
║                                                                      ║
║  Rôle :                                                              ║
║  Code-behind du panneau de paramètres                                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Initialisation UI                                                 ║
║  - Navigation entre pages                                            ║
║  - Gestion fermeture du panneau                                      ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Utilise uniquement le ViewModel                                   ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.Settings.Pages;
using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace LatuCollect.UI.WinUI.Settings.Panels
{
    public sealed partial class SettingsPanel : UserControl
    {
        // ==========================================
        // ÉVÉNEMENTS
        // ==========================================
        //
        // Permet à la MainWindow de réagir (fermeture panel)
        //

        public event Action? OnCloseRequested;


        // ==========================================
        // CHAMPS PRIVÉS
        // ==========================================
        //
        // État interne UI (navigation + ViewModel)
        //

        private MainViewModel? _vm;

        // Page actuelle (évite re-navigation inutile)
        private Type? _currentPage;


        // ==========================================
        // CONSTRUCTEUR
        // ==========================================

        public SettingsPanel()
        {
            this.InitializeComponent();
        }


        // ==========================================
        // INITIALISATION
        // ==========================================
        //
        // Injection du ViewModel
        //

        public void Initialize(MainViewModel vm)
        {
            _vm = vm;
            this.DataContext = vm;

            Navigate(typeof(SettingsGeneralPage));
        }


        // ==========================================
        // NAVIGATION INTERNE
        // ==========================================
        //
        // Gère le changement de page dans le panneau
        //

        private void Navigate(Type page)
        {
            // Évite reload inutile
            if (_currentPage == page)
                return;

            _currentPage = page;

            ContentFrame.Navigate(page, _vm);
        }


        // ==========================================
        // ACTIONS NAVIGATION (UI)
        // ==========================================

        private void OnGeneralClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(SettingsGeneralPage));
        }

        private void OnExclusionsClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(SettingsExclusionsPage));
        }


        // ==========================================
        // ACTION NAVIGATION EXPORT
        // ==========================================

        private void OnExportModeClicked(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(SettingsExportPage));
        }


        // ==========================================
        // FERMETURE
        // ==========================================

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            OnCloseRequested?.Invoke();
        }
    }
}