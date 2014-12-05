﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    static StateController _INSTANCE;
    public const string nextSceneAsMilo = "FinalBattleAsMilo";
    public const string nextSceneAsKOS = "FinalBattleAsKOS";
    public const string endingCutSecene = "EndingSceneVsMom";
    bool miloShooting;
    bool kosShooting;
    GameObject milo;
    GameObject kos;
    Text consecutiveHitsText;
    string actualText;
    static int kosHitsTakenMax = 10;
    static int kosHitsTakenValue = 0;
    static int miloHitsTakenMax = 10;
    static int miloHitsTakenValue = 0;
    static int consecutiveHitsMax = 3;
    static int consecutiveHitsValue = 0;
    const float speed = 2.0f;
    bool hasDialoguesBeenStarted = false;

    void Awake()
    {
        _INSTANCE = this;
    }

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
        if (hasDialoguesBeenStarted)//Ensures we need to hear the whole conversation before the AI starts shooting.
        {
            if (Application.loadedLevelName.Equals(nextSceneAsKOS) && !miloShooting && consecutiveHitsValue < consecutiveHitsMax)
            {
                miloShooting = !miloShooting;
                StartCoroutine("MiloShootAtKOS");
            } else if (Application.loadedLevelName.Equals(nextSceneAsMilo) && !kosShooting && consecutiveHitsValue < consecutiveHitsMax)
            {
                kosShooting = !kosShooting;
                StartCoroutine("KOSShootAtMilo");
            }
            consecutiveHitsText.text = actualText + consecutiveHitsValue + "/" + consecutiveHitsMax;
            ChangeToEndingScene();
        }
    }
    
    void FixedUpdate()
    {
        if (Application.loadedLevelName.Equals(nextSceneAsKOS))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) 
                || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                kos.transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0.0f, 0.0f);
            }
        } else if (Application.loadedLevelName.Equals(nextSceneAsMilo))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                milo.transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0.0f, 0.0f);
            }
        }
    }

    /// <summary>
    /// Gets the _ INSTANC.
    /// </summary>
    /// <value>The _ INSTANC.</value>
    public static StateController INSTANCE
    {
        get
        {
            return _INSTANCE;
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
    /// Gets the KOS hits taken max.
    /// </summary>
    /// <value>The KOS hits taken max.</value>
    public static int KOSHitsTakenMax
    {
        get
        {
            return kosHitsTakenMax;
        }
    }

    /// <summary>
    /// Gets or sets the KOS hits taken value.
    /// </summary>
    /// <value>The KOS hits taken value.</value>
    public static int KOSHitsTakenValue
    {
        get
        {
            return kosHitsTakenValue;
        }
        set
        {
            kosHitsTakenValue = value;
        }
    }

    /// <summary>
    /// Gets the milo hits taken max.
    /// </summary>
    /// <value>The milo hits taken max.</value>
    public static int MiloHitsTakenMax
    {
        get
        {
            return miloHitsTakenMax;
        }
    }

    /// <summary>
    /// Gets or sets the milo hits taken value.
    /// </summary>
    /// <value>The milo hits taken value.</value>
    public static int MiloHitsTakenValue
    {
        get
        {
            return miloHitsTakenValue;
        }
        set
        {
            miloHitsTakenValue = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has dialogues been started.
    /// </summary>
    /// <value><c>true</c> if this instance has dialogues been started; otherwise, <c>false</c>.</value>
    public bool HasDialoguesBeenStarted
    {
        get
        {
            return hasDialoguesBeenStarted;
        }
        set
        {
            hasDialoguesBeenStarted = value;
        }
    }

    /// <summary>
    /// Milo shoots at KOS.
    /// </summary>
    IEnumerator MiloShootAtKOS()
    {
        yield return new WaitForSeconds(2f);
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

    /// <summary>
    /// Changes to ending scene.
    /// </summary>
    void ChangeToEndingScene()
    {
        if (Application.loadedLevelName.Equals(nextSceneAsKOS) && !miloShooting && consecutiveHitsValue >= consecutiveHitsMax)
        {
            Application.LoadLevel(endingCutSecene);
        } else if (Application.loadedLevelName.Equals(nextSceneAsMilo) && !kosShooting && consecutiveHitsValue >= consecutiveHitsMax)
        {
            Application.LoadLevel(endingCutSecene);
        } 
        if (kosHitsTakenValue >= kosHitsTakenMax)
        {
            Application.LoadLevel(endingCutSecene);
        }
        if (miloHitsTakenValue >= miloHitsTakenMax)
        {
            Application.LoadLevel(endingCutSecene);
        }
    }
   
}
