<div align="center">

# 🧭 LATUCOLLECT

### Collecte simple et rapide de contenu multi-fichiers

🔹 Extraction, assemblage et export de fichiers
🔹 Pensé pour la lisibilité et l’usage avec des outils d’IA
🔹 Génération rapide d’un document propre et structuré

<br>

![Version](https://img.shields.io/badge/Version-0.16.0-FFDF20?style=for-the-badge)
![Statut](https://img.shields.io/badge/Statut-Architecture%20%26%20Split%20MainViewModel-28A745?style=for-the-badge)
![Licence](https://img.shields.io/badge/Licence-MIT-FF0000?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-8-800080?style=for-the-badge)
![UI](https://img.shields.io/badge/UI-WinUI3-0078D6?style=for-the-badge)

<br>

### 👨🏻‍💻 Auteur

Flo Latury

</div>

---

## 📌 Résumé

LatuCollect est un outil qui permet de regrouper rapidement le contenu de plusieurs fichiers en un seul document propre, sans modifier les fichiers d’origine.

---

## 📊 État des fonctionnalités

- ✅ Stabilisation majeure terminée (0.11.0)
- ✅ Optimisations performance terminées (0.12.0)
- ✅ Simplification architecture terminée (0.13.0)
- ✅ UX & comportement stabilisés (0.14.0)
- ✅ Version 0.15.0 terminée
- ✅ Version 0.16.0 terminée
- 🟡 Version 0.17.0 en préparation

🔮 Évolutions futures → [ROADMAP](./ROADMAP.md)

---

# 🚀 1. Objectif

LatuCollect permet de :

## 📂 Exploration du projet

- Charger un projet complet
- Naviguer dans sa structure
- Rechercher rapidement des fichiers

## ☑️ Sélection

- Sélectionner les fichiers à collecter
- Exclure dynamiquement des dossiers

## 👁️ Prévisualisation

- Générer un aperçu en temps réel
- Copier le contenu généré

## 📤 Export

- Exporter un document (.txt / .md)

## ⚙️ Confort d'utilisation

- Sauvegarder automatiquement les préférences utilisateur
- Restaurer automatiquement l’état ouvert de l’arborescence
- Ouvrir rapidement le dossier courant dans l’explorateur

## 🎯 Philosophie

- ✔ Aucun fichier source n’est modifié
- ✔ Lecture seule
- ✔ Aucune transformation du contenu
- ✔ Copier intelligent uniquement

---

# 📦 2. Installation

## 📋 Prérequis

- Windows 10 / 11
- .NET 8

## 🚧 État actuel

LatuCollect est actuellement en développement actif.

Le projet est compilé et exécuté depuis l'environnement de développement.

## 🔮 Distribution future

Les futures versions proposeront :

- Exécutable prêt à l'emploi
- Packaging simplifié
- Distribution des releases
- Documentation d'installation

👉 Voir [ROADMAP](./ROADMAP.md) pour les évolutions prévues.

---

# ⚡ 3. Démarrage rapide

## 📂 Étape 1

Ouvrir l’application

## 📁 Étape 2

Charger un dossier

## ☑️ Étape 3

Sélectionner les fichiers souhaités

## 👁️ Étape 4

Visualiser l’aperçu généré

## 📤 Étape 5

Exporter le document final

---

## 🎯 Résultat

👉 Quelques secondes suffisent pour obtenir un document prêt à être copié ou exporté.

---

# 🎯 4. Cas d’usage

## 🤖 Intelligence artificielle

- Préparer du code pour une IA
- Fournir un contexte projet complet
- Regrouper rapidement plusieurs fichiers

## 📚 Documentation

- Générer une documentation rapide
- Centraliser le contenu d’un projet
- Faciliter la relecture

## 🤝 Partage

- Partager du code proprement
- Extraire des parties spécifiques d’un projet
- Préparer des extraits pour un audit ou une analyse

## 👥 Public visé

- Débutants
- Développeurs
- Utilisateurs d’outils d’IA

👉 Compatible débutant et avancé

---

# 🧠 5. Fonctionnement

## 👤 Fonctionnement utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

## ⚙️ Fonctionnement interne

```text
Import → Lecture → Assemblage → Statistiques → Export
```

## 🎯 Principe

- Le contenu des fichiers est lu
- Les fichiers sélectionnés sont assemblés
- Les statistiques sont calculées
- Le contenu final est généré pour l’aperçu et l’export

## 🔒 Garantie

- ✔ Aucun fichier source n’est modifié
- ✔ Le contenu est conservé tel quel
- ✔ Aucune transformation complexe
- ✔ Copier intelligent uniquement

---

# 🖥️ 6. Interface

## 🟦 Gauche — Projet

- Arborescence complète
- Navigation dans les dossiers
- Recherche dynamique
- Sélection via checkbox
- Exclusions dynamiques
- Conservation de l’arbre réel

## 🟨 Centre — Options

- Choix du format (.txt / .md)
- Copier le contenu
- Ouvrir le dossier dans l’explorateur

### 📋 Accès

- Paramètres
- Statistiques
- Aide
- À propos

### 👨🏻‍💻 Mode développeur

- Activation via ⚙ Paramètres
- Indicateur visuel intégré

#### 🎯 Rôle

- Analyse interne
- Outils de développement
- Gestion avancée des exclusions protégées

👉 Aucun impact sur l’utilisateur standard

## 🟩 Droite — Aperçu

- Contenu final en temps réel
- Affichage type code (monospace)
- Mise à jour automatique
- Chargement progressif

### 📋 États UI

- Aucun fichier sélectionné
- Chargement
- Erreur
- État vide
- Contenu prêt

## 🔻 Bas — Actions

- Export du fichier
- Consultation des logs
- Export des logs

---

# 📄 7. Format d’export

```text
Chemin du fichier

(contenu du fichier)



----------------------------------------
```

---

## 🎯 Principe

👉 Aperçu = Export dans le fonctionnement standard

- Le contenu est conservé tel quel
- Aucun parsing complexe
- Aucune transformation du contenu
- Génération unique côté Core

---

# ⚙️ 8. Fonctionnement interne

👉 Pipeline simple

👉 Lecture uniquement

## 🧱 Architecture

LatuCollect suit une architecture ALC :

UI → ViewModel → Core

### 📋 Principes importants

- UI = affichage uniquement
- Core = logique métier
- Aucun accès disque depuis l’UI
- Pipeline centralisé
- Source unique de vérité pour l’export

### 🎯 Objectif

- Stabilité
- Lisibilité
- Maintenabilité
- Comportement prévisible

---

# 🧪 9. Validation & stabilité

LatuCollect possède désormais :

- ✔ 116 tests automatisés
- ✔ Validation TreeView
- ✔ Validation recherche
- ✔ Validation preview async
- ✔ Validation exclusions
- ✔ Validation export massif
- ✔ Validation gros projets
- ✔ Validation logging
- ✔ Stress tests mémoire

### 🎯 Objectif

Garantir une application :

- Stable
- Prévisible
- Maintenable

---

# ⚡ 10. Performance

## 🚀 Optimisations

- Cache fichiers
- Réduction I/O
- Optimisation mémoire
- Debounce preview async
- Réduction des recalculs inutiles
- Chargement progressif UI
- Protection anti multi-refresh
- Invalidation previews obsolètes

## 🛡️ Protection gros projets

- MAX_NODES
- MAX_DEPTH
- Chargement partiel
- UI responsive pendant import massif
- Protection contre les previews obsolètes

### 💬 Message utilisateur

⚠ Projet volumineux — affichage partiel

## ✂️ Preview limité

👉 Le contenu exporté reste toujours complet.

👉 Seul l’affichage peut être limité afin de préserver la fluidité UI et la stabilité mémoire.

Dans certains très gros projets :

- le preview peut être volontairement tronqué
- l’export complet reste conservé
- les statistiques restent calculées sur tous les fichiers sélectionnés

### 🎯 Objectif

- éviter les freezes UI
- limiter l’utilisation mémoire
- conserver une application fluide

---

# 🛠️ 11. Stabilité UI

## 📋 Garanties

- Taille minimale : 1600 x 1000
- Réduction du flickering
- Gestion propre du redimensionnement
- Fenêtres de dialogue stables
- Réduction des refresh inutiles
- Protection anti multi-refresh

---

# 🧾 12. Logs

## 📋 Fonctionnalités

- Logs intégrés
- Visualisation dans l’interface
- Filtrage des logs
- Export des logs

---

# ⚠️ 13. Ce que l’application NE fait PAS

## 🚫 Limitations volontaires

- Aucune analyse de code
- Aucune modification des fichiers
- Aucun parsing complexe

### 🎯 Philosophie

👉 LatuCollect = copier intelligent

---

# ⚙️ 14. Fonctionnalités principales

## ✅ Actuelles

- Import de dossier
- Arborescence
- Recherche dynamique (debounce)
- Exclusion de dossiers
- Aperçu temps réel
- Export TXT / Markdown
- Copie presse-papiers
- Statistiques
- Logs
- Mode développeur
- Architecture simplifiée
- Réduction du couplage Core/UI
- Chargement progressif UI
- Persistance expansion TreeView

## 🔮 À venir

- Création ExportViewModel
- Migration progressive des paramètres Settings
- Réduction des redirections MainViewModel
- Stabilisation async UI
- Optimisations mémoire avancées
- Améliorations UX futures

👉 Voir [ROADMAP](./ROADMAP.md)

---

# 🏗️ 15. Architecture

```text
Core = logique métier

UI = affichage
```

## 🧩 Services principaux

- FileImportService
- FileReaderService
- FileExportService
- FileStatisticsService

👉 ViewModel = orchestrateur

## 🔮 Évolution future

Le `MainViewModel` sera progressivement découpé en plusieurs ViewModels spécialisés afin de :

- améliorer la maintenabilité
- réduire les effets de bord
- améliorer la stabilité async UI

### 📋 Découpage prévu

#### ✅ Réalisé

- `LogsViewModel`
- `TreeViewViewModel`

#### 🟢 Migration avancée

- `PreviewViewModel`

#### 🟢 Migration partielle

- `SettingsViewModel`

#### ⬜ Prévu ultérieurement

- `ExportViewModel`

👉 Voir [ROADMAP](./ROADMAP.md) pour les détails

---

# 📁 16. Structure

## 📋 Organisation

```text
- Core/
- UI/WinUI/
- Resources/
- Tests/
```

👉 Voir : [DIRECTORY_STRUCTURE](./DIRECTORY_STRUCTURE.md)

---

# 📌 17. État actuel

## 🏗️ Architecture

- Application stable
- UI stabilisée
- Pipeline optimisé
- Logs intégrés
- Mode développeur actif

## 👁️ Preview & TreeView

- Pipeline preview async stabilisé
- Persistance TreeView stabilisée
- Exclusions UI stabilisées
- Chargement progressif UI actif

## 📈 Évolution du projet

- ✔ Stabilisation majeure terminée (0.11.0)
- ✔ Optimisations performance terminées (0.12.0)
- ✔ Simplification architecture terminée (0.13.0)
- ✔ UX & comportement stabilisés (0.14.0)
- ✔ Première étape du découpage du MainViewModel réalisée (v0.15.0)

- ✔ Extraction avancée PreviewViewModel (v0.16.0)
- ✔ Migration TreeViewViewModel poursuivie (v0.16.0)
- ✔ Préparation SettingsViewModel poursuivie (v0.16.0)

---

# ⭐ Points forts

- Simple
- Rapide
- Stable
- Lisible
- Non destructif
- Lecture seule
- Aucune modification

---

# ⚠️ 18. Limites actuelles

## 📋 Limitations connues

- Pas de virtualisation avancée
- Pas de lazy loading hiérarchique avancé
- Chargement partiel sur très gros projets

### 🎯 Pourquoi ?

👉 Choix volontaire pour privilégier :

- la simplicité
- la stabilité
- la maintenabilité
- la prévisibilité

---

# 🔮 Évolutions prévues

## 📋 Prochaines étapes

- Poursuite du découpage du MainViewModel
- Stabilisation async UI
- Optimisations mémoire avancées
- Optimisations gros projets
- Améliorations UX

## 🖥️ Découpage ViewModels

### ✅ Réalisé

- `LogsViewModel`
- `TreeViewViewModel`
- `SettingsViewModel` (première phase)

### 🟡 En cours

- `PreviewViewModel`

### ⬜ Prévu ultérieurement

- `ExportViewModel`

👉 Voir : [ROADMAP](./ROADMAP.md)

---

# 📝 Versions

👉 Voir : [PATCH NOTES](./PATCH_NOTES.md)

---

# 🔗 Liens utiles

- 📌 [ROADMAP](./ROADMAP.md)
- 📝 [PATCH NOTES](./PATCH_NOTES.md)
- 🧪 [TESTS](./TESTS.md)
- 🖥️ [UI GUIDE](./UI_GUIDE.md)
- 🏗️ [ARCHITECTURE](./ARCHITECTURE.md)
- 📂 [DIRECTORY STRUCTURE](./DIRECTORY_STRUCTURE.md)

---

# 🧠 Philosophie

## 🚫 Ce que LatuCollect n'est pas

- Analyser du code
- Transformer du code
- Parser des projets complexes

## ✅ Ce que LatuCollect fait

- Collecter
- Assembler
- Exporter

## 🎯 Objectif

Fournir un outil :

- Rapide
- Stable
- Lisible
- Prévisible

✔ Copier intelligent uniquement
