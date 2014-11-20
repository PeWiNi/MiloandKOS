using UnityEngine;
using System.Collections;

public class batteryInstantiator : MonoBehaviour {
	
	public Transform batteryPrefab;

	public int [,] batteryCoord;
	
	// Use this for initialization
	void Start () {

		batteryCoord = new int[,]
		{
			{3,7}, {3,16},{4,24},{7,7},{9,2},{9,18},{12,6},{15,20},{18,16},
			{20,10},{21,20},{28,23},{30,11},{34,24},{35,5},{37,17},{40,4},{45,11}
		};
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
			GameObject batteryInstance= Instantiate(batteryPrefab, positionB, Quaternion.Euler(rotationB)) as GameObject;
			transform.parent=transform; 
		}
				
	}
				
}				