using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakeController : MonoBehaviour {

	public GameObject cam;
	public Vector3[] camPos;
	public Vector3[] camRots;
	public GameObject[] overlayImages;
	public GameObject[] areaPanel;
	public GameObject backBtn;
	public GameObject[] garbagePile;
	public GameObject garbageMinigame;
	public float travelSpeed = 1.0f;

	bool isTraveling = false;
	float startTime;
	float journeyLength;
	float journeyRotLength;
	int startPosIndex;
	int endPosIndex;
	int tempIndex = 0;



	// Use this for initialization
	void Start () {
		endPosIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(isTraveling)
		{
			float distCovered = (Time.time - startTime) * travelSpeed;
			float fracJourney = distCovered / journeyLength;
			cam.transform.position = Vector3.Lerp(camPos[startPosIndex], camPos[endPosIndex], fracJourney);
			cam.transform.eulerAngles = Vector3.Lerp(camRots[startPosIndex], camRots[endPosIndex], fracJourney); //update rotation using position % amount, keep them together
			if(cam.transform.position == camPos[endPosIndex]) //if arrived, stop moving, start area assets (panels, minigames, etc)
			{
				isTraveling = false;
				startArea ();
			}
		}
	}

	//activates all assets relevant to the area arrived in
	void startArea()
	{
		//Hide area indicators
		//TODO replace this when replacing UI images with 3D models
		if(tempIndex != 0)
		{
			backBtn.SetActive (true);
		}
		else //if index = 0, at base layer, show UI circles highlighting areas
		{
			foreach (GameObject img in overlayImages)
				img.SetActive (true);
		}

		//activate areaPanel
		if(tempIndex  != 0)
			areaPanel[tempIndex ].SetActive(true);
		//TODO put switch case or something here
		//Activate relevant gameobjects for specific areas
		if (tempIndex  == 1)
			garbageMinigame.SetActive (true);
	}

	public void areaClicked(int index)
	{
		
		//update camera pos/rot to the area clicked
		//old, instant position update
		/*
		cam.transform.position = camPos [index];
		cam.transform.eulerAngles = camRots [index];
		*/

		//set up all variables for traveling between the two areas
		isTraveling = true;
		startPosIndex = endPosIndex; //start point is the point we are currently at
		endPosIndex = index;
		startTime = Time.time;
		journeyLength = Vector3.Distance (camPos [startPosIndex], camPos [endPosIndex]);
		journeyRotLength = Vector3.Distance (camRots [startPosIndex], camRots [endPosIndex]);
		tempIndex = index;

		//TODO fix this when moving from UI to 3D, hides UI elements while moving, disable all possible ui elements
		foreach (GameObject img in overlayImages)
			img.SetActive (false);
		for(int x = 1;x<areaPanel.Length;x++)
			areaPanel[x].SetActive (false);
		backBtn.SetActive (false);
	}

	public void garbageMinigameClick(int index)
	{
		garbagePile [index].SetActive (false);
	}
}
