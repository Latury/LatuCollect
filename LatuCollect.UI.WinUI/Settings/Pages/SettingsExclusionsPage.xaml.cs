/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Settings.Pages                                    ║
║  Fichier : SettingsExclusionsPage.xaml.cs                            ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gestion UI des dossiers exclus                                      ║
║                                                                      ║
║  Responsabilités :                                                   ║
║  - Réception du ViewModel                                            ║
║  - Interaction utilisateur                                           ║
║  - Modification simple de la configuration                           ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Pas de logique métier complexe                                    ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace LatuCollect.UI.WinUI.Settings.Pages
{
    public sealed partial class SettingsExclusionsPage : Page
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public SettingsExclusionsPage()
        {
            this.InitializeComponent();
        }

        // ═════════════════════════════════════════════════════════════
        // 2. ➕ AJOUT
        // ═════════════════════════════════════════════════════════════

        private void OnAddClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            var value = InputExclude.Text?.Trim();

            if (string.IsNullOrWhiteSpace(value))
                return;

            if (!vm.Config.ExcludedFolders.Contains(value))
            {
                vm.Config.ExcludedFolders.Add(value);
                _ = vm.SaveConfigurationAsync();
            }

            InputExclude.Text = "";
        }

        // ═════════════════════════════════════════════════════════════
        // 3. 🗑 SUPPRESSION
        // ═════════════════════════════════════════════════════════════

        private void OnRemoveClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            if (ExclusionsList.SelectedItem is string selected)
            {
                vm.Config.ExcludedFolders.Remove(selected);
                _ = vm.SaveConfigurationAsync();
            }
        }
    }
}