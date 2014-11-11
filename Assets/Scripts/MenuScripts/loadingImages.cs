using UnityEngine;
using System.Collections;
using UnityEditor;

public class loadingImages : MonoBehaviour {

	Texture2D [] imageList;
	public string nameOfCutscene;
	public int numberOFFrameInCutscene; 
	public int currentIndex;
	public	string returnString;
	public LoadingScreen46 loadPanel;
	public string folderName="Resources";
	//public static loadingImages instance;
	public GameObject planeCut;



	// At start, load all the images into a imageArray, unsing the counter as a indicator of which image to load
	void Start () {
		//instance = this;
		imageList=new Texture2D[numberOFFrameInCutscene+1];
		for (int i=0; i<=numberOFFrameInCutscene; i++) 
		{	
			Texture2D tempTex=Resources.Load<Texture2D>(toStringg(i));
			//GameObject go= Resources.LoadAssetAtPath<GameObject>(toStringg(i));
			//Debug.Log(go);
			imageList[i]= tempTex;
		}
		currentIndex = 0;
		//loadPanel = new LoadingScreen46 ();
		//LoadingScreen46.show ();

		//var matrix = Matrix4x4.TRS (offset, Quaternion.Euler (0, 0, rot), tiling);
	//	renderer.material.SetMatrix ("_Matrix", matrix);

		planeCut = GameObject.Find ("Plane");

	
	//	Texture2D tex=Resources.Load<Texture2D>("IntroSceneVideo/IntroSceneVideo0000");
		//Debug.Log (planeCut);
		//Debug.Log (tex);

	}


	// Update is called once per frame
	void Update () {
		if (currentIndex <= numberOFFrameInCutscene ) {
					
					planeCut.renderer.material.mainTexture=imageList[currentIndex];			
						//loadPanel.loadingScreenImage = imageList[currentIndex]; 
						//GameObject currentPic = Instantiate(imageList[currentIndex]) as GameObject;
						//currentPic.SetActive(true);
						//instance.imageList[currentIndex].SetActive(true);
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
	//	returnString += ".jpg";
		return returnString;
		
	}

}
