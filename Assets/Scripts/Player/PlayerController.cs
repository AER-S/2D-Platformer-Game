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
    [SerializeField] private float repel = 3f;
    [SerializeField] private HeartController heartController;
    [SerializeField] private PlayerSoundsController soundsController;
    

    private bool run;
    private bool jump;
    private bool crouch;
    private bool onGround;
    private bool hurt;
    private bool backward;
    private bool dead;
    private int hearts;

    private float horizontal;
    private float vertical;
    

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

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        run = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);
        
        Animate(horizontal, vertical);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // horizontal = Input.GetAxisRaw("Horizontal");
        // vertical = Input.GetAxisRaw("Vertical");
        // run = Input.GetKey(KeyCode.LeftShift);
        // crouch = Input.GetKey(KeyCode.LeftControl);
        //
        // Animate(horizontal, vertical);
        Move(horizontal, vertical);
    }

    void Animate(float _horizontal, float _vertical)
    {
        animator.SetBool("jump",jump);
        animator.SetBool("run",run);
        if (!dead && !hurt)
        {
            GetDirection(_horizontal);
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
        
    }

    void GetDirection(float _horizontal)
    {
        if (Mathf.Abs(_horizontal) <= 0.2f)
        {
            return;
        } 
        transform.rotation = Quaternion.Euler(0f, 90 - Mathf.Sign(_horizontal) * 90, 0f);
        
        
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
                    if (speed>0.2f)
                    {
                        soundsController.PlayPlayerSound(PlayerSound.Walk);

                        if (xMovingSpeed<desiredSpeed)
                        {
                            Vector2 newMove = new Vector2(_horizontal * desiredSpeed, rigidBody.velocity.y);
                            rigidBody.velocity = newMove;
                        }
                        
                    }
                    else 
                    {
                        StopPlayer();
                    }

                    if (_vertical>0 && !jump)
                    {
                        jump = true;
                        Vector2 newJump = new Vector2(rigidBody.velocity.x, jumpPower);
                        rigidBody.velocity = newJump;
                    }
                }
                else
                {
                    StopPlayer();
                }
            }
            else
            {
                soundsController.StopSoundOnJump();
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
        rigidBody.velocity *= Vector2.up;
        soundsController.StopPlayingMovementSound();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckForGround();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!onGround)
        {
            CheckForGround();
        }
    }
    

    bool CheckForGround()
    {
        RaycastHit2D ground = Physics2D.BoxCast(bottomLine.position, new Vector2(boxCollider2D.bounds.size.x *0.999f, 0.001f), 0f, Vector2.down, 0.05f,groundLayer);
        if (ground)
        {
            soundsController.PlayPlayerSound(PlayerSound.Land);
            jump = false;
            onGround = true;
        }

        return ground;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (!CheckForGround() &&(groundLayer | (1<<other.gameObject.layer)) != 0)
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
            soundsController.PlayPlayerSound(PlayerSound.Hurt);
        }

    }

    private void Die()
    {
        if (!dead)
        {
            animator.SetTrigger("die");
            dead = true;
            soundsController.PlayPlayerSound(PlayerSound.Die);
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
        LevelController.Instance.GameOverPanel();
    }

}
