# 🖥️ UI GUIDE – LATUCOLLECT (V2)

Guide officiel de l’interface utilisateur WinUI 3

---

## 📌 Résumé

Ce document décrit :

- La structure visuelle
- Le comportement utilisateur
- Les règles UX

👉 Il sert de référence pour toute évolution de l’interface.

---

## 📊 État

- ✔ Implémenté (v0.10.0)
- 🔄 Évolution prévue (v0.17.0 — UI & thèmes)

---

# 🎯 OBJECTIF

Créer une interface :

- Simple
- Lisible
- Rapide à comprendre
- Prévisible

👉 Sans complexité inutile

---

# 🧠 CONCEPT GLOBAL

```text
Importer → Sélectionner → Aperçu → Exporter
```

👉 Toute l’interface repose sur ce flux

---

# 🧩 STRUCTURE PRINCIPALE

```text
Gauche → Projet
Centre → Options
Droite → Aperçu
Bas → Actions
```

👉 Structure FIXE (ne doit jamais être modifiée)

---

# 💬 FEEDBACK UTILISATEUR

## 🎯 Rôle

Informer sans bloquer l’utilisateur

---

## 📦 Exemples

- ✔ Export réussi
- ❌ Erreur export
- ⚠ Aucun fichier sélectionné
- ✔ Contenu copié

---

## ⚙️ Comportement

- Non bloquant
- Temporaire
- Visible dans l’aperçu

---

# 🟦 ZONE GAUCHE — PROJET (CŒUR APP)

## 🎯 Rôle

Afficher la structure du projet

---

## 📦 Contenu

- Bouton charger un dossier
- Barre de recherche
- Arborescence
- Checkboxes

---

## ⚙️ Comportement

- Chargement asynchrone
- Navigation fluide
- Filtrage dynamique

---

## 🔎 Recherche

- Insensible à la casse
- Débounce actif
- Mise à jour rapide

---

## ⚠️ Aucun résultat

👉 Message affiché :

"Aucun résultat"

---

## ✅ Sélection

- Multi-sélection
- Mise à jour immédiate du preview

---

## ⚠️ Cas limites

- Dossier invalide → ignoré
- Accès refusé → ignoré

---

# 🟨 ZONE CENTRE — OPTIONS

## 🎯 Rôle

Configurer et interagir

---

## 📦 Contenu

- Format (TXT / Markdown)
- Bouton Copier

---

## Accès :

- Paramètres
- Statistiques
- Aide
- À propos
- Quitter

---

## 🧑‍💻 Mode développeur

👉 Rôle :

- Debug
- Analyse
- Outils internes

👉 Règles :

- Désactivé par défaut
- Aucun impact utilisateur standard

---

## 📊 Statistiques

- Fichiers
- Lignes
- Caractères
- Taille

👉 Temps réel
👉 Async

---

## 📋 Copier

- Copie du preview
- Feedback utilisateur

---

# 🟩 ZONE DROITE — APERÇU

## 🎯 Rôle

Afficher le résultat final

---

## 📦 Contenu

- Texte généré
- Scroll vertical
- Police monospace

---

## ⚙️ Comportement

Mise à jour automatique :

- Sélection
- Recherche
- Chargement

---

## 🔄 États UI

- Loading
- Ready
- Empty
- Error

---

## ⚠️ Cas

- Aucun fichier → message
- Long contenu → scroll

---

## ⚠️ Gros projets

Message :

```text
⚠ Projet volumineux — affichage partiel
```

---

## 🔁 Cohérence

👉 Aperçu = Export (règle absolue)

---

# 🔻 ZONE BASSE — ACTIONS

## 🎯 Rôle

Actions finales

---

## 📦 Contenu

- Exporter
- Logs

---

## ⚙️ Comportement

- Génération du fichier
- Respect du format
- Feedback utilisateur

---

## ⚠️ Erreurs

- Aucun fichier
- Échec export

---

## 🧾 Logs

- Affichage dialog
- Filtrage (Info / Warning / Error)
- Export possible

---

# 🖥️ STABILITÉ UI

## 📏 Fenêtre

- Minimum : 1600 x 1000

---

## ⚡ Redimensionnement

- UI toujours lisible
- Aucun flickering

---

# 🧠 RÈGLE ALC

- UI = affichage uniquement
- Aucune logique métier

---

# 🧠 RÈGLES UX

- Une action = un rôle
- Pas de surcharge
- Feedback immédiat
- Aucun recalcul inutile

---

# ⚠️ INTERDIT

- Logique métier dans UI
- Multiples écrans
- Actions cachées

---

# 🎨 ÉVOLUTION UI (v0.17.0)

## 🎯 Objectif

Améliorer l’interface sans changer son fonctionnement

---

## Axes

- Thèmes (clair / sombre)
- Couleurs centralisées
- Typographie améliorée
- Alignements propres
- Icônes homogènes

---

## 🔍 Audit UX/UI

👉 Fonctionnement :

- Choix guidé
- Comparaison visuelle
- Décisions utilisateur

---

## Sujets concernés

- Thème global
- Boutons
- Arborescence
- Preview
- Feedback
- Logs
- Paramètres
- Couleurs
- Typographie

---

# 📌 ÉTAT ACTUEL

- ✔ Structure stable
- ✔ UI fluide
- ✔ Pipeline respecté
- ✔ Preview = Export

---

# 🎯 OBJECTIF FINAL

Une interface :

- Intuitive
- Rapide
- Claire
- Prévisible

👉 L’utilisateur comprend immédiatement quoi faire
