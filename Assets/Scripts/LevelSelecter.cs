using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private int level;
    private Button button;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(GotoLevel);
    }

    public void SetLevel(int _level)
    {
        level = _level;
    }

    void GotoLevel()
    {
        SceneManager.LoadScene(level);
    }
}
