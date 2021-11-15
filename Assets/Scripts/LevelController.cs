using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    public static LevelController instance;
    

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    
}
