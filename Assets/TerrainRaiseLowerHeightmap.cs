//--------------------------------//
//  TerrainRaiseLowerHeightmap.cs //
//  Written by Jay Kay            //
//  2016/4/8                      //
//--------------------------------//


using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TerrainRaiseLowerHeightmap : MonoBehaviour 
{
	public Terrain terrain;

	public float changeHeightInUnits = 0f;

	[HideInInspector] public List< float[,] > undoHeights = new List< float[,] >();


	//    -------------------------------------------------------  Modify Heightmap Functions


	public void ModifyTerrainHeightmap() 
	{
		// check if terrain is assigned in inspector
		if ( !terrain )
		{
			Debug.LogError( gameObject.name + " has no terrain assigned in the inspector" );
			return;
		}

		// get reference variables
		TerrainData terrainData = terrain.terrainData;
		int heightmapWidth = terrainData.heightmapWidth;
		int heightmapHeight = terrainData.heightmapHeight;

		// copy current heights
		float[,] currentHeights = terrainData.GetHeights( 0, 0, heightmapWidth, heightmapHeight );
		undoHeights.Add( currentHeights );

		// make new height array
		float[,] newHeights = new float[ heightmapWidth, heightmapHeight ];
		float terrainHeight = terrainData.size.y;

		for ( int y = 0; y < heightmapWidth; y++ ) 
		{
			for ( int x = 0; x < heightmapHeight; x++ ) 
			{
				newHeights[ y, x ] = Mathf.Clamp01( currentHeights[ y, x ] + ( changeHeightInUnits / terrainHeight ) );
			}
		}

		// apply to terrain
		terrainData.SetHeights( 0, 0, newHeights );

		Debug.Log( "Raise/Lower Heights completed" );
	}


	//    -------------------------------------------------------  Undo Functions


	public void UndoModifyTerrainHeightmap() 
	{
		// check if terrain is assigned in inspector
		if ( !terrain )
		{
			Debug.LogError( gameObject.name + " has no terrain assigned in the inspector" );
			return;
		}

		// get last heights
		float[,] newHeights = undoHeights[ undoHeights.Count - 1 ];

		// apply to terrain
		terrain.terrainData.SetHeights( 0, 0, newHeights );

		// remove from list
		undoHeights.RemoveAt( undoHeights.Count - 1 );

		Debug.Log( "Undo Heights completed" );
	}
}
