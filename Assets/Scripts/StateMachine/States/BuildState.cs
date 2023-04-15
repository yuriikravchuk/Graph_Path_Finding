using System;
using UnityEngine;

public class BuildState : GameState
{
    [SerializeField] private CellsSpawner _cellsSpawner;
    [SerializeField] private ConnectionsSpawner _connectionsSpawner;
    [SerializeField] private ConnectionWeightsChanger _connectionweightsChanger;
    [SerializeField] private ConnectionTypesChanger _connectionTypesChanger;
    [SerializeField] private Disposer _disposer;

    [SerializeField] private UI _ui;

    public bool Enabled;

    public override void Enter()
    {
        Enabled = true;
        _ui.ShowBuildModeButtons();
    }

    protected override void Exit()
    {
        Enabled = false;
        _ui.HideBuildModeButtons();
    }

    public override bool CanTransit(GameState state)
    {
        if(state is SearchSettingsState)
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

        throw new InvalidOperationException();
    }

    public void OnLeftClick(Vector3 mousePosition)
    {
        if (Enabled)
        {
            _connectionsSpawner.TryGetCellToSpawn(mousePosition);
            _connectionweightsChanger.OnLeftClick(mousePosition);
        }

    }

    public void OnRightClick(Vector3 mousePosition)
    {
        if (Enabled)
            _disposer.TryDispose(mousePosition);
    }

    public void OnShiftAndLeftClick(Vector3 mousePosition)
    {
        if (Enabled)
            _cellsSpawner.Spawn(mousePosition);
    }

    public void OnShiftAndRightClick(Vector3 mousePosition)
    {
        if (Enabled)
            _connectionTypesChanger.TryChangeType(mousePosition);
    }
}