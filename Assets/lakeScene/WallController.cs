using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

	public GameObject panelOne;
	public GameObject panelTwo;
	int numWallsLeft;
	bool wallActive;

	// Use this for initialization
	void Start () {
		numWallsLeft = 3;
		wallActive = false;
		showPanelOne ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.touchCount > 0) && (wallActive)) {
			//Touch began, save position
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				Ray ray = GetComponent<Camera> ().ScreenPointToRay (Input.mousePosition);
				RaycastHit hit;
				Debug.Log ("Tap");
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					Debug.Log ("hit");
					if (hit.transform.tag == "retWall") {
						Debug.Log ("It worked");
						hit.transform.gameObject.SetActive (false);
						numWallsLeft--;
						if (numWallsLeft == 0)
							showPanelTwo ();
					}
				}
			}
		}
	}

	public void showPanelOne()
	{
		panelOne.SetActive (true);
	}

	public void hidePanelOne()
	{
		panelOne.SetActive (false);
		wallActive = true;
	}

	public void showPanelTwo()
	{
		panelTwo.SetActive (true);
		wallActive = false; //dont need to check raycasts anymore
	}

	public void hidePanelTwo()
	{
		panelTwo.SetActive (false);
	}
}
