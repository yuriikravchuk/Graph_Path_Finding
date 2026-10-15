# Graph Path Finding

An interactive graph editor and path-finding visualiser made in Unity 2021.3 as a university lab work for the Artificial Intelligence course.

**Windows build:** download it from [Releases](../../releases).

Build a graph with the mouse, pick a start and an end node, choose an algorithm, and watch which edges it explores and which path it finds.

## Algorithms

- Breadth-first search
- Depth-first search
- Wave (Lee) algorithm
- Dijkstra's algorithm, using edge weights

Each run returns the explored transitions, the final path and the search time, so the algorithms can be compared on the same graph.

## Features

- Shift + left click adds a node, left click on two nodes connects them, right click removes a node or edge.
- Edge weights are editable. Edges are undirected or one-way in either direction, and Shift + right click cycles the type.
- Choose the start and end nodes and the algorithm from a dropdown.
- Save and load graphs to a binary file.

## Code

```
Assets/Scripts/PathFinding/Algorithms  BFS, DFS, wave search, Dijkstra on a shared PathFindingAlgorithm base
Assets/Scripts/PathFinding             path finder, start/end selection, algorithm selector, results
Assets/Scripts/Cell, Connection        graph nodes and edges with presenters, views and spawners
Assets/Scripts/StateMachine            Build, SearchSettings and ViewResults states
Assets/Scripts/Save                    binary save of nodes and connections
```

## Running

Open the project in Unity 2021.3 and start `Assets/Scenes/Scene`.
