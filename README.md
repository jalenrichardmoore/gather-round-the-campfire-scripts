# **Gather 'Round the Campfire - Scripts**

## **Overview**
This repository contains all necessary scripts for the Unity game, *Gather 'Round the Campfire*. *Gather 'Round the Campfire* is a puzzle adventure game where the player must navigate mazes and complete objectives to help five hikers who are trapped in a winter forest. These scripts control the mechanics for the central hub level, the mazes and their unique objectives, as well as overall game flow between scenes.

## **Folder Structure**
```plaintext
/Scripts/
|-- Audio/
│   ├── AudioManager.cs                   # Manages the flow between audio clips throughout the entire game
|-- Dialogue/
│   ├── Dialgoue.cs                       # Defines the properties of a dialogue option
│   ├── DialgoueManager.cs                # Manages the display of all dialogue options throughout the entire game
│   ├── DialgoueTrigger.cs                # Defines the properties of dialogue triggers in mazes
|-- Management/
│   ├── GameData.cs                       # Stores global game information
|-- Mazes/
│   ├── AvatarSpawnPoint.cs               # Defines the properties of avatar spawn points
│   ├── LonelyAvatar.cs                   # Defines the properties and behavior of the lonely avatar game object
│   ├── MazeManager.cs                    # Manages the game flow during maze levels
│   ├── Passion.cs                        # Defines the properties of the passion game object
│   ├── PredatorAvatar.cs                 # Defines the properties and behavior of the predator avatar game object
│   ├── RiddleClue.cs                     # Defines the properties of riddle clue game objects
│   ├── RiddleManager.cs                  # Manages gameplay involving the riddle during the 'Blue Maze' level
│   ├── Treasure.cs                       # Defines the properties of treasure game objects
|-- The Cabin/
│   ├── CabinManager.cs                   # Manages game flow during the 'Cabin' level
│   ├── Campfire.cs                       # Defines the properties of the campfire game object
|-- The Forest/
│   ├── Boundary.cs                       # Defines the properties of level boundaries
│   ├── Campsite.cs                       # Defines the properties of campsites
│   ├── ForestItem.cs                     # Defines the properties and gameplay mechanics of forest item game objects
│   ├── ForestManager.cs                  # Manages the game flow during the 'Forest' level
│   ├── SpawnPoint.cs                     # Defines the properties of forest item spawn points
│   ├── TimeManager.cs                    # Manages the day-night cycle animation during the 'Forest' level
|-- Title Screen/
│   ├── TitleManager.cs                   # Manages the game flow during the 'Title Screen'
└── README.md
```

## **Dependencies**
Unity Version: 2022.3.4

## **Installation & Usage**
1. Clone the repository:
```sh
git clone https://github.com/jalenrichardmoore/gather-round-the-campfire-scripts.git
```

2. Copy the scripts into your Unity project's 'Assets/Scripts/' folder
   
## **Credits**
Developed by Jalen Moore
