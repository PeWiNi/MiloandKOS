using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MazeLevelDialogues : MonoBehaviour
{

    [SerializeField]
    Text[]
        dialogues;

    bool hasPlayedMomMiloPicksUpBattery;
    bool kosNotSpokenAfterTransition;
    static bool miloHasBeenStruckedByShadow;
    static int firstTimeKOSPicksUpLotusFlower = 0;
    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        Sequence();
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="MazeLevelDialogues"/> milo has been strucked by shadow.
    /// </summary>
    /// <value><c>true</c> if milo has been strucked by shadow; otherwise, <c>false</c>.</value>
    public static bool MiloHasBeenStruckedByShadow
    {
        get
        {
            return miloHasBeenStruckedByShadow;
        }
        set
        {
            miloHasBeenStruckedByShadow = value;
        }
    }

    /// <summary>
    /// Gets or sets the first time KOS picks up lotus flower.
    /// </summary>
    /// <value>The first time KOS picks up lotus flower.</value>
    public static int FirstTimeKOSPicksUpLotusFlower
    {
        get
        {
            return firstTimeKOSPicksUpLotusFlower;
        }
        set
        {
            firstTimeKOSPicksUpLotusFlower = value;
        }
    }
    
    void Sequence()
    {
        if (GameController.INSTANCE.SwitchTurnCounter % 2 == 0)// SwitchTurnCounter is Odd, active Character is KOS.
        {
        } else// SwitchTurnCounter is Even, active Character is Milo.
        {
            if (GameController.INSTANCE.IsPlayingAsMilo && GameController.INSTANCE.Milo.activeSelf && !hasPlayedMomMiloPicksUpBattery)
            {
                hasPlayedMomMiloPicksUpBattery = true;
                Mom_MiloPicksUpBattery();
            }
            if (miloHasBeenStruckedByShadow)
            {
                KOS_ShadowSpawnCollision();
                miloHasBeenStruckedByShadow = false;
            }
            if (GameController.INSTANCE.Kos.activeSelf && !kosNotSpokenAfterTransition)
            {
                kosNotSpokenAfterTransition = true;
                KOS_TransitionFromMiloToKOS();
            } else if (GameController.INSTANCE.IsPlayingAsMilo && kosNotSpokenAfterTransition)
            {
                kosNotSpokenAfterTransition = false;
            }
            if (!GameController.INSTANCE.IsPlayingAsMilo && firstTimeKOSPicksUpLotusFlower == 1)
            {
                firstTimeKOSPicksUpLotusFlower++;
                Teacher_StoryTellingStuff();
            }
        }
    }

    void Mom_MiloPicksUpBattery()
    {
        dialogues [5].enabled = true;
        StartCoroutine(FadeText(dialogues [5]));
    }

    void Teacher_StoryTellingStuff()
    {
        dialogues [3].enabled = true;
        StartCoroutine(FadeText(dialogues [3]));
    }

    void Milo_MazeEntrance()
    {
        dialogues [2].enabled = true;
        StartCoroutine(FadeText(dialogues [2]));
    }

    void Milo_AwakeBarAlmostFull()
    {
        dialogues [4].enabled = true;
        StartCoroutine(FadeText(dialogues [4]));
    }

    void KOS_ShadowSpawnCollision()
    {
        dialogues [6].enabled = true;
        StartCoroutine(FadeText(dialogues [6]));
    }

    void KOS_NotEnoughLotusFlowers()
    {
        dialogues [0].enabled = true;
        StartCoroutine(FadeText(dialogues [0]));
    }

    void KOS_TransitionFromMiloToKOS()
    {
        dialogues [1].enabled = true;
        StartCoroutine(FadeText(dialogues [1]));
    }

    /// <summary>
    /// Fades the text.
    /// </summary>
    /// <returns>The text.</returns>
    IEnumerator FadeText(Text text)
    {
        Color color;
        while (text.color.a > 0.0f)
        {
            color.a = text.color.a - 0.02f;
            color.r = text.color.r;
            color.g = text.color.g;
            color.b = text.color.b;
            text.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        color.a = 1.0f;
        color.r = text.color.r;
        color.g = text.color.g;
        color.b = text.color.b;
        text.color = color;
        text.enabled = false;
    }
}
