using UnityEngine;

public class ConnectionWeightsChanger : MonoBehaviour
{
    [SerializeField] private WeightsInput _weightsInput;

    private Connection _currentConnection;
    private ComponentFinder _componentFinder;


    private void Awake()
    {
        _componentFinder = new ComponentFinder();
        _weightsInput.WeightInputed += SetWeight;
    }

    private void SetWeight(int weight)
    {
        _currentConnection?.SetWeight(weight);
        _currentConnection = null;
    }

    public void OnLeftClick(Vector3 mousePosition)
    {
        _currentConnection = _componentFinder.TryGetComponentFromScene<ConnectionColiderHandler>(mousePosition)?.Connection;
        if (_currentConnection == null)
            return;

        _weightsInput.gameObject.SetActive(true);
    }
}
