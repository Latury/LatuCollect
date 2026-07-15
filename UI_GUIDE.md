<div align="center">

# 🖥️ UI_GUIDE – LATUCOLLECT

### Guide officiel de l'interface utilisateur

🔹 Organisation de l'interface
🔹 Comportement utilisateur
🔹 Règles UX/UI
🔹 Composants de l'application

</div>

Ce document décrit l'organisation officielle de l'interface utilisateur de **LatuCollect**.

Il présente la structure de la fenêtre principale, les différentes zones de l'interface, les comportements attendus ainsi que les règles UX/UI garantissant une interface simple, cohérente et conforme à l'architecture **ALC**.

> [!IMPORTANT]
> Ce document constitue la **référence officielle** concernant l'interface utilisateur de **LatuCollect**.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

## 📑 Sommaire

- [🎯 01. Objectif](#objectif)
- [🧠 02. Concept général](#concept-general)
- [🧩 03. Structure principale](#structure-principale)
- [💬 04. Feedback utilisateur](#feedback-utilisateur)
- [🟦 05. Zone gauche — Projet](#zone-gauche-projet)
- [🟨 06. Zone centre — Options](#zone-centre-options)
- [🟩 07. Zone droite — Aperçu](#zone-droite-apercu)
- [🔻 08. Zone basse — Actions](#zone-basse-actions)
- [🖥️ 09. Stabilité de l'interface](#stabilite-interface)
- [🚄 10. Performances de l'interface](#performances-interface)
- [🧠 11. Règles de l'interface](#regles-interface)
- [🔮 12. Évolutions de l'interface](#evolutions-interface)
- [🧠 13. Règles UX](#regles-ux)
- [⚠️ 14. Interdits](#interdits)
- [🎨 15. Évolution de l'interface](#evolution-ui)
- [📍 16. État actuel](#etat-actuel)
- [🎯 17. Objectif global](#objectif-global)

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="objectif"></a>

### **🎯 01. Objectif**

#### 📋 Vue d'ensemble

Cette section présente les principaux objectifs de l'interface utilisateur de **LatuCollect**.

L'interface est conçue afin d'offrir une expérience simple, cohérente et efficace tout en respectant les principes de l'architecture **ALC**.

---

#### 🎯 Objectifs

- ✅ Proposer une interface simple et intuitive.
- ✅ Faciliter la compréhension des principales fonctionnalités.
- ✅ Garantir une navigation claire et prévisible.
- ✅ Rendre les actions essentielles rapidement accessibles.
- ✅ Préserver une interface cohérente au fil des évolutions.

---

#### 💡 Principe fondamental

> L'interface utilisateur privilégie la simplicité, la lisibilité et la cohérence.

> Toute évolution de l'interface doit améliorer l'expérience utilisateur sans modifier les principes fondamentaux de **LatuCollect** ni introduire de complexité inutile.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="concept-general"></a>

### **🧠 02. Concept général**

#### 📋 Vue d'ensemble

Cette section présente le principe de fonctionnement général de l'interface utilisateur de **LatuCollect**.

L'ensemble de l'interface est organisé autour d'un parcours utilisateur simple, linéaire et prévisible afin de faciliter la prise en main de l'application.

---

#### 🔄 Parcours utilisateur

Le parcours utilisateur repose sur le flux suivant :

```text
Importer → Sélectionner → Aperçu → Exporter
```

---

#### 📋 Principes

- ✅ Chaque étape possède une responsabilité clairement définie.
- ✅ Le parcours utilisateur reste simple et prévisible.
- ✅ L'interface accompagne naturellement l'utilisateur tout au long de son utilisation.
- ✅ Les traitements internes restent transparents pour l'utilisateur.
- ✅ Le principe **`Preview = Export`** est préservé dans le fonctionnement normal de l'application.

---

#### 💡 Principe fondamental

> Toute l'interface de **LatuCollect** est conçue autour de ce parcours utilisateur.

> Les évolutions de l'interface doivent préserver ce fonctionnement afin de garantir une expérience utilisateur cohérente et prévisible.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="structure-principale"></a>

### **🧩 03. Structure principale**

#### 📋 Vue d'ensemble

Cette section présente l'organisation générale de l'interface utilisateur de **LatuCollect**.

L'interface est organisée en quatre zones ayant chacune une responsabilité clairement définie afin de garantir une navigation simple, une utilisation cohérente et un comportement prévisible.

---

#### 🖥️ Organisation de l'interface

```text
Gauche → Projet

Centre → Options

Droite → Aperçu

Bas → Actions
```

---

#### 📋 Répartition des zones

- **Gauche** → Projet (arborescence des fichiers).
- **Centre** → Options (configuration et paramètres).
- **Droite** → Aperçu du contenu généré.
- **Bas** → Actions principales de l'application.

---

#### ⚠️ Règle fondamentale

Cette organisation constitue la structure officielle de l'interface utilisateur de **LatuCollect**.

- ✅ Chaque zone possède une responsabilité clairement définie.
- ✅ Chaque fonctionnalité est regroupée dans la zone qui lui est dédiée.
- ✅ La disposition générale ne doit pas être modifiée.
- ✅ Toute évolution de l'interface doit préserver cette organisation.

---

#### 💡 Principe fondamental

> L'organisation de l'interface est volontairement simple et stable.

> Toute évolution doit préserver cette structure afin de garantir une expérience utilisateur cohérente et prévisible.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="feedback-utilisateur"></a>

### **💬 04. Feedback utilisateur**

#### 📋 Vue d'ensemble

Cette section présente les différents retours fournis par l'interface utilisateur afin d'informer l'utilisateur sur l'état des opérations en cours ou leur résultat.

Les messages doivent rester simples, explicites et non bloquants afin de préserver une expérience utilisateur fluide.

---

#### 🎯 Objectifs

- ✅ Informer l'utilisateur de l'état des opérations.
- ✅ Signaler les succès, les avertissements et les erreurs.
- ✅ Guider l'utilisateur sans interrompre son utilisation.
- ✅ Fournir un retour immédiat après chaque action importante.

---

#### 📋 Exemples

- ✅ Export terminé avec succès.
- ❌ Échec de l'export.
- ⚠️ Aucun fichier sélectionné.
- ✅ Contenu copié dans le presse-papiers.

---

#### 📋 Comportement

Les messages de retour doivent respecter les principes suivants :

- ✅ Être non bloquants.
- ✅ Être affichés temporairement.
- ✅ Être intégrés à l'interface utilisateur.
- ✅ Éviter les fenêtres de dialogue inutiles pendant les opérations courantes.

---

#### 💡 Bonnes pratiques

- ✅ Utiliser des messages courts, explicites et compréhensibles.
- ✅ Éviter les messages techniques destinés au développeur.
- ✅ Informer sans interrompre l'utilisateur.
- ✅ Adapter le message au contexte de l'action réalisée.
- ✅ Conserver un comportement cohérent dans l'ensemble de l'application.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="zone-gauche-projet"></a>

### **🟦 05. Zone gauche — Projet**

#### 📋 Vue d'ensemble

Cette section présente la zone **Projet** de l'interface utilisateur.

Elle permet de charger un projet, de parcourir son arborescence, de rechercher des fichiers et de sélectionner les éléments qui seront utilisés par le pipeline de **LatuCollect**.

---

#### ⚙️ Fonctionnalités

- ✅ Chargement d'un dossier.
- ✅ Barre de recherche.
- ✅ Affichage de l'arborescence.
- ✅ Sélection des fichiers via des cases à cocher.

---

#### 🔄 Comportement

- ✅ Chargement asynchrone.
- ✅ Navigation fluide.
- ✅ Mise à jour dynamique de l'arborescence.
- ✅ Synchronisation avec les autres zones de l'interface.

---

#### 🔍 Recherche

La recherche permet de filtrer dynamiquement l'arborescence tout en conservant sa structure réelle.

- ✅ Recherche insensible à la casse.
- ✅ Débounce pour limiter les recherches successives.
- ✅ Filtrage dynamique.
- ✅ Conservation de l'arborescence réelle.
- ✅ Expansion automatique des nœuds concernés.
- ✅ Restauration de l'état de l'arborescence après la recherche.

---

#### ☑️ Sélection

La sélection permet de choisir les fichiers qui seront utilisés pour l'aperçu et l'export.

- ✅ Sélection multiple.
- ✅ Synchronisation parent ↔ enfants.
- ✅ Mise à jour immédiate de l'aperçu.
- ✅ Comportement simple, cohérent et prévisible.

---

#### ⚠️ Cas particuliers

- ✅ Les dossiers invalides sont ignorés.
- ✅ Les accès refusés sont ignorés.
- ✅ Un message est affiché lorsqu'aucun résultat ne correspond à la recherche.

---

#### 💡 Bonnes pratiques

- ✅ Limiter les rechargements complets de l'arborescence.
- ✅ Préserver la fluidité pendant les recherches.
- ✅ Conserver une navigation simple et prévisible.
- ✅ Synchroniser automatiquement la sélection avec l'aperçu.
- ✅ Préserver l'arborescence réelle lors des opérations de filtrage.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="zone-centre-options"></a>

### **🟨 06. Zone centre — Options**

#### 📋 Vue d'ensemble

Cette section présente la zone **Options** de l'interface utilisateur.

Elle regroupe les principaux paramètres de l'application ainsi que les actions permettant d'interagir avec le contenu généré et les différentes fonctionnalités de **LatuCollect**.

---

#### ⚙️ Fonctionnalités

- ✅ Choix du format d'export (TXT / Markdown).
- ✅ Copie du contenu généré.
- ✅ Ouverture du dossier courant dans l'explorateur.

---

#### 🚪 Accès

La zone **Options** permet également d'accéder aux principales fonctionnalités complémentaires de l'application.

- ✅ Paramètres.
- ✅ Statistiques.
- ✅ Aide.
- ✅ À propos.
- ✅ Quitter.

---

#### 👨🏻‍💻 Mode développeur

Le mode développeur regroupe les outils destinés au diagnostic et au développement de l'application.

##### 📋 Principes

- ✅ Désactivé par défaut.
- ✅ Réservé au développement et au diagnostic.
- ✅ Aucun impact sur le fonctionnement normal de l'application.

---

#### 📊 Statistiques

Les statistiques sont calculées automatiquement à partir des données produites par le pipeline.

- ✅ Nombre de fichiers.
- ✅ Nombre de lignes.
- ✅ Nombre de caractères.
- ✅ Taille totale.
- ✅ Mise à jour en temps réel.
- ✅ Calcul en arrière-plan.

---

#### 📋 Actions

**📄 Copier**

- ✅ Copie le contenu de l'aperçu.
- ✅ Affiche un retour utilisateur après l'opération.

---

**📂 Ouvrir dans l'explorateur**

- ✅ Ouvre le dossier actuellement sélectionné.
- ✅ Vérifie la validité du dossier.
- ✅ Informe l'utilisateur en cas d'erreur.

---

#### 💡 Bonnes pratiques

- ✅ Regrouper les fonctionnalités par domaine.
- ✅ Fournir un retour utilisateur après chaque action importante.
- ✅ Limiter les interruptions de l'utilisateur.
- ✅ Préserver une interface simple et cohérente.
- ✅ Garantir un accès rapide aux principales fonctionnalités.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="zone-droite-apercu"></a>

### **🟩 07. Zone droite — Aperçu**

#### 📋 Vue d'ensemble

Cette section présente la zone **Aperçu** de l'interface utilisateur.

Elle affiche le contenu généré par le **Core** avant l'export et permet à l'utilisateur de visualiser le résultat final des fichiers sélectionnés.

---

#### 🎯 Fonction

La zone **Aperçu** permet de :

- ✅ Visualiser le contenu généré.
- ✅ Vérifier le résultat avant l'export.
- ✅ Afficher les modifications de la sélection en temps réel.

---

#### 📦 Contenu

- ✅ Texte généré.
- ✅ Défilement vertical.
- ✅ Police monospace.

---

#### 🔄 Comportement

L'aperçu est mis à jour automatiquement lors des principales interactions de l'utilisateur.

- ✅ Modification de la sélection.
- ✅ Recherche.
- ✅ Chargement d'un projet.
- ✅ Protection contre les générations multiples de l'aperçu.
- ✅ Optimisation des mises à jour selon la sélection.
- ✅ Limitation automatique des aperçus volumineux.
- ✅ Débounce des rafraîchissements.
- ✅ Invalidation des aperçus obsolètes.
- ✅ Génération indépendante des interactions de sélection.

---

#### 🔄 États de l'interface

- ✅ Chargement.
- ✅ Prêt.
- ✅ Aucun contenu.
- ✅ Erreur.

---

#### ⚠️ Cas particuliers

- ✅ Aucun fichier sélectionné → affichage d'un message.
- ✅ Contenu volumineux → défilement vertical automatique.

---

#### ⚠️ Projets volumineux

Pour les très gros projets, l'interface peut appliquer une limitation volontaire de l'aperçu afin de préserver les performances.

```text
⚠ Projet volumineux — affichage partiel
```

- ✅ Chargement partiel de l'aperçu.
- ✅ Limitation automatique du contenu affiché.
- ✅ Réduction des recalculs inutiles.

---

#### 🔁 Cohérence avec l'export

Dans le fonctionnement normal de l'application :

- ✅ Le contenu de l'aperçu est identique au contenu exporté.
- ✅ L'aperçu et l'export utilisent la même génération réalisée par le **Core**.

Pour les projets très volumineux :

- ✅ Seul l'affichage de l'aperçu peut être limité.
- ✅ Le contenu exporté reste toujours complet.
- ✅ Les statistiques sont calculées sur l'ensemble des fichiers sélectionnés.

> Le principe **`Preview = Export`** reste valable. Seul l'affichage de l'aperçu peut être volontairement limité afin de préserver les performances.

---

#### 💡 Bonnes pratiques

- ✅ Préserver la cohérence entre l'aperçu et l'export.
- ✅ Limiter les recalculs inutiles.
- ✅ Maintenir une interface réactive.
- ✅ Fournir un retour immédiat après chaque modification de la sélection.
- ✅ Garantir un aperçu fidèle au contenu exporté.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="zone-basse-actions"></a>

### **🔻 08. Zone basse — Actions**

#### 📋 Vue d'ensemble

Cette section présente la zone **Actions** de l'interface utilisateur.

Elle regroupe les principales commandes permettant de finaliser les opérations de **LatuCollect**, notamment l'export des fichiers sélectionnés et l'accès aux journaux d'exécution.

---

#### ⚙️ Fonctionnalités

- ✅ Export du contenu généré.
- ✅ Consultation des journaux d'exécution (Logs).

---

#### 🔄 Comportement

Les actions sont réalisées à partir du contenu généré par le **Core**.

- ✅ Génération du document exporté.
- ✅ Respect du format sélectionné (TXT / Markdown).
- ✅ Affichage d'un retour utilisateur après chaque action.

---

#### ⚠️ Gestion des erreurs

- ✅ Aucun fichier sélectionné.
- ✅ Échec de l'export.
- ✅ Affichage d'un message explicite en cas d'erreur.

---

#### 🧾 Journaux (Logs)

Les journaux permettent de consulter les informations relatives au fonctionnement de l'application.

- ✅ Affichage dans une fenêtre dédiée.
- ✅ Filtrage par niveau (Information, Avertissement, Erreur).
- ✅ Export des journaux.

---

#### 💡 Bonnes pratiques

- ✅ Fournir un retour utilisateur après chaque action importante.
- ✅ Préserver la cohérence entre l'aperçu et l'export.
- ✅ Présenter des messages d'erreur clairs et explicites.
- ✅ Limiter les interruptions de l'utilisateur.
- ✅ Garantir un accès simple aux principales actions.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="stabilite-interface"></a>

### **🖥️ 09. Stabilité de l'interface**

#### 📋 Vue d'ensemble

Cette section présente les règles garantissant la stabilité de l'interface utilisateur de **LatuCollect**.

L'interface est conçue pour rester lisible, réactive et prévisible quelles que soient les interactions de l'utilisateur.

---

#### 📏 Fenêtre

L'interface est conçue pour fonctionner dans une fenêtre offrant un espace d'affichage suffisant pour l'ensemble des composants.

- ✅ Taille minimale : **1600 × 1000**.
- ✅ Préservation de la lisibilité de l'interface.
- ✅ Conservation de la disposition des différentes zones.

---

#### 📐 Redimensionnement

Le redimensionnement de la fenêtre doit préserver la stabilité de l'interface.

- ✅ Interface toujours lisible.
- ✅ Réduction du scintillement (_flickering_).
- ✅ Comportement fluide pendant le redimensionnement.
- ✅ Conservation de l'organisation générale de l'interface.

---

#### ⚙️ Objectifs

- ✅ Garantir une interface stable.
- ✅ Préserver une navigation fluide.
- ✅ Maintenir une disposition cohérente des composants.
- ✅ Offrir une expérience utilisateur prévisible.

---

#### 💡 Bonnes pratiques

- ✅ Préserver une interface stable pendant toutes les interactions.
- ✅ Éviter les modifications brutales de la disposition.
- ✅ Garantir une expérience utilisateur fluide et cohérente.
- ✅ Conserver une présentation homogène lors du redimensionnement.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="performances-interface"></a>

### **🚄 10. Performances de l'interface**

#### 📋 Vue d'ensemble

Cette section présente les principales optimisations mises en œuvre afin de garantir une interface utilisateur fluide, réactive et cohérente.

Ces optimisations permettent de limiter les traitements inutiles, de préserver les performances et d'assurer une expérience utilisateur stable, même sur des projets volumineux.

---

#### 📋 Principes

- ✅ Éviter les rechargements complets inutiles.
- ✅ Préserver une interface toujours réactive.
- ✅ Limiter les recalculs inutiles.
- ✅ Maintenir un aperçu synchronisé avec la sélection.
- ✅ Garantir le principe **`Preview = Export`**.

---

#### 🌳 Optimisations de l'arborescence

Les optimisations de l'arborescence permettent de limiter les traitements inutiles tout en conservant une navigation fluide.

- ✅ Mise à jour ciblée des éléments.
- ✅ Conservation de l'arborescence réelle (sans duplication).
- ✅ Filtrage basé sur la visibilité (`IsVisible`).
- ✅ Réduction des rechargements complets.
- ✅ Conservation de l'état d'expansion.

---

#### 🖥️ Optimisations de l'interface

Les optimisations de l'interface visent à préserver la réactivité de l'application pendant les traitements.

- ✅ Chargement progressif.
- ✅ Préservation de la fluidité pendant les traitements.
- ✅ Réduction des blocages de l'interface utilisateur.

---

#### 👁️ Optimisations de l'aperçu

Les optimisations de l'aperçu permettent de limiter les mises à jour inutiles tout en garantissant un affichage cohérent.

- ✅ Protection contre les rafraîchissements multiples.
- ✅ Protection contre les actions répétées.
- ✅ Réduction des recalculs inutiles.
- ✅ Mise à jour optimisée en fonction de la sélection.

---

#### 📂 Gestion des exclusions

Les exclusions sont mises à jour de manière ciblée afin de limiter les traitements inutiles.

- ✅ Réduction des reconstructions complètes de la liste des exclusions.
- ✅ Mise à jour ciblée des éléments concernés.

---

#### 🎯 Objectifs

- ✅ Préserver une interface fluide.
- ✅ Garantir un comportement prévisible.
- ✅ Réduire les traitements inutiles.
- ✅ Maintenir de bonnes performances sur les projets volumineux.

---

#### 💡 Bonnes pratiques

- ✅ Éviter les traitements redondants.
- ✅ Réutiliser les données déjà disponibles.
- ✅ Préserver la réactivité de l'interface.
- ✅ Limiter les rafraîchissements complets.
- ✅ Conserver une cohérence entre l'aperçu et l'export.
- ✅ Privilégier les mises à jour ciblées plutôt que les rechargements complets.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="regles-interface"></a>

### **🧠 11. Règles de l'interface**

#### 📋 Vue d'ensemble

Cette section présente les principales règles de conception de l'interface utilisateur de **LatuCollect**.

Ces règles garantissent une interface cohérente avec l'architecture **ALC** et contribuent à préserver une séparation claire des responsabilités entre l'interface utilisateur, les **ViewModels** et le **Core**.

---

#### 📋 Règles

- ✅ L'interface est dédiée à l'affichage et aux interactions utilisateur.
- ✅ La logique métier est exclusivement gérée par le **Core**.
- ✅ Les **ViewModels** assurent l'orchestration entre l'interface utilisateur et le **Core**.
- ✅ L'interface ne lit jamais directement les fichiers.
- ✅ Chaque composant possède une responsabilité clairement définie.
- ✅ Toute évolution doit préserver une interface simple, cohérente et prévisible.

---

#### 💡 Principe fondamental

> L'interface utilisateur ne contient jamais de logique métier.

> Elle se limite à présenter les informations produites par le **Core** et à transmettre les actions de l'utilisateur par l'intermédiaire des **ViewModels**.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="evolutions-interface"></a>

### **🔮 12. Évolutions de l'interface**

#### 📋 Vue d'ensemble

Cette section présente les principales évolutions prévues pour l'interface utilisateur de **LatuCollect**.

Ces évolutions visent à améliorer l'expérience utilisateur tout en conservant une interface simple, cohérente et conforme aux principes de l'architecture **ALC**.

---

#### 🎯 Objectifs

Les prochaines évolutions de l'interface poursuivent les objectifs suivants :

- ✅ Moderniser l'apparence générale de l'application.
- ✅ Améliorer la lisibilité des différents composants.
- ✅ Harmoniser l'ensemble de l'interface utilisateur.
- ✅ Renforcer la cohérence visuelle de l'application.
- ✅ Préserver les habitudes des utilisateurs.

---

#### 🎨 Axes d'amélioration

Les évolutions porteront notamment sur :

- 🟡 Finalisation des thèmes clair et sombre.
- 🟡 Harmonisation des composants visuels.
- 🟡 Amélioration de la hiérarchie visuelle.
- 🟡 Optimisation des espacements.
- 🟡 Uniformisation des icônes.
- 🟡 Amélioration de la typographie.
- 🟡 Centralisation de la palette de couleurs.

---

#### 📊 État actuel

- ✅ Structure générale de l'interface stabilisée.
- ✅ Organisation des principales zones validée.
- ✅ Comportement général de l'interface défini.
- 🟡 Refonte visuelle progressive prévue conformément à la `ROADMAP.md`.

---

#### 💡 Principe fondamental

> Les évolutions de l'interface concernent uniquement la présentation et l'expérience utilisateur.

> Elles ne modifient pas le fonctionnement du pipeline, les traitements du **Core** ni le principe **`Preview = Export`**.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="regles-ux"></a>

### **🧠 13. Règles UX**

#### 📋 Vue d'ensemble

Cette section présente les principaux principes d'expérience utilisateur (**UX**) appliqués dans **LatuCollect**.

Ces principes visent à proposer une interface simple, cohérente et agréable à utiliser tout en limitant la complexité et les actions inutiles.

---

#### 📋 Principes

- ✅ Chaque action possède une responsabilité clairement définie.
- ✅ Les fonctionnalités sont organisées de manière logique.
- ✅ L'interface reste simple, lisible et cohérente.
- ✅ Un retour utilisateur est fourni après chaque action importante.
- ✅ Les traitements inutiles sont évités afin de préserver la fluidité.

---

#### 🎯 Objectifs

- ✅ Faciliter la prise en main de l'application.
- ✅ Limiter les erreurs de manipulation.
- ✅ Garantir un comportement prévisible.
- ✅ Préserver une interface fluide et réactive.
- ✅ Améliorer le confort d'utilisation.

---

#### 💡 Principe fondamental

> L'expérience utilisateur privilégie la simplicité, la cohérence et la prévisibilité.

> Toute évolution de l'interface doit améliorer l'expérience utilisateur sans modifier les principes fondamentaux de **LatuCollect** ni introduire de complexité inutile.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="interdits"></a>

### **⚠️ 14. Interdits**

#### 📋 Vue d'ensemble

Cette section présente les principales pratiques à éviter lors de la conception et de l'évolution de l'interface utilisateur de **LatuCollect**.

Le respect de ces règles contribue à préserver une interface simple, cohérente, prévisible et conforme aux principes de l'architecture **ALC**.

---

#### 🚫 Règles

- ❌ Introduire de la logique métier dans l'interface utilisateur.
- ❌ Contourner les **ViewModels** pour accéder directement au **Core**.
- ❌ Multiplier les écrans, les parcours ou les actions inutiles.
- ❌ Masquer des fonctionnalités ou des actions importantes.
- ❌ Complexifier inutilement l'interface utilisateur.
- ❌ Modifier la structure officielle de l'interface sans justification.
- ❌ Rompre la cohérence visuelle entre les différentes zones de l'interface.

---

#### 💡 Principe fondamental

> Toute évolution de l'interface doit améliorer l'expérience utilisateur tout en préservant la simplicité, la cohérence et la prévisibilité de **LatuCollect**.

> Une interface plus riche ne doit jamais devenir une interface plus complexe.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="evolution-ui"></a>

### **🎨 15. Refonte de l'interface utilisateur**

#### 📋 Vue d'ensemble

Cette section présente la manière dont les évolutions de l'interface utilisateur sont réalisées dans **LatuCollect**.

Chaque amélioration est intégrée progressivement afin de préserver la stabilité de l'application, de limiter les régressions et de garantir une expérience utilisateur cohérente.

---

#### 🛠️ Méthode

Les évolutions de l'interface sont réalisées progressivement selon les principes suivants :

- ✅ Analyse de l'existant.
- ✅ Proposition de plusieurs solutions visuelles.
- ✅ Comparaison des différentes approches.
- ✅ Validation des choix retenus.
- ✅ Intégration progressive dans l'application.

---

#### 🎯 Principes

- ✅ Préserver les habitudes des utilisateurs.
- ✅ Limiter les régressions.
- ✅ Garantir une cohérence visuelle.
- ✅ Conserver un comportement identique de l'application.
- ✅ Respecter les principes de l'architecture **ALC**.

---

#### 💡 Principe fondamental

> La refonte de l'interface améliore uniquement la présentation et l'expérience utilisateur.

> Le fonctionnement de **LatuCollect**, son pipeline et le principe **`Preview = Export`** restent inchangés.

---

#### 📋 Domaines concernés

Les évolutions de l'interface utilisateur s'appuient sur plusieurs axes d'amélioration afin d'offrir une expérience plus moderne, plus cohérente et plus agréable.

- 🟡 Finalisation des thèmes clair et sombre.
- 🟡 Centralisation de la palette de couleurs.
- 🟡 Amélioration de la typographie.
- 🟡 Harmonisation des espacements et des alignements.
- 🟡 Uniformisation des icônes.
- 🟡 Amélioration de la hiérarchie visuelle.

---

#### 🔍 Audit UX/UI

Les évolutions de l'interface sont réalisées progressivement à partir d'un audit UX/UI de chaque composant.

##### ⚙️ Méthode

Chaque évolution suit les étapes suivantes :

- ✅ Analyse de l'existant.
- ✅ Élaboration de plusieurs propositions visuelles.
- ✅ Comparaison des différentes solutions.
- ✅ Validation des choix retenus avant leur intégration.

---

#### 🎨 Composants concernés

L'audit UX/UI porte notamment sur les éléments suivants :

- ✅ Thème général.
- ✅ Boutons.
- ✅ Arborescence.
- ✅ Zone d'aperçu.
- ✅ Messages de retour utilisateur.
- ✅ Journaux (Logs).
- ✅ Paramètres.
- ✅ Palette de couleurs.
- ✅ Typographie.

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="etat-actuel"></a>

### **📌 16. État actuel**

#### 📋 Vue d'ensemble

Cette section présente l'état actuel de l'interface utilisateur de **LatuCollect**.

Elle résume les principaux éléments déjà implémentés ainsi que les fonctionnalités désormais stabilisées avant les prochaines évolutions prévues par la feuille de route.

---

#### 🖥️ Interface utilisateur

- ✅ Structure générale stabilisée.
- ✅ Organisation des quatre zones validée.
- ✅ Interface fluide et réactive.
- ✅ Chargement progressif de l'interface.
- ✅ Réduction des rafraîchissements inutiles.
- ✅ Système de retour utilisateur intégré.

---

#### 🌳 Arborescence

- ✅ Recherche dynamique.
- ✅ Filtrage basé sur la visibilité.
- ✅ Conservation de l'arborescence réelle.
- ✅ Persistance de l'état d'expansion.
- ✅ Gestion stabilisée des exclusions.

---

#### 👁️ Aperçu

- ✅ Mise à jour automatique selon la sélection.
- ✅ Optimisation des rafraîchissements.
- ✅ Limitation automatique pour les projets volumineux.
- ✅ Principe **`Preview = Export`** respecté dans le fonctionnement normal.

---

#### 🚄 Performances

- ✅ Interface réactive pendant les traitements.
- ✅ Réduction des recalculs inutiles.
- ✅ Optimisations pour les projets volumineux.
- ✅ Comportement utilisateur stable et prévisible.

---

#### 📄 Références

Pour plus d'informations sur l'architecture et les évolutions du projet, consulter :

- 📖 `ARCHITECTURE.md`
- 🗺️ `ROADMAP.md`
- 📝 `PATCH_NOTES.md`

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

<a id="objectif-global"></a>

### **🎯 17. Objectif global**

#### 📋 Vue d'ensemble

Cette dernière section rappelle les principaux objectifs poursuivis par l'interface utilisateur de **LatuCollect**.

Elle synthétise les principes qui guident sa conception ainsi que les futures évolutions afin de conserver une interface simple, cohérente et agréable à utiliser.

---

#### 🎯 Objectifs

- ✅ Proposer une interface intuitive.
- ✅ Faciliter la prise en main de l'application.
- ✅ Garantir une navigation claire et prévisible.
- ✅ Préserver une interface fluide et réactive.
- ✅ Maintenir une cohérence avec l'architecture **ALC**.

---

#### 💡 Principe fondamental

> L'interface utilisateur doit permettre à l'utilisateur de comprendre rapidement le fonctionnement de **LatuCollect**.

> Chaque évolution doit améliorer l'expérience utilisateur sans modifier les principes fondamentaux de **LatuCollect** ni introduire de complexité inutile.
