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
        // ═════════════════════════════════════════════════════════════
        // 1. CHAMPS / CONSTANTES
        // ═════════════════════════════════════════════════════════════
        //
        // (Aucun champ — service stateless)
        //


        // ═════════════════════════════════════════════════════════════
        // 2. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════
        //
        // Met à jour les statistiques globales
        // à partir d’un fichier (contenu + chemin)
        //

        public static void UpdateStatistics(
            StatisticsResult stats,
            string content,
            string path)
        {
            // 🔹 Sécurité minimale
            if (stats == null)
                return;

            stats.FileCount++;

            stats.TotalCharacters += content?.Length ?? 0;
            stats.TotalLines += CountLines(content);

            // ⚠ accès disque → peut échouer
            try
            {
                var fileInfo = new FileInfo(path);
                stats.TotalSizeBytes += fileInfo.Length;
            }
            catch
            {
                // volontairement ignoré (pas critique)
            }
        }


        // ═════════════════════════════════════════════════════════════
        // 3. MÉTHODES PRIVÉES
        // ═════════════════════════════════════════════════════════════
        //
        // Calcul interne :
        // - nombre de lignes
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