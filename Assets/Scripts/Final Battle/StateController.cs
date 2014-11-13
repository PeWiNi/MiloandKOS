using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    bool miloShooting;
    bool kosShooting;
    GameObject milo;
    GameObject kos;
    Text consecutiveHitsText;
    string actualText;
    static int consecutiveHitsMax = 3;
    static int consecutiveHitsValue = 0;
    const float speed = 2.0f;

    // Use this for initialization
    void Start()
    {
        milo = GameObject.Find("MiloCannon01");
        kos = GameObject.Find("KOSDoubleAxe01");
        consecutiveHitsText = GameObject.Find("NoConsecutiveHits").GetComponent<Text>();
        actualText = consecutiveHitsText.text;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (Application.loadedLevelName.Equals("OutroCutsceneKOS") && !miloShooting && consecutiveHitsValue < consecutiveHitsMax)
        {
            miloShooting = !miloShooting;
            StartCoroutine("MiloShootAtKOS");
        } else if (Application.loadedLevelName.Equals("OutroCutsceneMilo") && !kosShooting && consecutiveHitsValue < consecutiveHitsMax)
        {
            kosShooting = !kosShooting;
            StartCoroutine("KOSShootAtMilo");
        }
        consecutiveHitsText.text = actualText + consecutiveHitsValue + "/" + consecutiveHitsMax;
    }
    
    void FixedUpdate()
    {
        if (Application.loadedLevelName.Equals("OutroCutsceneKOS"))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                kos.transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0.0f, 0.0f);
            }
        } else if (Application.loadedLevelName.Equals("OutroCutsceneMilo"))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                milo.transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
    }

    /// <summary>
    /// Gets or sets the consecutive hits max.
    /// </summary>
    /// <value>The consecutive hits max.</value>
    public static int ConsecutiveHitsMax
    {
        get
        {
            return consecutiveHitsMax;
        }
    }

    /// <summary>
    /// Gets or sets the consecutive hits value.
    /// </summary>
    /// <value>The consecutive hits value.</value>
    public static int ConsecutiveHitsValue
    {
        get
        {
            return consecutiveHitsValue;
        }
        set
        {
            consecutiveHitsValue = value;
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
