using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TrashCanDetectKOSTeleport : MonoBehaviour
{
    bool hasEntered = false;
    GameObject[] trashCans;
    GameObject closest;
    GameObject kos;
    
    // Use this for initialization
    void Start()
    {
        trashCans = GameObject.FindGameObjectsWithTag("TrashCan");
        kos = GameController.INSTANCE.Kos;
    }
	
    // Update is called once per frame
    void Update()
    {
        CheckForTeleportState();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "KOSMinotaur")
        {
            hasEntered = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "KOSMinotaur")
        {
            hasEntered = false;
        }
    }

    /// <summary>
    /// Checks the state of the teleport mechanic.
    /// If the Player presses the 'T' button while within the TrashCan Box Collider
    /// of the TrashCan then proceed and find the closest TrashCan within range.
    /// </summary>
    void CheckForTeleportState()
    {
        if (Input.GetKeyDown(KeyCode.T) && hasEntered)
        {
            int rand = Random.Range(0, trashCans.Length);
            kos.rigidbody.MovePosition(trashCans [rand].transform.position);//This is the actual teleportation.
        }
    }
}
