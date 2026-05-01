/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Services.Statistics                                   ║
║  Fichier : FileStatisticsService.cs                                  ║
║                                                                      ║
║  Rôle :                                                              ║
║  Calculer les statistiques des fichiers                              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Compter les fichiers traités                                      ║
║  - Calculer le nombre de lignes                                      ║
║  - Calculer le nombre de caractères                                  ║
║  - Calculer la taille totale                                         ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - System.IO                                                         ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Service Core pur                                                  ║
║  - Aucune dépendance UI                                              ║
║  - Ne contient aucune logique d’export                               ║
║  - Ne lit pas les fichiers (responsabilité du Reader)                ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using LatuCollect.Core.Models;

namespace LatuCollect.Core.Services.Statistics
{
    public static class FileStatisticsService
    {
        // ═════════════════════════════════════════════════════════════
        // 1. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════

        public static void UpdateStatistics(
            StatisticsResult stats,
            string content,
            long fileSize)
        {
            if (stats == null)
                return;

            // 🔹 Sécurisation contenu
            content ??= string.Empty;

            stats.FileCount++;

            stats.TotalCharacters += content.Length;
            stats.TotalLines += CountLines(content);
            stats.TotalSizeBytes += fileSize;
        }


        // ═════════════════════════════════════════════════════════════
        // 2. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════

        private static int CountLines(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            int count = 0;

            foreach (char c in text)
            {
                if (c == '\n')
                    count++;
            }

            // 🔹 Si texte non vide → au moins 1 ligne
            return count + 1;
        }
    }
}