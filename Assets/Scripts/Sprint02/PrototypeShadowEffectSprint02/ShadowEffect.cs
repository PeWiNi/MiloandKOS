using UnityEngine;
using System.Collections;

public class ShadowEffect : MonoBehaviour
{
    Move controller;

    void Start()
    {
        controller = GameObject.Find("Milo").GetComponent<Move>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Shadow")
        {
            ShadowSlowEffect();
            StartCoroutine("SpeedTime");
            Destroy(GameObject.Find("HumanShadow01"));
        }
    }
    
    IEnumerator SpeedTime()
    {
        yield return new WaitForSeconds(5);
        RevertSpeed();
    }



    void RevertSpeed()
    {
        controller.ShadowSlowDownSpeed = 1.0f;// Reset to one so we are no longer slowed down.
    }

    void ShadowSlowEffect()
    {
        Debug.Log("Controller: " + controller);
        controller.ShadowSlowDownSpeed = 0.1f;// Slow down with this amount.
    }
}
