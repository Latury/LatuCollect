# 🖥️ UI GUIDE – LATUCOLLECT

Guide officiel de l’interface utilisateur WinUI 3.

Ce document définit :

- La structure visuelle
- Le comportement utilisateur
- Les interactions principales

👉 Référence unique pour toute la UI

---

# 🎯 OBJECTIF

Créer une interface :

- ✅ Simple
- ✅ Lisible
- ✅ Rapide à comprendre
- ✅ Fidèle au besoin réel

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

- Bouton Charger un dossier (📂)
- Barre de recherche
- Arborescence (dossiers + sous-dossiers)
- Fichiers sélectionnables (checkbox)

---

## ⚙️ Comportement

- Clic sur le bouton 📂 → ouvre un sélecteur de dossier
- Chargement récursif complet
- Navigation dans les dossiers
- Filtrage dynamique via la recherche

---

## 🔎 Recherche

- Filtre les fichiers et dossiers
- Recherche insensible à la casse
- Mise à jour instantanée

---

## ✅ Sélection

- Checkbox = sélection pour export
- Multi-sélection possible
- Mise à jour immédiate de l’aperçu

---

## ⚠️ Cas particuliers

- ❌ Dossier invalide → aucun chargement
- ❌ Accès refusé → aucun affichage

---

# 🟨 ZONE CENTRE — OPTIONS

## 🎯 Rôle

Configurer et interagir avec l’application

---

## 📦 Contenu

- Choix du format :
  - ✅ TXT
  - ✅ Markdown
- Bouton Copier
- Boutons :
  - Options
  - Aide
  - À propos
  - Quitter

---

## ⚙️ Comportement

- Un seul format actif
- Le format impacte l’export
- Le bouton Copier :
  - Activé si contenu présent
  - Désactivé si aucun contenu

---

## 📋 Copier

- Copie le contenu de l’aperçu
- Affiche un message de confirmation

👉 Le contenu copié correspond exactement à l’aperçu affiché

---

## ⚙️ Dialogs

- Options → paramètres simples
- Aide → explication rapide
- À propos → informations application
- Quitter → demande de confirmation

---

# 🟩 ZONE DROITE — APERÇU

## 🎯 Rôle

Afficher le document final généré

---

## 📦 Contenu

- Texte généré en temps réel
- Scroll vertical
- Police monospace (type code)

---

## ⚙️ Comportement

- Mise à jour automatique lors de :
  - Sélection fichier
  - Désélection
  - Recherche
  - Chargement d’un dossier

---

## ⚠️ Cas particuliers

- ❌ Aucun fichier sélectionné → message centré "Aucun fichier sélectionné..."
- ✅ Contenu long → scroll actif

---

## 📄 Contenu affiché

- Chemin du fichier
- Contenu du fichier
- Séparateur visuel

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

### 🔁 Cohérence (v0.4.0)

- ✅ Le contenu affiché provient du Core (source unique)
- ✅ Aucun recalcul côté UI
- ✅ L’aperçu est strictement identique à l’export

---

# 🔻 ZONE BASSE — ACTION

## 🎯 Rôle

Lancer l’export final

---

## 📦 Contenu

- Bouton Exporter

---

## ⚙️ Comportement

- Génère le fichier final
- Respecte le format sélectionné
- Utilise les fichiers cochés
- Affiche une confirmation

---

## ⚠️ Cas erreurs

- ❌ Aucun fichier sélectionné
- ❌ Échec export → message

---

### 🔒 Robustesse (v0.4.0)

- ✅ Aucun crash en cas d’erreur d’export
- ✅ Message d’erreur affiché à l’utilisateur
- ✅ Comportement stable même en cas de problème disque ou accès refusé

---

# 🧠 RÈGLE ALC (IMPORTANT)

- ❌ Aucune logique métier dans UI
- ✅ UI = affichage uniquement

---

# 🧠 RÈGLES UX IMPORTANTES

- ✅ Une action = un rôle clair
- ✅ Pas de surcharge visuelle
- ✅ Sélection ≠ aperçu
- ✅ Aperçu = résultat final
- ✅ Feedback immédiat
- ✅ Le contenu affiché doit toujours refléter exactement le résultat final exporté

---

# ⚠️ INTERDIT

- ❌ Pipeline complexe visible
- ❌ Multiples écrans
- ❌ Logique métier dans UI
- ❌ Actions cachées

---

# 🎯 OBJECTIF FINAL

Une interface :

- ✅ Intuitive
- ✅ Rapide
- ✅ Sans confusion
- ✅ Adaptée à un usage réel

👉 L’utilisateur comprend immédiatement quoi faire
