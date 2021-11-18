using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelecterPanel : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private LevelSelecter instance;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject lobbyPanel;

    [SerializeField] private int levels;
    private List<LevelSelecter> levelButtons = new List<LevelSelecter>();

    private void Awake()
    {
        closeButton.onClick.AddListener(ReturnToLobby);
    }

    private void ReturnToLobby()
    {
        lobbyPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        for(int level =1;level<=levels;level++)
        {
            LevelSelecter buttonInstance = Instantiate(instance, container,false);
            levelButtons.Add(buttonInstance);
            buttonInstance.SetLevel(level);
            buttonInstance.GetComponentInChildren<Text>().text = $"{level:00}";
        }
        UpdateLocks();
    }


    public void UpdateLocks()
    {
        
        foreach (LevelSelecter button in levelButtons)
        {
            button.HandleUnlocking();
        }
    }

}
