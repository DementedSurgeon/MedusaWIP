using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public bool isOpen = false;
	public bool isLocked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator OpenClose()
	{
		float t = 0;
		while (t < 1.0f) {
			t += Time.deltaTime;
			if (!isOpen) {
				transform.localEulerAngles = Vector3.Lerp (transform.localEulerAngles, new Vector3 (transform.localEulerAngles.x, 300, transform.localEulerAngles.z), t);
			} else if (isOpen) {
				transform.localEulerAngles = Vector3.Lerp (transform.localEulerAngles, new Vector3 (transform.localEulerAngles.x, 180, transform.localEulerAngles.z), t);
			}
			yield return null;
		}
		isOpen = !isOpen;

	}

	public void InteractWithDoor()
	{
		if (!isLocked) {
			StartCoroutine (OpenClose ());
		}
	}
}
