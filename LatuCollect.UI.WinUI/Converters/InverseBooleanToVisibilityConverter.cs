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
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════
        //
        // (Aucun champ — converter stateless)
        //


        // ═════════════════════════════════════════════════════════════
        // 2. CONVERT (VM → UI)
        // ═════════════════════════════════════════════════════════════
        //
        // true  → Collapsed
        // false → Visible
        //

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isVisible && isVisible)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }


        // ═════════════════════════════════════════════════════════════
        // 3. CONVERT BACK (UI → VM)
        // ═════════════════════════════════════════════════════════════
        //
        // Visibility.Visible → false
        // Visibility.Collapsed → true
        //

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
                return visibility != Visibility.Visible;

            return false;
        }
    }
}