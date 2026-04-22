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

    // Affiche le contenu du preview uniquement si l’état est Ready

    public class PreviewVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Visibility.Collapsed;

            // On compare le nom de l'état (évite dépendance forte)
            if (value.ToString() == "Ready")
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    // Affiche le message "Aucun fichier sélectionné"
    // uniquement si l’état est Empty

    public class PreviewEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return Visibility.Collapsed;

            if (value.ToString() == "Empty")
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}