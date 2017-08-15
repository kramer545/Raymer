// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Flowhiz/Water Reflection (Optimazed)" 
{
	Properties 
	{
		BaseColor("Color", COLOR) = (.172 , .463 , .435 , 0)
		[NoScaleOffset] BumpMap1("Waves Bump 1", 2D) = "" { }
		[NoScaleOffset] BumpMap2 ("Waves Bump 2", 2D) = "" { }
		ReflactionMap ("Reflection", 2D) = "white" {}
		[NoScaleOffset] FlowMap ("Flow map", 2D) = "gray" {}
		FlowMapSpeed ("Flow Map Speed", Float) = 0.1
		CycleLength("Cycle Length", Float) = 0.15
		WaveScale1("Wave scale 1", Float) = .07
		WaveScale2("Wave scale 2", Float) = .07
		WaveStrange("Wave strange", Range(0, 2)) = 1
	}

	Category 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		SubShader 
		{
			Pass 
			{
				Blend SrcAlpha OneMinusSrcAlpha
				ZWrite off

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
								
				struct appdata 
				{
					float4 vertex : POSITION;
					float2 uv	  : TEXCOORD0;	
				};

				struct v2f 
				{
					float4 pos : SV_POSITION;
					float2 uv	 : TEXCOORD0;
					float3 viewDir : TEXCOORD2;
					float4 reflUV : TEXCOORD3;
					float2 flowUV : TEXCOORD4;
				};

				
				uniform sampler2D FlowMap;
				uniform sampler2D BumpMap1;
				uniform sampler2D BumpMap2;
				uniform sampler2D ReflactionMap;

				uniform float4 FlowMap_ST;
				uniform half CycleLength;
				uniform half FlowMapSpeed;

				uniform fixed4 BaseColor;
				uniform half WaveScale1;
				uniform half WaveScale2;
				uniform half WaveStrange;
				uniform half4 ReflactionMap_ST;

				v2f vert (appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);					
					o.viewDir.xzy = normalize( WorldSpaceViewDir(v.vertex));
					o.uv = v.uv;

					#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
					#else
					float scale = 1.0;
					#endif
					
					float4x4 viewProj = {
						-1, 0, 0, _WorldSpaceCameraPos.x,
						0, 0, -1, _WorldSpaceCameraPos.z,
						0, 1, 0, _WorldSpaceCameraPos.y,
						0, 0, 0, 1
					};

					half4 reflVertex = mul(mul(viewProj, unity_ObjectToWorld), v.vertex);
					o.reflUV.xy = (float2(reflVertex.x, reflVertex.y * scale) + reflVertex.w) * 0.5 * ReflactionMap_ST.xy;
					o.reflUV.zw = float2(reflVertex.z, reflVertex.w * 5);
					o.flowUV = TRANSFORM_TEX(v.uv, FlowMap);

					return o;
				}


				half4 frag( v2f i ) : COLOR
				{
					half time = _Time.x;
					time = time - floor(time);
					
					fixed3 flowmap = tex2D(FlowMap, i.flowUV).rgb;
					flowmap.g = 1- flowmap.g;
					flowmap.rg = flowmap.rg * 2.0f - 1.0f;

					half halfCycle = CycleLength * 0.5h;
					half flowMapOffset0 = fmod((halfCycle + time * FlowMapSpeed), CycleLength);
					half flowMapOffset1 = fmod(time * FlowMapSpeed, CycleLength);

					half3 normalT0 = UnpackNormal(tex2D(BumpMap1, i.uv * WaveScale1 + flowmap.rg * flowMapOffset0));
					half3 normalT1 = UnpackNormal(tex2D(BumpMap2, i.uv * WaveScale2 + flowmap.rg * flowMapOffset1));

					half flowLerp = abs(halfCycle - flowMapOffset0) / halfCycle;
					half3 offset = lerp(normalT0, normalT1, flowLerp) * WaveStrange;

					half2 reflUV = i.reflUV.xy / i.reflUV.w + half2(offset.r, offset.g);
					fixed3 refl = tex2D(ReflactionMap, reflUV).rgb;

					fixed4 col = BaseColor * max(dot(normalize(_WorldSpaceLightPos0.xyz), offset), 0.75h);
					col.rgb *= refl;
					col.a = flowmap.b;
				
					return col;
				}
			ENDCG
			}
		}
	}
}
