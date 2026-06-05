# 1. 🖥️ UI GUIDE – LATUCOLLECT (V2)

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
- 🔄 Évolution UI prévue (v0.18.0 — thèmes & audit UX)

---

# 2. 🎯 OBJECTIF

Créer une interface :

- Simple
- Lisible
- Rapide à comprendre
- Prévisible

👉 Sans complexité inutile

---

# 3. 🧠 CONCEPT GLOBAL

```text
Importer → Sélectionner → Aperçu → Exporter
```

👉 Toute l’interface repose sur ce flux

---

# 4. 🧩 STRUCTURE PRINCIPALE

```text
Gauche → Projet

Centre → Options

Droite → Aperçu

Bas → Actions
```

👉 Structure FIXE (ne doit jamais être modifiée)

---

# 5. 💬 FEEDBACK UTILISATEUR

## 5.1 ⚙️ Rôle

Informer sans bloquer l’utilisateur

---

## 5.2 📋 Exemples

- ✔ Export réussi
- ❌ Erreur export
- ⚠ Aucun fichier sélectionné
- ✔ Contenu copié

---

## 5.3 🔄 Comportement

- Non bloquant
- Temporaire
- Visible dans l’aperçu

---

# 6. 🟦 ZONE GAUCHE — PROJET

## ⚙️ Rôle

Afficher la structure du projet

---

## 📋 Contenu

- Bouton charger un dossier
- Barre de recherche
- Arborescence
- Checkboxes

---

## 🔄 Comportement

- Chargement asynchrone
- Navigation fluide
- Filtrage dynamique

---

## 🔍 Recherche

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
- Conservation expansion après recherche
- Débounce anti-spam

---

## ❌ Aucun résultat

👉 Message affiché :

"Aucun résultat"

---

## ☑️ Sélection

- Multi-sélection
- Synchronisation simplifiée parent ↔ enfants
- Mise à jour immédiate du preview
- Sélection simplifiée et prévisible
- Comportement de sélection simple et prévisible

---

## ⚠️ Cas limites

- Dossier invalide → ignoré
- Accès refusé → ignoré

---

# 7. 🟨 ZONE CENTRE — OPTIONS

## ⚙️ Rôle

Configurer et interagir

---

## 📋 Contenu

- Format (TXT / Markdown)
- Bouton Copier
- Bouton Ouvrir dans l’explorateur

---

## 🚪 Accès

- Paramètres
- Statistiques
- Aide
- À propos
- Quitter

---

## 👨🏻‍💻 Mode développeur

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

## 📂 Ouvrir dans l’explorateur

- Ouvre le dossier courant
- Vérification dossier valide
- Feedback utilisateur en cas d’erreur

---

# 8. 🟩 ZONE DROITE — APERÇU

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
- Debounce preview async
- Invalidation previews obsolètes
- Génération preview découplée de la sélection

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

## ⚠️ Preview limité

Dans certains très gros projets :

- le preview peut être volontairement tronqué
- l’export complet reste conservé
- les statistiques restent calculées sur tous les fichiers sélectionnés

👉 Objectif :

- éviter les freezes UI
- limiter l’utilisation mémoire
- conserver une interface fluide

---

### ⚠️ Important

Dans ces cas spécifiques :

Preview ≠ Export

uniquement pour l’affichage.

Le contenu exporté reste complet.

---

## 🔁 Cohérence

👉 Aperçu = Export dans le fonctionnement standard

---

# 9. 🔻 ZONE BASSE — ACTIONS

## ⚙️ Rôle

Actions finales

---

## 📋 Contenu

- Exporter
- Logs

---

## 🔄 Comportement

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

# 10. 🖥️ STABILITÉ UI

## 📏 Fenêtre

- Minimum : 1600 x 1000

---

## 📐 Redimensionnement

- UI toujours lisible
- Aucun flickering

---

# 11. ⚡ STABILITÉ UI / PERFORMANCE

## 📋 Règles importantes

- Aucun reload complet inutile
- Aucun blocage UI volontaire
- Aucun recalcul massif inutile
- Preview synchronisé avec la sélection
- Aperçu toujours cohérent avec l’export

---

## 🚀 Optimisations actuelles

### 🌳 TreeView

- Mise à jour ciblée TreeView
- Conservation de l’arbre réel (pas de duplication)
- Filtrage basé visibilité (`IsVisible`)
- Mise à jour ciblée sans reload complet
- Conservation état ouvert TreeView

### 🖥️ Interface utilisateur

- Chargement progressif UI
- Yield UI pendant construction TreeView
- Préservation fluidité pendant imports volumineux

### ⚡ Preview

- Protection anti multi-refresh
- Protection anti multi-clic
- Réduction recalculs preview

### 📂 Exclusions

- Réduction rebuild complets ListView exclusions

---

## 🔄 Évolution future — Stabilisation async UI

### 🎯 Objectif

Le projet évolue progressivement vers une gestion async UI plus robuste afin de :

- réduire les race conditions
- améliorer la stabilité du TreeView
- limiter les doubles refresh preview
- améliorer la testabilité

---

### ✅ Déjà réalisé

- ✔ Stabilisation preview async réalisée en v0.14.0

---

### 🟡 Zones concernées

- sélection TreeView
- refresh preview
- recherche dynamique
- interactions rapides utilisateur

---

### 🎯 Résultat attendu

- UI plus prévisible
- interactions plus fiables
- réduction des comportements aléatoires

---

# 12. 🧠 RÈGLE ALC

- UI = affichage uniquement
- Aucune logique métier

---

# 13. 🔮 ÉVOLUTION FUTURE — SPLIT MAINVIEWMODEL

## 🎯 Objectif

Le `MainViewModel` sera progressivement séparé en plusieurs ViewModels spécialisés afin de :

- réduire les responsabilités centralisées
- améliorer la maintenabilité
- réduire les effets de bord
- améliorer la stabilité async

---

## 📊 État actuel du découpage

### ✅ LogsViewModel

Extraction réalisée en v0.15.0

### ✅ TreeViewViewModel

Première extraction réalisée en v0.15.0

### ✅ SettingsViewModel

Créé et intégré en v0.15.0

### 🟡 Découpage restant

- `PreviewViewModel`

### ⬜ Découpage prévu ultérieurement

- `ExportViewModel`

---

## ⚠️ Important

Cette évolution restera progressive afin de préserver :

- la stabilité UI
- les bindings
- la cohérence du pipeline

---

# 14. 🧠 RÈGLES UX

- Une action = un rôle
- Pas de surcharge
- Feedback immédiat
- Aucun recalcul inutile

---

# 15. ⚠️ INTERDIT

- Logique métier dans UI
- Multiples écrans
- Actions cachées

---

# 16. 🎨 ÉVOLUTION UI (v0.18.0)

## 🎯 Objectif

Améliorer l’interface sans changer son fonctionnement

---

## 🧭 Axes d’amélioration

- Thèmes (clair / sombre)
- Couleurs centralisées
- Typographie améliorée
- Alignements propres
- Icônes homogènes

---

## 🔍 Audit UX/UI

### ⚙️ Fonctionnement

- Choix guidé
- Comparaison visuelle
- Décisions utilisateur

---

## 🎨 Sujets concernés

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

# 17. 📌 ÉTAT ACTUEL

## 🏗️ Architecture

- ✔ Première étape du split MainViewModel
- ✔ Extraction LogsViewModel
- ✔ Extraction TreeViewViewModel
- ✔ Préparation SettingsViewModel
- ✔ Préparation PreviewViewModel

---

## 🖥️ Interface utilisateur

- ✔ Structure stable
- ✔ UI fluide
- ✔ Chargement progressif UI
- ✔ Réduction des refresh inutiles

---

## 🌳 TreeView

- ✔ Persistance expansion TreeView
- ✔ Exclusions UI stabilisées

---

## 🔄 Pipeline

- ✔ Pipeline respecté
- ✔ Preview = Export
- ✔ Pipeline preview async stabilisé

---

## 🕘 Historique majeur

- ✔ Système de simulation supprimé (v0.13.0)

---

# 18. 🎯 OBJECTIF FINAL

Une interface :

- Intuitive
- Rapide
- Claire
- Prévisible

---

## 🧭 Résultat recherché

👉 L’utilisateur comprend immédiatement quoi faire
