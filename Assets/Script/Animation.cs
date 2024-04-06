using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Animation : MonoBehaviour
{
    public Animator animator;
    //public bool isGround = true;
    public CharacterController character;
    private bool isSlashing = false;
    private WaitForSeconds slashDelay = new WaitForSeconds(1f);

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        //move
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("Slow Run", true);
        }
        else
        {
            animator.SetBool("Slow Run", false);
        }

        //run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        //jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Jump", true);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        //Slash
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSlashing)
            {
                isSlashing = true;
                animator.SetTrigger("Slash");
                StartCoroutine(ResetSlash());
            }
        }

        //Falling
        /*if (!character.isGrounded)
        {
            animator.SetBool("Falling", true);
        }
        else
        {
            animator.SetBool("Falling", false) ;
        }*/
    }
    IEnumerator ResetSlash()
    {
        yield return slashDelay;
        animator.ResetTrigger("Slash");
        isSlashing = false;
    }
}
