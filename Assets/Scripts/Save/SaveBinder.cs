using System.Collections.Generic;
using UnityEngine;

public class SaveBinder
{
    private SaveProvider<Save> _saveProvider;
    private CellsHandler _cellsHandler;
    private CellsSpawner _cellsSpawner;
    private ConnectionsSpawner _connectionsSpawner;

    public SaveBinder(CellsSpawner cellsSpawner, ConnectionsSpawner connectionsSpawner, CellsHandler cellsHandler)
    {
        _saveProvider = new SaveProvider<Save>();
        _cellsSpawner = cellsSpawner;
        _connectionsSpawner = connectionsSpawner;
        _cellsHandler = cellsHandler;
    }

    public void Save()
    {
        IReadOnlyList<CellPresenter> cells = _cellsHandler.Cells;
        var data = new CellData[cells.Count];

        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
        {
            CellPresenter cell = cells[cellIndex];
            ConnectionData[] connections = GetDataFromConnections(cell.Connections);
            data[cellIndex] = new CellData(cell.transform.position, connections);
        }

        var save = new Save(data);
        _saveProvider.UpdateSave(save, "save");

        Debug.Log(Application.persistentDataPath);
    }

    public void Load()
    {
        Save save = _saveProvider.TryGetSave("save");
        CellData[] data = save.Data;
        _cellsHandler.DisposeAll();
        SpawnCells(data);

        for(int i = 0; i < data.Length; i++)
        {
            SpawnConnectionsInCell(data[i].Connections);
        }
    }

    private void SpawnConnectionsInCell(ConnectionData[] data)
    {
        IReadOnlyList<CellPresenter> cells = _cellsHandler.Cells;

        for (int i = 0; i < data.Length; i++)
        {
            CellPresenter start = cells[data[i].StartCellNumber - 1];
            CellPresenter end = cells[data[i].EndCellNumber - 1];
            _connectionsSpawner.TrySpawn(start, end, data[i].Weight, data[i].ConnectionType);
        }

    }

    private void SpawnCells(CellData[] data)
    {
        for (int i = 0; i < data.Length; i++)
            _cellsSpawner.Spawn(data[i]._position.ToVector3());
    }

    private ConnectionData[] GetDataFromConnections(IReadOnlyList<Connection> connections)
    {
        var data = new ConnectionData[connections.Count];

        for (int i = 0; i < connections.Count; i++)
            data[i] = new ConnectionData(connections[i].Type, connections[i].StartNumber, connections[i].EndNumber, connections[i].Weight);

        return data;
    }
}