using UnityEngine;

public class Disposer : MonoBehaviour
{
    private ComponentFinder _componentFinder;

    private void OnEnable()
    {
        _componentFinder = new ComponentFinder();
    }

    public void TryDispose(Vector3 mousePosition)
    {
        IDisposable element = _componentFinder.TryGetComponentFromScene<IDisposable>(mousePosition);
        element?.Dispose();
    }
}