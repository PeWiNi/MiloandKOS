using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playCutscene : MonoBehaviour {

	public MovieTexture movie;
	public string nextScene;
	GameObject planeCut;

	Vector3 v3ViewPort;
	Vector3 v3BottomLeft;
	Vector3 v3TopRight;
	Text loadingPercentage;
	

	private AsyncOperation async = null; // When assigned, load is in progress.

	/*IEnumerator Loading (string level)
	{
		asyncL = Application.LoadLevelAsync (level);
		while (!asyncL.isDone)
		{
			
			loadingPercentage.text = (((int)(asyncL.progress * 100)).ToString()) + " %";
			Debug.Log(((int)(asyncL.progress * 100)));
			yield return null ;
			//StartCoroutine(Loading(levelName));
		} 
		yield return asyncL;
	}
*/
	IEnumerator Loading()
	{

		async = Application.LoadLevelAsync(SceneName);
		async.allowSceneActivation = false;
		while (!async.isDone)
		{
			loadingPercentage.text = (((int)(asyncL.progress * 100)).ToString()) + " %";
			if (async.progress >= 0.9f && !async.allowSceneActivation)
				async.allowSceneActivation = true;
			Debug.Log("I'm yielding at progress: " + async.progress);
			yield return null;
		}
		
		if (async.isDone)
		{
			loadingPercentage.text ="100%";
		}
	}


	// Use this for initialization
	void Start () {
		GameObject textDisplay = GameObject.Find ("Text");
		loadingPercentage = textDisplay.GetComponent<Text>();
		planeCut = GameObject.Find ("Plane");
		updateScreenSize();
		renderer.material.mainTexture = movie;
		movie.Play ();
	}
	
	void Update ()
	{
		updateScreenSize();
		if (movie.isPlaying & Input.GetKeyDown (KeyCode.Escape)) 
						{
						movie.Stop();
						StartCoroutine(Loading(nextScene));
		}

	 	if (!movie.isPlaying) 
		{		
			StartCoroutine(Loading(nextScene));
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
		float ratiow = 786 * (v3BottomLeft.x - v3TopRight.x) / 1024;
		float ratioh = 1024 * (v3BottomLeft.y - v3TopRight.y) / 786;
		planeCut.transform.localScale = new Vector3(ratiow, 1, ratioh - 0.5f);
	}
}
