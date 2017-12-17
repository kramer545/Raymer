using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapController : MonoBehaviour {

	public GameObject panelOne;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//base.rotate();
	}

	public void hidePanelOne()
	{
		panelOne.SetActive (false);
	}
}
