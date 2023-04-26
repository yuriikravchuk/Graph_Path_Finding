using System.Collections.Generic;
using System.Linq;

namespace pathFinding
{
    public class DephFirstSearch : PathFindingAlgorithm
    {
        public DephFirstSearch(IReadOnlyList<CellPresenter> cells) : base(cells) { }

        protected override IReadOnlyList<Transition> GetPath(CellPresenter start, CellPresenter end)
        {
            Connection currentConnection;
            var path = new List<Transition>();
            while (CurrentCell != end)
            {
                currentConnection = FindNextConnection(CurrentCell);

                if (currentConnection == null)
                {
                    path.Remove(path.Last());
                    CellPresenter next = path.Count > 0 ? path.Last().To : start;
                    SwitchCurrentCell(next);
                }
                else
                {
                    CellPresenter next = currentConnection.GetOtherCell(CurrentCell);
                    path.Add(new Transition(CurrentCell, next, currentConnection));
                    SwitchCurrentCell(next, currentConnection);
                }
            }
            return path;
        }

        private Connection FindNextConnection(CellPresenter cell)
        {
            IReadOnlyList<Connection> connections = GetAvailableConnections(cell);

            if (connections.Count == 0)
                return null;

            int minWeight = connections.Min(item => item.Weight);
            return connections.First(item => item.Weight == minWeight);
        }
    }
}