using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour {

	private AudioSource aSource;
	private Rigidbody rBody;
	private Collider[] cols;
	public string label = "None";
	public Transform thrower;

	// Use this for initialization
	void Start () {
		rBody = gameObject.GetComponent<Rigidbody> ();
		aSource = gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Terrain" && label == "Player") {
			if (rBody.velocity.x > 0 || rBody.velocity.y > 0 || rBody.velocity.z > 0) {
				label = "None";
				thrower = null;
				cols = Physics.OverlapSphere (transform.position, 50f);
				for (int i = 0; i < cols.Length; i++) {
					if (cols [i].gameObject.tag == "Medusa") {
						cols [i].GetComponent<AlertState> ().Alert (transform);
					}
				}
				aSource.Play ();
			}
		} else if (col.gameObject.tag == "Medusa" && label == "Player") {
			col.gameObject.GetComponent<AlertState> ().Alert (thrower);
			label = "None";
		}

	}

}
