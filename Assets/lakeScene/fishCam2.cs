using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishCam2 : rotateCam {

	public GameObject cam;
	public float moveSpeed;
	bool moving;

	// Use this for initialization
	void Start () {
		moving = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(moving)
			cam.transform.Translate (Vector3.forward * moveSpeed);
		if (Input.GetKeyDown("up")) {
			moving = !moving;
		}
	}
}
