using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Targeting : MonoBehaviour {

	private NavMeshAgent nMA;
	public Transform target;
	// Use this for initialization
	void Start () {
		nMA = gameObject.GetComponent<NavMeshAgent> ();
		nMA.destination = target.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (nMA.destination != target.position) {
			nMA.destination = target.position;
		}
	}
}
