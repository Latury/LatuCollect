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
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Convertit un booléen en visibilité inversée :
    // true  → Collapsed
    // false → Visible
    //
    // Converter stateless


    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        // ==========================================
        // 🔒 CHAMPS PRIVÉS
        // ==========================================
        // Aucun champ


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

        // Convertit bool → Visibility (inversé)
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isVisible && isVisible)
                return Visibility.Collapsed;

            return Visibility.Visible;
        }


        // Convertit Visibility → bool (inversé)
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
                return visibility != Visibility.Visible;

            return false;
        }


        // ==========================================
        // 🔧 MÉTHODES PRIVÉES
        // ==========================================
        // Aucune
    }
}