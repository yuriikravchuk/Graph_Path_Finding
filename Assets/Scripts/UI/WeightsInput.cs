using UnityEngine;
using TMPro;
using System;

public class WeightsInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField _field;

    public event Action<int> WeightInputed;

    private bool _enabled = false;

    private void Awake()
    {
        _field.onSubmit.AddListener(OnSubmit);
        _field.onDeselect.AddListener(e => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        _enabled = true;
        _field.Select();
    }

    private void OnDisable()
    {
        _enabled = false;
        _field.text = "";
    }

    public void OnSubmit(string data)
    {
        if (_enabled && _field.text.Length > 0)
        {
            WeightInputed.Invoke(int.Parse(data));
            gameObject.SetActive(false);
        }
    }
}
