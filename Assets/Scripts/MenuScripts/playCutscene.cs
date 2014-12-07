using UnityEngine;
using System.Collections;

public class playCutscene : MonoBehaviour {

	public MovieTexture movie;
	public string nextScene;
	GameObject planeCut;

	Vector3 v3ViewPort;
	Vector3 v3BottomLeft;
	Vector3 v3TopRight;


	// Use this for initialization
	void Start () {

		planeCut = GameObject.Find ("Plane");
		updateScreenSize();
		renderer.material.mainTexture = movie;
		movie.Play ();
	}
	
	void Update ()
	{
		updateScreenSize();
		if (movie.isPlaying & Input.GetKeyDown (KeyCode.Escape)) {
					//	Debug.Log("pressed escape");
						movie.Stop();	
						Application.LoadLevel (nextScene);
				}
	 	if (!movie.isPlaying) 
		{		//Debug.Log("no playing anymore");
						Application.LoadLevel (nextScene);
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
