using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocksController : MonoBehaviour {

	/*This script handles the dock area, where the user is in the fov of a fish, who cant cross under the rock covered docks
	Then after clicking the UI btn, this sends the cam to a new waypoint at end of dock, where they are eaten by a fish
	After this, the rocks are *cleared* and fish/cam goes under docks, before the ui goes back to main cam
	*/

	public Animator animator;
	public GameObject predatorFish;
	public GameObject blackoutPanel;
	public float blackoutTime = 0.5f;
	public GameObject cam;
	public GameObject rocks;
	public GameObject[] stageUI;
	int currStage;
	bool isBlackout;
	float currBlackoutTime;
	Vector3 camPos;
	Vector3 camRot;
	GameObject predatorClone;

	//3 UI stages, 1 is before gettting eaten, 2 is after eaten, 3 is after clearing rocks


	// Use this for initialization
	void Start () { //starts when set active, only once though
		currStage = 1;
		isBlackout = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(isBlackout)
		{
			if(Time.time - currBlackoutTime > blackoutTime)
			{
				blackoutPanel.SetActive (false);
				stageUI [1].SetActive (true);
				Destroy (predatorClone);
				isBlackout = false;
			}
		}
	}

	void OnEnable()
	{
		stageUI [0].SetActive (true);
		Debug.Log (camPos.x);
	}

	public void eatPlayer()
	{
		predatorClone = Instantiate (predatorFish);
	}

	public void swimOut()//first part, called by ui btn, starts anim to move cam around block and get eaten
	{
		animator.SetTrigger ("EdgeSwim");
		stageUI [0].SetActive (false);
	}

	public void activateBlackout()
	{
		blackoutPanel.SetActive (true);
		isBlackout = true;
		currBlackoutTime = Time.time;
	}

	public void removeRocks()
	{
		rocks.SetActive (false);
		animator.SetTrigger ("RockSwim");
		stageUI [1].SetActive (false);
	}

	public void showEnd()
	{
		stageUI [2].SetActive (true);
	}

	public void hideEnd()
	{
		stageUI [2].SetActive (false);
	}
}
