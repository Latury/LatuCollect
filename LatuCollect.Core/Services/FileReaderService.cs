/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core                                                       ║
║  Fichier : FileReaderService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Lire le contenu des fichiers                                        ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Lire un fichier texte                                             ║
║  - Retourner son contenu brut                                        ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - System.IO                                                         ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.IO;

namespace LatuCollect.Core.Services
{
    public static class FileReaderService
    {
        public static string ReadFile(string path)
        {
            if (!File.Exists(path))
                return "[Fichier introuvable]";

            return File.ReadAllText(path);
        }
    }
}