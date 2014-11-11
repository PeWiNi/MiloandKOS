using UnityEngine;
using System.Collections;
using UnityEditor;

public class loadingImages : MonoBehaviour {

	Texture2D [] imageList;
	public string nameOfCutscene;
	public int numberOFFrameInCutscene; 
	public int currentIndex;
	public	string returnString;
	public GameObject planeCut;



	// At start, load all the images into a imageArray, unsing the counter as a indicator of which image to load
	void Start () {
		imageList=new Texture2D[numberOFFrameInCutscene+1];
		for (int i=0; i<=numberOFFrameInCutscene; i++) 
		{	
			Texture2D tempTex=Resources.Load<Texture2D>(toStringg(i));
			imageList[i]= tempTex;
		}

		currentIndex = 0;
		planeCut = GameObject.Find ("Plane");

	}


	// Update is called once per frame
	void Update () {
		if (currentIndex <= numberOFFrameInCutscene ) 
				{
					planeCut.renderer.material.mainTexture=imageList[currentIndex];	
					//planeCut.renderer.material.mainTexture=tempTex;
					currentIndex++;
						
				} else
						Application.LoadLevel ("IntroMenuScene");
	}

	public string toStringg(int num)
	{	
		returnString = nameOfCutscene+"/"+nameOfCutscene;
		int zeros = 3 ;
		int tempNum = num;
		while (num/10>0) 
		{
			zeros--;
			num/=10;
		}
		for (int i=0; i<zeros; i++) 
		{	int n=0;
			returnString+=n.ToString();
		}
		
		returnString += tempNum.ToString ();
		return returnString;
		
	}

}
