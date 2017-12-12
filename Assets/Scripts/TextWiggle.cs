using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWiggle : MonoBehaviour {

	private Text text;
	private string initialText;
	private string modifiedText;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
		initialText = text.text;
		modifiedText = initialText;
		StartCoroutine (TextWiggler());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator TextWiggler()
	{
		while (true) {
			for (int i = 0; i < text.text.Length; i++) {
				text.text = initialText;
				modifiedText = text.text;
				StringBuilder mT = new StringBuilder (modifiedText);
				mT[i] = char.ToLower (text.text[i]);
				text.text = mT.ToString();
				yield return new WaitForSeconds (0.1f);
			}
		}
	}

	public void SetNewText (string newText)
	{
		initialText = newText;
	}
}
