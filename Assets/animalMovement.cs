using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalMovement : MonoBehaviour {

    public Vector3[] waypoint;
    public float speed;
	public bool inverted = false;//if model moves backwords, enable to move correctly
	public float breakTime = 0;//if breaktime > 0, dont start animation right away when x = 0;
    int x = 0;
	float currTime = 0;
    public bool animSwitch = false;
	public float waypointDistance = 1F;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currTime < breakTime)
        {
            currTime += Time.deltaTime;
        }
		else
        {
            if (animSwitch)
                GetComponent<Animator>().SetBool("animSwitch", false);
            float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, waypoint[x], step);
			if(Vector3.Distance(transform.position,waypoint[x]) < waypointDistance)
			{
				x++;
                if (x == waypoint.Length)
				{
					x = 0;
				}
				if(x == 1)
				{
					currTime = 0;
                    if(animSwitch)
                        GetComponent<Animator>().SetBool("animSwitch", true);
                }
                if (inverted)
					transform.LookAt(waypoint[x]);
				else
					transform.LookAt(2 * transform.position - waypoint[x]);
			}

		}
    }
}
