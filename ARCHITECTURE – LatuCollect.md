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

## 🔮 Évolution future — Split MainViewModel

### 🎯 Objectif

Réduire les responsabilités du `MainViewModel`
et améliorer la maintenabilité de l’UI.

---

### ⚠️ Constat actuel

Le `MainViewModel` centralise actuellement :

- sélection TreeView
- recherche
- preview
- export
- logs
- statistiques
- états UI
- commandes

👉 Cette centralisation simplifie actuellement le développement
mais augmente progressivement :

- les risques d’effets de bord
- la complexité des refresh UI
- la difficulté des tests
- les risques liés aux interactions async

---

### 🔧 Évolution prévue

Le projet évoluera progressivement vers plusieurs ViewModels spécialisés :

- `TreeViewViewModel`
- `PreviewViewModel`
- `ExportViewModel`
- `SettingsViewModel`
- `LogsViewModel`

---

### ⚠️ Important

Cette séparation restera progressive afin de :

- préserver la stabilité UI
- éviter les cassures bindings
- limiter les régressions
- conserver un pipeline prévisible

---

## 🔹 Rôle réel du ViewModel (v0.8.0)

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
- ✔ Services (lecture / export)
- ✔ Configuration globale

---

# 3.1 CONFIGURATION GLOBALE

## 📄 AppConfig

```text
Core/Configuration/AppConfig.cs
```

### Rôle :

- ✔ Centraliser les paramètres globaux
- ✔ Gérer les exclusions de dossiers

### Exemple :

```csharp
ExcludedFolders = ["bin", "obj", ".git"]
```

### Utilisation :

- ✔ Utilisé dans CreateNode (ViewModel)
- ✔ Permet d’éviter la création de nodes inutiles

👉 Impact :

- ✔ Amélioration des performances
- ✔ Réduction du bruit dans l’arborescence
- ✔ Base pour Options dynamiques

---

# 4. PIPELINE LATUCOLLECT

## 🔹 Pipeline réel (Core)

```text
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

⚠️ Note :

Le pipeline est maintenant partiellement découplé :

- Assemblage → FileExportService
- Statistiques → FileStatisticsService

👉 Séparation introduite en v0.9.0
👉 Évolution progressive vers un pipeline totalement modulaire

---

## 🔹 Pipeline utilisateur (UI simplifiée)

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

# 4.1 RÉALITÉ ACTUELLE

- ✔ Lecture via FileReaderService
- ✔ Assemblage via FileExportService
- ✔ Source unique de vérité pour le contenu
- ✔ Aperçu = Export dans le fonctionnement standard

---

## 🔍 État réel (v0.9.0)

👉 Le pipeline est maintenant partiellement découpé en services distincts.

### ✔ Services actuellement en place

- Import → `FileImportService`
- Lecture → `FileReaderService`
- Collection → `FileCollectionService`
- Export + Assemblage → `FileExportService`

👉 Les responsabilités sont maintenant réparties :

- `FileExportService` → assemblage + génération du contenu
- `FileStatisticsService` → calcul des statistiques

👉 Cette séparation améliore :

- la lisibilité
- la maintenabilité
- la scalabilité du Core

---

### ⚠️ Ce qui sera refactorisé plus tard

- Amélioration du FileStatisticsService
- Extension du système de statistiques
- Réduction du rôle du `FileExportService`
- Découpage plus fin du pipeline

👉 Voir ROADMAP pour la suite

---

# 5. CORRESPONDANCE SERVICES

| Étape        | Service               |
| ------------ | --------------------- |
| Import       | FileImportService     |
| Lecture      | FileReaderService     |
| Collection   | FileCollectionService |
| Statistiques | FileStatisticsService |
| Assemblage   | FileExportService     |
| Export       | FileExportService     |

---

# 6. SERVICES PRINCIPAUX

## 📄 FileReaderService

- ✔ Lit le contenu des fichiers
- ✔ Retourne le texte brut

---

## 📤 FileExportService

- ✔ Génère TXT / Markdown
- ✔ Structure le document final
- ✔ Méthode centrale : BuildContent()
- ✔ Garantit la cohérence aperçu/export

## 📊 FileStatisticsService (v0.9.0)

- ✔ Calcule les statistiques des fichiers
- ✔ Séparation complète de la logique métier liée aux stats
- ✔ Aucune dépendance UI

👉 Responsabilités :

- Nombre de fichiers
- Nombre de lignes
- Nombre de caractères
- Taille totale

👉 Objectif :

- Alléger FileExportService
- Préparer une architecture scalable

### 🔄 Évolution (v0.9.0)

- ✔ Optimisation du traitement des fichiers
- ✔ Participation au calcul global (via le flux)
- ✔ Réduction des lectures multiples

👉 Objectif :

- Améliorer les performances
- Centraliser la logique

---

# 📊 7. STATISTIQUES — COMPORTEMENT

Les statistiques sont calculées à partir des fichiers sélectionnés :

- ✔ Nombre de fichiers
- ✔ Nombre de lignes
- ✔ Nombre de caractères
- ✔ Taille totale

### ⚙️ Fonctionnement

- Calcul déclenché par le ViewModel
- Exécuté en arrière-plan (`Task.Run`)
- Mise à jour en temps réel

👉 Objectif :

- Fournir un retour utilisateur immédiat
- Sans impacter les performances

---

# ⚡ 8. PERFORMANCE

## ✔ État actuel (v0.11.0)

- Mise en cache des fichiers (FileReaderService)
- Réduction des accès disque (I/O)
- Réduction des recalculs inutiles
- Optimisation mémoire
- Amélioration du temps de génération du preview

- Réduction des reload complets TreeView
- Mise à jour ciblée des nodes
- Réduction des recalculs preview
- Protection anti multi-refresh
- Protection anti double génération preview
- Optimisation signature sélection
- Limitation automatique preview volumineux
- Conservation de l’arbre réel (sans duplication)

👉 Résultat :

- Application plus rapide
- UI plus fluide
- Pipeline plus efficace
- TreeView beaucoup plus stable
- Réduction importante des effets de bord

---

# 9. UI WINUI (STRUCTURE OFFICIELLE)

```text id="fix-structure-ui"
Gauche → Projet (arborescence)
Centre → Options (format + actions)
Droite → Aperçu
Bas → Actions
```

---

# 10. COMPORTEMENT UI

- ✔ Sélection via checkbox
- ✔ Navigation dossiers
- ✔ Recherche dynamique (filtrage géré dans le ViewModel)
- ✔ Aperçu en temps réel
- ✔ Export final

---

# 11. MODE DÉVELOPPEUR

Le mode développeur permet d’activer des fonctionnalités internes de debug.

### ✔ Règles

- Désactivé par défaut
- Activé uniquement via l’interface (Paramètres)
- Aucun impact sur l’utilisateur standard

### ✔ Comportement

- Affichage d’un message dans l’UI (pas de popup bloquant)
- Activation de fonctionnalités internes non visibles en mode standard

### ✔ Objectif

- Analyse interne
- Outils de développement
- Gestion avancée des exclusions protégées

👉 Le mode développeur reste strictement isolé du comportement normal.

---

# 12. FORMAT D’EXPORT

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

---

## 🎯 Règle

- Chaque fichier est affiché avec son chemin complet
- Le contenu est affiché tel quel (aucune modification)
- Un séparateur est ajouté entre chaque fichier

---

## ⚠️ IMPORTANT

- ✔ Aucun traitement du contenu
- ✔ Aucun parsing
- ✔ Aucun formatage complexe

👉 LatuCollect = copier intelligent uniquement

---

## 🔁 Cohérence

👉 Le format d’export doit être :

- Identique à l’aperçu
- Généré par le Core uniquement
- Indépendant de l’UI

---

## 📄 Format Markdown

```text
## 📄 Chemin du fichier

(contenu du fichier)

---
```

---

## 📌 Objectif

- Lisible
- Structuré
- Prévisible

👉 Utilisable directement (copie / export)

---

## 🔒 Source unique de vérité

Le contenu généré pour l’aperçu est également utilisé pour l’export.

👉 Aucun contenu spécifique n’est régénéré côté UI.

Objectif :

- éviter les désynchronisations
- garantir Preview = Export
- réduire les effets de bord

---

## ⚠️ Cas particulier — Preview limité

Pour les très gros projets :

- le preview peut être volontairement tronqué
- l’export complet reste conservé
- les statistiques restent calculées sur l’ensemble réel des fichiers

👉 Cette limitation protège :

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

# 13. STRUCTURE PROJET

```text
Core/
├── Services/
├── Configuration/
├── Logging/
├── Models/

UI/
└── WinUI/
    ├── ViewModels/
    ├── Models/
    ├── Converters/
```

👉 Voir : [DIRECTORY_STRUCTURE](./DIRECTORY_STRUCTURE.md)

---

# 14. RÈGLES STRICTES

- ✔ 1 classe = 1 responsabilité
- ✔ Pas de code mort
- ✔ Pas de logique UI dans Core
- ✔ Pas de logique métier complexe dans UI
- ✔ Pas de valeurs en dur UI
- ✔ Réduire les dépendances inutiles entre Core et UI

---

# 15. EMOJIS

- ❌ Interdits dans le code en version finale
- ✔ Autorisés dans la documentation

---

# 16. COMMENTAIRES

Classe :

- ✔ Rôle
- ✔ Responsabilités

Méthode :

- ✔ Objectif
- ✔ Paramètres
- ✔ Retour

---

# 17. ASYNCHRONE

- ✔ async / await
- ❌ .Result / .Wait()
- ✔ UI jamais bloquée

👉 Les opérations coûteuses doivent être contrôlées (ex : debounce côté UI)

---

### 🔮 Évolution future — Stabilisation async UI

Certaines interactions UI utilisent encore actuellement :

async void

👉 Une migration progressive vers :

async Task

est prévue pour :

- améliorer la stabilité
- réduire les race conditions
- améliorer la testabilité
- faciliter la gestion des erreurs async

---

### ⚠️ Important

Cette évolution impacte fortement :

- le TreeView
- les refresh preview
- les interactions rapides UI

👉 Elle doit être réalisée progressivement
et uniquement après stabilisation architecture.

---

# 18. STABILITÉ UI

### ✔ Taille minimale

- Définie à 1600 x 1000
- Empêche la dégradation de l’interface

### ✔ Redimensionnement

- Gestion native (Win32)
- Réduction du flickering
- Pas de boucle de resize agressive

### ✔ Dialogs

- Aucun dialog imbriqué
- Gestion contrôlée
- Aucun blocage UI

👉 Objectif : garantir une expérience fluide et stable

---

# 19. JOURNALISATION

- ✔ Tracer actions
- ✔ Tracer erreurs

❌ Pas d’écriture directe fichier

---

# 20. NOMMAGE

- ✔ PascalCase
- ✔ Noms explicites
- ✔ Suffixe Service

---

# 21. INJECTION DE DÉPENDANCES

Actuel :

- ✔ Instanciation directe

Futur :

- ✔ Injection via interfaces

---

# 22. ÉTAT ACTUEL

- ✔ Core fonctionnel
- ✔ Export opérationnel
- ✔ UI WinUI fonctionnelle
- ✔ Recherche performante et filtrage dynamique
- ✔ Configuration globale centralisée
- ✔ Statistiques temps réel
- ✔ Optimisation des performances (aperçu limité)

- ✔ Optimisation globale du pipeline (v0.9.0)
- ✔ Mise en cache des fichiers
- ✔ Séparation des statistiques
- ✔ UI plus stable (gestion des états améliorée)

- ✔ Sélection TreeView simplifiée et stabilisée
- ✔ Synchronisation parent ↔ enfants
- ✔ Filtrage basé visibilité (`IsVisible`)
- ✔ Conservation de l’arbre réel (sans duplication)
- ✔ Exclusions dynamiques stabilisées
- ✔ Réduction des reload complets TreeView
- ✔ Mise à jour ciblée des nodes
- ✔ Protection anti multi-refresh
- ✔ Protection anti double génération preview
- ✔ Réduction des recalculs inutiles
- ✔ Preview synchronisé avec la sélection
- ✔ Validation Preview = Export
- ✔ Core largement couvert par les tests
- ✔ Tests ViewModel stabilisés

- ✔ Suppression complète du système de simulation (v0.13.0)
- ✔ Réduction du couplage Core/UI
- ✔ Simplification architecture globale

---

# 23. ÉVOLUTIONS

## 🔮 À venir

- Séparation AppConfig / UserConfig
- Injection de dépendances (interfaces)
- Refactor avancé du Core
- Amélioration UI
- Centralisation des interfaces
- Stabilisation architecture cible
- Simplification du pipeline global
- Split progressif du MainViewModel
- Stabilisation async UI

👉 Voir [ROADMAP](./ROADMAP.md) pour le détail

---

# ⚠️ IMPORTANT — SIMPLICITÉ

LatuCollect est volontairement simplifié :

- ✔ Pas d’analyse de code
- ✔ Pas de transformation
- ✔ Pas de parsing complexe

👉 Copier intelligent
👉 Pas un analyseur

---

# 24. VALIDATION & TESTS

## ✔ Tests actuels

- Tests Core
- Tests ViewModel
- Tests recherche TreeView
- Tests sélection TreeView
- Tests synchronisation sélection ↔ preview
- Tests sélection massive
- Tests exclusions
- Tests export
- Tests statistiques

## 🎯 Objectif

Garantir :

- stabilité
- cohérence UI ↔ Core
- absence de régression

---

# 25. OBJECTIF GLOBAL

- ✔ Simple
- ✔ Structuré
- ✔ Compréhensible
- ✔ Évolutif
