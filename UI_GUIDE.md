# 🖥️ UI GUIDE – LATUCOLLECT

Guide officiel de l’interface utilisateur WinUI 3

Ce document définit :

- La structure visuelle
- Le comportement utilisateur
- Les interactions principales

👉 Document de référence pour toute l’interface utilisateur

---

# 🎯 OBJECTIF

Créer une interface :

- Simple
- Lisible
- Rapide à comprendre
- Fidèle au besoin réel

---

# 🧠 CONCEPT GLOBAL

```text id="pipelineui-guide"
Importer → Sélectionner → Aperçu → Exporter
```

👉 Toute l’interface repose sur ce flux

---

# 🧩 STRUCTURE PRINCIPALE

```text id="structureui-guide"
Gauche → Projet
Centre → Options
Droite → Aperçu
Bas → Export
```

---

# 🟦 ZONE GAUCHE — PROJET

## 🎯 Rôle

Afficher la structure complète du projet

---

## 📦 Contenu

- Bouton charger un dossier (📂)
- Barre de recherche
- Arborescence complète
- Fichiers sélectionnables (checkbox)

---

## ⚙️ Comportement

- Ouverture du sélecteur de dossier
- Chargement récursif complet
- Chargement asynchrone (UI non bloquée)
- Navigation dans les dossiers
- Filtrage dynamique

### 📁 Exclusions

- Dossiers exclus automatiquement :
  - bin
  - obj
  - .git

👉 Définis dans le Core (AppConfig)
👉 Jamais affichés dans l’arborescence

---

## 🔎 Recherche

- Filtrage fichiers + dossiers
- Insensible à la casse
- Mise à jour rapide (debounce)

### ✔ Améliorations

- Filtrage par extension :
  - .cs
  - .xaml
  - .json

- Optimisation :
  - Moins de recalculs
  - UI fluide même sur gros projets

### ⚠️ Aucun résultat

👉 Affichage :

"Aucun résultat"

---

## ✅ Sélection

- Checkbox = inclusion dans l’export
- Multi-sélection
- Mise à jour immédiate de l’aperçu

---

## ⚠️ Sélection globale

- Désactivée temporairement

👉 Raison :

- Éviter les ralentissements
- Préserver la fluidité

---

## ⚠️ Cas particuliers

- Dossier invalide → aucun chargement
- Accès refusé → ignoré

---

# 🟨 ZONE CENTRE — OPTIONS

## 🎯 Rôle

Configurer et interagir avec l’application

---

## 📦 Contenu

- Choix du format :
  - TXT
  - Markdown

- Bouton Copier

- Accès :

- Paramètres
- Statistiques
- Aide
- À propos
- Quitter

---

## 🧑‍💻 Mode développeur

- Activation via paramètres
- Indicateur visuel affiché
- Aucun impact utilisateur standard

---

## 📊 Statistiques

Affiche :

- Nombre de fichiers
- Nombre de lignes
- Nombre de caractères
- Taille totale

👉 Mise à jour en temps réel
👉 Calcul en arrière-plan

---

## 📋 Copier

- Copie le contenu de l’aperçu
- Message de confirmation

👉 Aperçu = contenu copié

---

## ⚙️ Dialogs

- Paramètres :
  - Gestion des dossiers exclus
  - Activation mode développeur

- Aide :
  - Guide rapide

- À propos :
  - Infos application

- Quitter :
  - Confirmation

👉 Toute modification → recharge de l’arbre

---

# 🟩 ZONE DROITE — APERÇU

## 🎯 Rôle

Afficher le document final

---

## 📦 Contenu

- Texte généré
- Scroll vertical
- Police monospace

---

## ⚙️ Comportement

Mise à jour automatique lors de :

- Sélection
- Désélection
- Recherche
- Chargement dossier

---

## 🔄 États UI

- Chargement → loader
- Erreur → message
- Prêt → contenu ou vide

---

## ⚠️ Cas

- Aucun fichier → message centré
- Contenu long → scroll actif

---

## ⚠️ Projets volumineux

👉 Message :

```text
⚠ Projet volumineux — affichage partiel
```

👉 Chargement partiel pour garantir la fluidité

---

## 📄 Contenu affiché

- Chemin du fichier
- Contenu
- Séparateur

---

# 📄 FORMAT D’AFFICHAGE

```text
Chemin du fichier

(contenu du fichier)

---

```

👉 Répété pour chaque fichier

---

## 🟢 Markdown

```text

## 📄 chemin du fichier

(contenu du fichier)

---

```

---

## 🔁 Cohérence

- Source unique : Core
- Aucun recalcul UI
- Aperçu = export

---

## ⚡ Optimisations

- Recalcul uniquement si nécessaire
- Cache interne
- Protection appels multiples

👉 Résultat :

- UI fluide
- Moins de charge

---

# 🔻 ZONE BASSE — ACTION

## 🎯 Rôle

Lancer l’export

---

## 📦 Contenu

- Bouton Exporter

---

## ⚙️ Comportement

- Génère le fichier final
- Respecte le format
- Utilise les fichiers sélectionnés
- Affiche confirmation

---

## ⚠️ Erreurs

- Aucun fichier sélectionné
- Échec export

👉 Message utilisateur

---

# 🧠 RÈGLE ALC

- Aucune logique métier dans UI
- UI = affichage uniquement

---

# 🧠 RÈGLES UX

- Une action = un rôle clair
- Pas de surcharge visuelle
- Aperçu = résultat final
- Feedback immédiat
- Aucun recalcul inutile

---

# ⚠️ INTERDIT

- Pipeline visible
- Multiples écrans
- Logique métier UI
- Actions cachées

---

# 🎯 OBJECTIF FINAL

Une interface :

- Intuitive
- Rapide
- Sans confusion
- Adaptée à un usage réel

👉 L’utilisateur comprend immédiatement quoi faire
