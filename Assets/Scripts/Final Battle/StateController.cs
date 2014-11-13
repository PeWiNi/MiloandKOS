using UnityEngine;
using System.Collections;

public class StateController : MonoBehaviour
{
    bool miloShooting;
    bool kosShooting;
    GameObject milo;
    GameObject kos;

    // Use this for initialization
    void Start()
    {
        milo = GameObject.Find("MiloCannon01");
        kos = GameObject.Find("KOSDoubleAxe01");
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName.Equals("OutroCutsceneKOS") && !miloShooting)
        {
            miloShooting = !miloShooting;
            StartCoroutine("MiloShootAtKOS");
        } else if (Application.loadedLevelName.Equals("OutroCutsceneMilo") && !kosShooting)
        {
            kosShooting = !kosShooting;
            StartCoroutine("KOSShootAtMilo");
        }
    }

    /// <summary>
    /// Milo shoots at KOS.
    /// </summary>
    IEnumerator MiloShootAtKOS()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(milo.GetComponent<MiloShootCannonBall>().SpawnCannonball());
        miloShooting = !miloShooting;
    }

    /// <summary>
    /// KOS shoots at Milo.
    /// </summary>
    IEnumerator KOSShootAtMilo()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(kos.GetComponent<KOSThrowAxe>().SpawnAxe());
        kosShooting = !kosShooting;
    }
}
