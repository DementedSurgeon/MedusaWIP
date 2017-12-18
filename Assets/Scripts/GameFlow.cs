using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

	public PlayerHealth pHealth;
	public GlyphManager gManager;
	public FadeToBlack fBlack;
	public Door door;
	public WinnerCircle wCircle;
	public GameObject medusa;

	// Use this for initialization
	void Start () {
		pHealth.OnDeath += EndSequence;
		wCircle.OnArrival += EndSequence;
		gManager.onGlyphsDestroyed += ExitOpen;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void EndSequence()
	{
		fBlack.Fade ();
		medusa.SetActive (false);
		pHealth.gameObject.GetComponent<Movement> ().enabled = false;
		if (pHealth.IsAlive()) {
			Debug.Log ("You win!");
		} else {
			Debug.Log ("You lose!");
		}
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
