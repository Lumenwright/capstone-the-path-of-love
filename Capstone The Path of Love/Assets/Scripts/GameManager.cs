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
	public Tile lastOpenTile;

	public int numberInPool = 10;
	int numberOfTileTypes = 3;
	List<Tile> cornerTiles; // pool of tiles
	List<Tile> straightTiles;
	List<Tile> crossTiles;

	// pool of placeholder tiles
	public int numberOfOpen = 20;
	List<Tile> openTiles;

	float tileSize = 3f; // length of one edge
	Vector3 north, south, east, west; // positions around the tile

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
		Vector3 currentPosition = currTile.transform.position;

		north = currTile.transform.TransformPoint(tileSize*Vector3.up);
		south = currTile.transform.TransformPoint(tileSize*Vector3.down);
		west = currTile.transform.TransformPoint(tileSize*Vector3.right);
		east = currTile.transform.TransformPoint(tileSize*Vector3.left);

		// start tile has 4 open edges
		if (currTile.tileType == "start" || currTile.tileType == "cross") {
			MakeTile(openTiles, north);
			MakeTile(openTiles, south);
			MakeTile(openTiles, east);
			MakeTile(openTiles, west);
		}

		// straight tile has 1 open edge
		if (currTile.tileType == "straight") {
			bool westIsOccupied = DetectNeighbourTile (west);
			bool eastIsOccupied = DetectNeighbourTile (east);

			if (westIsOccupied && !eastIsOccupied) {
				MakeTile (openTiles, east);
			} else if (!westIsOccupied && eastIsOccupied) {
				MakeTile (openTiles, west);
			} else if (!westIsOccupied && !eastIsOccupied) {

				// This case is for when the straight tile spawns 
				// perpendicular to the current tile and no open tiles appear.
				// This makes a dummy open tile to set to lastOpenTile so that a
				// existing open tile doesn't get deactivated when the straight tile is rotated.
				MakeTile (openTiles, west);
				lastOpenTile.gameObject.SetActive (false);
			}
		}

		// corner tile has 2 possible open edges
		// but only one of them is open at a time.
		// Walls assumed to be at north and east.
		if (currTile.tileType == "corner") {
			
			bool southIsOccupied = DetectNeighbourTile (south);
			bool westIsOccupied = DetectNeighbourTile (west);

			if (southIsOccupied && !westIsOccupied) {
				MakeTile (openTiles, west);
			} else if (westIsOccupied && !southIsOccupied) {
				MakeTile (openTiles, south);
			} else if (!westIsOccupied && !southIsOccupied) {
				MakeTile (openTiles, west); // just make one direction open so that rotation works
			}
		}

		if (currTile.tileType == "cross") {
			
			bool northIsOccupied = DetectNeighbourTile (north);
			bool southIsOccupied = DetectNeighbourTile (south);
			bool westIsOccupied = DetectNeighbourTile (west);
			bool eastIsOccupied = DetectNeighbourTile (east);

			Debug.Log ("north" + northIsOccupied);
			Debug.Log ("south" + southIsOccupied);
			Debug.Log ("east" + eastIsOccupied);
			Debug.Log ("west" + westIsOccupied);

			if (!northIsOccupied) {
				MakeTile (openTiles, north);
			}
			if (!southIsOccupied) {
				MakeTile (openTiles, south);
			}
			if (!eastIsOccupied) {
				MakeTile (openTiles, east);
			}
			if (!westIsOccupied) {
				MakeTile (openTiles, west);
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

		// get position of other tiles and compare to current tile
		for (int i = 0; i < numberInPool; i++) {
			if (cornerTiles [i].gameObject.transform.position == position) {
				return true;
			} else if (straightTiles [i].gameObject.transform.position == position) {
				return true;
			} else if (crossTiles [i].gameObject.transform.position == position) {
				return true;
			}
		}

		return false;

	}


	public void SetTile (Tile tileClicked){
		// only applies to open tiles
		// set the next tile into the clicked position
		//get next generated tile here TBD
		prevTile = currentTile;
		Debug.Log ("previous tile: " + prevTile);

		GenerateNextTile ();
		MakeTile(nextTiles, tileClicked.transform.position); //sets new current tile
//		MakeTile(straightTiles, tileClicked.transform.position);
		Debug.Log ("current tile in Settile: " + currentTile);
		tileClicked.gameObject.SetActive (false); // must be before showTilePlaces so there will be tiles in the pool
		showTilePlaces (currentTile);
	}

	public void MakeTile (List<Tile> tileList, Vector3 tilePosition){
		// Get the next tile object from its list
		for (int i = 0; i < tileList.Count; i++) {
			if (!tileList [i].gameObject.activeInHierarchy) {
				
				tileList [i].transform.position = tilePosition;
//				tileList [i].transform.rotation = Quaternion.identity;
				tileList [i].gameObject.SetActive(true);

				if (tileList [i].tileType != "open") {
					currentTile = tileList [i];
					Debug.Log ("current tile: " + currentTile);
				}

				if (tileList [i].tileType == "open") {
					lastOpenTile = tileList [i];
				}

				break;
			}
		}
	}
		
		
	public void GenerateNextTile(){
		// generate next tile type

		int randomNumber = Random.Range (0, numberOfTileTypes);

		if (randomNumber == 0) {
			nextTiles = cornerTiles;
		} else if (randomNumber == 1) {
			nextTiles = straightTiles;
		} else if (randomNumber == 2) {
			nextTiles = crossTiles;
		}
	}

	public void RotateTile(){
		//rotates the current tile, and remakes associated open tiles.
		currentTile.transform.Rotate(new Vector3 (0, 0, 90));

		// it should turn off the open tile placeholder and reshow it
		lastOpenTile.gameObject.SetActive(false);
		//make sure to remove from previous position after deactivation
		lastOpenTile.gameObject.transform.position = new Vector3 (0f,0f,0f); 

		showTilePlaces (currentTile);
	}
}
	