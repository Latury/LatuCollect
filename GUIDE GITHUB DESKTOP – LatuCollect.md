# 🧠 GUIDE GITHUB DESKTOP – LATUCOLLECT

Guide pédagogique officiel pour la gestion du versioning du projet LatuCollect.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🎯 OBJECTIF

Dans LatuCollect, Git est un élément central du projet.

Chaque commit doit être :

- ✔ Structuré
- ✔ Compréhensible
- ✔ Aligné avec la feuille de route
- ✔ Cohérent avec l’architecture ALC
- ✔ Cohérent avec les documents du projet

---

# 📚 DOCUMENTS DE RÉFÉRENCE

Chaque commit doit rester cohérent avec :

- README.md
- UI_GUIDE.md
- ARCHITECTURE – LatuColector.md
- ROADMAP.md
- GUIDE_COMMITS.md

---

# ⚙️ PIPELINE LATUCOLLECT

## 🔹 Pipeline interne

```text
Import → Lecture → Assemblage → Export
```

---

## 🔹 Flux utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🧩 WORKFLOW OFFICIEL LATUCOLLECT

## 🔹 Début de session

1. Ouvrir GitHub Desktop
2. Fetch origin
3. Vérifier les conflits
4. Lire la feuille de route
5. Identifier l’objectif du commit

---

## 🔹 Pendant le développement

### Règles obligatoires :

- ✔ 1 commit = 1 intention
- ✔ Code compilable
- ✔ Respect ALC
- ✔ Documentation mise à jour si nécessaire
- ✔ Cohérence avec la feuille de route
- ✔ Cohérence avec la documentation

---

### Fréquence :

Toutes les 30 à 60 minutes

---

# 🏷️ TYPES DE COMMITS (ALIGNÉS PROJET)

| Emoji | Type         | Utilisation             |
| ----- | ------------ | ----------------------- |
| ✨    | Feature      | Nouvelle fonctionnalité |
| ⛓️‍💥    | Bug          | Correction de bug       |
| 🛠️    | Fix          | Correction mineure      |
| ♻️    | Refactor     | Réorganisation code     |
| 📝    | Docs         | Documentation           |
| 🧪    | Test         | Tests                   |
| 🧹    | Cleanup      | Nettoyage               |
| 🔥    | Remove       | Suppression             |
| ⚙️    | Service      | Services Core           |
| 🏗️    | Architecture | Structure / MVVM        |

---

# 🧠 CONTEXTE LATUCOLLECT

Phase actuelle :

👉 UI WinUI (MVP validé)

Priorités :

- Arborescence projet
- Sélection fichiers
- Aperçu
- Export

---

# 🧩 PRÉCISION (IMPORTANT)

Indiquer si le commit concerne :

- UI → interface utilisateur
- ViewModel → logique UI
- Core → logique métier

---

## ✔ Exemple :

✨ [Feature][UI] Arborescence projet
⛓️‍💥 [Bug][Core] Correction export

---

# 📌 EXEMPLES

✔ Bon commit :

✨ [Feature][UI] Ajout arborescence projet

- Affichage dossiers et fichiers
- Checkbox sélection
- Navigation fonctionnelle
- Impact : base UI opérationnelle

---

✔ Bon commit :

✨ [Feature][UI] Aperçu temps réel

- Génération dynamique
- Format export respecté
- Mise à jour automatique
- Impact : visibilité utilisateur

---

❌ Mauvais commit :

update code

---

# ⚠️ NE PAS COMMIT SI

- Code non compilable
- Test non terminé
- UI incohérente
- Debug actif
- Fichiers inutiles présents

---

# ⚠️ ERREURS COURANTES

- Fichiers bin / obj commités
- Logs ajoutés
- Commits trop gros
- Commits vagues

---

# 🏁 FIN DE SESSION

1. Vérifier fichiers
2. Nettoyer
3. Commit propre
4. Push
5. Vérifier GitHub

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🧠 PHILOSOPHIE

Git est une mémoire.

Une mémoire mal tenue devient inutilisable.

---

# 🎯 OBJECTIF FINAL

- ✔ Historique clair
- ✔ Projet compréhensible
- ✔ Développement propre
