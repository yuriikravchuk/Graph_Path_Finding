using System.Collections.Generic;

public class CellsHandler
{
    public IReadOnlyList<CellPresenter> Cells => _cells;

    public CellsHandler() => _cells = new List<CellPresenter>();

    private List<CellPresenter> _cells;

    public void Add(CellPresenter cell)
    {
        _cells.Add(cell);
        UpdateCellNumbers();
    }

    public void Remove(CellPresenter cell)
    {
        _cells.Remove(cell);
        UpdateCellNumbers();
    }

    public void DisposeAll()
    {
        int _cellsCount = _cells.Count;
        for (int i = 0; i < _cellsCount; i++)
            _cells[0].Dispose();
    }

    private void UpdateCellNumbers()
    {
        for(int i = 0; i < _cells.Count; i++)
            _cells[i].Number = i+1;
    }
}