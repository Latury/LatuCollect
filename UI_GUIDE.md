# 🖥️ UI GUIDE – LATUCOLLECT

Guide officiel de l’interface utilisateur WinUI 3

Ce document définit :

- La structure visuelle
- Le comportement utilisateur
- Les interactions principales

👉 Document de référence pour toute l’interface utilisateur

## 📌 Résumé

Ce document décrit l’organisation de l’interface utilisateur, le comportement des différentes zones et les règles UX du projet LatuCollect.

👉 Il sert de référence pour toute modification de l’interface.

## 📊 État des fonctionnalités

- ✅ Implémenté
- 🔮 À venir [ROADMAP](./ROADMAP.md)

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

# 💬 FEEDBACK UTILISATEUR

## 🎯 Rôle

Informer l’utilisateur des actions en cours ou terminées

---

## 📦 Exemples

- ✔ Export réussi
- ❌ Erreur export
- ⚠ Aucun fichier sélectionné
- ✔ Contenu copié

---

## ⚙️ Comportement

- Affichage temporaire
- Non bloquant
- Affiché dans la zone aperçu (si actif)

👉 Permet un retour immédiat sans interrompre l’utilisateur

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

### 🔮 Évolutions

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

- Désactivée (choix volontaire actuel)

👉 Raison :

- Éviter les ralentissements
- Préserver la fluidité

👉 Réintégration possible dans les versions futures

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

Actions finales de l’application

---

## 📦 Contenu

- Bouton Exporter
- Bouton Logs
- Bouton Simulation (mode développeur uniquement)

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

## 🧾 Logs

- Accès via bouton dédié
- Affichage dans un dialog
- Filtrage (Info / Warning / Error)
- Export possible

👉 Utilisé pour debug et suivi application

---

# 🖥️ STABILITÉ UI

## 📏 Fenêtre

- Taille minimale : 1600 x 1000
- Empêche la casse de l’interface

---

## ⚡ Redimensionnement

- Gestion contrôlée
- Réduction du flickering
- UI toujours lisible

👉 Objectif : stabilité visuelle

---

# 📌 ÉTAT ACTUEL UI

- ✔ Interface structurée en 4 zones (Gauche / Centre / Droite / Bas)
- ✔ Chargement asynchrone du projet
- ✔ Recherche dynamique avec debounce
- ✔ Aperçu temps réel
- ✔ Export fonctionnel
- ✔ Mode développeur actif
- ✔ Système de logs accessible

👉 UI stable en version 0.10.0

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

---

# 🔮 Évolutions UI

Les améliorations futures de l’interface sont définies dans la roadmap.

👉 Voir : [ROADMAP](./ROADMAP.md)
