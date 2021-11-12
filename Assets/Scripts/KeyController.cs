using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    [SerializeField] private int keyValue = 100;
    private Animator animator;
    private bool fadingOut;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        fadingOut = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.PickUp(keyValue);
            if (!fadingOut)
            {
                animator.SetTrigger("fadeOut");
                fadingOut = true;
            }

            StartCoroutine("FadeOut");
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
