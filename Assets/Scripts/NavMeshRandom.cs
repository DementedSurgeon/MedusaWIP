using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRandom : MonoBehaviour {

	private NavMeshAgent agent;

	void Start()
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.N)) {
			Patrol ();
		}
	}

	void Patrol()
	{
		Vector3 result;
		Vector3 randomPoint = transform.position + Random.insideUnitSphere * 10;
		NavMeshHit hit;
		NavMesh.SamplePosition (randomPoint, out hit, 1, NavMesh.AllAreas);
		result = hit.position;
		agent.destination = result;
		Debug.DrawRay (result, Vector3.up, Color.blue, 1.0f);
	}

}
