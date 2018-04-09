// Sources:
// How to generate a random number from two different ranges:
// https://stackoverflow.com/questions/33681387/generate-random-number-from-2-different-ranges
// -----
// How to get something to face the mainCamera:
// http://wiki.unity3d.com/index.php?title=CameraFacingBillboard by Neil Carter, modified

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveNotes : MonoBehaviour {
	// drop notes with a random love message from a list of messages

	public GameManager gm;
	public GameObject scroll; // thing that drops
	public GameObject note;
	public Text message;
	List<string> messages; // list of messages

	public GameObject mainCamera;

	public AudioClip scrollAppearSound;

	// Use this for initialization
	void Start () {
		messages = new List<string>();
		MakeList ();
	}

	public void OnClick () {
		SetMessage ();

		float notex1 = mainCamera.transform.position.x + mainCamera.transform.forward.x * 1.5f;
		float notez1 = mainCamera.transform.position.z + mainCamera.transform.forward.z* 1.5f;
		float notex2 = note.transform.position.x + mainCamera.transform.forward.x ;
		float notez2 = note.transform.position.z + mainCamera.transform.forward.z;

		note.transform.LookAt (new Vector3 (notex2, note.transform.position.y, notez2)); // need to change rotation before position
		note.transform.position = new Vector3(notex1, note.transform.position.y, notez1);

		note.SetActive (true);
		scroll.SetActive (false);
	}

	// drop note (add physics component to scroll)
	public void DropNote (){
		scroll.transform.position = mainCamera.transform.position + mainCamera.transform.forward;
		scroll.SetActive (true);
		scroll.GetComponent<GvrAudioSource> ().PlayOneShot (scrollAppearSound, 0.3f);
	}

	// set message in the note
	void SetMessage(){
		int randomNumber = Random.Range (0, messages.Count);
		message.text = messages [randomNumber];
	}

	// make list of messages
	void MakeList (){
		messages.Add ("Get some rest. \n Celeste");
		messages.Add ("I know you're feeling frustrated right now, but let yourself recharge. Meditate. \n Celeste");
		messages.Add ("I grant you royal permission to work at your own pace. Take care of yourself. \n Celeste");
		messages.Add ("You're not lazy. You just have less energy than other mages. Go get help. \n Celeste");
		messages.Add ("You're worth it. Trust me. I wish you could see what I see in you. \n Celeste");
		messages.Add ("Your eyes are like Lake Clarica under the stars. \n Celeste");
		messages.Add ("I love your poem. You may think it's not good enough but you're good enough for me. \n Celeste");
		messages.Add ("I would do anything to ease your curse and see you smile. Just tell me what I need to do. \n Celeste");
		messages.Add ("Your smile is like the relief of rain after a hot, dry summer. \n Celeste");
		messages.Add ("Conserve your energy. Rest. Meditate. I can wait. \n Celeste");
		messages.Add ("You're not lazy. You're the hardest working sorceress I know. \n Celeste");
		messages.Add ("You deserve to rest. \n Celeste");
		messages.Add ("One step at a time. \n Celeste");
		messages.Add ("You're not the only one. \n Celeste");
		messages.Add ("You don't have to be the best to be important. You're important to me. \n Celeste");
		messages.Add ("Resting isn't giving up. \n Celeste");
		messages.Add ("You are not a burden to me. \n Celeste");
		messages.Add ("Let's get a cat! \n Celeste");
		messages.Add ("You like apple sweetrolls, right? \n Celeste");
		messages.Add ("Soon you'll have a reminder of how much I love you. \n Celeste");
	}
}
