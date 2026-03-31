# 🏗️ ARCHITECTURE – LATUCOLLECT (ALC)

Projet : **Application de collecte de contenu multi-fichiers**

Ce document définit le **standard officiel d’architecture (ALC)** du projet.

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

👉 Ce standard est **obligatoire pour chaque fichier créé**

---

# 1. EN-TÊTE OBLIGATOIRE

Tous les fichiers `.cs` et `.xaml` doivent contenir un en-tête standard.

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

- Imports
- Description
- Classe

Dans la classe :

- Champs privés
- Propriétés
- Constructeur
- Méthodes publiques
- Méthodes privées

---

# 3. SÉPARATION DES RESPONSABILITÉS

| Couche | Rôle                 |
| ------ | -------------------- |
| Core   | Logique métier       |
| UI     | Affichage uniquement |

❌ Interdit :

- logique métier dans UI
- accès fichiers depuis UI

---

# 4. PIPELINE LATUCOLLECT

## 🔹 Pipeline réel (Core)

```text
Import → Lecture → Assemblage → Export
```

👉 utilisé dans le code

---

## 🔹 Pipeline utilisateur (UI simplifiée)

```text
Importer → Sélectionner → Aperçu → Exporter
```

👉 basé sur la maquette réelle
👉 orienté simplicité utilisateur

---

# 5. CORRESPONDANCE SERVICES

| Étape                | Service               |
| -------------------- | --------------------- |
| Import               | FileImportService     |
| Lecture / Assemblage | FileCollectionService |
| Export               | FileExportService     |

---

# 6. SERVICES PRINCIPAUX

## 📥 FileImportService

- ajoute fichiers et dossiers
- filtre extensions
- évite doublons

---

## 📄 FileCollectionService

- lit contenu
- retourne chemin → texte

---

## 📤 FileExportService

- génère TXT / Markdown
- structure le document

---

# 7. UI WINUI (STRUCTURE OFFICIELLE)

```text
Gauche → Projet (arborescence)
Centre → Options (format)
Droite → Aperçu
Bas → Export
```

---

# 8. COMPORTEMENT UI

- sélection via checkbox
- navigation dans dossiers
- aperçu en temps réel
- export final

---

# 9. FORMAT D’EXPORT

```text
Chemin du fichier


(contenu)


----------------------------------------
```

---

# 10. STRUCTURE PROJET

```text
Core/
├── Services/

UI/
└── WinUI/
```

---

# 11. RÈGLES STRICTES

- ✔ 1 classe = 1 responsabilité
- ✔ pas de code mort
- ✔ pas de logique UI dans Core
- ✔ pas de logique métier dans UI
- ✔ pas de valeurs en dur UI

---

# 11.1 EMOJIS

- ❌ interdits dans code
- ✔ autorisés dans docs

---

# 12. COMMENTAIRES

Classe :

- rôle
- responsabilités

Méthode :

- objectif
- paramètres
- retour

---

# 13. ASYNCHRONE

- ✔ async/await
- ❌ .Result / .Wait()
- ✔ UI jamais bloquée

---

# 14. JOURNALISATION

- tracer actions
- erreurs

❌ pas d’écriture directe fichier

---

# 15. NOMMAGE

- PascalCase
- noms explicites
- suffixe Service

---

# 16. INJECTION DE DÉPENDANCES

Actuel :

- instanciation directe

Futur :

- injection via interfaces

---

# 17. ÉTAT ACTUEL

- ✔ Core fonctionnel
- ✔ export opérationnel
- 🔄 UI WinUI en cours

---

# 18. ÉVOLUTIONS

- MVVM
- amélioration UI
- services futurs

---

# 19. OBJECTIF GLOBAL

- ✔ simple
- ✔ structuré
- ✔ compréhensible
- ✔ évolutif
