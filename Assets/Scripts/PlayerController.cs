using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runFactor = 2f;
    [SerializeField] private float jumpPower = 2f;

    private bool run;
    private bool jump;
    private bool crouch;
    private bool onGround;
    
    
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        run = false;
        crouch = false;
        onGround = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        run = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);
        
        Animate(horizontal, vertical);
        Move(horizontal, vertical);
    }

    void Animate(float _horizontal, float _vertical)
    {
        
        if (Mathf.Abs(_horizontal) > 0.2f)
        {
            transform.rotation = Quaternion.Euler(0f, 90 - Mathf.Sign(_horizontal) * 90, 0f);
        }

        if (_vertical>0 && onGround && !jump)
        {
            animator.SetTrigger("jump");
            jump = true;
        }
        
        animator.SetBool("run",run);
        
        if (onGround)
        {
            animator.SetFloat("speed",Mathf.Abs(_horizontal));
            animator.SetBool("crouch",crouch);
        }
        else
        {
            animator.SetFloat("speed", 0f);
            
        }
    }

    void Move(float _horizontal, float _vertical)
    {

        if (onGround)
        {
            if (!crouch)
            {
                float speed = Mathf.Abs(_horizontal);
                float xMovingSpeed = Mathf.Abs(rigidBody.velocity.x);
                float desiredSpeed = (run)? (walkSpeed*runFactor):walkSpeed;
                if (speed>0.2f && xMovingSpeed<desiredSpeed)
                {
                    rigidBody.velocity = Vector2.right * (_horizontal * desiredSpeed);
                }
                else if (speed<0.2f)
                {
                    StopPlayer();
                }

                if (_vertical>0)
                {
                    rigidBody.velocity += Vector2.up * jumpPower;
                }
            }
            else
            {
                StopPlayer();
            }
        }
        
    }

    void StopPlayer()
    {
        rigidBody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && rigidBody.velocity.y < 0)
        {
            onGround = true;
            jump = false;
        } 
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && rigidBody.velocity.y > 0)
        {
            onGround = false;
        }
    }
}
