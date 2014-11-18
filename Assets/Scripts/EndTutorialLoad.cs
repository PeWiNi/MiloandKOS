using UnityEngine;
using System.Collections;

public class EndTutorialLoad : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.gameObject.name == "Milo")
		{
			Application.LoadLevel(3);
		}
	}
}
