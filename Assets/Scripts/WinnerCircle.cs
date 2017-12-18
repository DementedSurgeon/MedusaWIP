using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerCircle : MonoBehaviour {

	public bool isActive;
	public delegate void MyDelegate ();
	public MyDelegate OnArrival;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Player" && isActive) {
			if (OnArrival != null) {
				OnArrival ();
			}
		}
	}
}
