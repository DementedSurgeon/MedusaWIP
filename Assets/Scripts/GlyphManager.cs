using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphManager : MonoBehaviour {

	public GlyphPlacer glyphPlacer;
	public GameObject[] glyphs;

	// Use this for initialization
	void Start () {
		glyphs = glyphPlacer.GetGlyphs ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*public Transform GetActiveGlyph()
	{

	}*/
}
