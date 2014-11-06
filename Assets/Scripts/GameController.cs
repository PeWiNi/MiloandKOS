using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController INSTANCE;
    GameObject milo;
    GameObject kos;
    Flashlight miloFlashlightComponent;
    bool isPlayingAsMilo = true;
    float miloAwakeTimer = 0.0f;
    int miloAwakeTimerMax = 60;
    int switchCounter = 0;

    GameObject[] allKOSLotus;
    GameObject[] allBatteries;
    bool switchHasBeenExecuted = false;

    Texture2D backgroundKOS;
    Texture2D foregroundKOS;
    Rect box = new Rect(10, 10, 100, 20);
    
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
        SetMiloAwakeBar();
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
        } else if (miloAwakeTimer >= miloAwakeTimerMax && !isPlayingAsMilo)// Milo is awake once again.
        {
            SwitchToMilo();
        }
    }

    void OnGUI()
    {
        if (miloFlashlightComponent.Capacity <= 0.0f && miloAwakeTimer > 0.0f)
        {
//            GUI.Label(new Rect(10.0f, 10.0f, 75.0f, 50.0f), "Milo awakes in: " + miloAwakeTimer);
            GUI.BeginGroup(box);
            {
                GUI.DrawTexture(new Rect(0, 0, box.width, box.height), backgroundKOS, ScaleMode.StretchToFill);
                GUI.DrawTexture(new Rect(0, 0, box.width * miloAwakeTimer / miloAwakeTimerMax, box.height), foregroundKOS, ScaleMode.StretchToFill);
            }
            GUI.EndGroup();
        }
    }

    /// <summary>
    /// Gets the milo.
    /// </summary>
    /// <value>The milo.</value>
    public GameObject Milo
    {
        get
        {
            return milo;
        }
    }

    /// <summary>
    /// Gets the kos.
    /// </summary>
    /// <value>The kos.</value>
    public GameObject Kos
    {
        get
        {
            return kos;
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
        miloAwakeTimer = 0.0f;
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
        while (miloAwakeTimer < 60)
        {
            yield return new WaitForSeconds(1);
            miloAwakeTimer += 1.0f;
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

    /// <summary>
    /// Sets the milo awake bar.
    /// </summary>
    void SetMiloAwakeBar()
    {
        backgroundKOS = new Texture2D(1, 1, TextureFormat.RGB24, false);
        foregroundKOS = new Texture2D(1, 1, TextureFormat.RGB24, false);
        Color darkBlue = new Color(0.04f, 0.16f, 0.35f);
        foregroundKOS.SetPixel(0, 0, Color.yellow);
        backgroundKOS.SetPixel(0, 0, darkBlue);
        backgroundKOS.Apply();
        foregroundKOS.Apply();
    }
}
