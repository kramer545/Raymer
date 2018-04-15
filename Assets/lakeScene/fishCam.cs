using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishCam : rotateCam {

	public GameObject cam;
	public float moveSpeed;
	public Vector3 playerObjPos;
	public Vector3 playerObjRot;
	public lakeController controller;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(rotateCam.isActive) {
			rotate ();
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

			if (Input.GetKey("e")) {
				cam.transform.Rotate (Vector3.up * moveSpeed * 10);
			}

			if (Input.GetKey("q")) {
				cam.transform.Rotate (Vector3.up * moveSpeed * -10);
			}
		}
	}

	public void setAdultPos() {
		cam.transform.position = playerObjPos;
		cam.transform.eulerAngles = playerObjRot;
		rotateCam.setActive(true);
		controller.setUnderWater();
	}
}
