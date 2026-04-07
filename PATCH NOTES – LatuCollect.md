# 📝 PATCH NOTES – LATUCOLLECT

Historique officiel des évolutions du projet **LatuCollect**.

👉 Ce document liste uniquement les fonctionnalités réellement implémentées.

---

# 📚 Organisation documentaire

Les documents du projet sont organisés par rôle :

---

## 🧭 Documentation générale

| Fichier    | Rôle                           |
| ---------- | ------------------------------ |
| README.md  | Présentation globale du projet |
| LICENSE.md | Licence du projet              |

---

## 🧠 Documentation technique

| Fichier                | Rôle                          |
| ---------------------- | ----------------------------- |
| ARCHITECTURE.md        | Structure et règles du projet |
| UI_GUIDE.md            | Guide de l’interface          |
| TESTS.md               | Tests et validation           |
| DIRECTORY_STRUCTURE.md | Structure des dossiers        |

---

## 👤 Documentation utilisateur

| Fichier              | Rôle                         |
| -------------------- | ---------------------------- |
| GUIDE_UTILISATEUR.md | Utilisation de l’application |

---

## 🧱 Gestion du projet

| Fichier             | Rôle                         |
| ------------------- | ---------------------------- |
| FEUILLE_DE_ROUTE.md | Planification des évolutions |
| PATCH_NOTES.md      | Historique des versions      |

---

## ⚙️ Outils et workflows

| Fichier                 | Rôle                       |
| ----------------------- | -------------------------- |
| GUIDE_GITHUB_DESKTOP.md | Utilisation GitHub Desktop |
| GUIDE_COMMITS.md        | Bonnes pratiques commits   |

---

## 🎨 Ressources

| Fichier            | Rôle                      |
| ------------------ | ------------------------- |
| Code extractor.png | Maquette / visuel initial |

---

# 🚀 VERSION 0.1.0

## 📌 Statut

🟢 Fonctionnel — Version console (historique)

---

## 🎯 Objectif

Mettre en place un système simple de collecte de contenu.

---

## ✨ Fonctionnalités

### 📥 Import

- ✅ Ajout de fichiers
- ✅ Ajout de dossiers
- ✅ Gestion des chemins

---

### 📄 Lecture

- ✅ Lecture du contenu des fichiers
- ✅ Aucune transformation du code

---

### 📤 Export

- ✅ Génération d’un fichier TXT
- ✅ Contenu concaténé

---

## ⚙️ Pipeline

```text
Import → Lecture → Export
```

---

## ⚠️ Limitations

- ❌ Absence d’interface graphique
- ❌ Pas de sélection visuelle
- ❌ Export peu structuré

---

# 🚀 VERSION 0.2.0

## 📌 Statut

🟢 Fonctionnel — Application WinUI (MVP validé)

---

## 🎯 Objectif

Introduire une interface graphique permettant une utilisation visuelle, simple et contrôlée.

---

## ✨ Fonctionnalités

### 🖥️ Interface utilisateur

- ✅ Structure en 4 zones (gauche / centre / droite / bas)
- ✅ Interface claire et lisible
- ✅ Différenciation visuelle fichiers / dossiers

---

### 📂 Import

- ✅ Sélection d’un dossier via explorateur
- ✅ Construction automatique de l’arborescence
- ✅ Chargement récursif des sous-dossiers

---

### 🌳 Arborescence

- ✅ Affichage hiérarchique (TreeView)
- ✅ Navigation fluide
- ✅ Correction des problèmes d’affichage

---

### ☑️ Sélection

- ✅ Sélection des fichiers via CheckBox
- ✅ Gestion des sous-dossiers
- ✅ Mise à jour en temps réel

---

### 🔎 Recherche

- ✅ Barre de recherche
- ✅ Filtrage dynamique
- ✅ Recherche insensible à la casse

---

### 👁️ Aperçu

- ✅ Génération dynamique en temps réel
- ✅ Lecture réelle des fichiers
- ✅ Affichage structuré :
  - Chemin du fichier
  - Contenu
  - Séparateur
- ✅ Gestion des états (vide / contenu)
- ✅ Correction des problèmes d’affichage (doublons, visibilité)
- ✅ Scroll automatique
- ✅ Affichage type éditeur (monospace, alignement gauche)

---

### 📤 Export

- ✅ Export via FileSavePicker
- ✅ Génération fichier TXT et Markdown (.md)
- ✅ Choix du format utilisateur
- ✅ Contenu identique à l’aperçu
- ✅ Assemblage multi-fichiers
- ✅ Confirmation après export

---

### 📋 Copie

- ✅ Copie du contenu dans le presse-papiers
- ✅ Activation uniquement si contenu disponible
- ✅ Désactivation automatique si aucun contenu
- ✅ Confirmation utilisateur

---

### ⚙️ Dialogs

- ✅ Options
- ✅ Aide
- ✅ À propos
- ✅ Confirmation de fermeture

---

## ⚙️ Architecture

- ✅ Séparation Core / UI respectée
- ✅ Respect du standard ALC
- ✅ Implémentation MVVM
- ✅ Gestion des états UI :
  - IsPreviewEmpty
  - CanCopy
  - SearchText

---

## 🧠 Fonctionnement utilisateur

```text
Charger dossier → Sélectionner → Aperçu → Exporter
```

---

## ⚙️ Pipeline interne

```text
Import → Lecture → Assemblage → Export
```

👉 Lecture = récupération du contenu
👉 Assemblage = regroupement du texte

👉 ❗ Aucune transformation du code

---

## ⚠️ Limitations actuelles

- ❌ Options encore limitées
- ❌ Pas de sauvegarde des préférences utilisateur
- ❌ Gestion des erreurs encore basique

---

# 🚀 VERSION 0.3.0

## 📌 Statut

🟢 Fonctionnel — Système de simulation simplifié

---

## 🎯 Objectif

Permettre de simuler des cas d’erreurs essentiels sans complexité inutile ni impact sur l’application réelle.

---

## ✨ Fonctionnalités

### 🧪 Simulation

- ✅ Ajout d’un système de simulation (Core)
- ✅ Activation via configuration (SimulationConfig)
- ✅ Choix du scénario de simulation

---

### 📥 Lecture

- ✅ Simulation de fichiers vides
- ✅ Simulation d’erreurs de lecture
- ✅ Simulation de chemins longs

---

### 📤 Export

- ✅ Simulation d’erreurs d’export

---

## ⚙️ Architecture

- ✅ Ajout du dossier Simulation dans Core
- ✅ Création SimulationService
- ✅ Intégration propre dans FileReaderService
- ✅ Intégration propre dans FileExportService
- ✅ Simplification du système de simulation (suppression des cas inutiles)
- ✅ Suppression du code mort (GenerateLargeContent)

---

## 🧠 Objectif atteint

- ✔ Tester les cas d’erreurs essentiels (lecture / export)
- ✔ Garantir un comportement stable et prévisible
- ✔ Simplifier l’architecture sans sur-ingénierie

---

# 🚀 VERSION 0.4.0

## 📌 Statut

🟢 Fonctionnel — Version stable (fiabilité renforcée)

---

## 🎯 Objectif

Rendre l’application fiable, stable et prévisible dans tous les cas d’utilisation.

---

## ✨ Améliorations

### 🛡️ Gestion des erreurs

- ✅ Sécurisation complète de la lecture des fichiers
  - Gestion des erreurs d’accès
  - Gestion des fichiers corrompus
  - Gestion des chemins trop longs
  - Aucun crash possible

- ✅ Sécurisation complète de l’export
  - Gestion des erreurs d’écriture
  - Retour d’état via `ExportResult`
  - Messages d’erreur clairs

---

### 🔁 Cohérence système

- ✅ Garantie stricte : aperçu = export
- ✅ Suppression de la duplication de logique
- ✅ Centralisation de la construction du contenu dans le Core (`FileExportService`)

---

### 🧱 Architecture

- ✅ Meilleur respect du standard ALC
  - Déplacement de la logique d’assemblage vers le Core
  - Simplification du ViewModel
  - Réduction des responsabilités côté UI

- ✅ Introduction d’une source unique de vérité pour le contenu exporté

---

### ⚙️ Core

- ✅ Ajout de la méthode `BuildContent()` dans `FileExportService`
- ✅ Ajout du modèle `ExportResult`
- ✅ Gestion robuste des exceptions dans l’export

---

### 🖥️ UI / ViewModel

- ✅ Suppression de la méthode `BuildExportContent()`
- ✅ Utilisation directe du Core pour générer l’aperçu
- ✅ Synchronisation parfaite entre aperçu et export
- ✅ Simplification de la logique interne

---

## 🧠 Résultat

- ✅ Application stable
- ✅ Aucun crash lors de la lecture ou de l’export
- ✅ Comportement prévisible
- ✅ Code plus propre et maintenable
- ✅ Respect du pipeline :

```text
Import → Lecture → Assemblage → Export
```

---

## 🏁 Objectif atteint

👉 LatuCollect est désormais :

- ✔ Stable
- ✔ Fiable
- ✔ Cohérent
- ✔ Prêt pour l’amélioration UX (v0.5.0)

---

# 🚀 VERSION 0.5.0

## 📌 Statut

🟡 En cours — Amélioration UX

---

## 🎯 Objectif

Améliorer l’expérience utilisateur sans complexifier l’application.

---

## ✨ Améliorations

### 🔎 Recherche

- ✅ Ajout d’un système de recherche dynamique
- ✅ Filtrage en temps réel basé sur le nom des fichiers
- ✅ Recherche insensible à la casse
- ✅ Conservation de la hiérarchie (parents visibles si enfant correspondant)
- ✅ Implémentation simple et stable (sans reconstruction de l’arbre)

---

### 🧭 Interface utilisateur

- ✅ Ajout d’un bouton de recherche (🔍)
- ✅ Ouverture / fermeture de la barre de recherche (toggle)
- ✅ Amélioration du positionnement des éléments (layout propre en Grid)
- ✅ Suppression des chevauchements UI

---

### 📄 Affichage

- ✅ Amélioration de la lisibilité du contenu
- ✅ Utilisation d’une police monospace (type éditeur)
- ✅ Scroll fluide sur contenu volumineux

---

### 💬 Feedback utilisateur

- ✅ Amélioration des messages utilisateur (export, copie)
- ✅ Notification visuelle non intrusive

---

## 🧠 Résultat

- ✔ Interface plus claire
- ✔ Navigation plus fluide
- ✔ Recherche fonctionnelle et stable
- ✔ Expérience utilisateur améliorée

---

## ⚠️ Limites actuelles

- ❌ Pas encore de gestion des très gros projets (optimisation)
- ❌ Pas d’indicateur de chargement
- ❌ Pas de sélection globale optimisée (feature volontairement simplifiée)

---

## 🏁 Objectif

👉 LatuCollect devient :

- ✔ Plus agréable
- ✔ Plus intuitif
- ✔ Plus fluide

👉 Sans complexifier l’architecture

---

# 🧠 Philosophie

- ✅ Simplicité avant complexité
- ✅ Lisibilité avant optimisation
- ✅ Utilité avant fonctionnalité

---

# 📜 Rôle du Patch Notes

- ✅ Suivre l’évolution réelle du projet
- ✅ Comprendre les changements
- ✅ Garantir la cohérence

👉 Un historique fiable = projet maintenable
