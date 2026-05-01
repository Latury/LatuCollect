<div align="center">

# 🧭 LATUCOLLECT

### Collecte simple et rapide de contenu multi-fichiers

🔹 Extraction, assemblage et export de fichiers
🔹 Pensé pour la lisibilité et l’usage avec des outils d’IA
🔹 Génération rapide d’un document propre et structuré

<br>

![Version](https://img.shields.io/badge/Version-0.10.0-FFDF20?style=for-the-badge)
![Statut](<https://img.shields.io/badge/Statut-Stable%20(0.10.0)-28A745?style=for-the-badge>)
![Licence](https://img.shields.io/badge/Licence-MIT-FF0000?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-8-800080?style=for-the-badge)
![UI](https://img.shields.io/badge/UI-WinUI3-0078D6?style=for-the-badge)

<br>

Auteur : Flo Latury

</div>

---

## 📌 Résumé

LatuCollect est un outil qui permet de regrouper rapidement le contenu de plusieurs fichiers en un seul document propre, sans modifier les fichiers d’origine.

---

## 📊 État des fonctionnalités

- ✅ Implémenté
- 🔮 À venir [ROADMAP](./ROADMAP.md)

---

# 🚀 1. Objectif

LatuCollect permet de :

- Charger un projet complet
- Naviguer dans sa structure
- Rechercher rapidement des fichiers
- Sélectionner les fichiers à collecter
- Générer un aperçu en temps réel
- Copier le contenu généré
- Exporter un document (.txt / .md)

👉 Aucun fichier source n’est modifié
👉 Lecture seule, aucune transformation

---

# 📦 2. Installation

## 🔹 Prérequis

- Windows 10 / 11
- .NET 8

## 🔹 Lancer l’application

1. Télécharger la dernière version
2. Lancer l’exécutable

👉 Aucune installation complexe requise

---

# ⚡ 3. Démarrage rapide

1. Ouvrir l’application
2. Charger un dossier
3. Sélectionner des fichiers
4. Visualiser l’aperçu
5. Exporter

👉 Temps total : quelques secondes

---

# 🎯 4. Cas d’usage

- Préparer du code pour une IA
- Générer une documentation rapide
- Partager du code proprement
- Extraire des parties d’un projet

👉 Compatible débutant et avancé

---

# 🧠 5. Fonctionnement

## 🧠 Fonctionnement utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

## ⚙️ Fonctionnement interne

```text
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

---

# 🖥️ 6. Interface

## 🟦 Gauche — Projet (cœur de l’application)

- Arborescence complète
- Navigation dossiers
- Recherche dynamique
- Sélection via checkbox

---

## 🟨 Centre — Options

- Choix du format (.txt / .md)
- Copier le contenu

Accès :

- Paramètres
- Statistiques
- Aide
- À propos

---

### 🧑‍💻 Mode développeur

- Activation via ⚙ Paramètres
- Indicateur visuel intégré

👉 Rôle :

- Debug
- Analyse
- Outils internes

👉 Aucun impact sur l’utilisateur standard

---

## 🟩 Droite — Aperçu

- Contenu final en temps réel
- Affichage type code (monospace)

### États UI :

- Aucun fichier sélectionné
- Chargement
- Erreur

---

## 🔻 Bas — Actions

- Export du fichier
- Logs application

---

# 📄 7. Format d’export

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

👉 Aperçu = Export

---

# ⚙️ 8. Fonctionnement interne

👉 Pipeline simple
👉 Lecture uniquement

---

# ⚡ 9. Performance

### Optimisations

- Cache fichiers
- Réduction I/O
- Optimisation mémoire

### Protection gros projets

- MAX_NODES
- MAX_DEPTH
- Chargement partiel

👉 Message :

⚠ Projet volumineux — affichage partiel

---

# 🛠️ 10. Stabilité UI

- Taille minimale : 1600 x 1000
- Réduction flickering
- Gestion propre du resize
- Dialogs stables

---

# 🧾 11. Logs

- Logs intégrés
- Visualisation UI
- Export possible

---

# ⚠️ 12. Ce que l’application NE fait PAS

- Aucune analyse de code
- Aucune modification
- Aucun parsing complexe

👉 LatuCollect = copier intelligent

---

# ⚙️ 13. Fonctionnalités principales

### ✅ Actuelles

- Import de dossier
- Arborescence
- Recherche (debounce)
- Exclusion dossiers
- Aperçu temps réel
- Export TXT / Markdown
- Copie presse-papiers
- Statistiques
- Logs
- Mode développeur

---

### 🔮 À venir

- Optimisation gros projets
- Améliorations UX
- Améliorations performance

---

# 🏗️ 14. Architecture

```text
Core = logique métier
UI = affichage
```

### Services principaux

- FileImportService
- FileReaderService
- FileCollectionService
- FileExportService
- FileStatisticsService

👉 ViewModel = orchestrateur

---

# 📦 15. Structure

```text
Core/
UI/WinUI/
Resources/
```

👉 Voir : [DIRECTORY_STRUCTURE](./DIRECTORY_STRUCTURE.md)

---

# 📌 16. État actuel

- Application stable
- UI stabilisée
- Pipeline optimisé
- Logs intégrés
- Mode développeur actif

👉 Phase finale (0.10.0)

---

# ⭐ Points forts

- Simple
- Rapide
- Stable
- Lisible
- Sans risque

---

# 🧠 17. Philosophie

- Simplicité > complexité
- Lisibilité > optimisation
- Utilité > fonctionnalités

---

# ⚠️ 18. Limites (actuelles)

- Pas de virtualisation avancée
- Pas de lazy loading
- Chargement partiel

👉 Choix volontaire

---

# 🔮 Évolutions prévues

- Lazy loading
- Optimisation performance
- Amélioration UX

👉 Voir : [ROADMAP](./ROADMAP.md)

---

# 📝 Versions

👉 Voir : [PATCH NOTES](./PATCH_NOTES.md)
