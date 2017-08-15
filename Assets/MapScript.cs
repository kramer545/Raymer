using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour {
	
	//Land Cover Mats
	public Material[] LandCoverMats;
	public Material[] NaturalFragmenters;
	public Material[] ManmadeFragmenters;
	public Material terrain;
	public Material bec;
	public Material tem;
	public Color32 closeColor;
	public Color32 openColor;
	public GameObject controls;
	public GameObject terrainToggles;
	public GameObject landscapeTexts;
	public GameObject landCoverToggles;
	public GameObject becToggles;
	public GameObject intropanel;
	public GameObject landscapePanel;
	public GameObject landscapeSub;
	public GameObject fragmentPanel;
	public GameObject fragmentSub;
	public GameObject humanFragToggles;
	public GameObject naturalFragToggles;
	public GameObject fragmentationTexts;
	public GameObject solutionPanel;
	public GameObject connectivityPanel;
	public GameObject connectivityToggles;
	public GameObject connectivityText;
	public GameObject solutionPins;
	public GameObject solutionText;

	// Use this for initialization
	void Start () {
		terrain.renderQueue = 2000;
		LandCoverMats[0].renderQueue = 2002;
		bec.renderQueue = 2003;
		NaturalFragmenters[2].renderQueue = 2010;
		NaturalFragmenters[0].renderQueue = 2011;
		NaturalFragmenters[1].renderQueue = 2012;
		ManmadeFragmenters[0].renderQueue = 2050;
		ManmadeFragmenters[1].renderQueue = 2051;
		ManmadeFragmenters[2].renderQueue = 2052;
		ManmadeFragmenters[3].renderQueue = 2053;
		ManmadeFragmenters[4].renderQueue = 2054;
		tem.renderQueue = 2055;
	}
	
	//Terrain Toggle Checker - sublayers
	public void terrainToggle(){
		for(int i = 0; i < terrainToggles.transform.childCount; i++)
		{
			Toggle currentToggle = terrainToggles.transform.GetChild(i).GetComponent<Toggle>();
			if(currentToggle.isOn){
				terrain.mainTextureScale = new Vector2(1.0f, -1.0f);
			}else{
				terrain.mainTextureScale = new Vector2(0.0f, 0.0f);
			}
		}
	}
	
	//Land Cover Toggle Checker - sublayers
	public void landCoverToggle(){
		for(int i = 0; i < landCoverToggles.transform.childCount; i++)
		{
			Toggle currentToggle = landCoverToggles.transform.GetChild(i).GetComponent<Toggle>();
			if(currentToggle.isOn){
				LandCoverMats[i].mainTextureScale = new Vector2(1.0f, -1.0f);
			}else{
				LandCoverMats[i].mainTextureScale = new Vector2(0.0f, 0.0f);
			}
		}
	}
	
	//BEC Toggle Checker - sublayers
	public void becToggle(){
		for(int i = 0; i < becToggles.transform.childCount; i++)
		{
			Toggle currentToggle = becToggles.transform.GetChild(i).GetComponent<Toggle>();
			if(currentToggle.isOn){
				bec.mainTextureScale = new Vector2(1.0f, -1.0f);
			}else{
				bec.mainTextureScale = new Vector2(0.0f, 0.0f);
			}
		}
	}
	
	//Natural Fragmentation Toggle Checker - sublayers
	public void natfragToggle(){
		for(int i = 0; i < naturalFragToggles.transform.childCount; i++)
		{
			Toggle currentToggle = naturalFragToggles.transform.GetChild(i).GetComponent<Toggle>();
			if(currentToggle.isOn){
				NaturalFragmenters[i].mainTextureScale = new Vector2(1.0f, -1.0f);
			}else{
				NaturalFragmenters[i].mainTextureScale = new Vector2(0.0f, 0.0f);
			}
		}
	}
	
	//Human Fragmentation Toggle Checker - sublayers
	public void humanfragToggle(){
		for(int i = 0; i < humanFragToggles.transform.childCount; i++)
		{
			Toggle currentToggle = humanFragToggles.transform.GetChild(i).GetComponent<Toggle>();
			if(currentToggle.isOn){
				ManmadeFragmenters[i].mainTextureScale = new Vector2(1.0f, -1.0f);
			}else{
				ManmadeFragmenters[i].mainTextureScale = new Vector2(0.0f, 0.0f);
			}
		}
	}
	
	//Toggle landscape submenu
	public bool landscapeMainMenu(){
		Toggle landscapeToggle = landscapePanel.transform.Find("Toggle").GetComponent<Toggle>();
		if(landscapeToggle.isOn){
			Image img = landscapePanel.GetComponent<Image>();
			img.color = openColor;
			landscapeSub.SetActive(true);
			return true;
		}else{
			Image img = landscapePanel.GetComponent<Image>();
			img.color = closeColor;
			for(int i = 0; i < landscapeSub.transform.childCount; i++)
			{
				Toggle currentToggle = landscapeSub.transform.GetChild(i).GetChild(1).GetComponent<Toggle>();
				if(currentToggle.isOn){
					currentToggle.isOn = false;
				}
			}
			landscapeSub.SetActive(false);
			return false;
		}
	}
	
	//Toggle fragmentation submenu
	public bool fragmentMainMenu(){
		Toggle landscapeToggle = fragmentPanel.transform.Find("Toggle").GetComponent<Toggle>();
		if(landscapeToggle.isOn){
			Image img = fragmentPanel.GetComponent<Image>();
			img.color = openColor;
			fragmentSub.SetActive(true);
			return true;
		}else{
			Image img = fragmentPanel.GetComponent<Image>();
			for(int i = 0; i < fragmentSub.transform.childCount; i++)
			{
				Toggle currentToggle = fragmentSub.transform.GetChild(i).GetChild(1).GetComponent<Toggle>();
				if(currentToggle.isOn){
					currentToggle.isOn = false;
				}
			}
			img.color = closeColor;
			fragmentSub.SetActive(false);
			return false;
		}
	}
	
	//Handle landscape menu
	public void landscapeSubMenu(){
		for(int i = 0; i < landscapeSub.transform.childCount; i++)
		{
			Toggle currentToggle = landscapeSub.transform.GetChild(i).GetChild(1).GetComponent<Toggle>();
			if(currentToggle.isOn){
				Image img = landscapeSub.transform.GetChild(i).GetComponent<Image>();
				img.color = openColor;
				if(i==0){
					terrainToggles.SetActive(true);
					landscapeTexts.transform.GetChild(0).gameObject.SetActive(true);
				}
				if(i==1){
					landCoverToggles.SetActive(true);
					landscapeTexts.transform.GetChild(1).gameObject.SetActive(true);
				}
				if(i==2){
					becToggles.SetActive(true);
					landscapeTexts.transform.GetChild(2).gameObject.SetActive(true);
				}
			}else{
				Image img = landscapeSub.transform.GetChild(i).GetComponent<Image>();
				img.color = closeColor;
				if(i==0){
					terrainToggles.SetActive(false);
					landscapeTexts.transform.GetChild(0).gameObject.SetActive(false);
				}
				if(i==1){
					landCoverToggles.SetActive(false);
					landscapeTexts.transform.GetChild(1).gameObject.SetActive(false);
				}
				if(i==2){
					becToggles.SetActive(false);
					landscapeTexts.transform.GetChild(2).gameObject.SetActive(false);
				}
			}
		}
	}

	//Handle fragmentation menu
	public void fragmentSubMenu(){
		for(int i = 0; i < fragmentSub.transform.childCount; i++)
		{
			Toggle currentToggle = fragmentSub.transform.GetChild(i).GetChild(1).GetComponent<Toggle>();
			if(currentToggle.isOn){
				Image img = fragmentSub.transform.GetChild(i).GetComponent<Image>();
				img.color = openColor;
				if(i==0){
					naturalFragToggles.SetActive(true);
					if(naturalFragToggles.transform.GetChild(2).GetComponent<Toggle>().isOn){
						fragmentationTexts.transform.GetChild(1).gameObject.SetActive(true);
						fragmentationTexts.transform.GetChild(0).gameObject.SetActive(false);
					}else{
						fragmentationTexts.transform.GetChild(0).gameObject.SetActive(true);
						fragmentationTexts.transform.GetChild(1).gameObject.SetActive(false);
					}
				}
				if(i==1){
					humanFragToggles.SetActive(true);
					fragmentationTexts.transform.GetChild(2).gameObject.SetActive(true);
				}
			}else{
				Image img = fragmentSub.transform.GetChild(i).GetComponent<Image>();
				img.color = closeColor;
				if(i==0){
					naturalFragToggles.SetActive(false);
					fragmentationTexts.transform.GetChild(1).gameObject.SetActive(false);
					fragmentationTexts.transform.GetChild(0).gameObject.SetActive(false);
				}
				if(i==1){
					humanFragToggles.SetActive(false);
					fragmentationTexts.transform.GetChild(2).gameObject.SetActive(false);
				}
			}
		}
	}
	
	//Toggle solutions
	public bool solutionsMainMenu(){
		Toggle solutionToggle = solutionPanel.transform.Find("Toggle").GetComponent<Toggle>();
		if(solutionToggle.isOn){
			Image img = solutionPanel.GetComponent<Image>();
			img.color = openColor;
			solutionPins.SetActive(true);
			solutionText.SetActive(true);
			return true;
		}else{
			Image img = solutionPanel.GetComponent<Image>();
			img.color = closeColor;
			solutionPins.SetActive(false);
			solutionText.SetActive(false);
			return false;
		}
	}
	
	//Toggle connectivity (submenu?)
	public bool connectivityMainMenu(){
		Toggle connectivityToggle = connectivityPanel.transform.Find("Toggle").GetComponent<Toggle>();
		if(connectivityToggle.isOn){
			Image img = connectivityPanel.GetComponent<Image>();
			img.color = openColor;
			connectivityToggles.SetActive(true);
			connectivityText.SetActive(true);
			return true;
		}else{
			Image img = connectivityPanel.GetComponent<Image>();
			img.color = closeColor;
			connectivityToggles.SetActive(false);
			connectivityText.SetActive(false);
			return false;
		}
	}
	
	//Connectivity (TEM) Toggle Checker - sublayer
	public void connectivityToggle(){
		for(int i = 0; i < connectivityToggles.transform.childCount; i++)
		{
			Toggle currentToggle = connectivityToggles.transform.GetChild(i).GetComponent<Toggle>();
			if(currentToggle.isOn){
				tem.mainTextureScale = new Vector2(1.0f, -1.0f);
			}else{
				tem.mainTextureScale = new Vector2(0.0f, 0.0f);
			}
		}
	}
	
	
	
	public void mapcontrolenable(){
		intropanel.SetActive(false);
		controls.SetActive(true);
	}
	public void rotforward(){
		Camera.main.gameObject.transform.Rotate(15, 0, 0);
	}
	public void rotbackward(){
		Camera.main.gameObject.transform.Rotate(-15, 0, 0);
	}
	public void menureturn(){
		SceneManager.LoadScene("main_menu");
	}
	
	// Update is called once per frame
	void Update () {
		landscapeMainMenu();
		landscapeSubMenu();
		solutionsMainMenu();
		connectivityMainMenu();
		terrainToggle();
		landCoverToggle();
		becToggle();
		fragmentMainMenu();
		fragmentSubMenu();
		humanfragToggle();
		natfragToggle();
		connectivityToggle();
	}
}
