# 🧪 TESTS – LATUCOLLECT

Stratégie officielle de validation du projet LatuCollect.

---

# 🎯 Objectif

Garantir :

- ✅ Fiabilité du chargement de projet
- ✅ Robustesse de la sélection des fichiers
- ✅ Exactitude de l’aperçu
- ✅ Qualité des exports (TXT / Markdown)
- ✅ Cohérence globale du système

---

# 🧩 1. Niveaux de tests

Approche progressive :

1. ✅ Tests manuels (UI actuels)
2. 🔄 Tests unitaires (à venir)
3. 🔄 Tests système (futurs)

---

# 🧪 2. Tests manuels (UI)

Tests réalisés directement dans l’interface WinUI.

---

## 📂 Chargement du projet

### Cas OK

- ✅ Charger un dossier valide
- ✅ Chargement avec sous-dossiers
- ✅ Projet volumineux

### Cas erreurs

- ❌ Dossier invalide
- ❌ Accès refusé
- ❌ Sélection annulée par l’utilisateur

👉 Résultat attendu :

- ✅ Structure affichée correctement
- ✅ Message clair en cas d’erreur

---

## 🌳 Arborescence

### Vérifier :

- ✅ Affichage correct des dossiers
- ✅ Navigation fluide
- ✅ Retour en arrière fonctionnel

---

## 🔍 Recherche

### Vérifier :

- ✅ Filtrage correct des fichiers
- ✅ Mise à jour rapide (optimisée pour la fluidité)
- ✅ Conservation de la structure

### Cas limites :

- ❌ Recherche vide
- ❌ Aucun résultat

---

### Nouveautés (v0.6.0)

- ✅ Filtrage par extension :
  - Recherche ".cs" → uniquement fichiers .cs
  - Recherche ".xaml" → uniquement fichiers XAML

- ✅ Vérification :
  - Les fichiers affichés correspondent bien à l’extension demandée
  - Les dossiers parents restent visibles si un enfant correspond

---

- ✅ Gestion "aucun résultat" :
  - Recherche sans correspondance

👉 Résultat attendu :

- Message "Aucun résultat" affiché
- Arbre masqué
- Aucun écran vide (toujours un message affiché)

---

- ✅ Performance (debounce) :
  - Saisie rapide dans la barre de recherche

👉 Résultat attendu :

- Aucun freeze UI
- Pas de recalcul à chaque frappe
- Filtrage déclenché après une courte pause utilisateur

---

- ✅ Les dossiers exclus (bin, obj, .git) ne doivent jamais apparaître dans les résultats de recherche

👉 Important :

- Ça couvre exclusion + recherche ensemble

---

## ☑️ Sélection des fichiers

### Vérifier :

- ✅ Checkbox fonctionnelle
- ✅ Sélection multiple
- ✅ Désélection

### Cas limites :

- ❌ Aucun fichier sélectionné

👉 Résultat attendu :

- ✅ Message "Aucun fichier sélectionné..." affiché
- ✅ Export bloqué
- ✅ Bouton copier désactivé

---

### ⚠️ Sélection globale (v0.7.0)

### Vérifier :

- ❌ Bouton "Tout sélectionner" désactivé
- ✅ Clic affiche un popup explicatif

---

### Comportement attendu :

- Aucun traitement massif
- Aucun freeze UI
- Message clair utilisateur

---

## 👁️ Aperçu

### Vérifier :

- ✅ Mise à jour en temps réel
- ✅ Aperçu = export (strictement identique)
- ✅ Lisibilité

### Déclencheurs :

- ✅ Sélection fichier
- ✅ Désélection
- ✅ Changement format

### ⚠️ Limitation aperçu (v0.7.0)

### Vérifier :

- ✅ Maximum 20 fichiers affichés
- ✅ Message affiché :

```text
⚠ Aperçu limité à 20 fichiers
```

---

## 🔄 États UI (v0.5.0)

### Vérifier :

- 🔄 Chargement → affichage du loader
- ❌ Erreur → message affiché
- ✅ Prêt → contenu ou message vide

👉 L’UI doit toujours refléter l’état réel

---

---

### ⚠️ Limitation aperçu (v0.7.0)

### Vérifier :

- ✅ Maximum 20 fichiers affichés
- ✅ Message affiché :

````text
⚠ Aperçu limité à 20 fichiers

---

## ⏳ Loader

### Vérifier :

- ✅ Visible pendant le chargement
- ✅ Disparaît correctement
- ✅ Aucun blocage UI

---

## ⚠️ Projets volumineux

### Vérifier :

- ✅ Aucun freeze
- ✅ Chargement partiel
- ✅ Message affiché :

```text
⚠ Projet volumineux — affichage partiel
````

- ✅ Recherche fluide même sur projet volumineux (optimisation v0.6.0)

---

## 📤 Export

### Cas TXT

- ✅ Fichier créé
- ✅ Contenu conforme
- ✅ Séparateur correct

### 🔒 Robustesse (v0.4.0)

- ✅ Aucun crash lors d’un échec d’écriture
- ✅ Message d’erreur retourné proprement
- ✅ Comportement prévisible même en cas de problème disque ou accès refusé

### Cas Markdown

- ✅ Structure lisible
- ✅ Chemin affiché
- ✅ Contenu correct
- ✅ Format adapté au Markdown

### Comportement attendu

- ✅ Format dépend du choix utilisateur (.txt / .md)
- ✅ Export bloqué si aucun contenu
- ✅ Bouton Export désactivé si aucun contenu
- ✅ Confirmation après export
- ✅ Vérification des messages d’erreur spécifiques (accès refusé, fichier utilisé, etc.)

### Cas erreurs

- ❌ Aucun fichier sélectionné
- ❌ Échec d’écriture

---

## 📋 Copier

### Vérifier :

- ✅ Copie du contenu correct
- ✅ Correspond exactement à l’aperçu
- ✅ Désactivé si aucun contenu
- ✅ Message de confirmation affiché

---

---

## 📊 Statistiques (v0.7.0)

### Vérifier :

- ✅ Nombre de fichiers correct
- ✅ Nombre total de lignes correct
- ✅ Nombre total de caractères correct
- ✅ Taille totale cohérente

---

### Comportement attendu :

- Mise à jour en temps réel
- Aucun freeze UI
- Résultats cohérents avec les fichiers sélectionnés

---

### Cas limites :

- ❌ Aucun fichier sélectionné → toutes les valeurs à 0
- ❌ Fichiers vides

---

## 📄 Format

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

---

- ✅ Respect du format
- ✅ Vérification du séparateur
- ✅ Vérification des espacements

---

## ⚠️ Cas particuliers

- ❌ Fichier vide
- ❌ Fichier volumineux
- ❌ Caractères spéciaux
- ❌ Erreur lecture
- ❌ Chemins longs
- ❌ Échec d’écriture fichier

👉 Résultat attendu :

- ✅ Aucun crash
- ✅ Message d’erreur utilisateur

---

## ⚠️ Fichiers ignorés

- bin
- obj
- .git

---

# 🧠 3. Tests unitaires (À FAIRE)

Cibles :

- ✅ FileReaderService
- ✅ FileExportService

---

## Cas principaux

### Lecture

- ✅ Lecture fichier valide
- ✅ Gestion erreur lecture

---

### Export

- ✅ Génération TXT
- ✅ Génération Markdown
- ✅ Respect format

---

# 📊 4. Couverture cible

| Module  | Objectif |
| ------- | -------- |
| Lecture | 90%      |
| Export  | 90%      |

---

# 🧭 5. Stratégie

Priorité :

1. ✅ Import fiable
2. ✅ Sélection correcte
3. ✅ Aperçu exact
4. ✅ Export propre
5. ✅ Gestion erreurs

---

# 🧠 6. Fonctionnement validé

## 🔹 Flux utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

## 🔹 Pipeline interne

```text
Import → Lecture → Assemblage → Statistiques → Export
```

👉 Aucune transformation du code

---

# 🏁 Conclusion

Les tests garantissent :

- ✅ Aucun fichier incorrect
- ✅ Aucun export corrompu
- ✅ Cohérence UI / Core

👉 LatuCollect doit rester :

- ✅ Fiable
- ✅ Stable
- ✅ Prévisible
