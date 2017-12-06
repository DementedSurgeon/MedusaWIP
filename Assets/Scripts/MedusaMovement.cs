using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MedusaMovement : MonoBehaviour {

	private AlertState medBehaviour;

	public float walkRadius;
	public float scanRadius;
	public Transform target;

	public PillarManager pManager;
	public bool onCeiling;
	public int jumpCounter;
	public NavMeshAgent nMA;
	public Vector3 direction;
	Vector3 beamThing;

	void Awake()
	{
		jumpCounter = Random.Range (3, 11);
		nMA = gameObject.GetComponent<NavMeshAgent> ();
		medBehaviour = gameObject.GetComponent<AlertState> ();
		beamThing = new Vector3 (transform.position.x, transform.position.y + 2, transform.position.z);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision col)
	{
		Debug.Log ("Collision");
		medBehaviour.Anger (col.gameObject.GetComponent<Rigidbody> ());
	}

	Vector3 FindNewDirection(Transform newTransform)
	{
		Vector3 newDirection = newTransform.position + Random.onUnitSphere * walkRadius;
		NavMeshHit hit;
		NavMesh.SamplePosition (new Vector3(newDirection.x,0,newDirection.z), out hit, 1f, NavMesh.AllAreas);
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


	
		return newDirection;
	}

	IEnumerator Jump(Vector3 startPos, Vector3 endPos)
	{
		bool wait = false;
		if (onCeiling) {
			RaycastHit hit;
			Collider[] cols;
			if (Physics.Raycast (transform.position, Vector3.up * -1, out hit)) {
				if (hit.transform.tag == "Player") {
					wait = true;
				} else {
					cols = Physics.OverlapSphere (hit.point, scanRadius);
					for (int i = 0; i < cols.Length; i++) {
						if (cols [i].tag == "Player") {
							wait = true;
						}
					}
				}
			}
		}

		if (wait) {
			Debug.Log ("Waiting to jump");
			yield return new WaitForSeconds (5.0f);
		}

		if (nMA.enabled) {
			nMA.enabled = false;
		}
		float t = 0;
		while (t < 1.0f) {
			t += Time.deltaTime * 5;
			transform.position = Vector3.Lerp (startPos, endPos, t);
			transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(180, transform.eulerAngles.y, transform.eulerAngles.z), t);
			yield return null;
		}
		nMA.enabled = true;
		onCeiling = !onCeiling;
		yield return new WaitForSeconds (0.5f);
		Patrol ();
	}

	Vector3 CheckDirection (Vector3 direction)
	{
		int counter = 0;
		bool correcting = false;
		Collider[] cols = Physics.OverlapSphere (direction, scanRadius);
		Debug.DrawLine (beamThing, direction, Color.red, 10f);
		for (int i = 0; i < cols.Length; i++) {
			if (cols [i].transform.tag == "Player") {
				Vector3 heading = direction - transform.position;
				float dirNum = AngleDir(direction, heading, transform.up);
				Debug.Log (dirNum + " d");
				if (dirNum > 0) {
					correcting = true;
					while (correcting) {
						direction = Quaternion.Euler (0, -2, 0) * (direction - transform.position) + transform.position;
						Debug.DrawLine (beamThing, direction, Color.yellow, 10f);
						cols = Physics.OverlapSphere (direction, scanRadius);
						if (cols.Length > 0) {
							for (int c = 0; c < cols.Length; c++) {
								if (cols [c].transform.tag == "Player") {
									c = cols.Length;
								} else if (c == cols.Length - 1 && cols [c].transform.tag != "Player") {
									correcting = false;
								}
							}
						} else {
							correcting = false;
						}
						counter++;
						if (counter > 100) {
							correcting = false;
						}
					}
				} else if (dirNum < 0) {
					correcting = true;
					while (correcting) {
						direction = Quaternion.Euler (0, 2, 0) * (direction - transform.position) + transform.position;
						Debug.DrawLine (beamThing, direction, Color.magenta, 10f);
						cols = Physics.OverlapSphere (direction, scanRadius);
						if (cols.Length > 0) {
							for (int c = 0; c < cols.Length; c++) {
								if (cols [c].transform.tag == "Player") {
									c = cols.Length;
								} else if (c == cols.Length - 1 && cols [c].transform.tag != "Player") {
									correcting = false;
								}
							}
						} else {
							correcting = false;
						}
						counter++;
						if (counter > 100) {
							correcting = false;
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
		int counter = 0;
		bool correcting = false;
		bool furtherCorrecting = false;
		Vector3 fCorrVector;
		int layerMask = 1 << 8;
		RaycastHit hit;
		Debug.DrawLine (transform.position, new Vector3(path.x, transform.position.y, path.z), Color.blue, 10f);
		if (Physics.Linecast(transform.position,new Vector3(path.x, transform.position.y, path.z),out hit, layerMask))
			{
			if (hit.collider.tag == "Player") {
				fCorrVector = hit.point;
				Vector3 heading = path - transform.position;
				float dirNum = AngleDir(path, heading, transform.up);
				Debug.Log (dirNum + " p");
				if (dirNum > 0) {
					correcting = true;
					while (correcting) {
						path = Quaternion.Euler (0, -2, 0) * (path - transform.position) + transform.position;
						Debug.DrawLine (transform.position, new Vector3(path.x, transform.position.y, path.z), Color.cyan, 10f);
						Debug.DrawLine (transform.position, new Vector3(hit.point.x, transform.position.y + 2, hit.point.z), Color.black, 10f);
						if (!Physics.Linecast (transform.position, new Vector3 (path.x, transform.position.y, path.z), out hit, layerMask)) {
							correcting = false;
							furtherCorrecting = true;

						} else {
							fCorrVector = hit.point;
						}
						counter++;
						if (counter > 100) {
							correcting = false;
							counter = 0;
						}
					}
					while (furtherCorrecting) {
						path = Quaternion.Euler (0, -2, 0) * (path - transform.position) + transform.position;
						fCorrVector = Quaternion.Euler (0, -2, 0) * (fCorrVector - transform.position) + transform.position;
						Debug.DrawLine (transform.position, new Vector3(fCorrVector.x, transform.position.y, fCorrVector.z), Color.cyan, 10f);
						Collider[] cols = Physics.OverlapBox(fCorrVector,new Vector3(0.5f,0.5f,0.5f));
						if (cols.Length > 0) {
							for (int c = 0; c < cols.Length; c++) {
								if (cols [c].transform.tag == "Player") {
									c = cols.Length;
								} else if (c == cols.Length - 1 && cols [c].transform.tag != "Player") {
									furtherCorrecting = false;
								}
							}
						} else {
							furtherCorrecting = false;
						}
						counter++;
						if (counter > 100) {
							furtherCorrecting = false;
							counter = 0;
							Debug.Log ("Break");
						}
					}


				} else if (dirNum < 0) {
					correcting = true;
					while (correcting) {
						path = Quaternion.Euler (0, 2, 0) * (path - transform.position) + transform.position;
						Debug.DrawLine (transform.position, new Vector3(path.x, transform.position.y, path.z), Color.green, 10f);
						if (!Physics.Linecast (transform.position, new Vector3 (path.x, transform.position.y, path.z), out hit, layerMask)) {
							correcting = false;
							furtherCorrecting = true;
						} else {
							fCorrVector = hit.point;
						}
						counter++;
						if (counter > 100) {
							correcting = false;
						}
					}
					while (furtherCorrecting) {
						path = Quaternion.Euler (0, 2, 0) * (path - transform.position) + transform.position;
						fCorrVector = Quaternion.Euler (0, 2, 0) * (fCorrVector - transform.position) + transform.position;
						Debug.DrawLine (transform.position, new Vector3 (fCorrVector.x, transform.position.y, fCorrVector.z), Color.green, 10f);
						Collider[] cols = Physics.OverlapBox (fCorrVector, new Vector3 (0.5f, 0.5f, 0.5f));
						if (cols.Length > 0) {
							for (int c = 0; c < cols.Length; c++) {
								if (cols [c].transform.tag == "Player") {
									c = cols.Length;
								} else if (c == cols.Length - 1 && cols [c].transform.tag != "Player") {
									furtherCorrecting = false;
								}
							}
						} else {
							furtherCorrecting = false;
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

	public void Patrol()
	{
		Vector3 newDirection = FindNewDirection (pManager.GetPillar());
		beamThing = new Vector3 (transform.position.x, 0, transform.position.z);
		direction = newDirection;
		direction = ValidatePath (direction);
		direction = CheckDirection (direction);
		Debug.Log (direction);
		Debug.Log (jumpCounter);
		nMA.destination = direction;
		jumpCounter--;
		if (jumpCounter == 0)
		{
			if (!onCeiling) {
				StartCoroutine(Jump (transform.position, new Vector3(transform.position.x, 14, transform.position.z)));
			} else if (onCeiling) {
				StartCoroutine(Jump (transform.position, new Vector3(transform.position.x, 0, transform.position.z)));
			}
			jumpCounter = Random.Range (3, 11);
		}
	}
}
