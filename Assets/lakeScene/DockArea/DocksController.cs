using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocksController : MonoBehaviour {

	/*This script handles the dock area, where the user is in the fov of a fish, who cant cross under the rock covered docks
	Then after clicking the UI btn, this sends the cam to a new waypoint at end of dock, where they are eaten by a fish
	After this, the rocks are *cleared* and fish/cam goes under docks, before the ui goes back to main cam
	*/

	public bool rocksExist;
	public GameObject predatorFish;
	//waypoint pos and rotation vectors, since only 2, easier to use seperate variables then arrays


	// Use this for initialization
	void Start () { //starts when set active, only once though
		rocksExist = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void eatPlayer()
	{
		GameObject clone = Instantiate (predatorFish);
	}
}
