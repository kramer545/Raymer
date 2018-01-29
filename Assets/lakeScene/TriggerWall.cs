using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour {

	public fishCam cam;
	public lakeController controller;
	public GameObject fish;
	public GameObject dockRocks;
	public float turnSpeed;
	public float fishSpeed;
	public GameObject blackScreen;
	public GameObject playerObj;
	float timer;
	bool eventActive;


	private Vector3 camStartPos;
	private Quaternion camStartRot;
	private bool fishMoving;
	private bool camTurning;
	private bool fishPoison;

	// Use this for initialization
	void Start () {
		eventActive = false;
		fishMoving = false;
		camTurning = false;
		fishPoison = false;
		timer = 0;
		camStartPos = cam.gameObject.transform.position;
		camStartRot = cam.gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		stageOneUpdates();
		stageFourUpdates();
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "MainCamera")
		{
			startEvent();
		}
	}

	void startEvent() {
		if (!eventActive) {
			eventActive = true;
			int num = controller.stageNum;
			controller.stageNum++;
			switch(num) {
			case 0: //dock scene
				fish.SetActive (true);
				fish.transform.position = (cam.gameObject.transform.position - (cam.gameObject.transform.forward * 10));
				fish.transform.LookAt (cam.gameObject.transform);
				fish.transform.position = new Vector3 (fish.transform.position.x, cam.gameObject.transform.position.y - 1.5f, fish.transform.position.z);
				cam.isActive = false;
				camTurning = true;
				break;
			case 1: //boat scene?
				Debug.Log ("hit stage 2");
				controller.changeWalls ();
				eventActive = false;
				break;
			case 2: //garbage island
				Debug.Log ("hit stage 3");
				controller.changeWalls ();
				eventActive = false;
				break;
			case 3: //pollution?
				Debug.Log ("hit stage 4");
				controller.setPolluted ();
				timer = 0;
				fishPoison = true;
				break;
			default:
				break;
			}
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
				fishMoving = false;
				blackScreen.SetActive (false);
				cam.isActive = true;
				controller.changeWalls ();
				eventActive = false;
			}
		}
	}

	public void stageFourUpdates() {
		if (fishPoison) {
			timer += Time.deltaTime;
			if(timer < 2) {
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionX = (0.5f * timer);
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionY = (0.5f * timer);
			} else if (timer < 4 && timer > 2) {
				playerObj.GetComponent<MorePPEffects.Ripple>().distortion = (1.5f * (timer-2.0f));
			} else if (timer < 6 && timer > 4) {
				playerObj.GetComponent<MorePPEffects.Headache>().strength = ((timer/2)-4.0f);
			} else if (timer > 6 && timer < 8) {
				Debug.Log ("done poisioning");
				blackScreen.SetActive (true);
			} else if (timer > 7) {
				eventActive = false;
				blackScreen.SetActive (false);
				fishPoison = false;
			}
		}
	}
}
