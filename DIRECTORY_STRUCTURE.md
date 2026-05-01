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
- ❌ Aucun système de simulation dans la version finale

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

👉 Aucun test lié à la simulation

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

# 🧩 STRUCTURE ACTUELLE (v0.10.0)

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
├── Services/
│   ├── Import/
│   ├── Reader/
│   ├── Collection/
│   ├── Export/
│   └── Statistics/
├── Models/
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
- Themes/
- Resources/
- Tests structurés
- Installer
- Assets global

---

# 🧠 ÉTAT DU PROJET

```text
✔ Architecture stabilisée
✔ Pipeline optimisé
✔ Cache actif
✔ Logs intégrés
✔ UI stable
✔ Séparation UI / Core
```

---

# 📌 ÉTAT ACTUEL

- ✔ Core structuré par services
- ✔ Configuration organisée
- ✔ Logging séparé
- ✔ UI WinUI structurée
- ✔ Architecture respectée

👉 Structure stable en v0.10.0
👉 Évolution progressive vers la cible

---

# 🎯 OBJECTIF SUIVANT

```text
➡ Finaliser la structure cible
➡ Continuer le découpage des services
➡ Stabiliser la version 1.0.0
```
