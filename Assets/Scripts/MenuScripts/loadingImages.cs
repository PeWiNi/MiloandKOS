using UnityEngine;
using System.Collections;

public class loadingImages : MonoBehaviour {

	GameObject [] imageList;
	public string nameOfCutscene;
	public int numberOFFrameInCutscene; 
	int currentIndex;
	public	string returnString;

	// Use this for initialization
	void Start () {
		for (int i=0;i<numberOFFrameInCutscene;i++)
		imageList[i] = GameObject.Find (nameOfCutscene + toStringg(i));

	}

	public string toStringg(int num)
	{	 
		int zeros = 0;
		while (num/10>=9) 
		{
			zeros++;
			num/=10;
		}
		for (int i=0; i<zeros; i++) 
		{	int n=0;
			returnString+=n.ToString();
		}
		returnString += num.ToString ();
		return returnString;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
