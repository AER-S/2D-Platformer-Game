using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] private Transform[] roadPoints;
    [SerializeField] private float idleTime = 3f;
    [SerializeField] private float walkSpeed = 2f;
    private Animator animator;
    private Rigidbody2D rigidBody;
    private bool attack;
    private bool walking;
    private bool targetReached;
    private float idleCounter;
    private Transform target;
    private int index;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody =GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        attack = false;
         index = 0;
         target = roadPoints[index];
       
        targetReached = Mathf.Abs(GetTargetXDistance()) < 0.02f;
        walking = true;
    }

    private void FixedUpdate()
    {
        targetReached = MathF.Abs(GetTargetXDistance()) < 0.02f;
        if (targetReached)
        {
            index++;
            if (index==roadPoints.Length)
            {
                index = 0;
            }

            target = roadPoints[index];
        }
        Animate();
        Move();
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
                else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) 
                {
                    IdleAnimation();
                }
                else
                {
                    idleCounter += Time.fixedDeltaTime;
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

    void Move()
    {
        if (walking)
        {
            float direction = Mathf.Sign(GetTargetXDistance());
            transform.rotation = Quaternion.Euler(0f, 90-direction*90,0f);
            rigidBody.velocity = Vector2.right * (direction * walkSpeed);
        }
    }

    float GetTargetXDistance()
    {
        return (target.position.x - transform.position.x);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            AttackAnimation();
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            transform.rotation = Quaternion.Euler(0f, 90-Mathf.Sign(player.transform.position.x-transform.position.x)*90f,0f);
            player.ReduceHealth();
        }
    }
}
