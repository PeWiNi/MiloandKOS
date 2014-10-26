using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Animator anim; 
    int movementFloat;
    int jumpingTrigger;
    public static float verticalMovement;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movementFloat = Animator.StringToHash("Movement");
        jumpingTrigger = Animator.StringToHash("Jumping");
    }

    void FixedUpdate()
    {
        MovementControl();
    }

    /// <summary>
    /// Control the movements.; Walk, Run inputs and the appropriate animation states
    /// </summary>
   void MovementControl()
    {
        //Left & Right Rotation.
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0.0f, Input.GetAxis("Horizontal"), 0.0f));
        }
        //Forward direction.
        if (Input.GetKey(KeyCode.W))
        {
            //When Left Shift key is NOT hold down: Walking.
            if (!Input.GetKey(KeyCode.LeftShift) && Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f)
            {
                verticalMovement = Input.GetAxis("Vertical");
                anim.SetFloat(movementFloat, verticalMovement);
            }
            //When Left Shift key is hold down: Running.
            if (Input.GetKey(KeyCode.LeftShift))
            {
                verticalMovement = Input.GetAxis("Vertical") * 2.0f;
                anim.SetFloat(movementFloat, verticalMovement);
            }
            transform.Translate(0.0f, 0.0f, verticalMovement * Time.deltaTime);
        }
        //Backward direction.
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetFloat(movementFloat, 1.0f);
            transform.Translate(0.0f, 0.0f, Input.GetAxis("Vertical") * Time.deltaTime);
            //            Camera cam = GetComponentInChildren<Camera>();
            //            cam.transform.localRotation = Quaternion.LookRotation(new Vector3(transform.position.x, 0.0f, transform.position.z) - cam.transform.position);
            //            transform.rotation = Quaternion.LookRotation(new Vector3(cam.transform.position.x, 0.0f, cam.transform.position.z) - transform.position);
        }
        // Jumping.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetFloat(movementFloat, 0.0f);
            anim.SetTrigger(jumpingTrigger);
            rigidbody.velocity = new Vector3(0.0f, 5.0f, 0.0f);
        }
        //When NO keyboard events are present.
        if (!Input.anyKey)
        {
            anim.SetFloat(movementFloat, 0.0f);
        }
    }
}
  