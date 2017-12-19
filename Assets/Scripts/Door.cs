using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public bool isOpen = false;
	public bool isLocked = true;
	public AudioClip[] doorSounds;
	private AudioSource aSource;

	// Use this for initialization
	void Start () {
		aSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator OpenClose()
	{
		if (!isOpen) {
			aSource.clip = doorSounds [0];
			aSource.Play();
		} else if (isOpen) {
			aSource.clip = doorSounds [1];
			aSource.Play();
		}
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
		} else {
			aSource.clip = doorSounds [2];
			aSource.Play();
		}
	}

	public void LockUnlockDoor()
	{
		isLocked = !isLocked;
		aSource.clip = doorSounds [2];
		aSource.Play();
	}
}
