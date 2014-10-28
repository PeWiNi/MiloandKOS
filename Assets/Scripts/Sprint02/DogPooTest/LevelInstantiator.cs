using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class LevelInstantiator : MonoBehaviour {

	public Transform treePrefab;
	public Transform benchPrefab;
	public Transform lampPrefab;
	public Transform canPrefab;
	
		
	public string [,] levelMap;

	void Start (){
		//32 columns, 24 rows
		levelMap= new string[,]{
	{"#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","#","4","4","1","1","1"},
	{"0","1","1","1","2","1","1","1","s","4","1","1","2","1","1","1","1","2","1","#","#","#","#","#","2","0","0","0","0"},
	{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","1","#","#","#","1","4","a","0","1","1","1"},
	{"1","0","d","1","0","d","1","1","1","1","1","1","0","4","3","1","1","0","1","#","#","#","1","0","0","0","1","#","#"},
	{"1","0","4","1","0","4","#","#","#","#","#","4","0","1","#","#","a","0","1","1","1","1","1","0","2","3","4","#","#"},
	{"1","0","1","1","0","1","1","1","1","1","1","a","0","1","#","#","1","1","4","0","0","0","0","0","1","#","#","#","#"},
	{"1","0","1","2","0","0","0","0","0","0","0","0","0","1","#","#","#","#","a","0","2","4","3","0","1","#","#","#","#"},
	{"1","0","1","1","3","1","1","3","1","1","1","1","0","2","#","1","1","1","1","0","0","d","1","0","1","#","#","#","#"},
	{"2","0","0","d","0","0","0","0","0","0","0","0","0","1","#","1","0","0","0","0","0","0","1","1","0","1","s","1","1"},
	{"1","1","1","4","0","0","2","4","3","1","1","1","4","1","1","1","0","0","1","a","0","1","1","0","0","0","0","0","0"},
	{"1","1","1","0","0","0","1","1","1","1","1","1","s","4","1","1","0","0","1","1","0","d","1","1","1","3","1","1","1"},
	{"2","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","1","1","0","4","1","#","#","#","#","#","#"},
	{"1","0","1","1","0","4","1","1","1","4","3","1","1","1","3","2","1","1","1","1","0","1","1","1","#","#","#","#","#"},
	{"1","0","1","1","0","1","1","1","1","3","2","1","1","1","1","1","1","4","1","1","0","0","0","1","#","#","#","#","#"},
	{"1","0","1","1","0","1","1","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","1","3","2","1","1","1"},
	{"1","3","4","1","0","1","1","0","1","4","0","1","1","1","1","3","1","1","1","1","4","3","0","0","0","0","0","0","0"},
	{"#","#","#","1","0","1","1","0","1","1","0","1","1","2","1","1","1","1","1","1","4","1","0","0","0","0","0","0","0"},
	{"#","#","#","1","0","1","1","3","4","1","0","1","1","0","0","0","0","0","0","0","2","1","1","3","1","1","1","3","4"},
	{"#","#","#","4","0","1","1","1","1","1","0","1","1","0","1","1","1","4","0","0","1","1","1","1","1","#","#","#","#"},
	{"#","#","#","2","0","0","0","0","0","0","0","2","1","0","1","1","1","1","0","0","1","1","1","1","1","#","#","#","#"},
	{"#","#","#","#","1","1","1","3","1","1","0","d","1","s","1","s","0","0","0","0","0","0","0","0","1","#","#","#","#"},
	{"#","#","#","#","#","#","#","#","#","1","0","0","0","0","0","0","0","1","1","1","4","3","0","0","4","s","1","1","1"},
	{"#","#","#","#","#","#","#","#","#","1","0","0","0","0","0","0","0","d","#","#","#","1","0","0","0","0","0","0","0"},
	{"#","#","#","#","#","#","#","#","#","1","1","1","4","3","1","1","2","1","#","#","#","1","1","1","1","1","1","1","4"}
		};

		drawLevel ();

	}

		/// <summary>
		/// Draws the level, using a numerical map : 
		/// 0 - empty space, 
		/// 1 - tree, 
		/// 2 - street lamp, 	
		/// a - bench rotated -90 on y, d - bench rotated 90 on y, s - bench rotated 180 on y 
		/// 4 - trash can
		/// 
		/// </summary>
		void drawLevel ()
		{
				Vector3 position = new Vector3 ();
				position.y = 0f;
				Vector3 rotation = new Vector3 ();
				rotation.z = 0f;
				rotation.x = 0f;

				for (float i=0; i<levelMap.GetLength(0); i++) { 	
						for (float j=0; j<levelMap.GetLength(1); j++) {

								position.x=i*5.0f;
								position.z=j*8.0f;
								switch (levelMap [(int)i, (int)j]) {
								case "1":
										position.x=i*5.0f-(5.0f-Random.Range (3.5f,5.0f));
										position.z=j*8.0f-(8.0f-Random.Range (6.5f,8.0f));
										GameObject tree = Instantiate (treePrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;
										break;			
								case "2":GameObject lamp = Instantiate (lampPrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;						 
										break;
								case "3":GameObject bench = Instantiate (benchPrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;
										break;
								case "d":rotation.y = -90f;
										GameObject bencha = Instantiate (benchPrefab, position, Quaternion.Euler (rotation)) as GameObject;	
										transform.parent = transform;
										break;
								case "a":rotation.y = 90.0f;
										GameObject benchd = Instantiate (benchPrefab, position, Quaternion.Euler (rotation)) as GameObject;	
										transform.parent = transform;
										break;
								case "s":rotation.y = 180.0f;
										GameObject benchs = Instantiate (benchPrefab, position, Quaternion.Euler (rotation)) as GameObject;	
										transform.parent = transform;
										break;
								case "4":GameObject can = Instantiate (canPrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;
										break;
								case "#": position.x=i*5.0f-(5.0f-Random.Range (3.5f,5.0f));
										position.z=j*8.0f-(8.0f-Random.Range (3.5f,8.0f));
										GameObject trees = Instantiate (treePrefab, position, Quaternion.identity) as GameObject; 
										transform.parent = transform; 
										break;
								}
						}
				}
		
		}
	
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
