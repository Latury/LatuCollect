/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║                                                                      ║
║  Module : UI.WinUI.Models.Logs                                       ║
║  Fichier : LogFilter.cs                                              ║
║                                                                      ║
║  Rôle :                                                              ║
║  Représenter les filtres disponibles pour l'affichage des logs       ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Modèle UI uniquement                                              ║
║  - Aucune logique métier                                             ║
║  - Aucune dépendance Core                                            ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

namespace LatuCollect.UI.WinUI.Models.Logs
{
    public enum LogFilter
    {
        All,
        Info,
        Warning,
        Error
    }
}