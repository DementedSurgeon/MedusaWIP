using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThings : MonoBehaviour {

	private Collider hand;
	private Transform stuff;
	private Rigidbody stuffs;
	private bool canGrab = false;

	// Use this for initialization
	void Start () {
		hand = gameObject.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canGrab) {
			if (Input.GetMouseButtonDown (0)) {
				stuff.SetParent (transform);
				//stuffs.useGravity = false;
				stuffs.isKinematic = true;
				//stuffs.velocity = Vector3.zero;
				//stuffs.angularVelocity = Vector3.zero;
			}
			if (Input.GetMouseButtonDown (1)) {
				stuff.SetParent (null);
				//stuffs.useGravity = true;
				stuffs.isKinematic = false;
				stuffs.velocity = hand.transform.forward * 10;
			}
		}
	}

	void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "Grabbable") {
			canGrab = true;
			stuff = col.transform;
			stuffs = col.gameObject.GetComponent<Rigidbody> ();
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Grabbable") {
			canGrab = false;
			stuff = null;
			stuffs = null;
		}
	}
}
