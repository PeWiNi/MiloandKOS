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
    GameObject KOS;
    Vector3 originalForeArmPosition;
    Quaternion originalForeArmRotation;
    bool teleported = false;
    bool isOriginalPositionAndRotationSet = false;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        forearmCamera = GameObject.Find("ForeArmCamera").camera;
        mainCamera = GameObject.Find("Main Camera").camera;
        objectToFollow = GameObject.Find("bind_R_Wrist01");
        foreArm = GameObject.Find("bind_R_ForeArm01");
        KOS = GameObject.Find("KOSSprint02");//Should be 'KOS' in FINAL version.
        anim = KOS.GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update()
    {
        /* 
         * As long as we don't want to teleport, set these values for the forearmCamera, 
         * The values are the same as the Main Camera.
         */
        if (!Input.GetMouseButton(1))
        {
            forearmCamera.transform.LookAt(objectToFollow.transform.position);
            forearmCamera.transform.rotation = mainCamera.transform.rotation;
            forearmCamera.transform.position = mainCamera.transform.position;
        }
    }

    void FixedUpdate()
    {
        //If we are not currently moving, then proceed.
        if (anim.GetFloat("Movement") == 0.0f)
        {
            if (!isOriginalPositionAndRotationSet)
            {
                SetOriginalPositionAndRotationValues();
            }
            //Right Mouse Button to control Rotation.
            if (Input.GetMouseButton(1))
            {
                RotateForeArm();

                mainCamera.enabled = false;
                forearmCamera.enabled = true;
                //Left Mouse Button to control Direction.
                if (Input.GetMouseButton(0))
                {
                    DirectionForeArm();
                }
                if (Input.GetKeyDown(KeyCode.T))
                {
                    teleported = true;
                }
            } else if (!Input.GetMouseButton(1) && !teleported)
            {
                SetBackToOriginalPosAndRotValues();
                mainCamera.enabled = true;
                forearmCamera.enabled = false;
            } 
            if (!Input.GetMouseButton(1) && teleported)
            {
                TeleportToLocation();
            }
        }
    }

    /// <summary>
    /// The rotation of the fore arm.
    /// </summary>
    void RotateForeArm()
    {
        forearmCamera.transform.position = new Vector3(objectToFollow.transform.position.x, objectToFollow.transform.position.y + 1.0f, objectToFollow.transform.position.z - 2.0f);
        mouseRotationX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        mouseRotationY = Input.GetAxis("Mouse Y") * mouseSensitivityY;
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

    /// <summary>
    /// Teleports to location.
    /// </summary>
    void TeleportToLocation()
    {
        Vector3 teleportationLocation = objectToFollow.transform.position;
        SetOriginalPositionAndRotationValues();
        SetBackToOriginalPosAndRotValues();
        KOS.rigidbody.MovePosition(KOS.rigidbody.position += new Vector3(teleportationLocation.x, 0.0f, teleportationLocation.z));
        teleported = false;
    }

    /// <summary>
    /// Sets the original position and rotation values.
    /// </summary>
    void SetOriginalPositionAndRotationValues()
    {
        originalForeArmPosition = foreArm.transform.position;
        originalForeArmRotation = foreArm.transform.rotation;
        isOriginalPositionAndRotationSet = true;
    }

    /// <summary>
    /// Sets the position and rotation back to the original values.
    /// </summary>
    void SetBackToOriginalPosAndRotValues()
    {
        foreArm.transform.position = originalForeArmPosition;
        foreArm.transform.rotation = originalForeArmRotation;
        isOriginalPositionAndRotationSet = false;
    }
}
