using System;
using System.Collections.Generic;
using UnityEngine;

public class CellPresenter : MonoBehaviour, IDisposable
{
    [SerializeField] private CellView _view;

    public int Number
    {
        get => _number;
        set
        {
            _number = value;
            _view.SetNumber(value);
        }
    }
    public IReadOnlyList<Connection> Connections => _model.Connections;
    public event Action<CellPresenter> Disposed;

    private Cell _model = new Cell();
    private int _number;

    public void AddConnection(Connection connection) => _model.AddConnection(connection);

    public Connection FindConnectionWithCell(CellPresenter cell)
    {
        foreach (var connection in Connections)
        {
            if (connection.GetOtherCell(this) == cell)
                return connection;
        }
        return null;
    }

    public void Dispose()
    {
        _model.Dispose();
        Disposed.Invoke(this);
        Destroy(gameObject);
    }

    public void SetVisitedColor() => _view.SetVisitedColor();

    public void SetDefaultColor() => _view.SetDefaultColor();

    public void SetStartColor() => _view.SetStartColor();

    public void SetEndColor() => _view.SetEndColor();
}