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
║                                                                      ║
║  Dépendances :                                                       ║
║  - System.IO                                                         ║
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
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LatuCollect.Core.Services.Reader
{
    public static class FileReaderService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CONFIGURATION
        // ═════════════════════════════════════════════════════════════

        private const int MAX_CACHE_ITEMS = 100;

        private static readonly TimeSpan CACHE_DURATION =
            TimeSpan.FromMinutes(5);

        private const long MAX_FILE_SIZE =
            2 * 1024 * 1024; // 2 MB

        private const long MAX_CACHE_MEMORY_SIZE =
            50 * 1024 * 1024; // 50 MB

        public static Func<DateTime> NowProvider =
            () => DateTime.Now;

        // ═════════════════════════════════════════════════════════════
        // 2. CACHE — STRUCTURE
        // ═════════════════════════════════════════════════════════════

        public static int CacheCount =>
            _fileCache.Count;

        private class CacheItem
        {
            public FileReadResult Result { get; set; }

            // ⏱ Moment où l’entrée cache a été créée
            public DateTime Timestamp { get; set; }

            // 📄 Dernière modification réelle du fichier disque
            public DateTime LastWriteTimeUtc { get; set; }
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

                // 🔒 Fichier verrouillé
                if (IsFileLocked(path))
                    return FileReadResult.Fail("Fichier verrouillé");

                // 🚫 Fichier binaire
                if (IsBinaryFile(path))
                    return FileReadResult.Fail("Fichier binaire non supporté");

                // 🔁 Cache
                if (TryGetFromCache(path, out var cached))
                    return cached;

                // 📦 Lecture fichier
                var (content, fileSize, isPartial) =
                    await ReadContentAsync(path);

                var result = FileReadResult.Success(
                    content,
                    fileSize);

                result.IsPartial = isPartial;

                // 💾 Cache
                AddToCache(path, result);

                return result;
            }
            catch (FileNotFoundException)
            {
                return FileReadResult.Fail("Fichier introuvable");
            }
            catch (DirectoryNotFoundException)
            {
                return FileReadResult.Fail("Dossier introuvable");
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

        private static bool IsFileLocked(string path)
        {
            try
            {
                using var stream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.None);

                return false;
            }
            catch (IOException)
            {
                return true;
            }
        }

        private static bool IsBinaryFile(string path)
        {
            const int sampleSize = 512;

            try
            {
                using var stream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);

                byte[] buffer = new byte[sampleSize];

                int read = stream.Read(buffer, 0, buffer.Length);

                for (int i = 0; i < read; i++)
                {
                    // 🔥 caractère NULL → probablement binaire
                    if (buffer[i] == 0)
                        return true;
                }

                return false;
            }
            catch
            {
                // sécurité : si problème lecture
                // on considère non binaire ici
                return false;
            }
        }

        private static async Task<(string content, long fileSize, bool isPartial)>
    ReadContentAsync(string path)
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
                using var stream = new FileStream(
                    path,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read);

                using var reader = new StreamReader(stream);

                char[] buffer = new char[MAX_FILE_SIZE];

                int read = await reader.ReadBlockAsync(
                    buffer,
                    0,
                    buffer.Length);

                var content = new string(buffer, 0, read);

                content += "\n\n----------------------------------------\n";
                content += "⚠ Fichier tronqué (trop volumineux)";

                return (content, fileSize, true);
            }

            using var fullStream = new FileStream(
                path,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read);

            try
            {
                using var fullReader = new StreamReader(
                    fullStream,
                    new UTF8Encoding(false, false),
                    detectEncodingFromByteOrderMarks: true);

                var fullContent = await fullReader.ReadToEndAsync();

                return (fullContent, fileSize, false);
            }
            catch (DecoderFallbackException)
            {
                // 🔁 fallback UTF16
                fullStream.Position = 0;

                using var utf16Reader = new StreamReader(
                    fullStream,
                    new UnicodeEncoding(false, false, false),
                    detectEncodingFromByteOrderMarks: true);

                var utf16Content = await utf16Reader.ReadToEndAsync();

                return (utf16Content, fileSize, false);
            }
        }

        // ═════════════════════════════════════════════════════════════
        // 5. CACHE — GESTION
        // ═════════════════════════════════════════════════════════════

        private static bool TryGetFromCache(
    string path,
    out FileReadResult result)
        {
            result = FileReadResult.Fail("Cache invalide");

            if (string.IsNullOrWhiteSpace(path))
                return false;

            if (_fileCache.TryGetValue(path, out var cachedItem))
            {
                // 🔥 cache expiré → invalide
                if (NowProvider() - cachedItem.Timestamp >= CACHE_DURATION)
                {
                    _fileCache.TryRemove(path, out _);
                    return false;
                }

                // 🔥 fichier modifié → cache invalide
                try
                {
                    var currentWriteTime =
                        File.GetLastWriteTimeUtc(path);

                    if (currentWriteTime != cachedItem.LastWriteTimeUtc)
                    {
                        _fileCache.TryRemove(path, out _);
                        return false;
                    }
                }
                catch
                {
                    // 🔥 sécurité :
                    // si problème accès disque
                    // on invalide le cache
                    _fileCache.TryRemove(path, out _);
                    return false;
                }

                // 🔥 sécurité supplémentaire
                if (cachedItem.Result == null)
                {
                    _fileCache.TryRemove(path, out _);
                    return false;
                }

                result = cachedItem.Result;
                return true;
            }

            return false;
        }

        private static void AddToCache(
    string path,
    FileReadResult result)
        {
            // 🔒 validation
            if (string.IsNullOrWhiteSpace(path))
                return;

            if (result == null)
                return;

            // ❌ on ne cache jamais les erreurs
            if (!result.IsSuccess)
                return;

            _fileCache[path] = new CacheItem
            {
                Result = result,

                // ⏱ moment création cache
                Timestamp = NowProvider(),

                // 📄 état réel fichier disque
                LastWriteTimeUtc = File.GetLastWriteTimeUtc(path)
            };

            // 🔥 nettoyage cache
            while (
                _fileCache.Count > MAX_CACHE_ITEMS ||
                GetCurrentCacheMemorySize() > MAX_CACHE_MEMORY_SIZE)
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

        private static long GetCurrentCacheMemorySize()
        {
            long totalSize = 0;

            foreach (var item in _fileCache.Values)
            {
                if (item?.Result?.Content != null)
                {
                    totalSize +=
                        item.Result.Content.Length * sizeof(char);
                }
            }

            return totalSize;
        }

        // ═════════════════════════════════════════════════════════════
        // 6. CACHE — API PUBLIQUE
        // ═════════════════════════════════════════════════════════════

        public static bool IsInCache(string path)
        {
            return _fileCache.ContainsKey(path);
        }

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