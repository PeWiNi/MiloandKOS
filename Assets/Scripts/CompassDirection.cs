using UnityEngine;
using System.Collections;

public class CompassDirection : MonoBehaviour
{
    float normRot;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        if (GameController.INSTANCE.IsPlayingAsMilo)
        {
            SetDirectionTowardsEndOfMazePoint();
        } else if (!GameController.INSTANCE.IsPlayingAsMilo && GameController.INSTANCE.CurrentCollectedLotusFlowers != GameController.INSTANCE.MaxNeededLotusFlowers
            && GameController.INSTANCE.Kos.activeSelf)
        {
            SetDirectionTowardsNearestLotusFlower();
        } else if (GameController.INSTANCE.IsPlayingAsMilo && GameController.INSTANCE.CurrentCollectedLotusFlowers == GameController.INSTANCE.MaxNeededLotusFlowers)
        {
            SetDirectionTowardsEndOfMazePoint();
        }
    }

    /// <summary>
    /// Sets the direction towards nearest lotus flower.
    /// </summary>
    void SetDirectionTowardsNearestLotusFlower()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject nearestLotus = null;
        GameObject[] lotusFlowers = GameController.INSTANCE.AllKOSLotus.ToArray();
        foreach (GameObject currentLotusFlower in lotusFlowers)
        {
            Vector3 diff = (gameObject.transform.position - currentLotusFlower.transform.position);
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nearestLotus = currentLotusFlower;
                distance = curDistance;
            }
        }
        Vector3 targetDir = nearestLotus.transform.position - Camera.main.transform.position;
        targetDir.y = 0f;
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        float angle = Vector3.Angle(forward, targetDir);
        //        float angle2 = Mathf.Atan2(targetDir.z, targetDir.x);
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        
        //If the angle exceeds 90deg inverse the rotation to point correctly
//        if (angle > 180f)
//        {
//            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
//        } else
//        {
//            transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
//        }
    }

    /// <summary>
    /// Sets the direction towards end of maze point.
    /// </summary>
    void SetDirectionTowardsEndOfMazePoint()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject nearestExit = null;
        GameObject[] exits = GameObject.FindGameObjectsWithTag("Exit");
        foreach (GameObject currentExit in exits)
        {
            Vector3 diff = (gameObject.transform.position - currentExit.transform.position);
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nearestExit = currentExit;
                distance = curDistance;
            }
        }
        Vector3 targetDir = nearestExit.transform.position - Camera.main.transform.position;
//        targetDir.y = 0f;
//        targetDir.x = 0f;
//        targetDir.z = 0f;
        Vector3 camForward = Camera.main.transform.forward;
        float angle = Vector3.Angle(targetDir, camForward);
//        float angle2 = Mathf.Atan2(targetDir.z, targetDir.x) * Mathf.Rad2Deg;
        
        //If the angle exceeds 90deg inverse the rotation to point correctly
//        if (angle >= 0 || angle < 90f || angle > 180f)
        if (angle >= 0 && angle <= 180f)
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        } else
        {
            transform.localRotation = Quaternion.Euler(0f, 0f, -angle);
        }
    }
}
