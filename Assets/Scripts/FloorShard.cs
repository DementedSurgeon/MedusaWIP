using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorShard : MonoBehaviour {

	public delegate void MyDelegate ();
	public MyDelegate OnGrabbed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Equip()
	{
		gameObject.SetActive (false);
		if (OnGrabbed != null) {
			OnGrabbed ();
		}
	}
}
