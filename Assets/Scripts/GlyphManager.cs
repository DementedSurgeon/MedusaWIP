using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphManager : MonoBehaviour {

	public GlyphPlacer glyphPlacer;
	public GameObject[] glyphs;
	public int totalGlyphs;
	public static int activeGlyph;
	public delegate void MyDelegate ();
	public MyDelegate onGlyphsDestroyed;

	// Use this for initialization
	void Start () {
		glyphs = glyphPlacer.GetGlyphs ();
		totalGlyphs = glyphs.Length;
		activeGlyph = totalGlyphs - 1;
		onGlyphsDestroyed += FinishUp;
	}
	
	// Update is called once per frame
	void Update () {
		if (activeGlyph < 0) {
			onGlyphsDestroyed ();
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

	void FinishUp()
	{
		gameObject.SetActive (false);
	}
}
