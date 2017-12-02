using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public float speed = 0.0f;
	private float sprintSpeed;
	private float walkSpeed;
	// Use this for initialization
	void Start () {
		walkSpeed = speed;
		sprintSpeed = speed * 10;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = sprintSpeed;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			speed = walkSpeed;
		}

		if (Input.GetKey (KeyCode.W)) {
			transform.Translate(Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate(Vector3.back * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
	}
}
