using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class screenBtn : MonoBehaviour {

	public GameObject wall;
	bool movingWall = false;
	float startTime;
	float length;
	public float speed = 1.0F;
	public Vector3 startPos;
	public Vector3 endPos;
	float alpha = 0;
	public GameObject title;
    public GameObject cam;
    public float time2 = 0.0F;
	public float wallDisappearTime = 2;
	public float alphaSpeed = 100;

	// Use this for initialization
	void Start () {
		length = Vector3.Distance (startPos, endPos);
	}
	
	// Update is called once per frame
	void Update () {
		if(movingWall)
		{
			float distCovered = (Time.time-startTime)*speed;
			float fracJourney = distCovered / length;
			wall.transform.position = Vector3.Lerp (startPos, endPos, fracJourney);
			if((Time.time-startTime) > wallDisappearTime)
			{
                /*
				alpha += (55 * Time.deltaTime)/255;
				GetComponent<Image> ().color = new Color (255,255,255, alpha);
				if(alpha > 1.5)
				{
					SceneManager.LoadScene ("cake");
				}
				else if (alpha > 0.3)
				{
					title.SetActive (true);
				}
                */
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - 0.2F,cam.transform.position.z);
                if (cam.transform.position.y < 0 )
                {
                    title.SetActive(true);
                    alpha += (alphaSpeed * Time.deltaTime) / 255;
					title.GetComponent<Text>().color = new Color(26.0F/255.0F, 143.0F/255.0F, 0, alpha);
                    if (alpha > 1.5)
                    {
                        SceneManager.LoadScene("MainMenu");
                    }
                }
            }
            if(time2 > 0)
            {
                if((Time.time - time2) > 3)
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
		}

	}

	public void end()
	{
		if(!movingWall)
		{
			movingWall = true;
			startTime = Time.time;
		}
	}
}
