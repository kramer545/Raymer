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
		//TODO fix this when moving from UI to 3D, hides UI elements while moving, disable all possible ui elements
		foreach (GameObject img in overlayImages)
			img.SetActive (false);
		for(int x = 1;x<areaPanel.Length;x++)
			areaPanel[x].SetActive (false);
		foreach (GameObject cam in cams)
			cam.SetActive (false);
		cams [index].SetActive (true);
		backBtn.SetActive (false);

        if(tempIndex != 0)
        {
            overlayActive[tempIndex - 1] = false;
            //Destroy(overlayImages[tempIndex - 1]);
        }

        tempIndex = index;

        //Hide area indicators
        //TODO replace this when replacing UI images with 3D models
        if (tempIndex == 0) { //if index = 0, at base layer, show UI circles highlighting areas
			aboveLake.SetActive (true);
			levelLake.SetActive (false);
			setNormal (); //reset underwater effects/fog 
            for(int x = 0;x<overlayImages.Length;x++)
            {
                if(overlayActive[x] == true)
                {
                    overlayImages[x].SetActive(true);
                }
            }
		}

		//activate areaPanel
		if(tempIndex  != 0)
			areaPanel[tempIndex ].SetActive(true);
		//TODO put switch case or something here
		//Activate relevant gameobjects for specific areas
		if (tempIndex == 2) //docks
			setUnderWater ();
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
