using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementBtn : MonoBehaviour {

	public float moveModifier = 1;
    public OnEnableFlower[] rounds;
	GameObject cam;
	float yPos;
    int dir = -1;

	// Use this for initialization
	void Start () {
		cam = Camera.main.gameObject;
		yPos = cam.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        if (dir > -1)
            move();
	}

	void move()//move camera in direction given by int, 0 = forward, 1 = back, 2 = left, 3 = right
	{
		if(dir == 0)
		{
			cam.transform.position += moveModifier*cam.transform.forward;
		}
		else if (dir == 1)
		{
			cam.transform.position-=moveModifier*cam.transform.forward;
		}
		else if (dir == 2)
		{
			cam.transform.position-=moveModifier*cam.transform.right;
		}
		else if (dir == 3)
		{
			cam.transform.position+=moveModifier*cam.transform.right;
		}
		cam.transform.position = new Vector3 (cam.transform.position.x, yPos, cam.transform.position.z);
	}

    public void btnDown(int dir)
    {
        this.dir = dir;
        if (rounds != null)
        {
          for (int x = 0; x < rounds.Length; x++)
              rounds[x].moving = true;
        }
    }

    public void btnUp()
    {
        dir = -1;
        if (rounds != null)
        {
            for (int x = 0; x < rounds.Length; x++)
                rounds[x].moving = false;
        }
    }
}
