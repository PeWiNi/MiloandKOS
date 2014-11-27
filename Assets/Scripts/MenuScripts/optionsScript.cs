using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class optionsScript : MonoBehaviour {

	Text textDisplay;


	void Start()
	{	
		GameObject text = GameObject.Find ("TextBox");
		textDisplay = text.GetComponent<Text> ();
	}

	// Use this for initialization
	public void instructions()
	{
			textDisplay.text = " You have to help Milo get home from school.  \n" +
						" You have to make sure he is not caught by shadows and reach the end of his journey. \n" +
						" If he does...you'll see how darkness attempts to prevail. \n \n" +
						" Do your best ! ";
	}

	public void controls ()
	{
		textDisplay.text = 
						" Forwards\t: 'W' or the up arrow \n" +
						" Backwards\t: 'S' or the down arrow \n" +
						" Left\t: 'A' or the left arrow \n" +
						" Right\t: 'D' or the right arrow \n"+
						" Flip camera\t: 'F' \n \n \n"+
						" Special Ability\t: 'Space' or 'T' depending on case" ;

	}

	public void goBack()
	{
		Application.LoadLevel ("IntroMenuScene");
	}
}
