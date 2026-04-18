# 🖥️ UI GUIDE – LATUCOLLECT

Guide officiel de l’interface utilisateur WinUI 3.

Ce document définit :

* La structure visuelle
* Le comportement utilisateur
* Les interactions principales

👉 Document de référence pour toute l’interface utilisateur

---

# 🎯 OBJECTIF

Créer une interface :

* ✅ Simple
* ✅ Lisible
* ✅ Rapide à comprendre
* ✅ Fidèle au besoin réel

---

# 🧠 CONCEPT GLOBAL

```text
Importer → Sélectionner → Aperçu → Exporter
```

👉 Toute l’interface tourne autour de ce flux

---

# 🧩 STRUCTURE PRINCIPALE

```text
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

* Bouton Charger un dossier (📂)
* Barre de recherche
* Arborescence (dossiers + sous-dossiers)
* Fichiers sélectionnables (checkbox)

---

## ⚙️ Comportement (zone gauche)

* Clic sur le bouton 📂 → ouvre un sélecteur de dossier
* Chargement récursif complet
* Chargement asynchrone (UI non bloquée)
* Navigation dans les dossiers
* Filtrage dynamique via la recherche
* Certains dossiers sont exclus automatiquement (ex: bin, obj, .git)
* Ces exclusions sont définies dans la configuration globale (Core)
* Les dossiers exclus ne sont jamais affichés dans l’arborescence
* L’arbre est allégé pour améliorer les performances et la lisibilité

---

## 🔎 Recherche

* Filtre les fichiers et dossiers
* Recherche insensible à la casse
* Mise à jour rapide (optimisée avec un léger délai pour la fluidité)

### 🔄 Améliorations (v0.6.0)

* Filtrage possible par extension :

  * .cs
  * .xaml
  * .json

👉 Permet de cibler rapidement un type de fichier

---

* Optimisation des performances :

  * Déclenchement différé (debounce)
  * Réduction des recalculs inutiles

👉 Garantit une recherche fluide même sur de gros projets

---

* Gestion du cas "aucun résultat" :

👉 Si aucun fichier ne correspond :
→ un message "Aucun résultat" est affiché au centre de la zone

✔ Évite un écran vide incompréhensible
✔ Améliore la lisibilité de l’interface

---

## ✅ Sélection

* Checkbox = sélection pour export
* Multi-sélection possible
* Mise à jour immédiate de l’aperçu

---

### ⚠️ Sélection globale

* La sélection globale est temporairement désactivée

### 📢 Comportement (v0.7.0)

* Clic sur "Tout sélectionner" :
  → affiche un popup explicatif

👉 Raison :

* Éviter les ralentissements sur les gros projets
* Préserver la fluidité de l’application

👉 Une amélioration future est prévue (sélection intelligente)

---

## ⚠️ Cas particuliers

* ❌ Dossier invalide → aucun chargement
* ❌ Accès refusé → aucun affichage

---

# 🟨 ZONE CENTRE — OPTIONS

## 🎯 Rôle

Configurer et interagir avec l’application

---

## 📦 Contenu

* Choix du format :

  * ✅ TXT
  * ✅ Markdown

* Bouton Copier

* Boutons :

  * Options
  * Statistiques
  * Aide
  * À propos
  * Quitter

---

## 📊 Statistiques (v0.8.0)

* Bouton dédié (entre Paramètres et Aide)
* Affiche un dialog contenant :

  * Nombre de fichiers sélectionnés
  * Nombre total de lignes
  * Nombre total de caractères
  * Taille totale

---

### ⚙️ Comportement

* Mise à jour en temps réel
* Calcul effectué en arrière-plan
* Aucun blocage de l’interface

👉 Permet à l’utilisateur de mieux comprendre le contenu sélectionné
👉 Basé uniquement sur les fichiers sélectionnés

---

### ⚙️ Interaction utilisateur

* Un seul format actif
* Le format impacte l’export
* Le bouton Copier :

  * Activé si contenu présent
  * Désactivé si aucun contenu

---

## 📋 Copier

* Copie le contenu de l’aperçu
* Affiche un message de confirmation

👉 Le contenu copié correspond exactement à l’aperçu affiché

---

## ⚙️ Dialogs

* Options → paramètres simples

  * Ajouter un dossier à exclure
  * Supprimer un dossier de la liste

* Aide → explication rapide

* À propos → informations application

* Quitter → demande de confirmation

👉 Toute modification entraîne un rechargement de l’arborescence

---

# 🟩 ZONE DROITE — APERÇU

## 🎯 Rôle

Afficher le document final généré

---

## 📦 Contenu

* Texte généré en temps réel
* Scroll vertical
* Police monospace (type code)

---

## ⚙️ Comportement

* Mise à jour automatique lors de :

  * Sélection fichier
  * Désélection
  * Recherche
  * Chargement d’un dossier

---

## 🔄 États UI (v0.5.0)

* 🔄 Chargement → affichage d’un loader
* ❌ Erreur → message affiché
* ✅ Prêt → contenu ou message vide

👉 L’interface reflète toujours l’état réel de l’application

---

## ⏳ Chargement

* Affichage d’un indicateur visuel (loader)
* Empêche toute confusion pendant le chargement
* Disparaît automatiquement une fois terminé

---

## ⚠️ Cas particuliers

* ❌ Aucun fichier sélectionné → message centré "Aucun fichier sélectionné..."
* ✅ Contenu long → scroll actif

---

## ⚠️ Projets volumineux

* Chargement partiel si projet trop volumineux
* Aucun blocage de l’interface
* Message affiché :

```text
⚠ Projet volumineux — affichage partiel
```

👉 Ce comportement garantit la stabilité et la fluidité de l’application

---

## 📄 Contenu affiché

* Chemin du fichier
* Contenu du fichier
* Séparateur visuel

---

# 📄 FORMAT D’AFFICHAGE

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

👉 Répété pour chaque fichier sélectionné
👉 2 à 3 lignes vides entre chaque section

---

### 🟢 Format Markdown (v0.8.0)

```md
## 📄 chemin du fichier

(contenu du fichier)

---
```

---

### 🔁 Cohérence (v0.4.0)

* ✅ Le contenu affiché provient du Core (source unique)
* ✅ Aucun recalcul côté UI
* ✅ L’aperçu est strictement identique à l’export

---

# 🔻 ZONE BASSE — ACTION

## 🎯 Rôle

Lancer l’export final

---

## 📦 Contenu

* Bouton Exporter

---

## ⚙️ Comportement

* Génère le fichier final
* Respecte le format sélectionné
* Utilise les fichiers cochés
* Affiche une confirmation

---

## ⚠️ Cas erreurs

* ❌ Aucun fichier sélectionné
* ❌ Échec export → message

---

### 🔒 Robustesse (v0.4.0)

* ✅ Aucun crash en cas d’erreur d’export
* ✅ Message d’erreur affiché à l’utilisateur
* ✅ Comportement stable même en cas de problème disque ou accès refusé

---

# 🧠 RÈGLE ALC (IMPORTANT)

* ❌ Aucune logique métier dans UI
* ✅ UI = affichage uniquement

---

# 🧠 RÈGLES UX IMPORTANTES

* ✅ Une action = un rôle clair
* ✅ Pas de surcharge visuelle
* ✅ Sélection ≠ aperçu
* ✅ Aperçu = résultat final
* ✅ Feedback immédiat (non intrusif)
* ✅ Le contenu affiché reflète exactement le résultat final exporté

---

# ⚠️ INTERDIT

* ❌ Pipeline complexe visible
* ❌ Multiples écrans
* ❌ Logique métier dans UI
* ❌ Actions cachées

---

# 🎯 OBJECTIF FINAL

Une interface :

* ✅ Intuitive
* ✅ Rapide
* ✅ Sans confusion
* ✅ Adaptée à un usage réel

👉 L’utilisateur comprend immédiatement quoi faire
