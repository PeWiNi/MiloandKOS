using UnityEngine;
using System.Collections;

public class KOSCollectable : MonoBehaviour
{
    public GameObject KOSCollectablePrefab;
    KOSPlaySounds kosSounds;

    // Use this for initialization
    void Start()
    {
        kosSounds = GameController.INSTANCE.Kos.GetComponent<KOSPlaySounds>();
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "KOSMinotaur")
        {
            kosSounds.PlayLotusFlowerPickUp();
            GameController.INSTANCE.AllKOSLotus.Remove(gameObject);
            if (GameController.INSTANCE.CurrentCollectedLotusFlowers == GameController.INSTANCE.MaxNeededLotusFlowers)
            {

            } else
            {
                GameController.INSTANCE.CurrentCollectedLotusFlowers += 1;
            }
            gameObject.SetActive(false);
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
