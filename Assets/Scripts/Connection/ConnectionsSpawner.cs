using UnityEngine;
using static Connection;

public class ConnectionsSpawner : MonoBehaviour
{
    [SerializeField] private Connection _prefab;
    [SerializeField] private Transform _spawnParent;

    private CellPresenter _startCell;
    private ComponentFinder _componentFinder;

    private void Start()
    {
        _componentFinder = new ComponentFinder();
    }

    public void TryGetCellToSpawn(Vector3 mousePosition) {
        CellPresenter cell = _componentFinder.TryGetComponentFromScene<CellPresenter>(mousePosition);

        if (cell == null)
        {
            _startCell = null;
            return;
        }
            
        if (_startCell != null)
            TrySpawn(_startCell, cell);

        _startCell = cell;
    }

    public void TrySpawn(CellPresenter startCell, CellPresenter endCell, int weight = 1, ConnectionType type = ConnectionType.Mutual)
    {
        if (startCell == endCell)
            return;

        if (startCell.FindConnectionWithCell(endCell) != null)
            return;

        Connection connection = Instantiate(_prefab, _spawnParent);
        connection.Init(startCell, endCell, weight, type);
        startCell.AddConnection(connection);
        endCell.AddConnection(connection);
    }
}