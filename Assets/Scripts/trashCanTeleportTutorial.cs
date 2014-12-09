using UnityEngine;
using System.Collections;

public class trashCanTeleportTutorial : MonoBehaviour {
		
		public Texture infoPic;
		Texture2D frame;
		public string message;
		public static bool showPic = false;
		public static bool picShowed = false;
		//Texture2D background ;
		
		float width, height;
		
		
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
			width = infoPic.width / 3;
			height = infoPic.height / 3;
			
			if (showPic && !picShowed) {
				GUI.DrawTexture (new Rect (Screen.width /2-width/2, Screen.height - Screen.height / 3, width, height), frame);
				GUI.DrawTexture (new Rect (Screen.width /2-width/2, Screen.height - Screen.height / 3, width, height), infoPic);
				
			}
		}
		
		void OnTriggerEnter (Collider col)
		{	
			if (col.gameObject.name == "KOSMinotaur") {
						showPic = true;
				}
			//Debug.Log (message);
		}
		
		void OnTriggerExit (Collider col)
		{
			if (col.gameObject.name == "KOSMinotaur") {
						showPic = false;
						picShowed = true;
				}
			//Debug.Log (message);
		}
	}
