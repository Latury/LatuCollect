/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Tests.Helpers                                              ║
║  Fichier : TestTreeFactory.cs                                        ║
║                                                                      ║
║  Rôle :                                                              ║
║  Fournir des helpers pour créer des arbres de test                   ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Créer des FileNode cohérents                                      ║
║  - Assigner automatiquement les parents                              ║
║  - Réduire la duplication dans les tests                             ║
║                                                                      ║
║  IMPORTANT :                                                         ║
║  - Tests uniquement                                                  ║
║  - Aucun accès disque                                                ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.UI.WinUI.Models;

namespace LatuCollect.Tests.Helpers
{
    public static class TestTreeFactory
    {
        // ═════════════════════════════════════════════════════════════
        // CRÉATION DOSSIER
        // ═════════════════════════════════════════════════════════════

        public static FileNode CreateFolder(
            string name,
            bool isSelected = false)
        {
            return new FileNode
            {
                Name = name,
                IsDirectory = true,
                IsSelected = isSelected
            };
        }

        // ═════════════════════════════════════════════════════════════
        // CRÉATION FICHIER
        // ═════════════════════════════════════════════════════════════

        public static FileNode CreateFile(
            string name,
            bool isSelected = false)
        {
            return new FileNode
            {
                Name = name,
                IsDirectory = false,
                IsSelected = isSelected
            };
        }

        // ═════════════════════════════════════════════════════════════
        // AJOUT ENFANT
        // ═════════════════════════════════════════════════════════════

        public static void AddChild(
            FileNode parent,
            FileNode child)
        {
            child.Parent = parent;

            parent.Children.Add(child);
        }
    }
}