using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    [SerializeField] private Animator[] animators;

    public void UpdateHearts(int _index)
    {
        animators[_index].enabled = true;
    }
}
