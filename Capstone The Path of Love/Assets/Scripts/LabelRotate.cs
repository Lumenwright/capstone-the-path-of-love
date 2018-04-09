// How to get something to face the mainCamera:
// http://wiki.unity3d.com/index.php?title=CameraFacingBillboard by Neil Carter, modified 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabelRotate : MonoBehaviour {
	// Rotates label to always face the camera.

	public GameObject label;
	public GameObject mainCamera;
	Vector3 lookat;

	// Use this for initialization
	void Start () {
		lookat = new Vector3 (0, label.transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		float x = label.transform.position.x + mainCamera.transform.forward.x ;
		float z = label.transform.position.z + mainCamera.transform.forward.z;
		lookat.x = x;
		lookat.z = z;
		label.transform.LookAt (lookat);
	}
}
