# 📝 PATCH NOTES – LatuCollect

Historique officiel des évolutions du projet LatuCollect.

---

# 📚 Organisation documentaire

| Fichier              | Rôle         |
| -------------------- | ------------ |
| README.md            | Présentation |
| ARCHITECTURE.md      | Structure    |
| GUIDE_UTILISATEUR.md | Utilisation  |
| TESTS.md             | Validation   |
| PATCH_NOTES.md       | Historique   |

---

# 🚀 VERSION 0.1.0

## 📌 Statut

🟢 Fonctionnel (console – version historique)

---

## 🎯 Objectif

Mettre en place un système simple de collecte de contenu :

- ✔ Importer des fichiers
- ✔ Lire leur contenu
- ✔ Regrouper le texte
- ✔ Exporter un fichier unique

---

## ✨ Fonctionnalités

### 📥 Import

- ✔ Ajout de fichiers
- ✔ Ajout de dossiers
- ✔ Gestion des chemins

---

### 📄 Lecture

- ✔ Lecture du contenu des fichiers
- ✔ Aucune transformation du code

---

### 📤 Export

- ✔ Génération fichier TXT
- ✔ Contenu concaténé

---

## 🏗️ Architecture

Pipeline initial :

```text
Import → Lecture → Export
```

---

## ⚠️ Limitations

- ❌ Pas d’interface graphique
- ❌ Pas de sélection visuelle
- ❌ Export peu structuré

---

# 🚀 VERSION 0.2.0 (ACTUELLE)

## 📌 Statut

🟡 En développement (WinUI)

---

## 🎯 Objectif

Passer à une application graphique permettant une collecte visuelle et contrôlée du contenu.

---

## ✨ Évolutions majeures

### 🖥️ Interface WinUI

- ✔ Structure gauche / centre / droite
- ✔ Arborescence projet
- ✔ Navigation dossiers
- ✔ Sélection via checkbox

---

### 👁️ Aperçu

- ✔ Génération en temps réel
- ✔ Correspond exactement au fichier exporté

---

### 📤 Export

- ✔ TXT / Markdown
- ✔ Format structuré
- ✔ Ajout du chemin des fichiers

---

### ⚙️ Architecture

- ✔ séparation Core / UI
- ✔ préparation MVVM
- ✔ simplification du fonctionnement

---

## 🧠 Fonctionnement utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

## ⚙️ Pipeline interne

```text
Import → Lecture → Assemblage → Export
```

👉 Lecture = récupération du contenu
👉 Assemblage = regroupement du texte

👉 ❗ Aucune transformation du code

---

## ⚠️ Évolutions importantes

- ✔ passage d’un outil console à une interface visuelle
- ✔ suppression de la complexité inutile
- ✔ recentrage sur la collecte simple

---

# 🚀 VERSION 0.3.0 (PRÉVU)

## 🎯 Objectif

Améliorer l’expérience utilisateur

---

## ✨ Évolutions prévues

- 🔄 feedback utilisateur
- 🔄 gestion des erreurs
- 🔄 amélioration lisibilité

---

# 🚀 VERSION 0.4.0 (PRÉVU)

## 🎯 Objectif

Améliorer la qualité des exports

---

## ✨ Évolutions prévues

- 🔄 format optimisé pour IA
- 🔄 meilleure structuration du texte

---

# 🚀 VERSION 0.5.0 (PRÉVU)

## 🎯 Objectif

Améliorer le confort d’utilisation

---

## ✨ Évolutions prévues

- 🔄 filtrage fichiers
- 🔄 exclusion dossiers (bin, obj)
- 🔄 sauvegarde préférences

---

# 🚀 VERSION 0.6.0 (OPTIONNEL)

## 🎯 Objectif

Fonctionnalités avancées si nécessaire

---

## ✨ Évolutions possibles

- 🔄 analyse
- 🔄 déduplication
- 🔄 statistiques

👉 uniquement si besoin réel

---

# 🧠 Philosophie

- ✔ Simplicité avant complexité
- ✔ Lisibilité avant optimisation
- ✔ Utilité avant fonctionnalité

---

# 📜 Rôle du Patch Notes

- ✔ Suivre l’évolution réelle
- ✔ Comprendre les changements
- ✔ Garantir la cohérence

👉 Un historique fiable = projet maintenable
