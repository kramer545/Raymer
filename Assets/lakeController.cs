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

	public void garbageMinigameClick(int index)
	{
		garbagePile [index].SetActive (false);
	}
}
