// Note: ShowOpenTiles() kept getting in the way of incoming tiles.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestination : MonoBehaviour {
	// spawns the story destinations at a generated position

	public GameManager gm;

	public GameObject doctor;
	public GameObject end;

	public Vector3 position1;
	public Vector3 position2;

	float sizeOfDestinationDoctor = 3f; 
	float sizeOfDestinationEnd = 1f; 

	// how far away from the start, in tiles, can the destination be at most
	int gameSize = 4; //half length of grid in tiles
	float tileSize; 

	public Tile openTile;
	List<Tile> openTiles;

	// Use this for initialization
	void Start () {
		tileSize = gm.tileSize;

		// spawn the destinations at generated coordinates
		position1 = GeneratePositionCoordinates (sizeOfDestinationDoctor);

		position2 = GeneratePositionCoordinates (sizeOfDestinationEnd);

		while (doCoordinatesOverlap(position1,position2, sizeOfDestinationDoctor, sizeOfDestinationEnd)) { // if they are the same coordinate, make a new one
			position2 = GeneratePositionCoordinates (sizeOfDestinationEnd);
		}
		doctor.transform.position = position1;
		end.transform.position = position2;

		// make 4 open tiles
		openTiles = new List<Tile>();

		for (int i = 0; i < 8; i++) {
			Tile obj = (Tile)Instantiate (openTile);
			obj.gameObject.SetActive (false);
			openTiles.Add (obj);
		}

		// spawn open tiles
//		ShowTilePlaces(end, sizeOfDestinationEnd);
//		ShowTilePlaces(doctor, sizeOfDestinationDoctor);

	}


	Vector3 GeneratePositionCoordinates (float length){
		// randomly generate positions for the destinations relative to start tile

		int size = (int)length;

		// number of tiles away from start
		int num1 = Random.Range(-1*gameSize, -1*size);
		int num2 = Random.Range (size, gameSize);
		int randomStepsx = Random.Range (1, 3)==1 ? num1 : num2; // flip coin which side of origin to spawn at
		int randomStepsy = Random.Range (1, 3)==1 ? num1 : num2;

		// convert to world coordinates
		float distanceNorth = randomStepsy * tileSize + 1.5f;
		float distanceEast = randomStepsx * tileSize +1.5f;

		Vector3 position = new Vector3 (distanceNorth, 0, distanceEast);

		return position;
	}


//	void ShowTilePlaces(GameObject obj, float size){

//		float displace = (size + 1) * tileSize;
//
//		// show 4 little open tiles
//		Vector3 north = obj.transform.TransformPoint( displace* new Vector3(0.3333f,0,1f));
//		Vector3 south = obj.transform.TransformPoint( displace* new Vector3(0.3333f,0,1f)*-1f);
//		Vector3 west = obj.transform.TransformPoint( displace* new Vector3(1f,0,0.3333f));
//		Vector3 east = obj.transform.TransformPoint( displace* new Vector3(-1f, 0, -0.3333f));
//
//		gm.MakeTile(openTiles, north);
//		gm.MakeTile(openTiles, south);
//		gm.MakeTile(openTiles, east);
//		gm.MakeTile(openTiles, west);
//	}

	bool doCoordinatesOverlap(Vector3 pos1, Vector3 pos2, float size1, float size2){
		float max = (size1 + size2) * tileSize;
		float x = Mathf.Abs (pos1.x - pos2.x);
		float z = Mathf.Abs (pos1.z - pos2.z);

		if (x < max || z < max) {
			return true;
		} else {
			return false;
		}
	}
}
