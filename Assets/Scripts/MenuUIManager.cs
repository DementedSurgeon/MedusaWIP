using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour {

	public GameObject buttonCluster;
	public BoxCollider hand;
	private BoxCollider[] buttons;
	private float distance;
	private int closest;
	private Text text;
	public Camera toLookAt;
	private TextWiggle tWiggle;
	public Text credits;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text> ();
		tWiggle = gameObject.GetComponent<TextWiggle> ();
		buttons = new BoxCollider[buttonCluster.transform.childCount];
		for (int i = 0; i < buttonCluster.transform.childCount; i++) {
			buttons [i] = buttonCluster.transform.GetChild(i).GetComponent<BoxCollider>();
		}
		distance = (buttons [0].transform.position - hand.transform.position).sqrMagnitude;
		closest = 0;
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < buttons.Length; i++) {
			if ((new Vector3(hand.transform.position.x, 0, hand.transform.position.z) - new Vector3(buttons [i].transform.position.x, 0, buttons [i].transform.position.z)).sqrMagnitude < distance) {
				distance = (new Vector3(hand.transform.position.x, 0, hand.transform.position.z) - new Vector3(buttons [i].transform.position.x, 0, buttons [i].transform.position.z)).sqrMagnitude;
				closest = i;
			}
		}
		distance = 1000;
		text.transform.position = new Vector3 (buttons [closest].transform.position.x, buttons [closest].transform.position.y + buttons [closest].bounds.size.y/2, buttons [closest].transform.position.z);
		//text.transform.eulerAngles = new Vector3 (0, Vector3.Angle (hand.transform.parent.transform.position, throwables [closest].transform.position), 0);
		Vector3 v = toLookAt.transform.position - transform.position;
		v.x = v.z = 0.0f;
		transform.LookAt (toLookAt.transform.position - v);
		//transform.Rotate (0, 180, 0);
		if (text.text.ToLower() != buttons [closest].name.ToLower()) {
			text.text = buttons [closest].name;
			tWiggle.SetNewText (buttons [closest].name);
			if (buttons [closest].name != "CREDITS") {
				credits.gameObject.SetActive (false);
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			if (closest == 0) {
				SceneManager.LoadScene ("Test");
			} else if (closest == 1) {
				if (!credits.gameObject.activeSelf) {
					credits.gameObject.SetActive (true);
					Debug.Log ("Credits");
				}
			} else if (closest == 2) {
				Application.Quit ();
			}
		}
	}


}
