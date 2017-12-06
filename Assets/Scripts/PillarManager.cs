using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour {

	public Pillars[] pillars;
	private int activePillar = 0;

	// Use this for initialization
	void Start () {
		UpdateState ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateState()
	{
		for (int i = 0; i < pillars.Length; i++) {
			if (pillars [i].active == true) {
				activePillar = i;
			}
		}
	}

	public Transform GetPillar()
	{
		UpdateState ();
		Transform temp = pillars[activePillar].patrolPositions[Random.Range(0,pillars[activePillar].patrolPositions.Count)];
		return temp;
	}
}
