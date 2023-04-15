using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace pathFinding
{
    public class BreadthFirstSearch : PathFindingAlgorithm
    {
        public BreadthFirstSearch(IReadOnlyList<CellPresenter> cells) : base(cells) { }

        protected override IReadOnlyList<Transition> GetPath(CellPresenter start, CellPresenter end)
        {
            var root = new BFS_Cell(start);
            while (true)
            {
                IEnumerable<BFS_Cell> tails = root.GetTails();
                foreach (var tail in tails)
                {
                    var connections = GetAvailableConnections(tail.Cell);

                    foreach (var connection in connections)
                    {
                        CellPresenter cell = connection.GetOtherCell(tail.Cell);
                        BFS_Cell bfs_cell = new BFS_Cell(cell, tail);
                        tail.AddChild(bfs_cell);
                        SwitchCurrentCell(cell, connection);

                        if (cell == end)
                        {
                            IReadOnlyList<BFS_Cell> path = bfs_cell.GetPath();
                            var result = new List<Transition>();
                            for(int i = 0; i < path.Count - 1; i++)
                            {
                                Connection connection1 = path[i].Cell.FindConnectionWithCell(path[i + 1].Cell);
                                Transition transition = new Transition(path[i].Cell, path[i + 1].Cell, connection1);
                                result.Add(transition);
                            }
                            return result;
                        }
  
                    }
                }
            }
        }

        private class BFS_Cell
        {
            public readonly BFS_Cell Father;
            public readonly CellPresenter Cell;
            public readonly bool IsRoot;

            private readonly List<BFS_Cell> _children;

            public BFS_Cell(CellPresenter cell, BFS_Cell father = null)
            {
                if(father == null)
                    IsRoot = true;
                else
                    Father = father;

                Cell = cell;
                _children = new List<BFS_Cell>();
            }

            public void AddChild(BFS_Cell child) => _children.Add(child);

            public IEnumerable<BFS_Cell> GetTails()
            {
                var tail = new List<BFS_Cell>();

                if (_children.Count > 0)
                    for (int i = 0; i < _children.Count; i++)
                        tail.AddRange(_children[i].GetTails());
                else
                    tail.Add(this);

                return tail;
            }

            public IReadOnlyList<BFS_Cell> GetPath()
            {
                var result = new List<BFS_Cell>();
                if (!IsRoot)
                    result.AddRange(Father.GetPath());
                result.Add(this);
                return result;
            }
        }
    }
}