using UnityEngine;
using System.Collections;

public class batteryInstantiator : MonoBehaviour {
	
	public Transform batteryPrefab;

	public int [,] batteryCoord;
	[SerializeField] GameObject batteries;
	// Use this for initialization
	void Start () {

		batteryCoord = new int[,]
		{	{3,2},
			{3,7}, {3,13},{4,24},{5,18},{7,7},{7,13},
			{8,20},{9,2},{10,10},{11,26},{12,6},{13,11},
			{13,17},{14,2},{15,21},{16,10},{16,16},
			{18,5},{18,28},{19,18},{21,9},{22,17},
			{22,24},{23,11},{25,17},{27,9},{28,17},
			{29,24},{30,11},{32,3},{33,13},{33,19},
			{34,24},{35,8},{37,17},{38,5},{38,12},
			{41,8},{41,14},{41,21},{42,2},{44,17},
			{45,11},{45,28}
		};
		batteries = GameObject.Find ("Batteries");
		drawBatteries();
		
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
	string ToString (float x, float y)
	{
		return x + " " + y;
	}
				
}				