/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Models                                                ║
║  Fichier : ImportResult.cs                                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Résultat structuré d’un import                                      ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Collections.Generic;

namespace LatuCollect.Core.Models
{
    public class ImportResult
    {
        public List<FileNode> Nodes { get; set; } = new();

        public int TotalNodes { get; set; }

        public bool IsPartial { get; set; }

        public string Message { get; set; } = "";
    }
}