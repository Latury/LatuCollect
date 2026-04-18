/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : BoolToIconConverter.cs                                    ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir un booléen en icône UI                                    ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - true  → icône active                                              ║
║  - false → icône inactive                                            ║
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

using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    public partial class BoolToIconConverter : IValueConverter
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. CONVERSION → BOOL → ICÔNE
        // ═════════════════════════════════════════════════════════════════════

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isChecked && isChecked)
            {
                // ✔ Icône active (ex: cochée)
                return "✔";
            }

            // ❌ Icône inactive
            return "";
        }


        // ═════════════════════════════════════════════════════════════════════
        // 2. CONVERSION INVERSE (NON UTILISÉE)
        // ═════════════════════════════════════════════════════════════════════

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}