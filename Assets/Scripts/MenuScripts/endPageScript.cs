using UnityEngine;
using System.Collections;

public class endPageScript : MonoBehaviour {

	public static bool reachedEnd;

	// Use this for initialization
	void Start () {
		reachedEnd = true;
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
