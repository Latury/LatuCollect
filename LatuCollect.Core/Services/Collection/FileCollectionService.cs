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
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LatuCollect.Core.Services.Collection
{
    public class FileCollectionService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTANTES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_FILES = 5000;


        // ═════════════════════════════════════════════════════════════
        // 2. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════

        public List<string> GetSelectedFiles(IEnumerable<FileNode> roots)
        {
            var files = new HashSet<string>();

            if (roots == null)
                return new List<string>();

            foreach (var root in roots)
            {
                ProcessNode(root, files);

                // 🔥 STOP si limite atteinte
                if (files.Count >= MAX_FILES)
                    break;
            }

            // 🔹 conversion + tri
            var result = files.ToList();
            result.Sort();

            return result;
        }


        // ═════════════════════════════════════════════════════════════
        // 3. PARCOURS RÉCURSIF
        // ═════════════════════════════════════════════════════════════

        private void ProcessNode(FileNode node, HashSet<string> files)
        {
            if (node == null)
                return;

            // 🔥 STOP si limite atteinte
            if (files.Count >= MAX_FILES)
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

                if (files.Count >= MAX_FILES)
                    return;
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 4. VALIDATION
        // ═════════════════════════════════════════════════════════════

        private bool IsValidSelectedFile(FileNode node)
        {
            return node.IsSelected &&
                   !string.IsNullOrWhiteSpace(node.Path) &&
                   File.Exists(node.Path);
        }
    }
}