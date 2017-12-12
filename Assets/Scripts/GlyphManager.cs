using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphManager : MonoBehaviour {

	public GlyphPlacer glyphPlacer;
	public GameObject[] glyphs;
	public int totalGlyphs;
	public static int activeGlyph;

	// Use this for initialization
	void Start () {
		glyphs = glyphPlacer.GetGlyphs ();
		totalGlyphs = glyphs.Length;
		activeGlyph = totalGlyphs - 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (activeGlyph == -1) {
			activeGlyph = 0;
			Debug.Log ("You win!");
		}
	}

	public Transform GetActiveGlyph()
	{
		if (activeGlyph >= 0) {
			return glyphs [activeGlyph].transform;
		} else {
			return transform;
		}
	}

	public GameObject[] GetGlyphs()
	{
		return glyphs;
	}
}
