/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : PreviewStateConverters.cs                                 ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir l’état du preview (UiState) en visibilité UI              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Afficher le preview uniquement en état Ready                      ║
║  - Afficher le message vide uniquement en état Empty                 ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune logique métier                                             ║
║  - Transformation UI uniquement                                      ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Microsoft.UI.Xaml                                                 ║
║  - MainViewModel.UiState                                             ║
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
    // ═════════════════════════════════════════════════════════════
    // 1. PREVIEW VISIBLE CONVERTER
    // ═════════════════════════════════════════════════════════════
    //
    // Affiche le contenu du preview uniquement si l’état est Ready
    //

    public class PreviewVisibleConverter : IValueConverter
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

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Visibility.Collapsed;

            // Comparaison par string (découplage du ViewModel)
            if (value.ToString() == "Ready")
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        // ═════════════════════════════════════════════════════════════
        // 3. CONVERT BACK (NON UTILISÉ)
        // ═════════════════════════════════════════════════════════════

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    // ═════════════════════════════════════════════════════════════
    // 2. PREVIEW EMPTY CONVERTER
    // ═════════════════════════════════════════════════════════════
    //
    // Affiche le message "Aucun fichier sélectionné"
    // uniquement si l’état est Empty
    //

    public class PreviewEmptyConverter : IValueConverter
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

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Visibility.Collapsed;

            if (value.ToString() == "Empty")
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        // ═════════════════════════════════════════════════════════════
        // 3. CONVERT BACK (NON UTILISÉ)
        // ═════════════════════════════════════════════════════════════

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}