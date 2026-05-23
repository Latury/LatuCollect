# 🧪 TESTS – LATUCOLLECT (V2)

Stratégie officielle de validation du projet LatuCollect

---

## 📌 Résumé

Ce document définit la stratégie de tests de LatuCollect.

👉 Il permet de valider :

- La stabilité
- La fiabilité
- La cohérence UI / Core

---

## 📊 État des tests

- ✔ Tests manuels complets (UI)
- ✔ Tests unitaires Core
- ✔ Tests unitaires ViewModel
- 🔄 Extension couverture tests
- 🔮 Tests système complets (futurs)

---

# 🎯 OBJECTIF

Garantir :

- Fiabilité du chargement de projet
- Robustesse de la sélection des fichiers
- Exactitude de l’aperçu
- Qualité des exports (TXT / Markdown)
- Cohérence globale du système

---

# 🧩 NIVEAUX DE TESTS

Approche progressive :

1. Tests manuels (actuels)
2. Tests unitaires (Core)
3. Tests système (global)

---

# 🧪 TESTS MANUELS (UI)

Tests réalisés directement dans l’interface WinUI

---

## 📂 CHARGEMENT DU PROJET

### Cas OK

- Charger un dossier valide
- Chargement avec sous-dossiers
- Projet volumineux

### Cas erreurs

- Dossier invalide
- Accès refusé
- Sélection annulée
- Annulation utilisateur (fermeture du sélecteur)

👉 Résultat attendu :

- Structure affichée correctement
- Message clair en cas d’erreur
- Aucun crash

---

## 🌳 ARBORESCENCE

### Vérifier :

- Affichage correct
- Navigation fluide
- Aucun blocage UI

---

## 🔍 RECHERCHE

### Vérifier :

- Filtrage correct
- Mise à jour rapide
- Structure conservée

### Cas limites :

- Recherche vide
- Aucun résultat

👉 Résultat attendu :

- Message "Aucun résultat"
- UI cohérente

---

### ✔ Extensions

- .cs
- .xaml
- .json

👉 Vérifier cohérence des résultats

---

### ⚡ Performance

- Débounce actif
- Aucun freeze UI
- Filtrage fluide

---

### 📁 Exclusions

- bin
- obj
- .git

👉 Ne doivent jamais apparaître

---

## ☑️ SÉLECTION

### Vérifier :

- Checkbox fonctionnelle
- Multi-sélection
- Désélection correcte

---

### ⚠️ Cas limites :

- Aucun fichier sélectionné

👉 Résultat attendu :

- Message affiché
- Export bloqué
- Copier désactivé

---

## 🌳 COHÉRENCE ARBORESCENCE (CRITIQUE — 0.11.0)

### Vérifier :

- Sélection parent → sélection enfants
- Désélection cohérente
- Aucun comportement incohérent
- Synchronisation correcte sélection ↔ preview

### Cas critiques :

- Décoche un fichier → ne doit pas recocher le parent
- Dossier contenant des fichiers sélectionnés et non sélectionnés
- Synchronisation correcte de la sélection

👉 Résultat attendu :

- Cohérence parfaite parent ↔ enfants
- Aucun bug visuel
- Aucun état incohérent
- Conservation état ouvert TreeView
- Persistance dossiers ouverts
- Restauration expansion après reload

👉 Priorité : haute

---

## 👁️ APERÇU

### Vérifier :

- Mise à jour en temps réel
- Aperçu = export dans le fonctionnement standard
- Lisibilité correcte

### Déclencheurs :

- Sélection
- Désélection
- Changement format

---

### ⚠️ Limitation

- Maximum 20 fichiers affichés

👉 Message :

```text
⚠ Aperçu limité à 20 fichiers
```

👉 Cette limitation concerne uniquement l’affichage.
L’export final contient toujours l’ensemble des fichiers sélectionnés.

---

### ⚡ Optimisation

- Pas de recalcul inutile
- Cache actif
- Protection double appel
- Debounce preview async
- Invalidation previews obsolètes
- Protection anti race conditions
- Génération preview découplée de la sélection

---

## ⚠️ PREVIEW LIMITÉ / EXPORT COMPLET

### Vérifier :

- preview tronqué correctement
- export complet conservé
- statistiques calculées sur tous les fichiers
- cohérence des messages utilisateur

---

### Cas critiques :

- très gros projet
- limite mémoire atteinte
- grand nombre de fichiers sélectionnés
- preview volontairement limité

---

👉 Résultat attendu :

- Aucun freeze UI
- Export complet conservé
- Message utilisateur clair
- Comportement prévisible

---

## 🔄 ÉTATS UI

- Loading
- Ready
- Empty
- Error

👉 Toujours cohérents

---

## 💬 FEEDBACK UI

### Vérifier :

- Message affiché après action
- Disparition automatique
- Aucun blocage UI

### Cas :

- Export réussi
- Erreur export
- Copier
- Sélection invalide

👉 Résultat attendu :

- Feedback visible
- Compréhensible
- Non intrusif

---

## ⚠️ PROJETS VOLUMINEUX

👉 Message :

```text
⚠ Projet volumineux — affichage partiel
```

---

### Vérifier :

- Aucun freeze
- UI fluide
- Recherche fonctionnelle

---

## ⚡ PERFORMANCE

### Vérifier :

- Cache actif (pas de relecture disque)
- Pas de recalcul inutile
- UI stable
- Chargement progressif UI
- Yield UI pendant construction TreeView
- Réduction des freezes pendant import massif

---

### Cas :

- Clics rapides
- Sélections répétées

👉 Résultat :

- Aucun ralentissement
- Aucun bug visuel

---

### 🧠 Mémoire & cache

### Vérifier :

- Nettoyage correct du cache
- Réduction des recalculs inutiles
- Absence d’explosion mémoire
- Stabilité sur gros projets

---

👉 Résultat attendu :

- Mémoire stable
- UI fluide
- Aucun ralentissement progressif

---

## 🔄 TESTS ASYNC UI

### Vérifier :

- absence de double refresh
- absence de race conditions
- stabilité lors des clics rapides
- stabilité sélection + recherche simultanées
- validation previews obsolètes ignorés
- validation versioning preview async
- validation debounce preview

---

### Cas critiques :

- multi-clic rapide
- sélection massive
- refresh preview simultanés
- suppression exclusions pendant refresh

---

👉 Résultat attendu :

- Aucun comportement incohérent
- Aucun freeze
- Aucun refresh infini
- UI toujours réactive

---

### 🖥️ Fenêtre

- Impossible de réduire sous 1600 x 1000
- Aucun flickering visible

👉 Résultat :

- UI stable
- Aucun effet visuel parasite

---

## ⏳ LOADER

### Vérifier :

- Visible pendant chargement
- Disparaît correctement
- UI non bloquée

---

## 📤 EXPORT

### TXT

- Fichier créé
- Contenu correct
- Séparateur respecté

---

### MARKDOWN

- Structure correcte
- Lisible
- Conforme

---

### ⚠️ Comportement

- Format respecté
- Export bloqué si vide
- Confirmation affichée

---

### ❌ Cas erreurs

- Aucun fichier sélectionné
- Échec d’écriture

👉 Résultat :

- Aucun crash
- Message utilisateur

---

## 📋 COPIER

### Vérifier :

- Contenu exact
- Correspond à l’aperçu
- Désactivé si vide

---

## 🧾 LOGS

### Vérifier :

- Ouverture du dialog
- Affichage des logs
- Filtrage (Info / Warning / Error)

---

### Export logs :

- Fichier créé
- Contenu correct
- Format lisible

---

👉 Résultat attendu :

- Aucun crash
- Données cohérentes

---

## 📊 STATISTIQUES

### Vérifier :

- Fichiers
- Lignes
- Caractères
- Taille

---

### Comportement :

- Temps réel
- Aucun freeze
- Cohérence

---

## 👨🏻‍💻 MODE DÉVELOPPEUR

### Vérifier :

- Activation via paramètres
- Désactivation par défaut
- Affichage d’un indicateur visuel

---

### Comportement :

- Aucun impact sur utilisateur normal
- Activation immédiate
- UI mise à jour correctement

---

👉 Objectif :

- Analyse interne
- Outils de développement
- Gestion avancée des exclusions protégées

---

👉 Résultat attendu :

- Mode isolé
- Aucun impact métier

---

## 📄 FORMAT

```text
Chemin du fichier

(contenu du fichier)

---

```

---

## ⚠️ CAS PARTICULIERS

- Fichier vide
- Fichier volumineux
- Caractères spéciaux
- Erreur lecture
- Chemins longs
- Échec export
- Fermeture application pendant traitement

---

👉 Résultat attendu :

- Aucun crash
- Message clair

---

## 🧹 VALIDATION POST-SUPPRESSION SIMULATION

### Vérifier :

- absence de bouton simulation
- absence de dialog simulation
- absence de bindings simulation
- mode développeur toujours fonctionnel

---

👉 Résultat attendu :

- Aucun élément simulation restant
- Aucun binding cassé
- Aucun comportement incohérent
- UI simplifiée et stable

---

# 🧠 TESTS UNITAIRES

## 🎯 Objectif

Tester la logique métier indépendamment de l’UI

---

## 📦 Cibles principales

- FileReaderService
- FileExportService
- FileStatisticsService
- FileCollectionService
- MainViewModel
- Recherche TreeView
- Sélection TreeView
- États UI
- États Export
- Exclusions

---

## 📋 Cas principaux

### Recherche TreeView

- Filtrage visibilité
- Expansion automatique
- Reset visibilité
- Conservation expansion après recherche
- Suppression nodes

---

### Sélection TreeView

- Propagation parent ↔ enfants
- Synchronisation preview
- Cohérence sélection ↔ visibilité
- Sélection massive
- Multi-clic rapide

---

### Exclusions

- Exclusion fichiers
- Exclusion dossiers
- Persistance configuration
- Compatibilité ancien format
- Validation grouped exclusions
- Validation absence doublons exclusions
- Validation stabilité refresh exclusions

---

### États UI / Export

- États Loading / Ready / Empty / Error
- Validation export
- Validation CanExport
- Validation CanCopy

---

### Lecture

- Lecture valide
- Gestion des erreurs

---

### Export

- Génération TXT
- Génération Markdown
- Respect du format

---

### Statistiques

- Comptage lignes
- Comptage caractères
- Gestion fichiers vides

---

# 📊 COUVERTURE CIBLE

|  Module   | Objectif |
| :-------: | :------: |
|  Lecture  |   90%    |
|  Export   |   90%    |
| TreeView  |   85%    |
| ViewModel |   85%    |

---

# 🧭 STRATÉGIE

Priorité :

1. Import fiable
2. Sélection correcte
3. Aperçu exact
4. Export propre
5. Gestion erreurs

---

# 🧠 FONCTIONNEMENT VALIDÉ

## Flux utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

## Pipeline interne

```text
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

👉 Copier intelligent uniquement
👉 Aucun traitement complexe

---

# 📌 ÉTAT ACTUEL

- ✔ Tests manuels complets (UI)
- ✔ Base importante de tests unitaires
- ✔ Validation TreeView / Recherche / Sélection
- ✔ Validation Export / Statistics / Collection
- ✔ Validation états UI / ViewModel
- ✔ Vérification des performances principales
- ✔ Suppression complète du système de simulation (v0.13.0)
- ✔ Simplification architecture Core/UI
- ✔ Stabilisation pipeline preview async
- ✔ Validation persistance expansion TreeView
- ✔ Validation exclusions groupées
- ✔ Validation reset runtime configuration
- ✔ Validation chargement progressif UI
- ✔ 115 tests unitaires verts
- 🔄 Extension couverture tests avancés

---

# 🏁 CONCLUSION

Les tests garantissent :

- Aucun export incorrect
- Aucun crash
- Cohérence UI / Core

---

👉 LatuCollect reste :

- Stable
- Fiable
- Prévisible

---

# 🔮 ÉVOLUTIONS

- Extension couverture tests unitaires
- Extension couverture tests automatisés
- Tests de performance avancés
- Tests communication inter-ViewModels
- Validation synchronisation états UI
- Validation stabilité bindings
- Validation stabilité commandes async
