using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour
{
    //public GameObject MainCanvas;
    //public GameObject Introim;
    //public GameObject Menu;
    public static bool chosenMilo;

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
            Application.LoadLevel("MazeLevel");
        else
            Application.LoadLevel("tutorialLevel");

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
