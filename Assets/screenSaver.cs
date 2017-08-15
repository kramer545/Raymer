using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class screenSaver : MonoBehaviour {

	float resetTimer;

	// Use this for initialization
	void Start () {
		resetTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.touchCount > 0)
		{
			resetTimer = 0;
		}
		else
		{
			resetTimer += Time.deltaTime;
		}
		if(resetTimer > 180)
		{
			SceneManager.LoadScene ("intro");
		}
	}
}
