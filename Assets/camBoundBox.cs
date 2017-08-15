using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camBoundBox : MonoBehaviour {

	public float lowXLimit = 300;
	public float highXLimit = 337;
	public float lowZLimit = 160;
	public float highZLimit = 189;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < lowXLimit)
			transform.position = new Vector3 (lowXLimit, transform.position.y, transform.position.z);
		else if (transform.position.x > highXLimit)
			transform.position = new Vector3 (highXLimit, transform.position.y, transform.position.z);
		if (transform.position.z < lowZLimit)
			transform.position = new Vector3 (transform.position.x, transform.position.y, lowZLimit);
		else if (transform.position.z > highZLimit)
			transform.position = new Vector3 (transform.position.x, transform.position.y, highZLimit);
    }
}
