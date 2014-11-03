using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AwakeBar : MonoBehaviour {

	public Slider slider;

	public GameController currentState;

	// Use this for initialization
	void Start() {

		slider = GameObject.Find("Slider").GetComponent<Slider> ();
		slider.interactable = false;
		slider.minValue = 0;
		slider.maxValue = 15;
		slider.wholeNumbers = true;
		slider.value = 0;

		currentState=GameObject.Find("GameController").GetComponent<GameController>();



	}
	
	// Update is called once per frame
	void Update () {

		//slider.value = Mathf.MoveTowards (slider.value, slider.maxValue, 1);

		//the row below requires access from the Game controller script to the MiloAwakeTimer 
		// through an AwakeTimer property(needs only get) to show the true value of the AwakeBar

		slider.value = currentState.AwakeTimer;

	
	}
}
