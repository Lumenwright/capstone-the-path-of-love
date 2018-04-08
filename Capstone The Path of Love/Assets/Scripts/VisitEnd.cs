using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisitEnd : MonoBehaviour {
	// stuff that happens when the end destination is reached by the player

	public VisitDoctor doctor;
	public GameObject celeste; // the princess model
	public GameObject waypoint;
	public GameObject canvas; // to display dialogue
	public GameObject mainCamera;

	bool doctorVisited;

	public Text ending;

	public void WhichEnding (){
		// One of two endings, depending on whether Prydzylia visted the doctor
		
		doctorVisited = doctor.collectedMeds;
		celeste.SetActive (false);

		if (doctorVisited) {
			ending.text = "You made it! Before we go to the lake, I have a question to ask. You've been the one I go to for counsel and happiness for 5 years, and I...I wanted to make that official. \n Will you agree to be my consort? \n <THE END>";
		} else {
			ending.text = "I'm so glad you made it after all. I'll do my best to try to find you the help you need to deal with your curse. I'll be by your side, always. \n <THE END>";
		}

		DisplayEndText ();
	}

	void DisplayEndText (){
		float notex1 = mainCamera.transform.position.x + mainCamera.transform.forward.x * 1.5f;
		float notez1 = mainCamera.transform.position.z + mainCamera.transform.forward.z* 1.5f;
		float notex2 = canvas.transform.position.x + mainCamera.transform.forward.x ;
		float notez2 = canvas.transform.position.z + mainCamera.transform.forward.z;

		canvas.transform.LookAt (new Vector3 (notex2, canvas.transform.position.y, notez2)); // need to change rotation before position
		canvas.transform.position = new Vector3(notex1, canvas.transform.position.y, notez1);
		canvas.SetActive (true);
	}
}
