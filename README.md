<div align="center">

# 🧭 LATUCOLLECT

### Collecte simple et rapide de contenu multi-fichiers

Outil permettant de sélectionner des fichiers dans un projet et de générer un export propre, lisible et prêt à être utilisé (IA, documentation, partage).

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

- ✔ Charger un projet complet
- ✔ Naviguer dans sa structure (dossiers / fichiers)
- ✔ Sélectionner les fichiers à collecter
- ✔ Générer un aperçu du résultat final
- ✔ Exporter un fichier structuré (.txt ou .md)

👉 Aucun fichier source n’est modifié

---

# 🧠 2. Fonctionnement

L’application fonctionne avec un flux simple :

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🖥️ 3. Interface (WinUI)

L’interface est composée de 4 zones :

## 🟦 Gauche — Projet (CŒUR DE L’APP)

- Arborescence complète du projet
- Navigation dans les dossiers
- Sélection des fichiers via checkbox

---

## 🟨 Centre — Options

- Choix du format d’export :
  - ✔ TXT
  - ✔ Markdown

---

## 🟩 Droite — Aperçu

- Affichage du document final généré
- Mise à jour en temps réel
- 👉 correspond exactement au fichier exporté

---

## 🔻 Bas — Action

- Bouton **Exporter**
- Génère le fichier final

---

# 📄 4. Format d’export

Chaque fichier sélectionné est exporté avec :

```
C:\Projet\fichier.cs


(contenu du fichier)


----------------------------------------
```

👉 2 à 3 lignes vides entre chaque section

---

# ⚙️ 5. Fonctionnement interne

```text
Import → Lecture → Assemblage → Export
```

👉 Traitement automatique via le Core

---

# ⚙️ 6. Fonctionnalités principales

- ✔ Import de dossiers complets (récursif)
- ✔ Filtrage automatique des fichiers
- ✔ Sélection manuelle
- ✔ Aperçu en temps réel
- ✔ Export TXT / Markdown

---

# 🏗️ 7. Architecture

Le projet respecte le standard ALC :

- Core → logique métier
- UI → affichage uniquement

👉 aucune logique métier dans l’interface

---

# 📦 8. Structure

```
Core/
UI/
Resources/
```

---

# ⚠️ 9. État actuel

- ✔ Core fonctionnel
- ✔ UI en cours de développement (WinUI 3)

---

# 🧠 10. Philosophie

LatuCollect est conçu pour être :

- ✔ Simple
- ✔ Rapide
- ✔ Lisible

👉 Un outil utile avant tout

---
