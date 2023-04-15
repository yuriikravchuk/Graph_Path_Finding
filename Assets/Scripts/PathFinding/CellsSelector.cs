using System;
using UnityEngine;

public class CellsSelector : MonoBehaviour
{
    public event Action CellsSelected;

    private CellPresenter _start, _end;
    private ComponentFinder _componentFinder;
    private ICellsForSearchHandler _cellsForSearchHandler;

    public void Init(ICellsForSearchHandler cellsForSearchHandler)
    {
        _cellsForSearchHandler = cellsForSearchHandler;
        _componentFinder = new ComponentFinder();

    }

    public void OnLeftClick(Vector3 mousePosition)
    {
        CellPresenter cell = _componentFinder.TryGetComponentFromScene<CellPresenter>(mousePosition);

        if (cell == null || cell == _start)
            return;

        if (_start != null)
            _start.SetDefaultColor();

        _start = cell;
        _start.SetStartColor();

        if (_end != null)
            SelectCells();
        
    }

    public void OnRightClick(Vector3 mousePosition)
    {
        CellPresenter cell = _componentFinder.TryGetComponentFromScene<CellPresenter>(mousePosition);

        if (cell == null || cell == _end)
            return;

        if (_end != null)
            _end.SetDefaultColor();

        _end = cell;
        _end.SetEndColor();

        if (_start != null)
            SelectCells();
    }

    public void RestoreCells()
    {
        _start = null;
        _end = null;
    }

    private void SelectCells()
    {
        _cellsForSearchHandler.SetCells(_start, _end);
        CellsSelected.Invoke();
    }
}