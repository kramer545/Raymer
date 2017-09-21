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



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void areaClicked(int index)
	{
		//update camera pos/rot to the area clicked
		cam.transform.position = camPos [index];
		cam.transform.eulerAngles = camRots [index];

		//Hide area indicators
		//TODO replace this when replacing UI images with 3D models
		if(index != 0)
		{
			foreach (GameObject img in overlayImages)
				img.SetActive (false);
			backBtn.SetActive (true);
		}
		else
		{
			foreach (GameObject img in overlayImages)
				img.SetActive (true);
			backBtn.SetActive (false);
		}

		//activate areaPanel
		for(int x = 1;x<areaPanel.Length;x++)
			areaPanel[x].SetActive (false);
		if(index != 0)
			areaPanel[index].SetActive(true);
		//TODO put switch case or something here
		//Activate relevant gameobjects for specific areas
		if (index == 1)
			garbageMinigame.SetActive (true);
	}

	public void garbageMinigameClick(int index)
	{
		garbagePile [index].SetActive (false);
	}
}
