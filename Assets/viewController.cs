using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class viewController : MonoBehaviour {

	public GameObject[] bees;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void view()
	{
		GameObject[] hexs = GameObject.FindGameObjectsWithTag ("hex");//remove green hexs
		for(int x = 0;x<hexs.Length;x++)
		{
			hexs [x].SetActive (false);
		}

		hexs = GameObject.FindGameObjectsWithTag ("groundTile");//slightly increase ground tile size to fill gaps
		for(int x = 0;x<hexs.Length;x++)
		{
			hexs [x].transform.localScale = new Vector3 (0.2F, 0.2F, 0.2F);
		}

		for(int x = 0;x<bees.Length;x++)//activate the BEES!!!
		{
			bees [x].SetActive (true);
		}
	}

}
