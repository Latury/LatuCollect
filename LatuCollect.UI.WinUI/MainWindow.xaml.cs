/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI                                                         ║
║  Fichier : MainWindow.xaml.cs                                        ║
║                                                                      ║
║  Rôle :                                                              ║
║  Fenêtre principale (View)                                           ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Initialiser l’interface                                           ║
║  - Associer le ViewModel                                             ║
║  - Gérer les interactions utilisateur                                ║
║  - Afficher les dialogues (UI uniquement)                            ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - AUCUNE logique métier ici                                         ║
║  - Appelle uniquement le ViewModel                                   ║
║  - Affichage / orchestration uniquement                              ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - WinUI (Microsoft.UI.Xaml)                                         ║
║  - MainViewModel                                                     ║
║  - Windows.Storage.Pickers                                           ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Services;
using LatuCollect.Core.Simulation;
using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace LatuCollect.UI.WinUI
{
    public sealed partial class MainWindow : Window
    {
        // ======================================================
        // 🧠 VIEWMODEL (liaison UI)
        // ======================================================

        private readonly MainViewModel _viewModel = new();

        // ======================================================
        // 🚀 INITIALISATION
        // ======================================================

        public MainWindow()
        {
            this.InitializeComponent();

            if (this.Content is FrameworkElement root)
                root.DataContext = _viewModel;
        }

        // ======================================================
        // 📂 SÉLECTION DOSSIER
        // ======================================================

        private async void OnPickFolderClicked(object _, RoutedEventArgs __)
        {
            FolderPicker picker = new();

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            picker.FileTypeFilter.Add("*");

            StorageFolder folder = await picker.PickSingleFolderAsync();

            if (folder != null)
            {
                _viewModel.CurrentFolderPath = folder.Path;
                _viewModel.LoadTree(folder.Path);
            }
        }

        // ======================================================
        // 📄 FORMAT (RadioButton)
        // ======================================================

        private void OnTxtSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".txt";
        }

        private void OnMdSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".md";
        }

        // ======================================================
        // 📤 EXPORT
        // ======================================================

        private async void OnExportClicked(object sender, RoutedEventArgs e)
        {
            // 🔴 FORMAT NON SÉLECTIONNÉ
            if (string.IsNullOrWhiteSpace(_viewModel.SelectedFormat))
            {
                await ShowDialog(
                    "Format manquant",
                    "Veuillez sélectionner un format (.txt ou .md).");
                return;
            }

            // 🔍 CONTENU
            string content = _viewModel.GetExportContent();

            if (string.IsNullOrWhiteSpace(content))
            {
                await ShowDialog(
                    "Aucun contenu",
                    "Aucun contenu à exporter.");
                return;
            }

            // 📁 PICKER
            FileSavePicker picker = new();

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            bool isMd = _viewModel.SelectedFormat == ".md";

            picker.SuggestedFileName = "export";
            picker.FileTypeChoices.Add(
                isMd ? "Markdown" : "Text",
                new[] { isMd ? ".md" : ".txt" });

            StorageFile file = await picker.PickSaveFileAsync();

            if (file == null)
                return;

            // ⚙️ EXPORT
            try
            {
                FileExportService.Export(file.Path, content);

                await ShowDialog(
                    "Export réussi",
                    "Fichier exporté avec succès.");
            }
            catch (Exception ex)
            {
                await ShowDialog(
                    "Erreur d'export",
                    ex.Message);
            }
        }

        // ======================================================
        // 💬 DIALOGUES UI
        // ======================================================

        private async System.Threading.Tasks.Task ShowDialog(string title, string message)
        {
            ContentDialog dialog = new()
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        private async System.Threading.Tasks.Task<bool> ShowConfirm(string title, string message)
        {
            ContentDialog dialog = new()
            {
                Title = title,
                Content = message,
                PrimaryButtonText = "Oui",
                CloseButtonText = "Non",
                XamlRoot = this.Content.XamlRoot
            };

            return await dialog.ShowAsync() == ContentDialogResult.Primary;
        }

        // ======================================================
        // 🧪 SIMULATION (DEV)
        // ======================================================

        private async void OnSimulationClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Mode simulation",
                PrimaryButtonText = "Appliquer",
                CloseButtonText = "Annuler",
                XamlRoot = this.Content.XamlRoot
            };

            var stack = new StackPanel { Spacing = 10 };

            var toggle = new ToggleSwitch
            {
                Header = "Activer la simulation",
                IsOn = _viewModel.IsSimulationEnabled
            };

            var combo = new ComboBox
            {
                ItemsSource = new string[]
                {
                    "Aucun",
                    "FichiersVides",
                    "ErreursExport",
                    "ErreursLecture",
                    "CheminsLongs"
                },
                SelectedItem = _viewModel.SelectedSimulationScenario
            };

            stack.Children.Add(toggle);
            stack.Children.Add(new TextBlock { Text = "Scénario :" });
            stack.Children.Add(combo);

            dialog.Content = stack;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                _viewModel.IsSimulationEnabled = toggle.IsOn;
                _viewModel.SelectedSimulationScenario = combo.SelectedItem?.ToString() ?? "Aucun";

                SimulationConfig.IsEnabled = _viewModel.IsSimulationEnabled;
                SimulationConfig.Scenario = _viewModel.SelectedSimulationScenario;
            }
        }

        // ======================================================
        // 🧰 ACTIONS UI SECONDAIRES
        // ======================================================

        private void OnCopyClicked(object sender, RoutedEventArgs e)
        {
            string content = _viewModel.GetExportContent();

            var package = new Windows.ApplicationModel.DataTransfer.DataPackage();
            package.SetText(content);

            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(package);
        }

        private async void OnHelpClicked(object sender, RoutedEventArgs e)
        {
            await ShowDialog(
                "Aide",
                "1. Sélectionne un dossier\n2. Choisis des fichiers\n3. Exporte");
        }

        private async void OnOptionsClicked(object sender, RoutedEventArgs e)
        {
            await ShowDialog(
                "Options",
                "Options disponibles prochainement.");
        }

        private async void OnAboutClicked(object sender, RoutedEventArgs e)
        {
            await ShowDialog(
                "À propos",
                "LatuCollect v0.3.0");
        }

        private void OnQuitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}