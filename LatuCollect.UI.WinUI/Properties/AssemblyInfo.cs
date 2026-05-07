/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║  Module : UI.WinUI                                                   ║
║  Fichier : AssemblyInfo.cs                                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Déclarer les attributs d’assembly du projet UI.WinUI                ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - Autoriser l’accès aux membres internal pour les tests             ║
║  - Centraliser les attributs assembly                                ║
║                                                                      ║
║  IMPORTANT (ALC) :                                                   ║
║  - Aucun code métier                                                 ║
║  - Configuration assembly uniquement                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/

using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("LatuCollect.Tests")]