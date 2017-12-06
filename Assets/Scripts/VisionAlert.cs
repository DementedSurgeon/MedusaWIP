using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionAlert : MonoBehaviour {

	private Camera main;
	public AlertState target;
	private Vector3 targetPos;
	private float timer;
	private float otherTimer;


	// Use this for initialization
	void Start () {
		main = gameObject.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Vector3.Distance(transform.position, target.transform.position));
		targetPos = target.transform.position;
		targetPos = main.WorldToViewportPoint (targetPos);
		if (Vector3.Distance (targetPos, transform.position) < 100) {
			if (targetPos.x > 0.2f && targetPos.x < 0.8f && targetPos.y > 0.2f && targetPos.y < 0.8f && targetPos.z > 0) {
				timer += Time.deltaTime;
				otherTimer = 0;
			} else {
				otherTimer += Time.deltaTime;
				if (otherTimer >= 3) {
					timer = 0;
					otherTimer = 0;
				}
			}
		}
		if (timer >= 3) {
			target.Alert (transform);
			timer = 0;
		}
	}
}
