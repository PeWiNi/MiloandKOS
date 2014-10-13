using UnityEngine;
using System.Collections;

public class MiloKeyboardMovement : MonoBehaviour
{
    private Animator animator;
    private bool facingRight = false;
    private bool running = false;
    private float runningValue = 0.09f;
    private float walkingValue = 0.04f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MiloKeyboardMovement"/> facing right.
    /// </summary>
    /// <value><c>true</c> if facing right; otherwise, <c>false</c>.</value>
    public bool FacingRight
    {
        get
        {
            return facingRight;
        }
        set
        {
            facingRight = value;
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        //Toggle run.
        if (Input.GetKeyDown(KeyCode.R))
        {
            running = !running;
        }
        //Up
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 upPosition = transform.position;
            if (running)
            {
                upPosition.z += runningValue;
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingSideways", false);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningSideways", false);
                animator.SetBool("isRunningUp", true);
                animator.SetBool("isIdle", false);
            } else
            {
                upPosition.z += walkingValue;
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingSideways", false);
                animator.SetBool("isWalkingUp", true);
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningSideways", false);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isIdle", false);
            }
            transform.position = upPosition;
        }
        //Down
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 upPosition = transform.position;
            if (running)
            {
                upPosition.z -= runningValue;
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingSideways", false);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isRunningDown", true);
                animator.SetBool("isRunningSideways", false);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isIdle", false);
            } else
            {
                upPosition.z -= walkingValue;
                animator.SetBool("isWalkingDown", true);
                animator.SetBool("isWalkingSideways", false);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningSideways", false);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isIdle", false);
            }
            transform.position = upPosition;
        }
        //left
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (!FacingRight)
            {
                Flip();
            }
            Vector3 upPosition = transform.position;
            if (running)
            {
                upPosition.x -= runningValue;
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingSideways", false);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningSideways", true);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isIdle", false);
            } else
            {
                upPosition.x -= walkingValue;
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingSideways", true);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningSideways", false);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isIdle", false);
            }
            transform.position = upPosition;
        }
        //Right
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (FacingRight)
            {
                Flip();
            }
            Vector3 upPosition = transform.position;
            if (running)
            {
                upPosition.x += runningValue;
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingSideways", false);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningSideways", true);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isIdle", false);
            } else
            {
                upPosition.x += walkingValue;
                animator.SetBool("isWalkingDown", false);
                animator.SetBool("isWalkingSideways", true);
                animator.SetBool("isWalkingUp", false);
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningSideways", false);
                animator.SetBool("isRunningUp", false);
                animator.SetBool("isIdle", false);
            }
            transform.position = upPosition;
        } else
        {
            animator.SetBool("isWalkingDown", false);
            animator.SetBool("isWalkingSideways", false);
            animator.SetBool("isWalkingUp", false);
            animator.SetBool("isRunningDown", false);
            animator.SetBool("isRunningSideways", false);
            animator.SetBool("isRunningUp", false);
            animator.SetBool("isIdle", true);
        }
    }

    /// <summary>
    /// Flip the direction of this unit (Right or Left).
    /// </summary>
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
