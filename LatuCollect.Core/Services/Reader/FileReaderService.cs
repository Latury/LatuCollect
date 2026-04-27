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

using LatuCollect.Core.Simulation;
using System;
using System.Collections.Generic;
using System.IO;

namespace LatuCollect.Core.Services.Reader
{
    public static class FileReaderService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS PRIVÉS — CACHE
        // ═════════════════════════════════════════════════════════════
        //
        // Cache mémoire :
        // clé   → chemin fichier
        // valeur → contenu fichier
        //

        private static readonly Dictionary<string, string> _fileCache = new();


        // ═════════════════════════════════════════════════════════════
        // 2. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════
        //
        // Lecture sécurisée d’un fichier texte
        //

        public static string ReadFile(string path)
        {
            // 🔹 Sécurité minimale
            if (string.IsNullOrWhiteSpace(path))
                return "[Chemin invalide]";

            try
            {
                // 📄 Vérification existence
                if (!File.Exists(path))
                    return "[Fichier introuvable]";

                // 🧪 Simulation (prioritaire)
                var simulated = SimulationService.SimulateRead(path);
                if (simulated != null)
                    return simulated;

                // 🔁 Cache
                if (_fileCache.TryGetValue(path, out var cachedContent))
                {
                    return cachedContent;
                }

                // 📖 Lecture fichier
                var content = File.ReadAllText(path);

                // 💾 Mise en cache
                _fileCache[path] = content;

                return content;
            }
            catch (PathTooLongException)
            {
                return "[Chemin trop long]";
            }
            catch (UnauthorizedAccessException)
            {
                return "[Accès refusé]";
            }
            catch (IOException)
            {
                return "[Erreur de lecture]";
            }
            catch (Exception)
            {
                return "[Erreur inconnue]";
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 3. MÉTHODES UTILITAIRES (CACHE)
        // ═════════════════════════════════════════════════════════════
        //
        // Gestion du cache mémoire
        //

        public static void ClearCache()
        {
            _fileCache.Clear();
        }

        public static void RemoveFromCache(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                _fileCache.Remove(path);
        }
    }
}