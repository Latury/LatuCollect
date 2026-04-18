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
using LatuCollect.UI.WinUI;

namespace LatuCollect.UI.WinUI
{
    public partial class App : Application
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════════════
        //
        // Initialise l’application WinUI
        //

        public App()
        {
            this.InitializeComponent();
        }


        // ═════════════════════════════════════════════════════════════════════
        // 2. DÉMARRAGE APPLICATION
        // ═════════════════════════════════════════════════════════════════════
        //
        // Point d’entrée après lancement
        // Crée et affiche la fenêtre principale
        //

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var window = new MainWindow();
            window.Activate();
        }
    }
}