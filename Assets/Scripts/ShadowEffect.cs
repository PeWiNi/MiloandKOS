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
        if (col.gameObject.name == "HShadow")
        {
            ShadowSlowEffect();
            StartCoroutine("SpeedTime");
            Destroy(GameObject.Find("HumanShadow"));
        }

        if (col.gameObject.name == "CShadow")
        {
            ShadowSlowEffect();
            StartCoroutine("SpeedTime");
            Destroy(GameObject.Find("CatShadow"));
        }
    }

    /// <summary>
    /// Speeds the time.
    /// </summary>
    /// <returns>The time.</returns>
    IEnumerator SpeedTime()
    {
        yield return new WaitForSeconds(5);
        RevertSpeed();
    }

    /// <summary>
    /// Reverts the speed.
    /// </summary>
    void RevertSpeed()
    {
        controller.ShadowSlowDownSpeed = 1.0f;// Reset to one so we are no longer slowed down.
    }

    /// <summary>
    /// Shadows the slow effect.
    /// </summary>
    void ShadowSlowEffect()
    {
        controller.ShadowSlowDownSpeed = 0.1f;// Slow down with this amount.
    }
}
