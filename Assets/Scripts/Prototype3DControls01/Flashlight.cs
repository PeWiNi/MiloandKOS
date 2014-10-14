using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour
{
    bool on = false;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            on = !on;
        }
        if (on)
        {
            light.enabled = true;
        } else
        {
            light.enabled = false;
        }
    }
}
