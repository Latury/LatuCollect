# 1. 🧪 TESTS – LATUCOLLECT (V2)

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

# 2. 🎯 OBJECTIF

Garantir :

- Fiabilité du chargement de projet
- Robustesse de la sélection des fichiers
- Exactitude de l’aperçu
- Qualité des exports (TXT / Markdown)
- Cohérence globale du système

---

# 3. 🧩 NIVEAUX DE TESTS

Approche progressive :

1. Tests manuels (actuels)
2. Tests unitaires (Core)
3. Tests système (global)

---

# 4. 🖥️ TESTS MANUELS (UI)

Tests réalisés directement dans l’interface WinUI

---

## 4.1 📂 Chargement du projet

### ✅ Cas OK

- Charger un dossier valide
- Chargement avec sous-dossiers
- Projet volumineux

### ❌ Cas erreurs

- Dossier invalide
- Accès refusé
- Sélection annulée
- Annulation utilisateur (fermeture du sélecteur)

👉 Résultat attendu :

- Structure affichée correctement
- Message clair en cas d’erreur
- Aucun crash

---

## 4.2 🌳 Arborescence

### 🔍 Vérifier

- Affichage correct
- Navigation fluide
- Aucun blocage UI

---

## 4.3 🔍 Recherche

### 🔎 Vérifier

- Filtrage correct
- Mise à jour rapide
- Structure conservée

---

### ⚠️ Cas limites

- Recherche vide
- Aucun résultat

👉 Résultat attendu :

- Message "Aucun résultat"
- UI cohérente

---

### 📄 Extensions

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

## 4.4 ☑️ Sélection

### 🔎 Vérifier

- Checkbox fonctionnelle
- Multi-sélection
- Désélection correcte

---

### ⚠️ Cas limites

- Aucun fichier sélectionné

👉 Résultat attendu :

- Message affiché
- Export bloqué
- Copier désactivé

---

## 4.5 🌳 Cohérence arborescence

### 🔎 Vérifier

- Sélection parent → sélection enfants
- Désélection cohérente
- Aucun comportement incohérent
- Synchronisation correcte sélection ↔ preview

---

### 🚨 Cas critiques

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

---

### 🔥 Priorité

- Haute

---

## 4.6 👁️ Aperçu

### 🔎 Vérifier

- Mise à jour en temps réel
- Aperçu = export dans le fonctionnement standard
- Lisibilité correcte

### 🔄 Déclencheurs

- Sélection
- Désélection
- Changement format

---

### ⚠️ Limitation

- Maximum 20 fichiers affichés

👉 Message :

⚠ Aperçu limité à 20 fichiers

👉 Cette limitation concerne uniquement l’affichage.

L’export final contient toujours l’ensemble des fichiers sélectionnés.

---

### ⚡ Optimisations

- Pas de recalcul inutile
- Cache actif
- Protection double appel
- Debounce preview async
- Invalidation previews obsolètes
- Protection anti race conditions
- Génération preview découplée de la sélection

---

## 4.7 ✂️ Preview limité / Export complet

### 🔎 Vérifier

- preview tronqué correctement
- export complet conservé
- statistiques calculées sur tous les fichiers
- cohérence des messages utilisateur

---

### 🚨 Cas critiques

- très gros projet
- limite mémoire atteinte
- grand nombre de fichiers sélectionnés
- preview volontairement limité

---

### 🎯 Résultat attendu

- Aucun freeze UI
- Export complet conservé
- Message utilisateur clair
- Comportement prévisible

---

## 4.8 📊 États UI

- Loading
- Ready
- Empty
- Error

👉 Toujours cohérents

---

## 4.9 💬 Feedback UI

### 🔎 Vérifier

- Message affiché après action
- Disparition automatique
- Aucun blocage UI

---

### 📋 Cas testés

- Export réussi
- Erreur export
- Copier
- Sélection invalide

---

### 🎯 Résultat attendu

- Feedback visible
- Compréhensible
- Non intrusif

---

## 4.10 ⚠️ Projets volumineux

👉 Message :

⚠ Projet volumineux — affichage partiel

---

### 🔎 Vérifier

- Aucun freeze
- UI fluide
- Recherche fonctionnelle

---

## 4.11 ⚡ Performance

### 🔎 Vérifier

- Cache actif (pas de relecture disque)
- Pas de recalcul inutile
- UI stable
- Chargement progressif UI
- Yield UI pendant construction TreeView
- Réduction des freezes pendant import massif

---

### 📋 Cas testés

- Clics rapides
- Sélections répétées

---

### 🎯 Résultat attendu

- Aucun ralentissement
- Aucun bug visuel

---

### 🧠 Mémoire & cache

#### 🔎 Vérifier

- Nettoyage correct du cache
- Réduction des recalculs inutiles
- Absence d’explosion mémoire
- Stabilité sur gros projets

---

#### 🎯 Résultat attendu

- Mémoire stable
- UI fluide
- Aucun ralentissement progressif

---

## 4.12 🔄 Tests async UI

### 🔎 Vérifier

- absence de double refresh
- absence de race conditions
- stabilité lors des clics rapides
- stabilité sélection + recherche simultanées
- validation previews obsolètes ignorés
- validation versioning preview async
- validation debounce preview

---

### 🚨 Cas critiques

- multi-clic rapide
- sélection massive
- refresh preview simultanés
- suppression exclusions pendant refresh

---

### 🎯 Résultat attendu

- Aucun comportement incohérent
- Aucun freeze
- Aucun refresh infini
- UI toujours réactive

---

### 🖥️ Fenêtre

#### 🔎 Vérifier

- Impossible de réduire sous 1600 x 1000
- Aucun flickering visible

---

#### 🎯 Résultat attendu

- UI stable
- Aucun effet visuel parasite

---

## 4.13 ⏳ Loader

### 🔎 Vérifier

- Visible pendant chargement
- Disparaît correctement
- UI non bloquée

---

## 4.14 📤 Export

### 📄 TXT

- Fichier créé
- Contenu correct
- Séparateur respecté

---

### 📝 Markdown

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

## 4.15 📋 Copier

### Vérifier :

- Contenu exact
- Correspond à l’aperçu
- Désactivé si vide

---

## 4.16 🧾 Logs

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

## 4.17 📊 Statistiques

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

## 4.18 👨🏻‍💻 Mode développeur

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

## 4.19 📄 Format

```text
Chemin du fichier

(contenu du fichier)

----------------------------------------

```

---

## 4.20 ⚠️ Cas particuliers

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

## 4.21 🧹 Validation post-suppression simulation

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

# 5. 🧠 TESTS UNITAIRES

## 🎯 Objectif

Tester la logique métier indépendamment de l’UI

---

## 📦 Cibles principales

- FileReaderService
- FileExportService
- FileStatisticsService
- MainViewModel
- Recherche TreeView
- Sélection TreeView
- États UI
- États Export
- Exclusions

---

## 5.1 📋 Cas principaux

### 5.1.1 🌳 Recherche TreeView

- Filtrage visibilité
- Expansion automatique
- Reset visibilité
- Conservation expansion après recherche
- Suppression nodes

---

### 5.1.2 ☑️ Sélection TreeView

- Propagation parent ↔ enfants
- Synchronisation preview
- Cohérence sélection ↔ visibilité
- Sélection massive
- Multi-clic rapide

---

### 5.1.3 📁 Exclusions

- Exclusion fichiers
- Exclusion dossiers
- Persistance configuration
- Compatibilité ancien format
- Validation grouped exclusions
- Validation absence doublons exclusions
- Validation stabilité refresh exclusions

---

### 5.1.4 📊 États UI / Export

- États Loading / Ready / Empty / Error
- Validation export
- Validation CanExport
- Validation CanCopy

---

### 5.1.5 📖 Lecture

- Lecture valide
- Gestion des erreurs

---

### 5.1.6 📤 Export

- Génération TXT
- Génération Markdown
- Respect du format

---

### 5.1.7 📈 Statistiques

- Comptage lignes
- Comptage caractères
- Gestion fichiers vides

---

# 6. 📊 COUVERTURE CIBLE

|  Module   | Objectif |
| :-------: | :------: |
|  Lecture  |   90%    |
|  Export   |   90%    |
| TreeView  |   85%    |
| ViewModel |   85%    |

---

# 7. 🧭 STRATÉGIE

## 🎯 Priorités

1. Import fiable
2. Sélection correcte
3. Aperçu exact
4. Export propre
5. Gestion erreurs

---

# 8. 🧠 FONCTIONNEMENT VALIDÉ

## 👤 Flux utilisateur

Importer → Sélectionner → Aperçu → Exporter

---

## ⚙️ Pipeline interne

Import → Lecture → Collection → Assemblage → Statistiques → Export

👉 Copier intelligent uniquement

👉 Aucun traitement complexe

---

# 9. 📌 ÉTAT ACTUEL

## 🧪 Couverture de tests

- ✔ Tests manuels complets (UI)
- ✔ Base importante de tests unitaires
- ✔ Extension progressive de la couverture

---

## 🌳 TreeView & Recherche

- ✔ Validation TreeView / Recherche / Sélection
- ✔ Validation persistance expansion TreeView
- ✔ Validation exclusions groupées

---

## 🖥️ UI & ViewModels

- ✔ Validation états UI / ViewModel
- ✔ Validation chargement progressif UI
- ✔ Stabilisation pipeline preview async

---

## 🏗️ Architecture

- ✔ Simplification architecture Core/UI
- ✔ Validation reset runtime configuration
- ✔ Réduction progressive des responsabilités MainViewModel
- ✔ Extraction avancée PreviewViewModel
- ✔ Migration TreeViewViewModel
- ✔ Préparation SettingsViewModel

---

## 🕘 Historique majeur

- ✔ Suppression complète du système de simulation (v0.13.0)

---

## 📊 Résultat actuel

- ✔ 116 tests unitaires verts
- ✔ Validation PreviewViewModel
- ✔ Validation TreeViewViewModel
- ✔ Validation pipeline preview async
- 🔄 Extension couverture tests avancés

---

# 10. 🏁 CONCLUSION

## ✅ Garanties actuelles

- Aucun export incorrect
- Aucun crash
- Cohérence UI / Core

---

## 🎯 Résultat

LatuCollect reste :

- Stable
- Fiable
- Prévisible

---

# 11. 🔮 ÉVOLUTIONS

## 🧪 Couverture de tests

- Extension couverture tests unitaires
- Extension couverture tests automatisés

---

## ⚡ Performance

- Tests de performance avancés

---

## 🖥️ Architecture UI

- Validation communication inter-ViewModels
- Validation synchronisation états UI
- Validation stabilité bindings
- Validation stabilité commandes async
- Validation migration PreviewViewModel
- Validation migration TreeViewViewModel
- Préparation validation SettingsViewModel
- Préparation validation ExportViewModel
