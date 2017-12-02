using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRot : MonoBehaviour {

	public float mouseSensitivity = 100.0f;
	//public float clampAngle = 80.0f;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float rotX = 0.0f; // rotation around the right/x axis
	public Camera camM;
	private float mouseX;
	private float mouseY;

	void Start ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
	}

	void Update ()
	{
		Debug.Log (camM.transform.localEulerAngles.x);
		if (camM.transform.localEulerAngles.x > 30 && camM.transform.localEulerAngles.x < 90) {
			transform.localEulerAngles = new Vector3 (-45, transform.localEulerAngles.y, transform.localEulerAngles.z);
		} else if (camM.transform.localEulerAngles.x < 30) {
			transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y, transform.localEulerAngles.z);
		}


		if (Input.GetMouseButton (1)) {
			
			mouseX = -Input.GetAxis ("Mouse X");

			rotY += mouseX * mouseSensitivity * Time.deltaTime;


			//rotX = Mathf.Clamp (rotX, -clampAngle, clampAngle);

			Vector3 localRotation = new Vector3 (0, rotY, -45);
			transform.localEulerAngles = localRotation;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			transform.localPosition = new Vector3(1.3f, transform.localPosition.y, transform.localPosition.z);
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			transform.localPosition = new Vector3(-1.3f, transform.localPosition.y, transform.localPosition.z);
			transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
		}
	}
}
