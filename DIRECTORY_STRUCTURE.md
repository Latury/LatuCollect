# 📁 STRUCTURE DU PROJET – LATUCOLLECT (VERSION CIBLE)

👉 ⚠️ Cette structure correspond à l’organisation cible (v1.0.0)
👉 Elle n’est pas entièrement implémentée actuellement
👉 Voir ROADMAP pour l’évolution

---

# 🎯 OBJECTIF

Permettre :

- Navigation rapide
- Compréhension immédiate
- Maintenance facilitée
- Évolutivité propre

---

# 🧩 STRUCTURE PRINCIPALE (CIBLE)

```texte
LatuCollect/
│
├── Core/
│ ├── Services/
│ │ ├── Import/
│ │ ├── Reader/
│ │ ├── Collection/
│ │ ├── Statistics/
│ │ ├── Export/
│ │ └── Utils/
│ │
│ ├── Simulation/
│ │ ├── Scenarios/
│ │
│ ├── Configuration/
│ ├── Logging/
│ ├── Models/
│ ├── Interfaces/
│ ├── Helpers/
│ └── DTOs/
│
├── UI/
│ └── WinUI/
│ ├── Views/
│ ├── ViewModels/
│ ├── Models/
│ ├── Converters/
│ ├── Services/
│ └── Themes/
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

## Simulation/

Permet de simuler :

- Erreurs
- Gros projets
- Cas extrêmes

👉 Activé uniquement en mode développeur
👉 Jamais en production

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

👉 Base pour theming futur

---

## Tests/

- Tests unitaires
- Tests simulation

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

```texte
View → ViewModel → Core
```

---

# 🧩 FONCTIONNEMENT GLOBAL

```texte
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🖥️ UI STRUCTURE

```texte
Gauche → Arborescence
Centre → Options
Droite → Aperçu
Bas → Export
```

---

# 🔄 COMMUNICATION

```texte
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
LatuCollect/
│
├── LatuCollect.Core/
│ ├── Configuration/
│ ├── Models/
│ ├── Services/
│ │ ├── Import/
│ │ ├── Reader/
│ │ ├── Collection/
│ │ ├── Export/
│ │ └── Statistics/
│ │
│ ├── Simulation/
│ └── Logging/
│
├── LatuCollect.UI.WinUI/
│ ├── Views/
│ ├── ViewModels/
│ ├── Models/
│ ├── Services/
│ ├── Converters/
│ └── Assets/
│
├── Documentation/
│ ├── README.md
│ ├── ARCHITECTURE.md
│ ├── UI_GUIDE.md
│ ├── DIRECTORY_STRUCTURE.md
│ ├── GUIDE_UTILISATEUR.md
│ ├── ROADMAP.md
│ ├── PATCH_NOTES.md
│ ├── TESTS.md
```

---

# 🔄 DIFFÉRENCES AVEC LA CIBLE

❌ Non implémenté :

- Utils/
- Interfaces/
- Helpers/
- DTOs/
- Themes/
- Resources/
- Tests structuré
- Installer
- Assets global

---

# 🧠 ÉTAT DU PROJET

```texte
✔ Architecture stabilisée
✔ Pipeline optimisé
✔ Cache actif
✔ Logs intégrés
✔ Mode développeur
✔ UI stable
✔ Séparation UI / Core
```

---

# 🎯 OBJECTIF SUIVANT

```texte
➡ Finaliser la structure cible
➡ Continuer le découpage des services
➡ Stabiliser la version 1.0.0
```
