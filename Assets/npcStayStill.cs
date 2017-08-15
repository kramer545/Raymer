using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcStayStill : MonoBehaviour {

	public float yRot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3 (0, yRot, 0);
	}
}
