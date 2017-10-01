using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underwater : MonoBehaviour {

	public float waterLevel;
	public float underWaterFogStrength = 0.08f;
	public float fogStrength = 0.002f;
	private bool isUnderWater;
	public Color normalColor;
	public Color underWaterColor;
	bool movingDown = false;
	bool movingUp = false;
	float startTime;
	public float speed = 1.0f;
    float journeyLength;
	public Vector3 upPos;
	public Vector3 downPos;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.y <= waterLevel))
		{
			setUnderWater ();
		}
		else if ((transform.position.y > waterLevel))
		{
			setNormal ();
		}

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
