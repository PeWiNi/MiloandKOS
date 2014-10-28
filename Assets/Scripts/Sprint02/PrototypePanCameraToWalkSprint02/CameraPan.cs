using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraPan : MonoBehaviour
{
    bool isMovingDown;
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
        if (Input.GetKey(KeyCode.S) && !isMovingDown && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            isMovingDown = true;
        } else if (!Input.GetKey(KeyCode.S) && isMovingDown)
        {
            isMovingDown = false;
        }
    }

    /* 
     * Good for Camera positioning, because everything else which takes place in Update, will be executed before.
     * */
    void LateUpdate()
    {
        Vector3 characterOffset = attachedTo.transform.position + new Vector3(0.0f, distanceUpFromGround, 0.0f);
        lookDirection = characterOffset - transform.position;// Calculate direction from Camera to player.
        lookDirection.y = 0.0f;// Omit the Y position.
        lookDirection.Normalize();// Generate valid direction with unit magnitude.

//        cameraTargetPosition = attachedTo.transform.position + attachedTo.transform.up * distanceUpFromGround - attachedTo.transform.forward * distanceAwayFromCharacter;// Following the Character from the back.
//        transform.position = Vector3.Lerp(transform.position, cameraTargetPosition, Time.deltaTime * smooth);// Smooths the camera Current Position, to camera Target Position.

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