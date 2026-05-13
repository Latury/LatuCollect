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

- ✔ Stabilisé majoritairement (v0.11.0)
- 🔄 Évolution UI prévue (v0.17.0 — thèmes & audit UX)

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
- Conservation de l’arbre réel

---

## 🌳 Recherche TreeView

- Filtrage dynamique sans duplication d’arbre
- Conservation de la navigation réelle
- Expansion automatique pendant recherche
- Reset visibilité après recherche
- Reset expansion après recherche
- Débounce anti-spam

---

## ⚠️ Aucun résultat

👉 Message affiché :

"Aucun résultat"

---

## ✅ Sélection

- Multi-sélection
- Support tri-state (`true / false / null`)
- Synchronisation parent ↔ enfants
- Mise à jour immédiate du preview
- Comportement inspiré de Windows Explorer

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
- Protection anti double génération preview
- Optimisation basée signature sélection
- Limitation automatique contenu massif

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

- Chargement partiel possible
- Limitation preview automatique
- Protection contre les recalculs inutiles

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

# ⚡ STABILITÉ UI / PERFORMANCE

## Règles importantes

- Aucun reload complet inutile
- Aucun blocage UI volontaire
- Aucun recalcul massif inutile
- Preview synchronisé avec la sélection
- Aperçu toujours cohérent avec l’export

## Optimisations actuelles

- Protection anti multi-refresh
- Protection anti multi-clic
- Réduction recalculs preview
- Mise à jour ciblée TreeView
- Conservation de l’arbre réel (pas de duplication)
- Filtrage basé visibilité (`IsVisible`)
- Mise à jour ciblée sans reload complet

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
