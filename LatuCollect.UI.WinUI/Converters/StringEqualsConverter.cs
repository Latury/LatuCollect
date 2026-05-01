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
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Compare une string avec un paramètre
    // Utilisé pour binding RadioButton


    public class StringEqualsConverter : IValueConverter
    {
        // ==========================================
        // 🔒 CHAMPS PRIVÉS
        // ==========================================
        // Aucun (stateless)


        // ==========================================
        // 🌐 PROPRIÉTÉS
        // ==========================================
        // Aucune


        // ==========================================
        // 🏗️ CONSTRUCTEUR
        // ==========================================
        // Aucun


        // ==========================================
        // ⚙️ MÉTHODES PUBLIQUES
        // ==========================================

        // VM → UI
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string str && parameter is string param)
            {
                return string.Equals(str, param, StringComparison.Ordinal);
            }

            return false;
        }


        // UI → VM
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isChecked && isChecked)
            {
                return parameter?.ToString();
            }

            // Ne pas modifier la source si décoché
            return null;
        }


        // ==========================================
        // 🔧 MÉTHODES PRIVÉES
        // ==========================================
        // Aucune
    }
}