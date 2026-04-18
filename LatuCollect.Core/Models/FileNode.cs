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

        // ═════════════════════════════════════════════════════════════════════
        // 1. PROPRIÉTÉS PRINCIPALES
        // ═════════════════════════════════════════════════════════════════════
        //
        // Informations de base du fichier / dossier
        //

        public string Name { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;


        // ═════════════════════════════════════════════════════════════════════
        // 2. ÉTAT DE SÉLECTION
        // ═════════════════════════════════════════════════════════════════════
        //
        // Indique si le fichier est sélectionné pour l’export
        //

        public bool IsSelected { get; set; }


        // ═════════════════════════════════════════════════════════════════════
        // 3. STRUCTURE ARBORESCENTE
        // ═════════════════════════════════════════════════════════════════════
        //
        // Permet de représenter les sous-dossiers / fichiers
        //

        public List<FileNode> Children { get; set; } = new();
    }
}