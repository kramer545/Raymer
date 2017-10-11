using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class rotateCam : MonoBehaviour { //parent class for cameras that rotate with touchscreen for looking around

	private Vector3 firstpoint;
	private Vector3 secondpoint;
	private float xAngle = 0.0f;
	private float yAngle = 0.0f;
	private float xAngTemp = 0.0f;
	private float yAngTemp = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        
    }

    // Update is called once per frame
    protected virtual void rotate() {
		if(Input.touchCount > 0) {
			//Touch began, save position
			if(Input.GetTouch(0).phase == TouchPhase.Began) {
				firstpoint = Input.GetTouch(0).position;
				xAngTemp = xAngle;
				yAngTemp = yAngle;
			}
			//Move finger by screen
			if(Input.GetTouch(0).phase==TouchPhase.Moved) {
				secondpoint = Input.GetTouch(0).position;
				//Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
				xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
				yAngle = yAngTemp - (secondpoint.y - firstpoint.y) *90.0f / Screen.height;
				//Rotate camera
				if (yAngle > 90)//prevents camera flipping upside down
					yAngle = 90;
				else if (yAngle < -90)
					yAngle = -90;
				transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
			}
		}
	}
}
