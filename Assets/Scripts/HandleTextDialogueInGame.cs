using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleTextDialogueInGame : MonoBehaviour
{
    public Text switchTextKOS;
    public Text switchTextMilo;
    public Text introTextMOM;
    public Text introTextKOS;

    int currentSwitchCounter;
    bool switchTextKOSHasBeenPrinted = false;
    bool switchTextMiloHasBeenPrinted = false;
    bool introTextMOMHasBeenPrinted = false;

    // Use this for initialization
    void Start()
    {
        currentSwitchCounter = GameController.INSTANCE.SwitchCounter;
    }
	
    // Update is called once per frame
    void Update()
    {
        if (GameController.INSTANCE != null && GameController.INSTANCE.Milo.activeSelf)
        {
            if (!introTextMOMHasBeenPrinted)
            {
                PrintIntroSequencePlayingAsMilo();
                introTextMOMHasBeenPrinted = true;
            }
        }
        if (GameController.INSTANCE.SwitchCounter != currentSwitchCounter)// Check if a Switch Happened in the game.
        {
            currentSwitchCounter = GameController.INSTANCE.SwitchCounter;
            ShowTextInOrder();
        }
    }

    /// <summary>
    /// Shows the text in order.
    /// </summary>
    void ShowTextInOrder()
    {
        if (currentSwitchCounter % 2 != 0)// SwitchCounter is Odd, active Character is KOS.
        {
            if (!switchTextKOSHasBeenPrinted)
            {
                PrintSwitchTextKOS();
                switchTextKOSHasBeenPrinted = true;
            }
        } else// SwitchCounter is Even, active Character is Milo.
        {
            if (!switchTextMiloHasBeenPrinted)
            {
                PrintSwitchTextMilo();
                switchTextMiloHasBeenPrinted = true;
            }
        }
    }

    /// <summary>
    /// Prints the switch text KOS.
    /// </summary>
    void PrintSwitchTextKOS()
    {
        switchTextKOS.enabled = true;
        StartCoroutine("FadeText", switchTextKOS);
    }

    /// <summary>
    /// Prints the switch text Milo.
    /// </summary>
    void PrintSwitchTextMilo()
    {
        switchTextMilo.enabled = true;
        StartCoroutine("FadeText", switchTextMilo);
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
            color.a = text.color.a - 0.01f;
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

    /// <summary>
    /// Prints the intro sequence playing as Milo.
    /// </summary>
    void PrintIntroSequencePlayingAsMilo()
    {
        introTextMOM.enabled = true;
        StartCoroutine("FadeText", introTextMOM);
    }

    /// <summary>
    /// Prints Milos first contact with a battery.
    /// </summary>
    void PrintMiloFirstContactwithBattery()
    {

    }
}
