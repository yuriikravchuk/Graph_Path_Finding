using pathFinding;
using System.Collections.Generic;
using System.Linq;

namespace pathFinding
{
    public partial class WaveSearch : PathFindingAlgorithm
    {
        //private readonly List<WaveItem<CellPresenter>> _waveCells;
        public WaveSearch(IReadOnlyList<CellPresenter> cells) : base(cells)
        {
            //_waveCells = new List<WaveItem<CellPresenter>>();
        }


        protected override IReadOnlyList<Transition> GetPath(CellPresenter start, CellPresenter end)
        {
            var startNode = new WaveNode<CellPresenter>(start, 0);
            BuildPath(startNode);

            WaveNode<CellPresenter> endWaveCell = startNode.FindNode(end);
            var path = new List<Transition>();
            WaveNode<CellPresenter> currentCell = endWaveCell;
            while (currentCell.Distance > 0)
            {
                IEnumerable<Connection> avaiableConnections = GetAvailableConnections(currentCell.Item, false);
                List<CellPresenter> avaiableCells = avaiableConnections.Select(item => item.GetOtherCell(currentCell.Item)).ToList();
                List<WaveNode<CellPresenter>> avaiableNodes = avaiableCells.Select(item => startNode.FindNode(item)).ToList();
                int minDistance = avaiableNodes.Min(item => item.Distance);
                WaveNode<CellPresenter> nextCell = avaiableNodes.First(item => item.Distance == minDistance);
                Connection nextConnection = avaiableConnections.First(item => item.GetOtherCell(currentCell.Item) == nextCell.Item);
                var transition = new Transition(nextCell.Item, currentCell.Item, nextConnection);
                path.Add(transition);
                currentCell = nextCell;
            }
            path.Reverse();
            return path;
        }

        private void BuildPath(WaveNode<CellPresenter> startNode)
        {
            //var treeRoot = new WaveNode<WaveNode<CellPresenter>>(startNode);
            //_waveCells.Add(startNode);
            while (VisitedCells.Count < Cells.Count)
            {
                IEnumerable<WaveNode<CellPresenter>> tails = startNode.GetTails();
                foreach (var tail in tails)
                {
                   // WaveNode<CellPresenter> currentWaveCell = tail.Item;
                    CellPresenter currentCell = tail.Item;
                    IReadOnlyList<Connection> nextConnections = GetAvailableConnections(currentCell);
                    foreach (var connection in nextConnections)
                    {
                        CellPresenter cell = connection.GetOtherCell(currentCell);
                        SwitchCurrentCell(cell, connection);
                        var node = new WaveNode<CellPresenter>(cell, tail.Distance + 1, tail);
                        //var treeCell = new WaveNode<WaveItem<CellPresenter>>(waveCell);
                        tail.AddChild(node);
                        //_waveCells.Add(waveCell);
                    }
                }
            }
        }
    }
}

