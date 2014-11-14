using UnityEngine;
using System.Collections;

public class MazePointLoadNextScene : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Milo")
        {
            Application.LoadLevel(5);
        } else if (col.gameObject.name == "KOSMinotaur")
        {
            if (GameController.INSTANCE.CurrentCollectedLotusFlowers == GameController.INSTANCE.MaxNeededLotusFlowers)
            {
                Application.LoadLevel(6);
            }
        }
    }
}
