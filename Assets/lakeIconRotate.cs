using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lakeIconRotate : MonoBehaviour {

	public float rotateSpeed = 10.0f;
	public float scaleSpeed = 0.05f;
	public float maxScale = 1.2f;
	public float minScale = 1.0f;
	float rotation; // z rotation
	float scale;
	bool scaleIncreasing = true;

	// Use this for initialization
	void Start () {
		rotation = this.transform.rotation.z;
		scale = this.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		//rotating img
		rotation += rotateSpeed * Time.deltaTime;
		this.transform.eulerAngles = new Vector3 (0, 0, rotation);

		//scaling img 
		if (scaleIncreasing)
			scale += scaleSpeed * Time.deltaTime;
		else
			scale -= scaleSpeed * Time.deltaTime;
		if ((scale > maxScale) || (scale < minScale)) //once limit reached, switch direction of growth
			scaleIncreasing = !scaleIncreasing;
		this.transform.localScale = new Vector3 (scale, scale, scale);
	}
}
