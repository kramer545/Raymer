using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeeMenu : MonoBehaviour {

	public GameObject scorepanel;
	public bool fade;
	public GameObject flowerpanel;
	public GameObject hexagon;
	public GameObject panel_end;
	public GameObject endMenuButton;
	public GameObject intropanel;
	public GameObject concludepanel;
	public GameObject garden;
	public GameObject fadeOut;
	public GameObject finishBtn;
	public bool reminded = false;
	public Text final;
    public bool finished;
    bool lookEnabled = false;
	public viewController viewContrl;
	public int[] bloomValues; // 7 indexs for each month, value per month is number flowers that bloom in that month, just need 2 or more for value to be compelete
	public GameObject beeBar;
	public GameObject ezBar;
    private Vector3 firstpoint;
    private Vector3 secondpoint;
    private float xAngle = 0.0f;
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f;
    private float yAngTemp = 0.0f;
    public GameObject instructions;
	public int maxBeeScore = 120;
	public int maxEaseScore = 200;

	public int numWater = 0;
	public int numShelter = 0;
	public int numObj = 0;//num filled tiles
	int totalObjs;

	public GameObject[] infoPanels;
	public GameObject infoPanelParent;
	public GameObject autoFillBtn;

	public GameObject[] monthBars;

    public bool end = false;
	static int maxsize = 19;
    private int[] plantCount;
    public int beeFriendScore;
    public int ezScore;
	public GameObject[] buttons = new GameObject[maxsize];
	public GameObject[] plants = new GameObject[maxsize];
    public GameObject[] moveBtns;
	public int[,] plant_values = new int[,] {// (bee,grow ez), 1 = low, 5 = high
        {5,1}, //Gaillardia 1
		{1,1}, //Petunia 2
		{1,3}, //Primrose 3
		{5,2}, //Nodding Onion 4 
		{5,3}, //Bee Balm 5
		{3,1}, //Grape Hya 6 
		{4,4}, //Gold Aster 7
		{5,2}, //SunFlower 8 
		{4,3}, //Mother Thyme 9
		{5,3}, //White Clover 10
		{4,3}, //English Thyme 11
		{4,4}, //Rabbit Brush 12
		{3,3}, //Wax Currant 13 
		{3,4}, //SnowBerry 14
		{0,0}, //Dirt 15
		{0,0}, //Stone 16
		{0,0}, //Water, must be third last 17
		{0,0}, //House, must be second last 18
		{0,0} }; //Remove, must be last 19
    public bool[,] plant_bloom_values;
	public int selected;
	public int fadeval;
	public int maxBarSize = 120;
	Gradient g;
	// Use this for initialization
	void Start () {
        plant_bloom_values = new bool[,] {// months flower blooms from march - sept, 1 = true = in bloom in month
		{false,false,false,false,true,true,false}, //Gaillardia
		{false,true,true,true,true,true,true}, //Petunia
		{false,true,true,true,true,false,false}, //Primrose
		{false,false,true,true,false,false,false}, //Nodding Onion
		{false,false,false,true,true,true,true}, //Bee Balm
		{true,true,false,false,false,false,false}, //Grape Hya
		{false,false,false,true,true,true,false}, //Gold Aster
		{false,false,false,false,false,true,true}, //SunFlower
		{false,false,false,true,false,false,false}, //Mother Thyme
		{false,true,true,true,true,true,true}, //White Clover
		{false,false,false,true,false,false,false}, //English Thyme
		{false,false,false,false,false,true,true}, //Rabbit Brush
		{false,true,true,true,true,false,false}, //Wax Currant
		{false,false,true,false,false,false,false}, //SnowBerry
		{false,false,false,false,false,false,false}, //Dirt
		{false,false,false,false,false,false,false}, //Stone
		{false,false,false,false,false,false,false}, //Water
		{false,false,false,false,false,false,false}, //House
		{false,false,false,false,false,false,false} }; //Remove
        totalObjs  = hexagon.transform.childCount;//total num tiles
        finished = false;
        selected = maxsize - 1;
        plantCount = new int[maxsize];
        bloomValues = new int[] { 0, 0, 0, 0, 0, 0, 0 };
        beeFriendScore = 0;
        ezScore = 0;
        finished = false;
        fade = false;
        fadeval = 0;
        //setting up color gradient for use in score bars (green -> red)
        g = new Gradient ();
		GradientColorKey[] gck = new GradientColorKey[2];
		gck [0].color = new Color32 (54, 126, 19,255);
		gck [0].time = 0F;
		gck [1].color = new Color32 (255, 0, 0,255);
		gck [1].time = 1F;
		GradientAlphaKey[] gak = new GradientAlphaKey[2];
		gak [0].alpha = 1.0F;
		gak [0].time = 0F;
		gak [1].alpha = 1.0F;
		gak [1].time = 1.0F;
		g.SetKeys (gck, gak);
        for(int x = 0;x<infoPanels.Length;x++)
        {
            infoPanels[x].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (lookEnabled)
        {
            if (Input.touchCount > 0)
            {
                //Touch began, save position
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    firstpoint = Input.GetTouch(0).position;
                    xAngTemp = xAngle;
                    yAngTemp = yAngle;
                }
                //Move finger by screen
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    secondpoint = Input.GetTouch(0).position;
                    //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                    xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
                    yAngle = yAngTemp - (secondpoint.y - firstpoint.y) * 90.0f / Screen.height;
                    //Rotate camera
                    this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                }
            }
        }
            if (fade){
			if(fadeval==254){
				SceneManager.LoadScene("MainMenu"); //new main menu
			}
			fadeOut.GetComponent<Image>().color = new Color32(0,0,0,(byte)fadeval);
			fadeval += 1;
		}
		//scaling bars in score to illustrate value
		if(beeFriendScore > maxBarSize)
			beeBar.transform.localScale = new Vector3(maxBarSize,1,1);
		else if(beeFriendScore < 0)
			beeBar.transform.localScale = new Vector3(0,1,1);
		else
			beeBar.transform.localScale = new Vector3(beeFriendScore,1,1);
		
		beeBar.GetComponent<Image> ().color = g.Evaluate(1-((float)beeFriendScore / 120F));//color determined based on percent between 0-1 of gradient, this gradient flipped

		if(ezScore > 200)
			ezBar.transform.localScale = new Vector3(200,1,1);
		else if(ezScore < 0)
			ezBar.transform.localScale = new Vector3(0,1,1);
		else
			ezBar.transform.localScale = new Vector3(ezScore,1,1);

		ezBar.GetComponent<Image> ().color = g.Evaluate(((float)ezScore / 200F));

		for(int x = 0;x<bloomValues.Length;x++)
		{
			if(bloomValues[x] >= 2)
			{
				monthBars[x].transform.localScale = new Vector3(2,1,1);
				monthBars [x].GetComponent<Image> ().color = g.Evaluate (0);
			}
			else if(bloomValues[x] <= 0)
				monthBars[x].transform.localScale = new Vector3(0,1,1);
			else
			{
				monthBars[x].transform.localScale = new Vector3(1,1,1);
				monthBars [x].GetComponent<Image> ().color = g.Evaluate (0.5F);
			}
		}
		if(((float)numObj/(float)totalObjs) > 0.75 && !end){
			finishBtn.SetActive (true);
		}
	}
	
	void doneAnim() {
		intropanel.SetActive(true);
	}

	public void endGarden()
	{
		string final_string = "You garden has ";
		//Bee scoring
		if(beeFriendScore <= (maxBeeScore/3)){
			final_string = final_string + "very little or no pollen and nectar sources. ";
		}
		if(beeFriendScore > (maxBeeScore/3) && beeFriendScore <= ((maxBeeScore/3)*2)){
			final_string = final_string + "a fair amount of pollen and nectar, but could use more. ";
		}
		if(beeFriendScore > (2*(maxBeeScore/3))){
			final_string = final_string + "a great amount of pollen and nectar. ";
		}
		//Ease scoring
		if(ezScore <= (maxEaseScore/3)){
			final_string = final_string + "It is very easy to plant and maintain. ";
		}
		if(ezScore > (maxEaseScore/3) && ezScore <= ((maxEaseScore/3)*2)){
			final_string = final_string + "It takes an average amount of effort to plant and maintain. ";
		}
		if(ezScore > ((maxEaseScore/3)*2)){
			final_string = final_string + "It is hard to plant and/or maintain the garden. ";
		}

		if (numWater > 0)
			final_string += "Bees and other pollinators have access to a safe source of water. ";
		else
			final_string += "Bees and other pollinators do not have access to a safe water source. ";

		if (numShelter > 0)
			final_string += "Bees have access to shelter. ";
		else
			final_string += "Bees don't have access to shelter. ";

		bool bloomsGood = true;
		for(int x = 0;x<bloomValues.Length;x++)
		{
			if (bloomValues [x] < 2)
				bloomsGood = false;
		}
		if(bloomsGood)
			final_string += "It has blooms throughout the blooming seasons.";
		else
			final_string += "It doesn't have enough blooms throughout the blooming seasons to sustain the food needs of bees and other pollinators.";
		final.text = final_string;
		panel_end.SetActive(true);
		finished = true;
		end = true;
		finishBtn.SetActive (false);
	}


    public void examineGarden()
    {
        finished = true;
        concludepanel.SetActive(false);
        garden.SetActive(true);
        lookEnabled = true;
        GetComponent<Animator>().enabled = false;

        for (int x = 0; x < moveBtns.Length; x++)
            moveBtns[x].SetActive(true);
		GameObject[] hexs = GameObject.FindGameObjectsWithTag ("hex");
		for(int x = 0;x<hexs.Length;x++)
		{
			hexs[x].SetActive(false);
		}
    }
	
	public int[] getArrayPlant(){
		return plantCount;
	}
	
	public void setArrayPlant(int[] arr){
		plantCount = arr;
	}
	
	public void startBuild() {
		flowerpanel.SetActive(true);
		scorepanel.SetActive(true);
		hexagon.SetActive(true);
		intropanel.SetActive(false);
		infoPanelParent.SetActive (true);
		autoFillBtn.SetActive (true);
        instructions.SetActive(true);
	}
	
	public void fadeOutStart() {
		fade = true;
		fadeOut.SetActive(true);
	}
	
	public void concludePanel() {
		scorepanel.SetActive(false);
		flowerpanel.SetActive(false);
		panel_end.SetActive(false);
		concludepanel.SetActive(true);
		infoPanelParent.SetActive (false);
		autoFillBtn.SetActive (false);
        instructions.SetActive(false);
	}
	
	public void finishlvl(){
        lookEnabled = false;
        for (int x = 0; x < moveBtns.Length; x++)
            moveBtns[x].SetActive(false);
		GameObject[] hexs = GameObject.FindGameObjectsWithTag("hex");
		for(int x = 0;x<hexs.Length;x++)
		{
			hexs[x].SetActive(false);
		}
        finished = true;
		concludepanel.SetActive(false);
		garden.SetActive(true);
        GetComponent<Animator>().enabled = true;
		viewContrl.view ();
        gameObject.GetComponent<Animation>().Play("camera_flyout_raymer");
	}
	
	public int getSelected(){
		return selected;
	}
	
	public int getMax(){
		return maxsize;
	}
	
	public void closeEndMenu(){
		panel_end.SetActive(false);
        finished = false;
		end = false;
	}
	
	public int[,] getValues(){
		return plant_values;
	}

	public bool[,] getBloomValues(){
		return plant_bloom_values;
	}
	
	public GameObject[] getPlants(){
		return plants;
	}
	
	public void updateScore(int beeFriend, int growEase, bool[] bloom, bool addBloom){
		beeFriendScore += beeFriend;
		ezScore += growEase;
		for(int x = 0;x<bloomValues.Length;x++)
		{
			if (bloom [x])
			{
				if (addBloom)
					bloomValues [x]++;
				else//when removing/replacing flower
					bloomValues [x]--;
			}
		}
	}
	
	public void btnPressed(int num){
		for(int x = 0;x < buttons.Length;x++)
		{
			buttons[x].GetComponent<Image>().color = new Color32(255,255,255,90);
		}
		GameObject obj = buttons [num];
		selected = num;
		obj.GetComponent<Image>().color = new Color32(255,255,255,255);
		for(int x = 0;x<infoPanels.Length;x++)
		{
			infoPanels [x].SetActive (false);
		}
		if(num < infoPanels.Length)
			infoPanels [num].SetActive (true);
	}
}
