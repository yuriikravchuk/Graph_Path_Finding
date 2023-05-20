using pathFinding;
using System.Collections.Generic;
using System.Linq;

public class DijkstraSearch : PathFindingAlgorithm
{
    public DijkstraSearch(IReadOnlyList<CellPresenter> cells) : base(cells) { }

    protected override IReadOnlyList<Transition> GetPath(CellPresenter start, CellPresenter end)
    {
        var startNode = new WaveNode<CellPresenter>(start, 0);
        BuildPath(startNode);

        WaveNode<CellPresenter> endNode = startNode.Get(end);
        var path = new List<Transition>();
        WaveNode<CellPresenter> currentCell = endNode;
        while (currentCell.Distance > 0)
        {
            IEnumerable<Connection> avaiableConnections = GetAvailableConnections(currentCell.Item, false);
            int minDistance = avaiableConnections.Min(item => item.Weight);
            Connection connection = avaiableConnections.First(item => item.Weight == minDistance);
            CellPresenter otherCell = connection.GetOtherCell(currentCell.Item);
            WaveNode<CellPresenter> otherWaveCell = startNode.Get(otherCell);
            var transition = new Transition(otherCell, currentCell.Item, connection);
            path.Add(transition);
            currentCell = otherWaveCell;
        }
        path.Reverse();
        return path;
    }

    private void BuildPath(WaveNode<CellPresenter> startNode)
    {
        var treeRoot = startNode;
        while (VisitedCells.Count < Cells.Count)
        {
            IEnumerable<WaveNode<CellPresenter>> tails = treeRoot.GetTails();
            foreach (var tail in tails)
            {
                CellPresenter currentCell = tail.Item;
                IReadOnlyList<Connection> availableConnections = GetAvailableConnections(currentCell, false);
                foreach (var connection in availableConnections)
                {
                    CellPresenter otherCell = connection.GetOtherCell(currentCell);
                    if (IsCellVisited(otherCell))
                    {
                        WaveNode<CellPresenter> otherNode = startNode.Get(otherCell);

                        if (otherNode.Distance > tail.Distance + connection.Weight)
                            otherNode.UpdateFather(tail, connection.Weight);
                    }
                    else
                    {
                        SwitchCurrentCell(otherCell, connection);
                        var node = new WaveNode<CellPresenter>(otherCell, tail.Distance + 1);
                        tail.AddChild(node);
                    }

                }
            }
        }
    }
}