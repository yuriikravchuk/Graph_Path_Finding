using pathFinding;
using System.Collections.Generic;
using UnityEngine;

public class GameStatesFabric : MonoBehaviour
{
    [SerializeField] private BuildState _buildState;
    [SerializeField] private ViewResultsState _viewResultsState;
    [SerializeField] private SearchSettingsState _searchSettingsState;

    private IReadOnlyList<CellPresenter> _cells;

    public void Init(IReadOnlyList<CellPresenter> cells, PathFinder pathFinder)
    {
        _cells = cells;
        _viewResultsState.Init(_cells, pathFinder);
    }

    public List<GameState> GetStates()
    {
        return new List<GameState>()
        {
            _buildState,
            _viewResultsState,
            _searchSettingsState
        };
    }
}