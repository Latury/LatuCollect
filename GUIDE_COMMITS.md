# 🧠 GUIDE DES COMMITS – LATUCOLLECT

Application de collecte, organisation et export de contenu multi-fichiers

Ce document définit la convention officielle de commits pour le projet LatuCollect.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🎯 OBJECTIFS

- ✔ Structurer l’historique Git
- ✔ Clarifier chaque intention de modification
- ✔ Maintenir une cohérence documentaire
- ✔ Permettre un suivi technique clair
- ✔ Faciliter la compréhension long terme

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🎯 OBJECTIF D’UN COMMIT

Un commit dans LatuCollect est :

- ✔ Une trace historique
- ✔ Une décision documentée
- ✔ Une explication technique
- ✔ Un message destiné au futur développeur
- ✔ Un élément de stabilité du projet

Chaque commit doit répondre à :

1. Qu’est-ce qui change ?
2. Pourquoi ça change ?
3. Comment ça change ?
4. Quel est l’impact ?
5. Y a-t-il des risques ?

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🧩 PRINCIPE FONDAMENTAL

1 commit = 1 intention claire

Interdit :

- ❌ Corriger un bug + ajouter une feature
- ❌ Mélanger refactor + nouvelle fonctionnalité
- ❌ Commit vague
- ❌ Commit sans description

---

# ⚠️ RÈGLES SPÉCIFIQUES LATUCOLLECT

- ❌ Ne jamais modifier les fichiers source importés
- ✔ LatuCollect travaille uniquement en lecture

---

# 🔎 RÉFÉRENCE ARCHITECTURALE

Lorsqu’un commit modifie :

- La structure du code
- Les services
- Le pipeline interne
- L’architecture MVVM

👉 Consulter :

- ARCHITECTURE.md

---

# 🧠 RÈGLE ALC (RAPPEL IMPORTANT)

```text
Core = logique métier
UI = affichage uniquement
```

❌ Interdit :

- Logique métier dans UI
- Accès fichiers depuis UI

---

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# ⚙️ PIPELINE LATUCOLLECT

## 🔹 Pipeline interne

```text
Import → Lecture → Assemblage → Export
```

---

## 🔹 Flux utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

👉 Toute modification doit préciser :

- Étape modifiée
- Impact sur le traitement
- Impact sur le résultat final

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 📐 FORMAT STANDARD OBLIGATOIRE

## 1️⃣ SUMMARY

Longueur recommandée : 50 caractères

Format :

[Emoji] [Catégorie] [Contexte] Message clair

Exemples :

✨ [Feature][UI] Ajout arborescence projet
⛓️‍💥 [Bug][Core] Correction export
♻️ [Refactor][Service] Simplification ExportService

Règles :

- ✔ Pas de point final
- ✔ Pas de phrase longue
- ✔ Pas de formulation vague

---

## 2️⃣ DESCRIPTION OBLIGATOIRE

### 📎 Description simple

Explication claire, courte et compréhensible par tous.

👉 Elle doit expliquer :

- ✔ Ce qui a été ajouté, modifié ou corrigé
- ✔ À quoi cela sert concrètement
- ✔ Ce que l’utilisateur va voir ou ressentir

👉 Important :

- ✔ Utiliser des mots simples
- ✔ Éviter le jargon technique
- ✔ Être compréhensible même pour un débutant
- ✔ Aller à l’essentiel (2 à 4 phrases maximum)

👉 Exemple :

Ajout d’un bouton permettant de charger un dossier.
L’utilisateur peut maintenant importer son projet facilement.

---

### 🧑🏻‍💻 Description technique

Explication détaillée des modifications techniques.

👉 Elle doit répondre à :

1. Qu’est-ce qui a été modifié dans le code ?
2. Où (fichiers / classes) ?
3. Comment cela fonctionne maintenant ?

👉 Contenu attendu :

- ✔ Classes et fichiers modifiés
- ✔ Méthodes ajoutées ou modifiées
- ✔ Logique impactée
- ✔ Changements d’architecture (si applicable)

👉 Règles :

- ✔ Être précis
- ✔ Utiliser des termes techniques compréhensibles
- ✔ Structurer avec des listes
- ✔ Ne pas expliquer le "pourquoi" (réservé à la description simple ou impact)

👉 Exemple :

- MainViewModel.cs :
  - Ajout du chargement asynchrone avec `Task.Run`
  - Ajout de la gestion des états UI (Loading / Ready / Error)
- FileExportService :
  - Centralisation de la génération du contenu

---

### 📁 Fichiers concernés

- ✔ Fichiers ajoutés
- ✔ Fichiers modifiés
- ✔ Fichiers supprimés

---

### ⚠️ Impact

- ✔ Fonctionnel
- ✔ Performance
- ✔ Risques éventuels
- ✔ Impact utilisateur (UX)

---

# 🏷️ CATÉGORIES OFFICIELLES DE COMMIT – LATUCOLLECT

| Emoji | Type         | Utilisation                                |
| ----- | ------------ | ------------------------------------------ |
| 🚧    | Init         | Initialisation projet                      |
| 🏗️    | Architecture | Structure globale / MVVM                   |
| 🧱    | Core         | Logique principale                         |
| ✨    | Feature      | Nouvelle fonctionnalité                    |
| ⛓️‍💥    | Bug          | Correction dysfonctionnement               |
| 🛠️    | Fix          | Correction mineure                         |
| ♻️    | Refactor     | Réorganisation sans changement fonctionnel |
| 📝    | Docs         | Documentation                              |
| 📚    | Readme       | Modification README                        |
| 🔒    | Security     | Sécurité                                   |
| 🚀    | Performance  | Optimisation                               |
| 🧪    | Tests        | Ajout ou modification tests                |
| 🧹    | Cleanup      | Nettoyage code                             |
| 🔥    | Remove       | Suppression code                           |
| 🔧    | Config       | Configuration                              |
| 🔄    | Upgrade      | Mise à jour dépendances                    |
| 🧠    | Logic        | Logique métier                             |
| 🛡️    | Validation   | Validation données                         |
| 🧵    | Async        | Passage en asynchrone                      |
| 🧭    | Navigation   | Navigation UI                              |
| 🎯    | UX           | Amélioration UX                            |
| 📁    | Structure    | Organisation dossiers                      |
| 🧬    | Experimental | Fonctionnalité expérimentale               |
| ⚙️    | Service      | Création ou modification service           |

---

# 🖥️ CONTEXTE (OBLIGATOIRE)

Toujours préciser :

- UI → interface utilisateur
- ViewModel → logique UI
- Core → logique métier

---

# 📘 EXEMPLE LATUCOLLECT

## Summary

✨ [Feature][UI] Ajout arborescence projet

---

## Description

📎 Description simple :

Ajout de l’affichage des dossiers et fichiers du projet.

---

👨🏻‍💻 Description technique :

- Création vue XAML
- Ajout navigation dossiers
- Ajout checkbox sélection

---

📁 Fichiers concernés :

- UI/Views/MainWindow.xaml
- UI/ViewModels/MainViewModel.cs

---

⚠️ Impact :

- ✔ Amélioration UX
- ✔ Navigation projet possible

---

⚠️ Aucun fichier source modifié

---

# 📚 COMMITS VS PATCH NOTES

- Commit = détail technique
- Patch note = résumé de version

👉 Les versions sont définies dans PATCH_NOTES.md

---

# 📏 RÈGLES AVANCÉES

- ✔ Commit clair et court
- ✔ Commit testable
- ✔ Commit compréhensible seul
- ✔ Vérifier cohérence avec la feuille de route
- ✔ Vérifier cohérence avec la documentation
- ✔ Commit lié à une version de la roadmap si applicable

- ❌ Pas de commit automatique non vérifié

---

# 🚫 EXEMPLES INTERDITS

- ❌ update
- ❌ fix bug
- ❌ wip
- ❌ divers
- ❌ modifications

---

# ⚠️ NE PAS COMMIT SI

- Code non compilable
- Test incomplet
- UI incohérente
- Debug actif
- Fichiers inutiles présents

---

# 🧠 PHILOSOPHIE

Git est une mémoire.

Une mémoire mal tenue devient inutilisable.

---

# 🏁 OBJECTIF FINAL

- ✔ Un historique Git lisible
- ✔ Une architecture compréhensible
- ✔ Un projet maintenable
- ✔ Un développement propre dans le temps

👉 Un bon commit aujourd’hui évite un problème demain
