/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI                                                         ║
║  Fichier : MainWindow.xaml.cs                                        ║
║                                                                      ║
║  Rôle :                                                              ║
║  Fenêtre principale de l’application                                 ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Initialiser l’interface                                           ║
║  - Associer le ViewModel à la vue                                    ║
║  - Gérer la sélection d’un dossier utilisateur                       ║
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
using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace LatuCollect.UI.WinUI
{
    public sealed partial class MainWindow : Window
    {
        // =========================
        // CHAMPS PRIVÉS
        // =========================

        private readonly MainViewModel _viewModel = new();

        private IntPtr _oldWndProc = IntPtr.Zero;
        // évite le freeze
        private WndProcDelegate _wndProcDelegate;
        // Permet d'exécuter le code de démarrage une seule fois
        private bool _isInitialized = false;
        
        // =========================
        // CONSTRUCTEUR
        // =========================

        public MainWindow()
        {
            this.InitializeComponent();

            if (this.Content is FrameworkElement root)
            {
                root.DataContext = _viewModel;
            }

            // =========================
            // FERMETURE APPLICATION
            // =========================
            // 👉 évite process fantôme

            this.Closed += OnWindowClosed;

            // =========================
            // TAILLE FENÊTRE AU DÉMARRAGE
            // =========================

            this.Activated += (sender, args) =>
            {
                if (_isInitialized)
                    return;

                _isInitialized = true;

                IntPtr hwnd = WindowNative.GetWindowHandle(this);
                WindowId windowId = Win32Interop.GetWindowIdFromWindow(hwnd);
                AppWindow appWindow = AppWindow.GetFromWindowId(windowId);

                appWindow.Resize(new SizeInt32(1200, 800));
            };

            // =========================
            // BLOQUER TAILLE MINIMUM
            // =========================

            IntPtr hwndHandle = WindowNative.GetWindowHandle(this);

            _wndProcDelegate = WndProc;
            _oldWndProc = SetWindowLongPtr(hwndHandle, -4, _wndProcDelegate);
        }

        // =========================
        // GESTION TAILLE MINIMUM
        // =========================

        private IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == WM_GETMINMAXINFO)
            {
                MINMAXINFO mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);

                mmi.ptMinTrackSize.X = 1200;
                mmi.ptMinTrackSize.Y = 800;

                Marshal.StructureToPtr(mmi, lParam, true);
            }

            return CallWindowProc(_oldWndProc, hWnd, msg, wParam, lParam);
        }

        // =========================
        // STRUCTURES WIN32
        // =========================

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        private const int WM_GETMINMAXINFO = 0x0024;

        // =========================
        // IMPORTS NATIFS
        // =========================

        private delegate IntPtr WndProcDelegate(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtrW", SetLastError = true)]
        private static extern IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, WndProcDelegate newProc);

        [DllImport("user32.dll")]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        // =========================
        // EVENEMENTS UI
        // =========================

        // Gère la sélection d’un dossier, ouvre un dialogue de sélection et charge l’arborescence du dossier choisi
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

        // Gère l'export du contenu, propose un dialogue de sauvegarde et affiche une confirmation après export
        private async void OnExportClicked(object sender, RoutedEventArgs e)
        {
            FileSavePicker picker = new();

            IntPtr hwnd = WindowNative.GetWindowHandle(this);
            InitializeWithWindow.Initialize(picker, hwnd);

            bool isMd = _viewModel.IsMarkdownSelected;

            string extension = isMd ? ".md" : ".txt";
            string formatName = isMd ? "Markdown" : "Text";

            picker.SuggestedFileName = "export";
            picker.FileTypeChoices.Add(formatName, new[] { extension });

            StorageFile file = await picker.PickSaveFileAsync();

            if (file != null)
            {
                string content = _viewModel.BuildExportContent();

                FileExportService.Export(file.Path, content);

                // Popup après export
                ContentDialog dialog = new()
                {
                    Title = "Export réussi",
                    Content = $"Fichier exporté en {extension}",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };

                await dialog.ShowAsync();
            }
        }


        // Copie le contenu dans le presse-papiers
        private void OnCopyClicked(object sender, RoutedEventArgs e)
        {
            string content = _viewModel.GetExportContent();

            DataPackage package = new();
            package.SetText(content);

            Clipboard.SetContent(package);

            ShowCopiedMessage();
        }

        // Popup de confirmation après copie dans le presse-papiers
        private async void ShowCopiedMessage()
        {
            ContentDialog dialog = new()
            {
                Title = "Copié",
                Content = "Le contenu a été copié dans le presse-papiers.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }
        
        // Affiche un dialogue d’options, permet de configurer des paramètres
        private async void OnOptionsClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new()
            {
                Title = "Options",
                Content = new StackPanel
                {
                    Children =
            {
                new TextBlock { Text = "Paramètres de l'application" },
                new CheckBox { Content = "Activer option exemple" }
            }
                },
                CloseButtonText = "Fermer",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }
        
        // Affiche une aide contextuelle, guide l’utilisateur sur les étapes à suivre
        private async void OnHelpClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new()
            {
                Title = "Aide",
                Content = "Sélectionnez des fichiers puis exportez.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        // Affiche les informations sur l’application, version, auteur, etc.
        private async void OnAboutClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new()
            {
                Title = "À propos",
                Content = "LatuCollect v1.0\nApplication de collecte de fichiers.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };

            await dialog.ShowAsync();
        }

        // Popup de confirmation avant de quitter, évite les fermetures accidentelles
        private async void OnQuitClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new()
            {
                Title = "Quitter",
                Content = "Voulez-vous vraiment quitter l'application ?",
                PrimaryButtonText = "Oui",
                CloseButtonText = "Non",
                XamlRoot = this.Content.XamlRoot
            };

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                Application.Current.Exit();
            }
        }

        // =========================
        // FERMETURE PROPRE
        // =========================

        private void OnWindowClosed(object sender, WindowEventArgs args)
        {
            Environment.Exit(0);
        }
    }
}