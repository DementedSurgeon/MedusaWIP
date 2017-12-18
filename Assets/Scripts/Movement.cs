using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float speed = 0.0f;
	public float sprintSpeed;
	public float walkSpeed;
	public float crouchSpeed;
	private Rigidbody rBody;
	private bool moving = false;
	private float timer = 0;
	private AudioSource aSource;
	private Collider[] cols;
	// Use this for initialization
	void Start () {
		walkSpeed = speed;
		sprintSpeed = speed * 2;
		crouchSpeed = speed / 2;
		rBody = gameObject.GetComponent<Rigidbody> ();
		aSource = gameObject.GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (moving) {
			if (!aSource.isPlaying) {
				aSource.Play ();
			}
			if (timer <= 0) {
				cols = Physics.OverlapSphere (transform.position, 1 * speed);
				for (int i = 0; i < cols.Length; i++) {
					if (cols [i].gameObject.tag == "Medusa") {
						cols [i].GetComponent<AlertState> ().Alert (transform);
					}
					timer = 6 / speed;
				}
			} else {
				timer -= Time.deltaTime;
			}
		} else if (!moving) {
			if (aSource.isPlaying) {
				aSource.Stop ();
			}
		}

		float amountAD = Input.GetAxis ("Horizontal");
		float amountWS = Input.GetAxis ("Vertical");
		Vector3 movement = ((transform.right * amountAD) + (transform.forward * amountWS)) * speed;
		rBody.velocity = new Vector3 (movement.x, rBody.velocity.y, movement.z);
		if (amountAD == 0 && amountWS == 0) {
			rBody.velocity = new Vector3 (0, rBody.velocity.y, 0);
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = sprintSpeed;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = walkSpeed;
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			speed = crouchSpeed;
			for (int i = 0; i < gameObject.transform.childCount; i++) {
				gameObject.transform.GetChild (i).transform.localPosition = new Vector3 (gameObject.transform.GetChild (i).transform.localPosition.x, gameObject.transform.GetChild (i).transform.localPosition.y / 2, gameObject.transform.GetChild (i).transform.localPosition.z);
			}
		}
		if (Input.GetKeyUp (KeyCode.LeftControl)) {
			speed = walkSpeed;
			for (int i = 0; i < gameObject.transform.childCount; i++) {
				gameObject.transform.GetChild (i).transform.localPosition = new Vector3 (gameObject.transform.GetChild (i).transform.localPosition.x, gameObject.transform.GetChild (i).transform.localPosition.y * 2, gameObject.transform.GetChild (i).transform.localPosition.z);
			}
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			rBody.AddForce (Vector3.up * 500);
		}

		/*if (Input.GetKey (KeyCode.W)) {
			rBody.velocity = new Vector3 (transform.forward.x * speed, rBody.velocity.y, transform.forward.z * speed);
			moving = true;
		}

		if (Input.GetKey (KeyCode.A)) {
			rBody.velocity = transform.right * -speed;
			moving = true;
		}
		if (Input.GetKey (KeyCode.S)) {
			rBody.velocity = transform.forward * -speed;
			moving = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			rBody.velocity = new Vector3 (transform.right.x * speed, rBody.velocity.y, transform.right.z * speed);
			moving = true;
		}
		if (Input.GetKeyUp (KeyCode.W)) {
			rBody.velocity = new Vector3 (0, rBody.velocity.y, 0);
			moving = false;
		}
		if (Input.GetKeyUp (KeyCode.A)) {
			rBody.velocity = new Vector3 (0, rBody.velocity.y, 0);
			moving = false;
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			rBody.velocity = new Vector3 (0, rBody.velocity.y, 0);
			moving = false;
		}
		if (Input.GetKeyUp (KeyCode.D)) {
			rBody.velocity = new Vector3 (0, rBody.velocity.y, 0);
			moving = false;
		}*/
	}
}
