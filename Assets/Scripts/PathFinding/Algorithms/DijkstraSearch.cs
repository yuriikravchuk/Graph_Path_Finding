using pathFinding;
using System.Collections.Generic;
using System.Linq;

public class DijkstraSearch : PathFindingAlgorithm
{
    public DijkstraSearch(IReadOnlyList<CellPresenter> cells) : base(cells) { }

    protected override IReadOnlyList<Transition> GetPath(CellPresenter start, CellPresenter end)
    {
        var startNode = new WaveNode<CellPresenter>(start);
        BuildPath(startNode);

        WaveNode<CellPresenter> endNode = startNode.FindNode(end);
        IReadOnlyList<WaveNode<CellPresenter>> nodePath = endNode.GetPath();
        var path = new List<Transition>();
        for(int i = 0; i < nodePath.Count - 1; i++)
        {
            CellPresenter from = nodePath[i + 1].Item;
            CellPresenter to = nodePath[i].Item;

            Connection connection = from.FindConnectionWithCell(to);
            var transition = new Transition(from, to, connection);
            path.Add(transition);
            if (i > 100)
            {
                throw new System.StackOverflowException();
            }
        }

        
        path.Reverse();
        return path;
    }

    private void BuildPath(WaveNode<CellPresenter> startNode)
    {
        int i = 0;
        while (i < 100)
        {
            IEnumerable<WaveNode<CellPresenter>> tails = startNode.GetTails();

            foreach (var tail in tails)
            {
                CellPresenter currentCell = tail.Item;
                IReadOnlyList<Connection> availableConnections = GetAvailableConnections(currentCell, false);
                foreach (var connection in availableConnections)
                {
                    CellPresenter nextCell = connection.GetOtherCell(currentCell);
                    WaveNode<CellPresenter> otherNode = startNode.FindNode(nextCell);

                    if (otherNode == null)
                    {
                        otherNode = new WaveNode<CellPresenter>(nextCell, tail.Distance + connection.Weight, tail);
                        tail.AddChild(otherNode);
                        SwitchCurrentCell(nextCell, connection);
                    }
                    else if(otherNode != tail.Father)
                    {

                        if (otherNode.Distance > tail.Distance + connection.Weight)
                            otherNode.UpdateFather(tail, connection.Weight);
                    }

                }
            }
            if (i == 100)
                break;

            i++;
        }
    }
}