using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEnableFlower : MonoBehaviour {

	private bool fadeIn = false;
	private bool fadeOut = false;
	private bool isactive = false;
	public Xeriscape_intro_script script1;
	public GameObject panel1;
	public string flowername;
	private float alpha = 0f;
    public bool moving = false;
    public hideRounds hider;
	// Use this for initialization
	void Start () {
		Color alph = GetComponent<SpriteRenderer>().color;
		alph.a = alpha;
		GetComponent<SpriteRenderer>().color = alph;
	}
	
	public void EnableImage () {
		fadeIn = true;
		fadeOut = false;
	}
	
	public void DisableImage () {
		fadeIn = false;
		fadeOut = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(fadeIn && alpha < 1.0f){
			Color alph = GetComponent<SpriteRenderer>().color;
			alpha += 0.04f;
			alph.a = alpha;
			GetComponent<SpriteRenderer>().color = alph;
		}
		if(fadeOut && alpha > 0.0f){
			Color alph = GetComponent<SpriteRenderer>().color;
			alpha -= 0.04f;
			alph.a = alpha;
			GetComponent<SpriteRenderer>().color = alph;
		}
		if(alpha >= 1.0f){
			isactive = true;
		}
		if(alpha <= 0.0f){
			isactive = false;
		}
        if (Input.touchCount == 1)//checks for touch input over image
        {
            if (Input.touches[0].phase == TouchPhase.Stationary)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == this.gameObject && isactive && !moving)
                    {
                        hider.hide();
                        script1.toggleInstructions(false);
                        Cursor.lockState = CursorLockMode.None;
                        script1.lookEnabled = false;
                        panel1.SetActive(true);
                        Color alph = GetComponent<SpriteRenderer>().color;
                        alph.a = 0.0f;
                        GetComponent<SpriteRenderer>().color = alph;
                        fadeIn = false;
                        fadeOut = false;
                    }
                }
            }
        }
    }
}
