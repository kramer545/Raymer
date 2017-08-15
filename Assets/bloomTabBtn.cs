using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloomTabBtn : MonoBehaviour {

    public GameObject[] imgs;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void openTab(int i)
    {
        for (int x = 0; x < imgs.Length; x++)
            imgs[x].SetActive(false);
        imgs[i].SetActive(true);
    }
}
