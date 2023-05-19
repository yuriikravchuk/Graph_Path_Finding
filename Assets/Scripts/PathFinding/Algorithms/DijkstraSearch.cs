using pathFinding;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.MemoryProfiler;

public class DijkstraSearch : PathFindingAlgorithm
{
    private readonly List<WaveItem<CellPresenter>> _waveCells;
    public DijkstraSearch(IReadOnlyList<CellPresenter> cells) : base(cells)
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
        while (currentCell.Distance > 0)
        {
            IEnumerable<Connection> avaiableConnections = GetAvailableConnections(currentCell.Item, false);
            IEnumerable<CellPresenter> avaiableCells = avaiableConnections.Select(item => item.GetOtherCell(currentCell.Item));
            IEnumerable<WaveItem<CellPresenter>> waveItems = avaiableCells.Select(item => _waveCells.First(waveCell => waveCell.Item == item));
            int minDistance = waveItems.Min(item => item.Distance);
            WaveItem<CellPresenter> nextCell = waveItems.First(item => item.Distance == minDistance);
            Connection nextConnection = avaiableConnections.First(item => item.GetOtherCell(currentCell.Item) == nextCell.Item);
            var transition = new Transition(nextCell.Item, currentCell.Item, nextConnection);
            path.Add(transition);
            currentCell = nextCell;
        }
        path.Reverse();
        return path;
    }

    private void BuildPath(WaveItem<CellPresenter> startWaveCell)
    {
        var treeRoot = new TreeNode<WaveItem<CellPresenter>>(startWaveCell);
        _waveCells.Add(startWaveCell);
        while (VisitedCells.Count < Cells.Count)
        {
            IEnumerable<TreeNode<WaveItem<CellPresenter>>> tails = treeRoot.GetTails();
            foreach (var tail in tails)
            {
                WaveItem<CellPresenter> currentWaveCell = tail.Item;
                CellPresenter currentCell = currentWaveCell.Item;
                IReadOnlyList<Connection> nextConnections = GetAvailableConnections(currentCell);
                foreach (var connection in nextConnections)
                {
                    CellPresenter cell = connection.GetOtherCell(currentCell);
                    SwitchCurrentCell(cell, connection);
                    var waveCell = new WaveItem<CellPresenter>(cell, tail.Item.Distance + 1);
                    var treeCell = new TreeNode<WaveItem<CellPresenter>>(waveCell);
                    tail.AddChild(treeCell);
                    _waveCells.Add(waveCell);
                }
            }
        }
    }
}