<div align="center">

### **🧭 LATUCOLLECT**

### Collecte simple et rapide de contenu multi-fichiers

🔹 Extraction, assemblage et export de fichiers
🔹 Pensé pour la lisibilité et l’usage avec des outils d’IA
🔹 Génération rapide d’un document propre et structuré

<br>

![Version](https://img.shields.io/badge/Version-0.17.0-FFDF20?style=for-the-badge)
![Statut](https://img.shields.io/badge/Statut-Stabilisation%20Async%20UI%20%26%20Finalisation%20Architecture-28A745?style=for-the-badge)
![Licence](https://img.shields.io/badge/Licence-MIT-FF0000?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-8-800080?style=for-the-badge)
![UI](https://img.shields.io/badge/UI-WinUI3-0078D6?style=for-the-badge)

<br>

### 👤 Auteur

Flo Latury

</div>

### **📖 Sommaire**

#### Sections :

- [🏠 01. Présentation](#presentation)
- [🧭 02. Découverte](#decouverte)
- [🧩 03. Prise en main](#prise-en-main)
- [⚙️ 04. Fonctionnement](#fonctionnement)
- [🏗️ 05. Architecture](#architecture)
- [🧪 06. Qualité & performances](#qualite-performances)
- [📌 07. État du projet](#etat-du-projet)
- [📚 08. Documentation](#documentation)
- [🧠 09. Philosophie](#philosophie)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="presentation"></a>

### **🏠 01. Présentation**

### 📝 Résumé

LatuCollect est un outil qui permet de regrouper rapidement le contenu de plusieurs fichiers dans un document unique, clair et structuré, sans modifier les fichiers d'origine.

---

### 🚦 État des fonctionnalités

- ✅ Stabilisation majeure terminée (v0.11.0)
- ✅ Optimisations des performances terminées (v0.12.0)
- ✅ Simplification de l'architecture terminée (v0.13.0)
- ✅ UX & comportements stabilisés (v0.14.0)
- ✅ Split progressif du MainViewModel terminé (v0.15.0)
- ✅ Finalisation du découpage du MainViewModel (v0.16.0)
- ✅ Stabilisation Async UI & finalisation de l'architecture (v0.17.0)
- 🟡 Refonte UI, thèmes & audit UX en préparation (v0.18.0)
- 🔴 Finalisation & Distribution (v0.19.0)

---

### 🔗 Documents associés

- 🔮 Feuille de route → [ROADMAP](./ROADMAP.md)
- 📝 Historique des versions → [PATCH_NOTES](./PATCH_NOTES.md)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="decouverte"></a>

### **🧭 02. Découverte**

### 🎯 Objectif

LatuCollect a été conçu pour simplifier la collecte et l'assemblage de plusieurs fichiers dans un document unique.

Il permet notamment de :

#### 📂 Exploration & recherche

- Charger un projet complet
- Naviguer dans sa structure
- Rechercher rapidement des fichiers

#### ☑️ Sélection

- Sélectionner les fichiers à collecter
- Exclure dynamiquement des dossiers

#### 👁️ Prévisualisation

- Générer un aperçu du document final en temps réel
- Copier le contenu généré

#### 📤 Export

- Exporter un document (`.txt` / `.md`)
- Choisir entre un export **Normal** ou **Compatible IA**

#### 📊 Statistiques

- Nombre de fichiers
- Nombre de lignes
- Nombre de caractères
- Taille du contenu généré

#### 🛠️ Outils

- Sauvegarder automatiquement les préférences utilisateur
- Restaurer automatiquement l'état ouvert de l'arborescence
- Ouvrir rapidement le dossier courant dans l'explorateur

---

### 💼 Cas d'utilisation

#### 🤖 Intelligence artificielle

- Préparer un projet pour une IA
- Fournir un contexte complet à un assistant IA
- Regrouper rapidement plusieurs fichiers dans un document unique

#### 📄 Documentation

- Générer rapidement une documentation de travail
- Centraliser le contenu d'un projet
- Faciliter la relecture et les revues de code

#### 📦 Extraction

- Extraire un projet de manière structurée
- Extraire des fichiers ou dossiers spécifiques
- Préparer des extraits pour un audit, une analyse ou une assistance

#### 👤 Utilisateurs

- Débutants
- Développeurs
- Étudiants
- Utilisateurs d'outils d'IA

> Convient aussi bien aux utilisateurs débutants qu'aux utilisateurs expérimentés.

#### ✅ Résultat

En quelques étapes, LatuCollect génère un document unique prêt à être copié ou exporté.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="prise-en-main"></a>

### **🧩 03. Prise en main**

### 📦 Installation & lancement

#### 📋 Prérequis

- Windows 10 ou Windows 11
- .NET 8 SDK (pour compiler le projet)

---

#### 🚧 État actuel

LatuCollect est actuellement en développement actif.

À ce stade, l'application est principalement utilisée depuis son environnement de développement (**Visual Studio**).

---

#### 🔮 Distribution

Les prochaines versions proposeront notamment :

- Exécutable prêt à l'emploi
- Installateur dédié
- Packages de distribution (Release)
- Documentation d'installation

> Voir [🗺️ ROADMAP](./ROADMAP.md) pour les évolutions prévues.

---

### 🖥️ Interface

#### 🟦 Gauche — Projet

- Arborescence du projet
- Navigation dans les dossiers
- Recherche dynamique
- Sélection des fichiers via cases à cocher
- Gestion des exclusions
- Conservation de l'arborescence réelle

#### 🟨 Centre — Options

- Choix du format d'export (`.txt` / `.md`)
- Choix du mode d'export (**Normal** / **Compatible IA**)
- Copier le contenu généré
- Ouvrir le dossier dans l'explorateur

#### 📑 Menu

- Paramètres
- Statistiques
- Aide
- À propos

#### 💻 Mode développeur

- Activation depuis les paramètres
- Accès aux outils de développement et de diagnostic
- Gestion avancée des exclusions protégées

> Aucun impact sur l'utilisation standard de l'application.

#### 🟩 Droite — Aperçu

- Aperçu du document final en temps réel
- Affichage de type code (police monospace)
- Mise à jour automatique
- Chargement progressif sur les projets volumineux

#### 🚦 États de l'interface

- Aucun fichier sélectionné
- Chargement
- Erreur
- État vide
- Contenu prêt

#### 🔻 Bas — Actions

- Consulter les journaux (Logs)
- Exporter le document

---

### 🚀 Démarrage rapide

#### 📂 Étape 1 — Ouvrir l'application

Lancer LatuCollect.

#### 📁 Étape 2 — Importer un projet

Importer le dossier du projet.

#### ☑️ Étape 3 — Sélectionner les fichiers

Sélectionner les fichiers à inclure dans le document.

#### 👁️ Étape 4 — Vérifier l'aperçu

Vérifier le document généré dans l'aperçu.

#### 📤 Étape 5 — Copier ou exporter le document

Copier le contenu généré ou l'exporter au format souhaité (`.txt` ou `.md`).

#### ✅ Résultat

> En quelques étapes, LatuCollect génère un document unique prêt à être copié ou exporté.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="fonctionnement"></a>

### **🔄 04. Fonctionnement**

### 👤 Fonctionnement utilisateur

Le fonctionnement de LatuCollect suit un parcours simple :

```text
Importer → Sélectionner → Aperçu → Exporter
```

### 🔄 Fonctionnement interne

Ici on explique ce qui se passe **dans l'application**.

```text
Import → Sélection → Lecture → Assemblage → Export
```

#### 🔄 Déroulement

- Le projet est importé et son arborescence est construite.
- Les fichiers à inclure sont sélectionnés par l'utilisateur.
- Le contenu des fichiers sélectionnés est lu.
- Le document final est assemblé.
- Les statistiques sont calculées pendant l'assemblage.
- Le même contenu est utilisé pour l'aperçu et pour l'export.

---

### 🧱 Fonctionnement interne

#### 💡 Principe

LatuCollect repose sur un pipeline simple et centralisé.

Le traitement du contenu est effectué en lecture seule, sans modifier les fichiers d'origine.

#### 🏗️ Architecture

LatuCollect suit une architecture ALC :

```text
UI → ViewModel → Core
```

#### 📐 Principes importants

- UI = affichage uniquement
- ViewModel = orchestration des interactions
- Core = logique métier
- Aucun accès disque depuis l'UI
- Pipeline centralisé
- Source unique de vérité pour l'aperçu et l'export

#### 🎯 Objectif

- Stabilité
- Lisibilité
- Maintenabilité
- Comportement prévisible

> Les détails complets de l'architecture et du pipeline sont disponibles dans [🏗️ ARCHITECTURE](./ARCHITECTURE.md).

---

### 📄 Format d'export

#### 🔁 Principe de fonctionnement

👉 Le contenu est généré une seule fois par le Core, puis utilisé pour l'aperçu et l'export.

Dans le fonctionnement standard :

**Aperçu = Export**

- Le contenu est conservé tel quel
- Aucun parsing complexe
- Aucune transformation du contenu
- Génération unique côté Core
- Même source de données pour l'aperçu et l'export

```text
Chemin du fichier

(contenu du fichier)

----------------------------------------
```

---

### 🛡️ Garanties

- ✔ Aucun fichier source n'est modifié
- ✔ Lecture seule
- ✔ Même contenu pour l'aperçu et l'export
- ✔ Aucune transformation complexe
- ✔ Copier intelligent uniquement

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

### **🏗️ 05. Architecture**

### 🧱 Principe

```text
UI → ViewModel → Core
```

- **UI** : affichage et interactions utilisateur.
- **ViewModel** : orchestration des actions et communication avec le Core.
- **Core** : logique métier et traitement des données.

---

### 🧩 Services principaux

- `FileImportService`
- `FileReaderService`
- `FileExportService`
- `FileStatisticsService`

---

### 📐 Règles de conception

- Séparation claire des responsabilités
- Logique métier centralisée dans le Core
- Aucun accès disque depuis l'interface utilisateur
- Pipeline unique pour l'aperçu et l'export
- Architecture pensée pour être simple, stable et maintenable

---

### 📄 Documentation

> Pour une description détaillée de l'architecture ALC, consultez [🏗️ ARCHITECTURE](./ARCHITECTURE.md).

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="qualite-performances"></a>

### **🧪 06. Qualité & performances**

### ✅ Validation

LatuCollect est validé par une suite de tests automatisés couvrant notamment :

- ✔ TreeView
- ✔ Recherche
- ✔ Aperçu asynchrone
- ✔ Exclusions
- ✔ Export
- ✔ Gros projets
- ✔ Journaux (Logs)
- ✔ Stress tests mémoire

---

### 🎯 Objectif

Garantir une application :

- Stable
- Prévisible
- Maintenable

---

### 📄 Documentation

> Les détails des tests sont disponibles dans [🧪 TESTS](./TESTS.md).

---

### 🚄 Performances

#### 🚀 Optimisations

- Cache des fichiers
- Réduction des accès disque (`I/O`)
- Optimisation de l'utilisation mémoire
- `Debounce` de l'aperçu asynchrone
- Réduction des recalculs inutiles
- Chargement progressif de l'interface
- Protection contre les rafraîchissements multiples
- Invalidation des aperçus obsolètes

#### 📦 Gestion des projets volumineux

- Limitation du nombre de nœuds (`MAX_NODES`)
- Limitation de la profondeur (`MAX_DEPTH`)
- Chargement partiel de l'arborescence
- Interface réactive pendant les imports volumineux
- Protection contre les aperçus obsolètes

#### ℹ️ Information utilisateur

```text
⚠ Projet volumineux — affichage partiel
```

#### 👁️ Aperçu limité

Afin de préserver les performances, seul l'aperçu peut être limité.

Le document exporté reste toujours complet.

Pour les projets très volumineux :

- L'aperçu peut être volontairement tronqué ;
- L'export reste toujours complet ;
- Les statistiques sont calculées sur l'ensemble des fichiers sélectionnés.

#### 🎯 Objectif

- Préserver la fluidité de l'interface
- Limiter la consommation mémoire
- Garantir un comportement stable sur les projets volumineux

---

### 🖥️ Interface

#### 🛡️ Garanties

- Taille minimale de la fenêtre : **1600 × 1000**
- Réduction du scintillement de l'interface (`flickering`)
- Gestion stable du redimensionnement
- Fenêtres de dialogue cohérentes
- Réduction des rafraîchissements inutiles
- Protection contre les rafraîchissements multiples

#### 🎯 Objectif

Garantir une interface :

- Fluide
- Stable
- Réactive
- Prévisible

Même lors de l'utilisation de projets volumineux ou d'actions répétées.

---

### 🧾 Journaux (Logs)

#### 📋 Fonctionnalités

- Enregistrement des événements de l'application
- Consultation des journaux dans l'interface
- Filtrage par niveau de log
- Export des journaux
- Limitation automatique de la mémoire utilisée

#### 🎯 Objectif

Faciliter :

- Le diagnostic des problèmes ;
- Le suivi du fonctionnement de l'application ;
- L'analyse lors du développement et des tests.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="etat-du-projet"></a>

### **📌 07. État du projet**

### ✅ Fonctionnalités disponibles

#### 🧩 Fonctionnalités

- Import d'un projet
- Navigation dans l'arborescence
- Recherche dynamique
- Gestion des exclusions
- Aperçu du document en temps réel
- Export TXT / Markdown
- Mode d'export Compatible IA
- Copie du contenu dans le presse-papiers
- Statistiques
- Journaux (Logs)
- Mode développeur
- Chargement progressif de l'interface
- Persistance de l'état de l'arborescence

---

### 🔮 Évolutions prévues

#### 🚀 Versions à venir

- **v0.18.0** — Refonte UI, thèmes & audit UX
- **v0.19.0** — Finalisation & distribution

#### 🎯 Principaux objectifs

- Refonte de l'interface utilisateur
- Finalisation du système de thèmes
- Audit UX/UI guidé
- Harmonisation de l'identité visuelle
- Préparation de la distribution
- Validation finale avant publication

#### 📄 Documentation

> Consultez la [🗺️ ROADMAP](./ROADMAP.md) pour le détail des versions.

---

### 📍 État actuel

#### 🏗️ Architecture

- Architecture ALC stabilisée
- Pipeline centralisé
- Aperçu et export cohérents
- Journaux intégrés
- Mode développeur disponible

#### 🖥️ Interface

- Aperçu asynchrone stabilisé
- Arborescence persistante
- Recherche et exclusions stabilisées
- Chargement progressif de l'interface

#### ⭐ Points forts

- Simple
- Rapide
- Stable
- Lisible
- Prévisible
- Non destructif
- Lecture seule

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="documentation"></a>

### **📚 08. Documentation**

### 📁 Structure du projet

#### 📋 Organisation

```text
Core/
    Logique métier

UI/
    Interface utilisateur (WinUI 3)

Resources/
    Ressources de l'application

Tests/
    Tests automatisés
```

#### 📄 Documentation

> Consultez [📂 DIRECTORY_STRUCTURE](./DIRECTORY_STRUCTURE.md) pour la structure complète du projet.

---

### 🗂️ Documents du projet

#### 📖 Documentation générale

- 📖 [README](./README.md)
- 📝 [PATCH NOTES](./PATCH_NOTES.md)
- 🗺️ [ROADMAP](./ROADMAP.md)

#### 🛠️ Documentation technique

- 🏗️ [ARCHITECTURE](./ARCHITECTURE.md)
- 🖥️ [UI GUIDE](./UI_GUIDE.md)
- 🧪 [TESTS](./TESTS.md)
- 📂 [DIRECTORY STRUCTURE](./DIRECTORY_STRUCTURE.md)

#### 📘 Guides

- 📘 [GUIDE UTILISATEUR](./GUIDE_UTILISATEUR.md)
- 📝 [GUIDE COMMITS](./GUIDE_COMMITS.md)
- 💻 [GUIDE GITHUB DESKTOP](./GUIDE_GITHUB_DESKTOP.md)

#### ⚖️ Licence

- ⚖️ [LICENSE](./LICENSE.md)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="philosophie"></a>

### **🧠 09. Philosophie**

### 🚫 Ce que LatuCollect n'est pas

LatuCollect n'est pas un outil destiné à :

- Analyser du code
- Modifier ou transformer du code
- Parser des projets complexes

#### 📋 Limitations

Il ne réalise pas :

- Aucune analyse de code
- Aucune modification des fichiers d'origine
- Aucun parsing complexe
- Aucune génération ou réécriture de code
- Aucune interprétation du contenu des fichiers

#### 💡 Pourquoi ?

LatuCollect se concentre sur une seule mission :

> Collecter, assembler et exporter le contenu des fichiers en lecture seule.

> Copier intelligent uniquement.

---

### ✅ Ce que LatuCollect fait

LatuCollect est conçu pour :

- Collecter
- Assembler
- Exporter

#### 🧭 Mission

> Collecter, assembler et exporter le contenu des fichiers, sans le modifier.

> ✔ Copier intelligent uniquement.

#### 📐 Principes

- Simplicité
- Lisibilité
- Stabilité
- Prévisibilité
- Lecture seule
- Non destructif

---

### ⚖️ Choix de conception

#### 🛠️ Décisions

Afin de conserver une application simple, stable et prévisible, LatuCollect ne met volontairement pas en œuvre certaines optimisations complexes.

Par exemple :

- Pas de virtualisation avancée
- Pas de chargement hiérarchique différé (lazy loading)
- Chargement partiel pour les très gros projets

#### 🎯 Objectif

- Simplicité
- Stabilité
- Maintenabilité
- Prévisibilité
