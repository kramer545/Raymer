using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_tax : MonoBehaviour {

	// Use this for initialization
	public Button yourButton;
	public Image image;
	public bool test = false;
	Color myColor = new Color();
	Color myColor2 = new Color();

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
		 ColorUtility.TryParseHtmlString("#FFDBDBFF", out myColor);
		 ColorUtility.TryParseHtmlString("#DBFFDDFF", out myColor2);
	}

	void TaskOnClick(){
		test = !test;
	}
	
	void OnGUI(){
		if(test){
			image.color = myColor2;
		}
		else
		{
			image.color = myColor;
		}
	}
}
