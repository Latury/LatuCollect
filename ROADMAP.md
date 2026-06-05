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

# 🚀 15. Version 0.11.0 — STABILITÉ CORE + UX

## 🎯 Objectif

Rendre le Core prévisible, testable et sans effets de bord
ET améliorer l’expérience utilisateur (TreeView + exclusions)

---

# 🐞 Bugs critiques (PRIORITÉ HAUTE)

- ✅ Correction sélection TreeView :
  - ✅ propagation correcte parent ↔ enfants
  - ✅ décocher un fichier ne recoche plus le parent
  - ✅ support tri-state checkbox
  - ✅ comportement type Windows Explorer
  - ✅ état partiel cohérent

---

# 🌳 TreeView / Sélection (STABILISÉ MAJORITAIREMENT)

- ✅ Correction complète sélection parent / enfants
- ✅ Synchronisation UI ↔ ViewModel
- ✅ Suppression désynchronisations sélection
- ✅ Correction preview incohérent
- ✅ Protection anti multi-clic
- ✅ Stabilisation HandleNodeClick
- ✅ Arbre réel conservé (plus de duplication)
- ✅ Ajout état partiel (`bool?`)
- ✅ Synchronisation explicite CheckBox → ViewModel
- ✅ Correction rebond WinUI CheckBox
- ✅ Preview synchronisé après sélection
- ✅ Ajout tests sélection TreeView
- ✅ Tests propagation parent ↔ enfants
- ✅ Tests état partiel tri-state
- ✅ Validation états export ViewModel
- ✅ Validation états UI ViewModel

⚠️ Important

Le système tri-state introduit en v0.11.0
a ensuite été volontairement supprimé et simplifié en v0.12.0
afin de :

- réduire la complexité WinUI
- limiter les effets de bord
- améliorer la stabilité UI
- simplifier les interactions async
- rendre le comportement TreeView plus prévisible

---

# 🧠 FileReader

- ✅ Validation chemins null / vides
- ✅ Gestion fichier introuvable
- ✅ Gestion dossier introuvable
- ✅ Gestion chemin trop long
- ✅ Gestion accès refusé
- ✅ Gestion erreurs IO
- ✅ Gestion cache lecture
- ✅ Ajout RemoveFromCache()
- ✅ Ajout ClearCache()
- ✅ Gestion gros fichiers
- ✅ Lecture partielle fichiers volumineux
- ✅ Tests cache
- ✅ Tests gros fichiers
- ✅ Tests suppression fichier
- ✅ Tests remove cache
- ✅ Tests clear cache

---

# ✨ UX / TreeView (DÉJÀ FAIT)

- ✅ Ajout menu clic droit (exclusion)
- ✅ Exclusion directe depuis l’arborescence
- ✅ Exclusion protégée
- ✅ Suppression node sans reload complet
- ✅ Ajout IsExpanded (persistance visuelle)
- ✅ RemoveNodeFromTree()
- ✅ UI beaucoup plus fluide
- ✅ Option "Inclure"
- ✅ Option "Copier le chemin"
- ✅ Désactivation "Exclure" si déjà exclu

---

# 🎯 Système d’exclusion (MAJORITAIREMENT STABILISÉ)

## ✅ Déjà corrigé

- ✅ Passage nom → chemin complet
- ✅ Adaptation FileImportService
- ✅ Adaptation sauvegarde config
- ✅ Compatibilité ancien format conservée
- ✅ Support exclusions fichiers
- ✅ Support exclusions dossiers
- ✅ Distinction réelle fichier / dossier
- ✅ Correction bug :
  - "bin" excluait partout

- ✅ Ajout exclusions protégées
- ✅ Synchronisation config ↔ UI
- ✅ Suppression immédiate arbre
- ✅ Rechargement exclusions cohérent
- ✅ Tests exclusion fichier simple
- ✅ Tests exclusion dossier avec enfants
- ✅ Tests persistance config

---

# 🧾 Menu clic droit

- ✅ Ajouter option "Inclure"
- ✅ Ajouter "Copier le chemin"
- ✅ Désactiver "Exclure" si déjà exclu

---

# ⚡ Performance / refresh

- ✅ Suppression reload complet majeur
- ✅ Mise à jour ciblée arbre
- ✅ Protection anti multi-refresh
- ✅ Protection anti double preview
- ✅ Limitation preview volumineux
- ✅ Optimisation signature sélection
- ✅ Réduction recalculs inutiles

---

# 🔍 Recherche (TreeView)

## ✅ Déjà stabilisé

- ✅ Filtrage fonctionnel
- ✅ Filtrage basé visibilité (`IsVisible`)
- ✅ Recherche instantanée pendant la frappe
- ✅ Ajout `UpdateSourceTrigger=PropertyChanged`
- ✅ Suppression des espaces vides TreeView
- ✅ Ajout `VisibleChildren`
- ✅ Filtrage UI propre sans duplication
- ✅ Conservation de l’arbre réel
- ✅ Compatibilité expansion automatique
- ✅ Navigation TreeView conservée
- ✅ Plus de duplication arbre
- ✅ Ajout tests recherche TreeView
- ✅ Validation visibilité nodes
- ✅ Validation expansion automatique
- ✅ Validation `VisibleChildren`
- ✅ Reset visibilité après recherche
- ✅ Reset expansion après recherche
- ✅ Tests suppression nodes arbre

---

# 🧪 Tests

## ✅ Déjà ajoutés

- ✅ Tests sélection TreeView
- ✅ Tests propagation parent ↔ enfants
- ✅ Tests état partiel tri-state
- ✅ Tests recherche TreeView
- ✅ Tests visibilité nodes
- ✅ Tests expansion automatique
- ✅ Tests reset visibilité
- ✅ Tests reset expansion
- ✅ Tests suppression nodes
- ✅ Tests exclusions
- ✅ Tests FileReader
- ✅ Tests Export
- ✅ Tests Collection
- ✅ Tests Statistics
- ✅ Tests états export ViewModel
- ✅ Tests états UI ViewModel

---

# 📊 Statistics

- ✅ Fiabilisation calcul lignes
- ✅ Gestion contenu null
- ✅ Gestion contenu vide
- ✅ Gestion unicode
- ✅ Gestion tailles négatives
- ✅ Ajout tests statistiques
- ✅ Validation accumulation statistiques

---

# 📤 Export

- ✅ Validation export async
- ✅ Validation export sync
- ✅ Gestion fichier manquant
- ✅ Gestion dossier manquant
- ✅ Gestion contenu vide
- ✅ Gestion collection vide
- ✅ Gestion mode IA
- ✅ Limitation nombre fichiers
- ✅ Limitation taille contenu
- ✅ Gestion export partiel
- ✅ Ajout statistiques export
- ✅ Ajout format Markdown
- ✅ Validation Preview = Export
- ✅ Tests export massif
- ✅ Tests export async
- ✅ Tests chemins invalides

---

# 📦 Collection

- ✅ Gestion roots null
- ✅ Validation fichiers sélectionnés
- ✅ Ignorer dossiers
- ✅ Suppression doublons
- ✅ Limite sécurité MAX_FILES
- ✅ Tri stable résultats
- ✅ Tests doublons
- ✅ Tests limite MAX_FILES
- ✅ Tests fichiers sélectionnés

---

# 🧠 Résultat

✔ Sélection TreeView stabilisée
✔ Tri-state fonctionnel
✔ Synchronisation parent ↔ enfants cohérente
✔ Preview synchronisé et prévisible
✔ Recherche TreeView fonctionnelle
✔ Filtrage basé visibilité stabilisé
✔ Expansion automatique fonctionnelle
✔ Exclusions dynamiques stables
✔ Suppression nodes sans reload complet
✔ Synchronisation config ↔ UI stabilisée
✔ Support exclusions fichiers + dossiers
✔ Compatibilité ancien format conservée
✔ UI globalement plus fluide
✔ Réduction importante des effets de bord
✔ Ajout massif de tests unitaires
✔ Core beaucoup plus testable
✔ Export plus robuste
✔ Collection plus sécurisée
✔ FileReader stabilisé majoritairement
✔ Statistics fiabilisé
✔ Validation états export ViewModel
✔ Validation états UI ViewModel

---

# 🏁 Objectif atteint

✔ Core plus stable
✔ Architecture plus prévisible
✔ UX TreeView largement stabilisée
✔ Synchronisation UI fiable
✔ Base solide pour les prochaines versions
✔ Meilleure robustesse générale
✔ Meilleure maintenabilité
✔ Stabilisation majeure de la 0.11.0

---

# 🚀 16. Version 0.12.0 — PERFORMANCE

## 🎯 Objectif

Améliorer la gestion des gros projets.

---

## 📂 Import

- ✅ EnumerateFiles (streaming déjà utilisé)
- ✅ Tri optimisé
- ✅ Gestion gros arbres
- ✅ Validation récursion profonde
- ✅ Vérification comportement très gros projets
- ✅ Stabilisation affichage root + dossiers principaux
- ✅ Protection affichage partiel gros projets

## ⚡ Cache

- ✅ Expiration cache
- ✅ Invalidation cache si fichier modifié (LastWriteTime)
- ✅ Optimisation anti refresh preview inutile
- ✅ Vérification cohérence Preview = Export après modification disque

## 📊 Stats

- ✅ Réduction dépendance FileInfo (usage minimal conservé pour récupération taille disque)
- ✅ Réduction dépendance FileInfo (usage minimal conservé pour récupération taille disque)
- ✅ Utilisation fileSize uniquement
- ✅ Fiabilisation cas extrêmes
- ✅ Correction stats export massif réel
- ✅ Synchronisation stats ↔ preview

## 📤 Export

- ✅ Gestion export massif
- ✅ Robustesse export massif réel
- ✅ Gestion erreurs disque
- ✅ Protection mémoire export massif
- ✅ Gestion fichiers verrouillés
- ✅ Stabilisation preview partiel gros contenu

## 🔍 Recherche / TreeView

- ✅ Simplification sélection TreeView (suppression tri-state)
- ✅ Optimisation ApplyFilterRecursive
- ✅ Ajustement debounce validé sur gros projets
- ✅ Vérification absence freeze UI
- ✅ Vérification refresh WinUI très gros projets
- ✅ Cohérence sélection ↔ visibilité
- ✅ Vérification nodes masqués + recherche
- ✅ Optimisation sélection massive
- ✅ Conservation sélection après reset filtre
- ✅ Stabilisation reset preview/statistiques
- ✅ Validation recherche + sélection massive

## 🧪 Tests & stabilité

- ✅ Ajout tests sélection ↔ visibilité
- ✅ Ajout tests debounce recherche
- ✅ Ajout tests export fichiers verrouillés
- ✅ Validation stress tests massifs
- ✅ Stabilisation pipeline preview/export
- ✅ Validation comportement gros volumes mémoire
- ✅ 100 tests automatisés validés

---

# 🚀 17. Version 0.13.0 — SUPPRESSION SIMULATION

## 🎯 Objectif

Supprimer complètement le système de simulation afin de simplifier l’architecture,
réduire les effets de bord
et préparer les futurs refactors UI/Core.

---

## 🧠 Core

- ✅ Supprimer dossier Simulation/
- ✅ Supprimer SimulationService
- ✅ Supprimer tous les scénarios
- ✅ Nettoyer dépendances dans :
  - FileReaderService
  - FileExportService

---

## 🖥️ UI

- ✅ Supprimer bouton Simulation
- ✅ Supprimer logique ViewModel liée
- ✅ Supprimer activation mode simulation
- ✅ Supprimer bindings WinUI liés à la simulation
- ✅ Supprimer dialog simulation
- ✅ Nettoyer handlers UI liés

---

## ⚙️ Configuration

- ✅ Supprimer SimulationConfig
- ✅ Vérifier absence de dépendance simulation dans UserConfig

---

## 🧹 Nettoyage global

- ✅ Supprimer code mort
- ✅ Supprimer flags inutiles
- ✅ Vérifier qu’aucune référence ne reste
- ✅ Réduire le couplage Core/UI
- ✅ Simplifier MainViewModel
- ✅ Préparer les futurs refactors ViewModels

---

## 🧪 Validation

- ✅ Aucun problème majeur détecté
- ✅ Vérifier Preview = Export
- ✅ Vérifier absence de bindings WinUI cassés
- ✅ Vérifier mode développeur toujours fonctionnel

---

# 🚀 18. Version 0.14.0 — UX & COMPORTEMENT

## 🎯 Objectif

Améliorer l’expérience utilisateur
et stabiliser les comportements UI.

---

## 📂 Import

- ✅ Lazy loading
- ✅ CancellationToken
- ✅ ImportResult
- ✅ Affichage partiel

---

## 🖥️ UI

- ✅ Reset dossier propre
- ✅ Message état vide

---

## ✨ UX

- ✅ Message export partiel
- ✅ Sélection dossier fiable
- ✅ Ajouter "Ouvrir dans l’explorateur"
- ✅ Stabilisation scroll exclusions
- ✅ Conservation état ouvert après reload complet
- ✅ Persistance complète état ouvert arbre

---

## 🌳 TreeView

- ✅ Vérifier absence de refresh inutiles
- ✅ Vérifier cohérence sélection ↔ visibilité
- ✅ Vérifier stabilité après exclusions dynamiques
- ✅ Vérifier stabilité sélection massive
- ✅ Vérifier stabilité multi-clic rapide
- ✅ Stabilisation pipeline preview async
- ✅ Gestion previews obsolètes
- ✅ Préservation fluidité UI pendant génération preview

---

## 🧪 Validation UX

- ✅ Réouverture arbre après exclusion
- ✅ Recherche après exclusion
- ✅ Vérification stabilité exclusions protégées
- ✅ Vérification synchronisation visibilité ↔ exclusions

---

# 🚀 19. Version 0.15.0 — ARCHITECTURE & SPLIT MAINVIEWMODEL

## 🎯 Objectif

Corriger les écarts ALC restants,
réduire les responsabilités du MainViewModel
et préparer les futurs refactors async/UI.

---

## 🧠 Core

- ✅ Interfaces services

- ✅ Séparation AppConfig / UserConfig

- ✅ Homogénéisation progressive FileReadResult
  - ✅ Ajout état explicite `IsPartial`
  - ✅ Détection contenu partiel réel
  - ✅ Warnings Preview implémentés

- ✅ Préparation `UserExcludedFolders`

- ✅ Sécurisation ResetAsync / LoadAsync

- ✅ Éviter écrasement exclusions utilisateur

---

## 📦 Modèles

- ✅ Déplacer :
  - ExportResult
  - ExportData
  - StatisticsResult
    👉 vers Core/Models/Export/

---

## 🧱 FileNode

- ✅ Ajouter IsFolder réel

- ✅ Audit complet IsSelected
- ✅ Maintien temporaire IsSelected justifié par ConvertToCoreNodes()
- ✅ Réévaluation suppression IsSelected reportée (v0.17.0)

- ✅ Suppression FileCollectionService
- ✅ Suppression FileCollectionServiceTests
- ✅ Audit usages restants

---

## 🧱 FileReader

- ✅ Gestion encodages invalides
- ✅ Validation fichiers verrouillés
- ✅ Détection simple fichiers binaires
- ✅ Fallback UTF8 / UTF16 sécurisé
- ✅ Gestion caractères invalides

---

## 🧾 Logging

- ✅ Déplacer formatage Date côté UI
- ✅ Extraction logique logs vers LogsViewModel

---

## 🖥️ UI — Split MainViewModel

- 🟢 Allègement du MainViewModel
  - ✅ LogsViewModel extrait
  - ✅ TreeViewViewModel extrait
  - ✅ SettingsViewModel préparé

- 🟢 Réduction du couplage UI / Pipeline
  - ✅ Extraction logique logs
  - ✅ Extraction logique TreeView
  - ✅ Préparation extraction Settings

### 🟢 LogsViewModel

- ✅ Fichier créé
- ✅ LogFilter extrait
- ✅ LogFilter déplacé vers Models
- ✅ Branché dans MainViewModel
- ✅ Filtrage déplacé
- ✅ Collection Logs déplacée
- ✅ Compteurs erreurs déplacés
- ✅ Export logs
- ✅ Formatage logs
- ✅ Compatibilité UI préservée via redirections MainViewModel

### 🟢 TreeViewViewModel

- ✅ Sélection TreeView
  - ✅ SetNodeSelection extrait
  - ✅ GetSelectedFiles extrait
  - ✅ CollectSelectedFilesRecursive extrait
  - ✅ Redirection MainViewModel
  - ✅ OnNodeSelectionChanged conservé comme coordinateur UI

- 🟢 Expansion TreeView
  - ✅ Infrastructure créée
  - ✅ OnNodeExpandedChanged extrait
  - ✅ Redirection MainViewModel

- ✅ Propagation parent → enfants
  - ✅ SetNodeSelection extrait
  - ✅ Propagation parent → enfants
  - ✅ Redirection MainViewModel

- 🟢 Gestion visibilité recherche
  - ✅ Création TreeViewViewModel
  - ✅ SearchText
  - ✅ HasSearchResult
  - ✅ IsSearchVisible
  - ✅ ApplyFilterRecursive()
  - ✅ SetVisibilityRecursive()

### 🟢 PreviewViewModel

- ✅ Fichier créé

### 🟢 SettingsViewModel

- ✅ Fichier créé
- ✅ Dépendances préparées
- ✅ Intégration MainViewModel préparée

---

## ⚠️ Important

Le split MainViewModel doit rester progressif
afin d’éviter :

- régressions UI
- cassures bindings
- effets de bord async
- pertes stabilité TreeView

---

# 🚀 20. Version 0.16.0 — FINALISATION SPLIT MAINVIEWMODEL

## 🎯 Objectif

Finaliser le découpage du MainViewModel,
réduire les redirections temporaires
et poursuivre les améliorations d’architecture préparées en v0.15.0.

---

## 🧠 Core

### 🟡 Configuration

- ⬜ Migration complète exclusions système / utilisateur
- ⬜ Suppression progressive des dépendances héritées
- ⬜ Validation complète UserExcludedFolders

### 🟡 FileReadResult

- ⬜ Warnings Preview basés sur IsPartial
- ⬜ Préparation badges UI contenu partiel

---

## 🖥️ UI — Split MainViewModel

### ⬜ ExportViewModel

- ⬜ Création ExportViewModel
- ⬜ Déplacement logique export
- ⬜ Déplacement états export
- ⬜ Déplacement validation export
- ⬜ Compatibilité UI via redirections MainViewModel

### 🟡 PreviewViewModel

- ⬜ Extraction logique Preview
- ⬜ Finalisation extraction Preview
- ⬜ Déplacement états Preview
- ⬜ Déplacement génération Preview
- ⬜ Déplacement rafraîchissement Preview
- ⬜ Réduction redirections MainViewModel
- ⬜ Compatibilité UI via redirections MainViewModel

### 🟡 SettingsViewModel

- ⬜ Déplacement paramètres utilisateur
- ⬜ Déplacement préférences utilisateur
- ⬜ Déplacement gestion thème
- ⬜ Déplacement états configuration
- ⬜ Compatibilité UI via redirections MainViewModel
- ⬜ Déplacement niveau de logs
- ⬜ Déplacement mode développeur

### 🟡 TreeViewViewModel

#### ⬜ Finalisation migration Tree

- ⬜ Migration Tree
- ⬜ Migration FilteredTree
- ⬜ Réduction dépendances MainViewModel

#### ⬜ Finalisation recherche

- ⬜ Extraction visibilité recherche
- ⬜ Compatibilité Tree / FilteredTree

---

## 🧹 Nettoyage architecture

- ⬜ Réduction progressive des redirections MainViewModel
- ⬜ Migration progressive des bindings UI
- ⬜ Réduction taille MainViewModel
- ⬜ Simplification responsabilités MainViewModel

---

## 🧪 Tests

- ⬜ Couverture tests ExportViewModel
- ⬜ Couverture tests SettingsViewModel
- ⬜ Validation absence régression bindings UI
- ⬜ Validation complète Preview = Export

---

## ⚠️ Important

La suppression des redirections MainViewModel
doit rester progressive afin d’éviter :

- régressions UI
- cassures bindings
- effets de bord async
- pertes stabilité TreeView

---

# 🚀 21. Version 0.17.0 — STABILISATION ASYNC UI & FINALISATION ARCHITECTURE

## 🎯 Objectif

Finaliser les travaux préparés lors des versions précédentes
et fiabiliser complètement les interactions async/UI.

---

## 🧾 Logging

### ✅ Déjà réalisé

- ✅ Export logs
- ✅ Niveaux de logs
- ✅ Filtrage logs
- ✅ LogsViewModel
- ✅ Formatage logs côté UI

### ⬜ À étudier

- ⬜ Limite mémoire logs
- ⬜ Thread safety avancée
- ⬜ Rotation / nettoyage automatique des logs

---

## 🧠 Finalisation architecture

### ⬜ Suppression progressive des redirections MainViewModel

- ⬜ Migration progressive des bindings UI
- ⬜ Réduction des propriétés de redirection
- ⬜ Simplification MainViewModel

### ⬜ Finalisation exclusions système / utilisateur

- ⬜ Migration complète
- ⬜ Validation architecture définitive

### ⬜ Exploitation complète de IsPartial

- ⬜ Warnings Preview
- ⬜ Warnings Export
- ⬜ Badges UI contenu partiel

---

## 🔄 Async UI

### ⬜ Stabilisation pipeline async UI

Vérifier :

- sélection massive
- clics rapides
- multi-refresh preview
- recherche + sélection simultanée
- suppression exclusions + refresh

---

### ⬜ Améliorer testabilité async

Permettre aux tests de :

- await correctement les opérations
- détecter erreurs async
- éviter faux positifs
- éviter tests instables

---

### ⬜ Vérifier compatibilité WinUI

Valider :

- bindings commandes
- événements UI
- interactions TreeView
- absence freeze
- absence double refresh

---

## ⚡ Mémoire & cache avancé

- ⬜ Monitoring mémoire cache
- ⬜ Nettoyage mémoire intelligent
- ⬜ Stratégie eviction avancée
- ⬜ Profiling mémoire gros projets
- ⬜ Optimisation allocations preview/export
- ⬜ Preview limité configurable

---

## 📌 Preview limité / Export complet

### ⬜ Clarifier comportement utilisateur

- ⬜ Indicateur visuel preview tronqué
- ⬜ Distinction Preview / Export complet
- ⬜ Messages utilisateur améliorés
- ⬜ Validation cohérence UX

### ⬜ Validation architecture

- ⬜ Vérifier cohérence avec :
  - Preview = Export

- ⬜ Vérifier cohérence ALC

---

### 🌳 Synchronisation hiérarchique

- ⬜ Réévaluer nécessité de IsSelected dans le Core
- ⬜ Validation architecture sélection définitive
- ⬜ Synchronisation enfant → parent
- ⬜ Gestion tri-state
- ⬜ Validation UX sélection hiérarchique

### 🌳 Validation TreeView

- ⬜ Validation performances gros arbres
- ⬜ Validation stabilité sélection massive
- ⬜ Validation cohérence visibilité ↔ sélection

---

## 🧪 Tests

### Async

- ⬜ multi-clic rapide
- ⬜ double refresh
- ⬜ refresh simultanés
- ⬜ absence race conditions

### Preview / Export

- ⬜ cohérence preview ↔ export
- ⬜ preview tronqué gros projets
- ⬜ statistiques export complet

### Architecture

- ⬜ validation suppression redirections MainViewModel
- ⬜ validation migration bindings UI

---

## 🎯 Résultat attendu

✔ MainViewModel allégé

✔ Architecture plus propre

✔ Async plus fiable

✔ Tests plus robustes

✔ UI plus prévisible

✔ Base solide pour les futures évolutions

---

# 🎨 🚀 22. Version 0.18.0 — UI / THÈMES / AUDIT COMPLET

## 🎯 Objectif

Transformer l’application en produit propre, agréable et cohérent visuellement.

👉 Sans casser :

- simplicité
- lisibilité
- structure actuelle (gauche / centre / droite / bas)

---

## 🧱 1. BASE THÈME (OBLIGATOIRE)

- ✅ Mode clair / sombre
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
- Audit confort navigation gros projets

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
- Messages explicites fichiers binaires ignorés
- Feedback erreurs encodage
- Feedback accès refusés

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

---

# 🚀 23. Version 0.19.0 — FINALISATION & DISTRIBUTION

## 🎯 Objectif

Transformer l’application en produit final.

---

## ⚙️ BUILD

- ⬜ Validation stabilité très gros projets
- ⬜ Validation export massif réel
- ⬜ Validation mémoire
- ⬜ Validation performances TreeView
- ⬜ Build release propre
- ⬜ Tests complets
- ⬜ Vérification stabilité

- ⬜ Installateur
- ⬜ Validation installateur

- ⬜ Dépendances
- ⬜ Multi-architecture
- ⬜ Choix dossier
- ⬜ Raccourcis

- ⬜ Validation mise à jour propre

---

# 🏁 OBJECTIF FINAL

👉 LatuCollect devient :

- ✔ Stable
- ✔ Rapide
- ✔ Prévisible
- ✔ Lisible
- ✔ Agréable visuellement
- ✔ Distribuable
