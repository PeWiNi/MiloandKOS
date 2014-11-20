using UnityEngine;
using System.Collections;

public class LampSafeZone : MonoBehaviour
{
    Flashlight miloFlashLight;
    CapsuleCollider capsuleCollider;

    // Use this for initialization
    void Start()
    {
        miloFlashLight = GameObject.Find("Flashlight").GetComponent<Flashlight>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
	
    // Update is called once per frame
    void Update()
    {
        PreventKOSFromEnteringSafeZones();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Milo")
        {
            miloFlashLight.PauseCapacity = true;
        } else if (col.gameObject.tag == "EnemyShadow")
        {
            Destroy(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Milo")
        {
            miloFlashLight.PauseCapacity = false;
        } 
    }

    void OnTriggerStay(Collider col)
    {        
        if (col.gameObject.name == "Milo")
        {
            GameController.INSTANCE.Milo.GetComponent<DesiredDirectionMilo>().SetDirectionTowardsEndOfMazePoint();
        }
    }

    /// <summary>
    /// Prevents KOS from entering safe zones.
    /// </summary>
    void PreventKOSFromEnteringSafeZones()
    {
        if (GameController.INSTANCE != null && GameController.INSTANCE.Kos.activeSelf && capsuleCollider.isTrigger)
        {
            capsuleCollider.isTrigger = false;
        } else
            if (GameController.INSTANCE != null && !GameController.INSTANCE.Kos.activeSelf && !capsuleCollider.isTrigger)
        {
            capsuleCollider.isTrigger = true;
        }
    }
}
