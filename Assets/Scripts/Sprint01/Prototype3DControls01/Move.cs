using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{
    Animator anim;
    int movementFloat;
    public static float verticalMovement;
    float shadowSlowDownSpeed = 1;

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
    /// Gets or sets the shadow slow down speed.
    /// </summary>
    /// <value>The shadow slow down speed.</value>
    public float ShadowSlowDownSpeed
    {
        get
        {
            return shadowSlowDownSpeed;
        }
        set
        {
            shadowSlowDownSpeed = value;
        }
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
            verticalMovement = Input.GetAxis("Vertical") * 2.0f * shadowSlowDownSpeed;
            anim.SetFloat(movementFloat, verticalMovement);
            transform.Translate(0.0f, 0.0f, verticalMovement * Time.deltaTime);
        }
        //Backward direction.
        if (Input.GetKey(KeyCode.S))
        {
            verticalMovement = Input.GetAxis("Vertical") * 2.0f * shadowSlowDownSpeed;
            anim.SetFloat(movementFloat, verticalMovement);
            transform.Translate(0.0f, 0.0f, -1 * (verticalMovement * Time.deltaTime));
        }
        //When NO keyboard events are present.
        if (!Input.anyKey)
        {
            anim.SetFloat(movementFloat, 0.0f);
        }
    }
}
  