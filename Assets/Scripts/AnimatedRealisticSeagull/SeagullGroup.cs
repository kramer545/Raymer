using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SeagullGroup : MonoBehaviour 
{
	[SerializeField]
	private int _instances = 10;
	
	[SerializeField]
	private float _heightVariation = 20.0f;
	
	[SerializeField]
	private float _radius = 200.0f;
	
	[SerializeField]
	private float _speed = 20.0f;
	
	[SerializeField]
	private bool _forceLowPoly = false;
	
	public int instances
	{
		get
		{ 
			return _instances;  
		}
		set
		{ 
			if (value != _instances)
			{
				_instances = value; 
			}
		}
	}
	
	public float heightVariation
	{
		get
		{ 
			return _heightVariation;  
		}
		set
		{ 
			if (value != _heightVariation)
			{
				_heightVariation = value; 
			}
		}
	}
	
	public float radius
	{
		get
		{ 
			return _radius;  
		}
		set
		{ 
			if (value != _radius)
			{
				_radius = value; 
			}
		}
	}
	
	public float speed
	{
		get 
		{
			return _speed;
		}
		set
		{
			if (value != _speed)
			{
				_speed = value;
			}
		}
	}
	
	public bool forceLowPoly
	{
		get
		{ 
			return _forceLowPoly;  
		}
		set
		{ 
			if (value != _forceLowPoly)
			{
				_forceLowPoly = value; 
			}
		}
	}
	
	private const string BIRD_NAME = "Seagull";
	private const string PREFAB_BASE_PATH = "Prefabs/AnimatedRealisticSeagull/";
	private const float PI_DIV_2 = Mathf.PI / 2.0f;
	private const float PI_MUL_2 = Mathf.PI * 2.0f;
	
	private List<GameObject> birds;
	
	void Start () 
	{
		float radius_div_2 = _radius / 2.0f;
		
		birds = new List<GameObject> ();
		
		float stepAngle = PI_MUL_2 / _instances;
		
		for (int i = 0; i < _instances; i++)
		{
			GameObject bird = (GameObject) Instantiate (Resources.Load (PREFAB_BASE_PATH + (_forceLowPoly ? BIRD_NAME + "LowPoly" : BIRD_NAME + "LOD")));
			bird.name = BIRD_NAME + i;
			bird.transform.parent = transform;
			
			float angle = stepAngle * i + UnityEngine.Random.value * PI_DIV_2 - UnityEngine.Random.value * PI_DIV_2;
			float speed = _speed + UnityEngine.Random.value * _speed / 2.0f;
			
			bird.GetComponent<Seagull> ().Setup (angle, speed, radius_div_2 + UnityEngine.Random.value * radius_div_2, _heightVariation);
			
			birds.Add (bird);
		}
	}
	
	void Update () 
	{
	}
	
	public void Destroy ()
	{
		if (birds != null)
		{
			int l = birds.Count;
			
			for (int i = 0; i < l; i++)
			{
				GameObject.Destroy (birds[i]);
			}
			
			birds = null;
		}
	}
}
