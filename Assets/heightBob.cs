using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heightBob : MonoBehaviour {

	public float bobAmnt = 0.007F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y + bobAmnt*Mathf.Sin (Time.time), transform.position.z);
	}
}
