using UnityEngine;
using System.Collections;

public class Seagull : MonoBehaviour 
{
	private const string ANIMATION_GLIDING = "seagullGliding";
	private const string ANIMATION_FLYING = "seagullFlying";
	private const float ANIMATION_TRANSITION_DURATION = 1.0f;
	
	private const float RAD_TO_DEG = 180.0f / Mathf.PI;
	
	private float angle;
	private float speed;
	private float radius;
	private float minHeight;
	private float maxHeight;
	private float heightDif;
	private float height;
	private float decayVel;
	private float riseVel;
	
	private bool gliding = true;
	
	private Transform normalPoly;
	private Transform lowPoly;
	
	public void Setup (float initialAngle, float initialSpeed, float radius, float heightVariation)
	{
		angle = initialAngle;
		speed = initialSpeed;
		
		this.radius = radius;
		
		minHeight = - heightVariation / 4.0f - UnityEngine.Random.value * heightVariation / 4.0f;
		maxHeight =   heightVariation / 4.0f + UnityEngine.Random.value * heightVariation / 4.0f;
		heightDif = maxHeight - minHeight;
		height = minHeight + heightDif * Random.value - heightDif * Random.value / 2.0f;
		decayVel = heightDif / 20.0f * (0.9f + Random.value * 0.1f);
		riseVel = decayVel * 5.0f;
		
		float animationRandomPosition = Random.value;
		
		normalPoly = transform.Find ("Seagull");
		lowPoly = transform.Find ("SeagullLowPoly");
		
		if (normalPoly != null)
		{
			normalPoly.GetComponent<Animation>()[ANIMATION_GLIDING].normalizedTime = animationRandomPosition;
			lowPoly.GetComponent<Animation>()[ANIMATION_GLIDING].normalizedTime = animationRandomPosition;
			normalPoly.GetComponent<Animation>()[ANIMATION_FLYING].normalizedTime = animationRandomPosition;
			lowPoly.GetComponent<Animation>()[ANIMATION_FLYING].normalizedTime = animationRandomPosition;
			normalPoly.GetComponent<Animation>()[ANIMATION_FLYING].speed = 2.0f;
			lowPoly.GetComponent<Animation>()[ANIMATION_FLYING].speed = 2.0f;
		}
		else
		{
			GetComponent<Animation>()[ANIMATION_GLIDING].normalizedTime = animationRandomPosition;
			GetComponent<Animation>()[ANIMATION_FLYING].normalizedTime = animationRandomPosition;
			GetComponent<Animation>()[ANIMATION_FLYING].speed = 2.0f;
		}

		Update ();
	}
	
	private void ChangeAnimation (string newAnimation)
	{
		if (normalPoly != null)
		{
			normalPoly.GetComponent<Animation>().CrossFade (newAnimation, ANIMATION_TRANSITION_DURATION, PlayMode.StopAll);
			lowPoly.GetComponent<Animation>().CrossFade (newAnimation, ANIMATION_TRANSITION_DURATION, PlayMode.StopAll);
		}
		else
			GetComponent<Animation>().CrossFade (newAnimation, ANIMATION_TRANSITION_DURATION, PlayMode.StopAll);
	}
	
	void Start () 
	{
	}
	
	void Update () 
	{
		angle += speed * Time.deltaTime / radius;
		
		float posX = radius * Mathf.Sin (angle);
		float posZ = radius * Mathf.Cos (angle);
		
		if (gliding)
		{
			height -= Time.deltaTime * decayVel;
			
			if (height < minHeight)
			{
				height = minHeight;
				gliding = false;
				ChangeAnimation (ANIMATION_FLYING);
			}
		}
		else
		{
			height += Time.deltaTime * riseVel;
			if (height > maxHeight)
			{
				height = maxHeight;
				gliding = true;
				ChangeAnimation (ANIMATION_GLIDING);
			}
		}
		
		transform.localPosition = new Vector3 (posX, height, posZ);
		transform.eulerAngles = new Vector3 (0.0f, Mathf.Atan2 (posZ, -posX) * RAD_TO_DEG, 0.0f);
	}
}
