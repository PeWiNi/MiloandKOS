using UnityEngine;
using System.Collections;

public class triggerPointTutorial : MonoBehaviour
{
		public Texture infoPic;
		Texture2D frame;
		public string message;
		bool showPic = false;
		//Texture2D background ;


		// Use this for initialization
		void Start ()
		{
			frame = Resources.Load<Texture2D> ("frame");
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{
				if (showPic) {
						GUI.DrawTexture (new Rect (Screen.width /2-131.25f, Screen.height - Screen.height / 3, 262.5f, 165f), frame);
						GUI.DrawTexture (new Rect (Screen.width /2-131.25f, Screen.height - Screen.height / 3, 262.5f, 165f), infoPic);
				}
		}

		void OnTriggerEnter (Collider col)
		{	
				showPic = true;
				//Debug.Log (message);
		}

		void OnTriggerExit (Collider col)
		{
				showPic = false;
				//Debug.Log (message);
		}
}
