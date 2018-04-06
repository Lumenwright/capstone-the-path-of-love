﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestination : MonoBehaviour {
	// spawns the story destinations at a generated position

	public GameManager gm;

	public GameObject doctor;
	public GameObject end;

	public Vector3 position1;
	public Vector3 position2;

	float sizeOfDestinationDoctor = 1.5f; 
	float sizeOfDestinationEnd = 0.5f; 

	// how far away from the start, in tiles, can the destination be at most
	int gameSize = 15; //length of grid in tiles
	float tileSize; 

	public Tile openTile;
	List<Tile> openTiles;

	// Use this for initialization
	void Start () {
		tileSize = gm.tileSize;

		// spawn the destinations at generated coordinates
		position1 = GeneratePositionCoordinates (sizeOfDestinationDoctor);
		doctor.transform.position = position1;

		position2 = GeneratePositionCoordinates (sizeOfDestinationEnd);
		end.transform.position = position2;

		// make 4 open tiles
		openTiles = new List<Tile>();

		for (int i = 0; i < 8; i++) {
			Tile obj = (Tile)Instantiate (openTile);
			obj.gameObject.SetActive (false);
			openTiles.Add (obj);
		}

		// spawn open tiles
		ShowTilePlaces(end, sizeOfDestinationEnd);
		ShowTilePlaces(doctor, sizeOfDestinationDoctor);

	}


	Vector3 GeneratePositionCoordinates (float length){
		// randomly generate positions for the destinations relative to start tile

		int size = (int)length * 2;

		// number of tiles away from start
		int num1 = Random.Range(-1*gameSize, -1*size);
		int num2 = Random.Range (size, gameSize);
		int randomStepsx = Random.Range (1, 3)==1 ? num1 : num2; // which side of origin to spawn at
		int randomStepsy = Random.Range (1, 3)==1 ? num1 : num2;
		float distanceNorth = randomStepsy * tileSize - 1.5f;
		float distanceEast = randomStepsx * tileSize - 1.5f;

		Vector3 position = new Vector3 (distanceNorth, 0, distanceEast);

		return position;
	}


	void ShowTilePlaces(GameObject obj, float size){

		float displace = (size + 1) * tileSize;

		// show 4 little open tiles
		Vector3 north = obj.transform.TransformPoint(displace*Vector3.forward);
		Vector3 south = obj.transform.TransformPoint(displace*Vector3.forward*-1);
		Vector3 west = obj.transform.TransformPoint(displace*Vector3.right);
		Vector3 east = obj.transform.TransformPoint(displace*Vector3.left);

		gm.MakeTile(openTiles, north);
		gm.MakeTile(openTiles, south);
		gm.MakeTile(openTiles, east);
		gm.MakeTile(openTiles, west);
	}
}
