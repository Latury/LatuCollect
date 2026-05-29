# 🏗️ ARCHITECTURE – LatuCollect (ALC)

Projet : Application de collecte de contenu multi-fichiers

Ce document définit le standard officiel d’architecture (ALC) du projet.

## 📌 Résumé

Ce document décrit l’architecture du projet LatuCollect, les règles de développement (ALC), et l’organisation interne du code.

👉 Il sert de référence pour toute modification ou ajout de fonctionnalité.

---

# 🔎 Référence rapide : ALC

ALC = Architecture LatuCollect

Ce document sert à :

- ✔ Structurer le code
- ✔ Comprendre le fonctionnement interne
- ✔ Garantir la cohérence globale

---

# 🎯 Objectifs du standard

- ✔ Lisibilité du code
- ✔ Cohérence architecturale
- ✔ Maintenabilité long terme
- ✔ Discipline de développement
- ✔ Valeur pédagogique

👉 Ce standard est obligatoire pour chaque fichier créé

---

# ⚠️ RÈGLE CRITIQUE — AVANT MODIFICATION DE CODE

Avant toute modification :

- ✔ Analyser les fichiers existants
- ✔ Comprendre le fonctionnement actuel
- ✔ Vérifier la cohérence avec l’architecture ALC
- ✔ Ne jamais coder sans contexte

❌ Interdit :

- ❌ Ajouter du code sans analyser l’existant
- ❌ Modifier à l’aveugle
- ❌ Casser la structure du projet

---

# 1. EN-TÊTE OBLIGATOIRE

Tous les fichiers `.cs` et `.xaml` doivent contenir un en-tête standard.

---

## Modèle C#

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

## Modèle XAML

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

# 2. STRUCTURE INTERNE C#

Ordre recommandé :

- ✔ Imports (using en C#)
- ✔ Description
- ✔ Classe

Dans la classe :

- ✔ Champs privés
- ✔ Propriétés
- ✔ Constructeur
- ✔ Méthodes publiques
- ✔ Méthodes privées

---

# 3. SÉPARATION DES RESPONSABILITÉS

| Couche | Rôle           |
| ------ | -------------- |
| Core   | Logique métier |
| UI     | Affichage      |

❌ Interdit :

- ❌ Logique métier complexe dans UI
- ❌ Accès fichiers depuis UI

⚠️ Tolérance MVP :

- ✔ Logique légère autorisée dans le ViewModel
- ✔ La configuration globale appartient au Core (ex : AppConfig)

---

## 🔹 ViewModel (UI)

- ✔ Gère l’état de l’interface
- ✔ Orchestre les actions utilisateur
- ✔ Applique les filtres (recherche, visibilité)
- ✔ Déclenche les chargements (LoadTree)

---

# 🔮 ÉVOLUTION FUTURE — SPLIT MAINVIEWMODEL

## 🎯 Objectif

Réduire les responsabilités du `MainViewModel`
et améliorer la maintenabilité de l’UI.

---

## 📊 État actuel

Le `MainViewModel` centralise encore :

- sélection TreeView
- recherche
- preview
- export
- statistiques
- états UI
- commandes

---

### ✅ LogsViewModel

Extraction réalisée en v0.15.0 :

- filtrage logs
- export logs
- formatage logs
- compteurs erreurs

Compatibilité UI conservée
via redirections MainViewModel.

---

### 🟡 Découpage restant

- `TreeViewViewModel`
- `PreviewViewModel`

---

### ⬜ Découpage prévu ultérieurement

- `ExportViewModel`
- `SettingsViewModel`

---

## ⚠️ Important

Cette séparation restera progressive afin de :

- préserver la stabilité UI
- éviter les cassures bindings
- limiter les régressions
- conserver un pipeline prévisible

---

## 🔹 Rôle réel du ViewModel

Le ViewModel ne contient plus de logique métier complexe.

### ✔ Il fait uniquement :

- Gérer l’état UI
- Convertir les données UI → Core
- Appeler les services Core
- Gérer le rafraîchissement de l’aperçu

### ❌ Il ne fait pas :

- Lecture directe des fichiers
- Assemblage de contenu
- Calcul métier complexe

👉 Le ViewModel agit comme un orchestrateur UI.

---

## 🔹 Models (UI)

- ✔ Structure des données (FileNode)
- ✔ Sélection simplifiée (`bool`)
- ❌ Pas de logique métier

---

## 🔄 Conversion UI ↔ Core

Le projet utilise deux modèles distincts :

- `UI.Models.FileNode`
- `Core.Models.FileNode`

### ✔ Pourquoi ?

- Séparer les responsabilités
- Éviter les dépendances UI dans le Core

### ✔ Fonctionnement

- Conversion UI → Core avant appel des services
- Conversion Core → UI lors du chargement

👉 Cette conversion est gérée dans le ViewModel ou via des services dédiés.

---

## 🔹 Core

- ✔ Contient la logique métier
- ✔ Services métier
- ✔ Configuration globale
- ✔ Configuration utilisateur

---

# 4. ⚙️ CONFIGURATION

## 📄 AppConfig

Core/Configuration/AppConfig.cs

### ⚙️ Rôle

- ✔ Centraliser les paramètres globaux
- ✔ Gérer les exclusions système

### 📋 Exemple

ExcludedFolders = ["bin", "obj", ".git"]

---

## 📄 UserConfig

Core/Configuration/UserConfig.cs

### ⚙️ Rôle

- ✔ Stocker les préférences utilisateur
- ✔ Stocker les exclusions utilisateur
- ✔ Conserver les états UI persistants

### 📊 État

✔ Séparation AppConfig / UserConfig réalisée en v0.15.0

---

# 4.1 🔄 PIPELINE LATUCOLLECT

## ⚙️ Pipeline réel (Core)

```text
Import → Lecture → Assemblage → Statistiques → Export
```

---

## 🧩 Services du pipeline

- Import → `FileImportService`
- Lecture → `FileReaderService`
- Assemblage → `FileExportService`
- Statistiques → `FileStatisticsService`
- Export → `FileExportService`

---

## 📊 État actuel

- ✔ Lecture via `FileReaderService`
- ✔ Assemblage via `FileExportService`
- ✔ Calcul statistiques via `FileStatisticsService`
- ✔ Source unique de vérité pour le contenu
- ✔ Aperçu = Export dans le fonctionnement standard

---

## 🎯 Bénéfices

- ✔ Responsabilités mieux séparées
- ✔ Core plus maintenable
- ✔ Tests plus simples
- ✔ Architecture plus évolutive

---

## 🔮 Évolutions futures

- Extension du système de statistiques
- Amélioration du `FileStatisticsService`
- Réduction progressive des responsabilités du `FileExportService`
- Poursuite du découpage architectural du Core

👉 Voir [ROADMAP](./ROADMAP.md) pour les évolutions détaillées

---

## 🖥️ Pipeline utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

# 5. ⚙️ CORRESPONDANCE SERVICES

| Étape        | Service               |
| ------------ | --------------------- |
| Import       | FileImportService     |
| Lecture      | FileReaderService     |
| Statistiques | FileStatisticsService |
| Assemblage   | FileExportService     |
| Export       | FileExportService     |

---

# 6. 🛠️ SERVICES PRINCIPAUX

## 📄 FileReaderService

### ⚙️ Responsabilités

- ✔ Lit le contenu des fichiers
- ✔ Gère les encodages
- ✔ Détecte les fichiers binaires
- ✔ Détecte les fichiers verrouillés

---

## 📤 FileExportService

### ⚙️ Responsabilités

- ✔ Génère TXT / Markdown
- ✔ Structure le document final
- ✔ Méthode centrale : `BuildContent()`
- ✔ Garantit la cohérence aperçu/export

---

## 📊 FileStatisticsService

### ⚙️ Responsabilités

- ✔ Calcule les statistiques des fichiers
- ✔ Aucune dépendance UI
- ✔ Logique métier dédiée aux statistiques

---

### 📋 Calculs réalisés

- Nombre de fichiers
- Nombre de lignes
- Nombre de caractères
- Taille totale

---

### 🎯 Objectif

- Alléger `FileExportService`
- Séparer les responsabilités
- Préparer une architecture évolutive

---

### ⚡ Optimisations

- ✔ Réduction des lectures multiples
- ✔ Participation au calcul global du pipeline
- ✔ Amélioration des performances

---

# 7. 📊 STATISTIQUES

## 📋 Données calculées

- ✔ Nombre de fichiers
- ✔ Nombre de lignes
- ✔ Nombre de caractères
- ✔ Taille totale

---

## ⚙️ Fonctionnement

- Calcul déclenché par le ViewModel
- Exécution en arrière-plan (`Task.Run`)
- Mise à jour en temps réel

---

## 🎯 Objectif

- Fournir un retour utilisateur immédiat
- Limiter l’impact sur les performances

---

# 8. ⚡ PERFORMANCE

## 📊 État actuel

### 📂 Lecture & cache

- Mise en cache des fichiers (`FileReaderService`)
- Réduction des accès disque (I/O)
- Réduction des recalculs inutiles
- Optimisation mémoire
- Amélioration du temps de génération du preview

---

### 🌳 TreeView

- Réduction des reload complets TreeView
- Mise à jour ciblée des nodes
- Conservation de l’arbre réel (sans duplication)
- Conservation état ouvert TreeView
- Réduction rebuild complets exclusions

---

### 👁️ Preview

- Réduction des recalculs preview
- Protection anti multi-refresh
- Protection anti double génération preview
- Optimisation signature sélection
- Limitation automatique preview volumineux
- Debounce preview async
- Invalidation previews obsolètes

---

### 🖥️ Interface utilisateur

- Chargement progressif UI
- Yield UI pendant construction TreeView
- Préservation fluidité pendant imports massifs

---

## 🎯 Résultat

- Application plus rapide
- UI plus fluide
- Pipeline plus efficace
- TreeView beaucoup plus stable
- Réduction importante des effets de bord

---

# 9. 🖥️ UI WINUI (STRUCTURE OFFICIELLE)

Gauche → Projet (arborescence)
Centre → Options (format + actions)
Droite → Aperçu
Bas → Actions

---

## ⚠️ Règle

Cette structure est fixe et ne doit pas être modifiée.

---

# 10. 🖱️ COMPORTEMENT UI

## 📋 Fonctionnalités principales

- ✔ Sélection via checkbox
- ✔ Navigation dossiers
- ✔ Recherche dynamique (filtrage géré dans le ViewModel)
- ✔ Aperçu en temps réel
- ✔ Export final

---

# 11. 👨🏻‍💻 MODE DÉVELOPPEUR

## ⚙️ Rôle

Le mode développeur permet d’activer des fonctionnalités internes de diagnostic.

---

## 📋 Règles

- Désactivé par défaut
- Activé uniquement via l’interface (Paramètres)
- Aucun impact sur l’utilisateur standard

---

## 🔄 Comportement

- Affichage d’un message dans l’UI (pas de popup bloquant)
- Activation de fonctionnalités internes non visibles en mode standard

---

## 🎯 Objectif

- Analyse interne
- Outils de développement
- Gestion avancée des exclusions protégées

---

## ⚠️ Important

Le mode développeur reste strictement isolé du comportement normal.

---

# 12. 📤 FORMAT D’EXPORT

## 📄 Format standard

```text
Chemin du fichier

(contenu du fichier)

----------------------------------------
```

---

## 📋 Règles

- Chaque fichier est affiché avec son chemin complet
- Le contenu est affiché tel quel (aucune modification)
- Un séparateur est ajouté entre chaque fichier

---

## ⚠️ Restrictions

- ✔ Aucun traitement du contenu
- ✔ Aucun parsing
- ✔ Aucun formatage complexe

👉 LatuCollect = copier intelligent uniquement

---

## 🔁 Cohérence

Le format d’export doit être :

- Identique à l’aperçu
- Généré par le Core uniquement
- Indépendant de l’UI

---

## 📝 Format Markdown

## 📄 Chemin du fichier

```text
(contenu du fichier)

---
```

---

## 🎯 Objectif

- Lisible
- Structuré
- Prévisible

👉 Utilisable directement (copie / export)

---

## 🔒 Source unique de vérité

Le contenu généré pour l’aperçu est également utilisé pour l’export.

👉 Aucun contenu spécifique n’est régénéré côté UI.

---

### 🎯 Objectifs

- éviter les désynchronisations
- garantir Preview = Export
- réduire les effets de bord

---

## ✂️ Cas particulier — Preview limité

### 📋 Comportement

Pour les très gros projets :

- le preview peut être volontairement tronqué
- l’export complet reste conservé
- les statistiques restent calculées sur l’ensemble réel des fichiers

---

### 🛡️ Protection

Cette limitation protège :

- la mémoire
- les performances
- la stabilité UI

---

### ⚠️ Important

Dans ces cas extrêmes :

Preview ≠ Export

uniquement pour l’affichage.

Le contenu exporté reste complet.

---

### 🎯 Objectif

- éviter les freezes UI
- conserver une application fluide
- limiter l’utilisation mémoire

---

# 13. 📁 STRUCTURE PROJET

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
│
├── Settings/
│   ├── Pages/
│   ├── Panels/
│   └── ViewModels/
│
└── ViewModels/
    └── Logs/

LatuCollect.Tests/
├── Core/
├── UI/
│   ├── Logs/
│   ├── TreeView/
│   └── ViewModels/
│
└── Helpers/
```

👉 Voir : [DIRECTORY_STRUCTURE](./DIRECTORY_STRUCTURE.md)

---

# 14. 📏 RÈGLES STRICTES

## 📋 Principes

- ✔ 1 classe = 1 responsabilité
- ✔ Pas de code mort
- ✔ Pas de logique UI dans Core
- ✔ Pas de logique métier complexe dans UI
- ✔ Pas de valeurs en dur dans l’UI
- ✔ Réduire les dépendances inutiles entre Core et UI

---

# 15. 📝 DOCUMENTATION & EMOJIS

## 📋 Règles

- ❌ Interdits dans le code final
- ✔ Autorisés dans la documentation
- ✔ Utilisés pour améliorer la lisibilité

---

# 16. 💬 COMMENTAIRES

## 📄 Classes

- ✔ Rôle
- ✔ Responsabilités

---

## ⚙️ Méthodes

- ✔ Objectif
- ✔ Paramètres
- ✔ Retour

---

# 17. 🔄 ASYNCHRONE

## 📋 Règles

- ✔ Utilisation de `async / await`
- ❌ Utilisation de `.Result`
- ❌ Utilisation de `.Wait()`
- ✔ UI jamais bloquée

---

## ⚡ Bonnes pratiques

- Débounce côté UI
- Chargement progressif
- Yield UI sur traitements volumineux
- Protection contre les doubles rafraîchissements

---

## 🎯 Objectif

- Préserver la fluidité UI
- Limiter les race conditions
- Réduire les blocages utilisateur

---

## 🔮 ÉVOLUTION FUTURE — STABILISATION ASYNC UI

### 📊 État actuel

Certaines interactions UI utilisent encore :

async void

✔ Stabilisation majeure du pipeline preview async réalisée en v0.14.0

---

### 🎯 Objectif

Migration progressive vers :

async Task

afin de :

- améliorer la stabilité
- réduire les race conditions
- améliorer la testabilité
- faciliter la gestion des erreurs async

---

### ⚠️ Impact

Cette évolution impacte fortement :

- le TreeView
- les refresh preview
- les interactions rapides UI

---

### 📋 Règle

Cette migration doit être réalisée progressivement
et uniquement après stabilisation de l’architecture.

---

# 18. 🖥️ STABILITÉ UI

## 📏 Taille minimale

- Définie à 1600 x 1000
- Empêche la dégradation de l’interface

---

## 🔄 Redimensionnement

- Gestion native (Win32)
- Réduction du flickering
- Pas de boucle de resize agressive

---

## 💬 Dialogs

- Aucun dialog imbriqué
- Gestion contrôlée
- Aucun blocage UI

---

## 🎯 Objectif

Garantir une expérience fluide et stable

---

# 19. 🧾 JOURNALISATION

## 📋 Objectifs

- ✔ Tracer les actions
- ✔ Tracer les erreurs

---

## ⚠️ Restrictions

- ❌ Pas d’écriture directe fichier

---

# 20. 🏷️ NOMMAGE

## 📋 Règles

- ✔ PascalCase
- ✔ Noms explicites
- ✔ Suffixe Service

---

# 21. 🔌 INJECTION DE DÉPENDANCES

## 📊 État actuel

- ✔ Instanciation directe

---

## 🔮 Évolution future

- ✔ Injection via interfaces

---

## 🎯 Objectif

- Réduire le couplage
- Améliorer la testabilité
- Faciliter les futurs refactors

---

# 22. 📌 ÉTAT ACTUEL

## 🏗️ Architecture

- ✔ Core fonctionnel
- ✔ Export opérationnel
- ✔ UI WinUI fonctionnelle
- ✔ Réduction du couplage Core/UI
- ✔ Simplification architecture globale

---

## 🔍 Recherche & TreeView

- ✔ Recherche performante et filtrage dynamique
- ✔ Sélection TreeView simplifiée et stabilisée
- ✔ Synchronisation parent ↔ enfants
- ✔ Filtrage basé visibilité (`IsVisible`)
- ✔ Conservation de l’arbre réel (sans duplication)
- ✔ Exclusions dynamiques stabilisées
- ✔ Réduction des reload complets TreeView
- ✔ Mise à jour ciblée des nodes
- ✔ Persistance complète expansion TreeView

---

## 👁️ Preview & Export

- ✔ Optimisation des performances (aperçu limité)
- ✔ Preview synchronisé avec la sélection
- ✔ Validation Preview = Export
- ✔ Pipeline preview async stabilisé
- ✔ Validation previews obsolètes
- ✔ Chargement progressif UI

---

## ⚡ Performance & stabilité

- ✔ Optimisation globale du pipeline
- ✔ Mise en cache des fichiers
- ✔ Séparation des statistiques
- ✔ UI plus stable (gestion des états améliorée)
- ✔ Protection anti multi-refresh
- ✔ Protection anti double génération preview
- ✔ Réduction des recalculs inutiles
- ✔ Reset runtime configuration sécurisé
- ✔ Réduction importante des race conditions UI

---

## ⚙️ Configuration

- ✔ Configuration globale centralisée
- ✔ Séparation AppConfig / UserConfig
- ✔ Exclusions groupées stabilisées

---

## 🧪 Tests

- ✔ Core largement couvert par les tests
- ✔ Tests ViewModel stabilisés

---

## 🕘 Historique majeur

- ✔ Suppression complète du système de simulation (v0.13.0)

---

# 23. 🔮 ÉVOLUTIONS

## 📋 Prochaines étapes

- Injection de dépendances (interfaces)
- Refactor avancé du Core
- Amélioration UI
- Centralisation des interfaces
- Stabilisation architecture cible
- Simplification du pipeline global
- Split progressif du MainViewModel
- Stabilisation async UI avancée

👉 Voir [ROADMAP](./ROADMAP.md) pour le détail

---

## 🖥️ Split MainViewModel

### ✅ Réalisé

- LogsViewModel

### 🟡 Prévu

- TreeViewViewModel
- PreviewViewModel

### ⬜ Prévu ultérieurement

- ExportViewModel
- SettingsViewModel

---

👉 Voir ROADMAP.md pour le détail

---

# ⚠️ IMPORTANT — SIMPLICITÉ

## 🎯 Philosophie

LatuCollect est volontairement simplifié :

- ✔ Pas d’analyse de code
- ✔ Pas de transformation
- ✔ Pas de parsing complexe

👉 Copier intelligent

👉 Pas un analyseur

---

# 24. 🧪 VALIDATION & TESTS

## 📋 Tests actuels

- Tests Core
- Tests ViewModel
- Tests recherche TreeView
- Tests sélection TreeView
- Tests synchronisation sélection ↔ preview
- Tests sélection massive
- Tests exclusions
- Tests export
- Tests statistiques
- Tests preview async
- Tests persistance expansion
- Tests exclusions groupées
- Tests reset configuration runtime
- Tests logging
- Tests LogsViewModel

---

## 🎯 Objectif

Garantir :

- stabilité
- cohérence UI ↔ Core
- absence de régression

---

# 25. 🎯 OBJECTIF GLOBAL

- ✔ Simple
- ✔ Structuré
- ✔ Compréhensible
- ✔ Évolutif