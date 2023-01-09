using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject[] tutorialPanels;
    public Button tutorialBackButton;
    public Button tutorialForwardButton;

    private int _tutorialCount;
    private int _currentTutorial;
    private void Start()
    {
        _tutorialCount = tutorialPanels.Length;
        _currentTutorial = 0;
        EnableTutorialAtIndex(_currentTutorial);
        tutorialBackButton.onClick.AddListener(ShowPreviousTutorial);
        tutorialForwardButton.onClick.AddListener(ShowNextTutorial);
    }

    public void ShowNextTutorial()
    {
        _currentTutorial++;
        if (_currentTutorial >= _tutorialCount)
        {
            _currentTutorial = _tutorialCount - 1;
        }

        EnableTutorialAtIndex(_currentTutorial);
    }
    public void ShowPreviousTutorial()
    {
        _currentTutorial--;
        if (_currentTutorial < 0)
        {
            _currentTutorial = 0;
        }
        EnableTutorialAtIndex(_currentTutorial);
    }

    private void EnableTutorialAtIndex(int index)
    {
        for (int i = 0; i < tutorialPanels.Length; i++)
        {
            if (i == index)
            {
                tutorialPanels[i].SetActive(true);
            }
            else
            {
                tutorialPanels[i].SetActive(false);
            }
        }
    } 

    public void QuitGame()
    {
        Application.Quit();
    }
}
