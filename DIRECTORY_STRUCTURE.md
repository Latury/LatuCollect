# 📁 STRUCTURE DU PROJET – LATUCOLLECT (VERSION CIBLE V2)

👉 ⚠️ Cette structure correspond à l’organisation cible
👉 Elle n’est pas entièrement implémentée actuellement
👉 Voir ROADMAP pour l’évolution

---

## 📌 Résumé

Ce document décrit la structure du projet LatuCollect, la différence entre l’état actuel et la structure cible, ainsi que l’organisation des dossiers.

👉 Il sert de guide pour maintenir une architecture cohérente.

---

# 🎯 OBJECTIF

Permettre :

- Navigation rapide
- Compréhension immédiate
- Maintenance facilitée
- Évolutivité propre

---

# 🧩 STRUCTURE PRINCIPALE (CIBLE)

```text
LatuCollect/
│
├── Core/
│   ├── Services/
│   │   ├── Import/
│   │   ├── Reader/
│   │   ├── Collection/
│   │   ├── Statistics/
│   │   ├── Export/
│   │   └── Utils/
│   │
│   ├── Configuration/
│   ├── Logging/
│   ├── Models/
│   ├── Interfaces/ (centralisation future)
│   │   👉 Certaines interfaces existent déjà dans :
│   │      - Configuration/
│   │      - Logging/
│   │   👉 Elles seront centralisées plus tard
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
│       │   👉 Découpage progressif prévu du MainViewModel
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

# ⚠️ IMPORTANT

- Structure cible (pas encore complète)
- Implémentation progressive
- Alignement avec ROADMAP obligatoire
- ✔ Système de simulation supprimé (v0.13.0)
- ✔ Architecture simplifiée

---

# 🧠 DESCRIPTION DES DOSSIERS

## Core/

Contient toute la logique métier

Responsabilités :

- Import des fichiers
- Lecture du contenu
- Assemblage
- Export

👉 Source unique de vérité
👉 Aucune dépendance UI

---

### Utils/

Fonctions utilitaires simples (helpers techniques)

---

### Helpers/

Fonctions d’aide spécifiques au projet

---

### DTOs/

Objets de transfert de données entre services

---

## Configuration/

Centralise :

- Paramètres globaux
- Préférences utilisateur

---

## Logging/

Permet :

- Traçage des actions
- Gestion des erreurs
- Diagnostic

---

## UI/

Contient l’interface WinUI

Responsabilités :

- Affichage
- Interaction utilisateur
- Aperçu

👉 Aucune logique métier
👉 Consomme uniquement le Core

---

## Resources/

- Couleurs
- Styles
- Dimensions

👉 Base pour le système de thèmes

---

## Tests/

- Tests unitaires
- Tests fonctionnels
- Tests ViewModel

---

## Installer/

- Packaging
- Distribution
- Dépendances

---

## Assets/

- Icônes
- Images
- Logo

---

# 🧠 ARCHITECTURE MVVM

```text
View → ViewModel → Core
```

---

# 🧩 FONCTIONNEMENT GLOBAL

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🖥️ UI STRUCTURE

```text
Gauche → Arborescence
Centre → Options
Droite → Aperçu
Bas → Actions
```

---

# 🔄 COMMUNICATION

```text
UI → ViewModel → Core
```

❌ Jamais l’inverse

---

# ⚠️ RÈGLES

- Core ne dépend jamais de UI
- UI peut dépendre de Core
- 1 dossier = 1 responsabilité
- Pas de logique métier dans UI

---

# 🧠 PHILOSOPHIE

- Simplicité
- Lisibilité
- Efficacité

👉 Éviter toute complexité inutile

---

# 🧩 STRUCTURE ACTUELLE (v0.14.0)

```text
LatuCollect.UI.WinUI/
│
├── Assets/
├── Converters/
├── Models/
├── Properties/
├── Services/
├── Settings/
│   ├── Pages/
│   ├── Panels/
│   └── ViewModels/
├── Themes/
├── ViewModels/
│
├── App.xaml
├── MainWindow.xaml
│
├── Documentation/
│   ├── README.md
│   ├── ARCHITECTURE.md
│   ├── UI_GUIDE.md
│   ├── DIRECTORY_STRUCTURE.md
│   ├── GUIDE_UTILISATEUR.md
│   ├── ROADMAP.md
│   ├── PATCH_NOTES.md
│   ├── TESTS.md
│
├── LatuCollect.Core/
│   ├── Configuration/
│   ├── Logging/
│   ├── Models/
│   └── Services/
│       ├── Collection/
│       ├── Export/
│       ├── Import/
│       ├── Reader/
│       └── Statistics/
│
└── LatuCollect.Tests/
    ├── Core/
    │   ├── Collection/
    │   ├── Configuration/
    │   ├── Export/
    │   ├── Import/
    │   ├── Reader/
    │   └── Statistics/
    │
    └── UI/
        └── ViewModels/
            ├── Configuration/
            ├── Exclusions/
            ├── Export/
            ├── Performance/
            ├── Preview/
            ├── Search/
            ├── Selection/
            ├── State/
            └── TreeView/
```

---

# 🔄 DIFFÉRENCES AVEC LA CIBLE

## ✅ ÉLÉMENTS DÉJÀ EN PLACE

✔ Services Core principaux
✔ Logging centralisé
✔ Configuration globale (AppConfig)
✔ UI WinUI structurée
✔ Séparation stricte UI / Core

👉 Base solide déjà fonctionnelle

---

## ❌ Non implémenté

- Utils/
- Interfaces/
- Helpers/
- DTOs/
- Centralisation complète des ressources UI
- Structure finale des tests
- Centralisation complète des tests
- Tests système automatisés
- Installer
- Centralisation complète des Assets
- Système de thèmes avancé

---

# 🧠 ÉTAT DU PROJET

```text
- ✔ Architecture stabilisée
- ✔ Pipeline optimisé
- 🔄 Stabilisation async UI avancée prévue
- 🔄 Split progressif MainViewModel prévu
- ✔ Cache actif
- ✔ Logs intégrés
- ✔ UI stable
- ✔ Séparation UI / Core

- ✔ Système de simulation supprimé
- ✔ Couplage Core/UI réduit

- ✔ Pipeline simplifié
- ✔ Simplification architecture (v0.13.0)
- ✔ Réduction des dépendances inutiles

- ✔ Pipeline preview async stabilisé
- ✔ Chargement progressif UI
- ✔ Persistance expansion TreeView
- ✔ Réduction des refresh inutiles
```

---

# 📌 ÉTAT ACTUEL

- ✔ Core structuré par services
- ✔ Configuration organisée
- ✔ Logging séparé
- ✔ UI WinUI structurée
- ✔ Architecture respectée

👉 Structure largement stabilisée en v0.14.0
👉 Évolution progressive vers la cible

---

# 🎯 OBJECTIF SUIVANT

```text
➡ Finaliser la structure cible
➡ Continuer le découpage des services
➡ Continuer le split progressif MainViewModel
➡ Stabiliser la version 1.0.0
```
