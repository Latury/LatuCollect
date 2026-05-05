/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Settings.Pages                                    ║
║  Fichier : SettingsExclusionsPage.xaml.cs                            ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gestion UI des dossiers exclus                                      ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - UI uniquement                                                     ║
║  - Aucune logique métier                                             ║
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
        // ==========================================
        // 1. CHAMPS PRIVÉS
        // ==========================================
        //
        // (aucun pour l’instant — section conservée volontairement)
        //


        // ==========================================
        // 2. CONSTRUCTEUR
        // ==========================================

        public SettingsExclusionsPage()
        {
            this.InitializeComponent();
        }


        // ==========================================
        // 3. NAVIGATION → RÉCEPTION DU VIEWMODEL
        // ==========================================

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is MainViewModel vm)
            {
                DataContext = vm;
            }
        }


        // ==========================================
        // 4. ÉVÉNEMENTS UI — AJOUT
        // ==========================================

        private async void OnAddClicked(object sender, RoutedEventArgs e)
        {
            // Récupération ViewModel
            if (DataContext is not MainViewModel vm)
                return;

            // Lecture valeur utilisateur
            var value = InputExclude.Text?.Trim();

            // Validation : vide
            if (string.IsNullOrWhiteSpace(value))
                return;

            // Vérification doublon
            if (!vm.Config.ExcludedFolders.Contains(value))
            {
                vm.Config.ExcludedFolders.Add(value);

                // 💾 Sauvegarde configuration
                await vm.SaveConfigurationAsync();

                // 🔁 Rechargement arbre + preview
                if (!string.IsNullOrWhiteSpace(vm.CurrentFolderPath))
                {
                    await vm.LoadTreeAsync(vm.CurrentFolderPath);
                }
            }

            // Reset champ input
            InputExclude.Text = "";
        }


        // ==========================================
        // 5. ÉVÉNEMENTS UI — SUPPRESSION
        // ==========================================

        private async void OnRemoveClicked(object sender, RoutedEventArgs e)
        {
            // Récupération ViewModel
            if (DataContext is not MainViewModel vm)
                return;

            // Récupération sélection
            if (ExclusionsList.SelectedItem is string selected)
            {
                vm.Config.ExcludedFolders.Remove(selected);

                // 💾 Sauvegarde configuration
                await vm.SaveConfigurationAsync();

                // 🔁 Rechargement arbre + preview
                if (!string.IsNullOrWhiteSpace(vm.CurrentFolderPath))
                {
                    await vm.LoadTreeAsync(vm.CurrentFolderPath);
                }
            }
        }
    }
}