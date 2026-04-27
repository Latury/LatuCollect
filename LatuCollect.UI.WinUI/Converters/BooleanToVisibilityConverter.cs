/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : BooleanToVisibilityConverter.cs                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir un booléen en visibilité UI                               ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - true  → Visible                                                   ║
║  - false → Collapsed                                                 ║
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
    public partial class BooleanToVisibilityConverter : IValueConverter
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
        // Transforme :
        // bool → Visibility
        //
        // true  → Visible
        // false → Collapsed
        //

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isVisible && isVisible)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }


        // ═════════════════════════════════════════════════════════════
        // 3. CONVERT BACK (UI → VM)
        // ═════════════════════════════════════════════════════════════
        //
        // Visibility.Visible   → true
        // Visibility.Collapsed → false
        //

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
                return visibility == Visibility.Visible;

            return false;
        }
    }
}