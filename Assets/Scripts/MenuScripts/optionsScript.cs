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
						" Do your best ! \n \n" +
						" Tips: \n" +
						"1. Milo can jump over things and can stay in the light for as long as he need" +
						"\n2. Should you become a servant of darkness, your goal will be to keep Milo \n" +
						"under your control using the flowers you can find inside the park.";
	}

	public void controls ()
	{
		textDisplay.text = "To move: \n"+
						" Forwards: 'W' or the up arrow \n" +
						" Backwards: 'S' or the down arrow \n" +
						" Left: 'A' or the left arrow \n" +
						" Right: 'D' or the right arrow \n \n"+
						//" Flip camera\t: 'F' \n \n \n"+
						" Special Ability \n 'Space' or 'T' depending on case" ;

	}

	public void goBack()
	{
		Application.LoadLevel ("IntroMenuScene");
	}
}
