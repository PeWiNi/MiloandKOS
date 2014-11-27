using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleTextDialogueInGame : MonoBehaviour
{
    public Text switchTextKOS;
    public Text switchTextMilo;
    public Text introTextMOM;
    public Text introTextKOS;

    bool switchTextKOSHasBeenPrinted = false;
    bool switchTextMiloHasBeenPrinted = false;
    bool introTextMOMHasBeenPrinted = false;

    // Use this for initialization
    void Start()
    {

    }
	
    // Update is called once per frame
    void Update()
    {
        ShowTextInOrder();
    }

    /// <summary>
    /// Shows the text in order.
    /// </summary>
    void ShowTextInOrder()
    {
        if (GameController.INSTANCE.SwitchTurnCounter % 2 != 0)// SwitchTurnCounter is Odd, active Character is KOS.
        {
            if (!switchTextKOSHasBeenPrinted)
            {
                PrintSwitchTextKOS();
                switchTextKOSHasBeenPrinted = true;
            }
        } else// SwitchTurnCounter is Even, active Character is Milo.
        {
            if (!introTextMOMHasBeenPrinted && GameController.INSTANCE.SwitchTurnCounter == 0)//Start by displaying the advice from Milos Mother.
            {
                PrintIntroSequencePlayingAsMilo();
                introTextMOMHasBeenPrinted = true;
            } else if (!switchTextMiloHasBeenPrinted && GameController.INSTANCE.SwitchTurnCounter > 1)//We have played as KOS one time.
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
