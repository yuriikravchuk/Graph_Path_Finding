using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _resetButton;
    [SerializeField] private GameObject _buildModeButtons;
    [SerializeField] private GameObject _selectCellsText;
    [SerializeField] private GameObject _selectedCellPoiner;
    [SerializeField] private Text _resultsText;
    [SerializeField] private GameObject _dropdownAlgorithmSelector;

    public void ShowPlayButtons()
    {
        _nextButton.SetActive(true);
        _resetButton.SetActive(true);
    }
    public void HidePlayButtons()
    {
        _nextButton.SetActive(false);
        _resetButton.SetActive(false);
    }

    public void HideNextButton() => _nextButton.SetActive(false);

    public void ShowBuildModeButtons() => _buildModeButtons.SetActive(true);
    public void HideBuildModeButtons() => _buildModeButtons.SetActive(false);

    public void ShowSelectCellsText() => _selectCellsText.SetActive(true);
    public void HideSelectCellsText() => _selectCellsText.SetActive(false);

    public void ShowSelectedCellPointer() => _selectedCellPoiner.SetActive(true);
    public void HideSelectedCellPointer() => _selectedCellPoiner.SetActive(false);

    public void SetSelctedCellPointerPosition(Vector3 position) => _selectedCellPoiner.transform.position = position;

    public void ShowResultsText(float searchingDuration,int stepCount)
    {
        _resultsText.text = "Searching duration: " + searchingDuration.ToString() + " ms \n" + "Steps count: " + stepCount.ToString();
        _resultsText.gameObject.SetActive(true);
    }
    public void HideResultsText() => _resultsText.gameObject.SetActive(false);

    public void ShowDropdownAlgorithmSelector() => _dropdownAlgorithmSelector.SetActive(true);
    public void HideDropdownAlgorithmSelector() => _dropdownAlgorithmSelector.SetActive(false);

}