# Conway's Game of Life Simulator

## Preamble

In 1970, mathematician John Horton Conway introduced the Game of Life, a deceptively simple cellular automaton governed by just a few rules of life, death, and birth for cells on a grid. Initially a playful mathematical exploration, the Game of Life rapidly captured imaginations with its mesmerizing patterns and unexpected depth.

At its core, Conway's Game of Life is a deceptively simple experiment in emergence and computability. From a handful of rules governing the life and death of cells on a grid, unpredictable complexity blossoms. Patterns evolve, exhibiting behaviors reminiscent of living organisms: self-replication, organized movement, and even rudimentary computation.

### Historical Impact

Beyond its theoretical significance, the Game of Life found practical applications over the years:

- Modeling: Its ability to simulate dynamic systems made it a tool for modeling phenomena such as population growth, urban patterns, and even the behavior of fundamental particles. For instance, cellular automata similar to the Game of Life have been used to simulate the spread of wildfires, aiding in the development of prediction models.
- Artificial Life: The Game of Life spurred the field of artificial life, where researchers investigate the creation and simulation of life-like behaviors in computational systems. It served as an early inspiration for self-replicating patterns and evolving systems within computer simulations.
- Computational Implications: Remarkably, Conway's Game of Life has been proven to be Turing-complete. This means it has the same computational power as any modern computer, capable in theory of performing any calculation. For instance, researchers have constructed intricate patterns within the game that can function as logic gates and complex computational structures, demonstrating its capacity to emulate any computer program. This highlights the surprising power that lies within even the simplest sets of well-defined rules.

## Description

This project is console application that simulates Conway's Game of Life, emphasizing SOLID principles for well-structured, maintainable, and testable code. Users should be able to define an initial grid configuration and observe the evolution of patterns over time. Data persistence will be achieved through JSON files, allowing users to maintain their grid configurations across sessions. The project ensures code quality through unit testing with xUnit.

### Components
- ICell Interface: Defines the properties and behaviors of a single cell in the grid (state, neighbors).
- IGrid Interface: Specifies methods for representing the grid of cells (updating states, getting cell states).
- IStorage Interface Specifies methods for loading and saving the grid.
- Cell Class: Implements the ICell interface, representing an individual cell with its state (alive/dead).
- Grid Class: Implements the IGrid interface, managing a 2D array of cells and the logic for updating cell states in each generation according to Conway's Game of Life rules.
- JsonStorage Class: Implements the IStorage interface. Handles the saving and loading of grid data to and from JSON files.
- AutomatonSimulator Class: Manages the overall simulation, applying the Game of Life rules to the Grid over iterations.
- Program Class (User Interface): Provides a console interface for grid setup, stepping through generations, visualization of the simulation, and saving/loading grid configurations.

### Features

1. Accurate implementation of the rules of Conway's Game of Life:
    - Any live cell with fewer than two live neighbors dies, as if by underpopulation.
    - Any live cell with two or three live neighbors lives on to the next generation.
    - Any live cell with more than three live neighbors dies, as if by overpopulation.
    - Any dead cell with exactly three live neighbors becomes a live cell, as if by reproduction.
    - A neighbor is considered any of the eight cells around the current cell, unless it's an edge case (e.g., for the top row, we will consider that there are no neighbors above it).
2. Console Visualization:
    - Live cells are represented with the character 'O' and dead cells with a period ('.').
    - The grid is displayed in a rectangular format.
