using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// play tiles
	public Tile corner;
	public Tile straight;
	public Tile cross;
	public Tile open;

	public Tile start;
	public Tile currentTile;
	public List<Tile> nextTile;

	public int numberInPool = 10;
	public List<Tile> cornerTiles; // pool of tiles
	public List<Tile> straightTiles;
	public List<Tile> crossTiles;

	// placeholder tiles
	public int numberOfOpen = 4;
	List<Tile> openTiles;

	float tileSize = 1f; // length of one edge

	// Use this for initialization
	void Start () {

		// instantiate object pool
		cornerTiles = new List<Tile> ();
		straightTiles = new List<Tile> ();
		crossTiles = new List<Tile> ();
		openTiles = new List<Tile> ();

		for(int i = 0; i<numberInPool; i++)
		{
			Tile obj = (Tile)Instantiate (corner);
			obj.gameObject.SetActive (false);
			cornerTiles.Add (obj);

			obj = (Tile)Instantiate (straight);
			obj.gameObject.SetActive (false);
			straightTiles.Add (obj);

			obj = (Tile)Instantiate (cross);
			obj.gameObject.SetActive (false);
			crossTiles.Add (obj);
		}

		for (int i = 0; i < numberOfOpen; i++) {
			Tile obj = (Tile)Instantiate (open);
			obj.gameObject.SetActive (false);
			openTiles.Add (obj);
		}

		// For beginning tile show active edges
		showTilePlaces(start);
	}
		
	public void showTilePlaces (Tile currTile){
		// show possible positions of next tile based on current tile type
		Debug.Log("Current tile shown:"+currTile.tileType);

		// written so that the positions are in respect to the current rotation
		Vector3 north = currTile.transform.position + tileSize*currTile.transform.forward;
		Vector3 south = currTile.transform.position - tileSize*currTile.transform.forward;
		Vector3 east = currTile.transform.position + tileSize*transform.TransformPoint(Vector3.right);
		Vector3 west = currTile.transform.position + tileSize*transform.TransformPoint(Vector3.left);

		// start tile has 4 open edges
		if (currTile.tileType == "start") {
			MakeTile(openTiles, north);
			MakeTile(openTiles, south);
			MakeTile(openTiles, east);
			MakeTile(openTiles, west);
		}

		// straight tile has 1 open edge
		// chose a random edge and rotation will make sure that it is free
		if (currTile.tileType == "straight") {
			MakeTile (openTiles, north);
		}

		// corner tile has 2 possible open edges
		// but only one of them is open at a time.
		// for now have two edges
		if (currTile.tileType == "corner") {
			MakeTile (openTiles, west);
			MakeTile (openTiles, south);
		}
	}


	public void SetTile (Tile tileClicked){
		// only applies to open tiles
		// set the next tile into the clicked position
		//get next generated tile here TBD
		List<Tile> nextTiles = cornerTiles;
		MakeTile(nextTiles, tileClicked.transform.position);
		tileClicked.gameObject.SetActive (false); // must be before showTilePlaces so there will be tiles in the pool
		showTilePlaces (currentTile);
	}

	public void MakeTile (List<Tile> tileList, Vector3 tilePosition){
		// Get the next tile object from its list
		for (int i = 0; i < tileList.Count; i++) {
			if (!tileList [i].gameObject.activeInHierarchy) {
				
				tileList [i].transform.position = tilePosition;
				tileList [i].transform.rotation = Quaternion.identity;
				tileList [i].gameObject.SetActive(true);

				currentTile = tileList [i];

				break;
			}
		}
	}

	// generate next tile
}
	