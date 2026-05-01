/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : PreviewStateConverters.cs                                 ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir l’état du preview (UiState) en visibilité UI              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Preview visible uniquement en Ready                               ║
║  - Message vide uniquement en Empty                                  ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - UI uniquement                                                     ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Gestion de la visibilité du preview selon UiState


    // ==========================================
    // 📄 PREVIEW VISIBLE CONVERTER
    // ==========================================
    // Visible uniquement en état Ready

    public class PreviewVisibleConverter : IValueConverter
    {
        // ==========================================
        // ⚙️ MÉTHODES PUBLIQUES
        // ==========================================

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is MainViewModel.UiState state)
            {
                return state == MainViewModel.UiState.Ready
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return MainViewModel.UiState.Empty;
        }
    }


    // ==========================================
    // 📄 PREVIEW EMPTY CONVERTER
    // ==========================================
    // Visible uniquement en état Empty

    public class PreviewEmptyConverter : IValueConverter
    {
        // ==========================================
        // ⚙️ MÉTHODES PUBLIQUES
        // ==========================================

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is MainViewModel.UiState state)
            {
                return state == MainViewModel.UiState.Empty
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return MainViewModel.UiState.Empty;
        }
    }
}