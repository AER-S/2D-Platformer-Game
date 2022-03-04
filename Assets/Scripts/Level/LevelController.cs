using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelWonPanel;
    private static LevelController instance;
    public  static LevelController Instance
    {
        get { return instance; }
    }
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
        int nextScene = sceneIndex + 1;
        ProfileController.UpdateLocked(nextScene);
        SceneManager.LoadScene(nextScene);
    }

    public void LevelWonPanel()
    {
        levelWonPanel.SetActive(true);
    }
}
