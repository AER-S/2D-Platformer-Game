using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject levelSlecter;
    public void StartGame()
    {
        ProfileController.UpdateProfile();
        levelSlecter.SetActive(true);
        levelSlecter.GetComponent<LevelSelecterPanel>().UpdateLocks();
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
