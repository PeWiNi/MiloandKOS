using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerMomDialogeTutorialLevel : MonoBehaviour
{
    [SerializeField]
    Text
        momDialogueTutorialLevel;
    bool hasBeenTriggered;

    // Use this for initialization
    void Start()
    {
	
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Milo" && !hasBeenTriggered)
        {
            hasBeenTriggered = true;
            momDialogueTutorialLevel.enabled = true;
            StartCoroutine(FadeText(momDialogueTutorialLevel));
        }
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
}
