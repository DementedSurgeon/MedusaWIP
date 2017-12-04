using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphPlacer : MonoBehaviour {

	public GameObject prefab;

	public GameObject[] clusters;
	public GameObject[] glyphSpots;
	private GameObject[] glyphs;

	// Use this for initialization
	void Start () {
		glyphSpots = new GameObject[clusters.Length];
		glyphs = new GameObject[glyphSpots.Length];
		for (int i = 0; i < clusters.Length; i++) {
			glyphSpots[i] = clusters[i].transform.GetChild(Random.Range(0,clusters[i].transform.childCount)).gameObject;
			glyphs[i] = Instantiate(prefab,glyphSpots[i].transform.position,glyphSpots[i].transform.rotation, glyphSpots[i].transform);
			glyphs[i].transform.localPosition += new Vector3 (0, 0,  glyphs[i].transform.localPosition.z + 0.5f);
			if (glyphSpots [i].tag == "PosPillars") {
				glyphs [i].transform.position = new Vector3 (glyphs [i].transform.position.x, Random.Range (1, 3), glyphs [i].transform.position.z);
				glyphSpots [i].transform.eulerAngles = new Vector3 (glyphSpots [i].transform.eulerAngles.x, Random.Range (0, 4) * 90, glyphSpots [i].transform.eulerAngles.z);
			} else if (glyphSpots [i].tag == "Terrain") {
				glyphs [i].transform.position = new Vector3 (Random.Range(-glyphSpots [i].transform.localScale.x / 2, glyphSpots [i].transform.localScale.x / 2), Random.Range (1, 3), glyphs [i].transform.position.z);
			}
			glyphs[i].transform.localScale = new Vector3(glyphs[i].transform.localScale.x / glyphSpots[i].transform.localScale.x,
				glyphs[i].transform.localScale.y / glyphSpots[i].transform.localScale.y, 
				glyphs[i].transform.localScale.z / glyphSpots[i].transform.localScale.z);
		}
		/*pillarMesh = pillar.GetComponent<MeshRenderer> ();
		wallMesh = wall.GetComponent<MeshRenderer> ();
		boxMesh = box.GetComponent<MeshRenderer> ();
		glyphOne = Instantiate (prefab, new Vector3(pillar.transform.position.x, 2, pillar.transform.position.z), Quaternion.identity, pillar.transform);
		glyphTwo = Instantiate (prefab, wall.transform.position, Quaternion.identity, wall.transform);
		glyphThree = Instantiate (prefab, box.transform.position, Quaternion.identity, box.transform);
		glyphThree.transform.localPosition = new Vector3 (glyphThree.transform.localPosition.x, glyphThree.transform.localPosition.y, 0.5f);
		glyphOne.transform.localPosition = new Vector3 (glyphOne.transform.localPosition.x, glyphOne.transform.localPosition.y, 0.5f);
		glyphThree.transform.localScale = new Vector3(glyphThree.transform.localScale.x / box.transform.localScale.x, glyphThree.transform.localScale.y / box.transform.localScale.y, glyphThree.transform.localScale.z / box.transform.localScale.z);
		glyphTwo.transform.localScale = new Vector3(glyphTwo.transform.localScale.x / ((wall.transform.localScale.x * 2) / 10), glyphTwo.transform.localScale.y / wall.transform.localScale.y, glyphTwo.transform.localScale.z / wall.transform.localScale.z);
		glyphOne.transform.localScale = new Vector3(glyphOne.transform.localScale.x / pillar.transform.localScale.x, glyphOne.transform.localScale.y / pillar.transform.localScale.y, glyphOne.transform.localScale.z / pillar.transform.localScale.z);*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
