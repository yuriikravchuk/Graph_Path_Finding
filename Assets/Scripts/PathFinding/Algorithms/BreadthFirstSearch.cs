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
            var root = new WaveNode<CellPresenter>(start);
            while (true)
            {
                IEnumerable<WaveNode<CellPresenter>> tails = root.GetTails();
                foreach (var tail in tails)
                {
                    var connections = GetAvailableConnections(tail.Item);

                    foreach (var connection in connections)
                    {
                        CellPresenter cell = connection.GetOtherCell(tail.Item);
                        var cellWithChildren = new WaveNode<CellPresenter>(cell, father: tail);
                        tail.AddChild(cellWithChildren);
                        SwitchCurrentCell(cell, connection);

                        if (cell == end)
                        {
                            IReadOnlyList<WaveNode<CellPresenter>> path = cellWithChildren.GetPath();
                            return ConvertPath(path);
                        }

                    }
                }
            }
        }

        private static List<Transition> ConvertPath(IReadOnlyList<WaveNode<CellPresenter>> path)
        {
            var result = new List<Transition>();
            for (int i = 0; i < path.Count - 1; i++)
            {
                Connection connection = path[i].Item.FindConnectionWithCell(path[i + 1].Item);
                Transition transition = new Transition(path[i].Item, path[i + 1].Item, connection);
                result.Add(transition);
            }

            return result;
        }


    }
}