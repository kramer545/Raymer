// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Flowhiz/Water Reflection + Refraction(Optimazed)" 
{
	Properties 
	{
		BaseColor("Base Color", COLOR) = (.17 , .46 , .44 , 0)
		RefractionColor("Refraction Color", COLOR) = (.1 , .2 , .2 , 0)
		[NoScaleOffset] BumpMap1("Waves Bump 1", 2D) = "" { }
		[NoScaleOffset] BumpMap2 ("Waves Bump 2", 2D) = "" { }
		ReflectionMap("Reflection", 2D) = "white" {}
		RefractionMap ("Refraction", 2D) = "white" {}
		[NoScaleOffset] FlowMap ("Flow map", 2D) = "gray" {}
		FlowMapSpeed ("Flow Map Speed", Float) = 0.1
		CycleLength("Cycle Length", Float) = 0.15
		WaveScale1("Wave scale 1", Float) = .07
		WaveScale2("Wave scale 2", Float) = .07
		WaveStrange("Wave strange", Range(0, 2)) = 1
		DistCorection("Distance to  reflection", Float) = 4
		SunParams("Sun Factor - x, Sun Power - y", Vector) = (1, 1, 0, 0)
		SunColor("Sun Color", Color) = (1, 1, 1, 1)
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
					half3 viewDir : TEXCOORD2;
					half4 reflUV : TEXCOORD3;
					half2 flowUV : TEXCOORD4;
					half2 refrUV : TEXCOORD5;
				};

				
				uniform sampler2D FlowMap;
				uniform sampler2D BumpMap1;
				uniform sampler2D BumpMap2;
				uniform sampler2D ReflectionMap;
				uniform sampler2D RefractionMap;

				uniform float4 FlowMap_ST;
				uniform half CycleLength;
				uniform half FlowMapSpeed;

				uniform fixed4 BaseColor;
				uniform fixed4 RefractionColor;
				uniform half WaveScale1;
				uniform half WaveScale2;
				uniform half WaveStrange;
				uniform half DistCorection;
				uniform half4 ReflectionMap_ST;
				uniform half4 RefractionMap_ST;
				uniform half2 SunParams;
				uniform fixed4 SunColor;

				v2f vert (appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);					
					o.viewDir = UNITY_MATRIX_IT_MV[2].xyz;
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
					o.reflUV.xy = (float2(reflVertex.x, reflVertex.y * scale) + reflVertex.w) * 0.5 * ReflectionMap_ST.xy;
					o.reflUV.zw = float2(reflVertex.z, reflVertex.w * 5);
					o.flowUV = TRANSFORM_TEX(v.uv, FlowMap);
					o.refrUV = TRANSFORM_TEX(v.uv, RefractionMap);

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
					half3 normalT1 = UnpackNormal(tex2D(BumpMap1, i.uv * WaveScale2 + flowmap.rg * flowMapOffset1));

					half flowLerp = abs(halfCycle - flowMapOffset0) / halfCycle;
					half3 normalT = lerp(normalT0, normalT1, flowLerp);
					half3 offset = normalT * WaveStrange;
					half2 reflUV = i.reflUV.xy / i.reflUV.w + half2(offset.r, offset.g);
					fixed3 refl = tex2D(ReflectionMap, reflUV).rgb;
					fixed3 refr = tex2D(RefractionMap, i.refrUV + half2(offset.r, offset.g)).rgb * RefractionColor.rgb;
					
					fixed3 up = fixed3(0, 1, 0);
					fixed3 viewDir = normalize(i.viewDir);
					fixed3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

					fixed angle = saturate(dot(viewDir, up));
					fixed f = 0.2 + 0.8 * pow(1.0h - angle, 2);
					f = min(1.0h, 1 - saturate(i.pos.z + DistCorection) + f);

					half3 R = -viewDir - 2 * dot(-viewDir, normalT) * normalT;
					half4 sun = SunParams.x * pow(saturate(dot(R, lightDir)), SunParams.y) * SunColor;

					fixed4 col = BaseColor * max(dot(lightDir, offset), 0.75h);
					col.rgb *= lerp(refr, refl, f);
					col.rgb += sun.rgb;
					col.a = flowmap.b;
				
					return col;
				}
			ENDCG
			}
		}
	}
}
