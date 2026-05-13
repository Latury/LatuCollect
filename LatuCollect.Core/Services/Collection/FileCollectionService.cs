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
        // 2. API PUBLIQUE
        // ═════════════════════════════════════════════════════════════
        //
        // Point d’entrée principal
        //

        public List<string> GetSelectedFiles(IEnumerable<FileNode> roots)
        {
            // 🔒 sécurité
            if (roots == null)
                return new List<string>();

            var files = new HashSet<string>(
                System.StringComparer.OrdinalIgnoreCase);

            // 🔁 parcours des racines
            foreach (var root in roots)
            {
                TraverseNode(root, files);

                if (IsLimitReached(files))
                    break;
            }

            return BuildResult(files);
        }


        // ═════════════════════════════════════════════════════════════
        // 3. PARCOURS ARBORESCENCE
        // ═════════════════════════════════════════════════════════════
        //
        // Exploration récursive des nodes
        //

        private void TraverseNode(FileNode node, HashSet<string> files)
        {
            if (node == null || IsLimitReached(files))
                return;

            // 📄 Ajout fichier valide
            if (IsValidSelectedFile(node))
            {
                files.Add(node.Path);
            }

            // 🔁 enfants
            if (node.Children == null)
                return;

            foreach (var child in node.Children)
            {
                TraverseNode(child, files);

                if (IsLimitReached(files))
                    return;
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 4. VALIDATION
        // ═════════════════════════════════════════════════════════════
        //
        // Vérifie si un node représente un fichier valide
        //

        private bool IsValidSelectedFile(FileNode node)
        {
            if (node == null)
                return false;

            if (!node.IsSelected)
                return false;

            if (node.IsDirectory)
                return false;

            if (string.IsNullOrWhiteSpace(node.Path))
                return false;

            try
            {
                return File.Exists(node.Path);
            }
            catch
            {
                return false;
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 5. RÈGLES / LIMITES
        // ═════════════════════════════════════════════════════════════
        //
        // Gestion des contraintes de performance
        //

        private bool IsLimitReached(HashSet<string> files)
        {
            return files.Count >= MAX_FILES;
        }


        // ═════════════════════════════════════════════════════════════
        // 6. BUILD RÉSULTAT
        // ═════════════════════════════════════════════════════════════
        //
        // Transformation finale (tri + liste)
        //

        private List<string> BuildResult(HashSet<string> files)
        {
            var result = files.ToList();
            result.Sort();
            return result;
        }
    }
}