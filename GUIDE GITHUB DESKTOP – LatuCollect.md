# 🧠 GUIDE GITHUB DESKTOP – LATUCOLLECT

Guide pédagogique officiel pour la gestion du versioning du projet LatuCollect.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🎯 OBJECTIF

Dans LatuCollect, Git est un élément central du projet.

Chaque commit doit être :

- structuré
- compréhensible
- aligné avec la feuille de route
- cohérent avec l’architecture ALC
- cohérent avec les documents du projet

---

# 📚 DOCUMENTS DE RÉFÉRENCE

Chaque commit doit rester cohérent avec :

- README.md
- UI_GUIDE.md
- ARCHITECTURE.md
- FEUILLE_DE_ROUTE.md

---

# 🧩 WORKFLOW OFFICIEL LATUCOLLECT

## Début de session

1. Ouvrir GitHub Desktop
2. Fetch origin
3. Vérifier les conflits
4. Lire la feuille de route
5. Identifier l’objectif du commit

---

## Pendant le développement

Règles obligatoires :

- 1 commit = 1 intention
- Code compilable
- Respect ALC
- Documentation mise à jour si nécessaire

Fréquence :

Toutes les 30 à 60 minutes

---

## Structure du commit

Summary :

[Type] Message clair

Description :

- Description simple
- Description technique
- Fichiers concernés
- Impact global

---

# 🏷️ TYPES DE COMMITS

- [ADD] → ajout
- [FIX] → correction
- [REF] → refactor
- [DOC] → documentation
- [TEST] → tests

---

# 🧠 CONTEXTE LATUCOLLECT

Phase actuelle :

👉 UI WinUI en développement

Priorités :

- arborescence projet
- sélection fichiers
- aperçu
- export

---

# 🧩 PRÉCISION (IMPORTANT)

Indiquer si le commit concerne :

- UI → interface utilisateur
- CORE → logique métier

Exemple :

[ADD][UI] Arborescence projet
[FIX][CORE] Correction export

---

# 📌 EXEMPLES

✔ Bon commit :

[ADD][UI] Arborescence projet

- Affichage dossiers et fichiers
- Checkbox sélection
- Navigation fonctionnelle
- Impact : base UI opérationnelle

---

✔ Bon commit :

[ADD][UI] Aperçu temps réel

- Génération dynamique
- Format export respecté
- Mise à jour automatique
- Impact : visibilité utilisateur

---

❌ Mauvais commit :

update code

---

# ⚠️ NE PAS COMMIT SI

- code non compilable
- test non terminé
- UI incohérente
- debug actif
- fichiers inutiles présents

---

# ⚠️ ERREURS COURANTES

- fichiers bin / obj commités
- logs ajoutés
- commits trop gros
- commits vagues

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

- historique clair
- projet compréhensible
- développement propre
