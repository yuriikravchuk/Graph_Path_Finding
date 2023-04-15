using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ViewResultsState : GameState
{
    [SerializeField] private UI _ui;

    private PathFinder _pathFinder;
    private SearchingResults _searchingResults;
    private IReadOnlyList<CellPresenter> _cells;

    private int _stepsCount = 0;

    public void Init(IReadOnlyList<CellPresenter> cells, PathFinder pathFinder)
    {
        _cells = cells;
        _pathFinder = pathFinder;
    }

    public override void Enter()
    {
        _stepsCount = 0;
        _ui.ShowPlayButtons();
        _ui.ShowSelectedCellPointer();
        _searchingResults = _pathFinder.GetResults();
        _ui.ShowResultsText(_searchingResults.SearchingDuration, _searchingResults.SearchingPath.Count);
        CellPresenter start = _searchingResults.Path[0].From;
        _ui.SetSelctedCellPointerPosition(start.transform.position);
    }

    protected override void Exit()
    {
        _ui.HidePlayButtons();
        _ui.HideSelectedCellPointer();
        _ui.HideResultsText();
        _searchingResults = null;
        ResetGraphProgress();
    }

    public override bool CanTransit(GameState state)
    {
        if (state is SearchSettingsState)
            return true;
        else if (state is BuildState)
            return true;

        return false;
    }

    public override void Transit(GameState state)
    {
        if (state is SearchSettingsState)
        {
            Exit();
            return;
        }
        if (state is BuildState)
        {
            Exit();
            return;
        }

        throw new InvalidOperationException();
    }

    public void Step()
    {
        var transition = _searchingResults.SearchingPath[_stepsCount];

        transition.Connection?.SetCompleted();
        SetCurrentCell(transition.To);
        SetCellVisited(transition.To);

        if (_stepsCount == _searchingResults.SearchingPath.Count - 1)
        {
            OnPathFinded();
            return;
        }

        _stepsCount++;
    }

    private void ResetGraphProgress()
    {
        foreach (var cell in _cells)
        {
            cell.SetDefaultColor();
            foreach (var connection in cell.Connections)
                connection.ResetProgress();
        }
    }

    private void SetCurrentCell(CellPresenter cell) => _ui.SetSelctedCellPointerPosition(cell.transform.position);

    private void SetCellVisited(CellPresenter cell) => cell.SetVisitedColor();

    private void OnPathFinded()
    {
        _ui.HideNextButton();
        _ui.HideSelectedCellPointer();
        HideCellsNotInPath();
    }

   private void HideCellsNotInPath()
   {
        ResetGraphProgress();

        _searchingResults.Path.First().From.SetStartColor();

        foreach (var transition in _searchingResults.Path)
        {
            SetCellVisited(transition.To);
            Connection connection = transition.From.FindConnectionWithCell(transition.To);
            connection.SetCompleted();
        }
        
    }
}