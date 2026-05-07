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

using LatuCollect.Core.Configuration.Models;
using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            if (DataContext is not MainViewModel vm)
                return;

            var value = InputExclude.Text?.Trim();

            if (string.IsNullOrWhiteSpace(value))
                return;

            // Vérifie doublon (sur Name)
            if (!vm.Config.ExcludedFolders.Any(e =>
                string.Equals(e.Name, value, StringComparison.OrdinalIgnoreCase)))
            {
                var item = new ExclusionItem(value, false);

                vm.Config.ExcludedFolders.Add(item);

                await vm.SaveConfigurationAsync();

                if (!string.IsNullOrWhiteSpace(vm.CurrentFolderPath))
                {
                    await vm.LoadTreeAsync(vm.CurrentFolderPath);
                }
            }

            InputExclude.Text = "";
        }


        private async void OnRemoveClicked(object sender, RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            if (vm.SelectedExclusion == null)
                return;

            var item = vm.SelectedExclusion;

            if (item.IsProtected && !vm.IsDeveloperMode)
            {
                await new ContentDialog
                {
                    Title = "Protégé",
                    Content = "Impossible de supprimer un élément protégé sans le mode développeur.",
                    CloseButtonText = "OK",
                    XamlRoot = this.XamlRoot
                }.ShowAsync();

                return;
            }

            var confirm = await new ContentDialog
            {
                Title = "Confirmation",
                Content = $"Supprimer l'exclusion :\n{item.Name} ?",
                PrimaryButtonText = "Oui",
                CloseButtonText = "Non",
                XamlRoot = this.XamlRoot
            }.ShowAsync();

            if (confirm != ContentDialogResult.Primary)
                return;

            // 🔥 Sauvegarde position scroll
            double currentOffset = 0;

            var scrollViewer = FindScrollViewer(ExclusionsList);

            if (scrollViewer != null)
            {
                currentOffset = scrollViewer.VerticalOffset;
            }

            // 🔥 Suppression sécurisée
            var itemsToRemove = vm.Config.ExcludedFolders
                .Where(e =>
                    string.Equals(e.Name, item.Name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var exclusion in itemsToRemove)
            {
                vm.Config.ExcludedFolders.Remove(exclusion);
            }

            vm.SelectedExclusion = null;

            // 🔥 Refresh UI
            vm.RefreshExclusionsUi();

            // 🔥 Restauration scroll
            await Task.Delay(50);

            scrollViewer = FindScrollViewer(ExclusionsList);

            scrollViewer?.ChangeView(null, currentOffset, null);

            // 💾 Sauvegarde
            await vm.SaveConfigurationAsync();

            // 🔄 Rechargement arbre
            if (!string.IsNullOrWhiteSpace(vm.CurrentFolderPath))
            {
                await vm.LoadTreeAsync(vm.CurrentFolderPath);
            }
        }

        // ==========================================
        // 5. GESTION SÉLECTION LISTE
        // ==========================================

        private ScrollViewer? FindScrollViewer(DependencyObject parent)
        {
            if (parent == null)
                return null;

            if (parent is ScrollViewer scrollViewer)
                return scrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                var result = FindScrollViewer(child);

                if (result != null)
                    return result;
            }

            return null;
        }

        private void ExclusionsList_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            // 🔒 Ignore les headers texte
            if (ExclusionsList.SelectedItem is not ExclusionItem item)
            {
                ExclusionsList.SelectedItem = null;
                vm.SelectedExclusion = null;

                return;
            }

            vm.SelectedExclusion = item;
        }
    }
}