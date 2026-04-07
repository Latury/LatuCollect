# 👤 GUIDE UTILISATEUR – LATUCOLLECT

LatuCollect est une application permettant de :

- ✅ Charger un projet (dossier)
- ✅ Naviguer dans sa structure
- ✅ Rechercher des fichiers rapidement
- ✅ Sélectionner des fichiers
- ✅ Visualiser un aperçu du contenu
- ✅ Copier le contenu généré
- ✅ Exporter un document structuré

👉 Aucun fichier source n’est modifié
👉 Lecture seule, aucune transformation du code

---

# 🖥️ 1. Interface

L’application est composée de 4 zones principales :

```text
Gauche → Arborescence projet
Centre → Options (format + actions)
Droite → Aperçu
Bas → Bouton export
```

---

# 📂 2. Charger un projet

## Étapes :

1. Cliquer sur le bouton 📂
2. Sélectionner un dossier

---

## Comportement :

- ✅ Chargement automatique des sous-dossiers
- ✅ Affichage des fichiers
- ✅ Structure identique au projet réel

---

## ⚠️ Cas d’erreur

- ❌ Dossier invalide → aucun chargement
- ❌ Accès refusé → aucun affichage

---

# 🌳 3. Naviguer dans le projet

L’utilisateur peut :

- ✅ Ouvrir les dossiers
- ✅ Naviguer dans les sous-dossiers
- ✅ Explorer toute la structure

---

# 🔎 4. Rechercher un fichier

Une barre de recherche permet de :

- ✅ Filtrer les fichiers et dossiers
- ✅ Rechercher rapidement par nom
- ✅ Recherche insensible à la casse

👉 Le résultat est affiché instantanément

---

# ☑️ 5. Sélectionner les fichiers

- ✅ Chaque fichier possède une checkbox
- ✅ Seuls les fichiers cochés sont utilisés

---

## ⚠️ Cas particulier

- ❌ Aucun fichier sélectionné → aperçu vide
- ❌ Bouton Copier désactivé
- ❌ Export impossible

---

# 👁️ 6. Aperçu en temps réel

## Déclencheurs :

L’aperçu se met à jour :

- ✅ Lors de la sélection d’un fichier
- ✅ Lors de la désélection
- ✅ Lors d’une recherche

---

### 🔁 Cohérence du contenu (v0.4.0)

- ✅ L’aperçu correspond exactement au fichier exporté
- ✅ Aucun écart possible entre ce que vous voyez et ce qui sera généré
- ✅ Le contenu est généré une seule fois pour garantir la fiabilité

---

## Comportement :

- ✅ Message affiché si aucun fichier sélectionné
- ✅ Contenu affiché si sélection active
- ✅ Affichage type éditeur (aligné à gauche, monospace)
- ✅ Scroll automatique si contenu long

---

## Format affiché :

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

👉 Répété pour chaque fichier sélectionné
👉 2 à 3 lignes vides entre chaque section

---

# ⚙️ 7. Choisir le format

Dans la zone centrale :

- ✅ TXT
- ✅ Markdown (.md)

👉 Impact direct sur l’export

---

# 📋 8. Copier le contenu

## Étapes :

1. Cliquer sur "Copier"

---

## Résultat :

- ✅ Contenu copié dans le presse-papiers
- ✅ Message de confirmation affiché

---

## ⚠️ Cas particulier

- ❌ Bouton désactivé si aucun contenu

---

# 📤 9. Exporter

## Étapes :

1. Cliquer sur "Exporter"
2. Choisir un emplacement
3. Valider

---

## Résultat :

- ✅ Fichier généré
- ✅ Contenu identique à l’aperçu
- ✅ Format respecté (.txt ou .md)
- ✅ Message de confirmation affiché

---

## ⚠️ Cas d’erreur

- ❌ Aucun fichier sélectionné
- ❌ Chemin invalide
- ❌ Échec d’écriture

---

### 🔒 Fiabilité (v0.4.0)

- ✅ Aucun crash lors d’un problème d’export
- ✅ Message clair affiché en cas d’erreur
- ✅ L’application reste utilisable même si l’export échoue

---

# ⚙️ 10. Options et menus

L’application propose plusieurs actions :

---

## ⚙️ Options

- ✅ Paramètres de base (évolutifs)

---

## ❓ Aide

- ✅ Explication simple du fonctionnement

---

## ℹ️ À propos

- ✅ Informations sur l’application

---

## 🚪 Quitter

- ✅ Demande de confirmation avant fermeture

---

# ⚙️ 11. Fonctionnement interne

```text
Import → Lecture → Assemblage → Export
```

👉 L’application ne modifie jamais le contenu
👉 Elle copie uniquement le texte

👉 Le contenu est construit une seule fois puis utilisé pour l’aperçu et l’export

👉 Cela garantit une cohérence parfaite entre affichage et fichier final

---

## Traitement automatique :

- ✅ Lecture des fichiers
- ✅ Assemblage du contenu
- ✅ Génération du document final

---

# ⚠️ 12. Règles importantes

- ✅ Aucun fichier source n’est modifié
- ✅ Seuls les fichiers sélectionnés sont exportés
- ✅ L’aperçu = résultat final

---

# 🧠 13. Conseils

- ✅ Vérifier l’aperçu avant export
- ✅ Utiliser Markdown pour plus de lisibilité
- ✅ Sélectionner uniquement les fichiers utiles

---

# 🎯 Objectif

LatuCollect permet de :

- ✅ Regrouper du code rapidement
- ✅ Obtenir un export propre
- ✅ Éviter les erreurs manuelles

👉 Un outil simple, visuel et efficace

---

# ⚙️ 14. Évolutions futures

- 🔄 Mode débutant / expert
- 🔄 Options avancées
- 🔄 Amélioration UX
