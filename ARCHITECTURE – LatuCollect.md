# 🏗️ ARCHITECTURE – LatuCollect (ALC)

Projet : Application de collecte de contenu multi-fichiers

Ce document définit le standard officiel d’architecture (ALC) du projet.

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
- ✔ Contient les états UI (IsVisible, IsSelected)
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
- ✔ Aperçu = Export (strictement identique)

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

# 📊 7. STATISTIQUES (v0.7.0)

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

# ⚡ 8. PERFORMANCE (v0.7.0)

- ✔ Limitation de l’aperçu à 20 fichiers
- ✔ Calcul en arrière-plan (async)
- ✔ Réduction des recalculs inutiles

### ⚠️ Cas projet volumineux

- Affichage partiel activé
- Message utilisateur affiché

👉 Objectif :

- Éviter les freezes UI
- Garantir la fluidité

## ⚡ PERFORMANCE (v0.9.0)

- ✔ Mise en cache des fichiers (FileReaderService)
- ✔ Réduction des accès disque (I/O)
- ✔ Réduction des recalculs inutiles (cache ViewModel)
- ✔ Optimisation mémoire (moins d’allocations)
- ✔ Amélioration du temps de génération du preview

👉 Résultat :

- Application plus rapide
- UI plus fluide
- Pipeline plus efficace

---

# 9. UI WINUI (STRUCTURE OFFICIELLE)

```text
Gauche → Projet (arborescence)
Centre → Options (format + actions)
Droite → Aperçu
Bas → Export
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

Le mode développeur permet d’activer des fonctionnalités avancées de test.

### ✔ Règles

- Désactivé par défaut
- Activé uniquement via l’interface (Paramètres)
- Aucun impact sur l’utilisateur standard

### ✔ Comportement

- Affichage d’un message dans l’UI (pas de popup bloquant)
- Activation de fonctionnalités avancées non visibles en mode standard

### ✔ Fonctionnalités activées

- Mode simulation
- Accès aux scénarios de test

👉 Le mode développeur reste strictement isolé du comportement normal.

---

# 12. SIMULATION

Le système de simulation permet de reproduire des comportements spécifiques pour les tests.

### ✔ Accès

- Disponible uniquement en mode développeur

### ✔ Fonctionnement

- Activation via l’UI
- Choix d’un scénario
- Effet immédiat sur le comportement de l’application

### ✔ Objectif

- Tester les erreurs
- Simuler des cas extrêmes
- Valider la robustesse de l’application

### ⚠️ Règles

- Aucun impact sur les fichiers réels
- Doit rester isolé du Core réel
- Activation explicite uniquement

---

# 13. FORMAT D’EXPORT

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

---

# 14. STRUCTURE PROJET

```text
Core/
├── Services/
├── Configuration/

UI/
└── WinUI/
    ├── ViewModels/
    ├── Models/
    ├── Converters/
```

👉 Voir DIRECTORY_STRUCTURE.md pour le détail complet

---

# 15. RÈGLES STRICTES

- ✔ 1 classe = 1 responsabilité
- ✔ Pas de code mort
- ✔ Pas de logique UI dans Core
- ✔ Pas de logique métier complexe dans UI
- ✔ Pas de valeurs en dur UI

---

# 16. EMOJIS

- ❌ Interdits dans le code en version finale
- ✔ Autorisés dans la documentation

---

# 17. COMMENTAIRES

Classe :

- ✔ Rôle
- ✔ Responsabilités

Méthode :

- ✔ Objectif
- ✔ Paramètres
- ✔ Retour

---

# 18. ASYNCHRONE

- ✔ async / await
- ❌ .Result / .Wait()
- ✔ UI jamais bloquée

👉 Les opérations coûteuses doivent être contrôlées (ex : debounce côté UI)

---

# 19. STABILITÉ UI

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

# 20. JOURNALISATION

- ✔ Tracer actions
- ✔ Tracer erreurs

❌ Pas d’écriture directe fichier

---

# 21. NOMMAGE

- ✔ PascalCase
- ✔ Noms explicites
- ✔ Suffixe Service

---

# 22. INJECTION DE DÉPENDANCES

Actuel :

- ✔ Instanciation directe

Futur :

- ✔ Injection via interfaces

---

# 23. ÉTAT ACTUEL

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

---

# 24. ÉVOLUTIONS

- ✔ MVVM avancé
- ✔ Refactor Core
- ✔ Amélioration UI
- 🔄 Persistance configuration (JSON)
- 🔄 Filtrage avancé

---

# ⚠️ IMPORTANT — SIMPLICITÉ

LatuCollect est volontairement simplifié :

- ✔ Pas d’analyse de code
- ✔ Pas de transformation
- ✔ Pas de parsing complexe

👉 Copier intelligent
👉 Pas un analyseur

---

# 25. OBJECTIF GLOBAL

- ✔ Simple
- ✔ Structuré
- ✔ Compréhensible
- ✔ Évolutif
