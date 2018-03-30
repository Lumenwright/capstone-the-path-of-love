// =============================================
// Notes
// - Current tile will always be last placed tile regardless of where the player is
// - had to put in positions around tile in the function not in the Tile class to work (won't get correct position until after the function is done)
// =============================================

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
	public List<Tile> nextTiles;
	public Tile prevTile;

	public int numberInPool = 10;
	int numberOfTileTypes = 3;
	List<Tile> cornerTiles; // pool of tiles
	List<Tile> straightTiles;
	List<Tile> crossTiles;

	// pool of placeholder tiles
	public int numberOfOpen = 20;
	List<Tile> openTiles;

	public float tileSize = 1f; // length of one edge
	public Vector3 north, south, east, west; // positions around the tile

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
		currentTile = start;
		prevTile = start;
	}

	public void showTilePlaces (Tile currTile){
		// show possible positions of next tile based on current tile type
		//set the positions around the tile
		// written so that the positions are in respect to the current rotation
		north = currTile.transform.position + tileSize*currTile.transform.forward;
		south = currTile.transform.position - tileSize*currTile.transform.forward;
		east = currTile.transform.position + tileSize*currTile.transform.TransformPoint(Vector3.right);
		west = currTile.transform.position + tileSize*currTile.transform.TransformPoint(Vector3.left);

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
			Debug.Log ("showing open tiles for straight tile");
			Debug.Log ("current tile in showTile:"+currTile);
			Debug.Log ("current north" + north);
			MakeTile (openTiles, north);
		}

		// corner tile has 2 possible open edges
		// but only one of them is open at a time.
		// Walls assumed to be at north and east.
		// for now have two edges
		if (currTile.tileType == "corner") {
			bool southIsOccupied = DetectNeighbourTile (south);

			if (southIsOccupied) {
				MakeTile (openTiles, west);
			} else {
				MakeTile (openTiles, south);
			}
		}
	}


	public void SetTile (Tile tileClicked){
		// only applies to open tiles
		// set the next tile into the clicked position
		//get next generated tile here TBD
		prevTile = currentTile;
		Debug.Log ("previous tile: " + prevTile);

//		GenerateNextTile ();
//		MakeTile(nextTiles, tileClicked.transform.position); //sets new current tile
		MakeTile(straightTiles, tileClicked.transform.position);
		Debug.Log ("current tile in Settile: " + currentTile);
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

				if (tileList [i].tileType != "open") {
					currentTile = tileList [i];
					Debug.Log ("current tile: " + currentTile);
				}

				break;
			}
		}
	}

	public bool DetectNeighbourTile ( Vector3 position ){
		//detect if there is already a tile in the indicated direction

		// is there an open placeholder tile?
		for (int i = 0; i < openTiles.Count; i++) {
			if (openTiles [i].gameObject.transform.position == position) {
				return true;
			}
		}

		// get position of previous tile and compare to current tile
		Vector3 occupiedPosition = prevTile.gameObject.transform.position;
		if (occupiedPosition == position) {
			return true;
		} else {
			return false;
		}

	}
		
		
	public void GenerateNextTile(){
		// generate next tile type

		int randomNumber = Random.Range (1, numberOfTileTypes);

		if (randomNumber == 1) {
			nextTiles = cornerTiles;
		} else if (randomNumber == 2) {
			nextTiles = straightTiles;
		} else if (randomNumber == 3) {
			nextTiles = crossTiles;
		}
	}

//	public void RotateTile(Tile tileClicked){
		//rotates the clicked tile, and any associated open tiles.
//		tileClicked.transform.rotation = Quaternion.Euler (0, 90, 0);

		// if it's a corner piece, it should turn off the open tile placeholder and reshow it
//		for (int i = 0; i < openTiles.Count; i++) {
//			if (openTiles [i].gameObject.transform.position == tileClicked.south) {
//				openTiles [i].gameObject.SetActive (false);
//			}
//			else if (openTiles [i].gameObject.transform.position == tileClicked.north) {
//				openTiles [i].gameObject.SetActive (false);
//			}
//		}
//		showTilePlaces (tileClicked);
//	}
}
	