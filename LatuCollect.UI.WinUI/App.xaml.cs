/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI                                                         ║
║  Fichier : App.xaml.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Point d’entrée de l’application WinUI                               ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Initialiser l’application                                         ║
║  - Gérer le cycle de vie                                             ║
║  - Créer la fenêtre principale                                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Gestion globale uniquement                                        ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Microsoft.UI.Xaml                                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml;

namespace LatuCollect.UI.WinUI
{
    public partial class App : Application
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════
        //
        // Fenêtre principale de l’application
        //

        private Window? _mainWindow;


        // ═════════════════════════════════════════════════════════════
        // 2. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public App()
        {
            InitializeComponent();
        }


        // ═════════════════════════════════════════════════════════════
        // 3. DÉMARRAGE APPLICATION
        // ═════════════════════════════════════════════════════════════
        //
        // Point d’entrée après lancement
        // Crée et affiche la fenêtre principale
        //

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            _mainWindow = CreateMainWindow();
            _mainWindow.Activate();
        }


        // ═════════════════════════════════════════════════════════════
        // 4. CRÉATION FENÊTRE PRINCIPALE
        // ═════════════════════════════════════════════════════════════
        //
        // Centralise la création de la fenêtre
        //

        private Window CreateMainWindow()
        {
            return new MainWindow();
        }
    }
}