using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Xeriscape_intro_script : MonoBehaviour {

	public GameObject intropanel;
	public GameObject instructionpanel;
	public GameObject taskPanel;
	public GameObject outropanel;
	public GameObject outroBtn;
    public GameObject cameraCollider;
    public GameObject NPCPanel;
	public Animator flyin;
	public Text seedUI;
	private Vector3 firstpoint;
	private Vector3 secondpoint;
	private float xAngle = 0.0f;
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f;
	private float yAngTemp = 0.0f;
	private int seedscollected = 0;
	private int seedstotal = 5;
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;
	public bool lookEnabled = false;
	private bool flyout = false;
	public GameObject[] moveBtns;
	public GameObject npc;
    public GameObject bloomBtn;
    public GameObject bloomPanel;
	public GameObject dpad;
    public GameObject[] rounds;
 
    void Update () {
        if(!flyout)
        {
            if (seedscollected < seedstotal)
                seedUI.text = "View at least " + seedstotal + " flowers\n" + seedscollected + " / " + seedstotal + " flowers examined";
            else
                seedUI.text = "Feel free to keep exploring";
        }
		if(lookEnabled){
            cameraCollider.SetActive(true);
			if(Input.touchCount > 0) {
			//Touch began, save position
			if(Input.GetTouch(0).phase == TouchPhase.Began) {
			firstpoint = Input.GetTouch(0).position;
			xAngTemp = xAngle;
			yAngTemp = yAngle;
			}
			//Move finger by screen
			if(Input.GetTouch(0).phase==TouchPhase.Moved) {
			 secondpoint = Input.GetTouch(0).position;
			 //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
			 xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
			 yAngle = yAngTemp - (secondpoint.y - firstpoint.y) *90.0f / Screen.height;
             //Rotate camera
             if (yAngle > 90)//prevents camera flipping upside down
                yAngle = 90;
             else if (yAngle < -90)
                yAngle = -90;
			 this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
			}
		   }
		}
		if (seedscollected == seedstotal && !flyout) {
			outroBtn.SetActive (true);
			taskPanel.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (-55, 60, 0);
			seedUI.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (-55, -100, 0);
		}
    }
	public void incrementSeeds(){
		seedscollected += 1;
	}

	public void enableOutro()
	{
		outroBtn.SetActive (false);
		lookEnabled = false;
		instructionpanel.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		outropanel.SetActive(true);
		flyout = true;
		for(int x = 0;x<moveBtns.Length;x++)
		{
			moveBtns [x].SetActive (false);
		}
        taskPanel.SetActive(false);
        seedUI.text = "";
		dpad.SetActive (false);
        bloomBtn.SetActive(false);
		transform.GetChild (0).gameObject.SetActive (false);
        for (int x = 0; x < rounds.Length; x++)
            rounds[x].SetActive(false);
	}
	
	void enableCanvas () {
		Vector3 rot = transform.localRotation.eulerAngles;
        yAngle = rot.x;
        xAngle = rot.y;
		intropanel.SetActive(true);
		flyin.enabled = false;
	}

    void enableNPC()
    {
        NPCPanel.SetActive(true);
    }

    public void NPCBtnClick()
    {
        NPCPanel.SetActive(false);
        GetComponent<Animator>().Play("flyin_Xeri_2");
    }

    public void toggleInstructions (bool tog) {
		instructionpanel.SetActive(tog);
	}
	
	public void disableIntroPanel () {
		seedUI.gameObject.SetActive(true);
		taskPanel.SetActive (true);
		intropanel.SetActive(false);
		instructionpanel.SetActive(true);
		lookEnabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		for(int x = 0;x<moveBtns.Length;x++)
		{
			moveBtns [x].SetActive (true);
		}
		dpad.SetActive (true);
        bloomBtn.SetActive(true);
		//move/setup NPC
		npc.transform.position = new Vector3(328,0.29F,190);
		npc.transform.eulerAngles = new Vector3 (0, 185, 0);
        npc.transform.localScale = new Vector3(2.5F, 2.5F, 2.5F);
		npc.GetComponent<npcStayStill> ().yRot = 185;
	}
	
	public void nextLevel () {
		outropanel.SetActive(false);
		gameObject.GetComponent<Animation>().Play("flyout_xeriscape");
	}
	
	public void transitionLevel () {
		SceneManager.LoadScene("PlantGarden");
	}

    public void showBloom()
    {
        bloomBtn.SetActive(false);
        bloomPanel.SetActive(true);
		instructionpanel.SetActive (false);
    }

    public void hideBloom()
    {
        bloomBtn.SetActive(true);
        bloomPanel.SetActive(false);
		instructionpanel.SetActive (true);
    }
}
