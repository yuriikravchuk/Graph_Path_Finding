using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using pathFinding;
public class Starter : MonoBehaviour
{
    [SerializeField] private CellsSpawner _cellsSpawner;
    [SerializeField] private ConnectionsSpawner _connectionsSpawner;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _loadButton;
    [SerializeField] private Button _clearButton;
    [SerializeField] private PlayButton _changeModeButton;
    [SerializeField] private GameStatesFabric _gameStatesFabric;
    [SerializeField] private CellsSelector _cellsSelector;
    [SerializeField] private Button _resetSearchingButton;
    [SerializeField] private DropdownAlgorithmSelector _dropdownAlgorithmSelector;

    private GameStateMachine _gameStateMachine;
    private SaveBinder _saveBinder;
    private CellsHandler _cellsHandler;

    private void OnEnable()
    {
        _cellsHandler = new CellsHandler();
        _cellsSpawner.Init(_cellsHandler);
        _saveBinder = new SaveBinder(_cellsSpawner, _connectionsSpawner, _cellsHandler);
        _saveButton.onClick.AddListener(_saveBinder.Save);
        _loadButton.onClick.AddListener(_saveBinder.Load);
        _clearButton.onClick.AddListener(_cellsHandler.DisposeAll);

        _dropdownAlgorithmSelector.Init(new List<PathFindingAlgorithm> 
        {
            new DephFirstSearch(_cellsHandler.Cells), 
            new BreadthFirstSearch(_cellsHandler.Cells), 
            new WaveSearch(_cellsHandler.Cells) 
        });

        var pathFinder = new PathFinder(_dropdownAlgorithmSelector);
        _cellsSelector.Init(pathFinder);
        _gameStatesFabric.Init(_cellsHandler.Cells, pathFinder);
        _gameStateMachine = new GameStateMachine(_gameStatesFabric.GetStates());


        var playStateMediator = new GameStateMediator<ViewResultsState>(_gameStateMachine);
        _cellsSelector.CellsSelected += playStateMediator.SetState;

        var buildStateMediator = new GameStateMediator<BuildState>(_gameStateMachine);
        _changeModeButton.BuildModeSelected += buildStateMediator.SetState;

        var selectCellsStateMediator = new GameStateMediator<SearchSettingsState>(_gameStateMachine);
        _changeModeButton.PlayModeSelected += selectCellsStateMediator.SetState;
        _resetSearchingButton.onClick.AddListener(selectCellsStateMediator.SetState);
    }
}