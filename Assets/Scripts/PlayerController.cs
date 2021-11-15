using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runFactor = 2f;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private ScoreController score;
    [SerializeField] private float repel = 3;

    private bool run;
    private bool jump;
    private bool crouch;
    private bool onGround;
    private bool hurt;
    private bool backward;
    private bool dead;
    
    
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        run = false;
        crouch = false;
        onGround = false;
        hurt = false;
        dead = false;
        backward = false;

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
        
        if (!dead && !hurt)
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
        
            if (!jump)
            {
                
                animator.SetFloat("speed",Mathf.Abs(_horizontal));
                animator.SetBool("crouch",crouch);
            }
            else
            {
                animator.SetFloat("speed", 0f);
            
            }
        }
    }


    void Move(float _horizontal, float _vertical)
    {

        if (!dead && !hurt)
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
                        Vector2 newMove = new Vector2(_horizontal * desiredSpeed, rigidBody.velocity.y);
                        rigidBody.velocity = newMove;
                    }
                    else if (speed<0.2f)
                    {
                        StopPlayer();
                    }

                    if (_vertical>0)
                    {
                        Vector2 newJump = new Vector2(rigidBody.velocity.x, jumpPower);
                        rigidBody.velocity = newJump;
                    }
                }
                else
                {
                    StopPlayer();
                }
            }
        }
        else
        {
            GoBackward();
        }
        
    }

    void GoBackward()
    {
        Vector2 velocity = rigidBody.velocity;
        if (!backward)
        {
            Vector2 backwardMovement = new Vector2(-repel*velocity.x, (velocity.y>0)? 0:velocity.y);
            rigidBody.velocity = backwardMovement;
            backward = true;
        }
        else if (backward && Mathf.Abs(velocity.x)<0.002f)
        {
            backward = false;
            hurt = false;
        }
    }

    void StopPlayer()
    {
        rigidBody.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        RaycastHit2D ground = Physics2D.BoxCast(transform.position, new Vector2(0.67f, 1f), 0f, Vector2.down, 0.05f);
        if (ground)
        {
            onGround = true;
            jump = false;
        } 
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") )
        {
            onGround = false;
        }
    }


    public void PickUp(int _score)
    {
        Debug.Log("Player picked up a key");
        score.UpdatScore(_score);
    }

    public void Hurt()
    {
        if (!hurt)
        {
            animator.SetTrigger("hurt");
            hurt = true;
        }

    }

    public void Die()
    {
        if (!dead)
        {
            animator.SetTrigger("die");
            dead = true;
        }

    }
}
