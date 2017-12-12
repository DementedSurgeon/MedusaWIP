using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int health;
	public delegate void myDelegate();
	public myDelegate OnDeath;

	// Use this for initialization
	void Start () {
		OnDeath += Die;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Hurt()
	{
		health--;
		if (health <= 0)
		{
			if (OnDeath != null) {
				OnDeath ();
			}
		}
	}

	private void Die()
	{
		Debug.Log ("Player is dead");
	}
}
