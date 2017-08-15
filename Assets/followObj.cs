using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followObj : MonoBehaviour {

	public GameObject target;
	Transform rot;

	// Use this for initialization
	void Start () {
		rot = transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = rot.rotation;
		transform.position = new Vector3(target.transform.position.x,target.transform.position.y + 10,target.transform.position.z);
	}
}
