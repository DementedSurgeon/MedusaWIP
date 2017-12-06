using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : MonoBehaviour {

	private NavMeshAgent agent;
	private MedusaMovement medMovement;
	private bool hunting = false;
	private float huntTime = 5;
	public float patrolTimeWait;
	private float timer;
	private Transform prey;
	private int counter;
	private bool patrolling = true;
	private bool investigating = false;


	void Awake()
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();
		medMovement = gameObject.GetComponent<MedusaMovement> ();

	}
	// Use this for initialization
	void Start () {
		medMovement.Patrol ();
	}
	
	// Update is called once per frame
	void Update () {
		if (patrolling) {
			if (Vector3.Distance(agent.destination, transform.position) <= 2)
				{
				medMovement.Patrol ();
				}
			
		}
		else if (investigating) {
			//Debug.Log (Vector3.Distance (transform.position, agent.destination));
			if (Vector3.Distance (transform.position, agent.destination) <= 2) {
				PreyCheck (agent.destination);
			}
		}
	}

	IEnumerator PatrolWait()
	{
		yield return new WaitForSeconds (patrolTimeWait);
		medMovement.Patrol ();
	}

	public void Alert(Transform alertCenter)
	{
		investigating = true;
		agent.destination = alertCenter.position;

		if (alertCenter.transform.parent != null) {
			if (alertCenter.transform.parent.tag == "Player") {
				counter++;
				Debug.Log ("Alerted");
			}
		}
		if (counter >= 3) {
			prey = alertCenter;
			StartCoroutine(Hunt ());
			Debug.Log ("Hunting");
			counter = 0;
		}
	}

	IEnumerator Hunt()
	{
		timer = huntTime;
		agent.speed = 10;
		while (timer > 0) 
		{
			timer -= Time.deltaTime;
			agent.destination = prey.position;
			yield return null;

		}
		agent.speed = 3.5f;
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

	public void Anger (Rigidbody target)
	{
		target.AddForce (transform.forward * 1000);
	}

	void Attack ()
	{

	}
}
