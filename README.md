<div align="center">

# 🧭 LATUCOLLECT

### Collecte simple et rapide de contenu multi-fichiers

🔹 Outil simple pour extraire, assembler et exporter du contenu de fichiers
🔹 Pensé pour la lisibilité et l’usage avec des outils d’IA

<br>

![Version](https://img.shields.io/badge/Version-0.6.0-FFDF20?style=for-the-badge)
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
- Bouton "Tout sélectionner"

---

## 🟨 Centre — Options

- Choix du format d’export :
  - TXT
  - Markdown

- Bouton Copier

- Accès :
  - Options
  - Aide
  - À propos
  - Quitter

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
Import → Lecture → Assemblage → Export
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

---

# 🏗️ 10. Architecture

```text id="archreadme"
Core = logique métier
UI = affichage uniquement
```

👉 Respect strict du modèle ALC

---

# 📦 11. Structure

```text id="structreadme"
Core/
UI/WinUI/
Resources/
```

👉 Voir : DIRECTORY_STRUCTURE.md pour le détail complet

---

# 📌 12. État actuel

- ✅ Version 0.6.0 stable
- ✅ UX améliorée
- ✅ Application fluide
- ✅ Gestion des gros projets sécurisée

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
