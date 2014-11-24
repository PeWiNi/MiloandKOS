using UnityEngine;
using System.Collections;

public class triggerPointTutorial : MonoBehaviour {
	public Texture infoPic;
	public string message;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log (message);
	}
}
