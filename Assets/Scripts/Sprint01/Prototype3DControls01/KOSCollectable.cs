using UnityEngine;
using System.Collections;

public class KOSCollectable : MonoBehaviour
{
    public GameObject KOSCollectablePrefab;

    // Use this for initialization
    void Start()
    {

    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "KOSMinotaur01")
        {
//            Destroy(gameObject);
            gameObject.SetActive(false);
//            SpawnNewKOSCollectable();
            GameController.INSTANCE.ResetMiloAwakeTimer();
        }
    }

    /// <summary>
    /// Spawns a new KOS collectable.
    /// </summary>
    void SpawnNewKOSCollectable()
    {
        KOSCollectable newCollectable = ((GameObject)Instantiate(KOSCollectablePrefab, new Vector3(Random.Range(-2.0f, 2.0f), transform.position.y, Random.Range(-2.0f, 2.0f)), Quaternion.identity)).GetComponent<KOSCollectable>();
        newCollectable.name = "KOSCollectable";//Only renaming because otherwise if it continues spawning, their names will end with "(Clone)".
    }
}
