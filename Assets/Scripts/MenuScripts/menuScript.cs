using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuScript : MonoBehaviour {
	//public GameObject MainCanvas;
	//public GameObject Introim;
	//public GameObject Menu;
	bool menuShowed=false;
	bool chosenMilo;


	// Use this for initialization
	void Start () {	
		chosenMilo = true;
		StartCoroutine (delayMenu ());
	}

	IEnumerator delayMenu()
	{
		while (!menuShowed) 
		{
			yield return new WaitForSeconds (5);
			//wait a few second , then show the menu
			showMenu ();
		}
	}
	
	public void showMenu()
	{
		menuShowed = true;
		// Remove the intro pic
		// Show the menu
	}

	public void pickedMilo ()
	{ chosenMilo = true;
	}
	public void pickedKOS()
	{ chosenMilo = false;
	}

	public bool ChosenCharacter
	{
		get
		{
			return chosenMilo;
		}
	}

	public void newGame(){
		Application.LoadLevel (2);
		//Application.LoadLevel ("DogPooTest");
	}

	public void exitGame(){
		Application.Quit ();
	}

	public void optionsMenu()
	{ Application.LoadLevel ("optionMenuPage");
	}
	public void loadGame()
	{ //will follow
	}
	// Update is called once per frame
	void Update () {
		
	}
}
