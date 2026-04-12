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
- ✅ Mise à jour en temps réel
- ✅ Conservation de la structure

### Cas limites :

- ❌ Recherche vide
- ❌ Aucun résultat

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

## 👁️ Aperçu

### Vérifier :

- ✅ Mise à jour en temps réel
- ✅ Aperçu = export (strictement identique)
- ✅ Lisibilité

### Déclencheurs :

- ✅ Sélection fichier
- ✅ Désélection
- ✅ Changement format

---

## 🔄 États UI (v0.5.0)

### Vérifier :

- 🔄 Chargement → affichage du loader
- ❌ Erreur → message affiché
- ✅ Prêt → contenu ou message vide

👉 L’UI doit toujours refléter l’état réel

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
```

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
- ✅ Bouton export désactivé si aucun contenu
- ✅ Confirmation après export

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
Import → Lecture → Assemblage → Export
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
