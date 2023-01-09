using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Engine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIView gameOverPanel;
    public UIView pauseMenuPanel;

    private bool _isPaused = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(_isPaused)
                HidePauseMenu();
            else
                ShowPauseMenu();
        }
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.Show();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowPauseMenu()
    {
        _isPaused = true;
        Time.timeScale = 0f;
        pauseMenuPanel.Show();
    }

    public void HidePauseMenu()
    {
        _isPaused = false;
        Time.timeScale = 1f;
        pauseMenuPanel.Hide();
    }
}
