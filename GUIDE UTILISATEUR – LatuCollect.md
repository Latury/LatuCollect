# 👤 GUIDE UTILISATEUR – LATUCOLLECT

LatuCollect est une application permettant de :

- ✔ Charger un projet (dossier)
- ✔ Naviguer dans sa structure
- ✔ Sélectionner des fichiers
- ✔ Visualiser un aperçu du contenu
- ✔ Exporter un document structuré

👉 ✔ Aucun fichier source n’est modifié

---

# 🖥️ 1. Interface

L’application est composée de 4 zones principales :

```text
Gauche  → Arborescence projet
Centre  → Options (format)
Droite  → Aperçu
Bas     → Bouton export
```

---

# 📂 2. Charger un projet

## Étapes :

1. Cliquer sur la zone gauche
2. Sélectionner un dossier

---

## Comportement :

- ✔ Chargement automatique des sous-dossiers
- ✔ Affichage des fichiers
- ✔ Structure identique au projet réel

---

## ⚠️ Cas d’erreur

- ❌ Dossier invalide → aucun chargement
- ❌ Accès refusé → affichage erreur

---

# 🌳 3. Naviguer dans le projet

L’utilisateur peut :

- ✔ Ouvrir les dossiers
- ✔ Naviguer dans les sous-dossiers
- ✔ Revenir en arrière

---

# ☑️ 4. Sélectionner les fichiers

- ✔ Chaque fichier possède une checkbox
- ✔ Seuls les fichiers cochés sont utilisés

---

## ⚠️ Cas particulier

- ❌ Aucun fichier sélectionné → aperçu vide
- ❌ Export impossible

---

# 👁️ 5. Aperçu en temps réel

## Déclencheurs :

L’aperçu se met à jour :

- ✔ lors de la sélection d’un fichier
- ✔ lors de la désélection
- ✔ lors du changement de format

---

## Format affiché :

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

👉 répété pour chaque fichier sélectionné

👉 2 à 3 lignes vides entre chaque section

---

# ⚙️ 6. Choisir le format

Dans la zone centrale :

- ✔ TXT
- ✔ Markdown (.md)

👉 impact direct sur l’aperçu

---

# 📤 7. Exporter

## Étapes :

1. Cliquer sur "Exporter"
2. Choisir un emplacement

---

## Résultat :

- ✔ Fichier généré
- ✔ Contenu identique à l’aperçu
- ✔ Format respecté

---

## ⚠️ Cas d’erreur

- ❌ Aucun fichier sélectionné
- ❌ Chemin invalide
- ❌ Échec d’écriture

---

# ⚙️ 8. Comportement interne

```text
Import → Lecture → Assemblage → Export
```

👉 L’application ne modifie jamais le contenu
👉 Elle copie uniquement le texte
👉 traitement automatique :

- ✔ lecture des fichiers
- ✔ génération du document

---

## ⚠️ Fichiers ignorés

Certains dossiers peuvent être exclus :

- bin
- obj

---

# ⚠️ 9. Règles importantes

- ✔ Aucun fichier source n’est modifié
- ✔ Seuls les fichiers sélectionnés sont exportés
- ✔ L’aperçu = résultat final

---

# 🧠 10. Conseils

- ✔ Vérifier l’aperçu avant export
- ✔ Utiliser Markdown pour plus de lisibilité
- ✔ Sélectionner uniquement les fichiers utiles

---

# 🎯 Objectif

LatuCollect permet de :

- ✔ Regrouper du code rapidement
- ✔ Obtenir un export propre
- ✔ Éviter les erreurs manuelles

👉 ✔ Un outil simple, visuel et efficace

---

# ⚙️ 11. Évolutions futures

- 🔄 Mode débutant / expert
- 🔄 Options avancées
- 🔄 amélioration UX

---
