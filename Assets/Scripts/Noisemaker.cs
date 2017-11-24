using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour {

	private Rigidbody rBody;
	private Collider[] cols;

	// Use this for initialization
	void Start () {
		rBody = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.tag == "Terrain") {
			if (rBody.velocity.x > 0 || rBody.velocity.y > 0 || rBody.velocity.z > 0) {
				cols = Physics.OverlapSphere (transform.position, 50f);
				for (int i = 0; i < cols.Length; i++) {
					if (cols [i].gameObject.tag == "Medusa") {
						cols [i].GetComponent<AlertState> ().Alert (transform);
					}
				}

			}
		}
	}

}
