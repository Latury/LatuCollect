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
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;
using LatuCollect.Core.Configuration;
using System.Collections.Generic;

namespace LatuCollect.UI.WinUI
{
    public sealed partial class MainWindow : Window
    {
        // ===========================================================
        // 🧠 VIEWMODEL (LIEN AVEC LA VUE, PAS DE LOGIQUE MÉTIER ICI)
        // ===========================================================

        // Instance du ViewModel principal, qui contient l’état de l’application et la logique métier (appelée depuis la vue)
        private readonly MainViewModel _viewModel = new();

        // ==========================================================
        // 🚀 INITIALISATION DE LA FENÊTRE ET LIEN AVEC LE VIEWMODEL
        // ==========================================================

        // Constructeur de la fenêtre principale : initialise les composants, lie le DataContext au ViewModel et configure la taille minimale de la fenêtre
        public MainWindow()
        {
            this.InitializeComponent();

            if (this.Content is FrameworkElement root)
                root.DataContext = _viewModel;

            // 🔥 Taille minimale fenêtre (WinUI 3)
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            if (appWindow != null)
            {
                appWindow.Resize(new Windows.Graphics.SizeInt32(1400, 850));
            }
        }

        // ======================================================
        // 📂 SÉLECTION DE DOSSIER (FOLDERPICKER)
        // ======================================================

        // Ouvre un FolderPicker pour que l’utilisateur sélectionne un dossier, puis charge l’arborescence dans le ViewModel
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
            else
            {
                _viewModel.ShowFeedback("❌ Sélection annulée");
            }
        }

        // Affiche ou masque la barre de recherche dans l’arborescence
        private void OnSearchClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleSearch();
        }

        // ======================================================
        // 📄 FORMAT D'EXPORT (TXT, MD)
        // ======================================================

        // Lorsque l’utilisateur sélectionne le format Texte, on met à jour le ViewModel pour refléter ce choix
        private void OnTxtSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".txt";
        }

        // Lorsque l’utilisateur sélectionne le format Markdown, on met à jour le ViewModel pour refléter ce choix
        private void OnMdSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".md";
        }

        // ======================================================
        // 📤 EXPORTER (FICHIER OU PRESSE-PAPIERS)   
        // ======================================================

        // Exporte le contenu dans un fichier choisi par l’utilisateur et affiche un feedback
        private async void OnExportClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.SelectedFormat))
            {
                _viewModel.ShowFeedback("✖ Sélectionne un format");
                return;
            }

            string content = _viewModel.GetExportContent();

            if (content.StartsWith("⚠"))
            {
                _viewModel.ShowFeedback(content);
                return;
            }

            var check = _viewModel.CheckExportState();

            if (check == MainViewModel.ExportCheckResult.NoSelection)
            {
                await ShowDialog("Aucun contenu", "Aucun fichier sélectionné.");
                return;
            }

            if (check == MainViewModel.ExportCheckResult.EmptyFiles)
            {
                bool confirm = await ShowConfirm(
                    "Fichiers vides",
                    "Certains fichiers sont vides.\n\nVoulez-vous continuer l’export ?");

                if (!confirm)
                    return;
            }

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
            {
                _viewModel.ShowFeedback("❌ Export annulé");
                return;
            }

            try
            {
                var result = FileExportService.Export(file.Path, content);

                if (result.IsSuccess)
                {
                    _viewModel.ShowFeedback("✔ Export réussi");
                }
                else
                {
                    _viewModel.ShowFeedback("✖ " + result.Message);
                }
            }
            catch (Exception ex)
            {
                _viewModel.ShowFeedback("✖ " + ex.Message);
            }
        }

        // ======================================================
        // 📋 COPIER DANS LE PRESSE-PAPIERS  
        // ======================================================

        // Copie le contenu de l’export dans le presse-papiers et affiche un feedback
        private void OnCopyClicked(object sender, RoutedEventArgs e)
        {
            string content = _viewModel.GetExportContent();

            var package = new Windows.ApplicationModel.DataTransfer.DataPackage();
            package.SetText(content);

            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(package);

            _viewModel.ShowFeedback("✔ Contenu copié");
        }

        // =========================================================
        // 🧪 SIMULATION (UI UNIQUEMENT, AUCUNE LOGIQUE MÉTIER ICI)
        // =========================================================

        // Affiche une boîte de dialogue pour configurer la simulation de scénarios (UI uniquement)
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
                    "CheminsLongs",
                    "UI_Loader",
                    "UI_Error",
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
        // 💬 DIALOGS (UI UNIQUEMENT)
        // ======================================================

        // Affiche un message dans une boîte de dialogue avec un titre et un contenu (scrollable si nécessaire)
        private async Task ShowDialog(string title, string message)
        {
            var content = new ScrollViewer
            {
                MaxHeight = 420,
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                Content = new TextBlock
                {
                    Text = message,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 15,
                    LineHeight = 22,
                    Margin = new Thickness(10)
                }
            };

            ContentDialog dialog = new()
            {
                Title = title,
                Content = content,
                CloseButtonText = "OK",
                DefaultButton = ContentDialogButton.Close,
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        // Affiche une boîte de dialogue de confirmation avec un titre, un message et des boutons Oui/Non
        private async Task<bool> ShowConfirm(string title, string message)
        {
            ContentDialog dialog = new()
            {
                Title = title,
                Content = new TextBlock
                {
                    Text = message,
                    TextWrapping = TextWrapping.Wrap,
                    FontSize = 15,
                    Margin = new Thickness(10)
                },
                PrimaryButtonText = "Oui",
                CloseButtonText = "Non",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = this.Content.XamlRoot
            };

            return await dialog.ShowAsync() == ContentDialogResult.Primary;
        }

        // ======================================================
        // 🚪 QUITTER L'APPLICATION (CONFIRMATION AVANT QUIT)
        // ======================================================

        private async void OnQuitClicked(object sender, RoutedEventArgs e)
        {
            bool confirm = await ShowConfirm(
                "Quitter",
                "Voulez-vous vraiment quitter l'application ?");

            if (confirm)
            {
                Application.Current.Exit();
            }
        }

        // ======================================================
        // 🧰 AUTRES (AIDE, OPTIONS, À PROPOS - UI UNIQUEMENT)
        // ======================================================

        // Aide : guide d’utilisation
        private async void OnHelpClicked(object sender, RoutedEventArgs e)
        {
            await ShowDialog(
    "Aide",
    "📚 GUIDE COMPLET — LATUCOLLECT\n\n" +

    "🚀 DÉMARRAGE RAPIDE\n\n" +

    "1️⃣ Charger un dossier\n" +
    "Clique sur 📂 puis sélectionne ton projet.\n\n" +

    "2️⃣ Explorer les fichiers\n" +
    "Utilise l’arborescence à gauche pour naviguer.\n\n" +

    "3️⃣ Sélectionner des fichiers\n" +
    "Coche les fichiers à inclure.\n\n" +

    "4️⃣ Vérifier l’aperçu\n" +
    "Le contenu apparaît à droite.\n\n" +

    "5️⃣ Choisir un format\n" +
    "Sélectionne .txt ou .md.\n\n" +

    "6️⃣ Exporter ou copier\n" +
    "📤 Exporter → crée un fichier\n" +
    "📋 Copier → copie le contenu\n\n" +

    "🧠 COMMENT ÇA MARCHE\n\n" +
    "LatuCollect assemble le contenu des fichiers sélectionnés.\n" +
    "Aucun fichier n’est modifié.\n\n" +

    "⚠️ À SAVOIR\n\n" +
    "- Aucun fichier sélectionné → rien ne s’affiche\n" +
    "- L’aperçu = le résultat exporté\n\n" +

    "💡 CONSEILS\n\n" +
    "- Vérifie l’aperçu\n" +
    "- Sélectionne uniquement l’essentiel\n" +
    "- Utilise .md pour structurer");
        }

        // Options : gestion des dossiers exclus (UI uniquement, modifie la configuration globale AppConfig.ExcludedFolders) 
        private async void OnOptionsClicked(object sender, RoutedEventArgs e)
        {
            var listView = new ListView
            {
                ItemsSource = AppConfig.ExcludedFolders,
                Height = 200
            };

            var input = new TextBox
            {
                PlaceholderText = "Nom du dossier (ex: node_modules)"
            };

            var addButton = new Button
            {
                Content = "Ajouter"
            };

            var removeButton = new Button
            {
                Content = "Supprimer sélection"
            };

            // ➕ Ajouter
            addButton.Click += (_, __) =>
            {
                var value = input.Text?.Trim();

                if (!string.IsNullOrWhiteSpace(value) &&
                    !AppConfig.ExcludedFolders.Contains(value))
                {
                    AppConfig.ExcludedFolders.Add(value);

                    listView.ItemsSource = null;
                    listView.ItemsSource = AppConfig.ExcludedFolders;

                    input.Text = "";
                }
            };

            // ➖ Supprimer
            removeButton.Click += (_, __) =>
            {
                if (listView.SelectedItem is string selected)
                {
                    AppConfig.ExcludedFolders.Remove(selected);

                    listView.ItemsSource = null;
                    listView.ItemsSource = AppConfig.ExcludedFolders;
                }
            };

            var stack = new StackPanel { Spacing = 10 };

            stack.Children.Add(new TextBlock
            {
                Text = "Dossiers exclus",
                FontSize = 16
            });

            stack.Children.Add(listView);
            stack.Children.Add(input);
            stack.Children.Add(addButton);
            stack.Children.Add(removeButton);

            var dialog = new ContentDialog
            {
                Title = "Options - Exclusions",
                Content = stack,
                PrimaryButtonText = "Fermer",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();

            // 🔄 Recharge l’arbre après fermeture
            if (!string.IsNullOrWhiteSpace(_viewModel.CurrentFolderPath))
            {
                _viewModel.LoadTree(_viewModel.CurrentFolderPath);
            }
        }

        // A propos : informations sur l'application, le développeur, la licence, etc. (UI uniquement) 
        private async void OnAboutClicked(object sender, RoutedEventArgs e)
        {
            await ShowDialog(
    "À propos",
    "ℹ LATUCOLLECT\n\n" +

    "Version : 0.5.0\n" +
    "Créé en : Le 31 Mars 2026\n\n" +

    "🧩 PRÉSENTATION\n\n" +
    "LatuCollect permet de collecter et assembler le contenu de plusieurs fichiers.\n" +
    "C’est un copieur intelligent, pas un analyseur.\n\n" +

    "⚙ FONCTIONNALITÉS\n\n" +
    "✔ Navigation dans un projet\n" +
    "✔ Sélection de fichiers\n" +
    "✔ Aperçu en temps réel\n" +
    "✔ Export (.txt / .md)\n" +
    "✔ Copie du contenu\n\n" +

    "🏗 ARCHITECTURE\n\n" +
    "- MVVM\n" +
    "- Architecture ALC stricte\n" +
    "- Séparation UI / logique métier\n\n" +

    "🔒 GARANTIES\n\n" +
    "✔ Lecture seule\n" +
    "✔ Aucun fichier modifié\n" +
    "✔ Aperçu = export\n\n" +

    "👨‍💻 DÉVELOPPEUR\n\n" +
    "Flo Latury\n\n" +

    "🌐 GITHUB\n\n" +
    "https://github.com/Latury\n\n" +

    "📜 LICENCE\n\n" +
    "MIT");
        }
    }
}