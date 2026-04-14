# 📝 PATCH NOTES – LATUCOLLECT

Historique officiel des évolutions du projet **LatuCollect**.

👉 Ce document liste uniquement les fonctionnalités réellement implémentées.

---

# 📚 Organisation documentaire

👉 Voir la documentation complète dans le README.

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

| Fichier        | Rôle                         |
| -------------- | ---------------------------- |
| ROADMAP.md     | Planification des évolutions |
| PATCH_NOTES.md | Historique des versions      |

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
- ✅ Filtrage simple des fichiers (nom + extension)
- ✅ Mise à jour dynamique de l’arborescence après modification (sans rechargement complet)

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

🟢 Terminée — Amélioration UX + Stabilité

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
- ✅ Aucun rechargement de l’arbre (UI stable)

---

### 🧭 Interface utilisateur

- ✅ Ajout d’un bouton de recherche (🔍)
- ✅ Ouverture / fermeture de la barre de recherche (toggle)
- ✅ Amélioration du layout (Grid structuré)
- ✅ Suppression des chevauchements UI
- ✅ Amélioration de la compréhension des actions (tooltips 📂 🔍)
- ✅ Ajout d’une sélection globale (checkbox "Tout sélectionner")

---

### 📄 Affichage

- ✅ Amélioration de la lisibilité du contenu
- ✅ Utilisation d’une police monospace (type éditeur)
- ✅ Scroll fluide sur contenu volumineux
- ✅ Gestion correcte des états vides ("Aucun fichier sélectionné")

---

### 💬 Feedback utilisateur

- ✅ Système de feedback visuel non intrusif (toast en haut à droite)
- ✅ Messages clairs (export, copie, erreurs)
- ✅ Gestion des actions annulées (sélection dossier, export)
- ✅ Correction de l’affichage (retour à la ligne, responsive)

---

### 🔄 États UI

- ✅ Mise en place d’un système d’états global (Loading / Ready / Error)
- ✅ Affichage conditionnel (loader / erreur / contenu)
- ✅ Synchronisation ViewModel ↔ UI

---

### ⏳ Chargement

- ✅ Ajout d’un indicateur de chargement (loader)
- ✅ Affichage fluide grâce à l’async (Task.Run + UI thread sécurisé)
- ✅ Correction du blocage UI (plus de freeze visuel)

---

### ⚡ Performance (gros projets)

- ✅ Limitation du nombre de nœuds chargés (MAX_NODES)
- ✅ Limitation de la profondeur (MAX_DEPTH)
- ✅ Protection contre les boucles récursives lourdes
- ✅ Chargement partiel sécurisé
- ✅ Ajout protection anti-freeze (chargement asynchrone + limitation volume)

---

### ⚠️ Gestion des gros volumes

- ✅ Détection automatique des projets volumineux
- ✅ Affichage d’un message utilisateur :
  → "⚠ Projet volumineux — affichage partiel"
- ✅ Comportement expliqué (évite confusion)

---

## 🧠 Résultat

- ✔ Interface claire et lisible
- ✔ Navigation fluide
- ✔ Aucun freeze UI
- ✔ Feedback utilisateur cohérent
- ✔ Application stable même sur gros projets

---

## ⚠️ Limites actuelles

- ❌ Chargement partiel (pas encore de lazy loading)
- ❌ Pas de virtualisation de l’arbre
- ❌ Pas d’optimisation avancée (batch / streaming)

---

## 🏁 Objectif atteint

👉 LatuCollect devient :

- ✔ Plus agréable
- ✔ Plus intuitif
- ✔ Plus fluide
- ✔ Plus robuste

👉 Sans complexifier l’architecture (ALC respectée)

---

## 💡 Note technique

Cette version introduit :

- Un contrôle du volume de données (anti-freeze)
- Une gestion des états UI propre
- Une séparation claire entre logique et interface

👉 Base solide pour les futures évolutions (0.6.0+)

---

# 🚀 VERSION 0.6.0

## 📌 Statut

🟢 Terminée — Recherche & gestion des fichiers améliorées

---

## 🎯 Objectif

Améliorer la gestion des fichiers et la navigation dans l’arborescence.

---

## ✨ Améliorations

### 🔎 Recherche

- ✅ Recherche fiable et stable (zéro bug)
- ✅ Gestion du cas "aucun résultat" avec affichage clair
- ✅ Ajout d’un filtrage par extension :
  - `.cs`, `.xaml`, `.json`, etc.

- ✅ Conservation du filtrage classique (par nom)
- ✅ Conservation de la hiérarchie (parents visibles si enfant correspondant)

---

### ⚡ Performance

- ✅ Ajout d’un système de debounce sur la recherche
  - Réduction des recalculs inutiles
  - Amélioration de la fluidité sur gros projets

- ✅ Optimisation du cas recherche vide (reset immédiat sans recalcul lourd)

---

### 📁 Gestion des fichiers

- ✅ Exclusion automatique de dossiers :
  - `bin`
  - `obj`
  - `.git`

- ✅ Mise en place d’un système de configuration globale (`AppConfig`)
- ✅ Exclusion appliquée directement au chargement (meilleure performance)

---

### ⚙️ Options utilisateur

- ✅ Ajout d’un panneau Options fonctionnel
- ✅ Gestion dynamique des exclusions :
  - Ajouter un dossier à exclure
  - Supprimer un dossier exclu

- ✅ Mise à jour automatique de l’arborescence après modification

---

### 🖥️ Interface utilisateur

- ✅ Affichage explicite "Aucun résultat"
- ✅ Amélioration de la compréhension de la recherche
- ✅ Feedback visuel clair en cas de filtrage vide

---

### 🧱 Architecture

- ✅ Introduction d’un module Configuration (Core)
- ✅ Respect strict de l’architecture ALC :
  - Core → configuration globale
  - UI → interaction utilisateur

- ✅ Aucune logique métier ajoutée dans l’UI

---

## 🧠 Résultat

- ✔ Recherche rapide et fiable
- ✔ Filtrage plus puissant (nom + extension)
- ✔ Interface plus claire et compréhensible
- ✔ Meilleure performance sur gros projets
- ✔ Application plus configurable

---

## ⚠️ Limites actuelles

- ❌ Pas de sauvegarde des exclusions (non persistées)
- ❌ Filtrage avancé limité (pas de multi-critères)
- ❌ Pas de virtualisation de l’arbre

---

## 🏁 Objectif atteint

👉 LatuCollect devient :

- ✔ Plus performant
- ✔ Plus flexible
- ✔ Plus précis
- ✔ Plus agréable à utiliser

👉 Sans complexifier l’application (simplicité respectée)

---

## 💡 Note technique

Cette version introduit :

- Un système de recherche optimisé (debounce)
- Un filtrage étendu (extensions)
- Une configuration dynamique des exclusions

👉 Base prête pour :

- sauvegarde des préférences (JSON)
- filtrage avancé
- amélioration performance future

---

# 🚀 VERSION 0.7.0

## 📌 Statut

🟢 Terminée — Export amélioré & Statistiques temps réel

---

## 🎯 Objectif

Améliorer la qualité du contenu exporté et fournir des informations utiles à l’utilisateur, sans complexifier l’application.

---

## ✨ Améliorations

### 📤 Export

- ✅ Amélioration du format Markdown :
  - Ajout de titres par fichier (`## 📄 chemin`)
  - Encapsulation du contenu dans des blocs de code (` `)

- ✅ Amélioration de la lisibilité :
  - Espacement homogène
  - Séparateurs clairs (`---` en Markdown, `-----` en TXT)

- ✅ Nettoyage de la structure :
  - Centralisation complète dans `FileExportService`
  - Suppression de la duplication (aperçu = export garanti)

- ✅ Optimisation du traitement :
  - Lecture des fichiers en une seule passe
  - Construction du contenu + statistiques simultanée

---

### 📊 Statistiques

- ✅ Nombre de fichiers sélectionnés
- ✅ Nombre total de lignes
- ✅ Nombre total de caractères
- ✅ Taille totale des fichiers

- ✅ Calcul optimisé :
  - Aucune double lecture des fichiers
  - Exécution en arrière-plan (`Task.Run`)
  - Mise à jour en temps réel

---

### ⚡ Performance

- ✅ Suppression du double calcul (avant : contenu + stats séparés)
- ✅ Réduction importante de la consommation mémoire
- ✅ Amélioration de la fluidité sur gros projets

- ✅ Limitation de l’aperçu :
  - Maximum 20 fichiers affichés
  - Message utilisateur :
    → "⚠ Aperçu limité à 20 fichiers"

---

### 🧠 Architecture

- ✅ Respect strict de l’architecture ALC :
  - Core → logique d’export + statistiques
  - UI → affichage uniquement

- ✅ Suppression de `FileStatisticsService` :
  - Fusion des responsabilités dans `FileExportService`
  - Élimination des doublons

- ✅ Introduction du modèle `ExportData` :
  - Contenu + statistiques regroupés
  - Source unique de vérité

---

### 💬 Expérience utilisateur

- ✅ Ajout d’un affichage des statistiques via dialog :
  - Fichiers / lignes / caractères / taille

- ✅ Désactivation temporaire de la sélection globale :
  - Évite les freezes sur gros projets
  - Ajout d’un popup explicatif utilisateur

---

### 🛡️ Gestion des erreurs

- ✅ Amélioration des messages d’erreurs d’export :
  - Accès refusé
  - Fichier utilisé (ouvert ailleurs)
  - Chemin invalide
  - Erreur inattendue

- ✅ Messages clairs et compréhensibles pour l’utilisateur

---

## 🧠 Résultat

- ✔ Export plus lisible et structuré
- ✔ Statistiques fiables et en temps réel
- ✔ Application plus rapide et stable
- ✔ Meilleure expérience utilisateur
- ✔ Code plus propre et maintenable

---

## ⚠️ Limites actuelles

- ❌ Sélection globale désactivée (temporairement)
- ❌ Pas encore de sélection intelligente (limite / confirmation)

---

## 🏁 Objectif atteint

👉 LatuCollect devient :

- ✔ Plus performant
- ✔ Plus informatif
- ✔ Plus professionnel
- ✔ Plus stable sur gros projets

👉 Sans complexifier l’application (simplicité respectée)

---

## 💡 Note technique

Cette version introduit :

- Un pipeline optimisé (lecture unique)
- Une fusion logique export + statistiques
- Une meilleure gestion des performances

👉 Base solide pour :

- sélection intelligente
- optimisation avancée
- amélioration UX future (v0.8.0)

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

👉 Un historique fiable = un projet maintenable
