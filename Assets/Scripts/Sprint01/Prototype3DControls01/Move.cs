using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float backwardSpeed = -2.5f;
    public float turnSpeed = 500f;

    private Animator anim; 
    public int speedFloat;

    void Awake()
    {
        anim = GetComponent<Animator>();
        speedFloat = Animator.StringToHash("Speed");
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f));
        }
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetFloat(speedFloat, 2.0f);
            transform.Translate(0.0f, 0.0f, Input.GetAxis("Vertical") * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetFloat(speedFloat, 2.0f);
            transform.Translate(0.0f, 0.0f, Input.GetAxis("Vertical") * Time.deltaTime);
//            Camera cam = GetComponentInChildren<Camera>();
//            cam.transform.localRotation = Quaternion.LookRotation(new Vector3(transform.position.x, 0.0f, transform.position.z) - cam.transform.position);
//            transform.rotation = Quaternion.LookRotation(new Vector3(cam.transform.position.x, 0.0f, cam.transform.position.z) - transform.position);
        }
        if (!Input.anyKey)//No keyboard events are present.
        {
            anim.SetFloat(speedFloat, 0.0f);
        }
//        if (Input.GetKey(KeyCode.W))
//        {
//            ForwardMovement();
//        } else if (Input.GetKey(KeyCode.S))
//        {
//            BackwardMovement();
//        } else if (Input.GetKeyDown(KeyCode.D))
//        {
//            RotateRight();
//        } else if (Input.GetKeyDown(KeyCode.A))
//        {
//            RotateLeft();
//        } 
//        else
//        {
//            anim.SetFloat(speedFloat, 0f);	
//        }
    }


    void ForwardMovement()
    {
        anim.SetFloat(speedFloat, 2f);
        anim.transform.Translate(0f, 0f, forwardSpeed * Time.deltaTime);		
    }

    void BackwardMovement()
    {
        anim.SetFloat(speedFloat, 2f);
        anim.transform.Translate(0f, 0f, backwardSpeed * Time.deltaTime);	
    }

    void RotateRight()
    {
        anim.transform.Rotate(0, 90, 0);
    }

    void RotateLeft()
    {
        anim.transform.Rotate(0, -90, 0);
    }
}
  