using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(SeagullGroup))]
public class SeagullGroupCustomInspector : Editor 
{
	private const int MAX_INSTANCES = 100;
	
	public override void OnInspectorGUI () 
	{ 
		SeagullGroup prefab = target as SeagullGroup;
		
		prefab.instances = (int)EditorGUILayout.Slider ("Instances", prefab.instances, 0, MAX_INSTANCES);
		prefab.heightVariation =  EditorGUILayout.FloatField ("Height variation", prefab.heightVariation);
		prefab.radius =  EditorGUILayout.FloatField ("Radius", prefab.radius);
		prefab.speed =  EditorGUILayout.FloatField ("Speed", prefab.speed);
		prefab.forceLowPoly = EditorGUILayout.Toggle ("Force low poly", prefab.forceLowPoly);
	}
}
