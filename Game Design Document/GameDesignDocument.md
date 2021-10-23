
# Game Design Document
## Game Overview
### Introduction
Change your Path is a 2D adventure/puzzle game wrapped around a world altering puzzle mechanic. Change the environment around you to explore the mysterious land, interact with characters and guide Kvothe on his journey.

### Objective
The objective of the game is to find new pieces of map, solve the map puzzle and find the right way to the multiple ends of the story. 
### Feature
* 2D
* Third Person view
* Single player
* Dynamic Map
* NPCs
### Platform
* PC - Windows, MacOS
### Audience
The intended audience is primarily for both casual and non casual gamers since the mechanic is very simple even if it requires reasoning and effort to solve the puzzle. The game does not involve violence or other malicious actions and so it can be played by kids.
### Look and feel
This game should be able to make light-heart a difficult life situation like the one of Kvothe. This is not a perfect and wonderful world made of cheerful colors, fluttering happy little birds and people who are just waiting to help you. However, this is a world full of hope, curiosity and desire to discover what's beyond what you know. The context in which Kvothe is, most of the time should be calm and serene making the player feel like they have plenty of time to solve the next puzzle. Whoever plays is in control of the situation. They must feel free to explore, think, listen and solve.
## Gameplay
### Overview
The gameplay will consist of exploring the world, finding new map pieces, composing the map and moving in new areas.
The gamer will control the movement of Kvothe: he can move right, left, up, down and diagonally. There are no enemies or vertical obstacles, so jump and attack actions aren’t needed. 
The map is divided into square form map pieces.
At the beginning of the game there will be just one square and the player will be able to move only in that limited world portion. In that area will find another map piece that will be connected to the existing square in order to expand the map. The player will visit the new zone and there will find another map piece and so on and so forth. In the map menu all the map squares are movable and rotatable, even the one with the character inside. When moving and rotating the map, every element inside the square will rotate and move accordingly (NPC, Environmental details, objects).
Inside the map square the player can find npc and interactable objects. One type of interactable object is the map piece.
### Minigames
Apart from composing the map in the right way, there will be different minigames. Some of them will be triggered by an interaction of Kvothe with the environment or the NPCs:
* Move a specific square of the map in a well-defined position. For example, an NPC could tell you that it’s home is north of the forest: in the map you have many pieces that could compose a forest, so create it and place the map square where the NPC is inside at north of the forest. This will trigger an event.
* Signs - On one square you find a sign:” At the end of the straight road there is a bridge”. In your existing map there are some map pieces that have a straight road inside, put them in rows from that square and then a new piece with the bridge appears.
* Labyrinth - Consider two identical map pieces that are connectable: the player has to find how to move from one piece to the other in a specific way. For example, some detail in the piece where you stay tells that the similar piece has to be placed on the right. You put the other identical piece on the right and then you have to find on it other information to understand where the previous will be placed with respect to this one. After guessing the combination between the two squares for a certain number of times, the puzzle is solved.
* The player and NPC - Consider not an open area but specific map pieces composed by two tunnels for example: in one tunnel there is the main character, on the other there is the NPC. Your objective is to create the path for you and your friend in order to exit the tunnels. So there are pieces that will allow only you to move because you can connect only your tunnel for example but not the NPC one.
* Don’t get caught - An angry character starts to run to catch Kvothe. The only way you have to let him/her stop is opening the map view when Kvothe and the enemy are on two different map pieces and isolate that character on a piece alone. This will prevent the character from continuing and after a period of isolation the anger passes. If Kvothe is caught the minigame restarts. Pay attention to which piece you decide to momentarily sacrifice!
### Levels
Change your path is divided in chapters. Each of them consists of completing a discrete objective.

* Chapter 0: Kvothe is in the place it has always been since he was born. This chapter has a very simple objective: interact with NPCs and explore the village. This chapter is designed to familiarize the player with the controls and to collect all needed information before starting the journey. [Average playing time: 10 minutes]
* Chapter 1: The forest around Kvothe’s village is a labyrinth of vegetation and threats. The objective of this chapter is to figure out an escape route from the forest. [Average playing time: 20 minutes]
* Chapter 2: Kvothe is outside the forest.

More chapters will be implemented.

### FlowChart
## Controls
### Explorer View
Move | &#8592; | &#8594; | &#8593; | &#8595; 
--- | --- | --- | --- | ---
Interact | Space
Map view | Tab 
Items | W
### Map View
Move | &#8592; | &#8594; | &#8593; | &#8595; 
--- | --- | --- | --- | ---
Pick up/ Put down | Space
Rotate | A | D
Explore view | Tab 
New piece | W
Cancel | Esc 
## Story
### Synopsis
### Complete story
### Backstory
### Narrative devices
### Subplots
## Game Characters
### Character Design
### NPCs
### World
### Key locations
### Weather Condition
### Objects
### Scale
### Society/Culture
## Media List
### Interface assets
### Environments
### Characters
### Animation
### Music and sound effects

