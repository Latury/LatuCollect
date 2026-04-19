<div align="center">

# 🧭 LATUCOLLECT

### Collecte simple et rapide de contenu multi-fichiers

🔹 Outil simple pour extraire, assembler et exporter du contenu de fichiers
🔹 Pensé pour la lisibilité et l’usage avec des outils d’IA
🔹 Assemble rapidement le contenu de plusieurs fichiers en un seul document propre et lisible

<br>

![Version](https://img.shields.io/badge/Version-0.9.0-FFDF20?style=for-the-badge)
![Statut](https://img.shields.io/badge/Statut-Stable-008000?style=for-the-badge)
![Licence](https://img.shields.io/badge/Licence-MIT-FF0000?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-8-800080?style=for-the-badge)
![UI](https://img.shields.io/badge/UI-WinUI3-0078D6?style=for-the-badge)

<br>

Auteur : Flo Latury

</div>

---

# 🚀 1. Objectif

LatuCollect permet de :

- ✅ Charger un projet complet
- ✅ Naviguer dans sa structure (dossiers / fichiers)
- ✅ Rechercher rapidement des fichiers
- ✅ Sélectionner les fichiers à collecter
- ✅ Générer un aperçu du résultat final
- ✅ Copier le contenu généré
- ✅ Exporter un fichier structuré (.txt ou .md)

👉 Aucun fichier source n’est modifié
👉 Lecture seule, aucune transformation du code

---

# 🎯 2. Cas d’usage

LatuCollect est utile pour :

- ✅ Préparer du code pour une IA (ChatGPT, Copilot…)
- ✅ Générer une documentation rapide
- ✅ Partager du code de manière lisible
- ✅ Extraire des parties spécifiques d’un projet

👉 Idéal pour les développeurs débutants

---

# 🧠 3. Fonctionnement

```text id="flowreadme"
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🖥️ 4. Interface (WinUI)

## 🟦 Gauche — Projet (CŒUR DE L’APP)

- Arborescence complète du projet
- Navigation dans les dossiers
- Barre de recherche dynamique
- Sélection des fichiers via checkbox
- Bouton "Tout sélectionner" (désactivé temporairement)

---

## 🟨 Centre — Options

- Choix du format d’export :
  - TXT
  - Markdown

- Bouton Copier

- Accès :
  - Options
  - Statistiques
  - Aide
  - À propos
  - Quitter

---

- Bouton Statistiques

---

### 📊 Statistiques (v0.8.0)

- Nombre de fichiers sélectionnés
- Nombre total de lignes
- Nombre total de caractères
- Taille totale

👉 Mise à jour en temps réel
👉 Calcul en arrière-plan (aucun freeze)

---

## 🟩 Droite — Aperçu

- Affichage du document final
- Mise à jour en temps réel
- Affichage type éditeur (monospace)

### États UI :

- Message si aucun fichier sélectionné
- Contenu affiché si sélection active
- Loader pendant le chargement
- Message d’erreur si problème

---

## 🔻 Bas — Action

- Bouton Exporter
- Génération du fichier
- Confirmation utilisateur

---

# 📄 5. Format d’export

```text id="formatreadme"
C:\Projet\fichier.cs


(contenu du fichier)


----------------------------------------
```

👉 2 à 3 lignes vides entre chaque section

---

# ⚙️ 6. Fonctionnement interne

```text id="pipelinereadme"
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

👉 Aucun traitement complexe
👉 Lecture uniquement

---

# ⚡ 7. Performance & stabilité

## v0.5.0

- ✅ Chargement asynchrone (pas de blocage UI)
- ✅ Indicateur de chargement (loader)
- ✅ Protection contre les gros projets :
  - MAX_NODES
  - MAX_DEPTH

👉 Si le projet dépasse les limites définies :

⚠ Projet volumineux — affichage partiel

👉 Cela signifie que seuls une partie des fichiers est chargée
afin de garantir la fluidité de l’application

---

## v0.6.0

- ✅ Optimisation de la recherche (debounce)
- ✅ Réduction des recalculs inutiles
- ✅ Amélioration de la fluidité lors de la saisie

## v0.7.0

- ✅ Ajout des statistiques en temps réel
- ✅ Limitation de l’aperçu à 20 fichiers
- ✅ Calcul en arrière-plan (Task.Run)
- ✅ Réduction des freezes lors de grosses sélections

---

## v0.9.0

- ✅ Mise en cache des fichiers (lecture disque optimisée)
- ✅ Réduction des recalculs inutiles (cache ViewModel)
- ✅ Amélioration de la génération du preview
- ✅ Réduction des allocations mémoire
- ✅ Optimisation globale du pipeline

- ✅ Interface plus fluide :
  - Suppression des refresh multiples
  - Meilleure gestion des états (Loading / Ready)

👉 Résultat :

- Génération plus rapide
- Moins de charge CPU
- UI plus stable

---

# ⚠️ 8. Ce que l’application NE fait PAS

- ❌ Aucune analyse de code
- ❌ Aucune modification des fichiers
- ❌ Aucun parsing complexe

👉 LatuCollect est un outil de copie intelligente

---

# ⚙️ 9. Fonctionnalités principales

- ✅ Import de dossiers complets
- ✅ Arborescence automatique
- ✅ Recherche dynamique
- ✅ Sélection manuelle + globale
- ✅ Aperçu en temps réel
- ✅ Copie presse-papiers
- ✅ Export TXT / Markdown
- ✅ Feedback utilisateur
- ✅ États UI (Loading / Ready / Error)
- ✅ Filtrage par extension (.cs, .xaml, etc.)
- ✅ Gestion du cas "aucun résultat"
- ✅ Recherche optimisée (debounce)
- ✅ Exclusion de dossiers (bin, obj, .git)
- ✅ Statistiques en temps réel
- ✅ Limitation intelligente de l’aperçu

---

# 🏗️ 10. Architecture

```text id="archreadme"
Core = logique métier
UI = affichage uniquement
```

👉 Respect strict du modèle ALC

## 🔍 Architecture actuelle (v0.9.0)

Le projet est maintenant structuré en services distincts :

- Import → FileImportService
- Lecture → FileReaderService
- Collection → FileCollectionService
- Assemblage + Export → FileExportService

👉 Le ViewModel agit uniquement comme orchestrateur UI
👉 Le Core contient toute la logique métier

👉 Objectif : séparation claire UI / Core

### Améliorations 0.9.0

- Séparation du calcul des statistiques (`FileStatisticsService`)
- Allègement du `FileExportService`
- Optimisation des services Core
- Pipeline plus efficace (moins de doublons)

---

# 📦 11. Structure

```text id="structreadme"
Core/
UI/WinUI/
Resources/
```

👉 Voir : DIRECTORY_STRUCTURE.md pour le détail complet

---

# 📌 12. État actuel (v0.9.0)

- ✅ Version 0.9.0 stable
- ✅ Performance fortement améliorée
- ✅ Preview optimisé (cache + anti recalcul)
- ✅ Lecture fichiers optimisée (cache)
- ✅ UI plus fluide et stable
- ✅ Gestion des états améliorée
- ✅ Architecture renforcée (séparation des responsabilités)
- ✅ Pipeline optimisé (aucun doublon)

👉 Application prête pour la phase de finalisation (v0.10.0)

---

# ⭐ Points forts

- ✔ Très simple à utiliser
- ✔ Aperçu en temps réel
- ✔ Aucun risque (lecture seule)
- ✔ Export propre et structuré
- ✔ Performant même sur gros projets
- ✔ Architecture claire (ALC)

---

# 🧠 13. Philosophie

LatuCollect est conçu pour être :

- ✅ Simple
- ✅ Rapide
- ✅ Lisible

👉 Un outil utile avant tout

---

# 🧠 14. Pourquoi LatuCollect

Beaucoup d’outils analysent ou modifient le code.

LatuCollect fait un choix différent :

- ✔ Simplicité maximale
- ✔ Lecture uniquement
- ✔ Aucun traitement complexe

👉 Objectif :

Fournir un outil rapide et fiable pour préparer du contenu,
notamment pour une utilisation avec des IA

---

👉 LatuCollect n’est pas un analyseur

👉 C’est un outil de copie intelligente

---

# 🧪 15. Exemple concret

### Cas simple :

Tu veux envoyer ton projet à une IA.

Avec LatuCollect :

1. Tu importes ton dossier
2. Tu sélectionnes les fichiers utiles
3. Tu vois immédiatement le rendu
4. Tu exportes en .txt ou .md

👉 Résultat :

Un document propre, structuré et prêt à être utilisé

---

# ⚠️ 16. Limites actuelles

- Chargement partiel sur les très gros projets
- Pas de lazy loading
- Pas de virtualisation de l’arbre

👉 Ces limites sont volontaires pour garder :

- ✔ simplicité
- ✔ stabilité
- ✔ lisibilité du code

---

# 🔭 Évolutions

Les prochaines améliorations et orientations du projet sont définies dans la feuille de route.

👉 Voir : [ROADMAP](./ROADMAP.md)

---

# 📝 Versions

L’historique des évolutions et des modifications du projet est disponible dans les notes de version.

👉 Voir : [PATCH NOTES](./PATCH_NOTES.md)
