/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core                                                       ║
║  Fichier : FileExportService.cs                                      ║
║                                                                      ║
║  Rôle :                                                              ║
║  Écrire le contenu final dans un fichier                             ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Écrire du texte dans un fichier                                   ║
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
    public static class FileExportService
    {
        public static void Export(string path, string content)
        {
            File.WriteAllText(path, content);
        }
    }
}