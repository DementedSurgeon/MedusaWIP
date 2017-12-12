using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCircle : MonoBehaviour {

	public bool isActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player" && isActive) {
			Debug.Log ("You win!");
		}
	}
}
