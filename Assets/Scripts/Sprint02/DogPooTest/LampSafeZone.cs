using UnityEngine;
using System.Collections;

public class LampSafeZone : MonoBehaviour
{
    Flashlight miloFlashLight;

    // Use this for initialization
    void Start()
    {
        miloFlashLight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
    }
	
    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Milo")
        {
            miloFlashLight.PauseCapacity = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Milo")
        {
            miloFlashLight.PauseCapacity = false;
        }
    }
}
