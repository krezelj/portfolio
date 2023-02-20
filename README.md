# Jan Krężel - Portfolio

Overview of some of my projects.

**NOTE:** Some of the projects have their own github repository and relevant links are provided.

# Project List

1. [SpotiMy](#spotimy)
1. [RL Maze Solving](#maze-solving-using-reinforcement-learning)
1. [RL EXAPUNKS Hackmatch Bot](#exapunks-hackmatch-bot-wip)
1. [EXAPUNK Solitaire Bot](#exapunks-solitaire-bot)
1. [Django Car Rental Site](#django-car-rental-site)
1. [CUDA N-Body Gravity](#n-body-gravity-simulation)
1. [CUDA K-Means Clustering](#k-means-clustering)
1. [Checkers AI](#checkers-ai)
1. [Battleship AI](#battleship-ai)
1. [Evolution Simulator](#evolution-simulation)
1. [Patched Conics Gravity Simulation](#patched-conics-gravity-simulation)
1. [Maze Generator and Pathfinder](#maze-generator-and-pathfinder)
1. [Trigonometric Polynomial Root Finder](#trigonometric-polynomial-root-finder)

---

# Python

## SpotiMy

Python Dashboard presenting data collected by Spotify and fetched using Spotify API. Dashboard was made using `dash` library. Data was processed using `pandas`, `numpy`. Visualisations were made using `plotly`. In addition, `.css` files were used to customise the look of the dashboard.

Apart from graphs the data is also shown as modified-at-run-time `.md` files. The dashboard also includes an interactive song recommender (based on users streaming history).

Orignally the data was collected for the three members of the group but in order to protect the privacy of the other to members their data was removed and names were changed. The functionality that allows to switch active user was not removed but only one of the tabs is showing the data.
On top of that my personal data was obfuscated.

## Maze Solving using Reinforcement Learning

**Repository [Link](https://github.com/krezelj/reinforcement-maze-solver)**

Implementation of Q-Learning algorithm that solves mazes by looking at the relative visit frequncy of neighbouring cells and deciding where to go next.

---

## EXAPUNKS Hackmatch Bot (WIP)

**Repository [Link](https://github.com/krezelj/exapunks-hackmatch-bot)**

Implementation of DQN algorithm that plays Hackmatch - a mini game inside another game called EXAPUNKS. It's still work in progress and is not fully functional yet. A simple recreation of the game is implemented that can be used as the environment in which an agent acts. A simple neural network created using tensorflow and a training loop are also present.

Currently the next steps are to improve the model and the training process.

---

## EXAPUNKS Solitaire Bot

**Repository [Link](https://github.com/krezelj/exapunks-solitaire-bot)**

A bot that plays EXAPUNKS version of solitaire. It captures the state of the game and converts it to an easy to use data structure. Next it uses DFS to navigate through possible moves to find a winning strategy. Finally it applies the moves inside the game using `pyautogui`

---

## Django Car Rental Site

**Repository [Link](https://github.com/krezelj/car-rentals)**

A simple website implemented using Django. It allows the user to sign up/sign in and sends a confirmation email upon signing up. All users can browse through available cars using filters. Logged in users can rent a car and all current rentals will be shown on their profile page.

# CUDA

## N-Body Gravity Simulation

**Repository [Link](https://github.com/krezelj/cuda-gravity)**

Simulation of gravity with an arbitrary number of bodies using CUDA. The simulation can support up to 10 thousand bodies at a smooth framerate. Visualisation is done using SFML.

In the near future I would like to extend this project to support a larger number of bodies, implement "dust" particles that do not affect other bodies gravitationally, add other forms of visualisation and use OpenGL for better performance.

## K-Means Clustering

**Repository [Link](https://github.com/krezelj/cuda-kmeans/settings)**

Implementation of K-Means Clustering algorithm using CUDA. Currently there is no option to load own data. An included benchmark generates random data, clusters it and outputs the assignments and generated points to an external file.

# WinForms

## Checkers AI

Checkers game with full set of rules implemented as a Windows Forms application. The application allows for Human vs. Human games and Human vs. Computer games. Computer's AI is implemented as a simple Min-Max algorithm with Alpha-Beta pruning.

The application was made with time and memory efficiency in mind. On he hardest difficulty level the algorithm is able to efficiently traverse through millions of nodes.

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

# Matlab

## Trigonometric Polynomial Root Finder

MATLAB Window Application that calculates roots of polynomials of the form

$$\sum_{k=0}^{n}a_k\cdot\cos(kx)$$

using Newton's method. The sum is calculated using Goertzel's algorithm. Using application's interface the user can input coefficients. The roots are calculated in real time and plotted on the graph. The user can also visualise convergence of each starting point using color to group them by the number of iterations needed to converge or by the root they converge to.

The algorithm it self is able to immediately calcualte all of the roots for polynomials of degree up to a 1000 (with non zero coefficients) and quickly calculate most of the roots for polynomials of degree up to 12000.

If most of the coefficients are zero the calculation time may further be decreased.
