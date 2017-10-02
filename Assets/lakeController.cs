using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakeController : MonoBehaviour {
	public GameObject[] cams;
	public GameObject[] overlayImages;
	public GameObject[] areaPanel;
	public GameObject backBtn;
	public GameObject[] garbagePile;
	public GameObject garbageMinigame;
	public float travelSpeed = 1.0f;
	public float underWaterFogStrength = 0.012f;
	public float fogStrength = 0.002f;
	public Color normalColor;
	public Color underWaterColor;

	bool isTraveling = false;
	float startTime;
	float journeyLength;
	float journeyRotLength;
	int startPosIndex;
	int endPosIndex;
	int tempIndex = 0;

	private Vector3 firstpoint;
	private Vector3 secondpoint;
	private float xAngle = 0.0f;
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f;
	private float yAngTemp = 0.0f;



	// Use this for initialization
	void Start () {
		endPosIndex = 0;
		tempIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
			//rotating camera w/ touchscreen
			if(Input.touchCount > 0) {
				//Touch began, save position
				if(Input.GetTouch(0).phase == TouchPhase.Began) {
					firstpoint = Input.GetTouch(0).position;
					xAngTemp = xAngle;
					yAngTemp = yAngle;
				}
				//Move finger by screen
				if(Input.GetTouch(0).phase==TouchPhase.Moved) {
					secondpoint = Input.GetTouch(0).position;
					//Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
					xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
					yAngle = yAngTemp - (secondpoint.y - firstpoint.y) *90.0f / Screen.height;
					//Rotate camera
					if (yAngle > 90)//prevents camera flipping upside down
						yAngle = 90;
					else if (yAngle < -90)
						yAngle = -90;
				cams[tempIndex].transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
				}
			}
	}

	public void areaClicked(int index)
	{
		tempIndex = index;

		//TODO fix this when moving from UI to 3D, hides UI elements while moving, disable all possible ui elements
		foreach (GameObject img in overlayImages)
			img.SetActive (false);
		for(int x = 1;x<areaPanel.Length;x++)
			areaPanel[x].SetActive (false);
		foreach (GameObject cam in cams)
			cam.SetActive (false);
		cams [index].SetActive (true);
		backBtn.SetActive (false);

		//Hide area indicators
		//TODO replace this when replacing UI images with 3D models
		if (tempIndex == 0) { //if index = 0, at base layer, show UI circles highlighting areas
			setNormal (); //reset underwater effects/fog 
			foreach (GameObject img in overlayImages)
				img.SetActive (true);
		}

		//activate areaPanel
		if(tempIndex  != 0)
			areaPanel[tempIndex ].SetActive(true);
		//TODO put switch case or something here
		//Activate relevant gameobjects for specific areas
		if (tempIndex  == 1) //trash island
			garbageMinigame.SetActive (true);
		if (tempIndex == 2) //docks
			setUnderWater ();
	}

	public void garbageMinigameClick(int index)
	{
		garbagePile [index].SetActive (false);
	}

	public void setUnderWater()
	{
		RenderSettings.fogColor = underWaterColor;
		RenderSettings.fogDensity = underWaterFogStrength;
	}

	public void setNormal()
	{
		RenderSettings.fogColor = normalColor;
		RenderSettings.fogDensity = fogStrength;
	}
}
