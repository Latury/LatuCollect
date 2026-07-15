<div align="center">

### **🗺️ ROADMAP – LATUCOLLECT**

### Feuille de route du projet

🔹 Vision du projet
🔹 Historique des versions
🔹 Fonctionnalités réalisées
🔹 Évolutions prévues

</div>

Cette feuille de route présente les objectifs du projet et son évolution.

Les fonctionnalités réellement implémentées sont documentées dans
📑 [PATCH NOTES](./PATCH_NOTES.md).

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

### **📖 Sommaire**

#### Général

- [🎯 01. Objectif général](#objectif-général)
- [⚙️ 02. Fonctionnement](#fonctionnement)
- [🧠 03. Philosophie](#philosophie)
- [🧩 04. Concepts clés](#concepts-cles)

#### Versions

- [🚀 05. Version 0.1.0 — Base du projet](#v010)
- [🚀 06. Version 0.2.0 — Résilience & consolidation](#v020)
- [🚀 07. Version 0.3.0 — Base simulation](#v030)
- [🚀 08. Version 0.4.0 — Stabilisation](#v040)
- [🚀 09. Version 0.5.0 — Expérience utilisateur](#v050)
- [🚀 10. Version 0.6.0 — Recherche & navigation](#v060)
- [🚀 11. Version 0.7.0 — Export & statistiques](#v070)
- [🚀 12. Version 0.8.0 — Architecture](#v080)
- [🚀 13. Version 0.9.0 — Optimisations](#v090)
- [🚀 14. Version 0.10.0 — Finalisation](#v100)
- [🚀 15. Version 0.11.0 — Stabilisation](#v110)
- [🚀 16. Version 0.12.0 — Performances](#v120)
- [🚀 17. Version 0.13.0 — Simplification du Core](#v130)
- [🚀 18. Version 0.14.0 — UX & comportements](#v140)
- [🚀 19. Version 0.15.0 — Architecture & Split MainViewModel](#v150)
- [🚀 20. Version 0.16.0 — Finalisation Split MainViewModel](#v160)
- [🚀 21. Version 0.17.0 — Stabilisation Async UI & finalisation architecture](#v170)
- [🎨 22. Version 0.18.0 — Refonte UI, thèmes & audit visuel](#v180)
- [🚀 23. Version 0.19.0 — Finalisation & distribution](#v190)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="objectif-général"></a>

### **🎯 01. Objectif général**

#### 🧭 Finalité

Construire une application WinUI 3 (.NET 8) permettant de :

- ✅ Charger un projet
- ✅ Naviguer dans sa structure
- ✅ Sélectionner des fichiers
- ✅ Générer un aperçu en temps réel
- ✅ Copier le contenu généré
- ✅ Exporter un document structuré

#### 💡 Principe

> LatuCollect est un outil simple, visuel et efficace, reposant sur un principe de copie intelligente.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="fonctionnement"></a>

### **🔄 02. Fonctionnement**

#### 🔄 Pipeline utilisateur

```text
Importer → Sélectionner → Aperçu → Exporter
```

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="philosophie"></a>

### **🧠 3. Philosophie**

#### 📐 Principes

- ✅ Simplicité
- ✅ Lisibilité
- ✅ Rapidité
- ✅ Aucun élément inutile

#### 📋 Règles du projet

**⚠️ RÈGLE CRITIQUE**

👉 Toute nouvelle fonctionnalité non prévue doit être ajoutée immédiatement dans la version en cours.

**⚙️ PRIORITÉ DE DÉVELOPPEMENT**

👉 Core → Stabilité → UX → UI

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="concepts-cles"></a>

### **🧩 4. Concepts clés**

#### 🌳 Arborescence du projet (CŒUR DE L’APP)

- Navigation dans les dossiers
- Affichage des fichiers
- Sélection via cases à cocher
- Filtrage via la recherche

---

#### 👁️ Aperçu en temps réel

- Affichage du document final
- Mise à jour automatique
- Gestion des états (vide / contenu)

---

#### 📄 Format d'export

```text
Chemin du fichier


(contenu du fichier)


----------------------------------------
```

---

#### 🎛️ Modes d'utilisation

- Mode simple → utilisation directe
- Mode expert → options avancées (futur)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v010"></a>

### **🚀 5. Version 0.1.0 — Core**

🟢 Statut : Terminée

🎯 Objectif : Mettre en place le premier pipeline fonctionnel de collecte et d’export.

---

### **🧩 Fonctionnalités principales**

- ✅ Import de fichiers et dossiers
- ✅ Lecture et assemblage du contenu
- ✅ Export TXT et Markdown

---

### 🏁 Résultat

- ✔ Premier pipeline de collecte opérationnel
- ✔ Export fonctionnel
- ✔ Base du projet validée

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v020"></a>

### **🚀 6. Version 0.2.0 — Interface principale**

🟢 Statut : Terminée

🎯 Objectif : Développer la première interface graphique complète de LatuCollect.

---

### **🧩 Fonctionnalités principales**

### 🖥️ Interface

- ✅ Mise en place de l'interface en 4 zones
- ✅ Arborescence du projet
- ✅ Navigation dans les dossiers
- ✅ Sélection des fichiers via des cases à cocher

---

### 🔎 Recherche

- ✅ Barre de recherche
- ✅ Filtrage dynamique de l'arborescence

---

### 👁️ Aperçu

- ✅ Génération en temps réel
- ✅ Affichage du document final
- ✅ Gestion des états (aucun fichier sélectionné)

---

### 📤 Export

- ✅ Export TXT / Markdown
- ✅ Confirmation utilisateur
- ✅ Bouton désactivé si aucun contenu

---

### 📋 Copie

- ✅ Copie du contenu généré
- ✅ Retour utilisateur

---

### 💬 Boîtes de dialogue

- ✅ Options
- ✅ Aide
- ✅ À propos
- ✅ Confirmation de fermeture

---

### 🏁 Résultat

- ✔ Première interface WinUI entièrement fonctionnelle
- ✔ Navigation et sélection opérationnelles
- ✔ Aperçu connecté au Core
- ✔ Export fonctionnel

> ✅ **MVP validé**

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v030"></a>

### **🚀 7. Version 0.3.0 — Système de simulation**

🟡 Statut : Terminée _(fonctionnalité supprimée en v0.13.0)_

🎯 Objectif : Mettre en place un environnement de simulation destiné aux tests et au développement, sans impacter le fonctionnement réel de l'application.

---

### **🧩 Fonctionnalités principales**

### 🧪 Système de simulation

- ✅ Activation et désactivation du mode simulation
- ✅ Intégration au pipeline de lecture et d’export
- ✅ Isolation complète du fonctionnement normal

---

### 🧪 Cas simulés

- ✅ Fichiers vides
- ✅ Chemins trop longs
- ✅ Erreurs de lecture
- ✅ Erreurs d’export

---

### 🖥️ Mode développeur

- ✅ Activation depuis l’interface
- ✅ Sélection du scénario de simulation
- ✅ Indication visuelle du mode actif

---

### 🏁 Résultat

- ✔ Environnement de test dédié au développement
- ✔ Aucun impact sur le fonctionnement en production
- ✔ A servi à valider les premières versions du projet

> ℹ️ Ce système a été supprimé en **version 0.13.0** afin de simplifier l'architecture et de réduire les effets de bord.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v040"></a>

### **🚀 8. Version 0.4.0 — Stabilité**

🟢 Statut : Terminée

🎯 Objectif : Renforcer la fiabilité de l'application et garantir un comportement prévisible dans les principaux cas d'utilisation.

---

### **🧩 Fonctionnalités principales**

### 🛡️ Gestion des erreurs

- ✅ Gestion des erreurs de lecture des fichiers
- ✅ Gestion des erreurs d’export
- ✅ Gestion des chemins trop longs

---

### ✅ Validation

- ✅ Garantie de cohérence entre l'aperçu et l'export (**Preview = Export**)

---

### 🏁 Résultat

- ✔ Application plus fiable
- ✔ Gestion des erreurs améliorée
- ✔ Cohérence Preview = Export validée
- ✔ Base solide pour les améliorations UX des versions suivantes

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v050"></a>

### **🚀 9. Version 0.5.0 — Expérience utilisateur**

🟢 Statut : Terminée

🎯 Objectif : Améliorer la lisibilité, la fluidité et le confort d'utilisation de l'application.

---

### **🧩 Fonctionnalités principales**

### 💬 Retour utilisateur

- ✅ Messages d'information et d'erreur plus clairs
- ✅ Retour visuel pour les principales actions
- ✅ Gestion des actions annulées

---

### 🔄 États de l'interface

- ✅ Gestion centralisée des états de l'interface
- ✅ Affichage conditionnel (chargement, contenu, erreur)
- ✅ Gestion des états vides

---

### 🖥️ Interface

- ✅ Amélioration de l'ergonomie générale
- ✅ Uniformisation des boîtes de dialogue
- ✅ Sélection globale des fichiers
- ✅ Optimisation de la disposition des éléments

---

### 👁️ Aperçu

- ✅ Affichage plus lisible
- ✅ Rendu adapté au code source
- ✅ Mise à jour en temps réel
- ✅ Navigation fluide dans les contenus volumineux

---

### 🚄 Performances

- ✅ Chargement asynchrone
- ✅ Protection contre les blocages de l'interface
- ✅ Gestion optimisée des gros projets
- ✅ Limitation automatique des projets volumineux (`MAX_NODES`, `MAX_DEPTH`)

---

### 🔎 Recherche

- ✅ Recherche dynamique
- ✅ Filtrage en temps réel
- ✅ Conservation de la hiérarchie de l'arborescence
- ✅ Recherche insensible à la casse

---

### 🏁 Résultat

- ✔ Interface plus intuitive
- ✔ Navigation plus fluide
- ✔ Meilleure réactivité sur les gros projets
- ✔ Expérience utilisateur nettement améliorée

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v060"></a>

### **🚀 10. Version 0.6.0 — Recherche & gestion des fichiers**

🟢 Statut : Terminée

🎯 Objectif : Améliorer la recherche, la gestion des fichiers et la navigation dans l'arborescence.

---

### **🧩 Fonctionnalités principales**

### 🔎 Recherche

- ✅ Recherche fiable et stable
- ✅ Gestion du cas « Aucun résultat »
- ✅ Filtrage dynamique des fichiers
- ✅ Optimisation des performances (debounce)

---

### 📁 Gestion des fichiers

- ✅ Exclusion automatique des dossiers système (`bin`, `obj`)
- ✅ Navigation optimisée dans les projets volumineux

---

### 🏁 Résultat

- ✔ Recherche plus rapide et plus fiable
- ✔ Navigation améliorée
- ✔ Meilleure gestion des gros projets
- ✔ Base préparée pour les optimisations futures

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v070"></a>

### **🚀 11. Version 0.7.0 — Export & statistiques**

🟢 Statut : Terminée

🎯 Objectif : Améliorer la qualité des exports et fournir des statistiques utiles sur les fichiers sélectionnés.

---

### **🧩 Fonctionnalités principales**

### 📤 Export

- ✅ Amélioration du format d'export Markdown
- ✅ Amélioration de la lisibilité des documents exportés
- ✅ Réorganisation de la structure d'export
- ✅ Gestion renforcée des erreurs d'export

---

### 📊 Statistiques

- ✅ Nombre de fichiers sélectionnés
- ✅ Taille totale des fichiers
- ✅ Nombre total de lignes
- ✅ Nombre total de caractères

---

### 🏁 Résultat

- ✔ Exports plus lisibles et plus fiables
- ✔ Informations complémentaires disponibles en temps réel
- ✔ Meilleure qualité des documents générés

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v080"></a>

### **🚀 12. Version 0.8.0 — Architecture**

🟢 Statut : Terminée

🎯 Objectif : Structurer l'application afin de renforcer la séparation entre l'interface utilisateur et le Core, et améliorer la maintenabilité du projet.

---

### **🧩 Fonctionnalités principales**

### 🧱 Architecture

- ✅ Identification de la logique métier restante dans le ViewModel
- ✅ Migration progressive de la logique métier vers les services Core
- ✅ Simplification du `MainViewModel`
- ✅ Respect du pipeline métier
- ✅ Structuration des services Core

---

### 🏁 Résultat

- ✔ Architecture plus claire et plus lisible
- ✔ Séparation UI / Core renforcée
- ✔ Services Core dédiés à chaque responsabilité
- ✔ `MainViewModel` recentré sur l'orchestration
- ✔ Pipeline métier cohérent et centralisé

---

### 📈 Évolution du projet

#### ✨ Bénéfices

- ✔ Plus maintenable
- ✔ Plus structuré
- ✔ Plus évolutif

👉 Base solide pour les optimisations prévues en **v0.9.0**.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v090"></a>

### **🚀 13. Version 0.9.0 — Optimisations**

🟢 Statut : Terminée

🎯 Objectif : Améliorer les performances, la fluidité de l'interface et préparer une architecture plus évolutive.

---

### **🧩 Fonctionnalités principales**

### 🚄 Performances

- ✅ Optimisation de la lecture des fichiers
- ✅ Mise en cache des contenus
- ✅ Réduction des allocations mémoire
- ✅ Amélioration du temps de génération de l'aperçu
- ✅ Réduction des recalculs inutiles

---

### 🖥️ Interface

- ✅ Amélioration de la réactivité
- ✅ Optimisation du rafraîchissement de l'aperçu
- ✅ Réduction des appels inutiles au Core
- ✅ Gestion améliorée des états de l'interface

---

### 🧠 Core

- ✅ Séparation du calcul des statistiques
- ✅ Réduction des responsabilités du service d'export
- ✅ Préparation à l'évolution du système de statistiques
- ✅ Amélioration du pipeline interne

---

### 🔄 Pipeline

- ✅ Optimisation du pipeline de traitement
- ✅ Maintien de la règle **Preview = Export**
- ✅ Suppression des traitements redondants
- ✅ Mise en cache des opérations de lecture et d'aperçu

---

### 🏁 Résultat

- ✔ Application plus rapide
- ✔ Interface plus fluide
- ✔ Pipeline plus performant
- ✔ Architecture plus structurée et plus maintenable

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v100"></a>

### **🚀 14. Version 0.10.0 — Finalisation du produit**

🟢 Statut : Terminée

🎯 Objectif : Finaliser l'application pour une utilisation réelle, stable et prête à être distribuée.

---

### **🧩 Fonctionnalités principales**

### ⚙️ Configuration utilisateur

- ✅ Sauvegarde des préférences utilisateur
- ✅ Chargement automatique des paramètres
- ✅ Gestion persistante des exclusions
- ✅ Sauvegarde du dernier dossier ouvert
- ✅ Gestion du thème et du mode développeur
- ✅ Réinitialisation des paramètres

---

### 📤 Export et statistiques

- ✅ Ajout de plusieurs modes d'export
- ✅ Persistance du mode sélectionné
- ✅ Gestion des exports volumineux
- ✅ Amélioration de la fiabilité des exports

---

### 🧠 Core

- ✅ Renforcement de la stabilité des services
- ✅ Optimisation du pipeline de traitement
- ✅ Amélioration de la gestion des gros projets
- ✅ Généralisation des traitements asynchrones

---

### 🚄 Performances

- ✅ Optimisation du cache
- ✅ Optimisation des statistiques
- ✅ Réduction des traitements inutiles

---

### 🖥️ Interface

- ✅ Amélioration de la sélection des fichiers
- ✅ Corrections de l'aperçu
- ✅ Messages utilisateur plus clairs
- ✅ Amélioration de l'expérience globale

---

### 🛡️ Stabilité

- ✅ Optimisation du rendu de l'interface
- ✅ Réduction des effets de scintillement
- ✅ Amélioration de la stabilité générale

---

### 🧾 Journaux (Logs)

- ✅ Mise en place d'un système de journaux (logs) configurable

---

### 🧪 Validation

- ✅ Renforcement des tests des principaux services
- ✅ Validation de la stabilité générale

---

### 🏁 Résultat

- ✔ Application prête pour une utilisation réelle
- ✔ Configuration utilisateur persistante
- ✔ Export plus fiable
- ✔ Interface plus stable et plus agréable
- ✔ Base solide pour la poursuite du développement

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v110"></a>

### **🚀 15. Version 0.11.0 — Stabilisation du Core & UX**

🟢 Statut : Terminée

🎯 Objectif : Renforcer la stabilité du Core, fiabiliser le TreeView et améliorer l'expérience utilisateur.

---

### **🧩 Fonctionnalités principales**

### 🌳 TreeView & sélection

- ✅ Stabilisation de la sélection parent ↔ enfants
- ✅ Introduction du mode tri-state
- ✅ Synchronisation UI ↔ ViewModel
- ✅ Amélioration de la navigation et des interactions
- ✅ Renforcement des tests liés au TreeView

> ℹ️ Le système **tri-state** introduit dans cette version a été volontairement simplifié en **v0.12.0** afin de réduire la complexité et les effets de bord.

---

### 📂 Gestion des exclusions

- ✅ Exclusion de fichiers et dossiers depuis l'arborescence
- ✅ Synchronisation avec la configuration utilisateur
- ✅ Prise en charge des exclusions protégées
- ✅ Mise à jour dynamique de l'arborescence

---

### 🔎 Recherche

- ✅ Stabilisation du filtrage de l'arborescence
- ✅ Conservation de la structure réelle
- ✅ Amélioration de la visibilité des résultats
- ✅ Recherche plus fiable et plus fluide

---

### 🧠 Core

- ✅ Renforcement de la robustesse du `FileReader`
- ✅ Amélioration de la gestion du cache
- ✅ Validation des cas d'erreur
- ✅ Optimisation de la gestion des fichiers volumineux

---

### 📤 Export et statistiques

- ✅ Validation renforcée des exports
- ✅ Fiabilisation des statistiques
- ✅ Garantie **Preview = Export**

---

### 🧪 Tests

- ✅ Extension importante de la couverture des tests
- ✅ Validation du TreeView
- ✅ Validation du FileReader
- ✅ Validation de l'export et des statistiques

---

### 🏁 Résultat

- ✔ Core plus robuste et plus prévisible
- ✔ TreeView largement stabilisé
- ✔ Recherche plus fiable
- ✔ Export plus fiable
- ✔ Réduction importante des effets de bord
- ✔ Base solide pour les optimisations de la version **0.12.0**

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v120"></a>

### **🚀 16. Version 0.12.0 — Performances**

🟢 Statut : Terminée

🎯 Objectif : Améliorer les performances de l'application et renforcer la stabilité sur les projets volumineux.

---

### **🧩 Fonctionnalités principales**

### 📂 Import

- ✅ Optimisation du chargement des projets volumineux
- ✅ Amélioration de la gestion des arborescences importantes
- ✅ Stabilisation de l'affichage des gros projets

---

### ⚡ Cache & performances

- ✅ Optimisation du système de cache
- ✅ Invalidation automatique des fichiers modifiés
- ✅ Réduction des rafraîchissements inutiles
- ✅ Garantie **Preview = Export** après modification des fichiers

---

### 📊 Statistiques

- ✅ Optimisation du calcul des statistiques
- ✅ Amélioration de la fiabilité sur les cas extrêmes
- ✅ Synchronisation des statistiques avec l'aperçu en temps réel

---

### 📤 Export

- ✅ Renforcement de la gestion des exports volumineux
- ✅ Amélioration de la robustesse face aux erreurs disque
- ✅ Optimisation de la consommation mémoire
- ✅ Gestion des fichiers verrouillés

---

### 🌳 Recherche & TreeView

- ✅ Suppression du système tri-state pour simplifier la sélection
- ✅ Optimisation de la recherche et du filtrage
- ✅ Amélioration de la sélection sur les gros projets
- ✅ Réduction des risques de blocage de l'interface

---

### 🧪 Validation

- ✅ Renforcement des tests automatisés
- ✅ Validation des scénarios de charge
- ✅ Vérification de la stabilité sur les gros volumes

---

### 🏁 Résultat

- ✔ Application plus rapide
- ✔ Meilleure gestion des projets volumineux
- ✔ Interface plus fluide
- ✔ Pipeline Preview = Export renforcé
- ✔ Base solide pour les évolutions architecturales suivantes

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v130"></a>

### **🚀 17. Version 0.13.0 — Simplification du Core**

🟢 Statut : Terminée

🎯 Objectif : Simplifier l'architecture en supprimant le système de simulation et en réduisant les dépendances inutiles.

---

### **🧩 Fonctionnalités principales**

### 🧠 Core

- ✅ Suppression complète du système de simulation
- ✅ Nettoyage des dépendances associées
- ✅ Simplification du pipeline principal

---

### 🖥️ Interface

- ✅ Suppression des éléments liés à la simulation
- ✅ Nettoyage des ViewModels et des bindings
- ✅ Conservation du mode développeur

---

### ⚙️ Configuration

- ✅ Suppression de la configuration de simulation
- ✅ Vérification de l'absence de dépendances résiduelles

---

### 🧹 Architecture

- ✅ Suppression du code devenu inutile
- ✅ Réduction du couplage entre le Core et l'interface
- ✅ Allègement du `MainViewModel`
- ✅ Préparation des futurs refactors architecturaux

---

### 🧪 Validation

- ✅ Fonctionnement général validé
- ✅ Garantie **Preview = Export** conservée
- ✅ Vérification de la stabilité de l'interface

---

### 🏁 Résultat

- ✔ Architecture simplifiée
- ✔ Réduction du code mort
- ✔ Pipeline plus lisible
- ✔ Base solide pour les prochains refactors architecturaux

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v140"></a>

### **🚀 18. Version 0.14.0 — UX & comportements**

🟢 Statut : Terminée

🎯 Objectif : Améliorer l'expérience utilisateur et renforcer la stabilité des interactions de l'interface.

---

### **🧩 Fonctionnalités principales**

### 📂 Import

- ✅ Optimisation du chargement des projets
- ✅ Amélioration de la gestion des imports volumineux
- ✅ Stabilisation de l'affichage partiel

---

### 🖥️ Interface

- ✅ Amélioration de la gestion des états de l'application
- ✅ Réinitialisation plus fiable du projet chargé
- ✅ Messages utilisateur plus explicites

---

### ✨ Expérience utilisateur

- ✅ Amélioration de la gestion des exports partiels
- ✅ Fiabilisation de la sélection des dossiers
- ✅ Ajout de l'ouverture dans l'explorateur
- ✅ Persistance de l'état d'ouverture de l'arborescence
- ✅ Amélioration de la gestion des exclusions

---

### 🌳 TreeView & aperçu

- ✅ Stabilisation des interactions du TreeView
- ✅ Réduction des rafraîchissements inutiles
- ✅ Amélioration du pipeline d'aperçu asynchrone
- ✅ Gestion des aperçus obsolètes
- ✅ Interface plus fluide lors des traitements

---

### 🧪 Validation

- ✅ Validation des scénarios d'utilisation
- ✅ Vérification de la stabilité des exclusions
- ✅ Contrôle de la cohérence entre visibilité et sélection

---

### 🏁 Résultat

- ✔ Interface plus fluide
- ✔ Navigation plus stable
- ✔ Expérience utilisateur améliorée
- ✔ Meilleure gestion des gros projets
- ✔ Base prête pour les refactors architecturaux suivants

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v150"></a>

### **🚀 19. Version 0.15.0 — Architecture & Split MainViewModel**

🟢 Statut : Terminée

🎯 Objectif : Renforcer l'architecture ALC, réduire les responsabilités du `MainViewModel` et préparer les futurs refactors asynchrones.

---

### **🧩 Fonctionnalités principales**

### 🏗️ Architecture

- ✅ Renforcement de la séparation entre le Core et l'interface
- ✅ Introduction des interfaces de services
- ✅ Réorganisation des modèles d'export
- ✅ Préparation de la séparation `AppConfig` / `UserConfig`

---

### 🧱 Core

- ✅ Amélioration de la robustesse du `FileReader`
- ✅ Homogénéisation des résultats de lecture
- ✅ Renforcement de la gestion des encodages
- ✅ Préparation des futures évolutions de la configuration

---

### 🖥️ Split MainViewModel

- ✅ Début du découpage progressif du `MainViewModel`
- ✅ Création de `LogsViewModel`
- ✅ Création de `TreeViewViewModel`
- ✅ Préparation de `PreviewViewModel`
- ✅ Préparation de `SettingsViewModel`

---

### 🧾 Journaux (Logs)

- ✅ Déplacement de la logique de présentation vers l'interface
- ✅ Centralisation de la gestion des journaux dans un ViewModel dédié

---

### 🧪 Validation

- ✅ Compatibilité des bindings existants conservée
- ✅ Préservation de la stabilité de l'interface
- ✅ Migration progressive sans régression

---

### 🏁 Résultat

- ✔ Architecture ALC renforcée
- ✔ `MainViewModel` allégé
- ✔ Responsabilités mieux réparties
- ✔ Base prête pour la poursuite du Split MainViewModel

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v160"></a>

### **🚀 20. Version 0.16.0 — Finalisation du Split MainViewModel**

🟢 Statut : Terminée

🎯 Objectif : Poursuivre le découpage du `MainViewModel` afin de renforcer l'architecture et de préparer la suppression progressive des redirections temporaires.

---

### **🧩 Fonctionnalités principales**

### 🖥️ Split MainViewModel

- ✅ Poursuite de l'extraction du domaine **Preview**
- ✅ Migration des états et de la logique d'aperçu
- ✅ Préparation de la migration du domaine **Settings**
- ✅ Finalisation de la migration du **TreeView**
- ✅ Réduction des dépendances du `MainViewModel`

---

### 🧠 Architecture

- ✅ Réduction de la taille du `MainViewModel`
- ✅ Répartition plus claire des responsabilités
- ✅ Préparation de la suppression progressive des redirections
- ✅ Renforcement de l'architecture ALC

---

### 🧪 Validation

- ✅ Validation des migrations réalisées
- ✅ Vérification de la compatibilité des bindings existants
- ✅ Absence de régression fonctionnelle
- ✅ Stabilité de l'interface préservée

---

### 🏁 Résultat

- ✔ `MainViewModel` allégé
- ✔ Architecture plus structurée et plus maintenable
- ✔ ViewModels spécialisés progressivement mis en place
- ✔ Migration réalisée sans impact utilisateur
- ✔ Base prête pour la finalisation de l'architecture en **v0.17.0**

---

### 📐 Décision d'architecture

Le découpage du `MainViewModel` reste volontairement progressif afin de préserver :

- ✔ La stabilité de l'interface
- ✔ La compatibilité des bindings existants
- ✔ Les traitements asynchrones
- ✔ La stabilité du TreeView

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v170"></a>

### **🚀 21. Version 0.17.0 — Stabilisation Async UI & Finalisation Architecture**

🟢 Statut : Terminée

🎯 Objectif : Finaliser le découpage du `MainViewModel`, renforcer l'architecture ALC et fiabiliser les interactions asynchrones de l'interface.

---

### **🧩 Fonctionnalités principales**

### 🧠 Architecture

- ✅ Création de `ExportViewModel`
- ✅ Migration des états liés à l'export
- ✅ Migration des validations d'export
- ✅ Réduction des responsabilités du `MainViewModel`
- ✅ Finalisation des exclusions système et utilisateur
- ✅ Exploitation complète de l'état `IsPartial`
- ✅ Préservation de la cohérence Preview = Export
- ✅ Renforcement de l'architecture ALC

---

### 🔄 Interface asynchrone

- ✅ Stabilisation du pipeline Preview asynchrone
- ✅ Gestion des sélections massives
- ✅ Gestion des clics rapides
- ✅ Réduction des rafraîchissements inutiles
- ✅ Synchronisation recherche / sélection
- ✅ Stabilisation des mises à jour après exclusions
- ✅ Amélioration de la fluidité générale de l'interface

---

### 👁️ Aperçu & Export

- ✅ Indication visuelle des aperçus tronqués
- ✅ Distinction explicite entre aperçu limité et export complet
- ✅ Validation de la règle **Preview = Export**
- ✅ Validation des performances Preview / Export
- ✅ Messages utilisateur améliorés

---

### 🌳 TreeView

- ✅ Validation de la sélection hiérarchique
- ✅ Validation des performances sur les gros projets
- ✅ Stabilisation de la sélection massive
- ✅ Vérification de la cohérence des interactions utilisateur

---

### 🧾 Journaux (Logs)

- ✅ Validation de l'architecture du système de logs
- ✅ Stabilisation de `LogsViewModel`
- ✅ Gestion optimisée de la mémoire
- ✅ Conservation du filtrage et des niveaux de logs

---

### 🧪 Validation

- ✅ Validation des traitements asynchrones
- ✅ Suppression des temporisations artificielles dans les tests
- ✅ Vérification de la compatibilité WinUI
- ✅ Validation des interactions Preview, Export et TreeView
- ✅ Absence de régression fonctionnelle

---

### 🏁 Résultat

- ✔ Architecture plus structurée
- ✔ MainViewModel allégé
- ✔ Interface plus stable
- ✔ Traitements asynchrones fiabilisés
- ✔ Tests plus robustes
- ✔ Base solide pour les prochaines évolutions

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v180"></a>

### **🎨 22. Version 0.18.0 — Refonte UI, thèmes & audit visuel**

📌 Statut : 🟨 Audit graphique terminé — Implémentation en cours

🎯 Objectif : Définir et implémenter l'identité visuelle officielle de LatuCollect afin d'offrir une interface moderne, cohérente et harmonisée, tout en préservant l'architecture ALC, le pipeline utilisateur et les principes de simplicité de l'application.

---

### **🧩 Fonctionnalités principales**

### 🎨 Thèmes

- ⬜ Finalisation du thème clair
- ⬜ Finalisation du thème sombre
- ⬜ Centralisation de la palette de couleurs
- ⬜ Suppression des couleurs codées en dur
- ⬜ Uniformisation des couleurs d'état (succès, avertissement, erreur)

---

### 🖥️ Interface utilisateur

- ⬜ Harmonisation des espacements
- ⬜ Harmonisation des alignements
- ⬜ Amélioration de la hiérarchie visuelle
- ⬜ Uniformisation de la typographie
- ⬜ Uniformisation des icônes
- ⬜ Harmonisation des composants WinUI

---

### 🔘 Composants

- ⬜ Harmonisation des boutons
- ⬜ Harmonisation des cartes
- ⬜ Harmonisation des dialogues
- ⬜ Uniformisation des états visuels
- ⬜ Amélioration des feedbacks utilisateur

---

### 📐 Structure de l'application

**Structure conservée :**

- ✔ Gauche → Projet
- ✔ Centre → Options
- ✔ Droite → Aperçu
- ✔ Bas → Actions

**Améliorations visuelles prévues :**

- ⬜ Amélioration de la lisibilité de l'arborescence
- ⬜ Amélioration du confort de lecture de l'aperçu
- ⬜ Renforcement de la hiérarchie visuelle
- ⬜ Meilleure mise en valeur des actions principales

---

### 📚 Audit graphique

Objectif :

Définir l'identité visuelle officielle de LatuCollect.

Résultat :

- ✅ 14 audits documentés
- ✅ Design System défini
- ✅ Principes graphiques validés
- ✅ Référence officielle : UI_AUDIT.md

---

### 🎨 Audit du Design System

Validation des principes graphiques de chaque composant :

- ✅ Thème général
- ✅ Palette de couleurs
- ✅ Typographie
- ✅ Cartes
- 🟨 Boutons (principes validés, style final à confirmer)
- ✅ Arborescence
- ✅ Zone Aperçu
- ✅ Zone Actions
- ✅ Messages utilisateur
- ✅ Journaux (Logs)
- ✅ Paramètres
- ✅ Icônes
- ✅ Espacements

Pour chaque sujet :

- ✅ Analyse de plusieurs propositions
- ✅ Comparaison des avantages et des inconvénients
- ✅ Présentation de références visuelles
- ✅ Validation avant implémentation
- ✅ Respect de Fluent Design et de WinUI 3

---

### 🏁 Résultat attendu

- ⬜ Identité graphique unifiée
- ⬜ Interface moderne et harmonisée
- ⬜ Meilleure lisibilité
- ⬜ Composants visuels cohérents
- ⬜ Thèmes clair et sombre finalisés
- ✔ Architecture ALC préservée
- ✔ Pipeline utilisateur inchangé
- ✔ Philosophie de simplicité conservée

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="v190"></a>

### **🚀 23. Version 0.19.0 — Finalisation & Distribution**

📌 Statut : ⬜ Prévue

🎯 Objectif : Finaliser LatuCollect pour une utilisation en production, valider sa stabilité et préparer sa distribution.

---

### **🧪 Validation de l'application**

### 🧪 Stabilité

- ⬜ Validation sur de très gros projets
- ⬜ Validation des exports massifs
- ⬜ Validation de la consommation mémoire
- ⬜ Validation des performances du TreeView
- ⬜ Vérification de la stabilité générale

---

### ⚙️ Build

- ⬜ Génération d'une version Release
- ⬜ Validation complète de la compilation
- ⬜ Exécution de l'ensemble des tests
- ⬜ Vérification de l'absence de régressions

---

### 📦 Distribution

- ⬜ Création de l'installateur
- ⬜ Validation de l'installation
- ⬜ Gestion des dépendances
- ⬜ Support multi-architecture
- ⬜ Choix du dossier d'installation
- ⬜ Création des raccourcis
- ⬜ Validation des mises à jour

---

### 🏁 Résultat attendu

LatuCollect est désormais :

- ✔ Stable
- ✔ Fiable
- ✔ Maintenable
- ✔ Distribuable
- ✔ Prêt pour la production

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
