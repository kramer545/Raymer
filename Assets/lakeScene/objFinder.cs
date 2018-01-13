using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objFinder : MonoBehaviour {

	public Camera cam;
	public GameObject target;
	public GameObject icon;
	public GameObject arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetScreenPoint = cam.WorldToScreenPoint (target.transform.position);
		this.transform.position = targetScreenPoint;
		Debug.Log ("z: " + targetScreenPoint.z);
		Debug.Log ("x: " + this.gameObject.transform.position.x);
		Debug.Log ("y: " + this.gameObject.transform.position.y);
		//bool onScreen = targetScreenPoint.z > 0 && targetScreenPoint.x > 0 && targetScreenPoint.x < 800 && targetScreenPoint.y > 0 && targetScreenPoint.y < 400;
		bool onScreen = targetScreenPoint.z > 100;
		if(!onScreen) {
			icon.SetActive (false);
			arrow.SetActive (true);
			if((targetScreenPoint.z > 0 && this.gameObject.transform.position.x < 0) || (targetScreenPoint.z < 0 && this.gameObject.transform.position.x > 0)) {
				setArrowLeft();
			} else {
				setArrowRight();
			}
		} else {
			icon.SetActive (true);
			arrow.SetActive (false);
		}
	}

	void setArrowLeft() {
		arrow.transform.localEulerAngles = new Vector3(0,0,-90);
		arrow.transform.localPosition = new Vector3 (-600, 0, 0);
	}

	void setArrowRight() {
		arrow.transform.localEulerAngles = new Vector3(0,0,90);
		arrow.transform.localPosition = new Vector3 (600, 0, 0);
	}
}
