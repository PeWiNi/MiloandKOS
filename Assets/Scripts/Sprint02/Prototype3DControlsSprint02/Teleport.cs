using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    float mouseRotationX;
    float mouseRotationY;

    // Use this for initialization
    void Start()
    {

    }
	
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))//Right Mouse Button to control Rotation.
        {
            RotateForeArm();
            if (Input.GetMouseButton(0))//Left Mouse Button to control Direction.
            {
                DirectionForeArm();
            }
        }
    }

    /// <summary>
    /// The rotation of the fore arm.
    /// </summary>
    void RotateForeArm()
    {
        mouseRotationX = Input.GetAxis("Mouse X");
        mouseRotationY = Input.GetAxis("Mouse Y");
        rigidbody.transform.RotateAround(Camera.main.transform.up, Mathf.Deg2Rad * mouseRotationX);
        rigidbody.transform.RotateAround(Camera.main.transform.right, -Mathf.Deg2Rad * mouseRotationY);
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z = 0.0f;
        transform.eulerAngles = eulerAngles;
    }

    /// <summary>
    /// The direction of the fore arm.
    /// </summary>
    void DirectionForeArm()
    {
        rigidbody.transform.localPosition += transform.forward * Time.deltaTime;//Works on Total body of KOS.
    }
}
