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

        // ═════════════════════════════════════════════════════════════════════
        // 1. CONSTANTES / LIMITES
        // ═════════════════════════════════════════════════════════════════════

        private const int MAX_NODES = 1000;
        private const int MAX_DEPTH = 10;

        // ═════════════════════════════════════════════════════════════════════
        // 2. CONFIGURATION
        // ═════════════════════════════════════════════════════════════════════
        //
        // Contient :
        // - Instance de AppConfig
        // - Permet d'accéder aux paramètres globaux
        //

        private readonly AppConfig _config;

        // ═════════════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════════════
        //
        // Injection de la configuration globale
        //

        public FileImportService(AppConfig config)
        {
            _config = config;
        }

        // ═════════════════════════════════════════════════════════════════════
        // 4. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════════════
        //
        // Point d’entrée principal :
        // - charge toute l’arborescence
        //

        public async Task<List<FileNode>> LoadTreeAsync(string rootPath)
        {
            return await Task.Run(() =>
            {
                var result = new List<FileNode>();
                int count = 0;

                if (!Directory.Exists(rootPath))
                    return result;

                var rootNode = CreateNode(rootPath, 0, ref count);

                if (rootNode != null)
                    result.Add(rootNode);

                return result;
            });
        }


        // ═════════════════════════════════════════════════════════════════════
        // 5. CONSTRUCTION DE L’ARBORESCENCE
        // ═════════════════════════════════════════════════════════════════════
        //
        // Méthode récursive :
        // - crée les nodes
        // - applique les limites
        // - applique les exclusions
        //

        private FileNode CreateNode(string path, int depth, ref int count)
        {
            if (depth > MAX_DEPTH || count > MAX_NODES)
                return null;

            var node = new FileNode
            {
                Name = Path.GetFileName(path),
                Path = path
            };

            count++;

            if (Directory.Exists(path))
            {
                // 📁 Dossiers
                foreach (var dir in Directory.GetDirectories(path))
                {
                    if (count > MAX_NODES)
                        break;

                    try
                    {
                        string folderName = Path.GetFileName(dir);

                        if (_config.ExcludedFolders.Contains(folderName))
                            continue;

                        var child = CreateNode(dir, depth + 1, ref count);

                        if (child != null)
                            node.Children.Add(child);
                    }
                    catch
                    {
                        // Ignoré volontairement (accès refusé etc.)
                    }
                }

                // 📄 Fichiers
                foreach (var file in Directory.GetFiles(path))
                {
                    if (count > MAX_NODES)
                        break;

                    try
                    {
                        node.Children.Add(new FileNode
                        {
                            Name = Path.GetFileName(file),
                            Path = file
                        });

                        count++;
                    }
                    catch
                    {
                        // Ignoré volontairement
                    }
                }
            }

            return node;
        }
    }
}