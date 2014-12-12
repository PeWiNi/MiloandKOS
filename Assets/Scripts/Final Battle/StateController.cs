using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    static StateController _INSTANCE;
    public const string nextSceneAsMilo = "FinalBattleAsMilo";
    public const string nextSceneAsKOS = "FinalBattleAsKOS";
    public const string endingCutScene = "EndingSceneVsMom";
    bool miloShooting;
    bool kosShooting;
    GameObject milo;
    GameObject kos;
    Image consecutiveMiloHitsImage; 
    Image consecutiveKOSHitsImage; 
    Image losingKOSImageIndicator;
    Image losingMiloImageIndicator;
    static int kosHitsTakenMax = 10;
    static int kosHitsTakenValue = 0;
    static int miloHitsTakenMax = 10;
    static int miloHitsTakenValue = 0;
    static int consecutiveHitsMax = 3;
    static int consecutiveHitsValue = 0;
    const float speed = 2.0f;
    bool hasDialoguesBeenStarted = false;
    static GameObject instructionImages;
    [SerializeField]
    Sprite
        MBIimage01;
    [SerializeField]
    Sprite
        MBIimage02;
    [SerializeField]
    Sprite
        MBIimage03;
    [SerializeField]
    Sprite
        MBIimage04;
    Sprite[] kosLosingSprites;
    Sprite[] miloLosingSprites;
    Sprite[] sprites;
    void Awake()
    {
        _INSTANCE = this;
    }

    // Use this for initialization
    void Start()
    {
        instructionImages = GameObject.Find("Instruction");
        instructionImages.SetActive(true);
        milo = GameObject.Find("MiloCannon01");
        kos = GameObject.Find("KOSDoubleAxe01");
        sprites = Resources.LoadAll<Sprite>("BattleIndicatorBY"); 
        kosLosingSprites = Resources.LoadAll<Sprite>("KosLosingIndicator01");
        miloLosingSprites = Resources.LoadAll<Sprite>("MiloLosingIndicator01");
        if (!Application.loadedLevelName.Equals(nextSceneAsKOS))
        {
            consecutiveMiloHitsImage = GameObject.Find("MiloBattleIndicator").GetComponent<Image>();
            losingMiloImageIndicator = GameObject.Find("MiloLosingIndicator").GetComponent<Image>();
        } else if (!Application.loadedLevelName.Equals(nextSceneAsMilo))
        {
            consecutiveMiloHitsImage = GameObject.Find("KosBattleIndicator").GetComponent<Image>();
            losingKOSImageIndicator = GameObject.Find("KosLosingIndicator").GetComponent<Image>();
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        if (hasDialoguesBeenStarted)//Ensures we need to hear the whole conversation before the AI starts shooting.
        {
            if (!Application.loadedLevelName.Equals(nextSceneAsKOS))
            {
                UpdateMiloScore();
                UpdateMiloLosing();
            }
            if (!Application.loadedLevelName.Equals(nextSceneAsMilo))
            {
                UpdateKosScore();
                UpdateKosLosing();
            }
            if (Application.loadedLevelName.Equals(nextSceneAsKOS) && !miloShooting && consecutiveHitsValue < consecutiveHitsMax)
            {
                miloShooting = !miloShooting;
                StartCoroutine("MiloShootAtKOS");
            } else if (Application.loadedLevelName.Equals(nextSceneAsMilo) && !kosShooting && consecutiveHitsValue < consecutiveHitsMax)
            {
                kosShooting = !kosShooting;
                StartCoroutine("KOSShootAtMilo");
            }
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
            ResetValues();
            Application.LoadLevel(endingCutScene);
        } else if (Application.loadedLevelName.Equals(nextSceneAsMilo) && !kosShooting && consecutiveHitsValue >= consecutiveHitsMax)
        {
            ResetValues();
            Application.LoadLevel(endingCutScene);
        } 
        if (kosHitsTakenValue >= kosHitsTakenMax)
        {
            ResetValues();
            Application.LoadLevel(endingCutScene);
        }
        if (miloHitsTakenValue >= miloHitsTakenMax)
        {
            ResetValues();
            Application.LoadLevel(endingCutScene);
        }
    }
   
    /// <summary>
    /// Updates the milo score.
    /// </summary>
    void UpdateMiloScore()
    {
        if (consecutiveHitsValue == 0)
        {
            consecutiveMiloHitsImage.sprite = sprites [0];
        }
        if (consecutiveHitsValue == 1)
        {
            consecutiveMiloHitsImage.sprite = sprites [1];
        } else if (consecutiveHitsValue == 2)
        {
            consecutiveMiloHitsImage.sprite = sprites [2];
            
        } else if (consecutiveHitsValue == 3)
        {
            consecutiveMiloHitsImage.sprite = sprites [3];
        }
    }

    /// <summary>
    /// Updates the kos score.
    /// </summary>
    void UpdateKosScore()
    {
        if (consecutiveHitsValue == 0)
        {
            consecutiveMiloHitsImage.sprite = sprites [0];
        }
        if (consecutiveHitsValue == 1)
        {
            consecutiveMiloHitsImage.sprite = sprites [4];
        } else if (consecutiveHitsValue == 2)
        {
            consecutiveMiloHitsImage.sprite = sprites [5];
            
        } else if (consecutiveHitsValue == 3)
        {
            consecutiveMiloHitsImage.sprite = sprites [6];
        }
    }

    void UpdateKosLosing()
    {
        if (kosHitsTakenValue == 0)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [0];
        }
        if (kosHitsTakenValue == 1)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [1];
        } else if (kosHitsTakenValue == 2)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [2];
        } else if (kosHitsTakenValue == 3)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [3];
        } else if (kosHitsTakenValue == 4)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [4];
        } else if (kosHitsTakenValue == 5)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [5];
        } else if (kosHitsTakenValue == 6)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [6];
        } else if (kosHitsTakenValue == 7)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [7];
        } else if (kosHitsTakenValue == 8)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [8];
        } else if (kosHitsTakenValue == 9)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [9];
        } else if (kosHitsTakenValue == 10)
        {
            losingKOSImageIndicator.sprite = kosLosingSprites [10];
            consecutiveMiloHitsImage.sprite = sprites [3];
        }
    }


    void UpdateMiloLosing()
    {
        if (miloHitsTakenValue == 0)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [0];
        }
        if (miloHitsTakenValue == 1)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [1];
        } else if (miloHitsTakenValue == 2)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [2];
        } else if (miloHitsTakenValue == 3)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [3];
        } else if (miloHitsTakenValue == 4)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [4];
        } else if (miloHitsTakenValue == 5)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [5];
        } else if (miloHitsTakenValue == 6)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [6];
        } else if (miloHitsTakenValue == 7)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [7];
        } else if (miloHitsTakenValue == 8)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [8];
        } else if (miloHitsTakenValue == 9)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [9];
        } else if (miloHitsTakenValue == 10)
        {
            losingMiloImageIndicator.sprite = miloLosingSprites [10];
            consecutiveMiloHitsImage.sprite = sprites [6];
        }
    }

    /// <summary>
    /// Gets the instruction.
    /// </summary>
    /// <value>The instruction.</value>
    public static GameObject Instruction
    {
        get
        {
            return instructionImages; 
        }
    }

    /// <summary>
    /// Resets the values.
    /// </summary>
    void ResetValues()
    {
        consecutiveHitsValue = 0;
        miloHitsTakenValue = 0;
        kosHitsTakenValue = 0;
    }
}
