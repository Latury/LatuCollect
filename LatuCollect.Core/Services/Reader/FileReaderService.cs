/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Services.Reader                                       ║
║  Fichier : FileReaderService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Lire le contenu des fichiers                                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Lire un fichier texte                                             ║
║  - Gérer les erreurs de lecture                                      ║
║  - Appliquer la simulation si activée                                ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - System.IO                                                         ║
║  - SimulationService                                                 ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucune dépendance UI                                              ║
║  - Aucune logique d’export                                           ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Models;
using LatuCollect.Core.Simulation;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LatuCollect.Core.Services.Reader
{
    public static class FileReaderService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CACHE
        // ═════════════════════════════════════════════════════════════

        private const int MAX_CACHE_ITEMS = 100;
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(5);
        private class CacheItem
        {
            public FileReadResult Result { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private static readonly ConcurrentDictionary<string, CacheItem> _fileCache = new();

        private const long MAX_FILE_SIZE = 2 * 1024 * 1024; // 2 MB

        // ═════════════════════════════════════════════════════════════
        // 2. LECTURE FICHIER (ASYNC)
        // ═════════════════════════════════════════════════════════════

        public static async Task<FileReadResult> ReadFileAsync(string path)
        {
            // 🔹 Validation
            if (string.IsNullOrWhiteSpace(path))
                return FileReadResult.Fail("Chemin invalide");

            try
            {
                // 🔹 Existence fichier
                if (!File.Exists(path))
                    return FileReadResult.Fail("Fichier introuvable");

                // 🧪 Simulation
                var simulated = SimulationService.SimulateRead(path);
                if (simulated != null)
                    return FileReadResult.Success(simulated, 0);

                // 🔁 Cache
                if (_fileCache.TryGetValue(path, out var cachedItem))
                {
                    if (DateTime.Now - cachedItem.Timestamp < CACHE_DURATION)
                    {
                        return cachedItem.Result;
                    }
                    else
                    {
                        _fileCache.TryRemove(path, out _);
                    }
                }

                // 🔥 Limitation taille fichier (ex: 10 Mo)
                string content;

                long fileSize = 0;

                try
                {
                    fileSize = new FileInfo(path).Length;
                }
                catch
                {
                    // ignoré
                }

                // 🔥 GROS FICHIER → lecture partielle
                if (fileSize > MAX_FILE_SIZE)
                {
                    using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var reader = new StreamReader(stream);

                    char[] buffer = new char[MAX_FILE_SIZE];
                    int read = await reader.ReadBlockAsync(buffer, 0, buffer.Length);

                    content = new string(buffer, 0, read);

                    content += "\n\n----------------------------------------\n";
                    content += "⚠ Fichier tronqué (trop volumineux)";
                }
                else
                {
                    content = await File.ReadAllTextAsync(path);
                }

                // 📦 Résultat
                var result = FileReadResult.Success(content, fileSize);

                // 💾 Cache
                _fileCache[path] = new CacheItem
                {
                    Result = result,
                    Timestamp = DateTime.Now
                };

                // 🔥 LIMITATION CACHE (ICI EXACTEMENT)
                if (_fileCache.Count > MAX_CACHE_ITEMS)
                {
                    CleanOldestCacheItem();
                }

                return result;
            }
            catch (PathTooLongException)
            {
                return FileReadResult.Fail("Chemin trop long");
            }
            catch (UnauthorizedAccessException)
            {
                return FileReadResult.Fail("Accès refusé");
            }
            catch (IOException)
            {
                return FileReadResult.Fail("Erreur de lecture");
            }
            catch (Exception)
            {
                return FileReadResult.Fail("Erreur inconnue");
            }
        }

        private static void CleanOldestCacheItem()
        {
            if (_fileCache.IsEmpty)
                return;

            var oldest = _fileCache
                .OrderBy(kvp => kvp.Value.Timestamp)
                .FirstOrDefault();

            if (!string.IsNullOrWhiteSpace(oldest.Key))
            {
                _fileCache.TryRemove(oldest.Key, out _);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 3. CACHE UTILITAIRE
        // ═════════════════════════════════════════════════════════════

        public static void ClearCache()
        {
            _fileCache.Clear();
        }

        public static void RemoveFromCache(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                _fileCache.TryRemove(path, out _);
        }
    }
}