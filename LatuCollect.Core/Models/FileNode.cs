/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Models                                                ║
║  Fichier : FileNode.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter un fichier ou dossier dans le Core                      ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker les informations d’un node                                ║
║  - Représenter une structure arborescente                            ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune dépendance UI                                              ║
║  - Modèle simple (data only)                                         ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Collections.Generic;

namespace LatuCollect.Core.Models
{
    public class FileNode
    {
        // ═════════════════════════════════════════════════════════════
        // 1. IDENTITÉ DU NODE
        // ═════════════════════════════════════════════════════════════
        //
        // Informations de base du fichier / dossier
        //

        public string Name { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;


        // ═════════════════════════════════════════════════════════════
        // 2. TYPE DU NODE
        // ═════════════════════════════════════════════════════════════
        //
        // true = dossier
        // false = fichier
        //

        public bool IsDirectory { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 3. ÉTAT DE SÉLECTION
        // ═════════════════════════════════════════════════════════════
        //
        // ⚠ Utilisé par la couche UI
        // TODO (refactor futur) : déplacer dans le modèle UI
        //

        public bool IsSelected { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 4. STRUCTURE ARBORESCENTE
        // ═════════════════════════════════════════════════════════════
        //
        // Enfants du node (si dossier)
        //

        public List<FileNode> Children { get; set; } = new();


        // ═════════════════════════════════════════════════════════════
        // 5. PROPRIÉTÉS CALCULÉES
        // ═════════════════════════════════════════════════════════════

        // Indique si le node contient des enfants
        public bool HasChildren => Children.Count > 0;
    }
}