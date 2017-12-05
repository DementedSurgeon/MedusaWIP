using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphPlacer : MonoBehaviour {

	public GameObject prefab;

	public GameObject[] clusters;
	public GameObject[] glyphSpots;
	private GameObject[] glyphs;
	private BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		glyphSpots = new GameObject[clusters.Length];
		glyphs = new GameObject[glyphSpots.Length];
		for (int i = 0; i < clusters.Length; i++) {
			glyphSpots[i] = clusters[i].transform.GetChild(Random.Range(0,clusters[i].transform.childCount)).gameObject;
			glyphs[i] = Instantiate(prefab,glyphSpots[i].transform.position,glyphSpots[i].transform.rotation);

			glyphs[i].transform.SetParent(glyphSpots[i].transform);
			glyphs[i].transform.localPosition += new Vector3 (0, 0, 0.5f);

			if (glyphSpots [i].tag == "PosPillars") {
				glyphs [i].transform.position = new Vector3 (glyphs [i].transform.position.x, Random.Range (1, 3), glyphs [i].transform.position.z);
				glyphSpots [i].transform.eulerAngles = new Vector3 (glyphSpots [i].transform.eulerAngles.x, Random.Range (0, 4) * 90, glyphSpots [i].transform.eulerAngles.z);
			} else if (glyphSpots [i].tag == "Terrain") {
				//
				// -padre.y /2, padre.y/2  ( si es 10, me da de -5  a 5)
				// lo multiplico por mi escala
				glyphs [i].transform.localPosition = new Vector3 ((Random.Range (-glyphSpots [i].transform.localScale.x / 2, glyphSpots [i].transform.localScale.x / 2)) * glyphs [i].transform.localScale.x, (Random.Range (-glyphSpots [i].transform.localScale.y / 2, -glyphSpots [i].transform.localScale.y / 2 + 1)) * glyphs [i].transform.localScale.y, glyphs [i].transform.localPosition.z);
			} else {
				boxCollider = glyphSpots [i].GetComponent<BoxCollider> ();
				Debug.Log (boxCollider.size.ToString(), boxCollider.gameObject);
				glyphs [i].transform.localPosition = new Vector3 (0, 0, boxCollider.size.z/2);
			}
			/*glyphs[i].transform.localScale = new Vector3(glyphs[i].transform.localScale.x / glyphSpots[i].transform.localScale.x,
				glyphs[i].transform.localScale.y / glyphSpots[i].transform.localScale.y, 
				glyphs[i].transform.localScale.z / glyphSpots[i].transform.localScale.z);*/
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
