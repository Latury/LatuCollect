# 🗺️ FEUILLE DE ROUTE – LATUCOLLECT

Application de collecte de contenu multi-fichiers

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🎯 1. Vision Générale

Construire une application WinUI 3 (.NET 8) permettant :

- ✅ Charger un projet
- ✅ Naviguer dans sa structure
- ✅ Sélectionner des fichiers
- ✅ Générer un aperçu en temps réel
- ✅ Copier le contenu généré
- ✅ Exporter un document structuré

👉 LatuCollect est un outil simple, visuel et efficace

---

# 🧠 2. Fonctionnement

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🧭 3. Philosophie

- ✅ Simplicité
- ✅ Lisibilité
- ✅ Rapidité
- ✅ Aucun élément inutile

---

# ⚠️ RÈGLE CRITIQUE

👉 Toute nouvelle fonctionnalité non prévue doit être ajoutée immédiatement dans la version en cours

---

# 🧠 PRIORITÉ DE DÉVELOPPEMENT

👉 Core → Stabilité → UX → UI

---

# 🧩 4. Concepts clés

## 🌳 Arborescence projet (CŒUR DE L’APP)

- Navigation dans dossiers
- Affichage des fichiers
- Sélection via checkbox
- Filtrage via recherche

---

## 👁️ Aperçu temps réel

- Affichage du document final
- Mise à jour automatique
- Gestion des états (vide / contenu)

---

## 📄 Format d’export

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

---

## ⚙️ Modes utilisateur

- Mode simple → utilisation directe
- Mode expert → options avancées (futur)

---

# 🚀 5. Version 0.1.0 — CORE (TERMINÉ)

- ✅ Import fichiers / dossiers
- ✅ Lecture + assemblage
- ✅ Export TXT / Markdown

---

# 🎨 6. Version 0.2.0 — UI PRINCIPALE (TERMINÉ)

## 🎯 Objectif

Implémenter l’interface complète

---

## 🧩 Fonctionnalités

- ✅ Arborescence projet dynamique
- ✅ Navigation dans les dossiers
- ✅ Sélection via checkbox
- ✅ Barre de recherche
- ✅ Filtrage dynamique
- ✅ Aperçu en temps réel
- ✅ Gestion des états (aucun fichier sélectionné)
- ✅ Copie du contenu
- ✅ Export TXT / Markdown
- ✅ Dialogs (Options, Aide, À propos, Quitter)

---

## ✅ Résultat

- ✅ Interface complète fonctionnelle
- ✅ Arborescence dynamique opérationnelle
- ✅ Sélection utilisateur active
- ✅ Aperçu temps réel connecté au Core
- ✅ Export réel fonctionnel

👉 MVP VALIDÉ

---

### 🖥️ Interface

- ✅ Structure en 4 zones (gauche / centre / droite / bas)
- ✅ Arborescence projet
- ✅ Navigation dossiers
- ✅ Checkbox sélection
- ✅ Barre de recherche

---

### 👁️ Aperçu

- ✅ Génération en temps réel
- ✅ Affichage format final
- ✅ Séparateur entre fichiers
- ✅ Message si aucun fichier sélectionné

---

### 📤 Export

- ✅ Bouton export
- ✅ Désactivé si aucun contenu
- ✅ TXT / Markdown
- ✅ Confirmation utilisateur
- ✅ Format dépend du choix utilisateur (.txt / .md)

---

### 📋 Copier

- ✅ Copie du contenu
- ✅ Désactivé si aucun contenu
- ✅ Feedback utilisateur

---

# 🧪 7. Version 0.3.0 — SIMULATION (BASE DEV)

## 🎯 Objectif

Permettre de tester tous les cas (erreurs, gros projets, comportements) sans impacter l’application réelle.

---

## 🔧 Système de simulation

- ✅ Création dossier Simulation
- ✅ Activation / désactivation via booléen (true / false)
- ✅ Intégration dans le flux (lecture / export / UI)

---

## 📂 Cas simulés

- ✅ Fichiers volumineux
- ✅ Fichiers vides
- ✅ Chemins longs
- ✅ Erreurs lecture
- ✅ Erreurs export

---

## 🖥️ Interface simulation (mode développeur)

- ✅ Option avancée pour activer le mode simulation
- ✅ Affichage d’un bouton 🧪 dans la barre du bas
- ✅ Indicateur visuel du mode simulation actif
- ✅ Ouverture d’un popup de configuration
- ✅ Sélection du scénario directement depuis l’application
- ✅ Synchronisation UI ↔ SimulationConfig

---

## 🧠 Règles

- ✅ Aucun impact en production
- ✅ Code séparé du Core réel
- ✅ Activation simple (true / false)

---

# 🧱 8. Version 0.4.0 — STABILITÉ

## 🎯 Objectif

Rendre l’application fiable dans tous les cas.

---

## 🛡️ Gestion des erreurs

- ⬜ Gestion erreurs lecture fichier
- ⬜ Gestion erreurs export
- ⬜ Gestion accès refusé
- ⬜ Gestion fichiers invalides / corrompus
- ⬜ Gestion chemins longs

---

## ✔ Validation

- ⬜ Vérification stricte : aperçu = export

---

# 🧠 9. Version 0.5.0 — UX (EXPÉRIENCE UTILISATEUR)

## 🎯 Objectif

Rendre l’application claire, compréhensible et agréable.

---

## 💬 Feedback utilisateur

- ⬜ Messages clairs (erreurs / succès)
- ⬜ Feedback visuel (copie / export)
- ⬜ Gestion des actions annulées

---

## 🔄 États UI

- ⬜ Gestion états globaux (chargement / prêt / erreur)
- ⬜ Gestion du message "Aucun fichier sélectionné"

---

## 🖥️ Interface

- ⬜ Boutons plus compréhensibles
- ⬜ Uniformisation des dialogs
- ⬜ Bouton "Tout sélectionner / Tout désélectionner"

---

## ⏳ Chargement

- ⬜ Indicateur de chargement
- ⬜ Gestion gros projets (pas de freeze)

---

## 📄 Affichage

- ⬜ Amélioration lisibilité aperçu
- ⬜ Rendu type code
- ⬜ Scroll fluide sur gros contenu

---

# 🔍 10. Version 0.6.0 — RECHERCHE & FICHIERS

## 🎯 Objectif

Améliorer la gestion des fichiers et la navigation.

---

- ⬜ Recherche fiable (zéro bug)
- ⬜ Gestion "aucun résultat"
- ⬜ Performance sur gros projets
- ⬜ Exclusion dossiers (bin, obj)
- ⬜ Filtrage simple des fichiers

---

# 📦 11. Version 0.7.0 — EXPORT & STATISTIQUES

## 🎯 Objectif

Améliorer le contenu généré et ajouter des infos utiles.

---

## 📤 Export

- ⬜ Amélioration format Markdown
- ⬜ Amélioration lisibilité export
- ⬜ Nettoyage structure export
- ⬜ Gestion améliorée des erreurs export

---

## 📊 Statistiques

- ⬜ Nombre de fichiers sélectionnés
- ⬜ Taille totale (approx)
- ⬜ Nombre de lignes total
- ⬜ Nombre de caractères

---

# ⚙️ 12. Version 0.8.0 — ARCHITECTURE

## 🎯 Objectif

Rendre le code propre et maintenable.

---

- ⬜ Sortir BuildExportContent du ViewModel
- ⬜ Déplacer logique vers Core
- ⬜ Respect du pipeline complet
- ⬜ Nettoyage ViewModel
- ⬜ Structuration services

---

# 🚀 13. Version 0.9.0 — OPTIMISATION

## 🎯 Objectif

Améliorer les performances globales.

---

- ⬜ Optimisation lecture fichiers
- ⬜ Optimisation mémoire
- ⬜ Réduction ralentissements UI
- ⬜ Amélioration vitesse globale

---

# 🎨 14. Version 0.10.0 — FINALISATION PRODUIT

## 🎯 Objectif

Préparer l’application pour une distribution réelle.

---

## 📦 Distribution

- ⬜ Création installateur
- ⬜ Gestion dépendances
- ⬜ Multi-architecture (x64 / x86)
- ⬜ Choix dossier installation
- ⬜ Création raccourcis

---

## 🧾 Application

- ⬜ Version affichée (À propos)
- ⬜ Licence visible
- ⬜ Informations application

---

## 🖼️ Identité visuelle

- ⬜ Création logo application
- ⬜ Icône .ico (exe + Windows)
- ⬜ Icône barre des tâches

---

## ⚙️ Configuration utilisateur

- ⬜ Sauvegarde préférences
- ⬜ Format par défaut
- ⬜ Thème sélectionné

---

## 🌗 Thèmes

- ⬜ Mode sombre
- ⬜ Mode clair
- ⬜ Système de couleurs centralisé

---

## 🎨 Refonte UI

- ⬜ Modernisation design
- ⬜ Amélioration couleurs
- ⬜ Amélioration lisibilité
- ⬜ Cohérence visuelle

---

## 🔄 Mise à jour

- ⬜ Vérification version
- ⬜ Notification utilisateur

---

## ⚙️ Build

- ⬜ Build release propre
- ⬜ Optimisation finale
