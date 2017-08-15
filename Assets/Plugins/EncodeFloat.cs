using UnityEngine;
using System.Collections;
using System.IO;

// This class is designed to take 32 bit floating point data and get it into a render texture.
// As there is no way in Unity to load floating point data straight into a render texture the data for each
// channel must be encoded into a ARGB32 format texture and then decoded via a shader into the render texture.
//
// This does work but its still a bit experimental so dont fiddle with it unless you know what your doing.
// At the moment there are some conditions that must be meet for this to work
//
// 1 - The data must be 32 bit floating point
//
// 2 - The encode/decode step only works on data in the range 0 - 1 but the function will find the highest number and normalize the 
// the data if its over 1 and then un-normalize it in the shader. This way you can have numbers greater than 1. The function will also find 
// the lowest number and if its below 0 it will add this value to all the data so the lowest number is 0. This way you can have numbers lower than 0.

static public class EncodeFloat
{
	static Material m_decodeToFloat2D;
	
	static public void WriteIntoRenderTexture2D(RenderTexture tex, int channels, float[] data)
	{
		if(tex == null)
		{
			Debug.Log("EncodeFloat::WriteIntoRenderTexture2D - RenderTexture is null");
			return;
		}
		
		if(data == null)
		{
			Debug.Log("EncodeFloat::WriteIntoRenderTexture2D - Data is null");
			return;
		}
		
		if(channels < 1 || channels > 4)
		{
			Debug.Log("EncodeFloat::WriteIntoRenderTexture2D - Channels must be 1, 2, 3, or 4");
			return;
		}
		
		int w = tex.width;
		int h = tex.height;
		int size = w*h*channels;

		Color[] map = new Color[size];
		
		float max = 1.0f;
		float min = 0.0f;
		LoadData(data, map, size, ref max, ref min);
		
		Color[] encodedR = new Color[w*h];
		Color[] encodedG = new Color[w*h];
		Color[] encodedB = new Color[w*h];
		Color[] encodedA = new Color[w*h];
		
		for(int x = 0; x < w; x++)
		{
			for(int y = 0; y < h; y++)
			{
				encodedR[x+y*w] = new Color(0,0,0,0);
				encodedG[x+y*w] = new Color(0,0,0,0);
				encodedB[x+y*w] = new Color(0,0,0,0);
				encodedA[x+y*w] = new Color(0,0,0,0);
				
				if(channels > 0) encodedR[x+y*w] = map[(x+y*w)*channels+0];
				if(channels > 1) encodedG[x+y*w] = map[(x+y*w)*channels+1];
				if(channels > 2) encodedB[x+y*w] = map[(x+y*w)*channels+2];
				if(channels > 3) encodedA[x+y*w] = map[(x+y*w)*channels+3];
			}
		}
		
		Texture2D mapR = new Texture2D(w, h,TextureFormat.ARGB32, false, true);
		mapR.filterMode = FilterMode.Point;
		mapR.wrapMode = TextureWrapMode.Clamp;
		mapR.SetPixels(encodedR);
		mapR.Apply();
		
		Texture2D mapG = new Texture2D(w, h,TextureFormat.ARGB32, false, true);
		mapG.filterMode = FilterMode.Point;
		mapG.wrapMode = TextureWrapMode.Clamp;
		mapG.SetPixels(encodedG);
		mapG.Apply();
		
		Texture2D mapB = new Texture2D(w, h,TextureFormat.ARGB32, false, true);
		mapB.filterMode = FilterMode.Point;
		mapB.wrapMode = TextureWrapMode.Clamp;
		mapB.SetPixels(encodedB);
		mapB.Apply();
		
		Texture2D mapA = new Texture2D(w, h,TextureFormat.ARGB32, false, true);
		mapA.filterMode = FilterMode.Point;
		mapA.wrapMode = TextureWrapMode.Clamp;
		mapA.SetPixels(encodedA);
		mapA.Apply();
		
		if(m_decodeToFloat2D == null)
		{
			Shader shader = Shader.Find("EncodeFloat/DecodeToFloat2D");
			
			if(shader == null)
			{
				Debug.Log("EncodeFloat::WriteIntoRenderTexture2D - could not find shader EncodeToFloat/DecodeToFloat2D");
				return;
			}
			
			m_decodeToFloat2D = new Material(shader);
		}
		
		m_decodeToFloat2D.SetFloat("_Max", max);
		m_decodeToFloat2D.SetFloat("_Min", min);
		m_decodeToFloat2D.SetTexture("_TexR", mapR);
		m_decodeToFloat2D.SetTexture("_TexG", mapG);
		m_decodeToFloat2D.SetTexture("_TexB", mapB);
		m_decodeToFloat2D.SetTexture("_TexA", mapA);
		Graphics.Blit(null, tex, m_decodeToFloat2D);
		
	}
	
	static float[] EncodeFloatRGBA( float val )
    {
		//Thanks to karljj1 for this function
        float[] kEncodeMul = new float[]{ 1.0f, 255.0f, 65025.0f, 160581375.0f };
        float kEncodeBit = 1.0f / 255.0f;            
        for( int i = 0; i < kEncodeMul.Length; ++i )
        {
            kEncodeMul[i] *= val;
            // Frac
            kEncodeMul[i] = ( float )( kEncodeMul[i] - System.Math.Truncate( kEncodeMul[i] ) );
        }
     
        // enc -= enc.yzww * kEncodeBit;
        float[] yzww = new float[] { kEncodeMul[1], kEncodeMul[2], kEncodeMul[3], kEncodeMul[3] };
        for( int i = 0; i < kEncodeMul.Length; ++i )
        {
            kEncodeMul[i] -= yzww[i] * kEncodeBit;
        }
     
        return kEncodeMul;
    }
	
	static void LoadData(float[] data, Color[] map, int size, ref float max, ref float min) 
	{	
		
		for(int x = 0 ; x < size; x++) 
		{
			//Find the min and max range of data
			if(data[x] > max) max = data[x];
			if(data[x] < min) min = data[x];
		};
		
		min = Mathf.Abs(min);
		max += min;
		
		for(int x = 0 ; x < size; x++) 
		{
			float normalizedData = (data[x] + min) / max;
			
			//I was expecting to convert the float to 4 bytes using System.BitConverter.GetBytes() and store each in a color32 object
			// but this did not work for some reason. Instead you need to split the float into four floats 
			// using the EcodeFloatRGBA function  and store in acolor object. I suspect the decode function in the 
			//shader with only work on data that has been encode using this function.
			
			float[] farray = EncodeFloatRGBA(normalizedData);
			
			map[x] = new Color(farray[0], farray[1], farray[2], farray[3]);
		};
	}

}
