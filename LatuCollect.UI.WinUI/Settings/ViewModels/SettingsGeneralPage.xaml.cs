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

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using LatuCollect.UI.WinUI.ViewModels;

namespace LatuCollect.UI.WinUI.Settings.Pages
{
    public sealed partial class SettingsGeneralPage : Page
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public SettingsGeneralPage()
        {
            this.InitializeComponent();
        }
    }
}
