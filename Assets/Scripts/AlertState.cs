using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : MonoBehaviour {

	private NavMeshAgent agent;
	private MedusaMovement medMovement;
	private MedusaSounds medSounds;
	public float huntTime = 5;
	public float patrolTimeWait;
	private float timer;
	private Transform prey;
	public int alertCounter;
	public int jumpCounter;
	public bool patrolling = true;
	public bool investigating = false;
	public bool hunting = false;
	public float patrolSpeed;
	public float investigateSpeed;
	public float huntSpeed;
	public bool onCeiling = false;
	public GameObject body;


	void Awake()
	{
		agent = gameObject.GetComponent<NavMeshAgent> ();
		medMovement = gameObject.GetComponent<MedusaMovement> ();
		medSounds = gameObject.GetComponent<MedusaSounds> ();
	}
	// Use this for initialization
	void Start () {
		jumpCounter = Random.Range (3, 11);
		agent.speed = patrolSpeed;
		medMovement.Patrol ();
		medSounds.PlayTrack (4);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (agent.destination, transform.position) <= 2) {
			if (patrolling) {
				{
					jumpCounter--;
					if (jumpCounter > 0) {
						medMovement.Patrol ();
						if (!medSounds.IsPlaying()) {
							medSounds.PlayTrack (4);
						}
					} else {
						StartCoroutine (medMovement.Jump (transform.position, onCeiling));
						onCeiling = !onCeiling;
						jumpCounter = Random.Range (3, 11);
						body.transform.localEulerAngles += new Vector3 (0, 0, 180);
					}
				}
			
			} else if (investigating) {
				if (onCeiling) {
					StartCoroutine (medMovement.Jump (transform.position, onCeiling));
					onCeiling = !onCeiling;
					jumpCounter = Random.Range (3, 11);
				} else if (!onCeiling){
					PreyCheck (agent.destination);
				}
			
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
		if (!hunting) {
			if (!medSounds.IsPlaying()) {
				medSounds.PlayTrack (3);
			}
			agent.speed = investigateSpeed;
			patrolling = false;
			investigating = true;
			if (!onCeiling) {
				agent.destination = alertCenter.position;
			} else if (onCeiling) {
				agent.destination = new Vector3 (alertCenter.position.x, 14, alertCenter.position.z);
			}

			if (alertCenter.transform.tag == "Player") {
					alertCounter++;
					Debug.Log ("Alerted");
				}

			if (alertCounter >= 3) {
				prey = alertCenter;
				StartCoroutine (Hunt ());
				hunting = true;
				medSounds.PlayTrack (1);
				Debug.Log ("Hunting");
				alertCounter = 0;
			}
		}
	}

	IEnumerator Hunt()
	{
		timer = huntTime;
		agent.speed = huntSpeed;
		agent.acceleration = 20;
		while (timer > 0) 
		{
			timer -= Time.deltaTime;
			agent.destination = prey.position;
			yield return null;
		}
		Debug.Log ("Not hunting");
		agent.speed = patrolSpeed;
		agent.acceleration = 8;
		hunting = false;
	}

	void PreyCheck(Vector3 place)
	{
		timer = 0;
		Debug.Log ("Checking for prey");
		Collider[] cols = Physics.OverlapSphere (place, 2.5f);
		for (int i = 0; i < cols.Length; i++)
		{
			if (cols [i].tag == "Grabbable") {
				Anger (cols [i].gameObject.GetComponent<Rigidbody> ());
				Debug.Log ("Venting anger");
				medSounds.PlayTrack (0);
			} else if (cols[i].tag == "Player")
			{
				Attack(cols[i].gameObject.GetComponent<PlayerHealth>());
				Debug.Log ("You dead, boy.");
			}
		}
		investigating = false;
		patrolling = true;
		agent.speed = patrolSpeed;
	}

	public void Anger (Rigidbody target)
	{
		target.AddForce (transform.forward * 1000);
	}

	void Attack (PlayerHealth health)
	{
		health.Hurt ();
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.transform.tag == "Grabbable") {
			Anger (col.gameObject.GetComponent<Rigidbody> ());
		}
		if (col.transform.tag == "Player") {
			Attack (col.gameObject.GetComponent<PlayerHealth> ());
		}
	}
}
