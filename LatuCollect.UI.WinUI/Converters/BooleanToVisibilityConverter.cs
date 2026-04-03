/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI                                                         ║
║  Fichier : BooleanToVisibilityConverter.cs                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir un booléen en visibilité UI                               ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - true  → Visible                                                   ║
║  - false → Collapsed                                                 ║
║  - gérer une inversion optionnelle (Invert)                          ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Microsoft.UI.Xaml                                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    public partial class BooleanToVisibilityConverter : IValueConverter
    {
        // =========================
        // MÉTHODE PRINCIPALE
        // =========================
        // Convertit un bool → Visibility

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Vérifie que la valeur est bien un bool
            if (value is bool visible)
            {
                // =========================
                // GESTION DE L’INVERSION
                // =========================
                // Si on passe "Invert" dans le XAML,
                // on inverse le comportement

                if (parameter?.ToString() == "Invert")
                {
                    visible = !visible;
                }

                // =========================
                // CONVERSION
                // =========================
                // true  → Visible
                // false → Collapsed

                return visible ? Visibility.Visible : Visibility.Collapsed;
            }

            // Par sécurité : si valeur invalide
            return Visibility.Collapsed;
        }

        // =========================
        // NON UTILISÉ (OPTIONNEL)
        // =========================

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Pas utilisé dans notre cas
            return false;
        }
    }
}