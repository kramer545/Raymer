using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cellScript : MonoBehaviour {

	public string testString;
	public BeeMenu mainScript;
	public int growEase;
	public int beeFriendly;
	public int selfselect;
    bool colorChange = true;
    public GameObject defObj;
	bool[] bloom;
	bool started = false;
	// Use this for initialization
	void Start () {
        if (!started)
            init();
    }
	
	// Update is called once per frame
	void Update () {
        if (!mainScript.finished)
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
							replaceHex ();
                        }
                    }
                }
            }
        }
		/*
        else
        {
            if(colorChange)
            {
				gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = new Color32(58,83,35,120);
                colorChange = false;
            }
        }
        */
    }

    private void LateUpdate()
    {
        
    }

    public void init(int selected)
	{
		if (defObj != null)
		{
			GameObject clone = Instantiate(defObj);
			clone.transform.parent = this.gameObject.transform;
			clone.transform.localPosition = new Vector3(0,0,0);
		}
		if(selected == -1){
			selected = mainScript.getMax() - 1;
		}
		int[,] values = mainScript.getValues();
		beeFriendly = values[selected,0];
		growEase = values[selected,1];
		bloom = new bool[7];
		bool[,] bloomValues = mainScript.getBloomValues ();
		for(int x = 0;x<7;x++)//7 months in bloom calendar
		{
			bloom [x] = bloomValues [selected, x];
		}
		selfselect = selected;
		int[] arr = mainScript.getArrayPlant();
		arr[selfselect]++;
		mainScript.setArrayPlant(arr);
		colorChange = true;
		started = true;
	}

	public void init()
	{
		if (defObj != null)
		{
			GameObject clone = Instantiate(defObj);
			clone.transform.parent = this.gameObject.transform;
			clone.transform.localPosition = new Vector3(0,0,0);
		}
		int selected = mainScript.getSelected();
		if(selected == -1){
			selected = mainScript.getMax() - 1;
		}
		int[,] values = mainScript.getValues();
		beeFriendly = values[selected,0];
		growEase = values[selected,1];
		bloom = new bool[7];
		bool[,] bloomValues = mainScript.getBloomValues ();
		for(int x = 0;x<7;x++)//7 months in bloom calendar
		{
			bloom [x] = bloomValues [selected, x]; //will throw error for parents, but its ok
		}
		selfselect = selected;
		int[] arr = mainScript.getArrayPlant();
		arr[selfselect]++;
		mainScript.setArrayPlant(arr);
		colorChange = true;
		started = true;
	}
    
     //may interfere with touch controls
	void OnMouseDown(){
		if(!mainScript.finished)
		{
			replaceHex ();
		}
	}
    
	
	void OnMouseEnter(){
        if(!mainScript.finished)
		    gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = new Color32(58,83,35,120);
	}
	
	void OnMouseExit(){
        if (!mainScript.finished)
            gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = new Color32(88,148,30,120);
	}

	public void replaceHex()
	{
		int selected = mainScript.getSelected();
		GameObject[] plants = mainScript.getPlants();
		if (selected != -1)
		{
			mainScript.updateScore(-beeFriendly, -growEase,bloom, false);
			if(selfselect == mainScript.getMax()-3) //water
				mainScript.numWater--;
			else if (selfselect == mainScript.getMax()-2) //shelter
				mainScript.numShelter--;
			GameObject clone = Instantiate(plants[selected], transform.position, transform.rotation);
			if(selected == mainScript.getMax()-1)//selected is remove, give it tag for autofilling
			{
				clone.tag = "autoFill";
			}
			int[,] values = mainScript.getValues();
			bool[,] newBloom = mainScript.getBloomValues ();
			bool[] tempBloom = new bool[7];
			for (int x = 0; x < tempBloom.Length; x++)
				tempBloom [x] = newBloom [selected, x];
			if((growEase == 0) && (beeFriendly == 0))//for autofill hexs
			{
				clone.GetComponent<cellScript> ().init (selected);
			}
			mainScript.updateScore(values[selected, 0],values[selected, 1],tempBloom,true);
			if(selfselect != selected)
			{
				if (selfselect == mainScript.getMax () - 1)//if replacing empty, increment num objs
					mainScript.numObj++;
				else if (selected == mainScript.getMax () - 1)//reduce objs if replacing something with nothing
					mainScript.numObj--;
			}
			if(selected == mainScript.getMax()-3) //water
				mainScript.numWater++;
			else if (selected == mainScript.getMax()-2) //shelter
				mainScript.numShelter++;
			Destroy(gameObject);
		}
		else
		{
		}
	}
}
