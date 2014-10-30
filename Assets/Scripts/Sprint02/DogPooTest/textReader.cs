using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

[System.Serializable]
public class textReader : MonoBehaviour
{
		public TextAsset levelMap;
		public TextAsset legend;
		public string assetText;
		public string legendText;
		public string[,] map;
		public string[] elements;
		public Transform[] GameElements;
		Dictionary <string, Transform> prefabsRequired; 	

		

		// Use this for initialization
		void Start ()
		{
				readMapandSplit ();
				createRules ();
				drawMap ();

		}

		// Update is called once per frame
		void Update ()
		{
		}

		void readMapandSplit ()
		{
				assetText = levelMap.text; 
				string[] row = assetText.Split ('\n');
		
				string[] col = row [0].Split (',');
				map = new string[row.GetLength (0), col.GetLength (0)];
				for (int i=0; i<row.GetLength(0); i++) {

						col = row [i].Split (',');

						for (int j=0; j<col.GetLength(0); j++) {
								map [i, j] = col [j];

						}
			
				}

				legendText = legend.text;
				elements = legendText.Split (',');

				
		}

		void createRules()
		{
		prefabsRequired = new Dictionary<string, Transform> ();
		for (int i=0;i<elements.GetLength(0);i++)
		{prefabsRequired.Add(elements[i], GameElements[i]);
			
		}
		}

		void drawMap ()
		{
		Vector3 position = new Vector3 ();
		position.y = 0f;
	
		
		for (int i=0; i<map.GetLength(0); i++) { 	
			for (int j=0; j<map.GetLength(1); j++) 
			{
				position.x=(float)i*5.0f;
				position.z=(float)j*8.0f;
				Transform x=null;
				if (prefabsRequired.TryGetValue(map[i,j], out x ))
				{GameObject temp = Instantiate(prefabsRequired[map[i,j]], position, Quaternion.identity) as GameObject;
					transform.parent=transform;}			
			}
		}
		}


		}