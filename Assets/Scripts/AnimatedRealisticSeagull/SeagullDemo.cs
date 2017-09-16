using UnityEngine;
using System.Collections;

public class SeagullDemo : MonoBehaviour 
{
	private const string ANIMATION_FLYING = "seagullFlying";
	private const string ANIMATION_GLIDING = "seagullGliding";

	public GameObject seagull;
	public GameObject seagullLowPoly;
	private GameObject currentModel;

	private string currentAnimation = ANIMATION_GLIDING;
	private float animationSpeed = 1.0f;

	void Start () 
	{
		currentModel = seagull;
	}
	
	void Update () 
	{
	}

	void OnGUI ()
	{
		if (GUI.Button (new Rect(10, 10, 100, 30), "Flying"))
			ChangeAnimation (ANIMATION_FLYING);
		else if (GUI.Button (new Rect(10, 50, 100, 30), "Gliding"))
			ChangeAnimation (ANIMATION_GLIDING);

		GUI.Label (new Rect (10, 90, 120, 20), "Animation Speed:");

		float oldAnimationSpeed = animationSpeed;
		animationSpeed = GUI.HorizontalSlider (new Rect(10, 110, 100, 30), animationSpeed, 0.1f, 3.0f);

		if (animationSpeed != oldAnimationSpeed)
			ChangeAnimation (currentAnimation, 0.0f);

		if (GUI.Button (new Rect(10, 170, 100, 30), "Normal"))
			ChangeModel (seagull);
		else if (GUI.Button (new Rect(10, 210, 100, 30), "Low Poly"))
			ChangeModel (seagullLowPoly);
	}

	private void ChangeAnimation (string newAnimation, float duration = 1.0f)
	{
		currentModel.GetComponent<Animation>().CrossFade (newAnimation, duration, PlayMode.StopAll);
		currentModel.GetComponent<Animation>()[newAnimation].speed = animationSpeed;
		currentAnimation = newAnimation;
	}

	private void ChangeModel (GameObject newModel)
	{
		currentModel = newModel;

		seagull.SetActive (false);
		seagullLowPoly.SetActive (false);
		currentModel.SetActive (true);

		ChangeAnimation (currentAnimation, 0.0f);
	}
}
