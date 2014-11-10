using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraPan : MonoBehaviour
{
    bool isMovingDown;
    bool isMoving;
    GameObject attachedTo;

    [SerializeField]
    float
        distanceAwayFromCharacter;
    [SerializeField]
    float
        distanceUpFromGround;
    [SerializeField]
    float
        smooth;// Delay From CurrentPosition To TargetPosition of the Camera.
    [SerializeField]
    float
        cameraSmoothingDampTime = 0.1f;

    Vector3 cameraTargetPosition;
    Vector3 lookDirection;
    Vector3 velocityCameraSmoothing = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        attachedTo = GameObject.FindWithTag("Player").gameObject;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!attachedTo.activeSelf)
        {
            attachedTo = GameObject.FindWithTag("Player").gameObject;
        }
		//if you change S to any other key not used atm, that key will become a flip direction button. 
        if (Input.GetKey(KeyCode.H) && !isMovingDown && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            isMovingDown = true;
            isMoving = true;
            attachedTo.transform.RotateAround(Vector3.up, Mathf.PI);// Rotate the character in the opposite direction on the Y-axis.
        } else if (!Input.GetKey(KeyCode.H) && isMovingDown)
        {
            isMovingDown = false;
        } else if (!Input.anyKey)
        {
            isMoving = false;
        }
    }


    // Good for Camera positioning, because everything else which takes place in Update, will be executed before.
    void LateUpdate()
    {
        Vector3 characterOffset = attachedTo.transform.position + new Vector3(0.0f, distanceUpFromGround, 0.0f);
        if (isMoving && isMovingDown)
        {
            lookDirection = characterOffset - transform.position;// Calculate direction from Camera to player.
            lookDirection.x = 0.0f;// Omit the X position.
            lookDirection.Normalize();// Generate valid direction with unit magnitude.
        } else if (!isMoving)
        {
            lookDirection = attachedTo.transform.forward;// Set the position and rotation of the camera to look at the back of the character again.
        }
        cameraTargetPosition = characterOffset + attachedTo.transform.up * distanceUpFromGround - lookDirection * distanceAwayFromCharacter;
        DetectCollisionWithSurroundings(characterOffset, ref cameraTargetPosition);
        SmoothCameraPosition(transform.position, cameraTargetPosition);
        transform.LookAt(attachedTo.transform);// Insures the camera is looking at the correct object.
    }

    /// <summary>
    /// Smooths the camera position.
    /// </summary>
    /// <param name="currentPosition">Current position.</param>
    /// <param name="targetPosition">Target position.</param>
    void SmoothCameraPosition(Vector3 currentPosition, Vector3 targetPosition)
    {
        transform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref velocityCameraSmoothing, cameraSmoothingDampTime);// Making a smooth transition between Current Position and the Target Position.
    }

    /// <summary>
    /// Detects the collision with surroundings.
    /// </summary>
    /// <param name="fromCameraPosition">From camera position.</param>
    /// <param name="target">Target.</param>
    void DetectCollisionWithSurroundings(Vector3 fromCameraPosition, ref Vector3 target)
    {
        RaycastHit rayCastHitInfo = new RaycastHit();
        if (Physics.Linecast(fromCameraPosition, target, out rayCastHitInfo))
        {
            target = new Vector3(rayCastHitInfo.point.x, target.y, rayCastHitInfo.point.z);
        }
    }
}