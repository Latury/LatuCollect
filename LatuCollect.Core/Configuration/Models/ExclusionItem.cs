/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : Core.Configuration.Models                                  ║
║  Fichier : ExclusionItem.cs                                          ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter un élément d’exclusion                                  ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Stocker le nom du dossier/fichier exclu                           ║
║  - Indiquer si l’exclusion est protégée                              ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Données uniquement                                                ║
║  - Aucune logique métier                                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.Core.Configuration.Models
{
    public class ExclusionItem
    {
        // ═════════════════════════════════════════════════════════════
        // 1. PROPRIÉTÉS
        // ═════════════════════════════════════════════════════════════
        //
        // - Nom du dossier ou chemin exclu
        // - Exemple : "bin", "obj", ".git"
        //

        public string Name { get; set; } = string.Empty;

        // Indique si l’exclusion est protégée
        // true  → non supprimable
        // false → supprimable
        public bool IsProtected { get; set; }

        // Indique si l’exclusion est un dossier (true) ou un fichier (false)
        public bool IsDirectory { get; set; }


        // ═════════════════════════════════════════════════════════════
        // 2. CONSTRUCTEUR
        // ═════════════════════════════════════════════════════════════

        public ExclusionItem(
    string name,
    bool isProtected = false,
    bool isDirectory = false)
        {
            Name = name;
            IsProtected = isProtected;
            IsDirectory = isDirectory;
        }


        // ═════════════════════════════════════════════════════════════
        // 3. MÉTHODES
        // ═════════════════════════════════════════════════════════════

        public override string ToString()
        {
            return Name;
        }
    }
}