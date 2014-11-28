using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalbattleTextDialogue : MonoBehaviour
{

    [SerializeField]
    Text
        kosPreBattleIfMilo;
    [SerializeField]
    Text
        miloPreBattleIfMilo;
    [SerializeField]
    Text
        kosPreBattleIfKOS;
    [SerializeField]
    Text
        miloPreBattleIfKOS;
    bool hasPlayedKOSIfMilo = false;
    bool hasPlayedKOSIfKOS = false;
    bool hasPlayedMiloIfKOS = false;
    bool hasPlayedMiloIfMilo = false;
    bool isPlaying = false;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
        if (!StateController.INSTANCE.HasDialoguesBeenStarted)
        {
            StartDialogues();
        }
    }

    void StartDialogues()
    {
        if (Application.loadedLevel == 7)//As Milo.
        {
            if (!hasPlayedKOSIfMilo)
            {
                isPlaying = true;
                hasPlayedKOSIfMilo = true;
                kosPreBattleIfMilo.enabled = true;
                StartCoroutine(FadeText(kosPreBattleIfMilo, true));
            } else if (hasPlayedKOSIfMilo && !hasPlayedMiloIfMilo && !isPlaying)
            {
                hasPlayedMiloIfMilo = true;
                miloPreBattleIfMilo.enabled = true;
                StartCoroutine(FadeText(miloPreBattleIfMilo, false));
            }
        } else if (Application.loadedLevel == 8)//As KOS.
        {
            if (!hasPlayedMiloIfKOS)
            {
                isPlaying = true;
                hasPlayedMiloIfKOS = true;
                miloPreBattleIfKOS.enabled = true;
                StartCoroutine(FadeText(miloPreBattleIfKOS, true));
            } else if (hasPlayedMiloIfKOS && !hasPlayedKOSIfKOS && !isPlaying)
            {
                hasPlayedKOSIfKOS = true;
                kosPreBattleIfKOS.enabled = true;
                StartCoroutine(FadeText(kosPreBattleIfKOS, false));
            }
        }
    }

    /// <summary>
    /// Fades the text.
    /// </summary>
    /// <returns>The text.</returns>
    /// <param name="text">Text.</param>
    /// <param name="value">If set to <c>true</c> value.</param>
    IEnumerator FadeText(Text text, bool dialoguesInProgress)
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
        isPlaying = false;
        if (!dialoguesInProgress)
        {
            StateController.INSTANCE.HasDialoguesBeenStarted = true;
        }
    }
}
