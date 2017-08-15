using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapIntro : MonoBehaviour {

	public Animation flyin;
	public MonoBehaviour touch;
	public GameObject gameCanvas;
	public GameObject introCanvas;
	// Use this for initialization
	void Start () {
		touch.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void beginAnim(){
		introCanvas.SetActive(false);
		touch.enabled = false;
		flyin.Play();
	}
	
	void beginMap(){
		touch.enabled = true;
		gameCanvas.SetActive(true);
		flyin.Stop();
	}
}
