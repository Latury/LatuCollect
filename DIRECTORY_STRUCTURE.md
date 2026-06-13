# 1. 📁 STRUCTURE DU PROJET – LATUCOLLECT (VERSION CIBLE V2)

👉 ⚠️ Cette structure correspond à l’organisation cible

👉 Elle n’est pas entièrement implémentée actuellement

👉 Voir [ROADMAP](./ROADMAP.md) pour l’évolution

---

## 📌 Résumé

Ce document décrit la structure du projet LatuCollect, la différence entre l’état actuel et la structure cible, ainsi que l’organisation des dossiers.

👉 Il sert de guide pour maintenir une architecture cohérente.

---

# 2. 🎯 OBJECTIF

Permettre :

- Navigation rapide
- Compréhension immédiate
- Maintenance facilitée
- Évolutivité propre

---

# 3. 🧩 STRUCTURE PRINCIPALE (CIBLE)

```text
LatuCollect/
│
├── Core/
│   ├── Services/
│   │   ├── Import/
│   │   ├── Reader/
│   │   ├── Statistics/
│   │   ├── Export/
│   │   └── Utils/
│   │
│   ├── Configuration/
│   ├── Logging/
│   ├── Models/
│   ├── Interfaces/
│   │   👉 Centralisation progressive
│   │   👉 Plusieurs interfaces existent déjà
│   │      (Import, Export, Logging, Configuration)
│   │
│   ├── Helpers/
│   └── DTOs/
│
├── UI/
│   └── WinUI/
│       ├── Views/
│       ├── ViewModels/
│       │   ├── TreeView/
│       │   ├── Preview/
│       │   ├── Export/
│       │   ├── Logs/
│       │   ├── Search/
│       │   └── Settings/
│       │
│       │   👉 LogsViewModel extrait (v0.15.0)
│       │   👉 Découpage progressif du MainViewModel en cours
│       │
│       ├── Models/
│       ├── Converters/
│       ├── Services/
│       └── Themes/
│
├── Resources/
├── Tests/
├── Installer/
├── Assets/
│
├── README.md
├── ARCHITECTURE.md
├── UI_GUIDE.md
├── DIRECTORY_STRUCTURE.md
├── GUIDE_UTILISATEUR.md
├── ROADMAP.md
├── PATCH_NOTES.md
├── TESTS.md
```

---

# 4. ⚠️ IMPORTANT

- Structure cible (pas encore complète)
- Implémentation progressive
- Alignement avec la ROADMAP obligatoire
- ✔ Système de simulation supprimé (v0.13.0)
- ✔ Architecture simplifiée

---

# 5. 🧠 DESCRIPTION DES DOSSIERS

## 5.1 🧠 Core/

Contient toute la logique métier.

### 📋 Responsabilités

- Import des fichiers
- Lecture du contenu
- Assemblage
- Export

👉 Source unique de vérité

👉 Aucune dépendance UI

### 🛠️ Utils/

Fonctions utilitaires génériques.

👉 Helpers techniques réutilisables

### 🔧 Helpers/

Fonctions d’aide spécifiques au projet.

👉 Logique d'assistance interne

### 📦 DTOs/

Objets de transfert de données entre services.

👉 Échanges de données structurés

---

## 5.2 ⚙️ Configuration/

Centralise :

- Paramètres globaux
- Préférences utilisateur

### 📋 Éléments principaux

- AppConfig
- UserConfig

---

## 5.3 🧾 Logging/

Permet :

- Traçage des actions
- Gestion des erreurs
- Diagnostic

👉 Utilisé par l’ensemble du pipeline

---

## 5.4 🖥️ UI/

Contient l’interface WinUI.

### 📋 Responsabilités

- Affichage
- Interaction utilisateur
- Aperçu

👉 Aucune logique métier

👉 Consomme uniquement le Core

---

## 5.5 🎨 Resources/

- Couleurs
- Styles
- Dimensions

👉 Base pour le système de thèmes

---

## 5.6 🧪 Tests/

- Tests unitaires
- Tests fonctionnels
- Tests ViewModel

👉 Couverture de tests en constante évolution

---

## 5.7 📦 Installer/

- Packaging
- Distribution
- Dépendances

👉 Prévu dans les versions futures

---

## 5.8 🖼️ Assets/

- Icônes
- Images
- Logo

---

# 6. 🧠 ARCHITECTURE MVVM

View → ViewModel → Core

---

# 7. 🧩 FONCTIONNEMENT GLOBAL

Importer → Sélectionner → Aperçu → Exporter

---

# 8. 🖥️ UI STRUCTURE

Gauche → Arborescence

Centre → Options

Droite → Aperçu

Bas → Actions

---

# 9. 🔄 COMMUNICATION

UI → ViewModel → Core

❌ Jamais l’inverse

---

# 10. ⚠️ RÈGLES

- Core ne dépend jamais de UI
- UI peut dépendre de Core
- 1 dossier = 1 responsabilité
- Pas de logique métier dans UI

---

# 11. 🧠 PHILOSOPHIE

- Simplicité
- Lisibilité
- Efficacité

👉 Éviter toute complexité inutile

---

# 12. 🧩 STRUCTURE ACTUELLE (v0.16.0)

```text
LatuCollect.Core/
├── Configuration/
│   ├── Constants/
│   ├── Interfaces/
│   ├── Models/
│   └── Services/
├── Logging/
│   ├── Interfaces/
│   ├── Models/
│   └── Services/
├── Models/
│   └── Export/
└── Services/
    ├── Export/
    ├── Import/
    ├── Reader/
    └── Statistics/

LatuCollect.UI.WinUI/
├── Assets/
├── Converters/
├── Models/
│   └── Logs/
├── Properties/
├── Settings/
│   ├── Pages/
│   ├── Panels/
│   └── ViewModels/
├── Themes/
└── ViewModels/
    ├── MainViewModel.cs
    ├── Logs/
    │   └── LogsViewModel.cs
    ├── TreeView/
    │   └── TreeViewViewModel.cs
    ├── Preview/
    │   └── PreviewViewModel.cs
    └── Settings/
        └── SettingsViewModel.cs

LatuCollect.Tests/
├── Core/
│   ├── Configuration/
│   ├── Export/
│   ├── Import/
│   ├── Reader/
│   └── Statistics/
├── Helpers/
└── UI/
    ├── Logs/
    ├── TreeView/
    └── ViewModels/
        ├── Configuration/
        ├── Exclusions/
        ├── Export/
        ├── Performance/
        ├── Preview/
        ├── Search/
        ├── Selection/
        └── State/
```

---

# 13. 🔄 DIFFÉRENCES AVEC LA CIBLE

## ✅ ÉLÉMENTS DÉJÀ EN PLACE

✔ Services Core principaux

✔ Logging centralisé

✔ Configuration globale

✔ Séparation AppConfig / UserConfig

✔ UI WinUI structurée

✔ Séparation stricte UI / Core

✔ Première étape du split MainViewModel

✔ LogsViewModel extrait

✔ TreeViewViewModel créé

✔ PreviewViewModel créé

✔ SettingsViewModel créé

✔ PreviewViewModel créé

✔ SettingsViewModel créé

✔ Réduction progressive des responsabilités MainViewModel

✔ Centralisation progressive des responsabilités Preview

✔ Centralisation progressive des responsabilités TreeView

👉 Base solide déjà fonctionnelle

---

## ❌ NON IMPLÉMENTÉ

```text
- Utils/
- Interfaces/
- Helpers/
- DTOs/
- Centralisation complète des ressources UI
- Structure finale des tests
- Tests système automatisés
- Installer
- Centralisation complète des Assets
- Système de thèmes avancé
- ExportViewModel
```

## 🟡 PARTIELLEMENT IMPLÉMENTÉ

- PreviewViewModel
  👉 Créé et utilisé
  👉 États Preview migrés
  👉 États techniques migrés
  👉 Génération Preview partiellement migrée
  👉 Extraction avancée réalisée en v0.16.0
  👉 Migration complète encore en cours

- SettingsViewModel
  👉 Créé et intégré
  👉 États Settings migrés
  👉 Préparation des futures redirections réalisée en v0.16.0
  👉 Migration complète encore en cours
