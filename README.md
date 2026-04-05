<div align="center">

# 🧭 LATUCOLLECT

### Collecte simple et rapide de contenu multi-fichiers

🔹 Outil simple pour extraire, assembler et exporter du contenu de fichiers.
🔹 Pensé pour la lisibilité et l’usage avec des outils d’IA

<br>

![Version](https://img.shields.io/badge/Version-0.2.0-1E90FF?style=for-the-badge)
![Statut](https://img.shields.io/badge/Statut-En%20développement-FF8C00?style=for-the-badge)
![Licence](https://img.shields.io/badge/Licence-MIT-2E8B57?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-8-512BD4?style=for-the-badge)
![UI](https://img.shields.io/badge/UI-WinUI3-0078D6?style=for-the-badge)

<br>

--Auteur : Flo Latury--

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

👉 Idéal pour les développeurs débutant et les projets techniques

---

# 🧠 3. Fonctionnement

L’application fonctionne avec un flux simple :

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🖥️ 4. Interface (WinUI)

L’interface est composée de 4 zones :

## 🟦 Gauche — Projet (CŒUR DE L’APP)

- Arborescence complète du projet
- Navigation dans les dossiers
- Barre de recherche
- Sélection des fichiers via checkbox

---

## 🟨 Centre — Options

- Choix du format d’export :
  - ✅ TXT
  - ✅ Markdown
- Bouton Copier
- Accès :
  - Options
  - Aide
  - À propos
  - Quitter

---

## 🟩 Droite — Aperçu

- Affichage du document final généré
- Mise à jour en temps réel
- Affichage type éditeur (monospace, aligné à gauche)
- Gestion des états :
  - Message si aucun fichier sélectionné
  - Contenu affiché si sélection active

👉 correspond exactement au fichier exporté

---

## 🔻 Bas — Action

- Bouton Exporter
- Génère le fichier final
- Affiche une confirmation

---

# 📄 5. Format d’export

Chaque fichier sélectionné est exporté avec :

```text
C:\Projet\fichier.cs


(contenu du fichier)


----------------------------------------
```

👉 2 à 3 lignes vides entre chaque section

---

# ⚙️ 6. Fonctionnement interne

```text
Import → Lecture → Assemblage → Export
```

👉 Traitement automatique via le Core
👉 Aucune transformation du code

---

# ⚠️ 7. Ce que l’application NE fait PAS

- ❌ Aucune analyse de code
- ❌ Aucune modification des fichiers
- ❌ Aucun parsing complexe

👉 LatuCollect est un outil de copie intelligente, pas un analyseur

---

# ⚙️ 8. Fonctionnalités principales

- ✅ Import de dossiers complets (récursif)
- ✅ Construction automatique de l’arborescence
- ✅ Recherche dynamique
- ✅ Sélection manuelle des fichiers
- ✅ Aperçu en temps réel
- ✅ Copie dans le presse-papiers
- ✅ Export TXT / Markdown
- ✅ Dialogs utilisateur (Options, Aide, À propos)
- ✅ Confirmation des actions (export, quitter)

---

# 🏗️ 9. Architecture

Le projet respecte le standard ALC :

- Core → logique métier
- UI → affichage uniquement

👉 Aucune logique métier dans l’interface

---

# 📦 10. Structure

```text
Core/
UI/WinUI/
Resources/
```

---

# ⚠️ 11. État actuel

- ✅ Core fonctionnel
- ✅ UI fonctionnelle (WinUI 3)
- 🔄 Améliorations UX en cours

---

# 🧠 12. Philosophie

LatuCollect est conçu pour être :

- ✅ Simple
- ✅ Rapide
- ✅ Lisible

👉 Un outil utile avant tout
