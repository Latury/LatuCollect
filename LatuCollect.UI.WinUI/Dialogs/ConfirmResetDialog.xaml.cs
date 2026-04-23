/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Dialogs                                           ║
║  Fichier : ConfirmResetDialog.xaml.cs                                ║
║                                                                      ║
║  Rôle :                                                              ║
║  Code-behind du dialogue de confirmation                             ║
║                                                                      ║
║  Responsabilités :                                                   ║
║  - Initialisation du composant                                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - UI uniquement                                                     ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml.Controls;

namespace LatuCollect.UI.WinUI.Dialogs
{
    public sealed partial class ConfirmResetDialog : ContentDialog
    {
        public ConfirmResetDialog()
        {
            this.InitializeComponent();
        }
    }
}