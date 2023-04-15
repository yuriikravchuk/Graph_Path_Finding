using System;
using System.Collections.Generic;

public class Cell: IDisposable
{
    public IReadOnlyList<Connection> Connections => _connections;
    public int Number;

    private List<Connection> _connections = new List<Connection>();

    public void AddConnection(Connection connection)
    {
        _connections.Add(connection);
        connection.Disposed += OnConnectionDisposed;
    }

    public void Dispose()
    {
        int connectionsCount = _connections.Count;
        for (int i = 0; i < connectionsCount; i++)
            _connections[0].Dispose();
    }

    private void OnConnectionDisposed(Connection connection) 
        => _connections.Remove(connection);
}