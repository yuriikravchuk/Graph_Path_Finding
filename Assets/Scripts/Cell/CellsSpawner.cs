using UnityEngine;

public class CellsSpawner : MonoBehaviour
{
    [SerializeField] private CellPresenter _prefab;
    [SerializeField] private Transform _cellsParrent;

    private CellsHandler _cellsHandler;

    public void Init(CellsHandler cellsHandler) 
        => _cellsHandler = cellsHandler;

    public void Spawn(Vector3 position)
    {
        CellPresenter cell = Instantiate(_prefab, _cellsParrent);
        cell.transform.position = new Vector3(position.x, position.y, 0.99f);
        _cellsHandler.Add(cell);
        cell.Disposed += OnCellDisposed;
    }

    public void OnCellDisposed(CellPresenter cell) => _cellsHandler.Remove(cell);
}