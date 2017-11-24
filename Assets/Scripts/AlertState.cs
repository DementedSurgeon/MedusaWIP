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

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hunting) {
			Hunt ();
		}
	}

	public void Alert(Transform alertCenter)
	{
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
}
