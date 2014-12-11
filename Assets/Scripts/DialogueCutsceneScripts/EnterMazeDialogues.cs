using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterMazeDialogues : MonoBehaviour
{
    [SerializeField]
    Text
        kos;
    bool isPlaying;
    bool hasBeenInitiated;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!hasBeenInitiated)
        {
            KOSChasingMiloIntoMaze();
        } 
    }

    /// <summary>
    /// Koses the chasing milo into maze.
    /// </summary>
    void KOSChasingMiloIntoMaze()
    {
        hasBeenInitiated = true;
        StartCoroutine(Wait(12f));
    }

    /// <summary>
    /// Fades the text.
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="text">Text.</param>
    /// <param name="seconds">Seconds.</param>
    IEnumerator FadeText(Text text, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        text.enabled = false; 
    }

    /// <summary>
    /// Wait the specified amountBetweenDialogues.
    /// </summary>
    /// <param name="amountBetweenDialogues">Amount between dialogues.</param>
    IEnumerator Wait(float amountBetweenDialogues)
    {
        yield return new WaitForSeconds(amountBetweenDialogues);
        kos.enabled = true;
        StartCoroutine(FadeText(kos, 5.5f));
    }
}
