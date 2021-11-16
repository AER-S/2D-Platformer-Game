using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public static LevelController instance;
    private int sceneIndex;
    

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }
}
