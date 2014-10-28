using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Animator anim;
    int movementFloat;
    public static float verticalMovement;

    void Awake()
    {
        anim = GetComponent<Animator>();
        movementFloat = Animator.StringToHash("Movement");
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
        }
        //When NO keyboard events are present.
        if (!Input.anyKey)
        {
            anim.SetFloat(movementFloat, 0.0f);
        }
    }
}
  