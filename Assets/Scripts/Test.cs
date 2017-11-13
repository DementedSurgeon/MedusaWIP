using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test : MonoBehaviour {

	private NavMeshObstacle nMO;
	// Use this for initialization
	void Start () {
		nMO = gameObject.GetComponent<NavMeshObstacle> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			if (nMO.enabled == false)
				nMO.enabled = true;
			else
				nMO.enabled = false;
		}
	}
}
