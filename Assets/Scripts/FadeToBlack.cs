using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour {

	private Image black;

	// Use this for initialization
	void Start () {
		black = gameObject.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Fader()
	{
		Color temp = black.color;
		float t = 0;
		while (t < 1.0f)
		{
			t += Time.deltaTime;
			temp.a = Mathf.Lerp (0, 1, t);
			black.color = temp;
			yield return null;
		}
	}

	public void Fade()
	{
		StartCoroutine (Fader ());
	}
}
