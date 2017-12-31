using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour {

	public fishCam cam;
	public lakeController controller;
	public GameObject stageOneWalls;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "MainCamera")
		{
			startEvent();
		}
	}

	void startEvent() {
		int num = controller.stageNum;
		controller.stageNum++;
		switch(num) {
		case 0:
			Debug.Log ("one");
			break;
		default:
			Debug.Log ("default");
			break;
		}
	}
}
