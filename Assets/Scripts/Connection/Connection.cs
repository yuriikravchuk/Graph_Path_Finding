using System;
using TMPro;
using UnityEngine;

public class Connection : MonoBehaviour, IDisposable
{
    [SerializeField] private Transform _line;
    [SerializeField] private Transform _progressBar;
    [SerializeField] private TextMeshPro _weightView;
    [SerializeField] private GameObject _startPointer;
    [SerializeField] private GameObject _endPointer;

    public event Action<Connection> Disposed;
    public enum ConnectionType { Mutual = 0, StartToEnd = 1, EndToStart = 2 }
    public ConnectionType Type { get; private set; }
    public int Weight { get; private set; }

    public int StartNumber => _start.Number;
    public int EndNumber => _end.Number;

    private CellPresenter _start, _end;

    public void Init(CellPresenter start, CellPresenter end, int weight = 1, ConnectionType type = ConnectionType.Mutual)
    {
        _start = start;
        _end = end;
        transform.position = start.transform.position;
        transform.LookAt(end.transform, Vector3.up);
        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        _line.localScale = new Vector3(1, 1, distance);
        SetWeight(weight);
        _weightView.transform.position = (start.transform.position + end.transform.position) / 2;
        _weightView.transform.rotation = Quaternion.Euler(0, 0, 0);
        SetConnectionType(type);
        _endPointer.transform.position = end.transform.position;
    }

    private void SetConnectionType(ConnectionType type)
    {
        Type = type;
        UpdateConnectionTypeView();
    }

    private void UpdateConnectionTypeView()
    {
        switch (Type)
        {
            case ConnectionType.Mutual:
                _startPointer.SetActive(true);
                _endPointer.SetActive(true);
                break;
            case ConnectionType.StartToEnd:
                _startPointer.SetActive(false);
                _endPointer.SetActive(true);
                break;
            case ConnectionType.EndToStart:
                _startPointer.SetActive(true);
                _endPointer.SetActive(false);
                break;
        }
    }

    public CellPresenter GetOtherCell(CellPresenter cell)
    {
        if (cell == _start)
            return _end;
        else if (cell == _end)
            return _start;
        else
            throw new InvalidOperationException();
    }

    public void Dispose()
    {
        Disposed.Invoke(this);
        Destroy(gameObject);
    }

    public void ChangeConnectionType()
    {
        SetConnectionType(GetNextType());
    }

    public bool CanTransit(CellPresenter cell)
    {
        switch (Type)
        {
            case ConnectionType.Mutual:
                return true;
            case ConnectionType.StartToEnd:
                return cell == _start;
            case ConnectionType.EndToStart:
                return cell == _end;
            default:
                throw new InvalidOperationException();
        }
    }

    public void SetCompleted()
    {
        _progressBar.localScale = new Vector3(1, 1, 1);
    }

    public void ResetProgress()
    {
        _progressBar.localScale = new Vector3(1, 1, 0);
    }

    public void ShowWeights() => _weightView.gameObject.SetActive(true);
    public void HideWeights() => _weightView.gameObject.SetActive(true);

    public void SetWeight(int weight)
    {
        Weight = weight;
        _weightView.text = Weight.ToString();
    }

    private ConnectionType GetNextType()
    {
        int typeNumber = (int)Type;
        int typesCount = Enum.GetNames(typeof(ConnectionType)).Length;

        typeNumber = typeNumber < typesCount - 1 ? typeNumber + 1 : 0;
        return (ConnectionType)typeNumber;
    }
}