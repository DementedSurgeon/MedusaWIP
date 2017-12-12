using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour {

	public PlayerHealth pHealth;
	public GlyphManager gManager;
	public FadeToBlack fBlack;
	public Door door;
	public WinnerCircle wCircle;

	// Use this for initialization
	void Start () {
		pHealth.OnDeath += DeathSequence;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void DeathSequence()
	{
		fBlack.Fade ();
	}

	void WinSequence()
	{
		wCircle.isActive = true;
		door.InteractWithDoor ();
	}
}
