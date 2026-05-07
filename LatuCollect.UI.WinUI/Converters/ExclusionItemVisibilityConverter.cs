/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : ExclusionItemVisibilityConverter.cs                       ║
║                                                                      ║
║  Rôle :                                                              ║
║  Afficher uniquement les éléments d’exclusion                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Détecter si la valeur est un ExclusionItem                        ║
║  - Retourner Visible uniquement pour les items                       ║
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
using LatuCollect.Core.Configuration.Models;

namespace LatuCollect.UI.WinUI.Converters
{
    public class ExclusionItemVisibilityConverter : IValueConverter
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
            // ✔ Visible uniquement si c'est un ExclusionItem
            if (value is ExclusionItem)
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