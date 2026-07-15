<div align="center">

### **🏗️ ARCHITECTURE – LATUCOLLECT (ALC)**

### Standard officiel de l'architecture du projet

🔹 Architecture ALC
🔹 Règles de développement
🔹 Pipeline métier
🔹 Organisation du code

</div>

Ce document définit le **standard officiel** de l'architecture **ALC (Architecture LatuCollect)**.

Il décrit les **principes de conception**, les **règles de développement**, l'organisation interne du projet ainsi que les **bonnes pratiques** à respecter.

> Il constitue la **référence technique** pour toute modification ou ajout de fonctionnalité.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

### **📖 Sommaire**

#### Général

- [🏠 01. Présentation](#presentation)
- [🏗️ 02. Principes ALC](#principes-alc)
- [📄 03. Standards de développement](#standards-de-developpement)
- [🧱 04. Architecture des couches](#architecture-des-couches)
- [⚙️ 05. Configuration](#configuration)
- [🔄 06. Pipeline](#pipeline)
- [🛠️ 07. Services](#services)
- [📊 08. Statistiques](#statistiques)
- [🚄 09. Performances](#performances)
- [🖥️ 10. Interface WinUI](#interface-winui)
- [👨🏻‍💻 11. Mode développeur](#mode-developpeur)
- [📤 12. Export](#export)
- [📁 13. Structure du projet](#structure-du-projet)
- [🔄 14. Traitements asynchrones](#traitements-asynchrones)
- [🧾 15. Journalisation](#journalisation)
- [🔌 16. Injection de dépendances](#injection-de-dependances)
- [📍 17. État actuel](#etat-actuel)
- [🚀 18. Évolutions](#evolutions)
- [🧪 19. Validation & tests](#validation-tests)
- [🎯 20. Objectif global](#objectif-global)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="presentation"></a>

### **🏠 01. Présentation**

#### 💡 Référence rapide

**ALC** signifie **Architecture LatuCollect**.

Cette architecture définit l'organisation du projet et les règles qui garantissent sa **stabilité**, sa **lisibilité** et sa **maintenabilité**.

L'architecture ALC repose sur une séparation claire des responsabilités entre le Core, l'interface utilisateur et les ViewModels.

Elle garantit un pipeline de traitement cohérent, une évolution progressive de l'application ainsi que la stabilité des fonctionnalités existantes.

Ce document permet notamment de :

- ✅ Structurer le code.
- ✅ Comprendre le fonctionnement interne.
- ✅ Garantir la cohérence globale.
- ✅ Uniformiser les développements.

---

#### 🎯 Objectifs du standard

- ✅ Lisibilité.
- ✅ Cohérence architecturale.
- ✅ Maintenabilité.
- ✅ Évolutivité.
- ✅ Discipline de développement.
- ✅ Valeur pédagogique.

> Ce standard s'applique à chaque fichier créé ou modifié dans le projet.

---

#### ⚠️ Règle critique

Avant toute modification importante :

- ✅ Analyser les fichiers concernés.
- ✅ Comprendre le fonctionnement actuel.
- ✅ Vérifier la cohérence avec l'architecture ALC.
- ✅ Vérifier la cohérence avec la ROADMAP.
- ✅ Vérifier la cohérence avec les PATCH_NOTES.
- ✅ Vérifier les fonctionnalités déjà implémentées.
- ✅ Préserver la stabilité de l'application.
- ✅ Ne jamais modifier le code sans comprendre les impacts.

#### 🟥 Interdit

Les pratiques suivantes sont proscrites :

- 🟥 Ajouter du code sans analyser l'existant.
- 🟥 Modifier à l'aveugle.
- 🟥 Casser la structure du projet.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="principes-alc"></a>

### **🏗️ 02. Principes ALC**

#### 🏛️ Présentation

L'architecture **ALC (Architecture LatuCollect)** constitue le socle technique du projet.

Elle définit la manière dont les responsabilités sont réparties entre les différentes couches de l'application afin de garantir un code **simple**, **lisible** et **évolutif**.

---

#### 📐 Principes fondamentaux

- ✅ Une responsabilité par composant.
- ✅ Le Core contient la logique métier.
- ✅ L'interface utilisateur ne contient pas de logique métier.
- ✅ Les ViewModels orchestrent les traitements.
- ✅ Les services réalisent les opérations métier.
- ✅ Une source unique de vérité (`Single Source of Truth`).
- ✅ Le contenu affiché correspond toujours au contenu exporté (`Preview = Export`).

---

#### 📋 Règles de conception

- ✅ Privilégier la simplicité.
- ✅ Limiter les effets de bord.
- ✅ Préserver la stabilité de l'application.
- ✅ Respecter l'architecture ALC pour toute nouvelle fonctionnalité.
- ✅ Éviter les duplications de code.
- ✅ Préserver le pipeline métier unique.

### 🧠 Philosophie

LatuCollect est volontairement conçu pour rester simple.

Son objectif est de copier intelligemment le contenu sélectionné, sans chercher à analyser ou transformer le code.

Il ne constitue pas un analyseur de code.

Son rôle est uniquement de collecter, assembler et exporter le contenu des fichiers sélectionnés.

#### 📋 Principes

- ✅ Copier intelligent uniquement.
- ✅ Pas d'analyse de code.
- ✅ Pas de transformation du contenu.
- ✅ Pas de parsing complexe.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="standards-de-developpement"></a>

### **📄 03. Standards de développement**

Cette section regroupe les conventions de développement utilisées dans l'ensemble du projet.

Elle définit les règles communes relatives à l'organisation des fichiers, à la structure du code, au nommage et aux bonnes pratiques afin de garantir une base de code homogène, lisible et facile à maintenir.

Les standards présentés dans cette section s'appliquent à tout nouveau fichier créé ou modifié dans LatuCollect.

---

### 📋 Organisation des fichiers

#### 📄 En-têtes des fichiers

Tous les fichiers source du projet (`.cs` et `.xaml`) doivent commencer par un **en-tête standardisé**.

Cet en-tête facilite l'identification des fichiers, améliore la lisibilité du code et contribue à l'homogénéité du projet.

##### 📄 Modèle C#

À utiliser pour tous les fichiers `.cs` du projet.

```csharp
/*
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : NomDuModule                                                ║
║  Fichier : NomDuFichier.cs                                           ║
║                                                                      ║
║  Rôle :                                                              ║
║  Description précise du rôle du fichier                              ║
║                                                                      ║
║  Responsabilités principales :                                       ║
║  - À détailler                                                       ║
║                                                                      ║
║  Dépendances :                                                       ║
║  - Services utilisés                                                 ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
*/
```

---

##### 📄 Modèle XAML

À utiliser pour tous les fichiers `.xaml` du projet.

```xml
<!--
╔══════════════════════════════════════════════════════════════════════╗
║                        LATUCOLLECT                                   ║
║     Application de collecte et export de contenu multi-fichiers      ║
║                                                                      ║
║  Module : UI                                                         ║
║  Fichier : NomDuFichier.xaml                                         ║
║                                                                      ║
║  Rôle :                                                              ║
║  Description claire du rôle de cette vue                             ║
║                                                                      ║
║  Licence : MIT                                                       ║
║  Copyright © 2026 Flo Latury                                         ║
╚══════════════════════════════════════════════════════════════════════╝
-->
```

---

#### 🧱 Structure des fichiers C#

Les fichiers C# du projet suivent une **structure commune** afin de garantir une lecture claire, une maintenance facilitée et une homogénéité de l'ensemble du code.

##### 📋 Organisation du fichier

Chaque fichier C# doit respecter l'ordre suivant :

- ✅ Directives `using`.
- ✅ En-tête du fichier.
- ✅ Déclaration de l'espace de noms (`namespace`).
- ✅ Déclaration de la classe ou de l'interface.

---

##### 📋 Organisation de la classe

Les éléments d'une classe sont organisés dans l'ordre suivant :

- ✅ Champs privés.
- ✅ Propriétés.
- ✅ Constructeurs.
- ✅ Méthodes publiques.
- ✅ Méthodes privées.
- ✅ Types imbriqués (si nécessaire).

---

#### 📌 Règles d'organisation

Les recommandations suivantes permettent de garantir une organisation homogène des fichiers dans l'ensemble du projet.

Pour les en-têtes :

- ✅ Conserver l'en-tête en première ligne du fichier.
- ✅ Adapter le rôle et les responsabilités au contenu réel du fichier.
- ✅ Ne pas supprimer les informations de licence.
- ✅ Mettre à jour les dépendances lorsque le fichier évolue.
- ✅ Respecter le modèle officiel sans modifier sa structure.

---

Pour la structure des fichiers :

- ✅ Respecter cet ordre pour tous les nouveaux fichiers.
- ✅ Regrouper les éléments de même nature.
- ✅ Éviter les déclarations inutiles.
- ✅ Conserver une structure identique dans l'ensemble du projet.
- ✅ Respecter la séparation des responsabilités (UI / ViewModel / Core).

---

### 📝 Qualité du code

Les règles suivantes définissent les conventions de rédaction du code afin de garantir sa lisibilité, sa cohérence et sa maintenabilité.

#### 📝 Documentation

Les émojis sont utilisés uniquement dans la documentation officielle du projet afin d'améliorer la lisibilité.

#### 📋 Règles

- ✅ Autorisés dans la documentation.
- ✅ Utilisés uniquement comme aide visuelle.
- 🟥 Interdits dans le code source final.

#### 💬 Commentaires

Les commentaires doivent documenter uniquement les éléments qui apportent une **réelle valeur** à la compréhension du code.

Les commentaires peuvent notamment préciser :

- ✅ Le rôle d'une classe.
- ✅ Les responsabilités principales.
- ✅ L'objectif d'une méthode.
- ✅ Les paramètres importants.
- ✅ La valeur de retour lorsque nécessaire.
- 🟥 Éviter les commentaires qui répètent simplement le code.

---

#### 🏷️ Nommage

Les conventions de nommage suivantes sont utilisées dans **l'ensemble du projet** :

- ✅ PascalCase pour les classes, propriétés et méthodes publiques.
- ✅ camelCase pour les variables locales et paramètres.
- ✅ Préfixe `I` pour les interfaces.
- ✅ Suffixe `Service` pour les services.
- ✅ Suffixe `ViewModel` pour les ViewModels.
- ✅ Utiliser des noms explicites et représentatifs.

---

#### 💡 Principes de développement

- ✅ Privilégier un code simple et lisible.
- ✅ 1 classe = 1 responsabilité.
- ✅ Éviter les duplications de code.
- ✅ Pas de code mort.
- ✅ Pas de logique métier complexe dans UI.
- ✅ Pas de logique UI dans Core.
- ✅ Réduire les dépendances inutiles entre Core et UI.
- ✅ Pas de valeurs en dur dans l'UI.
- ✅ Dépendre des interfaces lorsque cela est pertinent.
- ✅ Privilégier les traitements asynchrones lorsqu'ils évitent le blocage de l'interface.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="architecture-des-couches"></a>

### **🧱 04. Architecture des couches**

Cette section présente l'organisation générale de l'architecture ALC.

Elle décrit les différentes couches de l'application, leurs responsabilités ainsi que les règles qui encadrent leurs interactions.

---

### 🏛️ Vue d'ensemble

La séparation en couches est l'un des **principes fondamentaux** de l'architecture **ALC**.

Chaque couche possède une **responsabilité clairement définie** afin de limiter le couplage et de faciliter la maintenance de l'application.

L'application repose sur deux couches principales :

| Couche | Responsabilité                        |
| ------ | ------------------------------------- |
| Core   | Logique métier                        |
| UI     | Affichage et interactions utilisateur |

🟥 Interdit :

- 🟥 Logique métier complexe dans UI.
- 🟥 Accès fichiers depuis UI.

#### ⚠️ Tolérance MVP

Pendant la phase de transition de l'architecture :

- ✅ Une logique d'orchestration peut encore être présente dans certains ViewModels pendant les migrations en cours.
- ✅ La configuration globale appartient au Core (`AppConfig`).

---

### 🧠 Core

Le Core regroupe l'ensemble de la logique métier de LatuCollect.

Il est totalement **indépendant** de l'interface utilisateur et constitue le **cœur fonctionnel** de l'application.

- ✅ Contient la logique métier.
- ✅ Regroupe les services métier.
- ✅ Contient la configuration globale (`AppConfig`).
- ✅ Contient la configuration utilisateur (`UserConfig`).

---

### 🖥️ Interface utilisateur

La couche UI est composée des vues et des ViewModels.

Les vues assurent l'affichage tandis que les ViewModels orchestrent les interactions avec le Core.

#### 🧩 ViewModel

Les ViewModels assurent l'orchestration entre l'interface utilisateur et les services du Core.

- ✅ Gèrent l'état de l'interface.
- ✅ Orchestrent les actions utilisateur.
- ✅ Appliquent les filtres (recherche, visibilité).
- ✅ Déclenchent les opérations d'import et de chargement.

---

#### 🔹 Rôle réel du ViewModel

**✅ Responsabilités**

- Gérer l'état de l'interface utilisateur.
- Convertir les données UI ↔ Core.
- Appeler les services du Core.
- Déclencher le rafraîchissement de l'aperçu.

---

**🟥 Ce qui ne relève pas du ViewModel**

- Lire directement les fichiers.
- Assembler le contenu.
- Réaliser des calculs métier complexes.

---

### 🔗 Communication entre les couches

#### 🔄 Conversion UI ↔ Core

Le projet utilise deux modèles distincts :

- `UI.Models.FileNode`
- `Core.Models.FileNode`

#### 🎯 Pourquoi deux modèles ?

- Séparer les responsabilités.
- Éviter les dépendances de l'UI vers le Core.

#### 🔄 Fonctionnement

- Conversion UI → Core avant appel des services.
- Conversion Core → UI lors du chargement.

> La conversion entre les modèles UI et Core est principalement assurée par les ViewModels. Certaines conversions peuvent également être réalisées par des services spécialisés lorsque cela est pertinent.

---

### 🧩 Architecture des ViewModels

Les **ViewModels** constituent la couche d'orchestration entre l'interface utilisateur et le **Core**.

Chaque **ViewModel** est spécialisé dans un **domaine fonctionnel** afin de limiter les responsabilités et de faciliter la maintenance de l'application.

Les ViewModels :

- ✅ Gèrent l'état de l'interface.
- ✅ Communiquent avec les services du Core.
- ✅ Coordonnent les différents ViewModels lorsque cela est nécessaire.
- ✅ Orchestrent les interactions utilisateur.
- ✅ N'implémentent pas de logique métier complexe.
- ✅ Peuvent collaborer entre eux lorsque cela est nécessaire.

---

### 🗂️ Modèles de données

Les modèles de la couche **UI** représentent les données manipulées par l'interface.

> Ils ne contiennent **aucune logique métier**.

#### 🗂️ Models (UI)

- ✅ Structure des données (`FileNode`).
- ✅ Représentent les données nécessaires à l'interface.
- 🟥 Aucune logique métier.

---

### 💡 Bonnes pratiques

Les règles suivantes garantissent le respect de l'architecture **ALC** dans l'ensemble du projet.

- ✅ Une responsabilité par composant.
- ✅ Le Core ne dépend jamais de l'UI.
- ✅ L'UI ne contient pas de logique métier.
- ✅ Les ViewModels orchestrent les traitements.
- ✅ Les modèles restent de simples structures de données.
- ✅ Limiter le couplage entre les couches.
- ✅ Préserver la règle **Preview = Export**.
- ✅ Réduire progressivement les responsabilités des composants les plus volumineux.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="configuration"></a>

### **⚙️ 05. Configuration**

Cette section présente le système de configuration de LatuCollect.

Elle décrit les différents fichiers de configuration, leurs responsabilités ainsi que les règles qui encadrent leur utilisation.

---

### 📋 Vue d'ensemble

Le système de configuration est séparé en deux parties :

- une configuration globale propre à l'application ;
- une configuration utilisateur propre à chaque installation.

Cette séparation permet de distinguer les paramètres communs à l'application des préférences propres à chaque utilisateur.

---

### ⚙️ Configuration globale

#### 📄 Fichier

`AppConfig.cs`

#### 📂 Emplacement

`Core/Configuration/AppConfig.cs`

#### ⚙️ Rôle

- ✅ Centraliser les paramètres globaux.
- ✅ Définir les exclusions système.
- ✅ Fournir les valeurs par défaut de l'application.

#### 📝 Exemple

```csharp
ExcludedFolders = ["bin", "obj", ".git"];
```

#### 💡 Responsabilités

`AppConfig` contient les paramètres communs à l'ensemble de l'application.

Son contenu est indépendant de l'utilisateur et sert de référence pour le fonctionnement global de LatuCollect.

---

### 👤 Configuration utilisateur

#### 📄 Fichier

`UserConfig.cs`

#### 📂 Emplacement

`Core/Configuration/UserConfig.cs`

#### ⚙️ Rôle

- ✅ Stocker les préférences utilisateur.
- ✅ Stocker les exclusions utilisateur.
- ✅ Conserver les paramètres persistants.
- ✅ Conserver certains états de l'interface.

---

#### 💡 Responsabilités

`UserConfig` enregistre les préférences propres à chaque utilisateur.

Il permet de conserver les préférences propres à chaque utilisateur entre deux utilisations de l'application, sans modifier la configuration globale définie par `AppConfig`.

---

### 🔄 Interaction entre les configurations

Les deux fichiers de configuration sont complémentaires.

- `AppConfig` définit les paramètres communs à toute l'application.
- `UserConfig` stocke les préférences propres à chaque utilisateur.

Cette séparation limite les dépendances et facilite l'évolution de l'application.

> La séparation entre `AppConfig` et `UserConfig` permet de faire évoluer les préférences utilisateur sans modifier le fonctionnement global de l'application.

---

### 📊 État actuel

- ✅ Séparation `AppConfig` / `UserConfig`.
- ✅ Configuration persistante opérationnelle.
- ✅ Écriture atomique des fichiers de configuration.
- ✅ Sauvegarde asynchrone sécurisée.

---

### 📌 Règles de configuration

- ✅ Utiliser `AppConfig` uniquement pour les paramètres globaux.
- ✅ Utiliser `UserConfig` pour les préférences utilisateur.
- ✅ Éviter les valeurs codées en dur dans le code.
- ✅ Centraliser les paramètres communs.
- ✅ Préserver la séparation entre configuration globale et configuration utilisateur.
- ✅ Conserver la configuration indépendante de l'interface utilisateur.
- ✅ Privilégier les sauvegardes asynchrones afin de préserver la fluidité de l'interface.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="pipeline"></a>

### **🔄 06. Pipeline**

Cette section décrit le fonctionnement du pipeline de LatuCollect.

Elle présente les différentes étapes de traitement des données, depuis l'import des fichiers jusqu'à leur export, ainsi que le rôle des principaux services impliqués.

---

### 📋 Vue d'ensemble

Le pipeline constitue le **cœur du traitement des données** de LatuCollect.

Chaque étape possède une responsabilité clairement définie et s'enchaîne dans un ordre précis afin de garantir un traitement cohérent des données.

LatuCollect s'appuie sur deux pipelines complémentaires :

#### 👤 Pipeline utilisateur

Le pipeline utilisateur représente le parcours suivi dans l'interface.

Il correspond aux différentes actions réalisées par l'utilisateur.

```text
Importer → Sélectionner → Aperçu → Exporter
```

> Ce pipeline correspond uniquement aux interactions visibles dans l'interface utilisateur.

---

#### ⚙️ Pipeline interne

Le pipeline interne est exécuté par le Core.

Contrairement au pipeline utilisateur, il décrit les traitements exécutés en interne par l'application.

Chaque étape est confiée à un ou plusieurs services spécialisés du Core.

```text
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

> Le résultat de ce pipeline est utilisé aussi bien pour l'aperçu que pour l'export final.

---

### 🛠️ Correspondance des services

Les principales étapes du pipeline sont prises en charge par des services spécialisés du Core.

| Étape        | Service               |
| ------------ | --------------------- |
| Import       | FileImportService     |
| Lecture      | FileReaderService     |
| Collection   | (pipeline interne)    |
| Assemblage   | FileExportService     |
| Statistiques | FileStatisticsService |
| Export       | FileExportService     |

Chaque service est responsable d'une étape précise du pipeline afin de limiter le couplage entre les composants.

---

### 🎯 Objectifs

Le pipeline a été conçu afin de répartir les traitements entre plusieurs services spécialisés.

Cette organisation permet notamment :

- ✅ Responsabilités mieux séparées.
- ✅ Pipeline plus lisible.
- ✅ Core plus maintenable.
- ✅ Tests plus simples.
- ✅ Architecture plus évolutive.

---

### 💡 Bonnes pratiques

Les recommandations suivantes permettent de garantir un pipeline cohérent et facilement maintenable.

- ✅ Respecter l'ordre des étapes du pipeline
- ✅ Confier chaque traitement au service prévu
- ✅ Éviter les responsabilités multiples dans un même service
- ✅ Ne pas contourner le pipeline
- ✅ Préserver une source unique de vérité
- ✅ Garantir la cohérence `Preview = Export`

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="services"></a>

### **🛠️ 07. Services**

Cette section présente les services spécialisés utilisés par le Core.

Elle décrit leurs responsabilités, leur rôle dans le pipeline ainsi que les règles qui encadrent leur utilisation.

---

### 📋 Vue d'ensemble

Les services du **Core** réalisent l'ensemble des traitements métier de LatuCollect.

Chacun possède une **responsabilité clairement définie** et intervient à une étape précise du pipeline interne.

Cette organisation permet de limiter le couplage entre **les composants**, de faciliter **les tests** et de rendre **l'architecture** plus simple à maintenir.

---

### 🧩 Services du Core

Les services sont organisés par domaine fonctionnel.

Chaque service réalise une tâche spécifique et communique avec les autres uniquement lorsque cela est nécessaire.

> Cette séparation des responsabilités constitue l'un des principes fondamentaux de l'architecture **ALC**.

---

### 📄 FileReaderService

#### ⚙️ Rôle

- ✅ Lire le contenu des fichiers.
- ✅ Gérer les encodages.
- ✅ Détecter les fichiers binaires.
- ✅ Détecter les fichiers verrouillés.

#### 💡 Responsabilités

Le `FileReaderService` est responsable de la lecture du contenu des fichiers du projet.

Il fournit au pipeline un contenu exploitable tout en gérant les encodages, les fichiers binaires et les erreurs de lecture.

Il ne modifie jamais le contenu des fichiers source.

---

### 📄 FileImportService

#### ⚙️ Rôle

- ✅ Importer les fichiers et dossiers.
- ✅ Construire l'arborescence utilisée par le pipeline.
- ✅ Préparer les données du Core.

#### 💡 Responsabilités

Le `FileImportService` prépare les données d'entrée du pipeline en construisant l'arborescence du projet.

Il recense les dossiers et les fichiers, construit la structure utilisée par l'application et fournit les informations nécessaires aux autres services du Core.

Il ne lit pas le contenu des fichiers, cette responsabilité étant assurée par le `FileReaderService`.

---

### 📄 FileExportService

#### ⚙️ Rôle

- ✅ Assembler le contenu des fichiers.
- ✅ Générer les exports TXT et Markdown.
- ✅ Structurer le document final.
- ✅ Garantir la cohérence `Preview = Export`.

#### 💡 Responsabilités

Le `FileExportService` assemble le contenu des fichiers sélectionnés afin de produire un document unique.

Le même contenu est ensuite utilisé pour générer l'aperçu et le fichier exporté, ce qui garantit la règle **Preview = Export**.

Il constitue la **source unique de vérité** utilisée aussi bien pour l'aperçu que pour l'export.

> Il garantit le principe **`Preview = Export`** dans le fonctionnement normal de l'application.

---

### 📄 FileStatisticsService

#### ⚙️ Rôle

- ✅ Calculer les statistiques.
- ✅ Rester indépendant de l'interface utilisateur.
- ✅ Centraliser la logique liée aux statistiques.

#### 💡 Responsabilités

Le `FileStatisticsService` calcule les statistiques à partir des données fournies par le pipeline.

Il centralise cette logique métier et met les résultats à disposition des autres composants du Core, sans dépendre de l'interface utilisateur.

---

### 🤝 Collaboration entre les services

#### 🤝 Fonctionnement

Chaque service réalise son traitement puis met son résultat à disposition de l'étape suivante du pipeline.

**Chaque service intervient uniquement dans son domaine de responsabilité.**

> Aucun service ne doit concentrer plusieurs responsabilités métier.

---

### 📋 Principes d'organisation

- ✅ Une responsabilité par service.
- ✅ Favoriser des services spécialisés.
- ✅ Séparer les traitements métier.
- ✅ Limiter le couplage.
- ✅ Faciliter les tests.
- ✅ Préserver un pipeline simple et prévisible.

---

### 📋 Règles des services

Les règles suivantes garantissent une architecture de services simple et maintenable.

- ✅ Un service = une responsabilité.
- ✅ Ne pas mélanger logique métier et interface utilisateur.
- ✅ Ne jamais dupliquer une logique métier existante.
- ✅ Réutiliser un service existant avant d'en créer un nouveau.
- ✅ Limiter les dépendances entre services.
- ✅ Préserver une faible dépendance entre les composants du Core.
- ✅ Conserver un pipeline clair et prévisible.
- ✅ Préserver le principe `Preview = Export`.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="statistiques"></a>

### **📊 08. Statistiques**

Cette section présente le système de statistiques utilisé par LatuCollect.

Elle décrit les informations calculées, leur mode de fonctionnement ainsi que les règles garantissant leur cohérence tout au long du pipeline.

---

### 📋 Vue d'ensemble

Les statistiques permettent de fournir une **vue synthétique** des données traitées par LatuCollect.

Elles sont calculées par le Core indépendamment de l'interface utilisateur et peuvent être réutilisées par les différents composants de l'application.

---

### 📊 Données calculées

Les statistiques sont calculées sur les fichiers actuellement sélectionnés.

- Nombre de fichiers
- Nombre de lignes
- Nombre de caractères
- Taille totale

---

### ⚙️ Fonctionnement

Les statistiques sont calculées au cours du pipeline à partir des données issues des fichiers sélectionnés afin de fournir des informations actualisées sur le contenu traité

- ✅ Déclenchement par le ViewModel.
- ✅ Calcul réalisé par le `FileStatisticsService`.
- ✅ Exécution en arrière-plan (`Task.Run`).
- ✅ Mise à jour en temps réel.

---

### ⚡ Optimisations

Les calculs sont optimisés afin de limiter leur impact sur les performances globales de l'application.

- ✅ Intégration au pipeline de traitement.
- ✅ Réutilisation des données déjà disponibles.
- ✅ Limitation des lectures redondantes.
- ✅ Réduction des recalculs inutiles.

---

### 🎯 Objectifs

Le système de statistiques a été conçu afin de fournir des informations fiables tout en conservant une application fluide.

- ✅ Fournir un retour utilisateur immédiat.
- ✅ Garantir des résultats cohérents.
- ✅ Limiter l'impact sur les performances.

---

### 💡 Bonnes pratiques

Les recommandations suivantes garantissent un système de statistiques cohérent et fiable.

- ✅ Centraliser les calculs dans `FileStatisticsService`.
- ✅ Éviter les calculs dupliqués.
- ✅ Conserver des statistiques indépendantes de l'interface utilisateur.
- ✅ Réutiliser les résultats lorsqu'ils sont déjà disponibles.
- ✅ Préserver la cohérence entre les statistiques, l'aperçu et l'export.
- ✅ Ne pas effectuer de calcul statistique directement dans l'interface utilisateur.
- ✅ Calculer les statistiques à partir des données du pipeline.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="performances"></a>

### **🚄 09. Performances**

Cette section présente les optimisations mises en œuvre dans LatuCollect afin de garantir une application fluide, réactive et capable de traiter efficacement des projets de grande taille.

---

### 📋 Vue d'ensemble

Les performances constituent un objectif majeur de l'architecture ALC.

Les optimisations mises en œuvre permettent de conserver une interface fluide, de limiter les traitements inutiles et d'améliorer les temps de réponse de l'application, même sur des projets volumineux.

---

### 📂 Lecture des fichiers

Les optimisations suivantes concernent la lecture des fichiers et la réduction des accès disque.

- Mise en cache des fichiers (`FileReaderService`)
- Réduction des accès disque (I/O)
- Réduction des recalculs inutiles
- Optimisation de la consommation mémoire
- Amélioration du temps de génération de l'aperçu

---

### 🌳 Optimisations TreeView

Ces optimisations permettent de limiter les rafraîchissements inutiles et de conserver un affichage fluide de l'arborescence.

- Réduction des rechargements complets de l'arborescence
- Mise à jour ciblée des nœuds
- Conservation de l’arbre réel (sans duplication)
- Conservation de l'état ouvert de l'arborescence
- Réduction des reconstructions complètes après modification des exclusions

---

### 👁️ Optimisations de l'aperçu

Les optimisations suivantes visent à réduire le coût de génération de l'aperçu tout en garantissant sa cohérence avec l'export.

- Réduction des recalculs de l'aperçu
- Protection contre les générations multiples de l'aperçu
- Optimisation du suivi des sélections
- Limitation automatique de l'aperçu pour les projets volumineux
- Debounce de l'aperçu asynchrone
- Invalidation des aperçus obsolètes

> L'aperçu reste synchronisé avec le contenu exporté dans le fonctionnement normal de l'application.

---

### 🖥️ Optimisations de l'interface

Ces optimisations permettent de maintenir une interface réactive pendant les traitements les plus coûteux.

- Chargement progressif de l'interface
- Préservation de la réactivité de l'interface pendant la construction du TreeView
- Préservation de la fluidité pendant les imports massifs

---

### 🎯 Objectifs

Les optimisations mises en œuvre visent à améliorer l'expérience utilisateur tout en conservant une architecture simple et maintenable.

- Application plus rapide
- UI plus fluide
- Pipeline plus performant
- TreeView beaucoup plus stable
- Réduction importante des effets de bord

---

### 💡 Bonnes pratiques

Les recommandations suivantes permettent de préserver les performances de l'application au fil des évolutions.

- ✅ Limiter les recalculs inutiles.
- ✅ Réutiliser les données déjà disponibles.
- ✅ Réduire les accès disque.
- ✅ Préserver la fluidité de l'interface utilisateur.
- ✅ Éviter les rafraîchissements complets lorsque cela n'est pas nécessaire.
- ✅ Conserver un pipeline simple et prévisible.
- ✅ Préserver la cohérence entre l'aperçu et l'export.
- ✅ Privilégier des optimisations simples, prévisibles et maintenables.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="interface-winui"></a>

### **🖥️ 10. Interface WinUI**

Cette section présente l'interface utilisateur de LatuCollect.

Elle décrit son organisation, les principales fonctionnalités mises à disposition de l'utilisateur ainsi que les règles garantissant une interface cohérente avec l'architecture ALC.

---

### 📋 Vue d'ensemble

L'interface utilisateur de LatuCollect est organisée en plusieurs zones ayant chacune une responsabilité précise.

Cette organisation garantit une navigation simple, une utilisation fluide et une cohérence avec l'architecture ALC.

---

### 🏗️ Structure de l'interface

L'interface principale est organisée en quatre zones ayant chacune une responsabilité précise.

- Gauche → Projet (arborescence)
- Centre → Options (format et actions)
- Droite → Aperçu
- Bas → Actions

> Cette organisation constitue la structure officielle de l'interface WinUI de LatuCollect.

---

### 🖱️ Fonctionnalités principales

L'interface met à disposition les principales fonctionnalités nécessaires au fonctionnement du pipeline utilisateur.

- ✅ Sélection via checkbox.
- ✅ Navigation dans l'arborescence.
- ✅ Recherche dynamique (filtrage géré dans le ViewModel).
- ✅ Aperçu en temps réel.
- ✅ Export final.

---

### 📐 Organisation des zones

#### 🌳 `Projet`

Affichage de l'arborescence des fichiers et gestion de leur sélection.

---

#### ⚙️ `Options`

Configuration des paramètres et des actions disponibles.

---

#### 👁️ `Aperçu`

Visualisation du contenu généré avant l'export.

---

#### ▶️ `Actions`

Accès aux principales commandes de l'application.

---

### ⚠️ Contraintes de l'interface

Cette structure fait partie intégrante de l'architecture de LatuCollect et contribue à garantir un comportement prévisible de l'application.

Toute évolution doit conserver cette organisation afin de préserver les habitudes utilisateur et la cohérence de l'application.

Toute évolution doit conserver cette structure afin de préserver les habitudes utilisateur et la cohérence de l'application.

> Toute évolution de la structure de l'interface doit être justifiée et documentée afin de préserver la cohérence de l'application.

---

### 🖥️ Stabilité de l'interface

L'interface est conçue afin de rester stable pendant les opérations de redimensionnement et les interactions utilisateur.

#### 📏 Taille minimale

- Définie à 1600 × 1000
- Empêche la dégradation de l'interface

---

#### 🔄 Redimensionnement

- Gestion native du redimensionnement (Win32)
- Réduction du flickering
- Pas de boucle de redimensionnement agressive

---

#### 💬 Dialogues

- Aucun dialogue imbriqué
- Ouverture contrôlée des dialogues
- Aucun blocage de l'interface

> Ces règles contribuent à préserver une interface utilisateur fluide et stable.

---

### 🎯 Objectifs

L'interface utilisateur a été conçue afin de :

- ✅ Faciliter la navigation.
- ✅ Rendre les actions rapidement accessibles.
- ✅ Préserver une interface claire.
- ✅ Offrir un aperçu immédiat avant export.
- ✅ Garantir un comportement prévisible.

> L'interface reste volontairement simple afin de faciliter son utilisation.

---

### 💡 Bonnes pratiques

- ✅ Respecter la structure officielle de l'interface.
- ✅ Limiter les modifications de disposition.
- ✅ Conserver une interface simple et lisible.
- ✅ Déléguer les traitements métier au Core.
- ✅ Préserver la cohérence entre l'interface et le pipeline utilisateur.
- ✅ Conserver une interface réactive pendant les traitements.
- ✅ Éviter d'introduire de la logique métier dans l'interface utilisateur.
- ✅ Conserver une séparation claire entre l'interface utilisateur et le Core.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="mode-developpeur"></a>

### **👨🏻‍💻 11. Mode développeur**

Cette section présente le mode développeur de LatuCollect.

Elle décrit son objectif, son fonctionnement ainsi que les règles garantissant son isolation par rapport au fonctionnement normal de l'application.

---

### 📋 Vue d'ensemble

Le mode développeur permet d'activer des fonctionnalités internes destinées au diagnostic, aux tests et au développement, sans modifier le fonctionnement normal de l'application.

---

### ⚙️ Fonctionnement

Le mode développeur est désactivé par défaut.

Son activation est réservée aux besoins de développement et de diagnostic.

- ✅ Désactivé par défaut.
- ✅ Activé depuis les paramètres.
- ✅ État sauvegardé dans les préférences utilisateur.
- ✅ Aucun impact sur l'utilisateur standard.

---

### 🔄 Comportement

Le mode développeur reste volontairement isolé du fonctionnement normal de l'application.

- ✅ Affichage d'un message dans l'interface (pas de fenêtre bloquante).
- ✅ Activation d'outils internes de diagnostic non visibles en mode standard.

---

### 🎯 Objectifs

- ✅ Analyse interne.
- ✅ Outils de développement.
- ✅ Gestion avancée des exclusions protégées.

---

### ⚠️ Contraintes

> Le mode développeur reste **strictement isolé** du comportement normal de l'application.

Son activation ne doit jamais modifier le pipeline utilisateur ni le fonctionnement des fonctionnalités destinées aux utilisateurs standards.

---

### 💡 Bonnes pratiques

- ✅ Réserver le mode développeur aux fonctionnalités de **diagnostic**.
- ✅ Conserver un comportement identique pour les **utilisateurs standards**.
- ✅ Éviter tout impact sur le fonctionnement normal de l'application.
- ✅ Isoler les fonctionnalités de développement du reste de l'interface.
- ✅ Désactiver le mode développeur pour une utilisation normale de l'application.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="export"></a>

### **📤 12. Export**

Cette section décrit le fonctionnement du système d'export de LatuCollect.

Elle présente les formats générés, les règles de construction des documents ainsi que les principes garantissant la cohérence entre l'aperçu et l'export.

---

### 📋 Vue d'ensemble

Le système d'export de LatuCollect génère un document regroupant le contenu des fichiers sélectionnés sans en modifier le contenu.

Cette section présente les formats d'export pris en charge, les règles de génération des documents, le principe **`Preview = Export`** ainsi que le comportement adopté pour les projets volumineux afin de préserver les performances de l'application.

Le document exporté est construit à partir des fichiers sélectionnés en lecture seule, sans transformation du contenu et selon un pipeline unique partagé avec l'aperçu.

---

### 📄 Format d'export

```text
Chemin du fichier

(contenu du fichier)

----------------------------------------
```

---

### 📋 Règles

- Chaque fichier est affiché avec son chemin complet.
- Le contenu est affiché tel quel (aucune modification).
- Un séparateur est ajouté entre chaque fichier.
- Les fichiers sont assemblés dans l'ordre déterminé par la sélection et l'arborescence.

---

### ⚠️ Restrictions

- ✅ Aucun traitement du contenu.
- ✅ Aucun parsing.
- ✅ Aucun formatage complexe.

> LatuCollect repose sur le principe du **copier intelligent uniquement**.

---

### 📝 Format Markdown

#### 📄 Structure

```text
Chemin du fichier

(contenu du fichier)

---
```

---

### 🎯 Objectifs

- Lisible.
- Structuré.
- Prévisible.

> Utilisable directement (copie / export)

---

### 🔁 Cohérence Preview = Export

Dans le fonctionnement standard :

- Le contenu exporté est identique au contenu affiché dans l'aperçu.
- Les deux utilisent la même génération réalisée par le Core.
- Seul un aperçu volontairement limité peut créer une différence d'affichage sur les très gros projets.

---

### 🔒 Source unique de vérité

Le contenu est généré une seule fois par le Core.

Cette génération unique est réutilisée à la fois pour l'aperçu et pour l'export.

> Aucun contenu spécifique n'est régénéré par l'interface utilisateur.

---

### 🎯 Objectifs

- ✅ Éviter les désynchronisations.
- ✅ Garantir `Preview = Export`.
- ✅ Réduire les effets de bord.

---

### ✂️ Cas particulier — Preview limité

### 📋 Comportement

Pour les très gros projets :

- Le Preview peut être volontairement tronqué.
- l’export complet reste conservé.
- les statistiques restent calculées sur l’ensemble réel des fichiers.

---

### 🛡️ Protection

Cette limitation protège :

- ✅ La mémoire.
- ✅ Les performances.
- ✅ La fluidité de l'interface utilisateur.

---

### ⚠️ Important

Dans certains projets très volumineux, une limitation volontaire de l'aperçu peut être appliquée afin de préserver les performances.

> Le principe **Preview = Export** reste valable.

> Seul l'affichage de l'aperçu peut être volontairement limité sur les projets très volumineux afin de préserver les performances.

Le contenu exporté reste complet.

---

### 🎯 Objectifs

- Éviter les blocages de l'interface.
- Conserver une application fluide.
- Limiter l'utilisation mémoire.

---

### 📊 État actuel

- ✅ Lecture via `FileReaderService` (Core).
- ✅ Assemblage via `FileExportService` (Core).
- ✅ Calcul des statistiques via `FileStatisticsService` (Core).
- ✅ Source unique de vérité pour le contenu.
- ✅ Aperçu = Export dans le fonctionnement standard.

---

### 💡 Bonnes pratiques

- ✅ Conserver un contenu identique entre l'aperçu et l'export dans le fonctionnement normal (`Preview = Export`).
- ✅ Préserver une source unique de vérité pour la génération du contenu.
- ✅ Ne jamais modifier le contenu des fichiers pendant l'export.
- ✅ Conserver le moteur d'export indépendant de l'interface utilisateur.
- ✅ Conserver la génération du contenu exclusivement dans le Core.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="structure-du-projet"></a>

### **📁 13. Structure du projet**

Cette section présente l'organisation générale des projets et des principaux dossiers qui composent LatuCollect.

Elle permet de visualiser la répartition des composants entre le Core, l'interface utilisateur et les tests afin de faciliter la compréhension de l'architecture globale.

---

### 📋 Vue d'ensemble

LatuCollect est organisé en plusieurs projets ayant chacun une responsabilité clairement définie.

Cette organisation favorise une séparation des responsabilités entre la logique métier, l'interface utilisateur et les tests, tout en facilitant la maintenance et l'évolution de l'application.

---

### 🗂️ Organisation des projets

La solution est organisée en trois projets principaux :

- `LatuCollect.Core` : contient la logique métier et les services du pipeline.
- `LatuCollect.UI.WinUI` : contient l'interface utilisateur, les ViewModels et les composants d'affichage.
- `LatuCollect.Tests` : regroupe les tests unitaires et les outils de validation.

> Chaque projet possède une responsabilité clairement définie afin de respecter les principes de l'architecture ALC.

---

### 📂 Arborescence

```text
LatuCollect.Core/
├── Configuration/
│   ├── Constants/
│   ├── Interfaces/
│   ├── Models/
│   └── Services/
│
├── Logging/
│   ├── Interfaces/
│   ├── Models/
│   └── Services/
│
├── Models/
│   └── Export/
│
└── Services/
    ├── Export/
    ├── Import/
    ├── Reader/
    └── Statistics/

LatuCollect.UI.WinUI/
├── Converters/
├── Models/
│   └── Logs/
├── Settings/
│   ├── Pages/
│   ├── Panels/
│   └── ViewModels/
└── ViewModels/
    ├── MainViewModel.cs
    ├── Export/
    ├── Logs/
    ├── Preview/
    ├── Settings/
    └── TreeView/

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

### 📄 Référence documentaire

Cette section présente une vue simplifiée de l'organisation générale du projet.

> Pour une description détaillée des dossiers et des fichiers du projet, consulter [📁 DIRECTORY_STRUCTURE](./DIRECTORY_STRUCTURE.md).

---

### 💡 Bonnes pratiques

- ✅ Conserver une responsabilité clairement définie pour chaque projet.
- ✅ Organiser les dossiers et les composants par domaine fonctionnel.
- ✅ Éviter de mélanger les responsabilités entre Core, UI et Tests.
- ✅ Maintenir une structure cohérente avec `DIRECTORY_STRUCTURE.md`.
- ✅ Éviter les dépendances croisées entre les projets.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="traitements-asynchrones"></a>

### **🔄 14. Traitements asynchrones**

Cette section présente les principes de programmation asynchrone utilisés dans LatuCollect.

Elle décrit les règles, les bonnes pratiques ainsi que les évolutions prévues afin de garantir une interface utilisateur fluide et un traitement fiable des opérations en arrière-plan.

---

### 📋 Vue d'ensemble

LatuCollect utilise la programmation asynchrone afin de maintenir une interface utilisateur réactive pendant l'exécution des traitements les plus coûteux.

Les opérations longues sont exécutées en arrière-plan afin de limiter les blocages de l'interface utilisateur.

---

### ⚙️ Fonctionnement

Les traitements asynchrones sont utilisés dans les principales opérations pouvant nécessiter un temps d'exécution important.

Certaines interactions de l'interface utilisent encore `async void`
lorsqu'elles correspondent à des gestionnaires d'événements WinUI.

Leur migration vers `async Task` est réalisée progressivement
lorsque cela est possible.

Le pipeline Preview asynchrone a été progressivement stabilisé entre les versions 0.14.0 et 0.17.0.

---

### 🧩 Principes

Les traitements asynchrones sont utilisés uniquement lorsque cela permet
de préserver la fluidité de l'interface utilisateur.

La logique métier reste centralisée dans le Core tandis que les ViewModels
orchestrent les opérations asynchrones sans contenir de logique métier.

---

### 📋 Règles

- ✅ Utilisation de `async / await`.
- ✅ Interface utilisateur jamais bloquée.
- 🟥 Éviter l'utilisation de `.Result`.
- 🟥 Éviter l'utilisation de `.Wait()`.

---

### ⚙️ Bonnes pratiques techniques

- Débounce côté UI
- Chargement progressif
- Débounce côté UI.
- Chargement progressif.
- Yield UI sur les traitements volumineux.
- Protection contre les doubles rafraîchissements.
- Annulation des traitements obsolètes.

---

### 🎯 Objectifs

- ✅ Préserver la fluidité de l'interface utilisateur.
- ✅ Limiter les race conditions.
- ✅ Réduire les blocages utilisateur.
- ✅ Garantir un comportement prévisible.

---

### 🔮 Évolutions prévues

Migration progressive vers :

`async Task`

afin de :

- Améliorer la stabilité
- Réduire les race conditions
- Améliorer la testabilité
- Faciliter la gestion des erreurs async

#### ⚠️ Migration progressive

Cette migration doit être réalisée progressivement
et uniquement après stabilisation de l’architecture.

---

### ⚠️ Impacts

Les évolutions asynchrones concernent principalement :

- ✅ Le TreeView
- ✅ Les rafraîchissements de l'aperçu
- ✅ Les interactions rapides de l'interface utilisateur

---

### 💡 Bonnes pratiques

- ✅ Utiliser `async` / `await` pour les opérations longues.
- ✅ Préserver la fluidité de l'interface utilisateur.
- ✅ Limiter les rafraîchissements inutiles.
- ✅ Effectuer la migration vers `async Task` de manière progressive.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="journalisation"></a>

### **🧾 15. Journalisation**

Cette section présente le système de journalisation utilisé par LatuCollect.

Elle décrit les objectifs de la journalisation ainsi que les principales règles encadrant son utilisation afin de faciliter le diagnostic et le suivi du fonctionnement de l'application.

---

### 📋 Vue d'ensemble

La journalisation permet de conserver une trace des événements importants survenant pendant l'exécution de l'application.

Elle constitue un outil d'aide au diagnostic et au suivi du fonctionnement interne de LatuCollect.

---

### 🎯 Objectifs

- ✅ Tracer les actions
- ✅ Tracer les erreurs

---

### ⚙️ Fonctionnement

Le système de journalisation est utilisé pour enregistrer les informations nécessaires au suivi de l'exécution de l'application.

Il intervient uniquement pour faciliter le diagnostic et le développement.

---

### ⚠️ Restrictions

- 🟥 Pas d’écriture directe fichier

> La journalisation ne doit pas écrire directement dans un fichier.

---

### 💡 Bonnes pratiques

- ✅ Utiliser la journalisation pour le diagnostic.
- ✅ Limiter les informations enregistrées aux éléments utiles.
- ✅ Respecter les restrictions de l'architecture ALC.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="injection-de-dependances"></a>

### **🔌 16. Injection de dépendances**

Cette section présente l'organisation actuelle de l'injection de dépendances dans LatuCollect.

Elle décrit le mode d'instanciation actuellement utilisé, les évolutions prévues ainsi que les objectifs poursuivis afin de faire évoluer progressivement l'architecture tout en préservant sa stabilité.

---

### 📋 Vue d'ensemble

LatuCollect utilise actuellement une instanciation directe des principaux composants de l'application.

Cette section présente l'état actuel de cette organisation ainsi que les évolutions prévues vers une injection de dépendances basée sur des interfaces afin de réduire le couplage et d'améliorer la testabilité.

---

### 🧩 Principes

L'injection de dépendances permet de fournir les services nécessaires à un composant sans que celui-ci ait à créer directement ses dépendances.

Cette approche facilite les tests, réduit le couplage entre les composants et améliore l'évolutivité de l'application.

---

### 📊 État actuel

LatuCollect utilise actuellement une instanciation directe des principaux services et ViewModels.

Cette approche a permis de mettre en place progressivement l'architecture ALC tout en simplifiant les premières phases de développement.

- ✅ Instanciation directe des principaux composants.
- ✅ Utilisation progressive des interfaces.
- ✅ Migration vers l'injection de dépendances en cours.

---

### 🔮 Évolutions prévues

- ⬜ Généralisation de l'injection de dépendances via des interfaces.
- ⬜ Réduction progressive des instanciations directes.
- ⬜ Centralisation de la création des services.

---

### 🎯 Objectifs

- ✅ Réduire le couplage.
- ✅ Améliorer la testabilité.
- ✅ Faciliter les futurs refactors.
- ✅ Renforcer la modularité de l'architecture.

---

### 💡 Bonnes pratiques

- ✅ Faire évoluer progressivement l'architecture.
- ✅ Préserver la compatibilité pendant les migrations.
- ✅ Privilégier les interfaces lors des futures évolutions.
- ✅ Éviter les instanciations directes lors des nouvelles évolutions lorsque cela est pertinent.
- ✅ Conserver une responsabilité clairement définie pour chaque service.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="etat-actuel"></a>

### **📍 17. État actuel**

Cette section présente l'état actuel de l'architecture de LatuCollect.

Elle regroupe les principaux éléments déjà implémentés, les migrations en cours ainsi que les composants actuellement utilisés afin d'offrir une vision globale de l'avancement du projet.

---

### 📋 Vue d'ensemble

L'architecture de LatuCollect est actuellement pleinement fonctionnelle et poursuit une évolution progressive vers une organisation toujours plus modulaire.

Les principaux composants du Core et de l'interface utilisateur sont opérationnels tandis que plusieurs migrations architecturales sont en cours afin de réduire progressivement le couplage entre les composants.

---

### 🏗️ Architecture

- ✅ Core fonctionnel
- ✅ Export opérationnel
- ✅ UI WinUI fonctionnelle
- ✅ Réduction du couplage Core/UI
- ✅ Simplification architecture globale
- ✅ Première phase du split `MainViewModel` terminée

---

- ✅ Extraction avancée `PreviewViewModel`
- ✅ Migration `TreeViewViewModel`
- ✅ Préparation et migration partielle `SettingsViewModel`
- ✅ Réduction progressive des responsabilités du `MainViewModel`

---

### 🧩 Architecture des ViewModels

Extraction réalisée en v0.15.0 :

- filtrage logs
- export logs
- formatage logs
- compteurs erreurs

Compatibilité UI conservée
via les redirections du `MainViewModel`.

Le `MainViewModel` conserve principalement :

- ✅ La coordination générale de l'interface
- ✅ Certaines redirections temporaires entre ViewModels
- ✅ Les interactions nécessaires à la compatibilité des bindings

---

### ✅ Réalisé

- `LogsViewModel`
- `TreeViewViewModel`
- `SettingsViewModel` (préparation et migration partielle)

---

### 🟢 Très avancé

- `PreviewViewModel`

Éléments déjà migrés :

- états Preview
- états techniques Preview
- génération Preview
- gestion du contenu Preview
- gestion des statistiques Preview

Migration restante :

- `RefreshPreviewAsync`
- `RequestPreviewRefresh`
- `DebouncePreviewAsync`

---

##### ⚠️ Migration progressive

Cette séparation restera progressive afin de :

- préserver la stabilité UI
- éviter les cassures bindings
- limiter les régressions
- conserver un pipeline prévisible

---

### 🌳 Architecture de sélection

- ✅ Audit complet `IsSelected` réalisé
- ✅ Maintien temporaire validé
- ✅ Dépendance `ConvertToCoreNodes()` documentée
- ✅ Réévaluation reportée en v0.17.0

#### 📋 Constat

- `IsSelected` reste utilisé par le pipeline actuel.
- `ConvertToCoreNodes()` dépend encore de `IsSelected`.
- La suppression immédiate provoquerait des régressions.

#### 🎯 Décision

> Le maintien temporaire de `IsSelected` dans `Core.Models.FileNode` a été validé en v0.15.0 afin de préserver la stabilité du pipeline.

La suppression éventuelle sera réévaluée lors de la finalisation
de l’architecture de sélection prévue en v0.17.0.

---

### 🔍 Recherche & TreeView

- ✅ Recherche performante et filtrage dynamique
- ✅ Sélection TreeView simplifiée et stabilisée
- ✅ Synchronisation parent ↔ enfants
- ✅ Filtrage basé visibilité (`IsVisible`)
- ✅ Conservation de l’arbre réel (sans duplication)
- ✅ Exclusions dynamiques stabilisées
- ✅ Réduction des reload complets TreeView
- ✅ Mise à jour ciblée des nodes
- ✅ Persistance complète expansion TreeView

---

### 👁️ Preview & Export

- ✅ Optimisation des performances (aperçu limité)
- ✅ Preview synchronisé avec la sélection
- ✅ Validation `Preview = Export`
- ✅ Pipeline preview async stabilisé
- ✅ Validation previews obsolètes
- ✅ Chargement progressif UI

---

### 🚄 Performances & stabilité

- ✅ Optimisation globale du pipeline
- ✅ Mise en cache des fichiers
- ✅ Séparation des statistiques
- ✅ Interface utilisateur plus stable grâce à une meilleure gestion des états.
- ✅ Protection anti multi-refresh
- ✅ Protection anti double génération preview
- ✅ Réduction des recalculs inutiles
- ✅ Reset runtime configuration sécurisé
- ✅ Réduction importante des race conditions UI

---

### ⚙️ Configuration

- ✅ Configuration globale centralisée
- ✅ Séparation AppConfig / UserConfig
- ✅ Exclusions groupées stabilisées

---

### 🧪 Tests

- ✅ Core largement couvert par les tests
- ✅ Validation PreviewViewModel
- ✅ Validation TreeViewViewModel
- ✅ Stabilisation pipeline preview async
- ✅ 116 tests verts

---

### 🕘 Historique majeur

- ✅ Suppression complète du système de simulation (v0.13.0)

---

### 💡 Bonnes pratiques

- ✅ Limiter chaque classe à une responsabilité clairement définie.
- ✅ Réaliser les migrations de manière progressive.
- ✅ Limiter les régressions lors des évolutions de l'architecture.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="evolutions"></a>

### **🚀 18. Évolutions**

Cette section présente les principales évolutions de l'architecture de LatuCollect.

Elle retrace les évolutions majeures déjà réalisées ainsi que les orientations retenues pour les prochaines versions, afin de faire évoluer progressivement l'application tout en préservant la stabilité et la cohérence de l'architecture ALC.

---

### 📋 Vue d'ensemble

Depuis les premières versions, l'architecture de LatuCollect a progressivement évolué vers une organisation plus modulaire, plus maintenable et plus robuste.

Les principales évolutions ont notamment permis de :

- Renforcer la séparation des responsabilités.
- Réduire progressivement les responsabilités du `MainViewModel`.
- Introduire des ViewModels spécialisés.
- Stabiliser les traitements asynchrones.
- Préserver le principe **`Preview = Export`**.
- Renforcer la cohérence de l'architecture ALC.

Toutes les évolutions sont réalisées progressivement afin de limiter les régressions et de préserver la stabilité de l'application.

---

### 🏗️ Évolutions de l'architecture

Les différentes versions ont permis de faire évoluer progressivement l'architecture sans remettre en cause son fonctionnement général.

#### ✅ Évolutions réalisées

- Introduction des interfaces de services.
- Renforcement de la séparation UI / Core.
- Simplification du pipeline métier.
- Réduction progressive du couplage.
- Stabilisation de l'architecture ALC.
- Fiabilisation des traitements asynchrones.
- Allègement progressif du `MainViewModel`.

👉 Voir [ROADMAP](./ROADMAP.md) pour l'historique complet.

---

### 🧩 Évolution des ViewModels

Le découpage du `MainViewModel` constitue l'une des évolutions majeures de l'architecture.

Cette migration progressive a permis de répartir les responsabilités entre plusieurs ViewModels spécialisés tout en conservant la compatibilité de l'interface utilisateur.

#### 🖥️ ViewModels spécialisés

##### ✅ Finalisés

- `LogsViewModel`
- `TreeViewViewModel`
- `PreviewViewModel`
- `SettingsViewModel`
- `ExportViewModel`

Le `MainViewModel` conserve désormais principalement un rôle d'orchestration entre les différents composants de l'application.

---

### 🔮 Évolutions futures

Les prochaines évolutions porteront principalement sur :

- L'amélioration continue de l'architecture.
- Les optimisations du Core.
- Les améliorations de l'interface utilisateur.
- La simplification du code lorsque cela est pertinent.
- Les optimisations des performances.
- La préparation des futures évolutions de LatuCollect.

👉 Voir [ROADMAP](./ROADMAP.md) pour le détail.

---

### 🎯 Objectifs

Les évolutions de l'architecture poursuivent plusieurs objectifs :

- ✅ Réduire progressivement le couplage entre les composants.
- ✅ Renforcer la modularité de l'application.
- ✅ Améliorer la maintenabilité du projet.
- ✅ Préserver la stabilité pendant les évolutions.
- ✅ Garantir la cohérence entre l'aperçu et l'export.
- ✅ Conserver une architecture simple, lisible et évolutive.

---

### 💡 Bonnes pratiques

Chaque évolution respecte les principes suivants :

- ✅ Évoluer progressivement.
- ✅ Préserver la compatibilité existante.
- ✅ Limiter les régressions.
- ✅ Respecter la feuille de route officielle.
- ✅ Vérifier les impacts architecturaux avant chaque évolution importante.
- ✅ Conserver la cohérence de l'architecture ALC.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="validation-tests"></a>

### **🧪 19. Validation & tests**

Cette section présente la stratégie de validation utilisée dans LatuCollect.

Elle décrit l'organisation des tests, les principaux domaines couverts ainsi que les objectifs poursuivis afin de garantir la stabilité, la fiabilité et la cohérence de l'application au fil de son évolution.

---

### 📋 Vue d'ensemble

Les tests permettent de vérifier le bon fonctionnement des principaux composants de LatuCollect ainsi que la cohérence entre le Core, les ViewModels et l'interface utilisateur.

Ils contribuent à détecter rapidement les régressions, à sécuriser les évolutions de l'architecture et à valider les traitements asynchrones introduits au fil des versions.

Ils accompagnent également les migrations progressives de l'architecture ALC afin de préserver la stabilité globale du projet.

---

### 🧪 Organisation des tests

Les tests sont organisés par domaine fonctionnel afin de faciliter leur maintenance et leur évolution.

Ils couvrent :

- Les services du Core.
- Les ViewModels spécialisés.
- Les traitements asynchrones.
- Les interactions de l'interface utilisateur.
- Les principaux scénarios d'utilisation.

Cette organisation permet d'isoler plus facilement les régressions tout en conservant une bonne lisibilité de la suite de tests.

---

### 📋 Domaines couverts

#### 🧠 Core

- ✅ Import
- ✅ Lecture des fichiers
- ✅ Export
- ✅ Statistiques
- ✅ Configuration
- ✅ Logging

---

#### 🖥️ Interface utilisateur

- ✅ `MainViewModel`
- ✅ `TreeViewViewModel`
- ✅ `PreviewViewModel`
- ✅ `ExportViewModel`
- ✅ `LogsViewModel`

---

#### 🌳 TreeView

- ✅ Navigation
- ✅ Recherche
- ✅ Sélection
- ✅ Sélection massive
- ✅ Gestion des exclusions
- ✅ Persistance de l'expansion

---

#### 👁️ Preview & Export

- ✅ Génération de l'aperçu
- ✅ Traitements asynchrones
- ✅ Validation de la règle **Preview = Export**
- ✅ Gestion des aperçus partiels

---

#### ⚙️ Configuration

- ✅ Sauvegarde
- ✅ Chargement
- ✅ Réinitialisation
- ✅ Validation des paramètres utilisateur

---

#### 🧾 Logging

- ✅ Filtrage
- ✅ États des journaux
- ✅ Export des journaux
- ✅ Gestion mémoire

---

### 🎯 Objectifs

Les tests permettent notamment de :

- ✅ Garantir la stabilité de l'application.
- ✅ Préserver la cohérence entre le Core, les ViewModels et l'interface utilisateur.
- ✅ Valider les traitements asynchrones.
- ✅ Vérifier la règle **Preview = Export**.
- ✅ Détecter rapidement les régressions.
- ✅ Accompagner les évolutions de l'architecture ALC.

---

### 💡 Bonnes pratiques

Chaque évolution importante du projet s'accompagne d'une validation adaptée.

Les principales règles sont les suivantes :

- ✅ Ajouter des tests lors de l'introduction d'un nouveau comportement.
- ✅ Mettre à jour les tests lors des évolutions de l'architecture.
- ✅ Vérifier les régressions avant chaque version.
- ✅ Préserver la cohérence entre Preview et Export.
- ✅ Maintenir une couverture des principaux composants du Core, des ViewModels et de l'interface utilisateur.
- ✅ Conserver les tests en cohérence avec l'évolution de l'architecture ALC.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="objectif-global"></a>

### **🎯 20. Objectif global**

Cette dernière section synthétise les principes qui guident l'architecture ALC.

Elle rappelle les objectifs poursuivis par LatuCollect afin de garantir une application simple, cohérente, maintenable et capable d'évoluer progressivement sans remettre en cause les fondations de l'architecture.

---

### 📋 Vue d'ensemble

L'architecture ALC a été conçue afin de proposer une base de code simple à comprendre, facile à maintenir et capable d'évoluer progressivement.

Elle repose sur une séparation claire des responsabilités entre le Core, les ViewModels et l'interface utilisateur, tout en garantissant un pipeline unique et cohérent.

Chaque évolution du projet doit contribuer à préserver ces principes fondamentaux.

---

### 🎯 Objectifs

L'architecture ALC poursuit les objectifs suivants :

- ✅ Proposer une architecture simple et lisible.
- ✅ Préserver une séparation claire des responsabilités.
- ✅ Garantir un pipeline unique et cohérent.
- ✅ Faciliter la maintenance et les évolutions futures.
- ✅ Limiter le couplage entre les composants.
- ✅ Préserver la stabilité de l'application.
- ✅ Garantir le principe **`Preview = Export`**.
- ✅ Conserver une architecture modulaire et évolutive.

---

### 🧩 Philosophie

LatuCollect reste volontairement centré sur son objectif principal :

- Collecter les fichiers.
- Assembler leur contenu.
- Générer un aperçu fidèle.
- Produire un export identique.

L'application ne cherche pas à analyser, interpréter ou transformer le contenu des fichiers.

Elle applique le principe du **copier intelligent uniquement**, conformément aux principes de l'architecture ALC.

---

### 💡 Principes de développement

Les évolutions du projet doivent toujours respecter les principes suivants :

- ✅ Faire évoluer l'application de manière progressive.
- ✅ Préserver la stabilité de l'architecture.
- ✅ Respecter la séparation des responsabilités.
- ✅ Maintenir une cohérence entre le Core, les ViewModels et l'interface utilisateur.
- ✅ Limiter les régressions lors des évolutions.
- ✅ Conserver une architecture simple, prévisible et facilement maintenable.
