using UnityEngine;
using System.Collections;

public class DesiredDirectionKOS : MonoBehaviour
{
    Animator kosAnimator;
    
    // Use this for initialization
    void Start()
    {
        kosAnimator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        SetDirectionTowardsNearestLotusFlower();
    }

    /// <summary>
    /// Sets the direction towards nearest lotus flower.
    /// </summary>
    void SetDirectionTowardsNearestLotusFlower()
    {
        if (!GameController.INSTANCE.IsPlayingAsMilo && kosAnimator.GetCurrentAnimatorStateInfo(0).IsName("Minotaur_Idle") 
            && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))//Detect only when, No movement or rotation.
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            GameObject nearestLotus = null;
            GameObject[] lotusFlowers = GameObject.FindGameObjectsWithTag("KOSLotus");
            foreach (GameObject currentLotusFlower in lotusFlowers)
            {
                Vector3 diff = (gameObject.transform.position - currentLotusFlower.transform.position);
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    nearestLotus = currentLotusFlower;
                    distance = curDistance;
                }
            }
            GameController.INSTANCE.Kos.transform.LookAt(nearestLotus.transform);
        }
    }
}
