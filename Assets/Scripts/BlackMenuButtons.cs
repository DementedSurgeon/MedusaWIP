using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackMenuButtons : MonoBehaviour {

	public Button mainMenu;
	public Button quit;

	// Use this for initialization
	void Start () {
		mainMenu.onClick.AddListener (delegate() {
			SceneManager.LoadScene ("Start");
		});
		quit.onClick.AddListener (Application.Quit);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
