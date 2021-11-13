using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool attack;
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        attack = false;
    }

    void walkAnimation()
    {
       animator.SetBool("stop",false); 
    }

    void IdleAnimation()
    {
        animator.SetBool("stop", true);
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
            attack = false;
        }
    }
    
    
}
