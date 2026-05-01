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
        // 1. CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        private const int MAX_CACHE_ITEMS = 100;
        private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromMinutes(5);
        private const long MAX_FILE_SIZE = 2 * 1024 * 1024; // 2 MB


        // ═════════════════════════════════════════════════════════════
        // 2. CACHE — STRUCTURE
        // ═════════════════════════════════════════════════════════════

        private class CacheItem
        {
            public FileReadResult Result { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private static readonly ConcurrentDictionary<string, CacheItem> _fileCache = new();


        // ═════════════════════════════════════════════════════════════
        // 3. API PUBLIQUE — LECTURE
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
                if (TryGetFromCache(path, out var cached))
                    return cached;

                // 📦 Lecture fichier
                var (content, fileSize) = await ReadContentAsync(path);

                var result = FileReadResult.Success(content, fileSize);

                // 💾 Cache
                AddToCache(path, result);

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


        // ═════════════════════════════════════════════════════════════
        // 4. LECTURE INTERNE
        // ═════════════════════════════════════════════════════════════

        private static async Task<(string content, long fileSize)> ReadContentAsync(string path)
        {
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

                var content = new string(buffer, 0, read);

                content += "\n\n----------------------------------------\n";
                content += "⚠ Fichier tronqué (trop volumineux)";

                return (content, fileSize);
            }

            var fullContent = await File.ReadAllTextAsync(path);
            return (fullContent, fileSize);
        }


        // ═════════════════════════════════════════════════════════════
        // 5. CACHE — GESTION
        // ═════════════════════════════════════════════════════════════

        private static bool TryGetFromCache(string path, out FileReadResult result)
        {
            result = null;

            if (_fileCache.TryGetValue(path, out var cachedItem))
            {
                if (DateTime.Now - cachedItem.Timestamp < CACHE_DURATION)
                {
                    result = cachedItem.Result;
                    return true;
                }

                _fileCache.TryRemove(path, out _);
            }

            return false;
        }

        private static void AddToCache(string path, FileReadResult result)
        {
            _fileCache[path] = new CacheItem
            {
                Result = result,
                Timestamp = DateTime.Now
            };

            if (_fileCache.Count > MAX_CACHE_ITEMS)
            {
                CleanOldestCacheItem();
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
        // 6. CACHE — API PUBLIQUE
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