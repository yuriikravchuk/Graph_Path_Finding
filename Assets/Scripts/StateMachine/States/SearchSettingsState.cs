using System;
using UnityEngine;

public class SearchSettingsState : GameState
{
    [SerializeField] private CellsSelector _cellsSelector;
    [SerializeField] private UI _ui;

    public bool Enabled;

    public override void Enter()
    {
        Enabled = true;
        _ui.ShowSelectCellsText();
        _ui.ShowDropdownAlgorithmSelector();
    }

    protected override void Exit()
    {
        Enabled = false;
        _ui.HideSelectCellsText();
        _cellsSelector.RestoreCells();
        _ui.HideDropdownAlgorithmSelector();
    }

    public override bool CanTransit(GameState state)
    {
        if (state is BuildState)
            return true;
        else if (state is ViewResultsState)
            return true;

        return false;
    }

    public override void Transit(GameState state)
    {
        if (state is BuildState)
        {
            Exit();
            return;
        }

        else if (state is ViewResultsState)
        {
            Exit();
            return;
        }

        throw new InvalidOperationException();
    }

    public void OnLeftClick(Vector3 mousePosition)
    {
        if (Enabled)
            _cellsSelector.OnLeftClick(mousePosition);
    }

    public void OnRightClick(Vector3 mousePosition)
    {
        if (Enabled)
            _cellsSelector.OnRightClick(mousePosition);
    }
}