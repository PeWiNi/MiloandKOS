using UnityEngine;
using System.Collections;

public class batteryInstantiator : MonoBehaviour {
	
	public Transform batteryPrefab;
	public Transform trashCanLidPrefab;

	public int [,] batteryCoord;
	[SerializeField] GameObject batteries;
	[SerializeField] GameObject lids;
	public int [,] lidsCoords; 
	// Use this for initialization
	void Start () {

		batteryCoord = new int[,]
		{	{3,2},
			{3,9},{4,24},{5,18},{7,7},{7,13},
			{8,20},{9,2},{10,10},{11,26},{12,6},{13,11},
			{13,17},{14,2},{15,21},{16,10},{16,16},
			{18,5},{18,28},{19,18},{21,9},{22,17},
			{22,24},{23,11},{25,17},{27,9},{28,17},
			{29,24},{30,11},{32,3},{33,13},{33,19},
			{34,24},{35,8},{37,17},{38,5},{38,12},
			{41,8},{41,14},{41,21},{42,2},{44,17},
			{45,11},{45,28}
		};


		lidsCoords = new int[,]
		{
			{2,10},{4,14},{5,3},{5,6},{5,12},
			{5,27},{6,19},{11,3},{11,8},{11,13},
			{12,14},{13,22},{14,6},{14,10},{15,18},
			{17,3},{17,10},{17,21},{19,9},{19,29},
			{20,18},{22,7},{23,21},{25,9},{26,4},
			{26,4},{26,18},{29,14},{29,21},{32,14},
			{33,3},{33,6},{33,12},{33,27},{34,19},
			{35,22},{39,4},{39,8},{39,13},{40,14},
			{41,22},{42,6},{42,10},{43,17},{45,3},
			{45,10},{45,21}
		};


		batteries = GameObject.Find ("Batteries");
		lids = GameObject.Find ("Lids");
		drawBatteries();
		drawLids ();
	}
	
	void drawBatteries()
	{
		Vector3 positionB=new Vector3();
		positionB.y = 0.1f;

		for (int i=0; i<batteryCoord.GetLength(0); i++) 
		{
			positionB.x=(float)(batteryCoord[i,0]-1)*1.5f;
			positionB.z=(float)(batteryCoord[i,1]-1)*2.5f;
			Vector3 rotationB = new Vector3();
			rotationB.x=0.0f;
			rotationB.y=((float)i%3.0f)*90.0f;
			rotationB.z=90.0f;
			Transform batteryInstance= Instantiate(batteryPrefab, positionB, Quaternion.Euler(rotationB)) as Transform;
			batteryInstance.transform.parent=batteries.transform;
			batteryInstance.name=ToString(batteryCoord[i,0],batteryCoord[i,1]);
		}
				
	}

	void drawLids ()
	{

		//maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (canPrefab, position, Quaternion.identity) as Transform;	
		//maze[(int)i-countMazeI,(int)j-countMazeJ].name = "can " + ToString (i, j);

		Vector3 positionL=new Vector3();
		positionL.y = 0;
		
		for (int i=0; i<lidsCoords.GetLength(0); i++) 
		{
			positionL.x=(float)(lidsCoords[i,0]-1)*1.5f;
			positionL.z=(float)(lidsCoords[i,1]-1)*2.5f;
			Transform lidInstance = Instantiate(trashCanLidPrefab, positionL, Quaternion.identity) as Transform;
			lidInstance.transform.parent=lids.transform;
			lidInstance.name=ToString(lidsCoords[i,0],lidsCoords[i,1]);
		}
			
	}
	string ToString (float x, float y)
	{
		return x + " " + y;
	}
				
}				