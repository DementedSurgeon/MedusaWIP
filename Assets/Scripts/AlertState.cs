using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : MonoBehaviour {

	private NavMeshAgent agent;
	private bool hunting = false;
	private float huntTime = 5;
	private float timer;
	private Transform prey;
	private int counter;
	private bool investigating = false;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hunting) {
			Hunt ();
		} else if (investigating) {
			Debug.Log (Vector3.Distance (transform.position, agent.destination));
			if (Vector3.Distance (transform.position, agent.destination) <= 2) {
				PreyCheck (agent.destination);
			}
		}
	}

	public void Alert(Transform alertCenter)
	{
		investigating = true;
		agent.destination = alertCenter.position;

		if (alertCenter.transform.parent != null) {
			if (alertCenter.transform.parent.tag == "Player") {
				counter++;
			}
		}
		if (counter >= 3) {
			prey = alertCenter;
			Hunt ();
			counter = 0;
		}
	}

	void Hunt()
	{
		if (!hunting) {
			hunting = true;
			timer = huntTime;
			agent.speed = 10;
		} 
		if (hunting) {
			agent.destination = prey.position;
			timer -= Time.deltaTime;
			if (timer <= 0) {
				hunting = false;
				agent.speed = 3.5f;
			}
		}
	}

	void PreyCheck(Vector3 place)
	{
		Debug.Log ("Checking for prey");
		Collider[] cols = Physics.OverlapSphere (place, 2.5f);
		for (int i = 0; i < cols.Length; i++)
		{
			if (cols [i].tag == "Grabbable") {
				Anger (cols [i].gameObject.GetComponent<Rigidbody> ());
				Debug.Log ("Venting anger");
			} else if (cols[i].tag == "Player")
			{
				Debug.Log ("You dead, boy.");
			}
		}
		investigating = false;
	}

	void Anger (Rigidbody target)
	{
		target.AddForce (transform.forward * 1000);
	}
}
