using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionCatchMouse : MonoBehaviour {
	public GameObject solutionText;
	public GameObject[] OtherTexts;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnMouseDown(){
		foreach(GameObject text in OtherTexts){
			text.SetActive(false);
		}
		solutionText.SetActive(true);
	}
}
