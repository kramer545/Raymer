using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class objFinderText : MonoBehaviour {

	public GameObject cam;
	public GameObject target;
	public Image finderImg;
	public Text thisText;
	bool isVisible;

	// Use this for initialization
	void Start () {
		isVisible = true;
	}
	
	// Update is called once per frame
	void Update () {
		//looks better divided by 10
		float distance = Vector3.Distance (cam.transform.position, target.transform.position) / 10.0f;
		if (distance < 5) {
			thisText.enabled = false;
			finderImg.enabled = false;
		} else {
			thisText.enabled = true;
			finderImg.enabled = true;
			thisText.text = string.Format("{0:N1}", distance);
		}
	}
}
