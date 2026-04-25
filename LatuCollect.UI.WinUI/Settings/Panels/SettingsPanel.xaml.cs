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
        // ═════════════════════════════════════════════════════════════
        // 1. ÉVÉNEMENTS
        // ═════════════════════════════════════════════════════════════
        //
        // Permet à la MainWindow de réagir (fermeture panel)
        //

        public event Action? OnCloseRequested;


        // ═════════════════════════════════════════════════════════════
        // 2. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public SettingsPanel()
        {
            this.InitializeComponent();
        }


        // ═════════════════════════════════════════════════════════════
        // 3. INITIALISATION
        // ═════════════════════════════════════════════════════════════
        //
        // Injection du ViewModel
        //

        public void Initialize(MainViewModel vm)
        {
            this.DataContext = vm;
            ContentFrame.Navigate(typeof(SettingsGeneralPage));
        }


        // ═════════════════════════════════════════════════════════════
        // 4. NAVIGATION
        // ═════════════════════════════════════════════════════════════

        private void OnGeneralClicked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(SettingsGeneralPage), DataContext);
        }

        private void OnExclusionsClicked(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(SettingsExclusionsPage), DataContext);
        }


        // ═════════════════════════════════════════════════════════════
        // 5. FERMETURE
        // ═════════════════════════════════════════════════════════════

        private void OnCloseClicked(object sender, RoutedEventArgs e)
        {
            OnCloseRequested?.Invoke();
        }
    }
}