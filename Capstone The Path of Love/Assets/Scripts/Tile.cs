using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public string tileType = "cross";

	public void Start(){
	}

	public void setTileType (string tt){
		// set tile type to "corner", "straight", "cross", or "open"
		tileType = tt;
	}
}
