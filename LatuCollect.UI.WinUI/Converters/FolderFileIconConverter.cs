/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI.WinUI.Converters                                        ║
║  Fichier : FolderFileIconConverter.cs                                ║
║                                                                      ║
║  Rôle :                                                              ║
║  Fournir une icône visuelle (emoji) selon le type de node            ║
║                                                                      ║
║  Fonctionnement :                                                    ║
║  - Si le node possède des enfants → dossier                          ║
║  - Sinon → fichier                                                   ║
║                                                                      ║
║  Entrée :                                                            ║
║  - int (nombre d’enfants du node)                                    ║
║                                                                      ║
║  Sortie :                                                            ║
║  - string (emoji)                                                    ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║   - UI uniquement                                                    ║
║   - Aucune logique métier                                            ║
║   - Aucune dépendance Core                                           ║
║                                                                      ║
║  Notes :                                                             ║
║  - Utilisé dans le TreeView                                          ║
║  - Logique strictement visuelle                                      ║
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
    // Convertit un nombre d’enfants en icône :
    // > 0 → 📁 (dossier)
    // 0   → 📄 (fichier)
    //
    // Converter stateless


    public class FolderFileIconConverter : IValueConverter
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

        // Convertit int → icône (string)
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int childrenCount)
            {
                return childrenCount > 0 ? "📁" : "📄";
            }

            // Sécurité : valeur inattendue → fichier par défaut
            return "📄";
        }


        // ConvertBack non utilisé
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // Sécurité : éviter crash si utilisé par erreur
            return 0;
        }


        // ==========================================
        // 🔧 MÉTHODES PRIVÉES
        // ==========================================
        // Aucune
    }
}