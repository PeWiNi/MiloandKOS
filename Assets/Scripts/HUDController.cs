using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public int compareCurrentAmountOfCollectedFlowers = 0;
    GameObject lotusHUDS;
    GameObject potionJar;

    int miloAwakeStatus;
    [SerializeField]
    Sprite
        collectedLotus;
    [SerializeField]
    Sprite
        miloAwake00;
    [SerializeField]
    Sprite
        miloAwake20;
    [SerializeField]
    Sprite
        miloAwake40;
    [SerializeField]
    Sprite
        miloAwake60;
    [SerializeField]
    Sprite
        miloAwake80;
    [SerializeField]
    Sprite
        miloAwake100;
    [SerializeField]
    Image
        img01;
    [SerializeField]
    Image
        img02;
    [SerializeField]
    Image
        img03;
    [SerializeField]
    Image
        img04;
    [SerializeField]
    Image
        img05;
    [SerializeField]
    Image
        img06;
    [SerializeField]
    Image
        img07;
    [SerializeField]
    Image
        img08;
    [SerializeField]
    Image
        img09;
    [SerializeField]
    Image
        img10;
    [SerializeField]
    Image
        miloAwakeHUD;
    // Use this for initialization
    void Start()
    {
        compareCurrentAmountOfCollectedFlowers = GameController.INSTANCE.CurrentCollectedLotusFlowers;
        lotusHUDS = GameObject.FindGameObjectWithTag("LotusHUDS");
        potionJar = GameObject.FindGameObjectWithTag("PotionJar");
        potionJar.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateMiloAwakeStatusHUD();
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
                case 3:
                    img03.sprite = collectedLotus;
                    break;
                case 4:
                    img04.sprite = collectedLotus;
                    break;
                case 5:
                    img05.sprite = collectedLotus;
                    break;
                case 6:
                    img06.sprite = collectedLotus;
                    break;
                case 7:
                    img07.sprite = collectedLotus;
                    break;
                case 8:
                    img08.sprite = collectedLotus;
                    break;
                case 9:
                    img09.sprite = collectedLotus;
                    break;
                case 10:
                    PotionCreatedHUD();
                    break;
                default:
                    break;
            }
        }
    }
    
    void UpdateMiloAwakeStatusHUD()
    {
        miloAwakeStatus = (int)GameController.INSTANCE.MiloAwakeTimer;
        switch (miloAwakeStatus / 72)
        {
            case 0:
                miloAwakeHUD.sprite = miloAwake00;
                break;
            case 1:
                miloAwakeHUD.sprite = miloAwake20;
                break;
            case 2:
                miloAwakeHUD.sprite = miloAwake40;
                break;
            case 3:
                miloAwakeHUD.sprite = miloAwake60;
                break;
            case 4:
                miloAwakeHUD.sprite = miloAwake80;
                break;
            case 5:
                miloAwakeHUD.sprite = miloAwake100;
                break;
            default:
                miloAwakeHUD.sprite = miloAwake00;
                break;
        }
        UpdateHUDCollectedLotusFlowers();
    }

    //Supposed to handle the potion HUD when it is created. Hiding the Lotus flower HUD and switch the potion off if your no longer playing as KOS
    void PotionCreatedHUD()
    {
              
        if (GameController.INSTANCE.IsPlayingAsMilo)
        {
            potionJar.SetActive(false);
        } else
        {
            potionJar.SetActive(true);
            lotusHUDS.SetActive(false);
        }


            

    }


}
