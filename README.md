
# Crash Bandicoot Endless Runner - Unity Game

Bienvenue dans le dépôt de **Crash Bandicoot Endless Runner**, un jeu développé avec Unity et C#. Le but du jeu est d'incarner Crash Bandicoot et d'aller le plus loin possible dans un environnement généré aléatoirement, en évitant les obstacles et en essayant de battre votre propre record.

## Table des matières

- [Présentation du jeu](#présentation-du-jeu)
- [Pré-requis](#pré-requis)
- [Installation](#installation)
- [Structure du projet](#structure-du-projet)
- [Développement](#développement)
- [Contribuer](#contribuer)
- [Support](#support)
- [Licence](#licence)

## Présentation du jeu

**Crash Bandicoot Endless Runner** est un jeu où le joueur incarne Crash Bandicoot dans une course infinie. Le but est de parcourir la plus grande distance possible tout en évitant les obstacles. Les boîtes, générées aléatoirement, apparaissent sur le chemin et ralentissent le joueur s'il entre en collision avec elles. Un rocher poursuit également le joueur dans le but de l'arrêter.

## Pré-requis

Avant de cloner et de travailler sur ce projet, assurez-vous d'avoir installé les éléments suivants :

- Unity 2021.3.x ou une version plus récente
- Visual Studio ou tout autre éditeur compatible avec C#
- Git

## Installation

1. Clonez le dépôt :

   ```bash
   git clone https://github.com/Joris07/CrashBandicoot.git
   ```

2. Accédez au répertoire du projet :

   ```bash
   cd CrashBandicoot
   ```

3. Ouvrez le projet avec Unity :

   - Lancez Unity Hub.
   - Cliquez sur "Open" et sélectionnez le répertoire du projet cloné.

## Structure du projet

Voici un aperçu des dossiers principaux du projet :

- **Assets/** : Contient tous les fichiers de jeu, y compris les scripts, les modèles, les textures, et les scènes.
  - **Scripts/** : Contient les scripts C# qui définissent la logique du jeu.
    - **CameraMovement.cs** : Gère le déplacement de la caméra.
    - **ObstacleGenerator.cs** : Gère la génération aléatoire des boîtes sur le chemin du joueur.
    - **GameController.cs** : Gère les états du jeu, comme le démarrage, le game over, et la mise à jour du score.
- **Scenes/** : Contient les scènes du jeu.
  - **MainScene.unity** : Scène principale du jeu.

## Développement

Pour modifier le jeu, suivez ces étapes :

1. Ouvrez le projet dans Unity.
2. Modifiez les scripts ou les éléments de la scène selon vos besoins.
3. Testez les modifications en lançant la scène principale dans l'éditeur Unity.

### Commandes du joueur

- **Déplacement** : Utilisez les touches fléchées ou les touches `Z`, `Q`, `S` et `D`.
- **Sauter** : Utilisez la touche `Espace`.
- **Attaquer** : Utilisez la touche `X`.

## Support

Pour toute question ou problème, veuillez ouvrir une issue sur GitHub.
