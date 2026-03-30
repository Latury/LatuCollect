# 📁 STRUCTURE DU PROJET – LATUCOLLECT

Ce document décrit l’organisation des dossiers du projet LatuCollect.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🎯 Objectif

Permettre :

- une navigation rapide
- une compréhension immédiate
- une maintenance facilitée

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🧩 Structure principale

```text id="final_struct"
LatuCollect/
│
├── Core/
│   ├── Services/
│   │   ├── Import/
│   │   │   └── FileImportService.cs
│   │   │
│   │   ├── Collection/
│   │   │   └── FileCollectionService.cs
│   │   │
│   │   ├── Export/
│   │   │   └── FileExportService.cs
│   │   │
│   │   └── Utils/
│   │       └── ClipboardService.cs → copie rapide du résultat dans le presse-papier
│   │
│   ├── Models/
│   ├── Interfaces/
│   └── Helpers/
│
├── UI/
│   ├── Console/
│   └── WinUI/
│       ├── Views/
│       └── ViewModels/
│
├── Resources/
│   ├── Colors/
│   ├── Styles/
│   └── Dimensions/
│
├── Tests/
│
├── README.md
├── UI_GUIDE.md
├── ARCHITECTURE.md
├── GUIDE_UTILISATEUR.md
├── FEUILLE_DE_ROUTE.md
├── PATCH_NOTES.md
├── LICENSE.md
├── .editorconfig
├── .gitignore
```

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

# 🧠 Description des dossiers

## Core/

Contient toute la logique métier.

👉 responsabilités :

- importer les fichiers
- lire le contenu
- générer le document final
- exporter

👉 aucun code UI

---

## UI/

Contient l’interface utilisateur WinUI.

👉 responsabilités :

- affichage
- interactions utilisateur
- aperçu du document

---

## 🧠 Architecture MVVM

Structure UI :

```text id="mvvm"
View → ViewModel → Core
```

- View = XAML (UI)
- ViewModel = logique UI
- Core = logique métier

---

# 🧩 Fonctionnement global

```text id="flow"
Importer → Sélectionner → Aperçu → Exporter
```

---

# 🖥️ Interface utilisateur

```text id="ui_layout"
Gauche → Arborescence projet
Centre → Options (format)
Droite → Aperçu
Bas → Export
```

---

# 🌳 Arborescence projet (IMPORTANT)

L’application repose sur :

- navigation dans les dossiers
- affichage des fichiers
- sélection via checkbox

---

# 🔄 Communication

```text id="flow_arch"
UI → ViewModel → Core
```

❌ jamais l’inverse

---

# ⚠️ Règles importantes

- Core ne dépend jamais de UI
- UI peut dépendre de Core
- 1 dossier = 1 responsabilité
- aucune logique métier dans UI

---

# 🧠 Évolution future

Possibilité d’ajouter :

- Analyse
- Déduplication
- Organisation

👉 uniquement si besoin réel

---

# 🧭 Philosophie

- simplicité
- lisibilité
- efficacité

👉 éviter toute complexité inutile
