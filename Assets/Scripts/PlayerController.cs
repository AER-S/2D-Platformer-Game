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
    
    
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        run = false;
        crouch = false;
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
        
        if (Mathf.Abs(_horizontal) > 0.2f)
        {
            transform.rotation = Quaternion.Euler(0f, 90 - Mathf.Sign(_horizontal) * 90, 0f);
        }

        if (_vertical>0 && onGround && !jump)
        {
            animator.SetTrigger("jump");
            jump = true;
        }
        
        animator.SetFloat("speed",Mathf.Abs(_horizontal));
        animator.SetBool("run",run);
        animator.SetBool("crouch",crouch);
    }
}
