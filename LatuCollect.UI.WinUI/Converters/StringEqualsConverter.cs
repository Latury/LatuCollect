/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : StringEqualsConverter.cs                                  ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir une chaîne en booléen pour binding RadioButton            ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Comparer une valeur avec un paramètre                             ║
║  - Retourner true si égal                                            ║
║  - Permettre le binding bidirectionnel                               ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Logique UI uniquement                                             ║
║  - Aucun accès Core                                                  ║
║                                                                      ║
║  Utilisation :                                                       ║
║  - Binding RadioButton <-> SelectedFormat                            ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    public class StringEqualsConverter : IValueConverter
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════
        //
        // (Aucun champ ici)
        // Converter stateless → logique pure
        //


        // ═════════════════════════════════════════════════════════════
        // 2. CONVERT (VM → UI)
        // ═════════════════════════════════════════════════════════════
        //
        // Compare :
        // SelectedFormat == ".txt" ?
        //

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value?.ToString() == parameter?.ToString();
        }


        // ═════════════════════════════════════════════════════════════
        // 3. CONVERT BACK (UI → VM)
        // ═════════════════════════════════════════════════════════════
        //
        // Si RadioButton coché → retourne ".txt" ou ".md"
        //

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isChecked && isChecked)
            {
                return parameter?.ToString();
            }

            // 🔥 WinUI : on ne retourne RIEN
            return null;
        }
    }
}