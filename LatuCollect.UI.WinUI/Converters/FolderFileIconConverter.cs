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
║  - Si le node possède des enfants ---- (dossier)                     ║
║  - Sinon --- (fichier)                                               ║
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
║  - Converter utilisé uniquement dans le TreeView                     ║
║  - Ne doit contenir AUCUNE logique autre que visuelle                ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using Microsoft.UI.Xaml.Data;
using System;

namespace LatuCollect.UI.WinUI.Converters
{
    public class FolderFileIconConverter : IValueConverter
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS
        // ═════════════════════════════════════════════════════════════
        //
        // (Aucun champ — converter stateless)
        //


        // ═════════════════════════════════════════════════════════════
        // 2. CONVERT (VM → UI)
        // ═════════════════════════════════════════════════════════════
        //
        // Transforme :
        // int (nombre d’enfants) → emoji
        //
        // - > 0 → 📁 (dossier)
        // - 0   → 📄 (fichier)
        //

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int childrenCount)
            {
                return childrenCount > 0 ? "📁" : "📄";
            }

            // fallback sécurité
            return "📄";
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