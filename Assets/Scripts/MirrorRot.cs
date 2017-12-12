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
	private bool angled = false;

	void Start ()
	{
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;

	}

	void Update ()
	{
		if (camM.transform.localEulerAngles.x > 30 && camM.transform.localEulerAngles.x < 90 && !angled) {
			StartCoroutine (Turn(transform.localEulerAngles, new Vector3(-45, transform.localEulerAngles.y, transform.localEulerAngles.z)));
			angled = true;
		} 
		if (camM.transform.localEulerAngles.x < 30 && angled) {
			StartCoroutine (Turn(transform.localEulerAngles, new Vector3(360, transform.localEulerAngles.y, transform.localEulerAngles.z)));
			angled = false;
		}


		if (Input.GetMouseButton (1)) {
			
			mouseX = -Input.GetAxis ("Mouse X");

			rotY = mouseX * mouseSensitivity * Time.deltaTime;

			Vector3 localRotation = new Vector3 (0, rotY, 0);
			transform.localEulerAngles += localRotation;
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			StartCoroutine (Side (transform.localPosition, new Vector3(1.3f, transform.localPosition.y, transform.localPosition.z)));
			StartCoroutine (Turn(transform.localEulerAngles, new Vector3(transform.transform.localEulerAngles.x, 180, transform.localEulerAngles.z)));
		}

		if (Input.GetKeyDown (KeyCode.Q)) {
			StartCoroutine (Side (transform.localPosition, new Vector3(-1.3f, transform.localPosition.y, transform.localPosition.z)));
			StartCoroutine (Turn(transform.localEulerAngles, new Vector3(transform.localEulerAngles.x, 180, transform.localEulerAngles.z)));
		}
	}

	IEnumerator Side(Vector3 startPos, Vector3 endPos)
	{
		float t = 0;
		while (t < 1.0f) {
			t += Time.deltaTime * 5;
			transform.localPosition = Vector3.Lerp(new Vector3(startPos.x, transform.localPosition.y, transform.localPosition.z), new Vector3(endPos.x, transform.localPosition.y, transform.localPosition.z), t);
			yield return null;
		}

	}

	IEnumerator Turn (Vector3 startPos, Vector3 endPos)
	{
		float t = 0;
		while (t < 1.0f) {
		t += Time.deltaTime * 5;
			transform.localEulerAngles = Vector3.Lerp(new Vector3(startPos.x, startPos.y, startPos.z), new Vector3(endPos.x, endPos.y, endPos.z), t);
			yield return null;
			}
	}

}	
