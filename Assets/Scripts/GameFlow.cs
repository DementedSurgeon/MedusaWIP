using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFlow : MonoBehaviour {

	public PlayerHealth pHealth;
	public GlyphManager gManager;
	public FadeToBlack fBlack;
	public Door door;
	public WinnerCircle wCircle;
	public GameObject medusa;
	public FloorShard floorShard;
	public Text victoryText;
	public Text defeatText;
	public GameObject buttons;

	// Use this for initialization
	void Start () {
		pHealth.OnDeath += EndSequence;
		wCircle.OnArrival += EndSequence;
		gManager.onGlyphsDestroyed += ExitOpen;
		floorShard.OnGrabbed += UnlockDoor;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	void EndSequence()
	{
		fBlack.Fade ();
		medusa.SetActive (false);
		pHealth.gameObject.GetComponent<Movement> ().enabled = false;
		if (pHealth.IsAlive()) {
			Debug.Log ("You win!");
			victoryText.gameObject.SetActive (true);
		} else {
			Debug.Log ("You lose!");
			defeatText.gameObject.SetActive (true);
		}
		buttons.SetActive (true);
		Cursor.lockState = CursorLockMode.None;
	}

	void UnlockDoor()
	{
		door.LockUnlockDoor ();
	}

	void ExitOpen()
	{
		wCircle.isActive = true;
		door.isLocked = false;
		door.InteractWithDoor ();
	}

	void OnTriggerEnter(Collider col)
	{
		Coroutine sSequence = null;
		if (col.tag == "Player" && sSequence == null) {
			sSequence = StartCoroutine (StartUpSequence ());
		}
	}

	IEnumerator StartUpSequence()
	{
		yield return new WaitForSeconds (1.0f);
		door.InteractWithDoor ();
		door.isLocked = true;
		if (!medusa.activeSelf) {
			medusa.SetActive (true);
		}
	}
}
