using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeFlowMap : MonoBehaviour {

	public Material[] flowMaps;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setMap(int index)
	{
		GetComponent<Renderer> ().material = flowMaps [index];
	}
}
