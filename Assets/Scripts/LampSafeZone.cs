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
        PreventKOSFromEnteringSafeZones();
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
        if (col.gameObject.name == "Milo" && Application.loadedLevelName.Equals("MazeLevel"))
        {
            if (!Input.anyKey && !Input.anyKeyDown && !GameController.INSTANCE.Milo.GetComponent<DesiredDirectionMilo>().HasBeenStarted)
            {
                GameController.INSTANCE.Milo.GetComponent<DesiredDirectionMilo>().HasBeenStarted = true;
                GameController.INSTANCE.Milo.GetComponent<DesiredDirectionMilo>().StartAwayState();
            } else if (Input.anyKey || Input.anyKeyDown)
            {
                GameController.INSTANCE.Milo.GetComponent<DesiredDirectionMilo>().CancelAwayState();
            }
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
