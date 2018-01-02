using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishCam : rotateCam {

	public GameObject cam;
	public float moveSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(base.isActive) {
			if (Input.GetKey("up")) {
				cam.transform.Translate (Vector3.forward * moveSpeed);
			}

			if (Input.GetKey("down")) {
				cam.transform.Translate (Vector3.forward * moveSpeed * -1);
			}

			if (Input.GetKey("left")) {
				cam.transform.Translate (Vector3.right * moveSpeed * -1);
			}

			if (Input.GetKey("right")) {
				cam.transform.Translate (Vector3.right * moveSpeed);
			}
		}
	}
}
