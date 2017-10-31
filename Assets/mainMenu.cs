using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void startBee() {
		SceneManager.LoadScene("xeriscape");
	}

	public void startCake() {
		SceneManager.LoadScene("MainMenu");
	}

	public void menu() {
		SceneManager.LoadScene("MainMenu"); //main menu is now cake
	}
}
