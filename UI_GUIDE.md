# 🖥️ UI GUIDE – LATUCOLLECT

Guide officiel de l’interface utilisateur WinUI 3.

Ce document définit :

- la structure visuelle
- le comportement utilisateur
- les interactions principales

👉 Référence unique pour toute la UI

---

# 🎯 OBJECTIF

Créer une interface :

- ✔ simple
- ✔ lisible
- ✔ rapide à comprendre
- ✔ fidèle au besoin réel

---

# 🧠 CONCEPT GLOBAL

```text
Importer → Sélectionner → Aperçu → Exporter
```

👉 Toute l’interface tourne autour de ce flux.

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

Afficher la structure complète du projet.

---

## 📦 Contenu

- Arborescence (dossiers + sous-dossiers)
- Fichiers sélectionnables (checkbox)

---

## ⚙️ Comportement

- Clic sur la zone → ouvre un sélecteur de dossier
- Chargement récursif complet
- Navigation dans les dossiers
- Bouton retour (remonter dossier)

---

## ✅ Sélection

- Checkbox = sélection pour export
- Multi-sélection possible
- Indépendant de l’aperçu

---

## ⚠️ Cas particuliers

- ❌ Dossier invalide → message
- ❌ Accès refusé → message

---

# 🟨 ZONE CENTRE — OPTIONS

## 🎯 Rôle

Configurer le format d’export.

---

## 📦 Contenu

- ☑ TXT
- ☑ Markdown

---

## ⚙️ Comportement

- Un seul format actif
- Le changement met à jour l’aperçu

---

# 🟩 ZONE DROITE — APERÇU

## 🎯 Rôle

Afficher le document final généré.

---

## 📦 Contenu

- Texte généré en temps réel
- Scroll vertical
- Police lisible (type code)

---

## ⚙️ Comportement

- Mise à jour automatique lors de :
  - sélection fichier
  - désélection
  - changement format

---

## ⚠️ Cas particuliers

- ❌ Aucun fichier sélectionné → aperçu vide
- ✔ Gros contenu → scroll actif

---

## 📄 Contenu affiché

- chemin du fichier
- contenu du fichier

---

# 📄 FORMAT D’AFFICHAGE

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

👉 répété pour chaque fichier sélectionné
👉 2 à 3 lignes vides entre chaque section

---

# 🔻 ZONE BASSE — ACTION

## 🎯 Rôle

Lancer l’export final.

---

## 📦 Contenu

- Bouton : **Exporter**

---

## ⚙️ Comportement

- Génère le fichier final
- Respecte le format sélectionné
- Utilise les fichiers cochés

---

## ⚠️ Cas erreurs

- ❌ Aucun fichier sélectionné → blocage
- ❌ Échec export → message

---

# ⚠️ FICHIERS IGNORÉS

- bin
- obj

---

# 🧠 RÈGLE ALC (IMPORTANT)

- ❌ aucune logique métier dans UI
- ✔ UI = affichage uniquement

---

# 🧠 RÈGLES UX IMPORTANTES

- ✔ Une action = un rôle clair
- ✔ Pas de surcharge visuelle
- ✔ Sélection ≠ aperçu
- ✔ Aperçu = résultat final
- ✔ Feedback immédiat

---

# ⚠️ INTERDIT

- ❌ Pipeline complexe visible
- ❌ Multiples écrans
- ❌ Logique métier dans UI
- ❌ Actions cachées

---

# 🎯 OBJECTIF FINAL

Une interface :

- ✔ intuitive
- ✔ rapide
- ✔ sans confusion
- ✔ adaptée à un usage réel

👉 L’utilisateur comprend immédiatement quoi faire
