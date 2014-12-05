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
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                kos.transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0.0f, 0.0f);
            }
        } else if (Application.loadedLevelName.Equals(nextSceneAsMilo))
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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
    }
}
