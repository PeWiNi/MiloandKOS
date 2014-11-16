using UnityEngine;
using System.Collections;

public class CheckShootable : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
