# 📁 STRUCTURE DU PROJET – LATUCOLLECT (VERSION CIBLE)

Ce document décrit la structure cible du projet LatuCollect.

👉 ⚠️ Structure cible (non entièrement implémentée à ce jour)
👉 Elle correspond à l’organisation prévue pour la version stable (v1.0.0)

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
│   │   ├── Collection/
│   │   │   └── FileCollectionService.cs
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
│       │   └── MainWindow.xaml
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
├── FEUILLE_DE_ROUTE.md
├── PATCH_NOTES.md
├── TESTS.md
├── LICENSE.md
├── .editorconfig
├── .gitignore
```

---

# ⚠️ IMPORTANT

👉 Certains services et dossiers sont prévus mais seront implémentés progressivement (voir feuille de route)

---

# 🧠 Description des dossiers

## Core/

Contient toute la logique métier.

👉 Responsabilités :

- Importer les fichiers
- Lire le contenu
- Assembler les données
- Exporter

👉 Aucune dépendance UI

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

- Debug
- Suivi des erreurs
- Diagnostic

---

## UI/

Contient l’interface utilisateur WinUI.

👉 Responsabilités :

- Affichage
- Interaction utilisateur
- Aperçu du document

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
- Logo application

---

# 🧩 Fonctionnement global

```text
Importer → Sélectionner → Aperçu → Exporter
```

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
- Ne jamais modifier cette structure sans mettre à jour la documentation

---

# 🧠 Philosophie

- Simplicité
- Lisibilité
- Efficacité

👉 Éviter toute complexité inutile
