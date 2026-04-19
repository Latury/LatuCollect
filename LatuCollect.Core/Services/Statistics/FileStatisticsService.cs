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

using System.IO;
using LatuCollect.Core.Services.Export;

namespace LatuCollect.Core.Services.Statistics
{
    public static class FileStatisticsService
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════════════
        //
        // Met à jour les statistiques globales
        // à partir du contenu d’un fichier
        //

        public static void UpdateStatistics(
            StatisticsResult stats,
            string content,
            string path)
        {
            stats.FileCount++;

            stats.TotalCharacters += content.Length;
            stats.TotalLines += CountLines(content);

            var fileInfo = new FileInfo(path);
            stats.TotalSizeBytes += fileInfo.Length;
        }


        // ═════════════════════════════════════════════════════════════════════
        // 2. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════════════
        //
        // Méthodes internes :
        // - calcul rapide du nombre de lignes
        //

        private static int CountLines(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            int count = 1;

            foreach (char c in text)
            {
                if (c == '\n')
                    count++;
            }

            return count;
        }
    }
}