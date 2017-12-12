using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMirrorRot : MonoBehaviour {

	public float mouseSensitivity = 100.0f;
	//public float clampAngle = 80.0f;

	private float rotY = 0.0f; // rotation around the up/y axis
	private float mouseX;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		mouseX = -Input.GetAxis ("Mouse X");

		rotY = mouseX * mouseSensitivity * Time.deltaTime;

		Vector3 localRotation = new Vector3 (0, rotY, 0);
		transform.localEulerAngles += localRotation;
		transform.localEulerAngles = new Vector3 (0, Mathf.Clamp (transform.localEulerAngles.y, 150, 210), 0);
	}
}
