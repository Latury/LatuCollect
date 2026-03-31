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
║                                                                      ║
║  Dépendances :                                                       ║
║  - WinUI (Microsoft.UI.Xaml)                                         ║
║  - MainViewModel                                                     ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;

namespace LatuCollect.UI.WinUI
{
    public sealed partial class MainWindow : Window
    {
        // =========================
        // PROPRIÉTÉS
        // =========================
        public MainViewModel ViewModel { get; } = new();

        // =========================
        // CONSTRUCTEUR
        // =========================
        public MainWindow()
        {
            this.InitializeComponent();

            // WinUI 3 → DataContext sur Content
            if (this.Content is FrameworkElement root)
            {
                root.DataContext = ViewModel;
            }
        }
    }
}