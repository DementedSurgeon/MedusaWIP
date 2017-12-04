using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThings : MonoBehaviour {

	private Collider hand;
	private Transform throwable;
	private Transform glyph;
	private Rigidbody stuffs;
	private bool canGrab = false;
	private bool canDestroy = false;

	// Use this for initialization
	void Start () {
		hand = gameObject.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canGrab) {
			if (Input.GetKeyDown (KeyCode.F)) {
				throwable.SetParent (transform);
				//stuffs.useGravity = false;
				stuffs.isKinematic = true;
				//stuffs.velocity = Vector3.zero;
				//stuffs.angularVelocity = Vector3.zero;
				throwable.gameObject.GetComponent<Noisemaker> ().label = "Player";
				throwable.gameObject.GetComponent<Noisemaker> ().thrower = transform;
			}
			if (Input.GetKeyDown (KeyCode.F)) {
				throwable.SetParent (null);
				//stuffs.useGravity = true;
				stuffs.isKinematic = false;
				stuffs.velocity = hand.transform.forward * 10;
			}
		} else if (canDestroy) {
			if (Input.GetKeyDown (KeyCode.F)) {
				Destroy (glyph.gameObject);
			}
		}
	}

	void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "Grabbable") {
			canGrab = true;
			throwable = col.transform;
			stuffs = col.gameObject.GetComponent<Rigidbody> ();
		} else if (col.gameObject.tag == "Glyph") {
			canDestroy = true;
			glyph = col.transform;
		}
	}

	void OnTriggerExit (Collider col)
	{
		if (col.gameObject.tag == "Grabbable") {
			canGrab = false;
			throwable = null;
			stuffs = null;
			if (col.gameObject.tag == "Glyph") {
				canDestroy = true;
				glyph = col.transform;
			}
		} else if (col.gameObject.tag == "Glyph") {
			canDestroy = true;
			glyph = col.transform;
		}
	}
}
