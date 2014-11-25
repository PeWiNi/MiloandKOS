using UnityEngine;
using System.Collections;

public class DesiredDirectionKOS : MonoBehaviour
{
    Animator kosAnimator;
    float waitForSeconds = 5.0f;
    static int numberStarted = 0;
    
    // Use this for initialization
    void Start()
    {
        kosAnimator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        SetDirectionTowardsNearestLotusFlower();
    }

    /// <summary>
    /// Sets the direction towards nearest lotus flower.
    /// </summary>
    void SetDirectionTowardsNearestLotusFlower()
    {
        if (GameController.INSTANCE.CurrentCollectedLotusFlowers != GameController.INSTANCE.MaxNeededLotusFlowers && !GameController.INSTANCE.IsPlayingAsMilo 
            && !Input.anyKey)//Detect only when, No movement or rotation.
        {
            StartCoroutine(LookAtNearestLotusFlower());
        } else if (GameController.INSTANCE.CurrentCollectedLotusFlowers == GameController.INSTANCE.MaxNeededLotusFlowers && !GameController.INSTANCE.IsPlayingAsMilo 
            && !Input.anyKey)//Detect only when, No movement or rotation.
        {
            StartCoroutine(LookAtNearestExitPoint());
        } else
        {
            StopCoroutine("LookAtNearestLotusFlower");
            StopCoroutine("LookAtNearestExitPoint");
        }
    }

    IEnumerator LookAtNearestLotusFlower()
    {
        yield return new WaitForSeconds(waitForSeconds);
        numberStarted++;
        Debug.Log("Coroutine started: " + numberStarted);
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
        GameController.INSTANCE.Kos.transform.LookAt(nearestLotus.transform);
    }

    IEnumerator LookAtNearestExitPoint()
    {
        yield return new WaitForSeconds(waitForSeconds);
        GameObject exit = GameObject.Find("EndOfMazePoint");
        GameController.INSTANCE.Kos.transform.LookAt(exit.transform);
    }
}
