using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	private Text text;
	public BoxCollider hand;
	private GrabThings handExtra;
	public Camera toLookAt;
	public GameObject throwablesCluster;
	public GameObject entranceCluster;
	public GlyphManager glyphManager;
	public GameObject shard;
	public BoxCollider[] throwables;
	private GameObject[] glyphs;
	public float distance;
	public int closest;
	private TextWiggle tWiggle;

	void Awake()
	{
		
	}

	// Use this for initialization
	void Start () {
		handExtra = hand.gameObject.GetComponent<GrabThings> ();
		tWiggle = gameObject.GetComponent<TextWiggle> ();
		glyphs = glyphManager.GetGlyphs ();
		throwables = new BoxCollider[throwablesCluster.transform.childCount + glyphs.Length + entranceCluster.transform.childCount];

		text = gameObject.GetComponent<Text> ();
		for (int i = 0; i < throwablesCluster.transform.childCount; i++) {
			throwables [i] = throwablesCluster.transform.GetChild (i).GetComponent<BoxCollider> ();
		}
		for (int i = 0; i < glyphs.Length; i++) {
			throwables [throwablesCluster.transform.childCount + i] = glyphs [i].GetComponent<BoxCollider> ();
		}
		for (int i = 0; i < entranceCluster.transform.childCount; i++) {
			if (entranceCluster.transform.GetChild (i).GetComponent<BoxCollider> () != null) {
				throwables [throwablesCluster.transform.childCount + glyphs.Length + i] = entranceCluster.transform.GetChild (i).GetComponent<BoxCollider> ();
			}
		}
		distance = (throwables [0].transform.position - hand.transform.position).sqrMagnitude;
		closest = 0;
		//StartCoroutine (textWiggle ());
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < throwables.Length; i++) {
			if ((new Vector3(hand.transform.position.x, hand.transform.position.y, hand.transform.position.z) - new Vector3(throwables [i].transform.position.x, throwables [i].transform.position.y, throwables [i].transform.position.z)).sqrMagnitude < distance) {
				distance = (new Vector3(hand.transform.position.x, 0, hand.transform.position.z) - new Vector3(throwables [i].transform.position.x, 0, throwables [i].transform.position.z)).sqrMagnitude;
				closest = i;
			}
		}
		distance = 1000;
		text.transform.position = new Vector3 (throwables [closest].transform.position.x, throwables [closest].transform.position.y + throwables [closest].bounds.size.y/2, throwables [closest].transform.position.z);
		//text.transform.eulerAngles = new Vector3 (0, Vector3.Angle (hand.transform.parent.transform.position, throwables [closest].transform.position), 0);
		Vector3 v = toLookAt.transform.position - transform.position;
		v.x = v.z = 0.0f;
		transform.LookAt (toLookAt.transform.position - v);
		transform.Rotate (0, 180, 0);
		if (throwables [closest].tag == "Grabbable") {
			if (handExtra.GetCurrentThrowable ()) {
				text.text = "[M1] THROW/[F] DROP";
				tWiggle.SetNewText (text.text);
			} else if (!handExtra.GetCurrentThrowable ()) {
				text.text = "[F] PICK UP";
				tWiggle.SetNewText (text.text);
			}
		} else if (throwables [closest].tag == "Glyph") {
			if (throwables [closest].gameObject.activeSelf) {
				text.text = "[F] DESTROY";
				tWiggle.SetNewText (text.text);
			} else {
				text.text = " ";
				tWiggle.SetNewText (text.text);
			}
		} else if (throwables [closest].tag == "Shard") {
			if (throwables [closest].gameObject.activeSelf) {
				if (!handExtra.CanGrabShard ()) {
					text.text = "A SPORTING CHANCE";
					tWiggle.SetNewText (text.text);
				} else {
					text.text = "[F] PICK UP";
					tWiggle.SetNewText (text.text);
				}
			} else {
				text.text = " ";
				tWiggle.SetNewText (text.text);
			}
		} else if (throwables [closest].tag == "Door") {
			if (!throwables [closest].gameObject.GetComponent<Door> ().isLocked) {
				if (!throwables [closest].gameObject.GetComponent<Door> ().isOpen) {
					if (handExtra.CanOpenDoor ()) {
						text.text = "F OPEN";
						tWiggle.SetNewText (text.text);
					} else {
						text.text = " ";
						tWiggle.SetNewText (text.text);
					}
				} else if (throwables [closest].gameObject.GetComponent<Door> ().isOpen) {
					if (handExtra.CanOpenDoor ()) {
						text.text = "F CLOSE";
						tWiggle.SetNewText (text.text);
					} else {
						text.text = " ";
						tWiggle.SetNewText (text.text);
					}
				}
			} else if (throwables [closest].gameObject.GetComponent<Door> ().isLocked) {
				if (handExtra.CanOpenDoor ()) {
					text.text = "LOCKED";
					tWiggle.SetNewText (text.text);
				} else {
					text.text = " ";
					tWiggle.SetNewText (text.text);
				}
			}


		}
	}

}
