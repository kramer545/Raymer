using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aboutBtn : MonoBehaviour {//credits btn for cake

	public GameObject menu;
	public GameObject instructions;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void show()
	{
		if(!menu.activeSelf)
		{
			menu.SetActive (true);
			instructions.SetActive (false);
			transform.GetChild(0).GetComponent<Text>().text = "Return to Main Menu";
		}
		else
		{
			menu.SetActive (false);
			instructions.SetActive (true);
			transform.GetChild(0).GetComponent<Text>().text = "Credits";
		}
	}
}
