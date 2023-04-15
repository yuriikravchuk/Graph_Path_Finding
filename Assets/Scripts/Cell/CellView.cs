using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Text))]
public class CellView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private TextMeshPro _text;

    private readonly Color _defaultColor = Color.yellow;
    private readonly Color _visitedColor = Color.red;
    private readonly Color _startColor = Color.green;
    private readonly Color _endColor = Color.red;


    public void SetDefaultColor() => SetColor(_defaultColor);
    public void SetVisitedColor() => SetColor(_visitedColor);
    public void SetStartColor() => SetColor(_startColor);
    public void SetEndColor() => SetColor(_endColor);
    public void SetNumber(int value)
    {
        _text.text = value.ToString();
    }

    private void SetColor(Color color) => _renderer.color = color;
}
