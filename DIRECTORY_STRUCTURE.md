<div align="center">

# 📁 DIRECTORY_STRUCTURE – LATUCOLLECT

### Organisation officielle des dossiers du projet

🔹 Structure du projet
🔹 Organisation des dossiers
🔹 Architecture physique
🔹 État actuel et structure cible

</div>

Ce document décrit l'organisation officielle des dossiers et des fichiers du projet **LatuCollect**.

Il présente la structure actuellement implémentée, la structure cible définie par l'architecture **ALC**, ainsi que les règles d'organisation permettant de garantir la cohérence, la lisibilité et la maintenabilité du projet.

> [!IMPORTANT]
> Ce document constitue la **référence officielle** concernant l'organisation des projets, des dossiers et des fichiers de **LatuCollect**.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

### **📖 Sommaire**

#### Général

- [📁 01. Structure du projet](#structure-du-projet)
- [🎯 02. Objectif](#objectif)
- [🧩 03. Structure principale](#structure-principale)
- [⚠️ 04. Informations importantes](#informations-importantes)
- [📦 05. Description des dossiers](#description-des-dossiers)
- [🧠 06. Architecture MVVM](#architecture-mvvm)
- [🔄 07. Fonctionnement global](#fonctionnement-global)
- [🖥️ 08. Structure de l'interface](#structure-interface)
- [🔄 09. Communication](#communication)
- [⚠️ 10. Règles d'organisation](#regles-organisation)
- [🧠 11. Philosophie](#philosophie)
- [🧩 12. Structure actuelle](#structure-actuelle)
- [🔄 13. Différences avec la structure cible](#differences-cible)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="structure-du-projet"></a>

### **📁 01. Structure du projet**

#### 📋 Vue d'ensemble

Cette section présente l'organisation générale des projets, des dossiers et des principaux fichiers qui composent **LatuCollect**.

Elle distingue la structure actuellement implémentée de la structure cible définie par l'architecture **ALC** afin de faciliter la compréhension, la maintenance et l'évolution du projet.

> [!NOTE]
> La structure cible correspond à l'organisation visée à long terme.
> Certaines parties sont encore en cours de migration et seront implémentées progressivement au fil des versions.

> Pour suivre l'avancement de cette évolution, consulter la [ROADMAP](./ROADMAP.md).

---

#### 🎯 Objectifs

- ✅ Présenter l'organisation générale de la solution.
- ✅ Décrire le rôle des principaux projets et dossiers.
- ✅ Distinguer la structure actuelle de la structure cible.
- ✅ Garantir une organisation cohérente de la solution.
- ✅ Faciliter les évolutions de l'architecture.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="objectif"></a>

### **🎯 02. Objectif**

#### 📋 Vue d'ensemble

Cette section présente les principaux objectifs de l'organisation des projets, des dossiers et des fichiers de **LatuCollect**.

Une structure claire facilite la navigation dans le dépôt, améliore la compréhension de l'architecture, simplifie la maintenance et accompagne l'évolution progressive du projet.

---

#### 🎯 Objectifs

- ✅ Faciliter la navigation dans le projet.
- ✅ Identifier rapidement le rôle de chaque projet et dossier.
- ✅ Préserver une organisation cohérente.
- ✅ Simplifier la maintenance du code.
- ✅ Favoriser une évolution progressive de l'architecture.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="structure-principale"></a>

### **🧩 03. Structure principale**

#### 📋 Vue d'ensemble

Cette section présente l'organisation cible de la solution **LatuCollect**.

Elle illustre la répartition des principaux projets, dossiers et composants conformément aux principes de l'architecture **ALC**.

> [!NOTE]
> Cette structure représente l'organisation cible du projet.
> Certaines parties sont encore en cours de migration et seront mises en place progressivement au fil des versions.

---

#### 📂 Arborescence cible

```text
LatuCollect/
│
├── Core/
│   ├── Configuration/
│   ├── Interfaces/
│   ├── Logging/
│   ├── Models/
│   └── Services/
│       ├── Export/
│       ├── Import/
│       ├── Reader/
│       └── Statistics/
│
├── UI/
│   └── WinUI/
│       ├── Converters/
│       ├── Models/
│       ├── Settings/
│       └── ViewModels/
│           ├── Export/
│           ├── Logs/
│           ├── Preview/
│           ├── Settings/
│           └── TreeView/
│
├── Tests/
│
├── README.md
├── ARCHITECTURE.md
├── DIRECTORY_STRUCTURE.md
├── GUIDE_UTILISATEUR.md
├── PATCH_NOTES.md
├── ROADMAP.md
├── TESTS.md
└── UI_GUIDE.md
```

---

#### 📋 Principes d'organisation

- ✅ La structure évolue progressivement au fil des versions.
- ✅ Chaque projet et chaque dossier possède une responsabilité clairement définie.
- ✅ Le découpage du `MainViewModel` est réalisé progressivement afin de limiter les régressions.
- ✅ Les interfaces sont progressivement centralisées dans le Core.
- ✅ L'organisation respecte les principes de l'architecture **ALC**.
- ✅ Toute évolution importante doit rester cohérente avec la `ROADMAP.md`.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="informations-importantes"></a>

### **⚠️ 04. Informations importantes**

#### 📋 Vue d'ensemble

Cette section regroupe les principales informations à connaître concernant l'organisation du projet et l'évolution de sa structure.

---

#### 📌 Points importants

- ✅ La structure présentée dans ce document correspond à l'organisation cible de LatuCollect.
- ✅ Certaines parties sont encore en cours de migration.
- ✅ Les évolutions sont réalisées progressivement afin de limiter les régressions.
- ✅ Toute modification importante doit rester cohérente avec la `ROADMAP.md`.
- ✅ L'organisation des dossiers doit respecter les principes de l'architecture **ALC**.

---

#### 📊 État actuel

- ✅ Le système de simulation a été supprimé (v0.13.0).
- ✅ L'architecture a été progressivement simplifiée.
- ✅ Le découpage des ViewModels est en cours de finalisation.
- ✅ L'organisation du projet continue d'évoluer au fil des versions.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="description-des-dossiers"></a>

### **📦 05. Description des dossiers**

#### 📋 Vue d'ensemble

Cette section présente le rôle des principaux dossiers qui composent la solution **LatuCollect**.

Chaque dossier possède une responsabilité clairement définie afin de respecter les principes de l'architecture **ALC**.

---

#### 🧠 Core/

Contient l'ensemble de la logique métier de l'application.

Le **Core** constitue le cœur fonctionnel de LatuCollect et reste totalement indépendant de l'interface utilisateur.

##### 📋 Responsabilités

- ✅ Import des fichiers.
- ✅ Lecture du contenu.
- ✅ Assemblage des données.
- ✅ Calcul des statistiques.
- ✅ Génération des exports.
- ✅ Gestion de la configuration.
- ✅ Gestion du logging.

---

#### 🖥️ UI/

Contient l'interface utilisateur **WinUI** ainsi que les composants dédiés à l'affichage et aux interactions avec l'utilisateur.

##### 📋 Responsabilités

- ✅ Affichage des données.
- ✅ Interactions utilisateur.
- ✅ Gestion des ViewModels.
- ✅ Présentation de l'aperçu.
- ✅ Communication avec le Core via les ViewModels.

---

#### 🧪 Tests/

Regroupe les tests automatisés du projet.

##### 📋 Responsabilités

- ✅ Tests du Core.
- ✅ Tests des ViewModels.
- ✅ Validation du comportement de l'application.
- ✅ Détection des régressions.

---

#### ⚙️ Configuration/

Regroupe les composants liés à la configuration globale et utilisateur.

##### 📋 Éléments principaux

- ✅ `AppConfig`
- ✅ `UserConfig`

> Les détails du fonctionnement de ces composants sont présentés dans `ARCHITECTURE.md`.

---

#### 🧾 Logging/

Regroupe les composants dédiés à la journalisation et au diagnostic de l'application.

##### 📋 Responsabilités

- ✅ Journalisation des événements.
- ✅ Gestion des erreurs.
- ✅ Outils de diagnostic.
- ✅ Suivi des traitements du Core.

---

#### 📌 Remarque

Les dossiers présentés dans cette section constituent les principaux composants de la solution.

Leur organisation peut évoluer progressivement afin de rester cohérente avec l'architecture **ALC** et la `ROADMAP.md`.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="architecture-mvvm"></a>

### **🧠 06. Architecture MVVM**

#### 📋 Vue d'ensemble

LatuCollect repose sur une architecture **MVVM (Model - View - ViewModel)** associée aux principes de l'architecture **ALC**.

Cette organisation permet de séparer clairement l'interface utilisateur de la logique métier afin de faciliter la maintenance, les tests et l'évolution du projet.

---

#### 🔄 Organisation générale

```text
View
   │
   ▼
ViewModel
   │
   ▼
Core
```

---

#### 📋 Responsabilités

**🖥️ View**

- ✅ Afficher les informations.
- ✅ Capturer les interactions utilisateur.
- ✅ Transmettre les actions au ViewModel.

---

**⚙️ ViewModel**

- ✅ Gérer l'état de l'interface utilisateur.
- ✅ Orchestrer les traitements.
- ✅ Communiquer avec le Core.
- ✅ Convertir les données entre l'UI et le Core.

---

**🧠 Core**

- ✅ Contenir la logique métier.
- ✅ Regrouper les services.
- ✅ Produire les données utilisées par l'interface.
- ✅ Rester indépendant de l'interface utilisateur.

---

#### 📌 Remarque

L'architecture **MVVM** constitue le lien entre l'interface utilisateur et les services du **Core**.

Elle contribue à limiter le couplage entre les composants et facilite les évolutions de l'application.

> Pour une description détaillée de l'architecture **ALC** et des responsabilités de chaque couche, consulter [📖 ARCHITECTURE.md](./ARCHITECTURE.md).

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="fonctionnement-global"></a>

### **🔄 07. Fonctionnement global**

#### 📋 Vue d'ensemble

Cette section présente le fonctionnement général de **LatuCollect**, depuis l'import des fichiers jusqu'à la génération du document exporté.

Elle met en évidence le parcours suivi par l'utilisateur ainsi que le pipeline interne utilisé par le **Core**.

---

#### 🖥️ Pipeline utilisateur

Le parcours utilisateur suit les étapes suivantes :

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

#### ⚙️ Pipeline interne

Le pipeline interne du **Core** réalise les traitements suivants :

```text
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

---

#### 📋 Fonctionnement

- ✅ L'utilisateur importe un projet ou un dossier.
- ✅ Les fichiers sont sélectionnés dans l'arborescence.
- ✅ Le **Core** construit la collection des fichiers sélectionnés.
- ✅ Le **Core** génère le contenu utilisé par l'aperçu et l'export.
- ✅ Les statistiques sont calculées à partir des données du pipeline.

---

#### 📌 Remarque

Le pipeline utilisateur décrit les actions réalisées dans l'interface, tandis que le pipeline interne représente les traitements exécutés par le **Core**.

> Pour une description détaillée du pipeline et des services associés, consulter [📖 ARCHITECTURE.md](./ARCHITECTURE.md).

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="structure-interface"></a>

### **🖥️ 08. Structure de l'interface**

#### 📋 Vue d'ensemble

Cette section présente l'organisation générale de l'interface utilisateur de **LatuCollect**.

L'interface est répartie en plusieurs zones ayant chacune une responsabilité clairement définie afin de garantir une utilisation simple, cohérente et prévisible.

---

#### 🧩 Organisation générale

```text
┌──────────────────────────────────────────────────────────────┐
│ Projet               │ Options │                      Aperçu │
├──────────────────────────────────────────────────────────────┤
│                          Actions                             │
└──────────────────────────────────────────────────────────────┘
```

---

#### 📋 Répartition des zones

- **Gauche** → Projet (arborescence)
- **Centre** → Options (configuration et paramètres)
- **Droite** → Aperçu
- **Bas** → Actions principales

---

#### 📌 Remarque

Cette organisation constitue la structure officielle de l'interface utilisateur de **LatuCollect**.

Toute évolution de l'interface doit préserver cette disposition afin de garantir une expérience utilisateur cohérente.

> Pour une description détaillée des composants, des comportements et des règles de l'interface, consulter [📖 UI_GUIDE.md](./UI_GUIDE.md).

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="communication"></a>

### **🔄 09. Communication**

#### 📋 Vue d'ensemble

Cette section présente les règles de communication entre les différentes couches de **LatuCollect**.

Les échanges suivent une direction unique afin de préserver une architecture modulaire, de limiter le couplage et de garantir une séparation claire des responsabilités.

---

#### 🔄 Flux de communication

```text
Interface utilisateur
        │
        ▼
   ViewModel
        │
        ▼
      Core
```

---

#### 📋 Règles

- ✅ L'interface utilisateur communique uniquement avec les ViewModels.
- ✅ Les ViewModels communiquent avec le Core.
- ✅ Le Core reste totalement indépendant de l'interface utilisateur.
- ✅ Les échanges entre les couches respectent l'architecture **ALC**.
- 🟥 Le Core ne dépend jamais de l'UI.
- 🟥 Les communications directes entre l'UI et le Core sont interdites.

---

#### 📌 Remarque

Cette organisation garantit une architecture plus simple, plus modulaire et plus facile à maintenir.

Elle permet également de limiter les dépendances entre les composants et de faciliter les évolutions du projet.

> Pour une description détaillée des responsabilités de chaque couche, consulter [📖 ARCHITECTURE.md](./ARCHITECTURE.md).

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="regles-organisation"></a>

### **⚠️ 10. Règles d'organisation**

#### 📋 Vue d'ensemble

Cette section regroupe les principales règles d'organisation des projets et des dossiers de **LatuCollect**.

Le respect de ces règles garantit une architecture cohérente, une maintenance facilitée et une évolution progressive du projet.

---

#### 📋 Règles

- ✅ Le **Core** ne dépend jamais de l'interface utilisateur.
- ✅ L'interface utilisateur communique avec le **Core** uniquement via les **ViewModels**.
- ✅ Chaque projet et chaque dossier possède une responsabilité clairement définie.
- ✅ La logique métier est centralisée dans le **Core**.
- ✅ L'interface utilisateur est dédiée à l'affichage et aux interactions.
- ✅ Les **ViewModels** assurent l'orchestration entre l'UI et le **Core**.
- ✅ Les composants sont organisés par domaine fonctionnel.
- ✅ Toute évolution de la structure doit rester cohérente avec l'architecture **ALC**.

---

#### 📌 Remarque

Ces règles constituent les fondements de l'organisation du projet.

Elles doivent être respectées lors de toute évolution de la structure afin de préserver la cohérence, la stabilité et la maintenabilité de **LatuCollect**.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="philosophie"></a>

### **🧠 11. Philosophie**

#### 📋 Vue d'ensemble

LatuCollect est conçu autour d'une architecture volontairement simple.

Chaque choix technique vise à limiter la complexité, à faciliter la compréhension du code et à garantir une évolution progressive de l'application.

---

#### 📋 Principes

- ✅ Privilégier la simplicité.
- ✅ Concevoir un code clair, lisible et maintenable.
- ✅ Organiser les composants par responsabilité.
- ✅ Favoriser une architecture modulaire.
- ✅ Préserver un comportement prévisible.
- ✅ Faire évoluer le projet de manière progressive.

---

#### 💡 Principe fondamental

> Une solution simple, cohérente et maintenable est toujours préférable à une architecture sur-ingénierée.

> Toute complexité doit être justifiée par un bénéfice réel pour l'application.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="structure-actuelle"></a>

### **🧩 12. Structure actuelle**

#### 📋 Vue d'ensemble

Cette section présente l'organisation actuellement implémentée de la solution **LatuCollect**.

Elle reflète l'état réel du projet au moment de la rédaction de cette documentation et permet de comparer la structure existante avec la structure cible présentée précédemment.

> [!NOTE]
> Cette arborescence reflète l'état actuel du projet au moment de la rédaction de cette documentation.
> Elle évoluera progressivement conformément à la `ROADMAP.md`.

---

#### 📂 Arborescence actuelle

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
    ├── Preview/
    │   └── PreviewViewModel.cs
    ├── Settings/
    │   └── SettingsViewModel.cs
    └── TreeView/
        └── TreeViewViewModel.cs

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

#### 📊 État actuel

- ✅ Architecture **ALC** opérationnelle.
- ✅ Organisation du **Core** stabilisée.
- ✅ Séparation entre le **Core**, l'interface utilisateur et les tests.
- ✅ Découpage du `MainViewModel` largement avancé.
- ✅ `LogsViewModel`, `TreeViewViewModel`, `PreviewViewModel` et `SettingsViewModel` sont désormais séparés.
- ✅ Les futures évolutions continueront de respecter les principes de l'architecture **ALC**.

---

#### 📄 Références

Pour suivre l'évolution de cette structure et de l'architecture du projet, consulter :

- `ARCHITECTURE.md`
- `ROADMAP.md`
- `PATCH_NOTES.md`

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="differences-cible"></a>

### **🔄 13. Différences avec la structure cible**

#### 📋 Vue d'ensemble

Cette section présente les principaux écarts entre la structure actuellement implémentée et l'organisation cible définie pour LatuCollect.

Les évolutions sont réalisées progressivement afin de préserver la stabilité de l'application et de limiter les régressions.

---

#### ✅ Éléments déjà implémentés

- ✅ Architecture **ALC** opérationnelle.
- ✅ Services principaux du Core.
- ✅ Logging centralisé.
- ✅ Séparation `AppConfig` / `UserConfig`.
- ✅ Séparation du Core, de l'UI et des tests.
- ✅ Organisation des services par domaine fonctionnel.
- ✅ Découpage progressif du `MainViewModel`.
- ✅ `LogsViewModel` spécialisé.
- ✅ `TreeViewViewModel` spécialisé.
- ✅ `PreviewViewModel` largement avancé.
- ✅ `SettingsViewModel` largement avancé.
- ✅ Centralisation progressive des responsabilités des ViewModels.

---

#### 🟡 Évolutions en préparation

- 🟡 Refonte progressive de l'interface utilisateur.
- 🟡 Finalisation des thèmes clair et sombre.
- 🟡 Harmonisation des couleurs, des icônes et de la typographie.
- 🟡 Amélioration de la hiérarchie visuelle et des espacements.
- 🟡 Audit UX/UI progressif des principaux composants de l'application.

---

#### ⏳ Évolutions prévues

- ⏳ Validation complète de l'application sur de très gros projets.
- ⏳ Validation des performances, de la consommation mémoire et des exports massifs.
- ⏳ Génération d'une version Release.
- ⏳ Préparation de la distribution de l'application.
- ⏳ Création de l'installateur et validation de l'installation.
- ⏳ Finalisation de LatuCollect pour une utilisation en production.

---

#### 📄 Références

Pour connaître le détail des évolutions prévues, consulter :

- `ROADMAP.md`
- `PATCH_NOTES.md`
