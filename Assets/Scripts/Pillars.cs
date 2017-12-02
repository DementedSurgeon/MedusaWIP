using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillars : MonoBehaviour {

	public List<Transform> patrolPositions;
	public bool active = false;

	void Awake()
	{
		patrolPositions = new List<Transform> ();
		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			if (hit.transform.tag == "PosPillars") {
				patrolPositions.Add (hit.transform);
			}
		}
		if (Physics.Raycast (transform.position, transform.right, out hit)) {
			if (hit.transform.tag == "PosPillars") {
				patrolPositions.Add (hit.transform);
			}
		}
		if (Physics.Raycast (transform.position, transform.forward * -1, out hit)) {
			Debug.DrawLine (transform.position, hit.transform.position, Color.red, 10f);
			if (hit.transform.tag == "PosPillars") {
				patrolPositions.Add (hit.transform);
			}
		}
		if (Physics.Raycast (transform.position, transform.right * -1, out hit)) {
			Debug.DrawLine (transform.position, hit.transform.position, Color.blue, 10f);
			if (hit.transform.tag == "PosPillars") {
				patrolPositions.Add (hit.transform);
			}
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider col)
	{
		if (col.tag == "Player") {
			gameObject.GetComponent<MeshRenderer> ().material.color = Color.red;
			active = true;
			foreach (Transform i in patrolPositions) {
				i.GetComponent<MeshRenderer> ().material.color = Color.red;
				i.GetComponent<Pillars> ().active = false;
			}
		}
	}

	void OnTriggerExit(Collider col)
	{
		if (col.tag == "Player") {
			gameObject.GetComponent<MeshRenderer> ().material.color = Color.black;

			foreach (Transform i in patrolPositions) {
				i.GetComponent<MeshRenderer> ().material.color = Color.black;

			}
		}
	}


}
