using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapMenuController : MonoBehaviour {

	public Material[] MapMaterials;
	public GameObject[] MenuButtons;
	public GameObject[] LayerButtons;
	public GameObject[] LayerTexts;
	public GameObject[] LayerLegends;
	public GameObject[] titles;
	public GameObject ReturnButton;
	public GameObject MenuBackdrop;
	public GameObject SolutionIcons;
	public GameObject SolutionTexts;
	public GameObject OkanaganText;
	public GameObject[] startTexts;
	bool menuEnabled = false;
	
	// Listen on each menu button for an action
	void Start () {
		MapMaterials[0].renderQueue = 1999;
		for (int a = 0; a < 8; a++){
			Button menuButton = MenuButtons[a].GetComponent<Button>();
			int index = a;
			menuButton.onClick.AddListener(() => menuListener(index));
		}
		/*
		for (int a = 0; a < 11; a++){
			Toggle layerToggle = LayerButtons[a].GetComponent<Toggle>();
			int index = a;
			layerToggle.onValueChanged.AddListener(delegate{toggleListener(index);});
		}
		*/
		for (int a = 0; a < 11; a++){
			MapMaterials[a].mainTextureScale = new Vector2(0.0f, -0.0f);
		}
		/*
		foreach(GameObject layer in LayerButtons){
				Color colOff;
				ColorUtility.TryParseHtmlString("808080FF", out colOff);
				ColorBlock cb = layer.GetComponent<Toggle>().colors;
				cb.normalColor = Color.gray;
				layer.GetComponent<Toggle>().colors = cb;
				Toggle toggle = layer.GetComponent<Toggle>();
				toggle.isOn = false;
				layer.SetActive(false);
		}
		*/
		foreach(GameObject text in LayerTexts){
				text.SetActive(false);
		}
		LayerTexts [11].SetActive (true);//instructions active at start
		foreach(GameObject legend in LayerLegends){
				legend.SetActive(false);
		}
		MapMaterials[0].mainTextureScale = new Vector2(1.0f, -1.0f);
		/*
		/ColorBlock colblock = LayerButtons[0].GetComponent<Toggle>().colors;
		colblock.normalColor = Color.white;
		LayerButtons[0].GetComponent<Toggle>().colors = colblock;
		*/
		Button returnButton = ReturnButton.GetComponent<Button>();
		returnButton.onClick.AddListener(() => menuListener(8));
		ReturnButton.SetActive(false);
		SolutionIcons.SetActive(false);
		SolutionTexts.SetActive(false);
		gameObject.SetActive(false);
		OkanaganText.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	//On a main menu button being clicked
	void menuListener(int index){
		LayerTexts [11].SetActive (false);
	switch (index)
      {
		case 0: //Okanagan Region
			MenuButtons [0].transform.parent.gameObject.SetActive (false);
			OkanaganText.SetActive (true);
			ReturnButton.SetActive (true);
              break;
        case 1: //Work Like a Bee
                //MenuButtons[0].transform.parent.gameObject.SetActive(false);
                LayerTexts[11].SetActive(true);
                //MenuButtons[6].SetActive(!MenuButtons[6].activeSelf);
              MenuButtons[7].SetActive(!MenuButtons[7].activeSelf);
              break;
        case 2: //Map Okanagan
			  MenuButtons[0].transform.parent.gameObject.SetActive(false);
			  ReturnButton.SetActive(true);
			  LayerButtons[0].SetActive(true); //Terrain
			  LayerButtons[1].SetActive(true); //LandCover
			  LayerButtons[2].SetActive(true); //BEC
			  MenuBackdrop.SetActive(false);
			startTexts [0].SetActive (true);
			  
			  //LayerButtons[0].GetComponent<Toggle>().isOn = true;
              break;
		case 3: //Analyze Connectivity
			  MenuButtons[0].transform.parent.gameObject.SetActive(false);
			  MapMaterials[0].SetColor("_Color", Color.gray);
			  ReturnButton.SetActive(true);
			  LayerButtons[10].SetActive(true); //TEM Map
			  MenuBackdrop.SetActive(false);
			startTexts [2].SetActive (true);
              break;
		case 4: //Explore Solutions
			  MenuButtons[0].transform.parent.gameObject.SetActive(false);
			  SolutionIcons.SetActive(true);
			  ReturnButton.SetActive(true);
			  MenuBackdrop.SetActive(false);
			  SolutionIcons.SetActive(true);
			  SolutionTexts.SetActive(true);
			startTexts [3].SetActive (true);
              break;
		case 5: //Understand Fragmentation
			  MenuButtons[0].transform.parent.gameObject.SetActive(false);
			  //MapMaterials[1].mainTextureScale = new Vector2(1.0f, -1.0f);
			  ReturnButton.SetActive(true);
			  LayerButtons[3].SetActive(true); //slope
			  LayerButtons[4].SetActive(true); //water
			  LayerButtons[5].SetActive(true); //private property
			  LayerButtons[6].SetActive(true); //agriculture
			  LayerButtons[7].SetActive(true); //roads
			  LayerButtons[8].SetActive(true); //forestry
			  LayerButtons[9].SetActive(true); //present land use
			  MenuBackdrop.SetActive(false);
			startTexts [1].SetActive (true);
              break;

            case 6: //Salmon Level
                MenuButtons[0].transform.parent.gameObject.SetActive(false);
                SceneManager.LoadScene("LakeDemo");
                break;
            case 7: //Bee Level
                MenuButtons[0].transform.parent.gameObject.SetActive(false);
                SceneManager.LoadScene("PatsGarden");
                break;

            case 8: //Return to Main Menu
                foreach (GameObject layer in LayerButtons)
                {
                    //Toggle toggle = layer.GetComponent<Toggle>();
                    //toggle.isOn = false;
                    layer.SetActive(false);
                    layer.GetComponent<Image>().color = new Color(1, 1, 1, 0.65F);
                }
                LayerButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);//reset default layer to active
                MapMaterials[0].mainTextureScale = new Vector2(1.0f, -1.0f);
                for (int x = 1; x < MapMaterials.Length; x++)
                {
                    MapMaterials[x].mainTextureScale = new Vector2(0, 0);
                }
                /*
                ColorBlock cb = LayerButtons[0].GetComponent<Toggle>().colors;
                cb.normalColor = Color.white;
                LayerButtons[0].GetComponent<Toggle>().colors = cb;
                */
                MenuButtons[0].transform.parent.gameObject.SetActive(true);
                foreach (GameObject text in LayerTexts)
                {
                    text.SetActive(false);
                }
                foreach (GameObject legend in LayerLegends)
                {
                    legend.SetActive(false);
                }
                ReturnButton.SetActive(false);
                MenuBackdrop.SetActive(true);
                SolutionIcons.SetActive(false);
                SolutionTexts.SetActive(false);
                OkanaganText.SetActive(false);
                LayerTexts[11].SetActive(true);
                MapMaterials[0].SetColor("_Color", Color.white);
                break;


        }
	}
	
	void toggleListener(int index){
		/* switched to activateLayer()
		if(LayerButtons[index].GetComponent<Toggle>().isOn){
			MapMaterials[index].mainTextureScale = new Vector2(1.0f, -1.0f);
			ColorBlock cb = LayerButtons[index].GetComponent<Toggle>().colors;
			cb.normalColor = Color.white;
			LayerButtons[index].GetComponent<Toggle>().colors = cb;
			foreach(GameObject text in LayerTexts){
				text.SetActive(false);
			}
			foreach(GameObject legend in LayerLegends){
				legend.SetActive(false);
			}
			LayerTexts[index].SetActive(true);
			LayerLegends[index].SetActive(true);

			
		}else{
			MapMaterials[index].mainTextureScale = new Vector2(0.0f, -0.0f);
			ColorBlock cb = LayerButtons[index].GetComponent<Toggle>().colors;
			cb.normalColor = Color.gray;
			LayerButtons[index].GetComponent<Toggle>().colors = cb;
			LayerTexts[index].SetActive(false);
			LayerLegends[index].SetActive(false);
		}
		*/
	}

	public void activateLayer(int index)
	{
		if(index == 0)
		{
			LayerTexts[0].SetActive(true);
			LayerLegends[0].SetActive(true);
			LayerTexts[12].SetActive(false);
            LayerButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 1);
		}
		else
		{
			LayerTexts[0].SetActive(false);
			LayerLegends[0].SetActive(false);
            LayerButtons[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.65F);
        }
		for(int x = 1;x<MapMaterials.Length;x++)
		{
			MapMaterials[x].mainTextureScale = new Vector2(0.0f, -0.0f);
			//ColorBlock cb = LayerButtons[x].GetComponent<Toggle>().colors;
			//cb.normalColor = Color.gray;
			//LayerButtons[x].GetComponent<Toggle>().colors = cb;
			LayerTexts[x].SetActive(false);
			LayerLegends[x].SetActive(false);
            LayerButtons[x].GetComponent<Image>().color = new Color(1, 1, 1, 0.65F);
            if (x == index)
			{
				MapMaterials[index].mainTextureScale = new Vector2(1.0f, -1.0f);
				//cb = LayerButtons[index].GetComponent<Toggle>().colors;
				//cb.normalColor = Color.white;
				//LayerButtons[index].GetComponent<Toggle>().colors = cb;
				foreach(GameObject text in LayerTexts){
					text.SetActive(false);
				}
				foreach(GameObject legend in LayerLegends){
					legend.SetActive(false);
				}
				LayerTexts[index].SetActive(true);
				LayerLegends[index].SetActive(true);
                LayerButtons[index].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            
		}
	}

	public void showTitle(int num)
	{
		for(int x = 0;x<titles.Length;x++)
		{
			titles [x].SetActive (false);
		}
		if(num != -1)
			titles [num].SetActive (true);
	}
}
