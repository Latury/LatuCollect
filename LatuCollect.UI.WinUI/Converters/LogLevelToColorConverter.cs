/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : LogLevelToColorConverter.cs                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertit un niveau de log (Info, Warning, Error) en couleur UI     ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Associer une couleur à chaque niveau de log                       ║
║  - Améliorer la lisibilité des logs dans l’interface                 ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Conversion UI uniquement                                          ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Microsoft.UI.Xaml                                                 ║
║  - LogLevel (Core.Logging.Models)                                    ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI;
using System;
using LatuCollect.Core.Logging.Models;

namespace LatuCollect.UI.WinUI.Converters
{
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Convertit un LogLevel en couleur UI :
    // Info    → gris clair
    // Warning → orange
    // Error   → rouge
    //
    // Optimisation :
    // - Brushes statiques (évite allocations répétées)


    public class LogLevelToColorConverter : IValueConverter
    {
        // ==========================================
        // 🔒 CHAMPS PRIVÉS
        // ==========================================
        // Brushes pré-créés pour performance

        private static readonly SolidColorBrush InfoBrush = new(Colors.LightGray);
        private static readonly SolidColorBrush WarningBrush = new(Colors.Orange);
        private static readonly SolidColorBrush ErrorBrush = new(Colors.Red);
        private static readonly SolidColorBrush DefaultBrush = new(Colors.White);


        // ==========================================
        // 🌐 PROPRIÉTÉS
        // ==========================================
        // Aucune


        // ==========================================
        // 🏗️ CONSTRUCTEUR
        // ==========================================
        // Aucun


        // ==========================================
        // ⚙️ MÉTHODES PUBLIQUES
        // ==========================================

        // Convertit LogLevel → Brush
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is LogLevel level)
            {
                return level switch
                {
                    LogLevel.Info => InfoBrush,
                    LogLevel.Warning => WarningBrush,
                    LogLevel.Error => ErrorBrush,
                    _ => DefaultBrush
                };
            }

            // Sécurité : valeur invalide
            return DefaultBrush;
        }


        // ConvertBack non utilisé
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Sécurité : éviter crash UI
            return LogLevel.Info;
        }


        // ==========================================
        // 🔧 MÉTHODES PRIVÉES
        // ==========================================
        // Aucune
    }
}