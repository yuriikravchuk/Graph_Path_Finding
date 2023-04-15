using UnityEngine;

public class ConnectionTypesChanger : MonoBehaviour
{
    private ComponentFinder _componentFinder;

    private void Awake()
    {
        _componentFinder = new ComponentFinder();
    }
    public void TryChangeType(Vector3 mousePosition)
    {
        Connection connection = _componentFinder.TryGetComponentFromScene<ConnectionColiderHandler>(mousePosition)?.Connection;
        connection?.ChangeConnectionType();
    }
}
