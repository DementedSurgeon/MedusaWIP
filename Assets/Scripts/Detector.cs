using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour {

	public Transform target;
	public GameObject mirror;
	public GlyphManager gManager;
	private MeshRenderer upCube;
	private MeshRenderer upRightCube;
	private MeshRenderer rightCube;
	private MeshRenderer downRightCube;
	private MeshRenderer downCube;
	private MeshRenderer downLeftCube;
	private MeshRenderer leftCube;
	private MeshRenderer upLeftCube;

	// Use this for initialization
	void Start () {
		
		upCube = mirror.transform.Find ("upCube").GetComponent<MeshRenderer> ();
		upRightCube = mirror.transform.Find ("upRightCube").GetComponent<MeshRenderer> ();
		rightCube = mirror.transform.Find ("rightCube").GetComponent<MeshRenderer> ();
		downRightCube = mirror.transform.Find ("downRightCube").GetComponent<MeshRenderer> ();
		downCube = mirror.transform.Find ("downCube").GetComponent<MeshRenderer> ();
		downLeftCube = mirror.transform.Find ("downLeftCube").GetComponent<MeshRenderer> ();
		leftCube = mirror.transform.Find ("leftCube").GetComponent<MeshRenderer> ();
		upLeftCube = mirror.transform.Find ("upLeftCube").GetComponent<MeshRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GlyphManager.activeGlyph >= 0) {
			target = gManager.GetActiveGlyph ();
		} else {
			this.enabled = false;
		}
		Vector3 targetDir = target.position - transform.position;
		float up = Vector3.Angle(targetDir, transform.forward);
		Vector3 vupRight = Quaternion.Euler (0, 45, 0) * transform.forward;
		float upRight = Vector3.Angle(targetDir, vupRight);
		float right = Vector3.Angle(targetDir, transform.right);
		Vector3 vdownRight = Quaternion.Euler (0, 45, 0) * transform.right;
		float downRight = Vector3.Angle (targetDir, vdownRight);
		float down = Vector3.Angle(targetDir, transform.forward * -1);
		Vector3 vdownLeft = Quaternion.Euler (0, 45, 0) * (transform.forward * -1);
		float downLeft = Vector3.Angle (targetDir, vdownLeft);
		float left = Vector3.Angle(targetDir, transform.right * -1);
		Vector3 vupLeft = Quaternion.Euler (0, 45, 0) * (transform.right * -1);
		float upLeft = Vector3.Angle (targetDir, vupLeft);

		if (Vector3.Distance (transform.position, target.position) > 20) {
			if (up <= 22.5f) {
				upCube.material.color = Color.red;
			} else {
				upCube.material.color = Color.black;
			}

			if (upRight <= 22.5f) {
				upRightCube.material.color = Color.red;
			} else {
				upRightCube.material.color = Color.black;
			}

			if (right <= 22.5f) {
				rightCube.material.color = Color.red;
			} else {
				rightCube.material.color = Color.black;
			}

			if (downRight <= 22.5f) {
				downRightCube.material.color = Color.red;
			} else {
				downRightCube.material.color = Color.black;
			}

			if (down <= 22.5f) {
				downCube.material.color = Color.red;
			} else {
				downCube.material.color = Color.black;
			}

			if (downLeft <= 22.5f) {
				downLeftCube.material.color = Color.red;
			} else {
				downLeftCube.material.color = Color.black;
			}

			if (left <= 22.5f) {
				leftCube.material.color = Color.red;
			} else {
				leftCube.material.color = Color.black;
			}

			if (upLeft <= 22.5f) {
				upLeftCube.material.color = Color.red;
			} else {
				upLeftCube.material.color = Color.black;
			}
		} else {
			upCube.material.color = Color.black;
			upRightCube.material.color = Color.black;
			rightCube.material.color = Color.black;
			downRightCube.material.color = Color.black;
			downCube.material.color = Color.black;
			downLeftCube.material.color = Color.black;
			leftCube.material.color = Color.black;
			upLeftCube.material.color = Color.black;
		}
			
	}
}
