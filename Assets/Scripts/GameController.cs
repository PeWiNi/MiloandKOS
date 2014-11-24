using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    static GameController _INSTANCE;
    GameObject milo;
    GameObject kos;
    Flashlight miloFlashlightComponent;
    bool isPlayingAsMilo = true;
    float miloAwakeTimer = 0.0f;
<<<<<<< HEAD
<<<<<<< HEAD
    int miloAwakeTimerMax = 180;//3mins.
=======
    int miloAwakeTimerMax = 360;
>>>>>>> origin/master
=======
    int miloAwakeTimerMax = 360;
>>>>>>> origin/master
    int switchCounter = 0;
    int maxNeededLotusFlowers = 10;//The maximum number needed to proceed the
    int currentCollectedLotusFlowers = 0;//The current amount collected.
    List<GameObject> allKOSLotus;
    List<GameObject> allBatteries;
    bool switchHasBeenExecuted = false;
    Texture2D backgroundKOS;
    Texture2D foregroundKOS;
    Rect box = new Rect(10, 10, 100, 20);
    Text kosCollectableUIText;

    // This happens before Start
    void Awake()
    {
        _INSTANCE = this;
    }

    // Use this for initialization
    void Start()
    {
        miloFlashlightComponent = GameObject.Find("Flashlight").GetComponent<Flashlight>();
        milo = GameObject.Find("Milo");//Find Milo.
        kos = GameObject.Find("KOSMinotaur");//Find !Milo.
        kosCollectableUIText = GameObject.Find("KOSCollectableCount").GetComponent<Text>();
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
            SwitchFadingInOut.SwitchStarting = true;
        } else if (miloAwakeTimer >= miloAwakeTimerMax && !isPlayingAsMilo)// Milo is awake once again.
        {
            SwitchFadingInOut.SwitchStarting = true;
        }
    }

    void OnGUI()
    {
        if (!isPlayingAsMilo)
        {
            if (miloFlashlightComponent.Capacity <= 0.0f && miloAwakeTimer > 0.0f)
            {
                GUI.BeginGroup(box);
                {
                    GUI.DrawTexture(new Rect(0, 0, box.width, box.height), backgroundKOS, ScaleMode.StretchToFill);
                    GUI.DrawTexture(new Rect(0, 0, box.width * miloAwakeTimer / miloAwakeTimerMax, box.height), foregroundKOS, ScaleMode.StretchToFill);
                }
                GUI.EndGroup();
            }
            string totalCollectedLotusFlowers = currentCollectedLotusFlowers + "/" + maxNeededLotusFlowers + " Lotus' Collected";
            kosCollectableUIText.enabled = true;
            kosCollectableUIText.text = totalCollectedLotusFlowers;
        } else
        {
            kosCollectableUIText.text = "";
            kosCollectableUIText.enabled = false;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is playing as milo.
    /// </summary>
    /// <value><c>true</c> if this instance is playing as milo; otherwise, <c>false</c>.</value>
    public bool IsPlayingAsMilo
    {
        get
        {
            return isPlayingAsMilo;
        }
    }

    /// <summary>
    /// Gets or sets the _ INSTANC.
    /// </summary>
    /// <value>The _ INSTANC.</value>
    public static GameController INSTANCE
    {
        get
        {
            return _INSTANCE;
        }
        set
        {
            _INSTANCE = value;
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
    /// Gets or sets all KOS lotus.
    /// </summary>
    /// <value>All KOS lotus.</value>
    public List<GameObject> AllKOSLotus
    {
        get
        {
            return allKOSLotus;
        }
        set
        {
            allKOSLotus = value;
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
    /// Gets or sets the max needed lotus flowers.
    /// </summary>
    /// <value>The max needed lotus flowers.</value>
    public int MaxNeededLotusFlowers
    {
        get
        {
            return maxNeededLotusFlowers;
        }
        set
        {
            maxNeededLotusFlowers = value;
        }
    }

    /// <summary>
    /// Gets or sets the current collected lotus flowers.
    /// </summary>
    /// <value>The current collected lotus flowers.</value>
    public int CurrentCollectedLotusFlowers
    {
        get
        {
            return currentCollectedLotusFlowers;
        }
        set
        {
            currentCollectedLotusFlowers = value;
        }
    }

    /// <summary>
    /// Switches to KOS & resets collectables for KOS.
    /// </summary>
    public void SwitchToKOS()
    {
        kos.gameObject.SetActive(true);
        kos.transform.position = milo.transform.position;
        kos.transform.rotation = milo.transform.rotation;
        isPlayingAsMilo = false;
        ResetMiloAwakeTimer();
        SwitchCounter++;
        SwitchActiveValuesForCollectables();
        StartCoroutine(MiloAwakeCountdown());
        CameraPan.AttachedTo = kos;
    }

    /// <summary>
    /// Switches to Milo & resets collectables for Milo.
    /// </summary>
    public void SwitchToMilo()
    {
        kos.gameObject.SetActive(false);
        milo.GetComponent<Animator>().SetFloat(Animator.StringToHash("Movement"), 0.0f);//Set to Idle.
        milo.transform.rotation = kos.transform.rotation;
        isPlayingAsMilo = true;
        miloFlashlightComponent.ResetValues();
        SwitchCounter++;
        SwitchActiveValuesForCollectables();
        CameraPan.AttachedTo = milo;
    }

    /// <summary>
    /// Start the countdown for when milo will awake.
    /// </summary>
    /// <returns>The awake countdown.</returns>
    IEnumerator MiloAwakeCountdown()
    {
        while (miloAwakeTimer < miloAwakeTimerMax)
        {
            yield return new WaitForSeconds(1.0f);
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
            GameObject[] batteries = GameObject.FindGameObjectsWithTag("Battery");
            allBatteries = new List<GameObject>(batteries);
            GameObject[] lotusFlowers = GameObject.FindGameObjectsWithTag("KOSLotus");
            allKOSLotus = new List<GameObject>(lotusFlowers);
        } 
        if (isPlayingAsMilo)
        {
            foreach (GameObject battery in allBatteries)
            {
                battery.SetActive(true);
            }
            foreach (GameObject lotus in allKOSLotus)
            {
                lotus.SetActive(false);
            }
        } else if (!isPlayingAsMilo)
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

    /// <summary>
    /// Sets the state for milo dragged.
    /// </summary>
//    public void SetStateForMiloDragged()
    public void SetStateForSwitching()
    {
        if (isPlayingAsMilo)
        {
            GameController.INSTANCE.SwitchToKOS();
            milo.transform.rotation = Quaternion.Euler(270.0f, 0.0f, 0.0f);
            milo.GetComponent<Move>().enabled = false;
            milo.GetComponent<Jump>().enabled = false;
            milo.GetComponent<ShadowEffect>().enabled = false;
            milo.GetComponent<Animator>().enabled = false;
//            milo.AddComponent<SpringJoint>();
//            milo.GetComponent<SpringJoint>().connectedBody = kos.rigidbody;
//            milo.GetComponent<SpringJoint>().anchor = new Vector3(0.0f, 0.0f, 0.0f);
//            milo.GetComponent<SpringJoint>().spring = 1000.0f;
//            milo.rigidbody.constraints = RigidbodyConstraints.None;
        } else
        {
//            milo.transform.position = kos.transform.position;
//            milo.transform.rotation = kos.transform.rotation;
            milo.GetComponent<Move>().enabled = true;
            milo.GetComponent<Jump>().enabled = true;
            milo.GetComponent<ShadowEffect>().enabled = true;
            milo.GetComponent<Animator>().enabled = true;
//            Destroy(milo.GetComponent<SpringJoint>());
//            milo.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            SwitchToMilo();
        }
    }
}
