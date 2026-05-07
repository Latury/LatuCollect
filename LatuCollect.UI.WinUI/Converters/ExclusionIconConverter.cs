/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : ExclusionIconConverter.cs                                 ║
║                                                                      ║
║  Rôle :                                                              ║
║  Générer l’icône complète d’une exclusion                            ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Détecter fichier / dossier                                        ║
║  - Ajouter un cadena si protégé                                      ║
║  - Retourner une seule chaîne prête à afficher                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Logique UI uniquement                                             ║
║  - Aucune logique métier                                             ║
║                                                                      ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration.Models;
using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    // ==========================================
    // 🧠 DESCRIPTION
    // ==========================================
    // Génère :
    // 🔒📁 / 🔒📄 / 📁 / 📄


    public class ExclusionIconConverter : IValueConverter
    {
        // ==========================================
        // ⚙️ MÉTHODES PUBLIQUES
        // ==========================================

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is not ExclusionItem item)
                return "";

            return item.IsDirectory
                ? "📁"
                : "📄";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}