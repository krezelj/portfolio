# Jan Krężel - Portfolio

Overview of some of my projects.

## Table of Contents
1. [SpotiMy](#spotimy)
1. [Checkers AI](#checkers-ai)
1. [Trigonometric Polynomial Root Finder](#trigonometric-polynomial-root-finder)
1. [Battleship AI](#battleship-ai)
1. [Evolution Simulator](#evolution-simulation)
1. [Patched Conics Gravity Simulation](#patched-conics-gravity-simulation)
1. [Maze Generator and Pathfinder](#maze-generator-and-pathfinder)

---
## SpotiMy
Python Dashboard presenting data collected by Spotify and fetched using Spotify API. Dashboard was made using ```dash``` library. Data was processed using ```pandas```, ```numpy```. Visualisations were made using ```plotly```. In addition, ```.css``` files were used to customise the look of the dashboard.

Orignally the data was collected for the three members of the group but in order to protect the privacy of the other to members their data was removed and names were changed. The functionality that allows to switch active user was not removed but only one of the tabs is showing the data.
On top of that my personal data was obfuscated.

---
## Checkers AI
Checkers game with full set of rules implemented as a Windows Forms application. The application allows for Human vs. Human games and Human vs. Computer games. Computer's AI is implemented as a simple Min-Max algorithm with Alpha-Beta pruning.

The application was made with time and memory efficiency in mind. On he hardest difficulty level the algorithm is able to efficiently traverse through millions of nodes.

---
## Trigonometric Polynomial Root Finder
MATLAB Window Application that calculates roots of polynomials of the form

$$\sum_{k=0}^{n}a_k\cdot\cos(kx)$$

using Newton's method. The sum is calculated using Goertzel's algorithm. Using application's interface the user can input coefficients. The roots are calculated in real time and plotted on the graph. The user can also visualise convergence of each starting point using color to group them by the number of iterations needed to converge or by the root they converge to. 

The algorithm it self is able to immediately calcualte all of the roots for polynomials of degree up to a 1000 (with non zero coefficients) and quickly calculate most of the roots for polynomials of degree up to 12000.

If most of the coefficients are zero the calculation time may further be decreased.

---
## Battleship AI
Battleship game implemented as a Windows Forms application. It only allows for Human vs. Computer games. The AI chooses the cell to hit by calculating the probability distribution that any given cell contains part of a ship taking into account factors such as

- Rules of the game
- Size of already sunken ships
- Relative position of already hit ship parts
- Seek & Destroy strategy

---
## Evolution Simulation
Simple evolution simulation implemented as a Windows Forms application. Food particles are generated periodically and creatures with a basic set of genes are spawned at the beginning. Eating a certain amount of food allows them to reproduce with a small chance of mutation. The genes include

- Speed
- Size
- Sight Range
- Exploration Range
- Predation

The application also plots data about the current state of the plane and provides a debug interface.

---
## Patched Conics Gravity Simulation
Simulation of gravity using patched conics implemented as a Windows Forms application. Bodies and their attributes as well as the simulation speed are hard-coded and can only be changed by modyfing the code. 

Thanks to the use of patched conics the simulation time scale can be changed to arbitrarily large values (as oposed to iteration based approach that cannot be scaled with the loss of accuracy).

---
## Maze Generator and Pathfinder
Maze generation using DFS implemented as a Windows Forms application.
User can specify the dimensions of the maze. After the generation is completed user can specify the start point, the end point (using LMB/RMB) and find the path between the two.