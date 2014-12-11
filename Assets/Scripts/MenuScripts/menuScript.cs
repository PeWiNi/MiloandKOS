using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour
{
    public static bool chosenMilo;
	public Text txt;
	private AsyncOperation async = null;

    // Use this for initialization
    void Start()
    {	
        chosenMilo = true;
        if (endPageScript.reachedEnd)
        {
            GameObject findQPic = GameObject.Find("Image");
            Image QPic = findQPic.GetComponent<Image>();
            QPic.enabled = false;
        }
		GameObject temp = GameObject.Find ("Text");
		txt = temp.GetComponent<Text> ();
    }

	IEnumerator loadAsync(string sceneName)
	{
		async = Application.LoadLevelAsync(sceneName);
		async.allowSceneActivation = false;
		while (!async.isDone)
		{
			txt.text = (((int)(async.progress * 100)).ToString()) + " %";
			if (async.progress >= 0.9f && !async.allowSceneActivation)
				async.allowSceneActivation = true;
			//Debug.Log("I'm yielding at progress: " + async.progress);
			yield return null;
		}
		
		if (async.isDone)
		{
			txt.text ="100%";
		}
	}

    public void pickedMilo()
    { 
		GameObject seeIfKostoggled = GameObject.Find ("kos");
		Toggle turnKos = seeIfKostoggled.GetComponent<Toggle> ();
		turnKos.isOn = false;
        chosenMilo = true;
    }

    public void pickedKOS()
    { 
        if (endPageScript.reachedEnd) 
		{ 	GameObject seeIfMilotoggled = GameObject.Find ("milo");
			Toggle turnMilo = seeIfMilotoggled.GetComponent<Toggle> ();
			turnMilo.isOn = false;
			chosenMilo = false;
		}
    }

    public static bool ChosenCharacter
    {
        get
        {
            return chosenMilo;
        }
    }



    public void newGame()
    {
        if (!chosenMilo)
			StartCoroutine (loadAsync ("MazeLevel"));
        else
			StartCoroutine (loadAsync ("tutorialLevel"));

        //Application.LoadLevel ("DogPooTest");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void optionsMenu()
    {
        Application.LoadLevel("optionMenuPage");
    }

    public void loadGame()
    { //will follow
    }
}
