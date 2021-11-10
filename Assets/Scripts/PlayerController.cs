using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool run;
    // Start is called before the first frame update
    void Start()
    {
        run = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        run = Input.GetKey(KeyCode.LeftShift);
        Animate(horizontal);
    }

    void Animate(float _horizontal)
    {
        transform.rotation = Quaternion.Euler(0f,90-Mathf.Sign(_horizontal)*90,0f);
        animator.SetFloat("speed",Mathf.Abs(_horizontal));
        animator.SetBool("run",run);
    }
}
