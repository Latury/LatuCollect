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
    public class LogLevelToColorConverter : IValueConverter
    {
        // ═════════════════════════════════════════════════════════════
        // 🎨 CONVERSION : LogLevel → Couleur
        // ═════════════════════════════════════════════════════════════

        // Convertit un LogLevel en une SolidColorBrush correspondante
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is LogLevel level)
            {
                return level switch
                {
                    LogLevel.Info => new SolidColorBrush(Colors.LightGray),
                    LogLevel.Warning => new SolidColorBrush(Colors.Orange),
                    LogLevel.Error => new SolidColorBrush(Colors.Red),
                    _ => new SolidColorBrush(Colors.White)
                };
            }

            return new SolidColorBrush(Colors.White);
        }

        // ═════════════════════════════════════════════════════════════
        // 🔁 NON UTILISÉ
        // ═════════════════════════════════════════════════════════════

        // La conversion inverse n'est pas implémentée car elle n'est pas nécessaire pour l'affichage des logs
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}