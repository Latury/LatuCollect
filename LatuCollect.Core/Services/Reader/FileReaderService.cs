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
using System.IO;

namespace LatuCollect.Core.Services.Reader
{
    public static class FileReaderService
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. MÉTHODE PUBLIQUE
        // ═════════════════════════════════════════════════════════════════════
        //
        // Lit le contenu d’un fichier :
        // - applique la simulation si activée
        // - gère les erreurs
        // - retourne toujours une string
        //

        public static string ReadFile(string path)
        {
            try
            {
                // 📄 Vérification existence
                if (!File.Exists(path))
                    return "[Fichier introuvable]";

                // 🧪 Simulation (prioritaire)
                string simulated = SimulationService.SimulateRead(path);
                if (simulated != null)
                    return simulated;

                // ✔ Lecture normale
                return File.ReadAllText(path);
            }
            catch (PathTooLongException)
            {
                return "[Chemin trop long]";
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
    }
}