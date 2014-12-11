using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroCutsceneDialogues : MonoBehaviour
{

    [SerializeField] 
    Text[]
        dialogues;//Teacher [0], Unkown [1], Milo [2], KOS [3], Classmates [5]
    bool teacherHasSpoken;
    bool unkownHasSpoken;
    bool miloHasSpoken;
    bool kosHasSpoken;

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
        if (!teacherHasSpoken)
        {
            isPlaying = true;
            teacherHasSpoken = true;
            dialogues [0].enabled = true;
            StartCoroutine(FadeText(dialogues [0], 7f));
        } else if (teacherHasSpoken && !unkownHasSpoken && !isPlaying)
        {
            isPlaying = true;
            unkownHasSpoken = true;
            dialogues [1].enabled = true;
            StartCoroutine(FadeText(dialogues [1], 5f));
        } else if (unkownHasSpoken && !miloHasSpoken && !isPlaying)
        {
            isPlaying = true;
            miloHasSpoken = true;
            dialogues [2].enabled = true;
            StartCoroutine(FadeText(dialogues [2], 1f));
        } else if (miloHasSpoken && !kosHasSpoken && !isPlaying)
        {
            isPlaying = true;
            kosHasSpoken = true;
            dialogues [3].enabled = true;
            StartCoroutine(FadeText(dialogues [3], 8f));
        }
    }

    /// <summary>
    /// Begins the dialogue sequence countdown.
    /// </summary>
    /// <returns>The dialogue sequence.</returns>
    IEnumerator BeginDialogueSequence()
    {
        yield return new WaitForSeconds(10f);
        hasBeenInitiated = true;
    }

    /// <summary>
    /// Fades the text.
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="text">Text.</param>
    IEnumerator FadeText(Text text, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        text.enabled = false;
        if (!teacherHasSpoken)
        {
            StartCoroutine(Wait(2f));
        } else if (teacherHasSpoken && !unkownHasSpoken)
        {
            StartCoroutine(Wait(3f));
        } else if (unkownHasSpoken && !miloHasSpoken)
        {
            Debug.Log("Milo has spoken");
            isPlaying = false;// No coroutine, becuase immediate reaction from Milo.
        } else if (miloHasSpoken && !kosHasSpoken)
        {
            StartCoroutine(Wait(2f));
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
