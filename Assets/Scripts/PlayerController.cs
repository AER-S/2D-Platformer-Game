using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool run;
    private bool jump;
    private bool crouch;
    private bool onGround;
    private bool hurt;
    private bool dead;
    
    
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        run = false;
        crouch = false;
        hurt = false;
        dead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        run = Input.GetKey(KeyCode.LeftShift);
        crouch = Input.GetKey(KeyCode.LeftControl);
        Animate(horizontal, vertical);
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
