using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour {

    public float moveModifier = 1;
    GameObject cam;
    int dir = -1;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (dir > -1)
            move();
    }

    void move()//move camera in direction given by int, 0 = forward, 1 = back, 2 = left, 3 = right
    {
        if (dir == 0)
        {
            cam.transform.position += moveModifier * cam.transform.up;
        }
        else if (dir == 1)
        {
            cam.transform.position -= moveModifier * cam.transform.up;
        }
        else if (dir == 2)
        {
            cam.transform.position -= moveModifier * cam.transform.right;
        }
        else if (dir == 3)
        {
            cam.transform.position += moveModifier * cam.transform.right;
        }
    }

    public void btnDown(int dir)
    {
        this.dir = dir;
    }

    public void btnUp()
    {
        dir = -1;
    }
}
