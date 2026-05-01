# 🗺️ ROADMAP – LATUCOLLECT

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
👉 basé sur un principe de copie intelligente

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
- ✅ Activation simple (true / false via configuration)

---

# 🧱 8. Version 0.4.0 — STABILITÉ

## 🎯 Objectif

Rendre l’application fiable dans tous les cas.

---

## 🛡️ Gestion des erreurs

- ✅ Gestion erreurs lecture fichier
- ✅ Gestion erreurs export
- ✅ Gestion chemins longs

---

## ✔ Validation

- ✅ Vérification stricte : aperçu = export

---

# 🧠 9. Version 0.5.0 — UX (EXPÉRIENCE UTILISATEUR)

## 🎯 Objectif

Rendre l’application claire, compréhensible et agréable.

---

## 💬 Feedback utilisateur

- ✅ Messages clairs (erreurs / succès)
- ✅ Feedback visuel (copie / export)
- ✅ Gestion des actions annulées
- ✅ Affichage responsive (retour à la ligne, pas de texte coupé)

---

## 🔄 États UI

- ✅ Gestion états globaux (chargement / prêt / erreur)
- ✅ Affichage conditionnel (loader / contenu / erreur)
- ✅ Gestion du message "Aucun fichier sélectionné"

---

## 🖥️ Interface

- ✅ Boutons plus compréhensibles
- ✅ Uniformisation des dialogs
- ✅ Bouton "Tout sélectionner / Tout désélectionner"
- ✅ Amélioration du layout (Grid propre, sans chevauchement)

---

## ⏳ Chargement

- ✅ Indicateur de chargement (loader)
- ✅ Chargement asynchrone (Task.Run)
- ✅ UI non bloquée
- ✅ Gestion gros projets (pas de freeze)

---

## 📄 Affichage

- ✅ Amélioration lisibilité aperçu
- ✅ Rendu type code (police monospace)
- ✅ Scroll fluide sur gros contenu
- ✅ Mise à jour en temps réel

---

## ⚡ Performance

- ✅ Limitation du nombre de fichiers (MAX_NODES)
- ✅ Limitation de la profondeur (MAX_DEPTH)
- ✅ Protection contre les traitements lourds
- ✅ Chargement partiel contrôlé

---

## ⚠️ Gestion des gros projets

- ✅ Détection automatique des projets volumineux
- ✅ Affichage d’un message utilisateur :
  → "⚠ Projet volumineux — affichage partiel"
- ✅ Comportement expliqué (évite confusion)

---

## 🔎 Recherche

- ✅ Barre de recherche avec bouton toggle
- ✅ Affichage dynamique (ouverture / fermeture)
- ✅ Filtrage en temps réel
- ✅ Recherche insensible à la casse
- ✅ Conservation des dossiers parents si correspondance enfant
- ✅ Aucun rechargement de l’arbre (UI stable)

---

# 🔍 10. Version 0.6.0 — RECHERCHE & FICHIERS

## 🎯 Objectif

Améliorer la gestion des fichiers et la navigation.

---

- ✅ Recherche fiable (zéro bug)
- ✅ Gestion "aucun résultat"
- ✅ Performance sur gros projets
- ✅ Exclusion dossiers (bin, obj)
- ✅ Filtrage simple des fichiers
- ✅ Optimisation de la recherche (debounce)

---

# 📦 11. Version 0.7.0 — EXPORT & STATISTIQUES

## 🎯 Objectif

Améliorer le contenu généré et ajouter des infos utiles.

---

## 📤 Export

- ✅ Amélioration format Markdown
- ✅ Amélioration lisibilité export
- ✅ Nettoyage structure export
- ✅ Gestion améliorée des erreurs export

---

## 📊 Statistiques

- ✅ Nombre de fichiers sélectionnés
- ✅ Taille totale (approx)
- ✅ Nombre de lignes total
- ✅ Nombre de caractères

---

# ⚙️ 12. Version 0.8.0 — ARCHITECTURE

## 🎯 Objectif

Rendre le code propre et maintenable.

---

- ✅ Identifier toute logique métier restante dans le ViewModel
- ✅ Déplacer la logique métier restante vers des services Core
- ✅ Simplifier le ViewModel pour qu’il ne fasse que :
  → gérer l’état UI
  → appeler le Core
- ✅ Vérifier que le ViewModel ne contient aucune logique métier complexe
- ✅ Respect du pipeline complet
- ✅ Nettoyage ViewModel
- ✅ Structuration services

---

## 🧠 Résultat

- ✔ Architecture propre et lisible
- ✔ Séparation UI / Core respectée
- ✔ Services clarifiés (Import / Reader / Collection / Export)
- ✔ ViewModel simplifié (orchestrateur uniquement)
- ✔ Pipeline cohérent et centralisé

---

## 🏁 Objectif atteint

👉 LatuCollect devient :

- ✔ Maintenable
- ✔ Structuré
- ✔ Évolutif

👉 Base solide pour optimisation (0.9.0)

---

# 🚀 13. Version 0.9.0 — OPTIMISATION

## 🎯 Objectif

Améliorer les performances, la fluidité et préparer les bases d’un comportement scalable.

---

## ⚡ Performance

- ✅ Optimisation lecture fichiers (réduction I/O inutiles)
- ✅ Mise en cache des fichiers (FileReaderService)
- ✅ Optimisation mémoire (réduction allocations)
- ✅ Amélioration du temps de génération preview
- ✅ Réduction des recalculs inutiles

---

## 🖥️ UI

- ✅ Amélioration réactivité interface
- ✅ Optimisation rafraîchissement aperçu
- ✅ Réduction des appels inutiles au Core
- ✅ Gestion plus fine des états (loading / ready)

---

## 🧠 Core

- ✅ Séparation des statistiques (FileStatisticsService)
- ✅ Préparation à l’extension du système de statistiques
- ✅ Externalisation du calcul des statistiques
- ✅ Réduction responsabilité FileExportService
- ✅ Amélioration pipeline interne

---

## 🔄 Pipeline

- ✅ Vérification complète du flux :
  Import → Lecture → Collection → Assemblage → Statistiques → Export
- ✅ Optimisation du flux Preview → Export (source unique maintenue)
- ✅ Suppression des doublons internes
- ✅ Mise en place de caches (lecture + preview)

---

## 🧪 Simulation (REPORTÉ)

⚠️ Décidé :

- ❌ Pas dans cette version
- ✔ À traiter dans une discussion dédiée

Objectif futur :

- Structurer simulation (UI + Core)
- Ajouter scénarios complets (erreurs, UI, performance)

---

## 🏁 Objectif final

- ✔ Application plus rapide
- ✔ UI plus fluide
- ✔ Code plus modulaire et maintenable

---

# 🎨 14. Version 0.10.0 — FINALISATION PRODUIT

## 🎯 Objectif

Finaliser l’application pour une utilisation réelle, stable, testée et distribuable.

---

# ⚙️ CONFIGURATION UTILISATEUR (PRIORITÉ HAUTE)

- ✅ Sauvegarde des préférences utilisateur (config.json)
- ✅ Format par défaut (.txt / .md)
- ✅ Gestion des exclusions persistées
- ✅ Thème sélectionné
- ✅ Activation mode développeur
- ✅ Sauvegarde du dernier dossier ouvert
- ✅ Chargement automatique au démarrage
- ✅ Option pour désactiver le chargement automatique
- ✅ Vérification validité du dossier au lancement
- ✅ Bouton "Réinitialiser les paramètres"

## 🔹 Export

- ✅ Mode d’export (Normal / Compatible IA)
- ✅ Persistance du mode dans config.json

---

# 🧠 CORE — STABILITÉ (PRIORITÉ CRITIQUE)

## 🔹 FileReaderService

- ✅ Async (`ReadFileAsync`)
- ✅ `FileReadResult` (résultat structuré)

## 🔹 FileStatisticsService

- ✅ Suppression `FileInfo`
- ✅ Passage `fileSize` en paramètre

## 🔹 ImportService

- ✅ `EnumerateFiles`
- ✅ Tri fichiers + dossiers
- ✅ Support CancellationToken
- ✅ ImportResult
- ✅ Gestion affichage partiel

## 🔹 ExportService

- ✅ Async export
- ✅ Gestion gros fichiers
- ✅ ExportResult structuré
- ✅ Support du mode d’export (Normal / IA)

## 🔹 CollectionService

- ✅ Tri fichiers
- ✅ HashSet (doublons)
- ✅ Limite max fichiers

---

# ⚡ PERFORMANCE

- ✅ Limite cache
- ✅ Expiration cache
- ✅ Optimisation stats
- ✅ Limite export

---

# 🖥️ UI / UX

## 🔧 Bugs

- ✅ Reset paramètres
- ✅ Message "Veuillez sélectionner un dossier"
- ✅ Supprimer sélection globale

## ✨ Améliorations

- ✅ Sélection dossier → inclure enfants
- ✅ Preview limité cohérent
- ✅ Message export partiel
- ✅ Preview = Export (corrigé)

---

# ⚠️ STABILITÉ UI

- ✅ Taille minimale
- ✅ Réduction flickering
- ✅ Optimisation rendu

---

# 🧾 LOGS

- ✅ LogService en place
- ✅ Niveau de log configurable

---

# 🧪 TESTS

- ✅ Tests FileReader
- ✅ Tests Export
- ✅ Tests Statistics

---

# 🚀 15. Version 0.11.0 — STABILITÉ CORE

## 🎯 Objectif

Rendre le Core prévisible, testable et sans effets de bord.

---

## 🐞 Bugs critiques (PRIORITÉ HAUTE)

- ⬜ Correction reset incomplet
- ⬜ Correction sélection TreeView :
  - décocher un fichier ne doit pas recocher le parent
  - propagation correcte parent ↔ enfants

---

## 🧠 FileReader

- ⬜ Gestion erreurs complète (cas extrêmes manquants)
- ⬜ Validation fichiers (null, accès, encodage)

---

## 📊 Statistics

- ⬜ Suppression dépendance FileInfo
- ⬜ Utilisation fileSize uniquement
- ⬜ Fiabilisation calcul

---

## 📤 Export

- ⬜ Validation stricte des entrées
- ⬜ Robustesse export massif

---

## 📦 Collection

- ⬜ Gestion cas limites
- ⬜ Tri stable
- ⬜ Limite sécurité

# 🚀 16. Version 0.12.0 — PERFORMANCE

## 🎯 Objectif

Améliorer la gestion des gros projets.

---

## 📂 Import

- ⬜ EnumerateFiles (streaming)
- ⬜ Tri optimisé

---

## ⚡ Cache

- ⬜ Limite mémoire
- ⬜ Expiration cache

---

## 📊 Stats

- ⬜ Optimisation calcul lignes

---

## 📤 Export

- ⬜ Gestion export massif
- ⬜ Protection mémoire

# 🚀 17. Version 0.13.0 — UX & COMPORTEMENT

## 🎯 Objectif

Améliorer l’expérience utilisateur.

---

## 📂 Import

- ⬜ Lazy loading
- ⬜ CancellationToken
- ⬜ ImportResult
- ⬜ Affichage partiel

---

## 🖥️ UI

- ⬜ Reset dossier propre
- ⬜ Message état vide

---

## ✨ UX

- ⬜ Sélection dossier fiable
- ⬜ Preview limité configurable
- ⬜ Message export partiel

# 🚀 18. Version 0.14.0 — ARCHITECTURE

## 🎯 Objectif

Corriger les écarts ALC restants.

---

## 🧠 Core

- ⬜ Interfaces services
- ⬜ Séparer AppConfig / UserConfig

---

## 📦 Modèles

- ⬜ Déplacer :
  - ExportResult
  - ExportData
  - StatisticsResult
    👉 vers Core/Models/Export/

---

## 🧱 FileNode

- ⬜ Supprimer IsSelected du Core
- ⬜ Ajouter IsFolder réel

---

## 🧾 Logging

- ⬜ Déplacer formatage Date côté UI

# 🚀 19. Version 0.15.0 — LOGS

## 🎯 Objectif

Améliorer debug et stabilité.

---

## 🧾 Logs

- ⬜ Limite mémoire
- ⬜ Thread safety
- ⬜ Export logs
- ⬜ Niveaux de logs
- ⬜ Filtrage

# 🚀 20. Version 0.16.0 — SUPPRESSION SIMULATION

## 🎯 Objectif

Supprimer complètement le système de simulation.

---

## 🧠 Core

- ⬜ Supprimer dossier Simulation/
- ⬜ Supprimer SimulationService
- ⬜ Supprimer tous les scénarios
- ⬜ Nettoyer dépendances dans :
  - FileReaderService
  - FileExportService

---

## 🖥️ UI

- ⬜ Supprimer bouton Simulation
- ⬜ Supprimer logique ViewModel liée
- ⬜ Supprimer activation mode simulation

---

## ⚙️ Configuration

- ⬜ Supprimer SimulationConfig
- ⬜ Nettoyer config utilisateur

---

## 🧹 Nettoyage global

- ⬜ Supprimer code mort
- ⬜ Supprimer flags inutiles
- ⬜ Vérifier qu’aucune référence ne reste

---

## 🧪 Validation

- ⬜ Vérifier :
  - lecture OK
  - export OK
  - aucun comportement cassé

# 🎨 🚀 21. Version 0.17.0 — UI / THÈMES / AUDIT COMPLET

## 🎯 Objectif

Transformer l’application en produit propre, agréable et cohérent visuellement.

👉 Sans casser :

- simplicité
- lisibilité
- structure actuelle (gauche / centre / droite / bas)

---

## 🧱 1. BASE THÈME (OBLIGATOIRE)

- ⬜ Mode clair / sombre
- ⬜ Centralisation couleurs (DynamicResource)
- ⬜ Suppression couleurs en dur
- ⬜ Palette cohérente :
  - primaire
  - secondaire
  - erreur
  - succès

---

## 🎨 2. AMÉLIORATION VISUELLE

- ⬜ Espacements homogènes
- ⬜ Alignements propres
- ⬜ Hiérarchie visuelle claire
- ⬜ Amélioration typographie
- ⬜ Uniformisation des icônes

---

## 🧩 3. COHÉRENCE UI

- ⬜ Harmonisation boutons
- ⬜ Harmonisation dialogs
- ⬜ Harmonisation états :
  - loading
  - error
  - empty
- ⬜ Feedback utilisateur plus clair

---

## 🖥️ 4. ZONES UI (IMPORTANT)

👉 On garde STRICTEMENT :

- Gauche → Arborescence
- Centre → Options
- Droite → Aperçu
- Bas → Actions

---

👉 Améliorations :

- ⬜ Lisibilité zone gauche (cœur app)
- ⬜ Clarté des actions centre
- ⬜ Confort lecture preview
- ⬜ Visibilité actions bas

---

## 🔍 5. AUDIT UX/UI GUIDÉ (TRÈS IMPORTANT)

### Objectif

- choix guidé
- visuel obligatoire
- décisions UX claires

---

## 🧠 FORMAT DE L’AUDIT

### 1️⃣ Question claire

Ex :
👉 “Quel style de boutons veux-tu ?”

---

### 2️⃣ 5 solutions minimum

- Style plat
- Style arrondi
- Style outline
- Style glass
- Style minimal

---

### 3️⃣ Analyse par solution

- ✔ explication simple
- ✔ avantages
- ✔ inconvénients
- ✔ cas d’usage

---

### 4️⃣ VISUELS

- minimum 5 images par solution
- comparables
- claires
- réalistes

👉 Objectif : choix visuel

---

## 🎯 SUJETS AUDITÉS

### 🎨 Thème global

- sombre / clair / hybride
- contraste fort vs doux

### 🔘 Boutons

- formes
- styles
- hover
- feedback

### 📦 Cartes / blocs UI

- plats
- ombrés
- bordures
- séparation

### 🌳 Arborescence

- indentation
- lignes
- icônes
- sélection

### 👁️ Preview

- fond
- police
- contraste
- espacement

### 📊 Feedback utilisateur

- toast
- inline
- badges
- couleurs

### 🧾 Logs UI

- couleurs
- lisibilité
- hiérarchie

### ⚙️ Paramètres

- navigation
- layout gauche/droite
- lisibilité

### 🎨 Couleurs

- palette
- accent
- danger / warning

### 🔤 Typographie

- police
- taille
- espacement

---

## ⚠️ RÈGLES

- ✔ simple
- ✔ lisible
- ✔ ALC respectée
- ✔ utile uniquement

# 🚀 22. Version 0.18.0 — FINALISATION & DISTRIBUTION

## 🎯 Objectif

Transformer l’application en produit final.

---

## ⚙️ BUILD

- ⬜ Build release propre
- ⬜ Tests complets
- ⬜ Vérification stabilité
- ⬜ Installateur
- ⬜ Dépendances
- ⬜ Multi-architecture
- ⬜ Choix dossier
- ⬜ Raccourcis

# 🏁 OBJECTIF FINAL

👉 LatuCollect devient :

- ✔ Stable
- ✔ Rapide
- ✔ Prévisible
- ✔ Lisible
- ✔ Agréable visuellement
- ✔ Distribuable
