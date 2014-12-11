using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitMazeEnterShipAsKOSDialogues : MonoBehaviour
{
    [SerializeField]
    Text[]
        dialogues;
    bool hasKOSSpoken;
    bool hasMiloSpoken;
    bool hasBeenInitiated;
    bool isPlaying;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!hasKOSSpoken)
        {
            hasKOSSpoken = true;
            StartCoroutine(BeginDialogueSequenceForKOS());
        } 
        if (!hasMiloSpoken)
        {
            hasMiloSpoken = true;
            StartCoroutine(BeginDialogueSequenceForMilo());
        }
    }
    
    /// <summary>
    /// Begins the dialogue sequence countdown.
    /// </summary>
    /// <returns>The dialogue sequence.</returns>
    IEnumerator BeginDialogueSequenceForKOS()
    {
        yield return new WaitForSeconds(8.6f);
        dialogues [0].enabled = true;
        StartCoroutine(DisplayTextDialogue(dialogues [0], 3.5f));
    }

    IEnumerator BeginDialogueSequenceForMilo()
    {
        yield return new WaitForSeconds(12.5f);
        dialogues [1].enabled = true;
        StartCoroutine(DisplayTextDialogue(dialogues [1], 1f));
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
    }
}
