using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndingCutsceneVSMomDialogues : MonoBehaviour
{
    [SerializeField]
    Text[]
        dialogues;
    bool momFirstWarning;
    bool momSecondWarning;
    bool narrator;

    bool hasBeenInitiated;
    bool isPlaying;
    
    // Use this for initialization
    void Start()
    {

    }
	
    // Update is called once per frame
    void Update()
    {
        if (!hasBeenInitiated)
        {
            StartCoroutine(BeginDialogueSequence());
        } else
        {
            CheckSequence();
        }
    }

    /// <summary>
    /// Checks the sequence.
    /// </summary>
    void CheckSequence()
    {
        if (!momFirstWarning)
        {
            isPlaying = true;
            momFirstWarning = true;
            dialogues [0].enabled = true;
            StartCoroutine(DisplayTextDialogue(dialogues [0], 6f));
        } else if (momFirstWarning && !momSecondWarning && !isPlaying)
        {
            isPlaying = true;
            momSecondWarning = true;
            dialogues [1].enabled = true;
            StartCoroutine(DisplayTextDialogue(dialogues [1], 6f));
        } else if (momSecondWarning && !narrator && !isPlaying)
        {
            isPlaying = true;
            narrator = true;
            dialogues [2].enabled = true;
            StartCoroutine(DisplayTextDialogue(dialogues [2], 12f));
        }
    }

    /// <summary>
    /// Begins the dialogue sequence countdown.
    /// </summary>
    /// <returns>The dialogue sequence.</returns>
    IEnumerator BeginDialogueSequence()
    {
        yield return new WaitForSeconds(3f);
        hasBeenInitiated = true;
    }

    /// <summary>
    /// Displaies the text dialogue.
    /// </summary>
    /// <returns>The text dialogue.</returns>
    /// <param name="text">Text.</param>
    /// <param name="seconds">Seconds.</param>
    IEnumerator DisplayTextDialogue(Text text, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        text.enabled = false;
        if (momFirstWarning && !momSecondWarning && !narrator)
        {
            StartCoroutine(Wait(8f));
        } else if (momSecondWarning && momSecondWarning && !narrator)
        {
            StartCoroutine(Wait(20f));
        } else if (narrator)
        {
            isPlaying = false;
        }
    }

    /// <summary>
    /// Wait the specified amountBetweenDialogues.
    /// </summary>
    /// <param name="amountBetweenDialogues">Amount between dialogues.</param>
    IEnumerator Wait(float amountBetweenDialogues)
    {
        yield return new WaitForSeconds(amountBetweenDialogues);
        isPlaying = false;
    }
}
