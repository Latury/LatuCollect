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

    public class FileImportService : IFileImportService
    {
        // ═════════════════════════════════════════════════════════════
        // 1.1 LIMITES
        // ═════════════════════════════════════════════════════════════

        private const int MAX_NODES = 5000;
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

            // 🔥 IMPORTANT
            // On autorise toujours :
            // - le root
            // - les enfants directs du root
            // même si MAX_NODES est atteint
            bool allowRootLevel = depth <= 1;

            if (depth > MAX_DEPTH ||
                (!allowRootLevel && count >= MAX_NODES))
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

            // 🔥 IMPORTANT
            // Si quota atteint :
            // on garde le node visible
            // mais on ne charge plus ses enfants
            if (count >= MAX_NODES)
            {
                result.IsPartial = true;
                result.Message = "⚠ Projet volumineux — affichage partiel";

                return node;
            }

            if (!node.IsDirectory)
                return node;

            // 📁 DOSSIERS
            TryAddDirectories(
                node,
                path,
                depth,
                ref count,
                result,
                token);

            // 📄 FICHIERS
            TryAddFiles(
                node,
                path,
                ref count,
                result,
                token);

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
            IEnumerable<string> directories;

            try
            {
                directories = Directory.EnumerateDirectories(path);
            }
            catch
            {
                // 🔥 IMPORTANT : si accès refusé → on abandonne CE dossier
                return;
            }

            foreach (var dir in directories.OrderBy(Path.GetFileName))
            {
                if (token.IsCancellationRequested)
                    return;

                bool allowRootChildren = depth == 0;

                if (count >= MAX_NODES && !allowRootChildren)
                {
                    result.IsPartial = true;
                    return;
                }

                // 🔥 EXCLUSION AVANT TOUT
                if (IsExcluded(dir))
                    continue;

                FileNode? child;

                try
                {
                    child = CreateNode(dir, depth + 1, ref count, result, token);
                }
                catch
                {
                    // 🔥 si un sous-dossier plante → on ignore proprement
                    continue;
                }

                if (child != null)
                    parent.Children.Add(child);
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
                    .OrderBy(Path.GetFileName);

                foreach (var file in files)
                {
                    if (token.IsCancellationRequested)
                        return;

                    if (count >= MAX_NODES)
                    {
                        result.IsPartial = true;
                        return;
                    }

                    // 🔥 IMPORTANT : exclusion fichiers
                    if (IsExcluded(file))
                        continue;

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

        private bool IsExcluded(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            string normalizedPath = NormalizePath(path);

            foreach (var exclusion in _config.ExcludedFolders)
            {
                if (exclusion == null)
                    continue;

                string name = exclusion.Name?.Trim();

                if (string.IsNullOrWhiteSpace(name) || name.Length < 2)
                    continue;

                // Cas chemin complet
                if (name.Contains("\\") || name.Contains("/"))
                {
                    string normalizedExclusion = NormalizePath(name);

                    if (normalizedExclusion.Length < 4)
                        continue;

                    if (normalizedPath.StartsWith(normalizedExclusion, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                // Cas nom simple (compatibilité)
                else
                {
                    string folderName = Path.GetFileName(normalizedPath);

                    if (string.Equals(folderName, name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
            }

            return false;
        }

        // 🔒 normalisation pour éviter les problèmes de slashs et de comparaisons
        private string NormalizePath(string path)
        {
            return path
                .Trim()
                .Replace('/', '\\')
                .TrimEnd('\\');
        }

        private string GetSafeName(string path)
        {
            var name = Path.GetFileName(path);
            return string.IsNullOrWhiteSpace(name) ? path : name;
        }
    }
}