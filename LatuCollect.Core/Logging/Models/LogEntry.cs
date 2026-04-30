/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : Core.Logging.Models                                        ║
║  Fichier : LogEntry.cs                                               ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représente une entrée de log dans l'application                     ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker les informations d’un log                                 ║
║  - Fournir une structure claire et exploitable                       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Modèle Core pur                                                   ║
║  - Aucune dépendance UI                                              ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - LogLevel                                                          ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System;

namespace LatuCollect.Core.Logging.Models
{
    public class LogEntry
    {
        // ═════════════════════════════════════════════════════════════
        // 1. DONNÉES BRUTES (CORE)
        // ═════════════════════════════════════════════════════════════
        //
        // Informations fondamentales du log
        //

        public DateTime Timestamp { get; }

        public LogLevel Level { get; }

        public string Message { get; }

        public string? Context { get; }


        // ═════════════════════════════════════════════════════════════
        // 2. DONNÉES FORMATÉES
        // ═════════════════════════════════════════════════════════════
        //
        // ⚠ Préparé pour affichage (UI)
        // TODO (refactor futur) : déplacer le formatage côté UI
        //

        public string Date { get; }


        // ═════════════════════════════════════════════════════════════
        // 3. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public LogEntry(LogLevel level, string message, string? context = null)
        {
            Timestamp = DateTime.Now;

            // Format lisible : jour/mois/année + heure
            Date = Timestamp.ToString("dd/MM/yyyy HH:mm:ss");

            Level = level;
            Message = message;
            Context = context;
        }
    }
}