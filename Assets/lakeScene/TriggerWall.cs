using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour {

	public fishCam cam;
	public lakeController controller;
	public GameObject stageOneWalls;
	public GameObject stageTwoWalls;
	public GameObject fish;
	public GameObject dockRocks;
	public float turnSpeed;
	public float fishSpeed;
	public GameObject blackScreen;
	float timer;


	private Vector3 camStartPos;
	private Quaternion camStartRot;
	private bool fishMoving;
	private bool camTurning;

	// Use this for initialization
	void Start () {
		fishMoving = false;
		camTurning = false;
		timer = 0;
		camStartPos = cam.gameObject.transform.position;
		camStartRot = cam.gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		stageOneUpdates ();
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
			fish.SetActive (true);
			fish.transform.position = (cam.gameObject.transform.position - (cam.gameObject.transform.forward * 10));
			fish.transform.LookAt (cam.gameObject.transform);
			fish.transform.position = new Vector3 (fish.transform.position.x, cam.gameObject.transform.position.y - 1.5f, fish.transform.position.z);
			cam.isActive = false;
			camTurning = true;
			Debug.Log ("one");
			break;
		default:
			Debug.Log ("default");
			break;
		}
	}

	public void stageOneUpdates() {
		if(camTurning) {
			Vector3 dir = fish.transform.position - cam.gameObject.transform.position;
			dir.y = 0;
			cam.gameObject.transform.rotation = Quaternion.RotateTowards(cam.gameObject.transform.rotation, Quaternion.LookRotation (dir), turnSpeed * Time.deltaTime);
			timer = timer + Time.deltaTime;
			if(timer > 0.9) {
				fishMoving = true;
			}
			if(timer > 1) {
				camTurning = false;
			}
		}
		if(fishMoving) {
			timer = timer + Time.deltaTime;
			fish.transform.position = Vector3.MoveTowards (fish.transform.position, 
				new Vector3(cam.gameObject.transform.position.x, cam.gameObject.transform.position.y-1.5f, cam.gameObject.transform.position.z), fishSpeed * Time.deltaTime);
			fish.GetComponent<Animation> ().Play ();
			if(timer > 1.8) {
				blackScreen.SetActive (true);
				fish.SetActive (false);
				cam.gameObject.transform.position = camStartPos;
				cam.gameObject.transform.rotation = camStartRot;
				dockRocks.SetActive (false);
			}
			if(timer > 2.3) {
				timer = 0;
				stageTwoWalls.SetActive (true);
				fishMoving = false;
				blackScreen.SetActive (false);
				cam.isActive = true;
				stageOneWalls.SetActive (false);
			}
		}
	}
}
