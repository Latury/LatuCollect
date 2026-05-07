/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : IsProtectedConverter.cs                                   ║
║                                                                      ║
║  Rôle :                                                              ║
║  Déterminer si un dossier est protégé pour affichage UI              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Vérifier si une exclusion est protégée                            ║
║  - Retourner une Visibility adaptée                                  ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - UI uniquement                                                     ║
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
    public class IsProtectedConverter : IValueConverter
    {
        // ═════════════════════════════════════════════════════════════
        // 1. DONNÉES PROTÉGÉES (TEMPORAIRE UI)
        // ═════════════════════════════════════════════════════════════

        private static readonly string[] Protected =
        {
            ".git",
            "bin",
            "obj"
        };


        // ═════════════════════════════════════════════════════════════
        // 2. CONVERT
        // ═════════════════════════════════════════════════════════════

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not string name)
                return Visibility.Collapsed;

            foreach (var item in Protected)
            {
                if (string.Equals(name, item, StringComparison.OrdinalIgnoreCase))
                    return Visibility.Visible;
            }

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