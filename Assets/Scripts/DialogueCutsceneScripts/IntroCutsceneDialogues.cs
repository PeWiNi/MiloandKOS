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
    bool classMatesHasSpoken;

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

    void CheckSequence()
    {
        if (!teacherHasSpoken)
        {
            isPlaying = true;
            teacherHasSpoken = true;
            dialogues [0].enabled = true;
            StartCoroutine(FadeText(dialogues [0]));
        } else if (teacherHasSpoken && !unkownHasSpoken && !isPlaying)
        {
            isPlaying = true;
            unkownHasSpoken = true;
            dialogues [1].enabled = true;
            StartCoroutine(FadeText(dialogues [1]));
        } else if (unkownHasSpoken && !miloHasSpoken && !isPlaying)
        {
            isPlaying = true;
            miloHasSpoken = true;
            dialogues [2].enabled = true;
            StartCoroutine(FadeText(dialogues [2]));
        } else if (miloHasSpoken && !kosHasSpoken && !isPlaying)
        {
            isPlaying = true;
            kosHasSpoken = true;
            dialogues [3].enabled = true;
            StartCoroutine(FadeText(dialogues [3]));
        } else if (kosHasSpoken && !classMatesHasSpoken && !isPlaying)
        {
            isPlaying = true;
            classMatesHasSpoken = true;
            dialogues [4].enabled = true;
            StartCoroutine(FadeText(dialogues [4]));
        }
    }

    IEnumerator BeginDialogueSequence()
    {
        yield return new WaitForSeconds(10f);
        hasBeenInitiated = true;
    }

    IEnumerator FadeText(Text text)
    {
        yield return new WaitForSeconds(3f);
//        Color color;
//        while (text.color.a > 0.0f)
//        {
//            color.a = text.color.a - 0.01f;
//            color.r = text.color.r;
//            color.g = text.color.g;
//            color.b = text.color.b;
//            text.color = color;
//            yield return new WaitForSeconds(0.1f);
//        }
//        color.a = 1.0f;
//        color.r = text.color.r;
//        color.g = text.color.g;
//        color.b = text.color.b;
//        text.color = color;
        text.enabled = false;
        isPlaying = false;
    }
}
