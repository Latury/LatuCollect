/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Services.Import                                       ║
║  Fichier : FileImportService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Charger l’arborescence des fichiers depuis le disque                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Lire les dossiers et fichiers                                     ║
║  - Construire les FileNode                                           ║
║  - Appliquer les exclusions                                          ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune dépendance UI                                              ║
║  - Accès disque autorisé                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Configuration;
using LatuCollect.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LatuCollect.Core.Services.Import
{
    public class FileImportService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONSTANTES / LIMITES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_NODES = 1000;
        private const int MAX_DEPTH = 10;


        // ═════════════════════════════════════════════════════════════
        // 2. CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        private readonly AppConfig _config;


        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public FileImportService(AppConfig config)
        {
            _config = config;
        }


        // ═════════════════════════════════════════════════════════════
        // 4. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════

        public async Task<List<FileNode>> LoadTreeAsync(string rootPath)
        {
            if (string.IsNullOrWhiteSpace(rootPath))
                return new List<FileNode>();

            if (!Directory.Exists(rootPath))
                return new List<FileNode>();

            return await Task.Run(() =>
            {
                var result = new List<FileNode>();
                int count = 0;

                var rootNode = CreateNode(rootPath, 0, ref count);

                if (rootNode != null)
                    result.Add(rootNode);

                return result;
            });
        }


        // ═════════════════════════════════════════════════════════════
        // 5. CONSTRUCTION ARBORESCENCE (RÉCURSIVE)
        // ═════════════════════════════════════════════════════════════

        private FileNode CreateNode(string path, int depth, ref int count)
        {
            // 🔒 Limites globales
            if (depth > MAX_DEPTH || count >= MAX_NODES)
                return null;

            var node = new FileNode
            {
                Name = GetSafeName(path),
                Path = path,
                IsDirectory = Directory.Exists(path)
            };

            count++;

            if (!Directory.Exists(path))
                return node;

            // 📁 Dossiers
            TryAddDirectories(node, path, depth, ref count);

            // 📄 Fichiers
            TryAddFiles(node, path, ref count);

            return node;
        }


        // ═════════════════════════════════════════════════════════════
        // 6. DOSSIERS
        // ═════════════════════════════════════════════════════════════

        private void TryAddDirectories(FileNode parent, string path, int depth, ref int count)
        {
            try
            {
                foreach (var dir in Directory.GetDirectories(path))
                {
                    if (count >= MAX_NODES)
                        break;

                    var folderName = Path.GetFileName(dir);

                    if (IsExcluded(folderName))
                        continue;

                    var child = CreateNode(dir, depth + 1, ref count);

                    if (child != null)
                        parent.Children.Add(child);
                }
            }
            catch
            {
                // accès refusé, ignoré volontairement
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 7. FICHIERS
        // ═════════════════════════════════════════════════════════════

        private void TryAddFiles(FileNode parent, string path, ref int count)
        {
            try
            {
                foreach (var file in Directory.GetFiles(path))
                {
                    if (count >= MAX_NODES)
                        break;

                    parent.Children.Add(new FileNode
                    {
                        Name = GetSafeName(file),
                        Path = file,
                        IsDirectory = false
                    });

                    count++;
                }
            }
            catch
            {
                // ignoré volontairement
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 8. MÉTHODES UTILITAIRES
        // ═════════════════════════════════════════════════════════════

        private bool IsExcluded(string folderName)
        {
            return _config.ExcludedFolders.Contains(folderName);
        }

        private string GetSafeName(string path)
        {
            var name = Path.GetFileName(path);

            if (string.IsNullOrWhiteSpace(name))
                return path;

            return name;
        }
    }
}