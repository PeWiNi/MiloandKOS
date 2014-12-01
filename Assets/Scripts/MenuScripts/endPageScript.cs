using UnityEngine;
using System.Collections;

public class endPageScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Restart()
	{
		Application.LoadLevel ("IntroMenuScene");
		}

	public void Quit()
	{
		Application.Quit ();
	}

}
