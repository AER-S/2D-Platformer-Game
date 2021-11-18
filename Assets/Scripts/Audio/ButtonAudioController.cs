using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

[RequireComponent(typeof(Button))]
public class ButtonAudioController : EventTrigger
{

    public UISound clickSound;
    public override void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUI(UISound.Havor);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUI(UISound.Exit);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayUI(clickSound);
    }
}
