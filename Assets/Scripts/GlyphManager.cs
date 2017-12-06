using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphManager : MonoBehaviour {

	public GlyphPlacer glyphPlacer;
	public GameObject[] glyphs;
	public int totalGlyphs;
	public int activeGlyph;

	// Use this for initialization
	void Start () {
		glyphs = glyphPlacer.GetGlyphs ();
		totalGlyphs = glyphs.Length;
		activeGlyph = totalGlyphs - 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Transform GetActiveGlyph()
	{
		return glyphs [activeGlyph].transform;
	}
}
