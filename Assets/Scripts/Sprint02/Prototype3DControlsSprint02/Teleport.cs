using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public float mouseSensitivityX = 100.0f;
    public float mouseSensitivityY = 100.0f;
    float mouseRotationX;
    float mouseRotationY;
    Camera forearmCamera;
    Camera mainCamera;
    GameObject objectToFollow;
    GameObject foreArm;
    Vector3 originalForeArmPosition;
    Quaternion originalForeArmRotation;

    // Use this for initialization
    void Start()
    {
        forearmCamera = GameObject.Find("ForeArmCamera").camera;
        mainCamera = GameObject.Find("Main Camera").camera;
        objectToFollow = GameObject.Find("bind_R_Wrist01");
        foreArm = GameObject.Find("bind_R_ForeArm01");
        originalForeArmPosition = foreArm.transform.position;
        originalForeArmRotation = foreArm.transform.rotation;
    }
	
    // Update is called once per frame
    void Update()
    {
        forearmCamera.transform.LookAt(objectToFollow.transform.position);
        forearmCamera.transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y + 1.0f, objectToFollow.transform.position.z - 2.0f);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1))//Right Mouse Button to control Rotation.
        {
            if (originalForeArmPosition == null)
            {
                originalForeArmPosition = foreArm.transform.position;
            }
            RotateForeArm();
            mainCamera.enabled = false;
            forearmCamera.enabled = true;
            if (Input.GetMouseButton(0))//Left Mouse Button to control Direction.
            {
                DirectionForeArm();
            }
        } else
        {
            mainCamera.enabled = true;
            forearmCamera.enabled = false;
            foreArm.transform.position = originalForeArmPosition;
//            originalForeArmPosition = Vector3.;
        }
    }

    /// <summary>
    /// The rotation of the fore arm.
    /// </summary>
    void RotateForeArm()
    {
        mouseRotationX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        mouseRotationY = Input.GetAxis("Mouse Y") * mouseSensitivityY;
//        rigidbody.transform.Rotate(Camera.main.transform.up, Mathf.Deg2Rad * mouseRotationX, Space.Self);
//        rigidbody.transform.Rotate(Camera.main.transform.right, -Mathf.Deg2Rad * mouseRotationY, Space.Self);
        transform.Rotate(forearmCamera.transform.up, Mathf.Deg2Rad * mouseRotationX, Space.Self);
        transform.Rotate(forearmCamera.transform.right, -Mathf.Deg2Rad * mouseRotationY, Space.Self);
        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z = 0.0f;
        transform.eulerAngles = eulerAngles;
    }

    /// <summary>
    /// The direction of the fore arm.
    /// </summary>
    void DirectionForeArm()
    {
//        rigidbody.transform.localPosition += transform.forward * 5.0f * Time.deltaTime;//Works on Total body of KOS.
        rigidbody.MovePosition(rigidbody.position += new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 1.0f) * Time.deltaTime);
    }
}
