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

using LatuCollect.Core.Services.Export;
using LatuCollect.Core.Simulation;
using LatuCollect.UI.WinUI.Settings.Panels;
using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;


namespace LatuCollect.UI.WinUI
{
    public sealed partial class MainWindow : Window
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS / SERVICES
        // ═════════════════════════════════════════════════════════════

        // ViewModel principal
        private readonly MainViewModel _viewModel = new();

        // Service export (⚠️ UI appelle → OK)
        private readonly FileExportService _exportService = new FileExportService();

        // Panel paramètres
        private SettingsPanel? _settingsPanel;


        // ═════════════════════════════════════════════════════════════
        // 2. WIN32 (TAILLE MIN)
        // ═════════════════════════════════════════════════════════════

        // ⚠️ Code bas niveau → isolé ici

        private const int WM_GETMINMAXINFO = 0x0024;

        private delegate IntPtr WndProcDelegate(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        private WndProcDelegate? _wndProc;
        private IntPtr _hwnd;
        private IntPtr _oldWndProc;


        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        // 🧑🏻‍💻 Description technique
        // - Initialise UI
        // - Attache ViewModel
        // - Centre fenêtre
        // - Définit taille minimale

        public MainWindow()
        {
            this.InitializeComponent();

            if (this.Content is FrameworkElement root)
                root.DataContext = _viewModel;

            _viewModel.ThemeChanged += ApplyTheme;

            // ⚠️ WIN32 / fenêtre
            var hwnd = WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            if (appWindow != null)
            {
                int minWidth = 1600;
                int minHeight = 1000;

                var displayArea = Microsoft.UI.Windowing.DisplayArea.GetFromWindowId(
                    windowId,
                    Microsoft.UI.Windowing.DisplayAreaFallback.Primary);

                int centerX = displayArea.WorkArea.Width / 2 - minWidth / 2;
                int centerY = displayArea.WorkArea.Height / 2 - minHeight / 2;

                appWindow.MoveAndResize(new Windows.Graphics.RectInt32(
                    centerX,
                    centerY,
                    minWidth,
                    minHeight
                ));

                SetupMinSize(minWidth, minHeight);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 3.1 GESTION DU THÈME
        // ═════════════════════════════════════════════════════════════

        // 🧑🏻‍💻 Description technique
        // Applique le thème Light / Dark sur toute l'UI

        private void ApplyTheme(string theme)
        {
            if (this.Content is FrameworkElement root)
            {
                root.RequestedTheme =
                    theme == "Light"
                    ? ElementTheme.Light
                    : ElementTheme.Dark;
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 4. WIN32 LOGIC
        // ═════════════════════════════════════════════════════════════

        // 🧑🏻‍💻 Description technique
        // Gestion taille minimale fenêtre (hook Windows)

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT { public int x; public int y; }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        private void SetupMinSize(int minWidth, int minHeight)
        {
            _hwnd = WindowNative.GetWindowHandle(this);

            _wndProc = new WndProcDelegate((hWnd, msg, wParam, lParam) =>
            {
                if (msg == WM_GETMINMAXINFO)
                {
                    var mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);

                    mmi.ptMinTrackSize.x = minWidth;
                    mmi.ptMinTrackSize.y = minHeight;

                    Marshal.StructureToPtr(mmi, lParam, true);
                }

                return CallWindowProc(_oldWndProc, hWnd, msg, wParam, lParam);
            });

            _oldWndProc = SetWindowLongPtr(_hwnd, -4, Marshal.GetFunctionPointerForDelegate(_wndProc));
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr newProc);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);


        // ═════════════════════════════════════════════════════════════
        // 5. ACTIONS UI PRINCIPALES
        // ═════════════════════════════════════════════════════════════

        // 📂 Chargement dossier
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
                await _viewModel.LoadTreeAsync(folder.Path);
            }
            else
            {
                await _viewModel.ShowFeedbackAsync("❌ Sélection annulée");
            }
        }

        // 🔍 Recherche
        private void OnSearchClicked(object sender, RoutedEventArgs e)
        {
            _viewModel.ToggleSearch();
        }

        // 📄 Format TXT
        private void OnTxtSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".txt";
        }

        // 📝 Format MD
        private void OnMdSelected(object sender, RoutedEventArgs e)
        {
            _viewModel.SelectedFormat = ".md";
        }

        // 🖱️ Click node
        private void OnNodeTapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            e.Handled = true;

            if (sender is FrameworkElement element &&
                element.DataContext is LatuCollect.UI.WinUI.Models.FileNode node)
            {
                _viewModel.HandleNodeClick(node);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 6. EXPORT / COPY
        // ═════════════════════════════════════════════════════════════

        // 📤 Export
        private async void OnExportClicked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_viewModel.SelectedFormat))
            {
                await _viewModel.ShowFeedbackAsync("✖ Sélectionne un format");
                return;
            }

            string content = await _viewModel.GetExportContentAsync();

            if (content.StartsWith("⚠"))
            {
                await _viewModel.ShowFeedbackAsync(content);
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
                await _viewModel.ShowFeedbackAsync("❌ Export annulé");
                return;
            }

            try
            {
                var result = _exportService.Export(file.Path, content);

                if (result.IsSuccess)
                {
                    await _viewModel.ShowFeedbackAsync("✔ Export réussi");
                }
                else
                {
                    await _viewModel.ShowFeedbackAsync("✖ " + result.Message);
                }
            }
            catch (Exception ex)
            {
                await _viewModel.ShowFeedbackAsync("✖ " + ex.Message);
            }
        }

        // 📋 Copier
        private async void OnCopyClicked(object sender, RoutedEventArgs e)
        {
            string content = await _viewModel.GetExportContentAsync();

            var package = new Windows.ApplicationModel.DataTransfer.DataPackage();
            package.SetText(content);

            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(package);

            await _viewModel.ShowFeedbackAsync("✔ Contenu copié");
        }


        // ═════════════════════════════════════════════════════════════
        // 7. LOGS UI
        // ═════════════════════════════════════════════════════════════

        // 🧑🏻‍💻 Description technique
        // - Affiche logs
        // - Filtrage
        // - Export / copy

        private async void OnLogsClicked(object sender, RoutedEventArgs e)
        {
            var vm = _viewModel;

            var root = this.Content as FrameworkElement;

            var template = Application.Current.Resources["LogItemTemplate"] as DataTemplate;

            var listView = new ListView
            {
                ItemsSource = vm.FilteredLogs,
                SelectionMode = ListViewSelectionMode.None,
                Height = 350,
                ItemTemplate = template
            };

            var filterPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10
            };

            var allBtn = new Button { Content = "Tout" };
            allBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.All;

            var infoBtn = new Button { Content = "Info" };
            infoBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.Info;

            var warnBtn = new Button { Content = "Warning" };
            warnBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.Warning;

            var errorBtn = new Button { Content = "Error" };
            errorBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.Error;

            filterPanel.Children.Add(allBtn);
            filterPanel.Children.Add(infoBtn);
            filterPanel.Children.Add(warnBtn);
            filterPanel.Children.Add(errorBtn);

            var copyButton = new Button { Content = "📋 Copier" };

            copyButton.Click += async (_, __) =>
            {
                string content = vm.GetLogsExportContent();

                if (string.IsNullOrWhiteSpace(content))
                {
                    await vm.ShowFeedbackAsync("⚠ Aucun log à copier");
                    return;
                }

                var package = new Windows.ApplicationModel.DataTransfer.DataPackage();
                package.SetText(content);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(package);

                await vm.ShowFeedbackAsync("✔ Logs copiés");
            };

            var exportButton = new Button { Content = "📤 Exporter" };

            exportButton.Click += async (_, __) =>
            {
                string content = vm.GetLogsExportContent();

                if (string.IsNullOrWhiteSpace(content))
                {
                    await vm.ShowFeedbackAsync("⚠ Aucun log à exporter");
                    return;
                }

                FileSavePicker picker = new();

                IntPtr hwnd = WindowNative.GetWindowHandle(this);
                InitializeWithWindow.Initialize(picker, hwnd);

                picker.SuggestedFileName = "logs";
                picker.FileTypeChoices.Add("Text", new[] { ".txt" });

                StorageFile file = await picker.PickSaveFileAsync();

                if (file == null)
                    return;

                await Windows.Storage.FileIO.WriteTextAsync(file, content);

                await vm.ShowFeedbackAsync("✔ Logs exportés");
            };

            var content = new StackPanel
            {
                Spacing = 10,
                Width = 900,
                Children =
        {
            new TextBlock
            {
                Text = "Logs en temps réel",
                FontSize = 16
            },
            filterPanel,
            listView,
        }
            };

            var dialog = new ContentDialog
            {
                Title = "🧾 Logs",
                Content = content,
                CloseButtonText = "Fermer",

                PrimaryButtonText = "📤 Exporter",
                SecondaryButtonText = "📋 Copier",

                XamlRoot = this.Content.XamlRoot
            };
            dialog.PrimaryButtonClick += async (_, __) =>
            {
                string content = vm.GetLogsExportContent();

                if (string.IsNullOrWhiteSpace(content))
                {
                    await vm.ShowFeedbackAsync("⚠ Aucun log à exporter");
                    return;
                }

                FileSavePicker picker = new();

                IntPtr hwnd = WindowNative.GetWindowHandle(this);
                InitializeWithWindow.Initialize(picker, hwnd);

                picker.SuggestedFileName = "logs";
                picker.FileTypeChoices.Add("Text", new[] { ".txt" });

                StorageFile file = await picker.PickSaveFileAsync();

                if (file == null)
                    return;

                await Windows.Storage.FileIO.WriteTextAsync(file, content);

                await vm.ShowFeedbackAsync("✔ Logs exportés");
            };

            dialog.SecondaryButtonClick += async (_, __) =>
            {
                string content = vm.GetLogsExportContent();

                if (string.IsNullOrWhiteSpace(content))
                {
                    await vm.ShowFeedbackAsync("⚠ Aucun log à copier");
                    return;
                }

                var package = new Windows.ApplicationModel.DataTransfer.DataPackage();
                package.SetText(content);
                Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(package);

                await vm.ShowFeedbackAsync("✔ Logs copiés");
            };

            await dialog.ShowAsync();
        }


        // ═════════════════════════════════════════════════════════════
        // 8. STATISTIQUES
        // ═════════════════════════════════════════════════════════════

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


        // ═════════════════════════════════════════════════════════════
        // 9. SIMULATION
        // ═════════════════════════════════════════════════════════════

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
                dialog.Hide();

                _viewModel.IsSimulationEnabled = toggle.IsOn;
                _viewModel.SelectedSimulationScenario = combo.SelectedItem?.ToString() ?? "Aucun";

                SimulationConfig.IsEnabled = _viewModel.IsSimulationEnabled;
                SimulationConfig.Scenario = _viewModel.SelectedSimulationScenario;
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 10. DIALOG HELPERS
        // ═════════════════════════════════════════════════════════════

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

        private async void ShowSelectAllDialog()
        {
            var dialog = new ContentDialog
            {
                Title = "Sélection globale désactivée",
                Content = "Pour éviter les ralentissements...",
                CloseButtonText = "Compris",
                XamlRoot = this.Content.XamlRoot
            };

            try { await dialog.ShowAsync(); }
            catch { }
        }

        // ═════════════════════════════════════════════════════════════
        // 11. NAVIGATION / MENUS
        // ═════════════════════════════════════════════════════════════

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

        private void OnOptionsClicked(object sender, RoutedEventArgs e)
        {
            if (_settingsPanel != null)
                return;

            _settingsPanel = new SettingsPanel();
            _settingsPanel.Initialize(_viewModel);

            _settingsPanel.OnCloseRequested += () =>
            {
                SettingsOverlay.Visibility = Visibility.Collapsed;
                SettingsContainer.Children.Clear();
                _settingsPanel = null;
            };

            SettingsContainer.Children.Clear();
            SettingsContainer.Children.Add(_settingsPanel);

            SettingsOverlay.Visibility = Visibility.Visible;
        }

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

        // ═════════════════════════════════════════════════════════════
        // 12. FERMETURE APP
        // ═════════════════════════════════════════════════════════════

        private async void OnQuitClicked(object sender, RoutedEventArgs e)
        {
            bool confirm = await ShowConfirm(
                "Quitter",
                "Voulez-vous vraiment quitter l'application ?");

            if (confirm)
            {
                SettingsOverlay.Visibility = Visibility.Collapsed;
                SettingsContainer.Children.Clear();
                _settingsPanel = null;
                Application.Current.Exit();
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 13. UTILITAIRES
        // ═════════════════════════════════════════════════════════════

        // 🧑🏻‍💻 Description technique
        // Format taille fichier

        private string FormatSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";

            double kb = bytes / 1024.0;
            if (kb < 1024) return $"{kb:F1} KB";

            double mb = kb / 1024.0;
            return $"{mb:F1} MB";
        }
    }
}