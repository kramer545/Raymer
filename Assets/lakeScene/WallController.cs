using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallController : rotateCam {

	public GameObject panelOne;
	public GameObject panelTwo;
	public Text panelText;
	public GameObject walls;
	public GameObject gravel;
	public GameObject wetland;

	// Use this for initialization
	void Start () {
		showPanelOne ();
	}
	
	// Update is called once per frame
	void Update () {
		//base.rotate();
	}

	public void showPanelOne()
	{
		panelOne.SetActive (true);
	}

	public void hidePanelOne()
	{
		panelOne.SetActive (false);
		showPanelTwo();
	}

	public void showPanelTwo()
	{
		panelTwo.SetActive (true);
	}

	public void hidePanelTwo()
	{
		panelTwo.SetActive (false);
	}

	public void wallBtn()
	{
		walls.SetActive (true);
		gravel.SetActive (false);
		wetland.SetActive (false);
		panelText.text = "Retaining walls are the most harmful option for the enviromnent as it does bad things";
	}

	public void gravelBtn()
	{
		walls.SetActive (false);
		gravel.SetActive (true);
		wetland.SetActive (false);
		panelText.text = "Gravel beachs are a good middle ground between human development and enviroment protection while reducing shoreline erosion";
	}

	public void wetlandBtn()
	{
		walls.SetActive (false);
		gravel.SetActive (false);
		wetland.SetActive (true);
		panelText.text = "Converting the shoreline, while drastic provides the best support for the enviorment, letting the water saturate the land or something";
	}
}
