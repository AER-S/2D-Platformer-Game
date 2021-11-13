using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float idleTime = 3f;
    private Animator animator;
    private bool attack;
    private bool walking;
    private bool targetReached;
    private float idleCounter;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
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
        }
        else if (attack && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            IdleAnimation();
            attack = false;
        }
    }
    
    
}
