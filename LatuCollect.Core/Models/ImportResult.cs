/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Models                                                ║
║  Fichier : ImportResult.cs                                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Résultat structuré d’un import                                      ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Contenir les nodes importés                                       ║
║  - Indiquer le nombre total de nodes                                 ║
║  - Indiquer si l’import est partiel                                  ║
║  - Fournir un message associé                                        ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Données uniquement                                                ║
║  - Aucun accès disque                                                ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Collections.Generic;

namespace LatuCollect.Core.Models
{
    public class ImportResult
    {
        // ═════════════════════════════════════════════════════════════
        // 1. DONNÉES IMPORTÉES
        // ═════════════════════════════════════════════════════════════
        //
        // Arborescence générée
        //

        public List<FileNode> Nodes { get; set; } = new();


        // ═════════════════════════════════════════════════════════════
        // 2. STATISTIQUES
        // ═════════════════════════════════════════════════════════════
        //
        // Nombre total de nodes traités
        //

        public int TotalNodes { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 3. ÉTAT IMPORT
        // ═════════════════════════════════════════════════════════════
        //
        // Indique si l’import est incomplet (limites atteintes)
        //

        public bool IsPartial { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 4. MESSAGE ASSOCIÉ
        // ═════════════════════════════════════════════════════════════
        //
        // Exemple :
        // ⚠ Projet volumineux — affichage partiel
        //

        public string Message { get; set; } = "";
    }
}