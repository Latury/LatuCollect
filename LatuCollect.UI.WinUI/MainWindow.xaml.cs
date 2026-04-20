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

using LatuCollect.Core.Configuration;
using LatuCollect.Core.Services.Export;
using LatuCollect.Core.Simulation;
using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;


// ═════════════════════════════════════════════════════════════
// 0. STRUCTURE GLOBALE DU FICHIER
// ═════════════════════════════════════════════════════════════
//
// Contient :
// 1. Champs privés / Services
// 2. Popups UI (Dialogues)
// 3. Actions principales utilisateur
// 4. Statistiques (UI)
// 5. Simulation (UI)
// 6. Dialogs génériques
// 7. Fermeture application
// 8. Menus / Aide / Options / À propos

namespace LatuCollect.UI.WinUI
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            if (this.Content is FrameworkElement root)
                root.DataContext = _viewModel;

            _viewModel.OnSelectAllBlocked += ShowSelectAllDialog;

            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            if (appWindow != null)
            {
                appWindow.Resize(new Windows.Graphics.SizeInt32(1400, 850));
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS / SERVICES
        // ═════════════════════════════════════════════════════════════
        //
        // Contient :
        // - ViewModel
        // - Services utilisés uniquement côté UI
        //

        private readonly MainViewModel _viewModel = new();
        private readonly FileExportService _exportService = new FileExportService();

        // ═════════════════════════════════════════════════════════════
        // 2. POPUPS UI (DIALOGUES)
        // ═════════════════════════════════════════════════════════════
        //
        // Dialogues affichés à l’utilisateur
        // (aucune logique métier)
        //

        private async void ShowSelectAllDialog()
        {
            var dialog = new ContentDialog
            {
                Title = "Sélection globale désactivée",
                Content = "Pour éviter les ralentissements, la sélection de tous les fichiers est temporairement désactivée.\n\nVeuillez sélectionner les fichiers manuellement.",
                CloseButtonText = "Compris",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        // ═════════════════════════════════════════════════════════════
        // 3. ACTIONS PRINCIPALES UTILISATEUR
        // ═════════════════════════════════════════════════════════════
        //
        // Boutons principaux :
        // - Ouvrir un dossier
        // - Toggle barre de recherche
        // - Sélection format export
        // - Exporter
        // - Copier
        // - Logs
        //

        // Ouvre un dialogue de sélection de dossier et charge l’arborescence
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

        // Toggle de la barre de recherche (UI uniquement, le filtrage est géré par le ViewModel)
        private void OnSearchClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleSearch();
        }

        // Sélection du format Texte
        private void OnTxtSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".txt";
        }

        // Sélection du format Markdown
        private void OnMdSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".md";
        }

        // Export le contenu dans un fichier choisi par l’utilisateur
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
                var result = _exportService.Export(file.Path, content);

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

        // Copie le contenu dans le presse-papiers
        private void OnCopyClicked(object sender, RoutedEventArgs e)
        {
            string content = _viewModel.GetExportContent();

            var package = new Windows.ApplicationModel.DataTransfer.DataPackage();
            package.SetText(content);

            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(package);

            _viewModel.ShowFeedback("✔ Contenu copié");
        }

        // ─────────────────────────────────────────────
        // 🧾 LOGS
        // ─────────────────────────────────────────────

        private async void OnLogsClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Logs",
                Content = "Affichage des logs à venir...",
                CloseButtonText = "Fermer",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        // ═════════════════════════════════════════════════════════════
        // 4. STATISTIQUES (UI)
        // ═════════════════════════════════════════════════════════════
        //
        // Affichage uniquement (les données viennent du ViewModel)
        //

        private async void OnStatsClicked(object sender, RoutedEventArgs e)
        {
            var content = new StackPanel { Spacing = 10 };

            content.Children.Add(new TextBlock
            {
                Text = "📊 Statistiques",
                FontSize = 18
            });

            content.Children.Add(new TextBlock
            {
                Text = $"📄 Fichiers : {_viewModel.FileCount}"
            });

            content.Children.Add(new TextBlock
            {
                Text = $"📏 Lignes : {_viewModel.TotalLines:N0}"
            });

            content.Children.Add(new TextBlock
            {
                Text = $"🔤 Caractères : {_viewModel.TotalCharacters:N0}"
            });

            content.Children.Add(new TextBlock
            {
                Text = $"💾 Taille : {FormatSize(_viewModel.TotalSize)}"
            });

            content.Children.Add(new TextBlock
            {
                Text = "────────────────────────────"
            });

            content.Children.Add(new TextBlock
            {
                Text = "✔ Données mises à jour en temps réel",
                FontStyle = Windows.UI.Text.FontStyle.Italic,
                Opacity = 0.7
            });

            var dialog = new ContentDialog
            {
                Title = "Statistiques",
                Content = content,
                CloseButtonText = "Fermer",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }


        private string FormatSize(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes} B";

            double kb = bytes / 1024.0;

            if (kb < 1024)
                return $"{kb:F1} KB";

            double mb = kb / 1024.0;

            return $"{mb:F1} MB";
        }


        // ═════════════════════════════════════════════════════════════
        // 5. SIMULATION (UI)
        // ═════════════════════════════════════════════════════════════
        //
        // Configuration des scénarios de simulation
        // (interaction UI uniquement)
        //

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

        // ═════════════════════════════════════════════════════════════
        // 6. DIALOGS GÉNÉRIQUES
        // ═════════════════════════════════════════════════════════════
        //
        // Helpers UI pour afficher des messages
        //

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


        // ═════════════════════════════════════════════════════════════
        // 7. FERMETURE APPLICATION
        // ═════════════════════════════════════════════════════════════
        //
        // Gestion de la fermeture avec confirmation
        //

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

        // ═════════════════════════════════════════════════════════════
        // 8. MENUS / AIDE / OPTIONS / À PROPOS
        // ═════════════════════════════════════════════════════════════
        //
        // Écrans secondaires de l’application
        //

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