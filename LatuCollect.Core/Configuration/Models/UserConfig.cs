/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration.Models                                  ║
║  Fichier : UserConfig.cs                                             ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter la configuration utilisateur persistée                  ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker les préférences utilisateur                               ║
║  - Être sérialisée en JSON                                           ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Données uniquement                                                ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Collections.Generic;

namespace LatuCollect.Core.Configuration.Models
{
    public class UserConfig
    {

        // ═════════════════════════════════════════════════════════════════════
        // 1. PRÉFÉRENCES UTILISATEUR
        // ═════════════════════════════════════════════════════════════════════
        //
        // Contient :
        // - Paramètres personnalisables par l’utilisateur
        //

        // ─────────────────────────────────────────────
        // 📄 FORMAT EXPORT
        // ─────────────────────────────────────────────

        public string DefaultFormat { get; set; } = ".txt";


        // ─────────────────────────────────────────────
        // 🧑‍💻 MODE DÉVELOPPEUR
        // ─────────────────────────────────────────────

        public bool IsDeveloperMode { get; set; } = false;


        // ─────────────────────────────────────────────
        // 📁 DOSSIERS EXCLUS
        // ─────────────────────────────────────────────

        public List<string> ExcludedFolders { get; set; } = new();

        // ─────────────────────────────────────────────
        // 📂 DERNIER DOSSIER
        // ─────────────────────────────────────────────

        public string LastOpenedFolder { get; set; } = string.Empty;

        public bool AutoLoadLastFolder { get; set; } = false;


        // ─────────────────────────────────────────────
        // 👁️ APERÇU
        // ─────────────────────────────────────────────

        public int PreviewMaxFiles { get; set; } = 20;


        // ─────────────────────────────────────────────
        // 🎨 THÈME (FUTUR)
        // ─────────────────────────────────────────────

        public string Theme { get; set; } = "Light";
    }
}