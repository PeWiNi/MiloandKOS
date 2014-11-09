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
	
		
	public string [,] levelMap;

	void Start (){
		//32 columns, 24 rows
		levelMap= new string[,]{
			// g, 1, 1, 2 
			{"g","y","1","1","1","1","1","7","1","1","7","1","1","1","1","1","t","1","y","#","#","#","#","#","t","1","y","1","1","#","#","#","#"},
			{"0","g","1","1","2","1","1","1","s","4","1","1","2","1","1","1","h","2","7","#","#","#","#","#","7","2","0","0","0","0","#","#","#"},
			{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","7","#","#","#","t","1","s","0","t","1","1","#","#","#","#"},
			{"y","0","d","y","0","d","t","1","1","1","1","y","0","4","3","t","y","0","7","#","#","#","7","0","0","0","7","#","#","#","#","#","#"},
			{"7","0","4","7","0","4","#","#","#","#","#","4","0","t","#","#","a","0","g","1","1","1","h","0","2","3","4","#","#","#","#","#","#"},
			{"7","0","t","h","0","g","1","1","1","1","h","a","0","7","#","#","g","1","4","0","0","0","0","0","t","#","#","#","#","#","#","#","#"},
			{"7","0","7","2","0","0","0","0","0","0","0","0","0","g","y","#","#","#","a","0","2","4","3","0","7","#","#","#","#","#","#","#","#"},
			{"7","0","7","7","3","t","y","3","t","1","1","y","0","2","7","1","1","1","h","0","0","d","7","0","7","#","#","#","#","#","#","#","#"},
			{"7","0","g","1","1","1","1","1","1","1","1","h","0","t","h","7","0","0","0","0","0","7","7","0","g","#","#","#","#","#","#","#","#"},
			{"2","0","0","d","0","0","0","0","0","0","0","0","0","7","#","7","0","0","0","0","0","7","7","0","0","g","s","1","1","#","#","#","#"},
			{"1","1","1","4","0","0","2","4","3","t","1","1","4","h","1","7","0","0","t","a","0","t","7","0","0","0","0","0","0","0","#","#","#"},
			{"g","1","h","0","0","0","g","1","1","1","1","1","s","4","1","h","0","0","7","y","0","d","g","1","1","3","1","1","1","#","#","#","#"},
			{"2","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","7","7","0","4","1","#","#","#","#","#","#","#","#","#","#"},
			{"y","0","t","y","0","4","1","1","1","4","3","1","1","1","3","2","1","1","h","7","0","g","1","y","#","#","#","#","#","#","#","#","#"},
			{"7","0","7","7","0","7","t","1","1","s","2","1","1","1","1","1","1","4","1","h","0","0","0","7","#","#","#","#","#","#","#","#","#"},
			{"7","0","g","7","0","7","7","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","g","s","2","1","1","1","#","#","#","#"},
			{"7","3","4","7","0","7","7","0","t","4","0","t","1","1","t","3","y","1","1","y","4","3","0","0","0","0","0","0","0","#","#","#","#"},
			{"g","1","h","7","0","7","7","0","7","7","0","7","t","2","g","1","h","1","1","1","1","y","0","0","0","0","0","0","0","0","#","#","#"},
			{"#","#","#","7","0","7","7","3","4","7","0","7","7","0","0","0","0","0","0","0","2","g","h","3","t","1","1","3","4","#","#","#","#"},
			{"#","#","#","4","0","g","1","1","1","h","0","7","7","0","t","1","y","4","0","0","7","1","1","1","1","#","#","#","#","#","#","#","#"},
			{"#","#","#","2","0","0","0","0","0","0","0","2","7","0","7","1","g","h","0","0","g","1","1","1","1","#","#","#","#","#","#","#","#"},
			{"#","#","#","g","1","1","y","3","t","y","0","d","g","3","h","s","0","0","0","0","0","0","0","0","g","#","#","#","#","#","#","#","#"},
			{"#","#","#","#","#","#","#","#","#","7","0","0","0","0","0","0","0","t","1","1","4","3","0","0","0","s","1","1","1","#","#","#","#"},
			{"#","#","#","#","#","#","#","#","#","7","0","0","0","0","0","0","0","d","#","#","#","1","0","0","0","0","0","0","#","#","#","#","#"},
			{"#","#","#","#","#","#","#","#","#","g","1","1","4","3","1","1","2","1","#","#","#","1","1","1","1","1","1","1","4","#","#","#","#"}
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

								position.x=i*1.5f;
								position.z=j*2.5f;
								rotation.y = 0.0f;
								switch (levelMap [(int)i, (int)j]) {
								case "1":
										position.x=i*1.5f-(1.0f-Random.Range (0.5f,0.8f));
										position.z=j*2.5f-(2.0f-Random.Range (1.5f,1.8f));
										GameObject tree = Instantiate (treePrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;
										break;			
								case "2":GameObject lamp = Instantiate (lampPrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;						 
										break;
								case "3": rotation.y = 90.0f;
										position.x+=0.4f;
										position.z+=0.3f;
										GameObject bench = Instantiate (benchPrefab, position, Quaternion.Euler(rotation)) as GameObject;	
										transform.parent = transform;
										break;
								case "d": position.z+=0.5f;
										position.x+=0.5f;
										GameObject bencha = Instantiate (benchPrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;
										break;
								case "a":rotation.y = 180.0f;
										position.z+=0.5f;
										position.x+=0.5f;
										GameObject benchd = Instantiate (benchPrefab, position, Quaternion.Euler (rotation)) as GameObject;	
										transform.parent = transform;
										break;
								case "s":rotation.y = -90.0f;
										position.z+=0.5f;
										position.x-=0.8f;
										GameObject benchs = Instantiate (benchPrefab, position, Quaternion.Euler (rotation)) as GameObject;	
										transform.parent = transform;
										break;
								case "4": position.x-=0.4f;
										position.z-=0.4f;
										GameObject can = Instantiate (canPrefab, position, Quaternion.identity) as GameObject;	
										transform.parent = transform;
										break;
								case "7": position.x=i*1.5f-(1.0f-Random.Range (0.5f,0.8f));
									position.z=j*2.5f-(2.0f-Random.Range (1.5f,1.8f));
									rotation.y=90.0f;
									GameObject tree2 = Instantiate (treePrefab, position, Quaternion.Euler (rotation)) as GameObject;	
									transform.parent = transform;
									break;	
								case "#": 	position.x=i*1.5f-(1.0f-Random.Range (0.5f,0.8f));
											position.z=j*2.5f-(2.0f-Random.Range (1.5f,1.8f));
											if (Random.Range (0.0f, 1.0f) >=0.5) rotation.y=90.0f;
												else rotation.y=0f;
														GameObject trees = Instantiate (treePrefab, position, Quaternion.Euler(rotation)) as GameObject; 
														transform.parent = transform; 
														break;
				case "g": position.x=i*1.5f-(1.0f-Random.Range (0.5f,1.0f));
					position.z=j*2.5f-(2.0f-Random.Range (1.5f,2.0f));
					rotation.y=180.0f;
					GameObject treet = Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as GameObject;	
					transform.parent = transform;
					break;	
				case "t": 
					position.x=i*1.5f-(1.0f-Random.Range (0.5f,1.0f));
					position.z=j*2.5f-(2.0f-Random.Range (1.5f,2.0f));
					rotation.y=270.0f;
					GameObject treey = Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as GameObject;	
					transform.parent = transform;break;
				case "h": position.x=i*1.5f-(1.0f-Random.Range (0.5f,1.0f));
					position.z=j*2.5f-(2.0f-Random.Range (1.5f,2.0f));
					rotation.y=90.0f;
					GameObject treeg = Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as GameObject;	
					transform.parent = transform;break;
				case "y": 
					position.x=i*1.5f-(1.0f-Random.Range (0.5f,1.0f));
					position.z=j*2.5f-(2.0f-Random.Range (1.5f,2.0f));
					rotation.y=0.0f;
					GameObject treeh = Instantiate (cornerTreePrefab, position, Quaternion.Euler (rotation)) as GameObject;	
					transform.parent = transform;break;
								}
						}
				}
		
		}
	
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
