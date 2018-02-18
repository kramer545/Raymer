using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandController : rotateCam {

	public GameObject panelOne;
	public GameObject panelTwo;
	int numItemsLeft;
	bool garbageActive;

	// Use this for initialization
	void Start () {
		numItemsLeft = 3;
	}
	
	// Update is called once per frame
	void Update () {
		base.rotate();
		if ((Input.touchCount > 0) && (garbageActive)) {
			//Touch began, save position
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				Ray ray = GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				Debug.Log ("Tap");
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					Debug.Log ("hit "+ hit.transform.name);
					if (hit.transform.tag == "garbage") {
						Debug.Log ("It worked");
						hit.transform.gameObject.SetActive (false);
						numItemsLeft--;
						if (numItemsLeft == 0)
							showPanelTwo ();
					}
				}
			}
		}
	}

	public void removeItem() {
		numItemsLeft--;
		if(numItemsLeft == 0) {
			showPanelTwo ();
		}
	}

	public void showPanelOne()
	{
		panelOne.SetActive (true);
	}

	public void hidePanelOne()
	{
		panelOne.SetActive (false);
		garbageActive = true;
	}

	public void showPanelTwo()
	{
		panelTwo.SetActive (true);
		garbageActive = false; //dont need to check raycasts anymore
	}

	public void hidePanelTwo()
	{
		panelTwo.SetActive (false);
	}
}
