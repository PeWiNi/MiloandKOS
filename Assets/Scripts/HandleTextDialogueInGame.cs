using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HandleTextDialogueInGame : MonoBehaviour
{
    Text switchTexting;
    float letterPause;
    int currentSwitchCounter;
    string sentenceKOS;
    string sentenceMilo;
    bool kosTexthasBeenPrinted = false;
    bool miloTexthasBeenPrinted = false;

    // Use this for initialization
    void Start()
    {
        switchTexting = GetComponent<Text>();
        sentenceKOS = switchTexting.text.Substring(0, 63);// Copy the text into the new string.
        sentenceMilo = switchTexting.text.Substring(sentenceKOS.Length, switchTexting.text.Length - sentenceKOS.Length);
        switchTexting.text = "";
        switchTexting.enabled = false;
        currentSwitchCounter = GameController.INSTANCE.SwitchCounter;
    }
	
    // Update is called once per frame
    void Update()
    {
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
            if (!kosTexthasBeenPrinted)
            {
                PrintLetterSequenceKOS();
                kosTexthasBeenPrinted = true;
            }
        } else// SwitchCounter is Even, active Character is Milo.
        {
            if (!miloTexthasBeenPrinted)
            {
                PrintLetterSequenceMilo();
                miloTexthasBeenPrinted = true;
            }
        }
    }

    /// <summary>
    /// Prints the letter sequence for KOS.
    /// </summary>
    /// <returns>The letter sequence KO.</returns>
    void PrintLetterSequenceKOS()
    {
        switchTexting.text = sentenceKOS;
        switchTexting.enabled = true;
        StartCoroutine("FadeText");
    }

    /// <summary>
    /// Prints the letter sequence for Milo.
    /// </summary>
    /// <returns>The letter sequence milo.</returns>
    void PrintLetterSequenceMilo()
    {
        switchTexting.text = sentenceMilo;
        switchTexting.enabled = true;
        StartCoroutine("FadeText");
    }

    /// <summary>
    /// Fades the text.
    /// </summary>
    /// <returns>The text.</returns>
    IEnumerator FadeText()
    {
        Color color;
        while (switchTexting.color.a > 0.0f)
        {
            color.a = switchTexting.color.a - 0.01f;
            color.r = switchTexting.color.r;
            color.g = switchTexting.color.g;
            color.b = switchTexting.color.b;
            switchTexting.color = color;
            yield return new WaitForSeconds(0.1f);
        }
        color.a = 1.0f;
        color.r = switchTexting.color.r;
        color.g = switchTexting.color.g;
        color.b = switchTexting.color.b;
        switchTexting.color = color;
        switchTexting.text = "";
    }
}
