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

    public GameObject botWater;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.position.y <= waterLevel) && !isUnderWater)
		{
			isUnderWater = true;
			setUnderWater ();
		}
		else if ((transform.position.y > waterLevel) && isUnderWater)
		{
			isUnderWater = false;
			setNormal ();
		}

        if(movingUp)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(downPos,upPos, fracJourney);
            if (fracJourney >= 1)
                movingUp = false;
        }
        else if(movingDown)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(upPos, downPos, fracJourney);
            if (fracJourney >= 1)
                movingDown = false;
        }

	}

	public void setUnderWater()
	{
        botWater.SetActive(true);
		Debug.Log ("underwater");
		RenderSettings.fogColor = underWaterColor;
		RenderSettings.fogDensity = underWaterFogStrength;
        GetComponent<MorePPEffects.Wiggle>().enabled = true;
	}

	public void setNormal()
	{
        botWater.SetActive(false);
        Debug.Log ("above water");
		RenderSettings.fogColor = normalColor;
		RenderSettings.fogDensity = fogStrength;
        GetComponent<MorePPEffects.Wiggle>().enabled = false;
    }

	public void moveToRiver()
	{
		transform.position = new Vector3 (55, 6, -17);
	}

	public void moveToLake()
	{
		transform.position = new Vector3 (423F, 44.6F, -49.6F);
		transform.eulerAngles = new Vector3 (32.82F, 0, 0);
		GetComponent<Camera> ().backgroundColor = new Color32 (61,152,229,255);
	}

	public void moveDown()
	{
        if(!movingUp && !movingDown)
        {
            movingDown = true;
            startTime = Time.time;
            journeyLength = Vector3.Distance(upPos, downPos);
        }
    }

    public void moveUp()
    {
        if (!movingUp && !movingDown)
        {
            movingUp = true;
            startTime = Time.time;
            journeyLength = Vector3.Distance(upPos, downPos);
        }
    }


}
