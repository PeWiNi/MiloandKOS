using UnityEngine;
using System.Collections;

public class CompassDirection : MonoBehaviour
{
    GameObject[] exits;
    
    // Use this for initialization
    void Start()
    {
        exits = GameObject.FindGameObjectsWithTag("Exit");
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
        } else if (!GameController.INSTANCE.IsPlayingAsMilo && GameController.INSTANCE.CurrentCollectedLotusFlowers == GameController.INSTANCE.MaxNeededLotusFlowers)
        {
            SetDirectionTowardsMilo();
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
            Vector3 diff = (currentLotusFlower.transform.position - GameController.INSTANCE.Kos.transform.position);
            diff.y = 0f;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nearestLotus = currentLotusFlower;
                distance = curDistance;
            }
        }
        Vector3 referenceForward = GameController.INSTANCE.Kos.transform.forward;
        Vector3 referenceRight = GameController.INSTANCE.Kos.transform.right;
        Vector3 newDirection = nearestLotus.transform.position - GameController.INSTANCE.Kos.transform.position;
        newDirection.y = 0f;
        float newAngle = Vector3.Angle(newDirection, referenceForward);
        float sign = Mathf.Sign(Vector3.Dot(-newDirection, referenceRight));
        float finalAngle = sign * newAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, finalAngle);
    }

    /// <summary>
    /// Sets the direction towards the closest end of maze point.
    /// </summary>
    void SetDirectionTowardsEndOfMazePoint()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject nearestExit = null;
        foreach (GameObject currentExit in exits)
        {
            Vector3 diff;
            if (GameController.INSTANCE.Milo.activeSelf)
            {
                diff = (currentExit.transform.position - GameController.INSTANCE.Milo.transform.position);
            } else
            {
                diff = (currentExit.transform.position - GameController.INSTANCE.Kos.transform.position);
            }
            diff.y = 0f;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                nearestExit = currentExit;
                distance = curDistance;
            }
        }
        Vector3 referenceForward;
        Vector3 referenceRight;
        Vector3 newDirection;
        if (GameController.INSTANCE.Milo.activeSelf)
        {
            referenceForward = GameController.INSTANCE.Milo.transform.forward;
            referenceRight = GameController.INSTANCE.Milo.transform.right;
            newDirection = nearestExit.transform.position - GameController.INSTANCE.Milo.transform.position;
        } else
        {
            referenceForward = GameController.INSTANCE.Milo.transform.forward;
            referenceRight = GameController.INSTANCE.Milo.transform.right;
            newDirection = nearestExit.transform.position - GameController.INSTANCE.Milo.transform.position;
        }
        newDirection.y = 0f;
        float newAngle = Vector3.Angle(newDirection, referenceForward);
        float sign = Mathf.Sign(Vector3.Dot(-newDirection, referenceRight));
        float finalAngle = sign * newAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, finalAngle);
    }

    /// <summary>
    /// Sets the direction towards milo.
    /// </summary>
    void SetDirectionTowardsMilo()
    {
        Vector3 referenceForward = GameController.INSTANCE.Kos.transform.forward;
        Vector3 referenceRight = GameController.INSTANCE.Kos.transform.right;
        Vector3 newDirection = GameController.INSTANCE.Milo.transform.position - GameController.INSTANCE.Kos.transform.position;
        newDirection.y = 0f;
        float newAngle = Vector3.Angle(newDirection, referenceForward);
        float sign = Mathf.Sign(Vector3.Dot(-newDirection, referenceRight));
        float finalAngle = sign * newAngle;
        transform.rotation = Quaternion.Euler(0f, 0f, finalAngle);
        GameController.INSTANCE.Milo.transform.FindChild("MiloGatePortalParticle").gameObject.SetActive(true);//Enable the particles.
    }
}
