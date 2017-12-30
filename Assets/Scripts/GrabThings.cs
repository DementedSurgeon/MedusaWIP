using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabThings : MonoBehaviour {

	private Collider hand;
	public GameObject shard;
	public GlyphManager gManager;
	private Transform throwable;
	private Transform glyph;
	private FloorShard floorShard;
	private Door door;
	private Rigidbody stuffs;
	private bool grabbed = false;
	private bool canGrab = false;
	private bool canDestroy = false;
	private bool canEquip = false;
	private bool canOpen = false;

	// Use this for initialization
	void Start () {
		hand = gameObject.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canDestroy) {
			if (Input.GetKeyDown (KeyCode.F)) {
				glyph.gameObject.SetActive (false);
				canDestroy = false;
				glyph = null;
				GlyphManager.activeGlyph--;
			}
		} else if (canGrab) {
			if (!grabbed) {
				if (Input.GetKeyDown (KeyCode.F)) {
					throwable.SetParent (transform);
					//stuffs.useGravity = false;
					stuffs.isKinematic = true;
					grabbed = true;
					//stuffs.velocity = Vector3.zero;
					//stuffs.angularVelocity = Vector3.zero;
					throwable.gameObject.GetComponent<Noisemaker> ().label = "Player";
					throwable.gameObject.GetComponent<Noisemaker> ().thrower = transform;
				}
			} else if (grabbed) {
				if (Input.GetKeyDown (KeyCode.F)) {
					throwable.SetParent (null);
					//stuffs.useGravity = true;
					stuffs.isKinematic = false;
					grabbed = false;
				}
				if (Input.GetMouseButtonDown (0)) {
					throwable.SetParent (null);
					//stuffs.useGravity = true;
					stuffs.isKinematic = false;
					stuffs.velocity = hand.transform.forward * 10;
					grabbed = false;
				}
			}
		} 
	
		if (canEquip) {
			if (Input.GetKeyDown (KeyCode.F)) {
				shard.gameObject.SetActive (true);
				floorShard.Equip ();
				canEquip = false;
			}
		}
		if (canOpen) {
			if (Input.GetKeyDown (KeyCode.F)) {
				door.InteractWithDoor ();
			}
		}

	}

	void OnTriggerStay (Collider col)
	{
		if (col.gameObject.tag == "Grabbable") {
			canGrab = true;
			throwable = col.transform;
			stuffs = col.gameObject.GetComponent<Rigidbody> ();
		} 
		if (col.gameObject.tag == "Glyph") {
			canDestroy = true;
			glyph = col.transform;
		}
		if (col.gameObject.tag == "Shard") {
			canEquip = true;
			floorShard = col.GetComponent<FloorShard> ();
		}
		if (col.gameObject.tag == "Door") {
			canOpen = true;
			door = col.gameObject.GetComponent<Door> ();
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
		}
		if (col.gameObject.tag == "Glyph") {
			canDestroy = true;
			glyph = col.transform;
		}
		if (col.gameObject.tag == "Shard") {
			canEquip = false;
			floorShard = null;
		}

		if (col.gameObject.tag == "Door") {
			canOpen = false;
			door = null;
		}
	}

	public bool GetCurrentThrowable()
	{
		return grabbed;
	}

	public bool CanGrabShard()
	{
		return canEquip;
	}

	public bool CanOpenDoor()
	{
		return canOpen;
	}
}
