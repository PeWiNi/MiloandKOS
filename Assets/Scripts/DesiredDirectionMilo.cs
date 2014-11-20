using UnityEngine;
using System.Collections;

public class DesiredDirectionMilo : MonoBehaviour
{
    Animator miloAnimator;
    
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
    /// Sets the direction towards end of maze point.
    /// </summary>
    public void SetDirectionTowardsEndOfMazePoint()
    {
        if (GameController.INSTANCE.IsPlayingAsMilo && miloAnimator.GetCurrentAnimatorStateInfo(0).IsName("MiloAnim-Idle-01") 
            && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))//Detect only when, No movement or rotation.
        {
            Debug.Log("PØLSE");
            GameObject exit = GameObject.Find("EndOfMazePoint");
            GameController.INSTANCE.Milo.transform.LookAt(exit.transform);
        }
    }
}
