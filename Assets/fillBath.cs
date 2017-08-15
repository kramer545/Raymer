using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fillBath : MonoBehaviour {

    public GameObject Obj;
    public bool org = false;

	// Use this for initialization
	void Start () {
		if(org)
        {
            org = false;
            float xPos = transform.position.x;
            float yPos = transform.position.y;
            float zPos = transform.position.z;
            /*
            for (int x = 0; x < 100; x++)
            {
                GameObject clone = Instantiate(Obj);
                clone.transform.position = new Vector3(xPos + 0.5F * Mathf.Sin(Random.value * 360F), yPos, zPos + 0.4F * Mathf.Cos(Random.value * 360F));
            }
            */
            for (int x = 0; x < 7; x++)
            {
                for(int y=0;y<5*x;y++)
                {
                    GameObject clone = Instantiate(Obj);
                    clone.transform.position = new Vector3(xPos + (0.085F*(x)) * Mathf.Sin( (360F/(8*x)) * (y+1) + (Random.value -0.5F)), yPos, zPos + (0.085F * (x)) * Mathf.Cos( (360F / (8 * x)) * (y + 1) + (Random.value - 0.5F)));
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
