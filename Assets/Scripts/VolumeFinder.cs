using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeFinder : MonoBehaviour {

	private BoxCollider bCollider;
	private Vector3 upRight;
	private Vector3 downRight;
	private Vector3 upLeft;
	private Vector3 downLeft;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FindCorners()
	{
		upRight.x += bCollider.size.x / 2;
		upRight.z += bCollider.size.z / 2;

		downRight.x += bCollider.size.x / 2;
		downRight.z -= bCollider.size.z / 2;

		upLeft.x -= bCollider.size.x / 2;
		upLeft.z += bCollider.size.z / 2;

		downLeft.x -= bCollider.size.x / 2;
		downLeft.z -= bCollider.size.z / 2;
	}
}
