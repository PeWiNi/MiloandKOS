using UnityEngine;
using System.Collections;

public class DetectingFloor : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "RotatingAxe01" || col.gameObject.name == "CannonBall01")
        {
            Destroy(col.gameObject);
        }

    }
}
