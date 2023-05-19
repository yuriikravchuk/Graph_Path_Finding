using System.Collections.Generic;
using System.Linq;

namespace pathFinding
{
    public partial class WaveSearch : PathFindingAlgorithm
    {
        private readonly List<WaveItem<CellPresenter>> _waveCells;
        public WaveSearch(IReadOnlyList<CellPresenter> cells) : base(cells) 
        {
            _waveCells = new List<WaveItem<CellPresenter>>();
        }


        protected override IReadOnlyList<Transition> GetPath(CellPresenter start, CellPresenter end)
        {
            var startWaveCell = new WaveItem<CellPresenter>(start, 0);
            BuildPath(startWaveCell);

            WaveItem<CellPresenter> endWaveCell = _waveCells.First(cell => cell.Item == end);
            var path = new List<Transition>();
            WaveItem<CellPresenter> currentCell = endWaveCell;
            while(currentCell.Distance > 0)
            {
                IEnumerable<Connection> avaiableConnections = GetAvailableConnections(currentCell.Item, false);
                foreach (var connection in avaiableConnections)
                {
                    CellPresenter otherCell = connection.GetOtherCell(currentCell.Item);
                    WaveItem<CellPresenter> otherWaveCell = _waveCells.First(cell => cell.Item == otherCell);
                    if(otherWaveCell.Distance == currentCell.Distance - 1)
                    {
                        var transition = new Transition(otherCell, currentCell.Item, connection);
                        path.Add(transition);
                        currentCell = otherWaveCell;
                        break;
                    }

                }
            }
            path.Reverse();
            return path;
        }

        private void BuildPath(WaveItem<CellPresenter> startWithDistance)
        {
            var treeRoot = new TreeNode<WaveItem<CellPresenter>>(startWithDistance);
            _waveCells.Add(startWithDistance);
            while (VisitedCells.Count < Cells.Count)
            {
                IEnumerable<TreeNode<WaveItem<CellPresenter>>> tails = treeRoot.GetTails();
                foreach (var tail in tails)
                {
                    WaveItem<CellPresenter> tailWaveCell = tail.Item;
                    CellPresenter currentCell = tailWaveCell.Item;
                    IReadOnlyList<Connection> nextConnections = GetAvailableConnections(currentCell, false);
                    foreach (var connection in nextConnections)
                    {
                        CellPresenter otherCell = connection.GetOtherCell(currentCell);
                        if(VisitedCells.Any(item => item == otherCell))
                        {
                            var otherWaveCell = _waveCells.First(item => item.Item == otherCell);
                            if (otherWaveCell.Distance > tailWaveCell.Distance + connection.Weight)
                            {
                                
                            }
                        }
                        else 
                        {
                        SwitchCurrentCell(otherCell, connection);
                        var waveCell = new WaveItem<CellPresenter>(otherCell, tail.Item.Distance + 1);
                        var treeCell = new TreeNode<WaveItem<CellPresenter>>(waveCell);
                        tail.AddChild(treeCell);
                        _waveCells.Add(waveCell);
                        }

                    }
                }
            }
        }
    }
}