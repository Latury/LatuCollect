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
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Convertit un booléen en visibilité UI
    // true  → Visible
    // false → Collapsed
    //
    // Converter stateless (aucun état interne)


    public partial class BooleanToVisibilityConverter : IValueConverter
    {
        // ==========================================
        // 🔒 CHAMPS PRIVÉS
        // ==========================================
        // Aucun champ (converter stateless)


        // ==========================================
        // 🌐 PROPRIÉTÉS
        // ==========================================
        // Aucune propriété


        // ==========================================
        // 🏗️ CONSTRUCTEUR
        // ==========================================
        // Aucun constructeur spécifique


        // ==========================================
        // ⚙️ MÉTHODES PUBLIQUES
        // ==========================================

        // Convertit bool → Visibility
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isVisible && isVisible)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }


        // Convertit Visibility → bool
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
                return visibility == Visibility.Visible;

            return false;
        }


        // ==========================================
        // 🔧 MÉTHODES PRIVÉES
        // ==========================================
        // Aucune
    }
}