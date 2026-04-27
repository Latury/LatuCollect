/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core.Services.Collection                                   ║
║  Fichier : FileCollectionService.cs                                  ║
║                                                                      ║
║  Rôle :                                                              ║
║  Gérer la sélection et la récupération des fichiers                  ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Parcourir une arborescence de fichiers                            ║
║  - Identifier les fichiers sélectionnés                              ║
║  - Retourner une liste de chemins exploitables                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Ne dépend PAS de l’UI                                             ║
║  - Travaille uniquement avec Core.Models.FileNode                    ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - System.IO                                                         ║
║  - Core.Models.FileNode                                              ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Models;
using System.Collections.Generic;
using System.IO;

namespace LatuCollect.Core.Services.Collection
{
    public class FileCollectionService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════
        //
        // Récupère tous les fichiers sélectionnés
        //

        public List<string> GetSelectedFiles(IEnumerable<FileNode> roots)
        {
            var files = new List<string>();

            if (roots == null)
                return files;

            foreach (var root in roots)
            {
                ProcessNode(root, files);
            }

            return files;
        }


        // ═════════════════════════════════════════════════════════════
        // 2. PARCOURS RÉCURSIF
        // ═════════════════════════════════════════════════════════════

        private void ProcessNode(FileNode node, List<string> files)
        {
            if (node == null)
                return;

            // 📄 Fichier sélectionné valide
            if (IsValidSelectedFile(node))
            {
                files.Add(node.Path);
            }

            // 🔁 Parcours enfants
            foreach (var child in node.Children)
            {
                ProcessNode(child, files);
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 3. VALIDATION
        // ═════════════════════════════════════════════════════════════

        private bool IsValidSelectedFile(FileNode node)
        {
            return node.IsSelected &&
                   !string.IsNullOrWhiteSpace(node.Path) &&
                   File.Exists(node.Path);
        }
    }
}
