using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private Animator[] animators;

    public void UpdateHearts(int _index)
    {
        if (_index>=0)
        {
            animators[_index].enabled = true;
        }
    }
}
