using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    int compareCurrentAmountOfCollectedFlowers;
    [SerializeField]
    Sprite
        collectedLotus;
    [SerializeField]
    Image
        img01;
    [SerializeField]
    Image
        img02;

    // Use this for initialization
    void Start()
    {
        compareCurrentAmountOfCollectedFlowers = GameController.INSTANCE.CurrentCollectedLotusFlowers;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUDCollectedLotusFlowers();
    }

    /// <summary>
    /// Updates the HUD to display the current collected lotus flowers.
    /// </summary>
    void UpdateHUDCollectedLotusFlowers()
    {
        if (GameController.INSTANCE.CurrentCollectedLotusFlowers != compareCurrentAmountOfCollectedFlowers)
        {
            compareCurrentAmountOfCollectedFlowers = GameController.INSTANCE.CurrentCollectedLotusFlowers;
            switch (compareCurrentAmountOfCollectedFlowers)
            {
                case 1:
                    img01.sprite = collectedLotus;
                    break;
                case 2:
                    img02.sprite = collectedLotus;
                    break;
                default:
                    break;
            }
        }
    }
}
