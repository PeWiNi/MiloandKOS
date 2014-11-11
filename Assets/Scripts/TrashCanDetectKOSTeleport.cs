using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TrashCanDetectKOSTeleport : MonoBehaviour
{
    bool hasEntered = false;
    GameObject[] trashCans;
    GameObject closest;
    // Use this for initialization
    void Start()
    {
        trashCans = GameObject.FindGameObjectsWithTag("TrashCan");
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && hasEntered)
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            Debug.Log("Wants to Teleport");
            // Dictionary<GameObject, float> distances = new Dictionary<GameObject, float>();
            foreach (GameObject trashCan in trashCans)
            {
                if (gameObject != trashCan)
                {
                    Vector3 diff = (trashCan.transform.position - position);
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = trashCan;
                        distance = curDistance;

                        //   hasEntered = true;
                    }
                }
            }
            Debug.Log(closest.transform.position);
        }
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
}
