/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Settings.Pages                                    ║
║  Fichier : SettingsExportPage.xaml.cs                                ║
║                                                                      ║
║  Rôle :                                                              ║
║  Code-behind de la page mode d’export                                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Initialisation UI                                                 ║
║  - Réception du ViewModel                                            ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Binding uniquement                                                ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace LatuCollect.UI.WinUI.Settings.Pages
{
    public sealed partial class SettingsExportPage : Page
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public SettingsExportPage()
        {
            this.InitializeComponent();
        }

        // ═════════════════════════════════════════════════════════════
        // 2. NAVIGATION → RÉCEPTION DU VIEWMODEL
        // ═════════════════════════════════════════════════════════════

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is MainViewModel vm)
            {
                DataContext = vm;
            }
        }
    }
}