using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelSelecterPanel : MonoBehaviour
{
    [SerializeField] private RectTransform container;
    [SerializeField] private GameObject instance;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject lobbyPanel;

    [SerializeField] private int levels;

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
            GameObject buttonInstance = (GameObject)Instantiate(instance, container,false);
            buttonInstance.GetComponent<LevelSelecter>().SetLevel(level);
            buttonInstance.GetComponentInChildren<Text>().text = $"{level:00}";
        }
        
    }
}
