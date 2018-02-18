using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakeController : MonoBehaviour {
	public GameObject[] cams;
	public GameObject[] overlayImages;
	public GameObject[] areaPanel;
	public GameObject[] walls;
	public GameObject[] aboveWaterObjs;
	public GameObject backBtn;
	public GameObject aboveLake;
	public GameObject levelLake;
	public GameObject rocks;
	public float travelSpeed = 1.0f;
	public float underWaterFogStrength = 0.012f;
	public float fogStrength = 0.002f;
	public float pollutedFogStrength = 0.02f;
	public Color normalColor;
	public Color underWaterColor;
	public Color pollutedColor;
	public int stageNum;
	bool isTraveling = false;
	float startTime;
	float journeyLength;
	float journeyRotLength;
	int startPosIndex;
	int endPosIndex;
	int tempIndex = 0;
    bool[] overlayActive;

	// Use this for initialization
	void Start () {
		endPosIndex = 0;
		tempIndex = 0;
        overlayActive = new bool[overlayImages.Length];
        for(int x = 0;x<overlayActive.Length;x++)
        {
            overlayActive[x] = true;
        }
		stageNum = 0;
		setUnderWater ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeWalls() {
		walls [stageNum-1].SetActive (false);
		walls [stageNum].SetActive (true);
	}

	public void areaClicked(int index)
	{
		setCamMove(false);
		areaPanel[index].SetActive(true);
		if(index == 1) {
			cams [0].SetActive (false);
			cams [1].SetActive (true);
		}
	}

	public void setCamMove(bool canMove) {
		rotateCam.setActive(canMove);
	}

	public void removeRocks() {
		rocks.SetActive (false);
	}

	public void setUnderWater()
	{
		Debug.Log ("set water");
		for(int x = 0;x<aboveWaterObjs.Length;x++)
		{
			aboveWaterObjs[x].SetActive(false);
		}
		RenderSettings.fogColor = underWaterColor;
		RenderSettings.fogDensity = underWaterFogStrength;
	}

	public void setNormal()
	{
		Debug.Log ("set normal");
		for(int x = 0;x<aboveWaterObjs.Length;x++)
		{
			aboveWaterObjs[x].SetActive(true);
		}
		RenderSettings.fogColor = normalColor;
		RenderSettings.fogDensity = fogStrength;
	}

	public void setPolluted() {
		Debug.Log ("set Polluteed");
		for(int x = 0;x<aboveWaterObjs.Length;x++)
		{
			aboveWaterObjs[x].SetActive(false);
		}
		RenderSettings.fogColor = pollutedColor;
		RenderSettings.fogDensity = pollutedFogStrength;
	}
}
