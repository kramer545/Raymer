using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lakeMoveBtns : MonoBehaviour {
	
	public GameObject fishCam;
	public GameObject wallCam;
	public float moveSpeed;
	GameObject cam;
	int dir = -1;
	float yPos;

	// Use this for initialization
	void Start () {
		cam = fishCam;
		yPos = cam.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if (rotateCam.isActive && dir > -1) {
			move ();
		}
	}

	public void btnDown(int dir)
	{
		this.dir = dir;
	}

	public void btnUp()
	{
		dir = -1;
	}

	public void setFishCam() {
		cam = fishCam;
		yPos = cam.transform.position.y;
	}

	public void setWallCam() {
		cam = wallCam;
		yPos = cam.transform.position.y;
	}

	void move() {
		if(dir == 0)
		{
			cam.transform.Translate (Vector3.forward * moveSpeed);
		}
		else if (dir == 1)
		{
			cam.transform.Translate (Vector3.forward * moveSpeed * -1);
		}
		else if (dir == 2)
		{
			cam.transform.Translate (Vector3.right * moveSpeed * -1);
		}
		else if (dir == 3)
		{
			cam.transform.Translate (Vector3.right * moveSpeed);
		}
		cam.transform.position = new Vector3 (cam.transform.position.x, yPos, cam.transform.position.z);
	}
}
