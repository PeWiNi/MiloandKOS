using UnityEngine;
using System.Collections;

public class DesiredDirectionMilo : MonoBehaviour
{
    Animator miloAnimator;
    float waitForSeconds = 5.0f;
    bool hasBeenStarted;
    
    // Use this for initialization
    void Start()
    {
        miloAnimator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Gets a value indicating whether this instance has been started.
    /// </summary>
    /// <value><c>true</c> if this instance has been started; otherwise, <c>false</c>.</value>
    public bool HasBeenStarted
    {
        get
        {
            return hasBeenStarted;
        }
    }

    /// <summary>
    /// Starts the state of the away.
    /// </summary>
    public void StartAwayState()
    {
        StartCoroutine("SetDirectionTowardsEndOfMazePoint");
    }

    /// <summary>
    /// Determines whether this instance cancel away state.
    /// </summary>
    /// <returns><c>true</c> if this instance cancel away state; otherwise, <c>false</c>.</returns>
    public void CancelAwayState()
    {
        StopCoroutine("SetDirectionTowardsEndOfMazePoint");
    }

    /// <summary>
    /// Sets the direction towards end of maze point.
    /// </summary>
    /// <returns>The direction towards end of maze point.</returns>
    IEnumerator SetDirectionTowardsEndOfMazePoint()
    {
        Debug.Log("hasBeenStarted Before Yield: " + hasBeenStarted);
        yield return new WaitForSeconds(waitForSeconds);
        GameObject exit = GameObject.Find("EndOfMazePoint");
        GameController.INSTANCE.Milo.transform.LookAt(exit.transform);
        hasBeenStarted = false;
        Debug.Log("hasBeenStarted After Yield: " + hasBeenStarted);
    }
}
