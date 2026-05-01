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
║  - Gérer les limites (performance)                                   ║
║  - Retourner un résultat structuré                                   ║
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LatuCollect.Core.Services.Import
{
    // ═════════════════════════════════════════════════════════════
    // 1. SERVICE IMPORT
    // ═════════════════════════════════════════════════════════════

    public class FileImportService
    {
        // ═════════════════════════════════════════════════════════════
        // 1.1 LIMITES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_NODES = 1000;
        private const int MAX_DEPTH = 10;

        private readonly AppConfig _config;

        // ═════════════════════════════════════════════════════════════
        // 1.2 CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public FileImportService(AppConfig config)
        {
            _config = config;
        }

        // ═════════════════════════════════════════════════════════════
        // 1.3 MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════

        public async Task<ImportResult> LoadTreeAsync(
    string rootPath,
    CancellationToken cancellationToken = default)
        {
            var result = new ImportResult();

            if (string.IsNullOrWhiteSpace(rootPath) || !Directory.Exists(rootPath))
                return result;

            return await Task.Run(() =>
            {
                int count = 0;

                var rootNode = CreateNode(
    rootPath,
    depth: 0,
    ref count,
    result,
    cancellationToken
);

                if (rootNode != null)
                    result.Nodes.Add(rootNode);

                result.TotalNodes = count;

                return result;

            }, cancellationToken);
        }

        // ═════════════════════════════════════════════════════════════
        // 1.4 CRÉATION NODE
        // ═════════════════════════════════════════════════════════════

        private FileNode? CreateNode(
            string path,
            int depth,
            ref int count,
            ImportResult result,
            CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return null;

            if (depth > MAX_DEPTH || count >= MAX_NODES)
            {
                result.IsPartial = true;
                result.Message = "⚠ Projet volumineux — affichage partiel";
                return null;
            }

            var node = new FileNode
            {
                Name = GetSafeName(path),
                Path = path,
                IsDirectory = Directory.Exists(path)
            };

            count++;

            if (!node.IsDirectory)
                return node;

            // 📁 DOSSIERS (triés)
            TryAddDirectories(node, path, depth, ref count, result, token);

            // 📄 FICHIERS (triés)
            TryAddFiles(node, path, ref count, result, token);

            return node;
        }

        // ═════════════════════════════════════════════════════════════
        // 1.5 DOSSIERS
        // ═════════════════════════════════════════════════════════════

        private void TryAddDirectories(
            FileNode parent,
            string path,
            int depth,
            ref int count,
            ImportResult result,
            CancellationToken token)
        {
            try
            {
                var directories = Directory
                    .EnumerateDirectories(path)
                    .OrderBy(d => d);

                foreach (var dir in directories)
                {
                    if (token.IsCancellationRequested)
                        return;

                    if (count >= MAX_NODES)
                    {
                        result.IsPartial = true;
                        return;
                    }

                    var folderName = Path.GetFileName(dir);

                    if (IsExcluded(folderName))
                        continue;

                    var child = CreateNode(dir, depth + 1, ref count, result, token);

                    if (child != null)
                        parent.Children.Add(child);
                }
            }
            catch
            {
                // ignoré volontairement
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 1.6 FICHIERS
        // ═════════════════════════════════════════════════════════════

        private void TryAddFiles(
            FileNode parent,
            string path,
            ref int count,
            ImportResult result,
            CancellationToken token)
        {
            try
            {
                var files = Directory
                    .EnumerateFiles(path)
                    .OrderBy(f => f);

                foreach (var file in files)
                {
                    if (token.IsCancellationRequested)
                        return;

                    if (count >= MAX_NODES)
                    {
                        result.IsPartial = true;
                        return;
                    }

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
        // 1.7 UTILITAIRES
        // ═════════════════════════════════════════════════════════════

        private bool IsExcluded(string folderName)
        {
            return _config.ExcludedFolders.Contains(folderName);
        }

        private string GetSafeName(string path)
        {
            var name = Path.GetFileName(path);
            return string.IsNullOrWhiteSpace(name) ? path : name;
        }
    }
}