/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Models                                                ║
║  Fichier : FileReadResult.cs                                         ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter le résultat d’une lecture de fichier                    ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Indiquer si la lecture a réussi                                   ║
║  - Contenir le contenu du fichier                                    ║
║  - Fournir un message d’erreur si nécessaire                         ║
║  - Fournir la taille du fichier                                      ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucun accès disque                                                ║
║  - Aucune logique métier                                             ║
║  - Simple objet de données (DTO)                                     ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Models
{
    public class FileReadResult
    {
        // ═════════════════════════════════════════════════════════════
        // 1. PROPRIÉTÉS
        // ═════════════════════════════════════════════════════════════

        public bool IsSuccess { get; set; }

        public string Content { get; set; } = string.Empty;

        public string ErrorMessage { get; set; } = string.Empty;

        public long FileSize { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 2. MÉTHODES STATIQUES (FACILITATEURS)
        // ═════════════════════════════════════════════════════════════

        public static FileReadResult Success(string content, long fileSize)
        {
            return new FileReadResult
            {
                IsSuccess = true,
                Content = content,
                FileSize = fileSize,
                ErrorMessage = string.Empty
            };
        }

        public static FileReadResult Fail(string message)
        {
            return new FileReadResult
            {
                IsSuccess = false,
                Content = string.Empty,
                FileSize = 0,
                ErrorMessage = message
            };
        }
    }
}