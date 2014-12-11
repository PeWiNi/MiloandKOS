using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playCutscene : MonoBehaviour {

	public MovieTexture movie;
	public string nextScene;
	GameObject planeCut;
	bool coroutineStarted=false;

	Vector3 v3ViewPort;
	Vector3 v3BottomLeft;
	Vector3 v3TopRight;
	float movieRatio; 
	float screenSizeOldHeight;
	float screenSizeOldWidth;

	public Text txt;
	private AsyncOperation async = null;

	// Use this for initialization
	void Start () {

		planeCut = GameObject.Find ("Plane");
		screenSizeOldHeight = Screen.height;
		screenSizeOldWidth = Screen.width;

		renderer.material.mainTexture = movie;
		movie.Play ();
		movieRatio = (float)movie.width / (float)movie.height;
		updateScreenSize();

		GameObject temp = GameObject.Find ("Text");
		txt = temp.GetComponent<Text> ();
	}

	IEnumerator loadAsync(string sceneName)
	{
		async = Application.LoadLevelAsync(sceneName);
		async.allowSceneActivation = false;
		coroutineStarted = true;
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

	void Update ()
	{

		if (movie.isPlaying) 
			if (screenSizeOldWidth!= Screen.width || screenSizeOldHeight!= Screen.height) 
				{ 
						screenSizeOldWidth= Screen.width;
						screenSizeOldHeight= Screen.height;
						movieRatio = (float)movie.width / (float)movie.height;
						Debug.Log(movie.width+" "+movie.height); 
						updateScreenSize();
				}

		if (movie.isPlaying & Input.GetKeyDown (KeyCode.Escape)) {
					//	Debug.Log("pressed escape");
						movie.Stop();	
						StartCoroutine (loadAsync (nextScene));
				}
		else if (!movie.isPlaying & !coroutineStarted) 
				{		//Debug.Log("no playing anymore");
						StartCoroutine (loadAsync (nextScene));
				}
	}

	void updateScreenSize()
	{
		v3ViewPort.Set(0, 0, -1);
		v3BottomLeft = Camera.main.ViewportToWorldPoint(v3ViewPort);
		v3ViewPort.Set(1, 1, -1);
		v3TopRight = Camera.main.ViewportToWorldPoint(v3ViewPort);
		// float ratiow = 360 * (v3BottomLeft.x - v3TopRight.x) / 540;
		//  float ratioh = 540 * (v3BottomLeft.y - v3TopRight.y) / 360;
		/*float ratiow = 786 * (v3BottomLeft.x - v3TopRight.x) / 1024;
		float ratioh = 1024 * (v3BottomLeft.y - v3TopRight.y) / 786;
		Debug.Log ("height:" + ratioh + " width:" + ratiow);*/

		//Debug.Log ("screenRAtio:" + (float)Screen.width/ (float)Screen.height);
		Debug.Log (movieRatio);
		float newHeight = movie.height * Screen.height/Screen.width;
		newHeight=newHeight* (v3BottomLeft.y - v3TopRight.y)/newHeight;
		Debug.Log (newHeight);
		float newWidth = newHeight*movieRatio; //*(v3BottomLeft.x - v3TopRight.x)/(v3BottomLeft.y - v3TopRight.y);
		Debug.Log (newWidth);
		planeCut.transform.localScale = new Vector3(newWidth,  1, newHeight);
	//	planeCut.transform.localScale = new Vector3(ratiow, 1, ratioh - 0.5f);
	}
}
