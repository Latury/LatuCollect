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

        // ═════════════════════════════════════════════════════════════════════
        // 1. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════════════
        //
        // Point d’entrée principal :
        // - récupère tous les fichiers sélectionnés
        //

        public List<string> GetSelectedFiles(List<FileNode> roots)
        {
            List<string> files = new();

            foreach (var root in roots)
            {
                ProcessNode(root, files);
            }

            return files;
        }


        // ═════════════════════════════════════════════════════════════════════
        // 2. PARCOURS RÉCURSIF
        // ═════════════════════════════════════════════════════════════════════
        //
        // Parcourt l’arborescence :
        // - ajoute les fichiers sélectionnés
        // - descend dans les enfants
        //

        private void ProcessNode(FileNode node, List<string> files)
        {
            // 📄 Si sélectionné et fichier réel
            if (node.IsSelected && File.Exists(node.Path))
            {
                files.Add(node.Path);
            }

            // 🔁 Parcours des enfants
            foreach (var child in node.Children)
            {
                ProcessNode(child, files);
            }
        }
    }
}
