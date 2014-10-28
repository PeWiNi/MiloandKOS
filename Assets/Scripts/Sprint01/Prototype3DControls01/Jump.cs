using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    Animator anim;
    int jumpingTrigger;
    int groundedBool;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        jumpingTrigger = Animator.StringToHash("Jumping");
        groundedBool = Animator.StringToHash("Grounded");
    }
	
    void FixedUpdate()
    {
        // Jumping.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(jumpingTrigger);
            anim.SetBool(groundedBool, false);
            rigidbody.velocity = new Vector3(0.0f, 5.0f, 0.0f);
        }
        //If grounded.
        if (rigidbody.position.y <= 0.0f && !anim.GetBool(groundedBool))
        {
            anim.SetBool(groundedBool, true);
        }
    }
}