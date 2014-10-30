using UnityEngine;
using System.Collections;

public class ShadowEffect : MonoBehaviour
{
    Move controller;

    void Start()
    {
        controller = GameObject.Find("MiloSprint02").GetComponent<Move>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Shadow")
        {
            ShadowSlowEffect();
            StartCoroutine("speedTime");
            //Destroy(col.gameObject);
        }
    }
    
    IEnumerator speedTime()
    {
        yield return new WaitForSeconds(10);
        revertSpeed();
    }

    void revertSpeed()
    {
        controller.ShadowSlowDownSpeed = 1.0f;// Reset to one so we are no longer slowed down.
    }

    void ShadowSlowEffect()
    {
        Debug.Log("Controller: " + controller);
        controller.ShadowSlowDownSpeed = 0.1f;// Slow down with this amount.
    }
}
