using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoFiller : MonoBehaviour {

	GameObject[] hexs;
	public BeeMenu controller;
	int[][] bloomFlowers = {
		new int[] {5}, //mar
		new int[] {5,9,12}, //apr
		new int[] {3,9,12,13}, //may
		new int[] {3,4,6,7,8,9,10,12}, //june
		new int[] {0,4,6,9,12}, //july
		new int[] {0,4,6,7,9,11}, //aug
		new int[] {4,7,9,11} //sept
	};
	int[] allFlowers = { 0, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

	int oldSelected;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void autoFill()
	{
		oldSelected = controller.selected;
		hexs = GameObject.FindGameObjectsWithTag ("autoFill"); //remove tiles have tag
		int x = 0;
		for(int y = 0;y<controller.bloomValues.Length;y++)
		{
			if((controller.bloomValues[y] < 2) && (x < hexs.Length))//if bloom month not filled, add random flower that fits that month
			{
				controller.selected = bloomFlowers [y][Random.Range (0, bloomFlowers[y].Length)];
				hexs [x].GetComponent<cellScript> ().replaceHex ();//replaces hex w/ current selected flower
				x++;
				y--;
			}
		}
		while (x < hexs.Length)
		{
			controller.selected = allFlowers [Random.Range (0, allFlowers.Length)];
			hexs [x].GetComponent<cellScript> ().replaceHex ();//replaces hex w/ current selected flower
			x++;
		}
		controller.selected = oldSelected;
	}
}
