# GameOfLife
##The following things can be configured for this game:

Width / Height of window. When the game is launched this cannot be changed.

Number of columns and rows of cells.

The speed of the autorun function.

The color of living and dead cells.

Configurations for the game can be made at the top of the GameRenderer.cs file.

##Controls of the game:
Left click: run one cycle.

Right click: Spawn a new cell at mouse position.

toggle s: Enable or disable autorun function.

##Structure of the code:
The Cell class handles everything a cell can do. It can live and it can die but the cell itself has no logic.

The GameSimulator class runs a simulation of the game. It holds a grid with rows * columns cells and has logic implemented to decide which cells live and die.

The GameRenderer class uses the GameSimulator to simulate a game and then draws the result of the simulation on the screen.
