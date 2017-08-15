using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeBush : MonoBehaviour {

	public GameObject org;
	public bool active = false;
	public float radius = 0.8F;
	public int amount = 75;
	public float avgScale = 1.4F;
    public bool rotRev = false;


	// Use this for initialization
	void Start () {
		if (active) {
			active = false;
			float xPos = transform.position.x;
			float zPos = transform.position.z;
			for(int x = 0;x<amount;x++)
			{
				GameObject clone = Instantiate (org);
				clone.transform.position = new Vector3 (xPos + radius*Mathf.Sin(Random.value * 360F), 0F, zPos + radius*Mathf.Cos(Random.value * 360F));
                if(rotRev)
				    clone.transform.eulerAngles = new Vector3 (Random.Range (-15F, 15F), Random.Range (-15F, 15F), Random.Range (0F, 360F));
                else
                    clone.transform.eulerAngles = new Vector3(Random.Range(-15F, 15F), Random.Range(0F, 360F), Random.Range(-15F, 15F));
                float scale = Random.Range (avgScale-0.2F, avgScale+0.3F);
				clone.transform.localScale = new Vector3 (scale, scale, scale);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
