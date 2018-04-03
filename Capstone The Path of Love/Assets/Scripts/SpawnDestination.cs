using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDestination : MonoBehaviour {
	// spawns the story destinations at a generated position

	public GameManager gm;

	public GameObject doctor;
	public GameObject end;

	public Vector3 position1;

	// how far aawy from the start, in tiles, can the destination be at most
	int GameSize = 10;
	float tileSize;

	// Use this for initialization
	void Start () {
		tileSize = gm.tileSize;

//		int randomStepsx = Random.Range (-1 * GameSize, GameSize); // number of tiles away from start
		int randomStepsx = 3;
		int randomStepsy = 4;
		float distanceNorth = randomStepsy * tileSize;
		float distanceEast = randomStepsx * tileSize;

		position1 = new Vector3 (distanceNorth, 0, distanceEast);
		GameObject.Instantiate (doctor);
		doctor.transform.position = position1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// randomly generate the position of the destination
}
