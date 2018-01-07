using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objFinder : MonoBehaviour {

	public Camera cam;
	public GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 targetScreenPoint = cam.WorldToScreenPoint (target.transform.position);
		this.transform.position = targetScreenPoint;
	}
}
