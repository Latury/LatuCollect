<div align="center">

# 🧭 LATUCOLLECT

### Collecte simple et rapide de contenu multi-fichiers

🔹 Extraction, assemblage et export de fichiers
🔹 Pensé pour la lisibilité et l’usage avec des outils d’IA
🔹 Génération rapide d’un document propre et structuré

<br>

![Version](https://img.shields.io/badge/Version-0.10.0-FFDF20?style=for-the-badge)
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

# 🎯 2. Cas d’usage

- Préparer du code pour une IA
- Générer une documentation rapide
- Partager du code proprement
- Extraire des parties d’un projet

👉 Compatible débutant et avancé

---

# 🧠 3. Fonctionnement

```text id="pipelinereadme"
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🖥️ 4. Interface

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

### 🧑‍💻 Mode développeur

- Activation via paramètres
- Indicateur visuel actif
- Aucun impact utilisateur normal

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
- Simulation (mode développeur)

---

# 📄 5. Format d’export

```text id="flowreadme"
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

👉 Aperçu = Export (strictement identique)

---

# ⚙️ 6. Fonctionnement interne

```text id="pipelinereadme"
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

👉 Pipeline simple
👉 Lecture uniquement

---

# ⚡ 7. Performance

### Optimisations

- Cache des fichiers
- Réduction des accès disque
- Réduction des recalculs
- Optimisation mémoire

### Protection gros projets

- MAX_NODES
- MAX_DEPTH
- Chargement partiel

👉 Message :

⚠ Projet volumineux — affichage partiel

---

# 🧾 8. Logs

- Système de logs intégré
- Visualisation dans l’application
- Export possible
- Badge d’erreurs

👉 Utile pour debug et analyse

---

# ⚠️ 9. Ce que l’application NE fait PAS

- Aucune analyse de code
- Aucune modification
- Aucun parsing complexe

👉 LatuCollect = copier intelligent

---

# ⚙️ 10. Fonctionnalités principales

- Import de dossier
- Arborescence dynamique
- Recherche optimisée (debounce)
- Filtrage par extension
- Exclusion dossiers (bin, obj, .git)
- Aperçu temps réel
- Export TXT / Markdown
- Copie presse-papiers
- Statistiques temps réel
- Logs
- Mode développeur

---

# 🏗️ 11. Architecture

```text id="archreadme"
Core = logique métier
UI = affichage
```

### Services principaux

- FileImportService
- FileReaderService
- FileCollectionService
- FileExportService
- FileStatisticsService

👉 ViewModel = orchestrateur uniquement

---

# 📦 12. Structure

```text id="structreadme"
Core/
UI/WinUI/
Resources/
```

👉 Voir DIRECTORY_STRUCTURE.md

---

# 📌 13. État actuel

- Application stable
- UI fonctionnelle
- Pipeline optimisé
- Logs intégrés
- Mode développeur actif
- Architecture ALC respectée

👉 Phase de finalisation (0.10.0)

---

# ⭐ Points forts

- Simple
- Rapide
- Stable
- Lisible
- Sans risque (lecture seule)
- Optimisé pour IA

---

# 🧠 14. Philosophie

- Simplicité avant complexité
- Lisibilité avant optimisation
- Utilité avant fonctionnalité

---

# 🧠 15. Pourquoi LatuCollect

Contrairement aux autres outils :

- Pas d’analyse
- Pas de transformation
- Pas de complexité inutile

👉 Juste l’essentiel

---

# 🧪 16. Exemple

1. Import du dossier
2. Sélection des fichiers
3. Aperçu immédiat
4. Export

👉 Résultat : document propre et exploitable

---

# ⚠️ 17. Limites

- Pas de virtualisation avancée
- Pas de lazy loading
- Chargement partiel sur gros projets

👉 Choix volontaire pour garantir :

- stabilité
- simplicité
- lisibilité

---

# 🔭 Évolutions

Les prochaines améliorations et orientations du projet sont définies dans la feuille de route.

👉 Voir : [ROADMAP](./ROADMAP.md)

---

# 📝 Versions

L’historique des évolutions et des modifications du projet est disponible dans les notes de version.

👉 Voir : [PATCH NOTES](./PATCH_NOTES.md)
