using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour {

	public fishCam cam;
	public lakeController controller;
	public GameObject fish;
	public float turnSpeed;
	public float fishSpeed;
	public GameObject blackScreen;
	public GameObject playerObj;
	public Vector3 playerObjPos;
	public Vector3 playerObjRot;
	public GameObject fishChokeBubbles;
	public GameObject garbage;
	public GameObject aboveCam;
	public GameObject abovePanel;
	float timer;
	bool eventActive;


	private Vector3 camStartPos;
	private Quaternion camStartRot;
	private bool fishMoving;
	private bool camTurning;
	private bool fishPoison;
	private bool fishPoison2;
	private bool fishChoke;
	private int turnDir;
	private float centerPoint;

	// Use this for initialization
	void Start () {
		eventActive = false;
		fishMoving = false;
		camTurning = false;
		fishPoison = false;
		fishPoison2 = false;
		fishChoke = false;
		turnDir = 1;
		timer = 0;
		camStartPos = cam.gameObject.transform.position;
		camStartRot = cam.gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		stageOneUpdates();
		stageTwoUpdates();
		stageFourUpdates();
		stageThreeUpdates ();
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
				fishSpeed = Vector3.Distance (cam.gameObject.transform.position, fish.gameObject.transform.position)/2;
				Debug.Log (fishSpeed);
				fish.SetActive (true);
				fish.transform.LookAt (cam.gameObject.transform);
				rotateCam.isActive = false;
				camTurning = true;
				break;
			case 1: //boat scene?
				controller.setPolluted ();
				timer = 0;
				fishPoison = true;
				break;
			case 2: //garbage island
				centerPoint = playerObj.transform.eulerAngles.y;
				timer = 0;
				fishChoke = true;
				fishChokeBubbles.transform.position = playerObj.transform.position;
				fishChokeBubbles.transform.position = new Vector3 (fishChokeBubbles.transform.position.x, fishChokeBubbles.transform.position.y - 5, fishChokeBubbles.transform.position.z);
				break;
			case 3: //pollution
				controller.setPolluted ();
				timer = 0;
				fishPoison2 = true;
				break;
			default:
				break;
			}
		}
	}

	public void stageOneUpdates() { //eaten
		if(camTurning) {
			Vector3 dir = fish.transform.position - cam.gameObject.transform.position;
			dir.y = dir.y + 5;
			cam.gameObject.transform.rotation = Quaternion.RotateTowards(cam.gameObject.transform.rotation, Quaternion.LookRotation (dir), turnSpeed * Time.deltaTime);
			timer = timer + Time.deltaTime;
			fishMoving = true;
			if(timer > 1) {
				camTurning = false;
			}
		}
		if(fishMoving) {
			timer = timer + Time.deltaTime;
			fish.transform.position = Vector3.MoveTowards (fish.transform.position, 
				new Vector3(cam.gameObject.transform.position.x, cam.gameObject.transform.position.y-2.5f, cam.gameObject.transform.position.z), fishSpeed * Time.deltaTime);
			fish.GetComponent<Animation> ().Play ();
			if(timer > 2.2) {
				blackScreen.SetActive (true);
				fish.SetActive (false);
				cam.gameObject.transform.position = camStartPos;
				cam.gameObject.transform.rotation = camStartRot;
			}
			if(timer > 3.2) {
				timer = 0;
				fishMoving = false;
				blackScreen.SetActive (false);
				rotateCam.isActive = true;
				controller.changeWalls ();
				eventActive = false;
				controller.areaClicked (2);
			}
		}
	}

	public void stageTwoUpdates() { //poison
		if (fishPoison) {
			timer += Time.deltaTime;
			if(timer < 2) {
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionX = (0.5f * timer);
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionY = (0.5f * timer);
			} else if (timer < 4 && timer > 2) {
				playerObj.GetComponent<MorePPEffects.Ripple>().distortion = (1.5f * (timer-2.0f));
			} else if (timer < 6 && timer > 4) {
				playerObj.GetComponent<MorePPEffects.Headache>().strength = ((timer/2)-4.0f);
				playerObj.transform.eulerAngles = new Vector3 (0, playerObj.transform.rotation.y, 180 * (timer / 2) - 4.0f);
				fishCam.setActive (false);
			} else if (timer > 6 && timer < 7) {
				blackScreen.SetActive (true);
			} else if (timer > 7) {
				fishCam.setActive (true);
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionX = 1;
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionY = 1;
				playerObj.GetComponent<MorePPEffects.Ripple>().distortion = 0;
				playerObj.GetComponent<MorePPEffects.Headache>().strength = 0;
				playerObj.transform.position = playerObjPos;
				playerObj.transform.eulerAngles = playerObjRot;
				eventActive = false;
				blackScreen.SetActive (false);
				fishPoison = false;
				controller.changeWalls();
				controller.setUnderWater ();
				controller.areaClicked (4);
			}
		}
	}

	public void stageFourUpdates() { //poison
		if (fishPoison2) {
			timer += Time.deltaTime;
			if(timer < 2) {
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionX = (0.5f * timer);
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionY = (0.5f * timer);
			} else if (timer < 4 && timer > 2) {
				playerObj.GetComponent<MorePPEffects.Ripple>().distortion = (1.5f * (timer-2.0f));
			} else if (timer < 6 && timer > 4) {
				playerObj.GetComponent<MorePPEffects.Headache>().strength = ((timer/2)-4.0f);
				playerObj.transform.eulerAngles = new Vector3 (0, playerObj.transform.rotation.y, 180 * (timer / 2) - 4.0f);
				fishCam.setActive (false);
			} else if (timer > 6 && timer < 7) {
				blackScreen.SetActive (true);
			} else if (timer > 7) {
				playerObj.SetActive (false);
				aboveCam.SetActive (true);
				abovePanel.SetActive (true);
				playerObj.transform.eulerAngles = playerObjRot;
				eventActive = false;
				blackScreen.SetActive (false);
				fishPoison2 = false;
				controller.setNormal ();
			}
		}
	}

	public void stageThreeUpdates() { //choke
		if (fishChoke) {
			timer += Time.deltaTime;
			fishCam.setActive (false);
			if(timer < 2) {
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionX = (0.5f * timer);
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionY = (0.5f * timer);
			} else if (timer < 4 && timer > 2) {
				playerObj.transform.Rotate (Vector3.up * timer * turnDir);
				if (playerObj.transform.eulerAngles.y < centerPoint - 10 || playerObj.transform.eulerAngles.y > centerPoint + 10)
					turnDir = turnDir * -1;
				playerObj.GetComponent<MorePPEffects.Ripple>().distortion = (1.5f * (timer-2.0f));
			} else if (timer < 6 && timer > 4) {
				playerObj.transform.Rotate (Vector3.up * timer * turnDir);
				if (playerObj.transform.eulerAngles.y < centerPoint - 10 || playerObj.transform.eulerAngles.y > centerPoint + 10)
					turnDir = turnDir * -1;
				playerObj.GetComponent<MorePPEffects.Headache>().strength = ((timer/2)-4.0f);
				playerObj.transform.eulerAngles = new Vector3 (0, playerObj.transform.rotation.y, 180 * (timer / 2) - 4.0f);
			} else if (timer > 6 && timer < 7) {
				blackScreen.SetActive (true);
			} else if (timer > 7) {
				fishCam.setActive (true);
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionX = 1;
				playerObj.GetComponent<MorePPEffects.Wiggle>().distortionY = 1;
				playerObj.GetComponent<MorePPEffects.Ripple>().distortion = 0;
				playerObj.GetComponent<MorePPEffects.Headache>().strength = 0;
				playerObj.transform.position = playerObjPos;
				playerObj.transform.eulerAngles = playerObjRot;
				fishChokeBubbles.transform.position = new Vector3 (0, -1000, 0);
				eventActive = false;
				blackScreen.SetActive (false);
				fishPoison = false;
				controller.changeWalls();
				controller.setNormal();
				controller.areaClicked (1);
			}
		}
	}
}
