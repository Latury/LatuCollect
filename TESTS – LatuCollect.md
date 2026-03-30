# 🧪 TESTS – LATUCOLLECT

Stratégie officielle de validation du projet LatuCollect.

---

# 🎯 Objectif

Garantir :

- ✔ Fiabilité du chargement de projet
- ✔ Robustesse de la sélection des fichiers
- ✔ Exactitude de l’aperçu
- ✔ Qualité des exports (TXT / Markdown)
- ✔ Cohérence globale du système

---

# 🧩 1. Niveaux de tests

Approche progressive :

1. ✔ Tests manuels (UI actuels)
2. 🔄 Tests unitaires (à venir)
3. 🔄 Tests système (futurs)

---

# 🧪 2. Tests manuels (UI)

Tests réalisés directement dans l’interface WinUI.

---

## 📂 Chargement du projet

### Cas OK

- ✔ Charger un dossier valide
- ✔ Chargement avec sous-dossiers
- ✔ Projet volumineux

### Cas erreurs

- ❌ Dossier invalide
- ❌ Accès refusé

👉 Résultat attendu :

- ✔ Structure affichée correctement
- ✔ Message clair en cas d’erreur

---

## 🌳 Arborescence

### Vérifier :

- ✔ Affichage correct des dossiers
- ✔ Navigation fluide
- ✔ Retour en arrière fonctionnel

---

## ☑️ Sélection des fichiers

### Vérifier :

- ✔ Checkbox fonctionnelle
- ✔ Sélection multiple
- ✔ Désélection

### Cas limites :

- ❌ Aucun fichier sélectionné

👉 Résultat attendu :

- ✔ Aperçu vide
- ✔ Export bloqué

---

## 👁️ Aperçu

### Vérifier :

- ✔ Mise à jour en temps réel
- ✔ Correspondance exacte avec export
- ✔ Lisibilité

### Déclencheurs :

- ✔ sélection fichier
- ✔ désélection
- ✔ changement format

---

## 📤 Export

### Cas TXT

- ✔ Fichier créé
- ✔ Contenu conforme
- ✔ Séparateur correct

### Cas Markdown

- ✔ Structure lisible
- ✔ Chemin affiché
- ✔ Contenu correct

### Cas erreurs

- ❌ Aucun fichier sélectionné
- ❌ Échec d’écriture

---

## 📄 Format

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

- ✔ Respect du format
- ✔ Espacement correct

---

## ⚠️ Cas particuliers

- ❌ Fichier vide
- ❌ Fichier volumineux
- ❌ Caractères spéciaux
- ❌ Erreur lecture
- ❌ Chemins longs

---

## ⚠️ Fichiers ignorés

- bin
- obj

---

# 🧠 3. Tests unitaires (À FAIRE)

Cibles :

- ✔ FileImportService
- ✔ FileExportService

---

## Cas principaux

### Import

- ✔ Ajout dossier
- ✔ filtre extensions
- ✔ gestion erreurs

---

### Export

- ✔ génération TXT
- ✔ génération Markdown
- ✔ respect format

---

# 📊 4. Couverture cible

| Module | Objectif |
| ------ | -------- |
| Import | 90%      |
| Export | 90%      |

---

# 🧭 5. Stratégie

Priorité :

1. ✔ Import fiable
2. ✔ Sélection correcte
3. ✔ Aperçu exact
4. ✔ Export propre
5. ✔ Gestion erreurs

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

- ✔ Aucun fichier incorrect
- ✔ Aucun export corrompu
- ✔ Cohérence UI / Core

👉 LatuCollect doit rester :

- ✔ Fiable
- ✔ Stable
- ✔ Prévisible
