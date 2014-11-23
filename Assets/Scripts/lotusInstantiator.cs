using UnityEngine;
using System.Collections;

public class lotusInstantiator : MonoBehaviour {

	public Transform lotusPrefab;
	public int [,] lotusMap;
	[SerializeField] GameObject lotusFlowers;

	void Start () {

		lotusMap = new int[,] 
		{
			{2,9}, {5,3},{9,17},{10,11},{11,26},
			{15,2},{18,11},{18,28},{22,22},{25,5},
			{25,20},{32,2},{35,12},{38,21},{45,5}
		};
		lotusFlowers = GameObject.Find ("LotusFlowers");
		drawLotus ();
	
	}

	void drawLotus()
	{
		Vector3 positionB;
		positionB.y = 0.2F;
		
		for (int i=0; i<lotusMap.GetLength(0); i++) {

			positionB.x=(float)(lotusMap[i,0]-1)*1.5f+0.4f;
			positionB.z=(float)(lotusMap[i,1]-1)*2.5f-0.4f;
			Transform lotusInstance= Instantiate(lotusPrefab, positionB, Quaternion.identity) as Transform;
			lotusInstance.transform.parent=lotusFlowers.transform; 

		}
		
		
	}


	// Update is called once per frame
	void Update () {
	
	}
}
