/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI                                                         ║
║  Fichier : InverseBooleanToVisibilityConverter.cs                    ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir un booléen en Visibility inversée                         ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Transformer true → Collapsed                                      ║
║  - Transformer false → Visible                                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Utilisé uniquement par la couche UI                               ║
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
        // ======================================================
        // 🔄 CONVERSION (bool → Visibility inversée)
        // ======================================================

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool b)
                return b ? Visibility.Collapsed : Visibility.Visible;

            return Visibility.Visible;
        }

        // ======================================================
        // 🔁 CONVERSION INVERSE (non utilisée)
        // ======================================================

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}