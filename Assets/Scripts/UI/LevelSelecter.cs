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

    public bool CheckLock()
    {
        return (level <= ProfileController.Getunlocked());
    }

    public void HandleUnlocking()
    {
        bool interactable= CheckLock();
        button.interactable = interactable;
        Color buttonColor = button.GetComponent<Image>().color;
        if (!interactable && buttonColor==Color.white)
        {
            button.GetComponent<Image>().color=Color.gray;
            button.GetComponent<ButtonAudioController>().enabled = false;
        }

        if (interactable && buttonColor==Color.gray)
        {
            buttonColor = Color.white;
            button.GetComponent<ButtonAudioController>().enabled = true;
        }
    }
}
