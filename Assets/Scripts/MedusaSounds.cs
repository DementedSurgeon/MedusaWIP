using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaSounds : MonoBehaviour {

	private AudioSource aSource;
	public AudioClip[] clips;

	void Awake()
	{
		aSource = gameObject.GetComponent<AudioSource> ();
	}
	 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayTrack(int track)
	{
		aSource.clip = clips [track];
		aSource.Play ();
	}

	public bool IsPlaying()
	{
		if (aSource.isPlaying) {
			return true;
		} else {
			return false;
		}
	}
}
