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
- 🔄 Tests unitaires (à venir)
- 🔮 Tests système (futurs)

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
- Désélection enfant → mise à jour parent correcte
- Aucun comportement incohérent

### Cas critiques :

- Décoche un fichier → ne doit pas recocher le parent
- Sélection partielle d’un dossier
- Propagation correcte des états

👉 Résultat attendu :

- Cohérence parfaite parent ↔ enfants
- Aucun bug visuel
- Aucun état incohérent

👉 Priorité : haute

---

## 👁️ APERÇU

### Vérifier :

- Mise à jour en temps réel
- Aperçu = export
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

---

### Cas :

- Clics rapides
- Sélections répétées

👉 Résultat :

- Aucun ralentissement
- Aucun bug visuel

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

## 🧑‍💻 MODE DÉVELOPPEUR

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

- Debug
- Analyse interne
- Outils de développement

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

# 🧠 TESTS UNITAIRES (À VENIR)

## 🎯 Objectif

Tester la logique métier indépendamment de l’UI

---

## 📦 Cibles principales

- FileReaderService
- FileExportService
- FileStatisticsService

---

## 📋 Cas principaux

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

| Module  | Objectif |
| ------- | -------- |
| Lecture | 90%      |
| Export  | 90%      |

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
- ✔ Validation des cas principaux
- ✔ Vérification des performances
- 🔄 Tests unitaires en préparation

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

- Tests unitaires complets
- Tests automatisés
- Tests de performance avancés
