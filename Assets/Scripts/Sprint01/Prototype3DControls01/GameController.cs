using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController INSTANCE;
    GameObject milo;
    GameObject kos;
    Flashlight miloFlashlightComponent;
    bool isPlayingAsMilo = true;
    float miloAwakeTimer = 30.0f;
    int switchCounter = 0;

    GameObject[] allKOSLotus;
    GameObject[] allBatteries;
    bool switchHasBeenExecuted = false;

    // This happens before Start
    void Awake()
    {
        INSTANCE = this;
    }

    // Use this for initialization
    void Start()
    {
        miloFlashlightComponent = GameObject.Find("Flashlight").GetComponent<Flashlight>();
        milo = GameObject.Find("Milo");//Find Milo.
        kos = GameObject.Find("KOSMinotaur");//Find !Milo.
        kos.gameObject.SetActive(false);
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!kos.activeSelf && !switchHasBeenExecuted)
        {
            SwitchActiveValuesForCollectables();// Start by hiding all Lotus flowers within in the maze.
            switchHasBeenExecuted = true;
        }
        if (miloFlashlightComponent.Capacity <= 0.0f && isPlayingAsMilo)//Minimum Capacity for the Flashlight.
        {
            SwitchToKOS();
        } else if (miloAwakeTimer <= 0.0f && !isPlayingAsMilo)// Milo is awake once again.
        {
            SwitchToMilo();
        }
    }

    void OnGUI()
    {
        if (miloFlashlightComponent.Capacity <= 0.0f && miloAwakeTimer > 0.0f)
        {
            GUI.Label(new Rect(10.0f, 10.0f, 75.0f, 50.0f), "Milo awakes in: " + miloAwakeTimer);
        }
    }

    /// <summary>
    /// Gets or sets the switch counter.
    /// </summary>
    /// <value>The switch counter.</value>
    public int SwitchCounter
    {
        get
        {
            return switchCounter;
        }
        set
        {
            switchCounter = value;
        }
    }

    /// <summary>
    /// Resets the milo awake timer.
    /// </summary>
    public void ResetMiloAwakeTimer()
    {
        miloAwakeTimer = 30.0f;
    }

    /// <summary>
    /// Switches to KOS & resets collectables for KOS.
    /// </summary>
    void SwitchToKOS()
    {
        milo.gameObject.SetActive(false);
        kos.gameObject.SetActive(true);
        kos.transform.position = milo.transform.position;
        kos.transform.rotation = milo.transform.rotation;
        ResetMiloAwakeTimer();
        isPlayingAsMilo = false;
        SwitchCounter++;
        SwitchActiveValuesForCollectables();
        StartCoroutine(MiloAwakeCountdown());
    }

    /// <summary>
    /// Switches to Milo & resets collectables for Milo.
    /// </summary>
    void SwitchToMilo()
    {
        kos.gameObject.SetActive(false);
        milo.gameObject.SetActive(true);
        milo.transform.position = kos.transform.position;
        milo.transform.rotation = kos.transform.rotation;
        miloFlashlightComponent.ResetValues();
        isPlayingAsMilo = true;
        SwitchCounter++;
        SwitchActiveValuesForCollectables();
    }

    /// <summary>
    /// Start the countdown for when milo will awake.
    /// </summary>
    /// <returns>The awake countdown.</returns>
    IEnumerator MiloAwakeCountdown()
    {
        while (miloAwakeTimer > 0)
        {
            yield return new WaitForSeconds(1);
            miloAwakeTimer -= 1.0f;
        }
    }

    /// <summary>
    /// Switchs the active values for the collectables of Milo and KOS.
    /// When playing as Milo, hide KOSLotus. When playing as KOS, hide Batteries.
    /// </summary>
    void SwitchActiveValuesForCollectables()
    {
        if (allBatteries == null && allKOSLotus == null)
        {
            allBatteries = GameObject.FindGameObjectsWithTag("Battery");
            allKOSLotus = GameObject.FindGameObjectsWithTag("KOSLotus");
//            Debug.Log("BATS: " + allBatteries.Length + " ,LOTUS: " + allKOSLotus.Length);
        }
        if (milo.activeSelf && !kos.activeSelf)
        {
            foreach (GameObject battery in allBatteries)
            {
                battery.SetActive(true);
            }
            foreach (GameObject lotus in allKOSLotus)
            {
                lotus.SetActive(false);
            }
        } else if (kos.activeSelf && !milo.activeSelf)
        {
            foreach (GameObject lotus in allKOSLotus)
            {
                lotus.SetActive(true);
            }
            foreach (GameObject battery in allBatteries)
            {
                battery.SetActive(false);
            }
        }
    }
}
