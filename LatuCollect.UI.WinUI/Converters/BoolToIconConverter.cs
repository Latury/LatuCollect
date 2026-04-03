/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI                                                         ║
║  Fichier : BoolToIconConverter.cs                                    ║
║                                                                      ║
║  Rôle :                                                              ║
║  Convertir un booléen (IsFolder) en icône visuelle                   ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Retourner  si dossier                                             ║
║  - Retourner  si fichier                                             ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Microsoft.UI.Xaml.Data                                            ║
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
        // =========================
        // MÉTHODES PUBLIQUES
        // =========================

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool isFolder && isFolder)
                return "📁";

            return "📄";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return false;
        }
    }
}