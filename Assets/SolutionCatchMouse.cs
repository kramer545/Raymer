using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionCatchMouse : MonoBehaviour {
	public GameObject solutionText;
	public GameObject[] OtherTexts;
	public GameObject[] Icons;
	public bool iconsOn;
	public bool isActive;
	bool activateText;
	// Use this for initialization
	void Start () {
		iconsOn = false;
		isActive = true;
		activateText = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate() //to prevent closing menu and hitting button at once, enable icons at end of fram
	{
		if(iconsOn)
		{
			iconsOn = false;
			onClose ();
		}

		if(activateText)
		{
			solutionText.SetActive(true);
			activateText = false;
		}
	}
	
	void OnMouseDown(){
		if(isActive)
		{
			foreach(GameObject text in OtherTexts){
				text.SetActive(false);
			}
			activateText = true;

			foreach(GameObject icon in Icons){ //hide icons to prevent opening their menus when closing one
				icon.GetComponent<SolutionCatchMouse> ().isActive = false;
			}
		}
	}

	public void onClose() //reenables icons
	{
		foreach(GameObject icon in Icons){
			icon.GetComponent<SolutionCatchMouse> ().isActive = true;
		}
	}

	public void startIcons()
	{
		iconsOn = true;
	}
}
