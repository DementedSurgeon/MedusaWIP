using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MedusaBehaviour : MonoBehaviour {

	public float walkRadius;
	public float scanRadius;
	public float timerDelay = 1.5f;
	public Transform target;

	public Vector3 patrol1;
	public Vector3 patrol2;
	public Vector3 patrol3;

	private Vector3[] patrolLocations = new Vector3[3];

	private float timer;
	private int counter = 0;
	private NavMeshAgent nMA;
	Vector3 direction;
	Vector3 beamThing;

	// Use this for initialization
	void Start () {
		timer = timerDelay;
		nMA = gameObject.GetComponent<NavMeshAgent> ();
		patrolLocations [0] = patrol1;
		patrolLocations [1] = patrol2;
		patrolLocations [2] = patrol3;
		beamThing = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			Patrol ();
		}
		//Debug.Log (scanning);
		if (Vector3.Distance (transform.position, direction) <= 1) {
			if (timer > 0) {
				timer -= Time.deltaTime;
				if (timer <= 0) {
					Patrol ();
					timer = timerDelay;
				}
			}
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

	Vector3 FindNewDirection()
	{
		Vector3 newDirection = transform.position + Random.onUnitSphere * 20f;
		NavMeshHit hit;
		NavMesh.SamplePosition (new Vector3(newDirection.x,transform.position.y,newDirection.z), out hit, 20f, NavMesh.AllAreas);
		for (int i = 0; i < 100; i++) {
			if (Vector3.Distance (transform.position, hit.position) >= 5.0f) {
				i = 100;
			} else {
				NavMesh.SamplePosition (new Vector3(newDirection.x,0,newDirection.z), out hit, 1.0f, NavMesh.AllAreas);
				Debug.DrawRay (hit.position, Vector3.up, Color.red, 3f);
			}
		}
		newDirection = hit.position;
		if (newDirection.x == Mathf.Infinity && newDirection.y == Mathf.Infinity && newDirection.z == Mathf.Infinity) {
			Vector3 temp = new Vector3 (0, 0, 0);
			newDirection = temp;
		}



		Debug.Log (newDirection);
		return newDirection;
	}

	Vector3 CheckDirection (Vector3 direction)
	{
		bool correcting = false;
		Collider[] cols = Physics.OverlapSphere (direction, scanRadius);
		Debug.DrawLine (beamThing, direction, Color.red, 10f);
		for (int i = 0; i < cols.Length; i++) {
			if (cols [i].transform.tag == "Player") {
				Vector3 heading = cols [i].transform.position - direction;
				float dirNum = AngleDir(direction, heading, transform.up);
				if (dirNum > 0) {
					correcting = true;
					while (correcting) {
						direction = Quaternion.Euler (0, -10, 0) * direction;
						Debug.DrawLine (beamThing, direction, Color.yellow, 10f);
						cols = Physics.OverlapSphere (direction, scanRadius);
						for (int c = 0; c < cols.Length; c++) {
							if (cols [c].transform.tag == "Player") {
								c = cols.Length;
							} else if (c == cols.Length - 1 && cols [c].transform.tag != "Player") {
								correcting = false;
							}
						}
					}
				} else if (dirNum < 0) {
					correcting = true;
					while (correcting) {
						direction = Quaternion.Euler (0, 10, 0) * direction;
						Debug.DrawLine (beamThing, direction, Color.magenta, 10f);
						cols = Physics.OverlapSphere (direction, scanRadius);
						for (int c = 0; c < cols.Length; c++) {
							if (cols [c].transform.tag == "Player") {
								c = cols.Length;
							} else if (c == cols.Length - 1 && cols [c].transform.tag != "Player") {
								correcting = false;
							}
						}
					}
				} else {
					direction = Quaternion.Euler(0, -10, 0) * direction;
					Debug.DrawLine (beamThing, direction, Color.white, 10f);
				}
			}
		}
		return direction;
	}

	Vector3 ValidatePath (Vector3 path)
	{
		bool correcting = false;
		int layerMask = 1 << 8;
		RaycastHit hit;
		Debug.DrawLine (transform.position, new Vector3(path.x, transform.position.y, path.z), Color.blue, 10f);
		if (Physics.Linecast(transform.position,new Vector3(path.x, transform.position.y, path.z),out hit, layerMask))
		{
			Debug.Log ("Working");
			if (hit.collider.tag == "Player") {
				Vector3 heading = hit.transform.position - path;
				float dirNum = AngleDir(path, heading, transform.up);
				if (dirNum > 0) {
					correcting = true;
					while (correcting) {
						path = Quaternion.Euler(0, -10, 0) * path;
						Debug.DrawLine (transform.position, new Vector3(path.x, transform.position.y, path.z), Color.cyan, 10f);
						if (!Physics.Linecast (transform.position, new Vector3 (path.x, transform.position.y, path.z), out hit, layerMask)) {
							correcting = false;
						}
					}


				} else if (dirNum < 0) {
					correcting = true;
					while (correcting) {
						path = Quaternion.Euler(0, 10, 0) * path;
						Debug.DrawLine (transform.position, new Vector3(path.x, transform.position.y, path.z), Color.green, 10f);
						if (!Physics.Linecast (transform.position, new Vector3 (path.x, transform.position.y, path.z), out hit, layerMask)) {
							correcting = false;
						}
					}
				} else {
					path = Quaternion.Euler(0, -10, 0) * path;
					Debug.DrawLine (transform.position, new Vector3(path.x, transform.position.y, path.z), Color.grey, 10f);
				}
			}


		}
		return path;
	}

	float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);

		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
	}

	void Patrol()
	{
		Vector3 newDirection = FindNewDirection ();
		NavMeshHit hit;
		NavMesh.SamplePosition (new Vector3(newDirection.x,transform.position.y,newDirection.z), out hit, 1f, NavMesh.AllAreas);
		beamThing = new Vector3 (transform.position.x, 0, transform.position.z);
		direction = hit.position;
		Debug.Log (direction);
		direction = ValidatePath (direction);
		direction = CheckDirection (direction);
		nMA.destination = direction;
	}
}
