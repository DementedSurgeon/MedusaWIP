using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walking : MonoBehaviour {

	public float walkRadius;
	public float scanRadius;
	public float timerDelay = 1.5f;

	public Vector3 patrol1;
	public Vector3 patrol2;
	public Vector3 patrol3;

	private Vector3[] patrolLocations = new Vector3[3];

	private float timer;
	private int counter = 0;
	private NavMeshAgent nMA;
	private int reverser = -1;
	private bool scanning = false;

	// Use this for initialization
	void Start () {
		timer = timerDelay;
		nMA = gameObject.GetComponent<NavMeshAgent> ();
		patrolLocations [0] = patrol1;
		patrolLocations [1] = patrol2;
		patrolLocations [2] = patrol3;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (scanning);
		timer -= Time.deltaTime;
		if (Input.GetKeyDown(KeyCode.Space)) {
			timer = timerDelay;
			Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
			randomDirection += patrolLocations[counter];
			NavMeshHit hit;
			NavMesh.SamplePosition(randomDirection, out hit, walkRadius, NavMesh.AllAreas);
			Vector3 finalPosition = hit.position;
			Collider[] impact = Physics.OverlapSphere (finalPosition, scanRadius);
			for (int i = 0; i < impact.Length; i++) {
				if (impact [i].gameObject.name == "Player") {
					finalPosition = Quaternion.Euler (0, -i  * 5 * reverser, 0) * finalPosition;
					impact = Physics.OverlapSphere (finalPosition, scanRadius);
					i = 0;

				}
			}
			RaycastHit result1;
			RaycastHit result2;
			Physics.Linecast(transform.position, finalPosition, out result1);
			if (!(result1.collider == null)) {
				if (result1.collider.gameObject.name == "Player") {
					scanning = true;
				}
			}
			while (scanning) {
				int scanCounter = 1;
				Vector3 scanPosition1 = Quaternion.Euler(0, scanCounter * 5, 0) * finalPosition;
				Vector3 scanPosition2 = Quaternion.Euler(0, -scanCounter * 5, 0) * finalPosition;
				Physics.Linecast (transform.position, scanPosition1, out result1);
				Physics.Linecast (transform.position, scanPosition2, out result2);
				if (result1.collider == null) {
					impact = Physics.OverlapSphere (scanPosition1, scanRadius);
					for (int i = 0; i < impact.Length; i++) {
						if (i == impact.Length - 1 && !(impact [i].gameObject.name == "Player")) {
							scanning = false;
							finalPosition = scanPosition1;
						}
					}
				}
				else if (result2.collider == null) {
					impact = Physics.OverlapSphere (scanPosition2, scanRadius);
					for (int i = 0; i < impact.Length; i++) {
						if (i == impact.Length - 1 && !(impact [i].gameObject.name == "Player")) {
							scanning = false;
							finalPosition = scanPosition2;
						}
					}
				}
				else if (!(result1.collider == null)) {
					if (!(result1.collider.name == "Player")) {
						impact = Physics.OverlapSphere (scanPosition1, scanRadius);
						for (int i = 0; i < impact.Length; i++) {
							if (i == impact.Length - 1 && !(impact [i].gameObject.name == "Player")) {
								scanning = false;
								finalPosition = scanPosition1;
							}
						}
					} 
				}else if (!(result2.collider == null)) { 
					if (!(result2.collider.name == "Player")) {
						impact = Physics.OverlapSphere (scanPosition2, scanRadius);
						for (int i = 0; i < impact.Length; i++) {
							if (i == impact.Length - 1 && !(impact [i].gameObject.name == "Player")) {
								scanning = false;
								finalPosition = scanPosition2;
							}
						}
					}
				} 	
				scanCounter++;
			}
			nMA.destination = finalPosition;
			Debug.DrawRay (finalPosition, Vector3.up, Color.red, 1.0f);
			counter++;
			if (!(counter < patrolLocations.Length))
			{
				counter = 0;
			}
			reverser = reverser * -1;
		}
	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log ("Collision");
		GameObject impact;
		impact = col.gameObject;
		if (impact.GetComponent<NavMeshObstacle> ().enabled == false) {
			impact.gameObject.GetComponent<Rigidbody> ().AddForce (nMA.destination * 100000000);
		}
	}
}
