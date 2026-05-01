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
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Convertit un booléen en icône UI
    // true  → ✔
    // false → "" (vide)
    //
    // Converter stateless


    public partial class BoolToIconConverter : IValueConverter
    {
        // ==========================================
        // 🔒 CHAMPS PRIVÉS
        // ==========================================
        // Aucun champ


        // ==========================================
        // 🌐 PROPRIÉTÉS
        // ==========================================
        // Aucune propriété


        // ==========================================
        // 🏗️ CONSTRUCTEUR
        // ==========================================
        // Aucun constructeur spécifique


        // ==========================================
        // ⚙️ MÉTHODES PUBLIQUES
        // ==========================================

        // Convertit bool → icône (string)
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isChecked && isChecked)
                return "✔";

            return string.Empty;
        }


        // ConvertBack non utilisé
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Sécurité : éviter crash si utilisé par erreur
            return false;
        }


        // ==========================================
        // 🔧 MÉTHODES PRIVÉES
        // ==========================================
        // Aucune
    }
}