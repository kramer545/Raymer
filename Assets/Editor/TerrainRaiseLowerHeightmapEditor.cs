//--------------------------------//
//  TerrainRaiseLowerHeightmap.cs //
//  Written by Jay Kay            //
//  2016/4/8                      //
//--------------------------------//


using UnityEditor;
using UnityEngine;


[ CustomEditor( typeof( TerrainRaiseLowerHeightmap ) ) ]
public class TerrainRaiseLowerHeightmapEditor : Editor 
{
	private GameObject obj;
	private TerrainRaiseLowerHeightmap objScript;

	void OnEnable()
	{
		obj = Selection.activeGameObject;
		objScript = obj.GetComponent< TerrainRaiseLowerHeightmap >();
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		// spacing between buttons
		EditorGUILayout.Space();

		// check if there is a terrain
		if ( objScript.terrain == null )
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.Label( "Assign a terrain to modify", GUILayout.MinWidth( 80 ), GUILayout.MaxWidth( 350 ) );
			EditorGUILayout.EndHorizontal();

			return;
		}

		// raise/lower button
		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button( "Raise/Lower Terrain", GUILayout.MinWidth( 80 ), GUILayout.MaxWidth( 350 ) ) )
		{
			objScript.ModifyTerrainHeightmap();
		}
		EditorGUILayout.EndHorizontal();

		// check if there is an undo array
		if ( objScript.undoHeights.Count > 0 )
		{
			// spacing between buttons
			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			if ( GUILayout.Button( "UNDO", GUILayout.MinWidth( 80 ), GUILayout.MaxWidth( 350 ) ) )
			{
				objScript.UndoModifyTerrainHeightmap();
			}
			EditorGUILayout.EndHorizontal();
		}
	}
}
