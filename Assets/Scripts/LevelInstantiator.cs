using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class LevelInstantiator : MonoBehaviour {
	
	public Transform treePrefab;
	public Transform benchPrefab;
	public Transform lampPrefab;
	public Transform canPrefab;
	public Transform cornerTreePrefab; 
	[SerializeField] GameObject parentMaze;
	GameObject player;
	public Transform[,] maze;
	float pastPlayerPosX;
	float pastPlayerPosZ;
	int [] limits = new int[4];

	public string [,] levelMap;
	
	void Start (){
		
		levelMap= new string[,]{
			{"g","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","t","-","-","-","y","#","#"},
			{"0","2","g","-","2","-","-","-","s","4","-","-","-","-","-","-","h","2","y","#","#","#","#","#","|","h","0","0","0","0","|","#","#"},
			{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","|","#","#","#","t","-","s","0","0","t","-","-","h","#","#"},
			{"y","0","d","y","0","d","t","-","-","-","-","y","0","4","3","t","y","0","|","#","#","#","|","0","0","0","0","|","#","#","#","#","#"},
			{"|","0","4","|","0","4","#","#","#","#","#","4","0","t","#","#","a","0","g","-","-","-","h","0","2","3","4","h","#","#","#","#","#"},
			{"|","0","t","|","0","g","-","-","-","-","h","a","0","|","#","#","g","-","4","0","0","0","0","0","t","#","#","#","#","#","#","#","#"},
			{"|","0","|","|","0","0","0","0","0","0","0","0","0","g","-","#","#","#","a","0","0","2","3","0","|","#","#","#","#","#","#","#","#"},
			{"|","0","|","y","3","t","y","3","t","-","-","y","0","2","y","t","-","-","h","0","0","d","y","0","|","#","#","#","#","#","#","#","#"},
			{"|","0","g","g","-","-","-","-","-","-","-","h","0","t","h","|","0","0","0","0","0","t","|","0","g","#","#","#","#","#","#","#","#"},
			{"|","0","0","d","0","0","0","0","0","0","0","0","0","|","#","|","0","0","0","0","0","|","|","0","0","g","s","-","-","y","#","#","#"},
			{"g","y","4","t","y","0","2","4","3","t","-","y","4","h","-","|","0","0","t","a","0","t","|","0","0","0","0","0","0","|","#","#","#"},
			{"t","-","-","-","h","0","g","-","-","-","-","h","s","4","g","h","0","0","|","y","0","d","g","-","-","3","-","-","-","h","#","#","#"},
			{"2","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","|","|","0","4","-","#","#","#","#","#","#","#","#","#","#"},
			{"y","0","t","y","0","4","t","-","y","4","3","t","-","y","3","2","t","-","h","|","0","g","-","y","#","#","#","#","#","#","#","#","#"},
			{"|","0","|","|","0","t","h","-","h","s","2","g","-","-","-","-","h","4","g","h","0","0","0","|","#","#","#","#","#","#","#","#","#"},
			{"|","0","g","|","0","|","|","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","g","s","2","-","-","y","#","#","#","#"},
			{"|","3","4","|","0","|","|","0","t","4","0","t","-","-","y","3","t","-","-","y","4","3","0","0","0","0","0","0","|","#","#","#","#"},
			{"g","-","h","|","0","|","|","0","g","y","0","|","y","g","-","-","-","-","-","g","-","y","0","0","0","0","0","0","g","#","#","#","#"},
			{"#","#","t","|","0","|","g","3","4","|","0","|","|","0","0","0","0","0","0","0","2","t","y","3","t","-","y","3","4","#","#","#","#"},
			{"#","#","|","h","0","g","-","-","-","h","0","|","|","0","t","-","y","4","0","0","t","h","g","-","h","#","#","#","#","#","#","#","#"},
			{"#","#","|","2","0","0","0","0","0","0","0","g","|","0","|","h","g","h","0","0","g","-","-","-","-","#","#","#","#","#","#","#","#"},
			{"#","#","g","-","-","y","4","3","t","y","0","d","g","3","g","s","0","0","0","0","0","0","0","0","g","#","#","#","#","#","#","#","#"},
			{"#","#","#","t","-","y","g","-","h","|","0","0","0","0","0","0","0","t","-","y","4","t","y","0","0","s","g","-","y","#","#","#","#"},	
			{"#","#","#","|","0","|","|","#","|","|","0","t","-","-","-","-","-","-","-","-","-","y","|","0","0","0","0","0","|","#","#","#","#"},
			{"#","#","#","h","0","|","|","3","4","|","0","|","t","0","0","0","0","0","0","0","2","g","h","3","t","-","-","-","h","#","#","#","#"},
			{"#","#","#","4","0","g","-","-","-","h","0","g","|","0","t","-","y","4","0","0","t","-","-","-","-","#","#","#","#","#","#","#","#"},
			{"#","#","#","2","0","0","0","0","0","0","0","2","|","0","|","t","g","h","0","0","g","-","-","-","y","#","#","#","#","#","#","#","#"},
			{"#","#","#","g","-","-","y","3","t","y","0","t","t","3","y","|","0","0","0","0","0","0","0","0","g","#","#","#","#","#","#","#","#"},
			{"#","#","#","#","#","#","#","#","#","|","0","g","h","4","g","h","0","t","-","y","4","3","0","0","0","s","-","y","#","#","#","#","#"},
			{"#","#","#","#","#","#","#","#","#","|","0","0","0","0","0","0","0","d","#","g","-","|","0","0","0","0","0","|","#","#","#","#","#"},
			{"t","-","-","-","-","g","#","#","#","g","-","y","0","3","t","y","0","g","#","#","#","g","-","-","-","-","-","h","#","#","#","#","#"},
			{"|","0","0","0","0","d","t","-","-","-","t","h","0","4","3","|","0","0","y","-","g","#","t","-","-","-","y","#","#","#","#","#","#"},
			{"|","0","4","y","0","4","#","#","#","#","|","4","0","t","#","|","a","0","0","0","y","-","h","0","0","0","4","#","#","#","#","#","#"},
			{"|","0","t","h","0","g","-","-","-","-","h","a","0","|","#","g","-","y","4","0","0","0","0","0","2","3","h","#","#","#","#","#","#"},
			{"|","0","|","2","0","0","0","0","0","0","0","0","0","g","y","#","#","#","a","0","0","4","3","0","t","#","#","#","#","#","#","#","#"},
			{"|","0","|","|","3","t","y","3","t","-","-","y","0","2","|","g","-","-","h","0","0","d","|","0","|","#","#","#","#","#","#","#","#"},
			{"|","0","g","-","-","-","-","-","-","-","-","h","0","t","h","|","0","0","0","0","2","|","|","0","g","#","#","#","#","#","#","#","#"},
			{"|","0","|","d","0","0","0","0","0","0","0","0","0","|","#","|","0","0","0","0","0","|","|","0","0","g","s","-","y","#","#","#","#"},
			{"g","-","h","4","0","0","2","4","3","t","-","-","4","h","-","|","0","0","t","a","0","t","|","0","0","0","0","0","|","#","#","#","#"},
			{"t","-","h","0","0","0","g","-","-","-","-","-","s","4","-","h","0","0","|","y","0","d","g","-","-","3","-","-","h","#","#","#","#"},
			{"2","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","|","|","0","4","-","#","#","#","#","#","#","#","#","#","#"},
			{"g","0","t","y","0","4","-","-","-","4","3","-","-","y","3","t","-","-","h","|","0","g","-","y","#","#","#","#","#","#","#","#","#"},
			{"|","0","|","|","0","|","t","-","h","s","2","g","-","g","-","h","4","g","-","h","0","0","0","|","#","#","#","#","#","#","#","#","#"},
			{"|","0","g","|","0","|","|","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","g","s","2","-","-","y","#","#","#","#"},
			{"|","3","4","|","0","|","|","0","t","4","0","t","-","-","t","3","y","-","-","y","4","3","0","0","0","0","0","0","|","#","#","#","#"},
			{"g","-","h","g","-","h","g","-","h","g","-","h","-","-","-","-","-","-","-","-","-","-","-","-","-","-","t","-","h","#","#","#","#"},
			{"-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","-","#","#"}
			
		};
		parentMaze = GameObject.Find ("mazeLayoutloader");
		maze = new Transform[levelMap.GetLength (0), levelMap.GetLength (1)];
		//Debug.Log (maze);

		drawLevel (0,levelMap.GetLength(0),0,levelMap.GetLength(1));

		if (GameController.INSTANCE.IsPlayingAsMilo) 
			player = GameObject.Find ("Milo");
		else
			player = GameObject.Find ("KOSMinotaur");
		pastPlayerPosX = player.transform.position.x;
		pastPlayerPosZ = player.transform.position.z;

	}
	
	// Update is called once per frame

		 void Update ()
		{	
		if (GameController.INSTANCE.IsPlayingAsMilo) 
						player = GameObject.Find ("Milo");
				else
						player = GameObject.Find ("KOSMinotaur");
				
			//mazeHider ((int)pastPlayerPosX, (int)pastPlayerPosZ);
			//mazeShower((int)player.transform.position.x,(int)player.transform.position.z);

				pastPlayerPosX = player.transform.position.x;
				pastPlayerPosZ = player.transform.position.z;
				
		}

	void mazeHider(int oldX, int oldZ)
	{	setBoundaries (oldX,oldZ);
		for (int i=limits[0]; i<limits[1]; i++)
			for (int j=limits[2]; j<limits[3]; j++)
				if (maze [i,j]!=null)
					maze [i,j].gameObject.renderer.enabled = false;
	}
	
	void mazeShower(int x, int z){

		setBoundaries (x,z);
		for (int i=limits[0]; i<limits[1]; i++)
			for (int j=limits[2]; j<limits[3]; j++)
				if (maze [i,j]!=null)
				maze [i, j].renderer.enabled = true;
		}

	void setBoundaries(int x, int z)
	{	

		if (x - 4 < 0)
			limits[0] = 0;
		else
			limits[0] = x - 4;
		if (x + 4 > levelMap.GetLength (0))
			limits[1]= levelMap.GetLength (0);
		else
				limits[1] = x + 4;
		if (z - 4 < 0) 
				limits[2] = 0+z;
		else
				limits[2] = z - 4;
		if (z + 4 > levelMap.GetLength (1))
				limits[3] = levelMap.GetLength (1);
		else
				limits[3] = z + 4;
		
	}
	
	/// <summary>
	/// Draws the level, using a character map : 
	/// 0 - empty space, 
	/// 1 - tree, 
	/// 2 - street lamp, 	
	/// 4 - trash can
	/// g,h,t,y - corner trees
	/// a,s,d,3 - benches are no longer instantiated 
	/// </summary>
	void drawLevel (int xmin, int xmax, int zmin, int zmax)
	{	
		Vector3 position = new Vector3 ();
		position.y = 0f;
		Vector3 rotation = new Vector3 ();
		rotation.z = 0f;
		rotation.x = 0f;
		
		int countMazeI = 0;
		int countMazeJ = 0;
		for (int i=xmin; i<xmax; i++) { 	
			for (int j=zmin; j<zmax; j++) {
				position.x = (float)i * 1.5f;
				position.z = (float)j * 2.5f;
				rotation.y = 0.0f;
				switch (levelMap [i, j]) {
				case "-":
					position.x = i * 1.5f - (1.0f - Random.Range (0.5f, 1.0f));
					position.z = j * 2.5f - (2.0f - Random.Range (1.5f, 2.0f));
					maze[(int)i-countMazeI,(int)j-countMazeJ]= Instantiate (treePrefab, position, Quaternion.identity) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "htree " + ToString (i, j);
					break;			
				case "2":
					maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (lampPrefab, position, Quaternion.identity) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "lamp " + ToString (i, j);
					break;
				case "4":
					maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (canPrefab, position, Quaternion.identity) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "can " + ToString (i, j);
					break;
				case "|":
					position.x = i * 1.5f - (1.0f - Random.Range (0.5f, 1.0f));
					position.z = j * 2.5f - (2.0f - Random.Range (1.5f, 2.0f));
					rotation.y = 90.0f;
					maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (treePrefab, position, Quaternion.Euler (rotation)) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "vtree " + ToString (i, j);
					break;	
				case "#":
					if (Random.Range (0.0f, 1.0f) >= 0.5)
						rotation.y = 90.0f;
					else
						rotation.y = 0f;
					maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (treePrefab, position, Quaternion.Euler (rotation)) as Transform; 
					maze[(int)i-countMazeI,(int)j-countMazeJ].name= "unreachableTree " + ToString (i, j);
					break;
				case "g":
					position.x = i * 1.5f - (1.0f - Random.Range (0.5f, 1.0f));
					position.z = j * 2.5f - (2.0f - Random.Range (1.5f, 2.0f));
					rotation.y = 180.0f;
					maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "tree |_ " + ToString (i, j);
					break;	
				case "t": 
					position.x = i * 1.5f - (1.0f - Random.Range (0.5f, 1.0f));
					position.z = j * 2.5f - (2.0f - Random.Range (1.5f, 2.0f));
					rotation.y = 270.0f;
					maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "tree |¯ " + ToString (i, j);
					break;
				case "h":
					position.x = i * 1.5f - (1.0f - Random.Range (0.5f, 1.0f));
					position.z = j * 2.5f - (2.0f - Random.Range (1.5f, 2.0f));
					rotation.y = 90.0f;
					maze[(int)i-countMazeI,(int)j-countMazeJ] = Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "tree _| " + ToString (i, j);
					break;
				case "y": 
					position.x = i * 1.5f - (1.0f - Random.Range (0.5f, 1.0f));
					position.z = j * 2.5f - (2.0f - Random.Range (1.5f, 2.0f));
					rotation.y = 0.0f;
					maze[(int)i-countMazeI,(int)j-countMazeJ]= Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as Transform;	
					maze[(int)i-countMazeI,(int)j-countMazeJ].name = "tree ¯| " + ToString (i, j);
					break;
				}
				if (maze[(int)i-countMazeI,(int)j-countMazeJ]) 
				{maze[(int)i-countMazeI,(int)j-countMazeJ].transform.parent = parentMaze.transform;
					//maze[(int)i-countMazeI,(int)j-countMazeJ].renderer.enabled=false; 
				}
			}
		}
	}
	string ToString (float x, float y)
	{
		return x + " " + y;
	}
	
	
	
}