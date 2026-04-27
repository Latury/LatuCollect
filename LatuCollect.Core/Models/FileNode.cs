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
        // 1. PROPRIÉTÉS PRINCIPALES
        // ═════════════════════════════════════════════════════════════

        public string Name { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;


        // ═════════════════════════════════════════════════════════════
        // 2. TYPE DU NODE
        // ═════════════════════════════════════════════════════════════
        //
        // Permet de savoir si c’est un fichier ou un dossier
        //

        public bool IsDirectory { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 3. ÉTAT DE SÉLECTION
        // ═════════════════════════════════════════════════════════════

        public bool IsSelected { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 4. STRUCTURE ARBORESCENTE
        // ═════════════════════════════════════════════════════════════

        public List<FileNode> Children { get; set; } = new();


        // ═════════════════════════════════════════════════════════════
        // 5. PROPRIÉTÉS CALCULÉES
        // ═════════════════════════════════════════════════════════════
        //
        // Aide pour éviter des erreurs ailleurs
        //

        public bool HasChildren => Children.Count > 0;
    }
}