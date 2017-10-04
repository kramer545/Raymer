using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class lakeIconRotate : MonoBehaviour {

	public float rotateSpeed = 10.0f;
	public float scaleSpeed = 0.05f;
	public float maxScale = 1.2f;
	public float minScale = 1.0f;
	public int areaID;
	public lakeController controller;
	float rotation; // z rotation
	float scale;
	bool scaleIncreasing = true;

	// Use this for initialization
	void Start () {
		rotation = this.transform.rotation.z;
		scale = this.transform.localScale.x;
	}

	void Update()
	{
		if (Input.touchCount == 1)//checks for touch input over image
		{
			if (Input.touches[0].phase == TouchPhase.Stationary)
			{
				RaycastHit hit;
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
				if (Physics.Raycast(ray, out hit))
				{
					if (hit.collider.gameObject == this.gameObject)
					{
						controller.areaClicked (areaID);
					}
				}
			}
		}
	}
	
	// late update to avoid interferance with lookAtCam Script, which both affect rot
	void LateUpdate () {
		//rotating img
		rotation += rotateSpeed * Time.deltaTime;
		this.transform.eulerAngles = new Vector3 (this.transform.eulerAngles.x, this.transform.eulerAngles.y, rotation);

		//scaling img 
		if (scaleIncreasing)
			scale += scaleSpeed * Time.deltaTime;
		else
			scale -= scaleSpeed * Time.deltaTime;
		if ((scale > maxScale) || (scale < minScale)) //once limit reached, switch direction of growth
			scaleIncreasing = !scaleIncreasing;
		this.transform.localScale = new Vector3 (scale, scale, scale);
	}

	void OnMouseDown() //for mouse testing, not used in final version, instead uses touch input in update
	{
		controller.areaClicked (areaID);
	}
}
