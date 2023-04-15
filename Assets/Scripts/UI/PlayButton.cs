using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    [SerializeField] private Text _buttonText;

    public event Action PlayModeSelected;
    public event Action BuildModeSelected;

    private bool _isPlayMode = false;

    public void ChangeMode()
    {
        _isPlayMode = !_isPlayMode;
        if (_isPlayMode)
        {
            PlayModeSelected.Invoke();
            _buttonText.text = "Play";
        }
        else
        {
            BuildModeSelected.Invoke();
            _buttonText.text = "Build";
        }

    }
}