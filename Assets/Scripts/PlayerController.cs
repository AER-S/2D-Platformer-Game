using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private ScoreController score;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private Transform bottomLine;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runFactor = 2f;
    [SerializeField] private float jumpPower = 2f;
<<<<<<< HEAD
    [SerializeField] private ScoreController score;
    [SerializeField] private float repel = 3;
<<<<<<< HEAD
    [SerializeField] private HeartController heartController;
=======
    [SerializeField] private Transform bottomLine;
>>>>>>> Feature_3_Movement
=======
    [SerializeField] private float repel = 3f;
>>>>>>> Feature_3_Movement

    private bool run;
    private bool jump;
    private bool crouch;
    private bool onGround;
    private bool hurt;
    private bool backward;
    private bool dead;
    private int hearts;
    

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
        hearts = 3;

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
            animator.SetBool("run",run);
        
            if (onGround)
            {
                animator.SetBool("jump",false);
                animator.SetFloat("speed",Mathf.Abs(_horizontal));
                animator.SetBool("crouch",crouch);
            }
            else
            {
                animator.SetFloat("speed", 0f);
            
            }
            if (_vertical>0 && onGround && !jump)
            {
                jump = true;
                animator.SetBool("jump",true);
                             
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
        else if (backward && Mathf.Abs(velocity.x)<0.02f)
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
<<<<<<< HEAD
<<<<<<< HEAD
        RaycastHit2D ground = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 1f), 0f, Vector2.down, 0.05f);
=======
        RaycastHit2D ground = Physics2D.BoxCast(bottomLine.position, new Vector2(0.5f, 0.001f), 0f, Vector2.down, 0.05f);
>>>>>>> Feature_3_Movement
=======
        RaycastHit2D ground = Physics2D.BoxCast(bottomLine.position, new Vector2(boxCollider2D.bounds.size.x *0.8f, 0.001f), 0f, Vector2.down, 0.05f,groundLayer);
>>>>>>> Feature_3_Movement
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
    
    private void Hurt()
    {
        if (!hurt)
        {
            animator.SetTrigger("hurt");
            hurt = true;
        }

    }

    private void Die()
    {
        if (!dead)
        {
            animator.SetTrigger("die");
            dead = true;
        }

    }

    public void PickUp(int _score)
    {
        Debug.Log("Player picked up a key");
        score.UpdatScore(_score);
    }

    public void ReduceHealth()
    {
        hearts--;
        heartController.UpdateHearts(hearts);
        if (hearts>0)
        {
            Hurt();
        }
        else
        {
            Die();
            gameObject.layer= 6;
            StartCoroutine(LoseLevel());
        }

        
    }

    IEnumerator LoseLevel()
    {
        yield return new WaitForSeconds(3);
        LevelController.instance.GameOverPanel();
    }

}
