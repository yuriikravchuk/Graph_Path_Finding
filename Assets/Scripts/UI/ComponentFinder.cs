using UnityEngine;

public class ComponentFinder
{
    public T TryGetComponentFromScene<T>(Vector3 mousePosition) where T : class
    {
        RaycastHit2D hit = Physics2D.Linecast(mousePosition, mousePosition);
        return hit.collider?.GetComponent<T>();
    }
}