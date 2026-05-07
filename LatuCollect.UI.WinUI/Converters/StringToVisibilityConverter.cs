/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : StringToVisibilityConverter.cs                            ║
║                                                                      ║
║  Rôle :                                                              ║
║  Afficher uniquement les headers (string) dans la liste              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Détecter si la valeur est une string                              ║
║  - Retourner Visible uniquement pour les headers                     ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Logique UI uniquement                                             ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════
        // Aucun


        // ═════════════════════════════════════════════════════════════
        // 2. PROPRIÉTÉS
        // ═════════════════════════════════════════════════════════════
        // Aucune


        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════
        // Aucun


        // ═════════════════════════════════════════════════════════════
        // 4. MÉTHODES PUBLIQUES
        // ═════════════════════════════════════════════════════════════

        // Convertit une valeur en visibilité
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // ✔ Visible uniquement si string non vide
            if (value is string text && !string.IsNullOrWhiteSpace(text))
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        // Non utilisé
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }


        // ═════════════════════════════════════════════════════════════
        // 5. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════
        // Aucune
    }
}