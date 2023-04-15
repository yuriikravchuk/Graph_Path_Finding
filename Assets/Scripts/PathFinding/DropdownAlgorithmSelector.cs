using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using pathFinding;

[RequireComponent(typeof(Dropdown))] 
public class DropdownAlgorithmSelector : MonoBehaviour, IPathFindingAlgorithmProvider
{
    [SerializeField] private Dropdown _dropdown;

    public PathFindingAlgorithm CurrentAlgorithm { get; private set; }
    public IReadOnlyList<PathFindingAlgorithm> _algorithms;

    public void Init(IReadOnlyList<PathFindingAlgorithm> algorithms)
    {
        _algorithms = algorithms;
        CurrentAlgorithm = _algorithms[0];
        var options = new List<string>();

        foreach (var algorithm in _algorithms)
            options.Add(algorithm.GetType().Name);
        
        _dropdown.AddOptions(options);
        _dropdown.onValueChanged.AddListener(OnDropDownValueChanged);
    }

    private void OnDropDownValueChanged(int index)
    {
        var next = _algorithms[index];

        if (CurrentAlgorithm == next)
            return;

        CurrentAlgorithm = next;
    }
}