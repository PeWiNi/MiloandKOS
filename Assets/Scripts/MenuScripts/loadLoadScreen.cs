using UnityEngine;
using System.Collections;

public class loadLoadScreen : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}

	public static void loadLevel(string sceneName)
	{
		LoadingScreen46.show();
		Application.LoadLevel(sceneName);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
