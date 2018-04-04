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

	// how far away from the start, in tiles, can the destination be at most
	int GameSize = 10;
	float tileSize;

	// Use this for initialization
	void Start () {
		tileSize = gm.tileSize;

		// spawn the destinations at generated coordinates
		position1 = GeneratePositionCoordinates ();
		doctor.transform.position = position1;

		position2 = GeneratePositionCoordinates ();
		end.transform.position = position2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	Vector3 GeneratePositionCoordinates (){
		// generate positions for the destinations relative to start tile
		int randomStepsx = Random.Range (-1 * GameSize, GameSize); // number of tiles away from start
		int randomStepsy = Random.Range (-1 * GameSize, GameSize);
		float distanceNorth = randomStepsy * tileSize;
		float distanceEast = randomStepsx * tileSize;

		Vector3 position = new Vector3 (distanceNorth, 0, distanceEast);

		return position;
	}

	// randomly generate the position of the destination
}
