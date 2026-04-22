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
using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;


// ═════════════════════════════════════════════════════════════
// 1. INITIALISATION & CONFIGURATION
// ═════════════════════════════════════════════════════════════
//
// Contient :
// - Initialisation de la fenêtre
// - Configuration de la taille minimale de la fenêtre
// - Association du ViewModel à la vue
// - Abonnement aux événements du ViewModel
// - Aucune logique métier, uniquement de l’orchestration UI
//
// Note : la logique métier est entièrement contenue dans le ViewModel et les services appelés par celui-ci.
// Cette séparation stricte garantit que la UI reste légère, réactive et facile à maintenir.
// Toute interaction utilisateur déclenche des appels au ViewModel, qui gère ensuite la logique métier et met à jour l’interface en conséquence.
// Cette approche respecte les principes de l’architecture ALC (Architecture LatuCollect) et du pattern MVVM, assurant une application robuste et évolutive.
// La UI est responsable de l’affichage et de l’orchestration, tandis que le ViewModel gère la logique métier et les données.
// Toute fonctionnalité liée à la manipulation de données, à la validation, à l’export ou à la gestion des erreurs doit être implémentée dans le ViewModel ou les services associés, jamais dans la code-behind de la fenêtre.
// Cette séparation claire permet de maintenir une architecture propre, facilite les tests unitaires et garantit que la UI reste réactive et facile à modifier sans risque d’introduire des bugs liés à la logique métier.
// En résumé : la MainWindow est le chef d’orchestre de l’interface, tandis que le ViewModel est le cerveau qui gère la logique métier. Toute interaction utilisateur doit passer par le ViewModel, et la MainWindow doit se contenter d’afficher les données et de réagir aux événements du ViewModel.

// Note : pour les dialogues complexes (logs, options, à propos), la logique métier doit rester dans le ViewModel, même si le contenu est défini dans la fenêtre. Par exemple, le filtrage des logs doit être géré par le ViewModel, et la fenêtre doit simplement afficher les logs filtrés et envoyer les commandes de filtrage au ViewModel.
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
        
        // ─────────────────────────────────────────────
        // 🧱 WIN32 - BLOQUER TAILLE MIN (ZERO FLICKER)
        // ─────────────────────────────────────────────

        private const int WM_GETMINMAXINFO = 0x0024;

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        private delegate IntPtr WndProcDelegate(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        private WndProcDelegate? _wndProc;
        private IntPtr _hwnd;

        // Configure la taille minimale de la fenêtre en utilisant une procédure Windows personnalisée (anti-flicker)
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

        private IntPtr _oldWndProc;

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr newProc);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);


        // ═════════════════════════════════════════════════════════════
        // 2. CHAMPS PRIVÉS / SERVICES
        // ═════════════════════════════════════════════════════════════
        //
        // Contient :
        // - Instance du ViewModel
        // - Services utilisés par la fenêtre (ex: export)
        // - Aucune logique métier, uniquement des références aux composants nécessaires à l’affichage et à l’orchestration

        // Instance du ViewModel (le cerveau de l’application, gère la logique métier et les données)
        private readonly MainViewModel _viewModel = new();

        // Service d’export de fichiers (utilisé pour exporter le contenu dans un fichier choisi par l’utilisateur)
        private readonly FileExportService _exportService = new FileExportService();

        // ═════════════════════════════════════════════════════════════
        // 2. POPUPS UI (DIALOGUES)
        // ═════════════════════════════════════════════════════════════
        // Contient :
        // - Dialogues d’information
        // - Dialogues de confirmation

        // Affiche un dialogue d’information lorsque la sélection globale est bloquée (UI uniquement, appelé par le ViewModel via l’événement OnSelectAllBlocked)
        private async void ShowSelectAllDialog()
        {
            var dialog = new ContentDialog
            {
                Title = "Sélection globale désactivée",
                Content = "Pour éviter les ralentissements, la sélection de tous les fichiers est temporairement désactivée.\n\nVeuillez sélectionner les fichiers manuellement.",
                CloseButtonText = "Compris",
                XamlRoot = this.Content.XamlRoot
            };

            try
            {
                await dialog.ShowAsync();
            }
            catch
            {
                // évite crash si déjà ouvert
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 4. ACTIONS PRINCIPALES UTILISATEUR
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


                await _viewModel.LoadTreeAsync(folder.Path);
            }
            else
            {
                await _viewModel.ShowFeedbackAsync("❌ Sélection annulée");
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
                await _viewModel.ShowFeedbackAsync("✖ Sélectionne un format");
                return;
            }

            string content = _viewModel.GetExportContent();

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

        // Copie le contenu dans le presse-papiers
        private void OnCopyClicked(object sender, RoutedEventArgs e)
        {
            string content = _viewModel.GetExportContent();

            var package = new Windows.ApplicationModel.DataTransfer.DataPackage();
            package.SetText(content);

            Windows.ApplicationModel.DataTransfer.Clipboard.SetContent(package);

            _ = _viewModel.ShowFeedbackAsync("✔ Contenu copié");
        }

        // ─────────────────────────────────────────────
        // 🧾 LOGS
        // ─────────────────────────────────────────────

        private async void OnLogsClicked(object sender, RoutedEventArgs e)
        {
            var vm = _viewModel;

            // ─────────────────────────────────────────────
            // 🧾 LISTE LOGS
            // ─────────────────────────────────────────────

            var listView = new ListView
            {
                ItemsSource = vm.FilteredLogs,
                SelectionMode = ListViewSelectionMode.None,
                Height = 400,
                ItemTemplate = (DataTemplate)((FrameworkElement)this.Content).Resources["LogItemTemplate"]
            };

            // ─────────────────────────────────────────────
            // 📦 CONTENU
            // ─────────────────────────────────────────────

            // Conteneur principal
            var stack = new StackPanel { Spacing = 10 };

            // Titre
            stack.Children.Add(new TextBlock
            {
                Text = "Logs en temps réel",
                FontSize = 16
            });

            // Filtres
            var filterPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Spacing = 10
            };

            // Boutons de filtre (UI uniquement, modifie la propriété SelectedLogFilter du ViewModel)
            var allBtn = new Button { Content = "Tout" };
            allBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.All;

            // Style simple pour indiquer le filtre actif
            var infoBtn = new Button { Content = "Info" };
            infoBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.Info;

            // (Le style de chaque bouton pourrait être amélioré pour refléter l’état actif, par exemple en changeant la couleur de fond)
            var warnBtn = new Button { Content = "Warning" };
            warnBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.Warning;

            // (Le style de chaque bouton pourrait être amélioré pour refléter l’état actif, par exemple en changeant la couleur de fond)
            var errorBtn = new Button { Content = "Error" };
            errorBtn.Click += (_, __) => vm.SelectedLogFilter = MainViewModel.LogFilter.Error;

            filterPanel.Children.Add(allBtn);
            filterPanel.Children.Add(infoBtn);
            filterPanel.Children.Add(warnBtn);
            filterPanel.Children.Add(errorBtn);

            stack.Children.Add(filterPanel);

            // Liste logs
            stack.Children.Add(listView);

            // Export logs (UI uniquement, n’exporte rien pour l’instant)
            var exportButton = new Button
            {
                Content = "📤 Exporter les logs",
                HorizontalAlignment = HorizontalAlignment.Right
            };

            stack.Children.Add(exportButton);

            // Action export logs
            exportButton.Click += async (_, __) =>
            {
                try
                {
                    if (!vm.FilteredLogs.Any())
                    {
                        await _viewModel.ShowFeedbackAsync("⚠ Aucun log à exporter");
                        return;
                    }

                    var lines = vm.FilteredLogs.Select(log =>
                        $"[{log.Date}] [{log.Level}] {log.Message} {(string.IsNullOrEmpty(log.Context) ? "" : $"({log.Context})")}"
                    );

                    string content = string.Join(
                        Environment.NewLine + "----------------------------------------" + Environment.NewLine,
                        lines
                    );

                    FileSavePicker picker = new();

                    IntPtr hwnd = WindowNative.GetWindowHandle(this);
                    InitializeWithWindow.Initialize(picker, hwnd);

                    picker.SuggestedFileName = "logs";
                    picker.FileTypeChoices.Add("Text", new[] { ".txt" });

                    StorageFile file = await picker.PickSaveFileAsync();

                    if (file == null)
                        return;

                    await Windows.Storage.FileIO.WriteTextAsync(file, content);

                    await _viewModel.ShowFeedbackAsync("✔ Logs exportés");
                }
                catch (Exception ex)
                {
                    await _viewModel.ShowFeedbackAsync("✖ Erreur export logs : " + ex.Message);
                }
            };

            // ─────────────────────────────────────────────
            // 🪟 DIALOG
            // ─────────────────────────────────────────────

            var dialog = new ContentDialog
            {
                Title = "🧾 Logs",
                Content = new Grid
                {
                    Width = 900,
                    MaxHeight = 600,
                    Children = { stack }
                },
                CloseButtonText = "Fermer",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        // ═════════════════════════════════════════════════════════════
        // 5. STATISTIQUES (UI)
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
        // 6. SIMULATION (UI)
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
                dialog.Hide();

                _viewModel.IsSimulationEnabled = toggle.IsOn;
                _viewModel.SelectedSimulationScenario = combo.SelectedItem?.ToString() ?? "Aucun";

                SimulationConfig.IsEnabled = _viewModel.IsSimulationEnabled;
                SimulationConfig.Scenario = _viewModel.SelectedSimulationScenario;
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 7. DIALOGS GÉNÉRIQUES
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
        // 8. FERMETURE APPLICATION
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
        // 9. MENUS / AIDE / OPTIONS / À PROPOS
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
            var stack = new StackPanel { Spacing = 12 };

            ContentDialog dialog = new ContentDialog();

            stack.Children.Add(new TextBlock
            {
                Text = "Paramètres",
                FontSize = 18
            });

            var exclusionsButton = new Button
            {
                Content = "📁 Dossiers exclus"
            };

            exclusionsButton.Click += async (_, __) =>
            {
                dialog.Hide();
                await Task.Delay(50);
                ShowExclusionsDialog();
            };

            var devToggle = new ToggleSwitch
            {
                Header = "🧑🏻‍💻 Mode développeur",
                IsOn = _viewModel.IsDeveloperMode,
                OnContent = "Activé",
                OffContent = "Désactivé"
            };

            devToggle.Toggled += (_, __) =>
{
    _viewModel.IsDeveloperMode = devToggle.IsOn;
};

            // ✅ AJOUT UNE SEULE FOIS
            stack.Children.Add(exclusionsButton);
            stack.Children.Add(devToggle);

            dialog.Title = "⚙ Paramètres";
            dialog.Content = stack;
            dialog.CloseButtonText = "Fermer";
            dialog.XamlRoot = this.Content.XamlRoot;

            await dialog.ShowAsync();
        }

        // Affiche un dialogue pour gérer les dossiers exclus (UI uniquement, modifie la configuration globale AppConfig.ExcludedFolders)
        private async void ShowExclusionsDialog()
        {
            var listView = new ListView
            {
                ItemsSource = _viewModel.Config.ExcludedFolders,
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

            addButton.Click += (_, __) =>
            {
                var value = input.Text?.Trim();

                if (!string.IsNullOrWhiteSpace(value) &&
                    !_viewModel.Config.ExcludedFolders.Contains(value))
                {
                    _viewModel.Config.ExcludedFolders.Add(value);

                    listView.ItemsSource = null;
                    listView.ItemsSource = _viewModel.Config.ExcludedFolders;

                    input.Text = "";
                }
            };

            removeButton.Click += (_, __) =>
            {
                if (listView.SelectedItem is string selected)
                {
                    _viewModel.Config.ExcludedFolders.Remove(selected);

                    listView.ItemsSource = null;
                    listView.ItemsSource = _viewModel.Config.ExcludedFolders;
                }
            };

            var stack = new StackPanel { Spacing = 10 };

            stack.Children.Add(new TextBlock
            {
                Text = "Dossiers exclus",
                FontSize = 16
            });

            var scroll = new ScrollViewer
            {
                Content = listView,
                MaxHeight = 250
            };

            stack.Children.Add(scroll);
            stack.Children.Add(input);
            stack.Children.Add(addButton);
            stack.Children.Add(removeButton);

            var dialog = new ContentDialog
            {
                Title = "📁 Dossiers exclus",
                Content = new Grid
                {
                    Width = 500,
                    Children = { stack }
                },
                CloseButtonText = "Fermer",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
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