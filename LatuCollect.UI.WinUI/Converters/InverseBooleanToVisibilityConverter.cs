/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : InverseBooleanToVisibilityConverter.cs                    ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir un booléen en visibilité inversée                         ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - true  → Collapsed                                                 ║
║  - false → Visible                                                   ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Utilisé uniquement dans l’UI                                      ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Microsoft.UI.Xaml                                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. CONVERSION → BOOL → VISIBILITY (INVERSE)
        // ═════════════════════════════════════════════════════════════════════

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isVisible && isVisible)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }


        // ═════════════════════════════════════════════════════════════════════
        // 2. CONVERSION INVERSE (OPTIONNELLE)
        // ═════════════════════════════════════════════════════════════════════

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
                return visibility != Visibility.Visible;

            return false;
        }
    }
}