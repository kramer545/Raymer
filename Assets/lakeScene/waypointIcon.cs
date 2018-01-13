using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypointIcon : MonoBehaviour {

	public float speed;
	public float lowerLimit;
	public float upperLimit;
	bool growing;
	float size;

	// Use this for initialization
	void Start () {
		size = this.gameObject.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		if(growing) {
			size += Time.deltaTime * speed;
		} else {
			size -= Time.deltaTime * speed;
		}
		if(size > upperLimit || size < lowerLimit) {
			growing = !growing;
		}
		this.gameObject.transform.localScale = new Vector3 (size, size, size);
	}
}
