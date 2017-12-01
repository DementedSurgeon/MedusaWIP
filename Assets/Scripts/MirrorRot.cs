using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRot : MonoBehaviour {

	public float mouseSensitivity = 100.0f;
	//public float clampAngle = 80.0f;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis
	public Camera camM;

	void Start ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}

	void Update ()
	{
		if (Input.GetMouseButton (1)) {
			float mouseX = -Input.GetAxis ("Mouse X");
			float mouseY = -Input.GetAxis ("Mouse Y");

			rotY += mouseX * mouseSensitivity * Time.deltaTime;
			rotX += mouseY * mouseSensitivity * Time.deltaTime;

			//rotX = Mathf.Clamp (rotX, -clampAngle, clampAngle);

			Quaternion localRotation = Quaternion.Euler (rotX + transform.rotation.x, rotY + transform.rotation.y, -45);
			transform.rotation = localRotation;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			transform.position = new Vector3(camM.ViewportToWorldPoint(new Vector3(0.9f,0,2)).x,camM.ViewportToWorldPoint(new Vector3(0.9f,0,2)).y,2);
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			transform.position = new Vector3(camM.ViewportToWorldPoint(new Vector3(0.1f,0,2)).x,camM.ViewportToWorldPoint(new Vector3(0.1f,0,2)).y,2);
		}
	}
}
