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
    private List<GameObject> levelButtons = new List<GameObject>();

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
            levelButtons.Add(buttonInstance);
            LevelSelecter buttonInstanceButtonSelector = buttonInstance.GetComponent<LevelSelecter>();
            buttonInstanceButtonSelector.SetLevel(level);
            buttonInstance.GetComponentInChildren<Text>().text = $"{level:00}";
        }
        UpdateLocks();
    }
    
    

    public void UpdateLocks()
    {
        
        foreach (GameObject button in levelButtons)
        {
            bool interactable=button.GetComponent<LevelSelecter>().CheckLock();
            button.GetComponent<Button>().interactable = interactable;
            Color buttonColor = button.GetComponent<Image>().color;
            if (!interactable && buttonColor==Color.white)
            {
                button.GetComponent<Image>().color=Color.gray;
            }

            if (interactable && buttonColor==Color.gray)
            {
                buttonColor = Color.white;
            }
        }
    }
}
