using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSoundsController : MonoBehaviour
{
    [SerializeField] private UISound clickSound;
    

    private void Awake()
    {
        ButtonAudioController _buttonAudioController = gameObject.AddComponent<ButtonAudioController>();
        _buttonAudioController.clickSound = clickSound;
    }
}
