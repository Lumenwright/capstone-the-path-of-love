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
	public List<Tile> nextTile;

	public int numberInPool = 10;
	List<Tile> cornerTiles; // pool of tiles
	List<Tile> straightTiles;
	List<Tile> crossTiles;

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
		nextTile = openTiles; 
		showTilePlace(nextTile, start);
	}
		
	public void showTilePlace (List<Tile> nextTileType, Tile currTile){
		// show possible positions of next tile based on current tile type

		Vector3 north = currTile.transform.forward*tileSize;
		Vector3 south = -1f * north;
		Vector3 east = currTile.transform.position + new Vector3 (tileSize, 0f, 0f);
		Vector3 west = -1f * east;

		if (currTile.tileType == "start") {
			MakeTile(nextTileType, north);
			MakeTile(nextTileType, south);
			MakeTile(nextTileType, east);
			MakeTile(nextTileType, west);
		}
	}

	public void MakeTile (List<Tile> tileList, Vector3 tilePosition){
		// Get the next tile object from its list
		for (int i = 0; i < tileList.Count; i++) {
			if (!tileList [i].gameObject.activeInHierarchy) {
				tileList [i].transform.position = tilePosition;
				tileList [i].gameObject.SetActive(true);
				break;
			}
		}
	}

}
	