# 🏗️ ARCHITECTURE – LATUCOLLECT (ALC)

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

---

# 🔹 ViewModel (UI)

Le ViewModel agit comme intermédiaire entre l’UI et le Core :

- ✔ Gère l’état de l’interface
- ✔ Orchestre les actions utilisateur
- ✔ Prépare les données pour l’affichage

👉 Il ne doit pas contenir de logique métier complexe

---

# 🔹 Models (UI)

Les Models représentent les données manipulées dans l’interface :

- ✔ Structure des fichiers (FileNode)
- ✔ Données simples sans logique complexe

👉 Ils ne contiennent pas de logique métier

---

# 4. PIPELINE LATUCOLLECT

## 🔹 Pipeline réel (Core)

```text
Import → Lecture → Assemblage → Export
```

👉 objectif final du projet

---

## 🔹 Pipeline utilisateur (UI simplifiée)

```text
Importer → Sélectionner → Aperçu → Exporter
```

👉 orienté simplicité utilisateur

---

# ⚠️ 4.1 RÉALITÉ ACTUELLE (MVP)

Actuellement :

- ✔ Lecture via FileReaderService
- ✔ Assemblage dans le ViewModel
- ✔ Aperçu géré par le ViewModel
- ✔ Export via FileExportService

👉 Le Core ne gère pas encore tout le pipeline
👉 Refactorisation prévue

---

# 5. CORRESPONDANCE SERVICES

| Étape   | Service           |
| ------- | ----------------- |
| Lecture | FileReaderService |
| Export  | FileExportService |

---

# 6. SERVICES PRINCIPAUX

## 📄 FileReaderService

- ✔ Lit le contenu des fichiers
- ✔ Retourne le texte brut

---

## 📤 FileExportService

- ✔ Génère TXT / Markdown
- ✔ Structure le document final

---

# 7. UI WINUI (STRUCTURE OFFICIELLE)

```text
Gauche → Projet (arborescence)
Centre → Options (format + actions)
Droite → Aperçu
Bas → Export
```

---

# 8. COMPORTEMENT UI

- ✔ Sélection via checkbox
- ✔ Navigation dans dossiers
- ✔ Recherche dynamique
- ✔ Aperçu en temps réel
- ✔ Export final

---

# 9. FORMAT D’EXPORT

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

👉 2 à 3 lignes vides entre chaque section

---

# 10. STRUCTURE PROJET

```text
Core/
├── Services/

UI/
└── WinUI/
    ├── ViewModels/
    ├── Models/
    ├── Converters/
```

---

# 11. RÈGLES STRICTES

- ✔ 1 classe = 1 responsabilité
- ✔ Pas de code mort
- ✔ Pas de logique UI dans Core
- ✔ Pas de logique métier complexe dans UI
- ✔ Pas de valeurs en dur UI

---

# 11.1 EMOJIS

- ❌ Interdits dans code
- ✔ Autorisés dans docs

---

# 12. COMMENTAIRES

Classe :

- ✔ Rôle
- ✔ Responsabilités

Méthode :

- ✔ Objectif
- ✔ Paramètres
- ✔ Retour

---

# 13. ASYNCHRONE

- ✔ async / await
- ❌ .Result / .Wait()
- ✔ UI jamais bloquée

---

# 14. JOURNALISATION

- ✔ Tracer actions
- ✔ Tracer erreurs

❌ Pas d’écriture directe fichier

---

# 15. NOMMAGE

- ✔ PascalCase
- ✔ Noms explicites
- ✔ Suffixe Service

---

# 16. INJECTION DE DÉPENDANCES

Actuel :

- ✔ Instanciation directe

Futur :

- ✔ Injection via interfaces

---

# 17. ÉTAT ACTUEL

- ✔ Core fonctionnel
- ✔ Export opérationnel
- ✔ UI WinUI fonctionnelle (MVP)
- 🔄 Améliorations UX en cours

---

# 18. ÉVOLUTIONS

- ✔ MVVM avancé
- ✔ Refactor Core
- ✔ Amélioration UI

---

# ⚠️ IMPORTANT — SIMPLICITÉ

LatuCollect est volontairement simplifié :

- ✔ Pas d’analyse de code
- ✔ Pas de transformation
- ✔ Pas de parsing complexe

👉 Copier intelligent
👉 Pas un analyseur

---

# 19. OBJECTIF GLOBAL

- ✔ Simple
- ✔ Structuré
- ✔ Compréhensible
- ✔ Évolutif
