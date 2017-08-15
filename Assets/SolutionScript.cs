using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionScript : MonoBehaviour {

	public GameObject BighornPanel;
	public GameObject HeronPanel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform child in transform)
		{
			string name = child.gameObject.name;
			bool state = false;// = child.gameObject.GetComponent<SolutionCatchMouse>().isOn;
			if(name == "Bighorn_Panel"){
				if(state){
					BighornPanel.SetActive(true);
					HeronPanel.SetActive(false);
				}else{
					BighornPanel.SetActive(false);
				}
			}
			if(name == "Heron_Panel"){
				if(state){
					HeronPanel.SetActive(true);
					BighornPanel.SetActive(false);
				}else{
					HeronPanel.SetActive(false);
				}
			}
		}
	}
}
