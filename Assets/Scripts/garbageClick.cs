using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class garbageClick : MonoBehaviour {
	
	public IslandController island;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		Debug.Log ("hit");
		island.removeItem ();
		this.gameObject.SetActive (false);
	}
}
