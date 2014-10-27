using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour
{
    Vector3 originalCameraPosition;
    Quaternion originalCameraRotation;
    bool isMovingDown;
    GameObject attachedTo;

    // Use this for initialization
    void Start()
    {
        attachedTo = GameObject.Find("MiloSprint02").gameObject;
        originalCameraPosition = attachedTo.transform.position - transform.position;
        originalCameraRotation = transform.rotation;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S) && !isMovingDown)
        {
            isMovingDown = true;
            attachedTo.transform.Rotate(0.0f, attachedTo.transform.rotation.y + 180.0f, 0.0f);
            transform.position = attachedTo.transform.position - originalCameraPosition;
            transform.rotation = originalCameraRotation;
        } else if (!Input.GetKey(KeyCode.S) && isMovingDown)
        {
            isMovingDown = false;
        }
    }
}
