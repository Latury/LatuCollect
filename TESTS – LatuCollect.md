# 🧪 TESTS – LATUCOLLECT

Stratégie officielle de validation du projet LatuCollect

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
2. Tests unitaires (à venir)
3. Tests système (futurs)

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

👉 Résultat attendu :

- Structure affichée correctement
- Message clair en cas d’erreur

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
- Aucun écran vide

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
- Désélection

### Cas limites :

- Aucun fichier sélectionné

👉 Résultat attendu :

- Message affiché
- Export bloqué
- Copier désactivé

---

### ⚠️ Sélection globale

- Désactivée volontairement

👉 Clic = popup explicatif
👉 Aucun freeze

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

- Maximum 20 fichiers

👉 Message :

```texte
⚠ Aperçu limité à 20 fichiers
```

---

### ⚡ Optimisation

- Pas de recalcul inutile
- Cache actif
- Protection double appel

---

## 🔄 ÉTATS UI

- Chargement → loader
- Erreur → message
- Prêt → contenu

👉 Toujours cohérent

---

## ⚠️ PROJETS VOLUMINEUX

👉 Message :

```texte
⚠ Projet volumineux — affichage partiel
```

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

### Cas :

- Clics rapides
- Sélections répétées

👉 Résultat :

- Aucun ralentissement
- Aucun bug visuel

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

## 📊 STATISTIQUES

### Vérifier :

- Fichiers
- Lignes
- Caractères
- Taille

### Comportement :

- Temps réel
- Aucun freeze
- Cohérence

---

## 📄 FORMAT

```texte
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

👉 Résultat attendu :

- Aucun crash
- Message clair

---

# 🧠 TESTS UNITAIRES (À FAIRE)

Cibles :

- FileReaderService
- FileExportService

---

## Cas principaux

### Lecture

- Lecture valide
- Gestion erreur

---

### Export

- TXT
- Markdown
- Format respecté

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

```text id="fluxtest-latucollect"
Importer → Sélectionner → Aperçu → Exporter
```

---

## Pipeline interne

```text id="pipelinetest-latucollect"
Import → Lecture → Collection → Assemblage → Statistiques → Export
```

👉 Aucun traitement complexe
👉 Copier intelligent uniquement

---

# 🏁 CONCLUSION

Les tests garantissent :

- Aucun export incorrect
- Aucun crash
- Cohérence UI / Core

👉 LatuCollect reste :

- Fiable
- Stable
- Prévisible
