using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController INSTANCE;
    private GameObject milo;
    private GameObject kos;
    private Flashlight miloFlashlightComponent;
    private bool isPlayingAsMilo = true;
    private float miloAwakeTimer = 15.0f;

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
    /// Resets the milo awake timer.
    /// </summary>
    public void ResetMiloAwakeTimer()
    {
        miloAwakeTimer = 15.0f;
    }

    /// <summary>
    /// Switches to KOS.
    /// </summary>
    void SwitchToKOS()
    {
        milo.gameObject.SetActive(false);
        kos.gameObject.SetActive(true);
        kos.transform.position = milo.transform.position;
        kos.transform.rotation = milo.transform.rotation;
        ResetMiloAwakeTimer();
        isPlayingAsMilo = false;
        StartCoroutine(MiloAwakeCountdown());
    }

    /// <summary>
    /// Switches to Milo.
    /// </summary>
    void SwitchToMilo()
    {
        kos.gameObject.SetActive(false);
        milo.gameObject.SetActive(true);
        milo.transform.position = kos.transform.position;
        milo.transform.rotation = kos.transform.rotation;
        miloFlashlightComponent.ResetValues();
        isPlayingAsMilo = true;
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
}
