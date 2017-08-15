using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCamBtn : MonoBehaviour {

    public GameObject cam;
    Vector3 startPos;
    Quaternion startRot;
    
	void Start () {//record start cam pos/rot
        startPos = cam.transform.position;
        startRot = cam.transform.rotation;
	}

    public void click()//called on btn click
    {
        cam.transform.position = startPos;
        cam.transform.rotation = startRot;
    }
}
