using UnityEngine;
using System.Collections;

public class DesiredDirectionKOS : MonoBehaviour
{
    Animator kosAnimator;
    float waitForSeconds = 5.0f;
    bool hasBeenStarted;

    // Use this for initialization
    void Start()
    {
        kosAnimator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        SetDirection();
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has been started.
    /// </summary>
    /// <value><c>true</c> if this instance has been started; otherwise, <c>false</c>.</value>
    public bool HasBeenStarted
    {
        set
        {
            hasBeenStarted = value;
        }
        get
        {
            return hasBeenStarted;
        }
    }
    
    /// <summary>
    /// Starts the state of the away.
    /// </summary>
    /// <param name="number">Number.</param>
    public void StartAwayState(int number)
    {
        if (number == 0)
        {
            StartCoroutine("SetDirectionTowardsNearestLotusFlower");
        } else
        {
            StartCoroutine("SetDirectionTowardsEndOfMazePoint");
        }
    }
    
    /// <summary>
    /// Determines whether this instance cancel away state.
    /// </summary>
    /// <returns><c>true</c> if this instance cancel away state; otherwise, <c>false</c>.</returns>
    public void CancelAwayState()
    {
        hasBeenStarted = false;
        StopCoroutine("SetDirectionTowardsNearestLotusFlower");
        StopCoroutine("SetDirectionTowardsEndOfMazePoint");
    }

    /// <summary>
    /// Sets the direction.
    /// </summary>
    void SetDirection()
    {
        if (GameController.INSTANCE.CurrentCollectedLotusFlowers != GameController.INSTANCE.MaxNeededLotusFlowers && !GameController.INSTANCE.IsPlayingAsMilo 
            && !Input.anyKey && !Input.anyKeyDown && !hasBeenStarted)//Detect only when, No movement or rotation.
        {
            hasBeenStarted = true;
            StartAwayState(0);
        } else if (GameController.INSTANCE.CurrentCollectedLotusFlowers == GameController.INSTANCE.MaxNeededLotusFlowers && !GameController.INSTANCE.IsPlayingAsMilo 
            && !Input.anyKey && !Input.anyKeyDown)//Detect only when, No movement or rotation.
        {
            StartAwayState(1);
        } else if (Input.anyKey || Input.anyKeyDown)
        {
            CancelAwayState();
        }
    }

    /// <summary>
    /// Sets the direction towards nearest lotus flower.
    /// </summary>
    /// <returns>The direction towards nearest lotus flower.</returns>
    IEnumerator SetDirectionTowardsNearestLotusFlower()
    {
        yield return new WaitForSeconds(waitForSeconds);
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
        hasBeenStarted = false;
    }

    /// <summary>
    /// Sets the direction towards end of maze point.
    /// </summary>
    /// <returns>The direction towards end of maze point.</returns>
    IEnumerator SetDirectionTowardsEndOfMazePoint()
    {
        yield return new WaitForSeconds(waitForSeconds);
        GameObject exit = GameObject.Find("EndOfMazePoint");
        GameController.INSTANCE.Kos.transform.LookAt(exit.transform);
        hasBeenStarted = false;
    }
}
