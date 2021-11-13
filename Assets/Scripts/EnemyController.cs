using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float idleTime = 3f;
    private Animator animator;
    private Rigidbody2D rigidBody;
    
    private bool attack;
    private bool walking;
    private bool targetReached;
    private float idleCounter;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        attack = false;
    }

    private void FixedUpdate()
    {
        Animate();
    }

    void Animate()
    {
        attack = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        if (!attack)
        {
            if (walking)
            {
                if (targetReached)
                {
                    IdleAnimation();
                }
            }
            else
            {
                if (idleCounter>idleTime)
                {
                    walkAnimation();
                }
                else
                {
                    idleCounter += Time.deltaTime;
                }
            }
        }
    }

    void walkAnimation()
    {
       animator.SetBool("stop",false);
       walking = true;
    }

    void IdleAnimation()
    {
        animator.SetBool("stop", true);
        walking = false;
        idleCounter = 0;
    }

    void AttackAnimation()
    {
        if (!attack)
        {
            animator.SetTrigger("attack");
            attack = true;
            walking = false;
            idleCounter = 0;
        }
    }
    
    
}
