using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBoundBox : MonoBehaviour {

	public TouchCamera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "MainCamera")
		{
			cam.gameObject.transform.position = cam.oldPos;
		}
	}
}
