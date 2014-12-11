using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitMazeEnterShipAsMiloDialogues : MonoBehaviour
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
        if (!hasKOSSpoken)
        {
            isPlaying = true;
            hasKOSSpoken = true;
            dialogues [1].enabled = true;
            StartCoroutine(DisplayTextDialogue(dialogues [1], 5f));
        } else if (hasKOSSpoken && !isPlaying)
        {
            isPlaying = true;
            hasMiloSpoken = true;
            dialogues [0].enabled = true;
            StartCoroutine(DisplayTextDialogue(dialogues [0], 1f));
        }
    }
    
    /// <summary>
    /// Begins the dialogue sequence countdown.
    /// </summary>
    /// <returns>The dialogue sequence.</returns>
    IEnumerator BeginDialogueSequence()
    {
        yield return new WaitForSeconds(1.5f);
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
        if (hasKOSSpoken && !hasMiloSpoken)
        {
            StartCoroutine(Wait(0.5f));
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
