using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ConnectionColiderHandler : MonoBehaviour, IDisposable
{
    [SerializeField] private Connection _connection;

    public Connection Connection => _connection;

    public void Dispose() => _connection.Dispose();
}