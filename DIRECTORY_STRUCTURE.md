# 📁 STRUCTURE DU PROJET – LATUCOLLECT (VERSION CIBLE)

👉 ⚠️ Cette structure correspond à l’organisation cible du projet (v1.0.0)

👉 Elle n’est pas entièrement implémentée actuellement.

👉 Certains dossiers et services seront ajoutés progressivement (voir ROADMAP)

👉 ⚠️ Les noms actuels des projets (`LatuCollect.Core`, `LatuCollect.UI.WinUI`) seront simplifiés en `Core` et `UI` dans la version cible.

---

# 🎯 Objectif

Permettre :

- ✅ Une navigation rapide
- ✅ Une compréhension immédiate
- ✅ Une maintenance facilitée
- ✅ Une évolutivité propre

---

# 🧩 Structure principale

```text
LatuCollect/
│
├── Core/
│   ├── Services/
│   │   ├── Import/
│   │   │   └── FileImportService.cs
│   │   │
│   │   ├── Reader/
│   │   │   └── FileReaderService.cs
│   │   │
│   │   ├── Collection/
│   │   │   └── FileCollectionService.cs
│   │   │
│   │   ├── Statistics/
│   │   │   └── FileStatisticsService.cs
│   │   │
│   │   ├── Export/
│   │   │   └── FileExportService.cs
│   │   │
│   │   └── Utils/
│   │       └── ClipboardService.cs
│   │
│   ├── Simulation/
│   │   ├── SimulationConfig.cs
│   │   ├── SimulationService.cs
│   │   └── Scenarios/
│   │       ├── LargeFiles/
│   │       ├── CorruptedFiles/
│   │       ├── EmptyFiles/
│   │       └── AccessErrors/
│   │
│   ├── Configuration/
│   │   └── AppConfig.cs
│   │
│   ├── Logging/
│   │   └── LoggerService.cs
│   │
│   ├── Models/
│   ├── Interfaces/
│   ├── Helpers/
│   └── DTOs/ (Optionnel)
│
├── UI/
│   └── WinUI/
│       ├── Views/
│       │   ├── MainWindow.xaml
│       │   └── MainWindow.xaml.cs
│       │
│       ├── ViewModels/
│       │   └── MainViewModel.cs
│       │
│       ├── Models/
│       │   └── FileNode.cs
│       │
│       ├── Converters/
│       │   ├── BooleanToVisibilityConverter.cs
│       │   └── BoolToIconConverter.cs
│       │   └── InverseBooleanToVisibilityConverter.cs
│       │
│       ├── Services/
│       │   └── UiSimulationService.cs
│       │
│       └── Themes/
│           ├── Light/
│           └── Dark/
│
├── Resources/
│   ├── Colors/
│   ├── Styles/
│   └── Dimensions/
│
├── Tests/
│   ├── Unit/
│   └── Simulation/
│
├── Installer/
│   ├── Setup/
│   └── Assets/
│
├── Assets/
│   ├── Icons/
│   └── Images/
│
├── README.md
├── UI_GUIDE.md
├── ARCHITECTURE.md
├── GUIDE_UTILISATEUR.md
├── ROADMAP.md
├── PATCH_NOTES.md
├── TESTS.md
├── LICENSE.md
├── .editorconfig
├── .gitignore
```

---

# ⚠️ IMPORTANT

👉 Cette structure est une cible.

👉 Tous les éléments ne sont pas encore implémentés.

👉 Se référer à la ROADMAP pour suivre l’évolution.

👉 Les services actuels seront progressivement répartis dans les dossiers :
Import / Collection / Export.

👉 Sera utilisé pour le theming (non implémenté actuellement)
Themes/
├── Light/
└── Dark/

---

# 🧠 Description des dossiers

## Core/

Contient toute la logique métier.

👉 Responsabilités :

- Importer les fichiers
- Lire le contenu
- Assembler les données
- Exporter

👉 Assemblage du contenu et structuration centralisés dans `FileExportService`
👉 Source unique de vérité pour aperçu et export

👉 Aucune dépendance UI

👉 Contient également la construction du contenu final (aperçu/export)

---

## Simulation/

Permet de simuler des cas complexes pour les tests.

👉 Objectifs :

- Tester les erreurs
- Simuler des gros projets
- Simuler des comportements extrêmes

👉 Activable via configuration (true / false)
👉 Jamais actif en production

---

## Configuration/

Centralise les paramètres globaux de l’application.

👉 Contient :

- Format par défaut
- Activation simulation
- Paramètres utilisateur

---

## Logging/

Permet de tracer les actions et erreurs.

👉 Utilisé pour :

- Actions
- Erreurs
- Diagnostic

---

## UI/

Contient l’interface utilisateur WinUI.

👉 Responsabilités :

- Affichage
- Interaction utilisateur
- Aperçu du document

👉 Ne contient aucune logique d’assemblage
👉 Consomme uniquement le contenu généré par le Core

---

## 🧠 Architecture MVVM

Structure UI :

```text
View → ViewModel → Core
```

- View = XAML
- ViewModel = logique UI
- Core = logique métier

---

## Resources/

Centralise les ressources visuelles.

👉 Contient :

- Couleurs
- Styles
- Dimensions

👉 Utilisé pour le theming (Dark / Light)

---

## Tests/

Contient les tests du projet.

- Unit → tests unitaires
- Simulation → scénarios simulés

---

## Installer/

Contient tout ce qui concerne la distribution.

👉 Responsabilités :

- Création installateur
- Gestion dépendances
- Packaging application

---

## Assets/

Contient les éléments visuels.

👉 Contient :

- Icônes
- Images
- Logo

---

# 🧩 Fonctionnement global

```text
Importer → Sélectionner → Aperçu → Exporter
```

👉 Le contenu est construit une seule fois dans le Core

---

# 🖥️ Interface utilisateur

```text
Gauche → Arborescence projet
Centre → Options (format)
Droite → Aperçu
Bas → Export
```

---

# 🔄 Communication

```text
UI → ViewModel → Core
```

❌ Jamais l’inverse

---

# ⚠️ Règles importantes

- Core ne dépend jamais de UI
- UI peut dépendre de Core
- 1 dossier = 1 responsabilité
- Aucune logique métier dans UI
- Documentation alignée avec la ROADMAP

---

# 🧠 Philosophie

- Simplicité
- Lisibilité
- Efficacité

👉 Éviter toute complexité inutile

---

---

# 🧩 Structure actuelle (v0.9.0)

👉 Cette section reflète l’état réel du projet actuellement.

```text
LatuCollect/
│
├── LatuCollect.Core/
│   ├── Configuration/
│   │   └── AppConfig.cs
│   │
│   ├── Models/
│   │   └── FileNode.cs
│   │
│   ├── Services/
│   │   ├── Import/
│   │   │   └── FileImportService.cs
│   │   ├── Reader/
│   │   │   └── FileReaderService.cs
│   │   ├── Collection/
│   │   │   └── FileCollectionService.cs
│   │   ├── Export/
│   │   │   └── FileExportService.cs
│   │
│   └── Simulation/
│       ├── SimulationConfig.cs
│       └── SimulationService.cs
│
├── LatuCollect.UI.WinUI/
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   │
│   ├── ViewModels/
│   │   └── MainViewModel.cs
│   │
│   ├── Models/
│   │   └── FileNode.cs
│   │
│   ├── Services/
│   │   └── UiSimulationService.cs
│   │
│   ├── Converters/
│   │   ├── BooleanToVisibilityConverter.cs
│   │   ├── BoolToIconConverter.cs
│   │   └── InverseBooleanToVisibilityConverter.cs
│   │
│   └── Assets/
│
├── README.md
├── ARCHITECTURE.md
├── DIRECTORY_STRUCTURE.md
├── UI_GUIDE.md
├── GUIDE_UTILISATEUR.md
├── ROADMAP.md
├── PATCH_NOTES.md
├── TESTS.md
```

---

# 🔄 Différences avec la structure cible

👉 Ce qui n’est PAS encore implémenté :

- ❌ Dossier `Utils/` (prévu)
- ❌ Dossier `Logging/`
- ❌ Dossier `Interfaces/`
- ❌ Dossier `Helpers/`
- ❌ Dossier `DTOs/`
- ❌ Dossier `Themes/`
- ❌ Dossier `Resources/`
- ❌ Dossier `Tests/` structuré
- ❌ Dossier `Installer/`
- ❌ Dossier `Assets/` global (hors UI)

---

👉 Différence de nommage :

- Actuel :
  - `LatuCollect.Core`
  - `LatuCollect.UI.WinUI`

- Cible :
  - `Core`
  - `UI`

---

````md
# 🧠 État du projet

```text
✔ Architecture stabilisée (v0.9.0)
✔ Services principaux en place (Import / Reader / Collection / Export / Statistics)
✔ Séparation UI / Core respectée
✔ Optimisation globale du pipeline
✔ Mise en cache des fichiers (lecture + preview)
✔ Réduction des recalculs inutiles
✔ Séparation des statistiques (FileStatisticsService)
✔ Base solide pour évolution vers v1.0.0
```
````

---

# 🎯 Objectif prochain

```text
➡ Continuer le découpage des services
➡ Finaliser la séparation complète des responsabilités
➡ Implémenter les dossiers manquants progressivement
```

---
