using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace pathFinding
{
    public abstract class PathFindingAlgorithm
    {
        protected CellPresenter CurrentCell { get; private set; }
        protected List<CellPresenter> VisitedCells { get; private set; }
        protected IReadOnlyList<CellPresenter> Cells { get; private set; }

        private List<Transition> _transitions;

        public PathFindingAlgorithm(IReadOnlyList<CellPresenter> cell)
        {
            Cells = cell;
        }

        public SearchingResults GetSearchingResults(CellPresenter start, CellPresenter end)
        {
            CurrentCell = start;
            VisitedCells = new List<CellPresenter> {start};
            _transitions = new List<Transition>();
            IReadOnlyList<Transition> path = GetPath(start, end);
            return new SearchingResults(_transitions, path, Time.deltaTime * 1000);
        }

        protected abstract IReadOnlyList<Transition> GetPath(CellPresenter start, CellPresenter end);


        protected IReadOnlyList<Connection> GetAvailableConnections(CellPresenter cell)
        {
            var result = new List<Connection>();
            foreach (var connection in cell.Connections)
            {
                if (connection.CanTransit(cell) == false)
                    continue;

                CellPresenter cellToTransit = connection.GetOtherCell(cell);

                if (VisitedCells.Any(item => item == cellToTransit))
                    continue;

                result.Add(connection);
            }

            return result;
        }

        protected void SwitchCurrentCell(CellPresenter next, Connection connection = null)
        {
            Transition transition = new Transition(CurrentCell, next, connection);

            if(connection != null)
                VisitedCells.Add(next);

            _transitions.Add(transition);
            CurrentCell = next;
        }
    }

    public struct Transition
    {
        public readonly CellPresenter From;
        public readonly CellPresenter To;
        public readonly Connection Connection;

        public Transition(CellPresenter from, CellPresenter to, Connection connection = null)
        {
            From = from;
            To = to;
            Connection = connection;
        }
    }
}
