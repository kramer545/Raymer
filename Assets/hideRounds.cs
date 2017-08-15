using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideRounds : MonoBehaviour {

    public GameObject[] rounds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void hide()
    {
        for(int x = 0;x<rounds.Length;x++)
        {
            rounds[x].SetActive(false);
        }
    }

    public void show()
    {
        for (int x = 0; x < rounds.Length; x++)
        {
            rounds[x].SetActive(true);
        }
    }
}
