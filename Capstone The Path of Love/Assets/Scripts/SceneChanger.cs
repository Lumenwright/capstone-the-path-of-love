using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public void LoadScene(string name){
		// Change the current scene to name.
		SceneManager.LoadScene(name);
	}
}
