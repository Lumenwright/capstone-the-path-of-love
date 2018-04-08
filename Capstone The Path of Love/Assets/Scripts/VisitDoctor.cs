using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitDoctor : MonoBehaviour {

	public GameManager gm;

	public bool collectedMeds; // has this destination been visited at least once


	// Use this for initialization
	void Start () {
		collectedMeds = false;
	}
	
	public void increaseMaxEnergy (){
		//when the Doctor destination is clicked, max spoons is increased.
		// spoons are not refilled bc going to the doctor is hard.
		if (!collectedMeds) {
			gm.maxEnergys = 18;
			collectedMeds = true;
		}
	}
}
