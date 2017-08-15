Shader "Flowhiz/Editor/FlowBrush"
{
	Properties
	{
		_MainTex("Editor Texture", 2D) = "white" { }
		_BrushSize("BrushSize", Float) = 0.1
		_BrushStrange("Brush Strange", Range(0, 1)) = 1
		_BrushPosition("Brush Position", Vector) = (0,0,0,0)
		_BrushColor("Brush Color", Color) = (0,0,1,1)
		[NoScaleOffset] ArrowTex("Arrow Texture", 2D) = "black" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex	: POSITION;
				float2 uv		: TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex	: SV_POSITION;
				float2 uv		: TEXCOORD0;
			};

			uniform float _BrushSize;
			uniform float _BrushStrange;
			uniform float3 _BrushPosition;
			uniform float4 _BrushColor;
			uniform sampler2D _MainTex;
			uniform fixed _ShowBlue;
			uniform fixed _ShowRedGreen;
			uniform sampler2D ArrowTex;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed SmoothStep(fixed edge0, fixed edge1, fixed x)
			{
				fixed t = clamp((x - edge0) / (edge1 - edge0), 0.0, 1.0);
				return t * t * (3.0 - 2.0 * t);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float brushSize = length(i.uv - _BrushPosition) / (_BrushSize * 0.5h);
				fixed4 col = tex2D(_MainTex, i.uv);
				col.b *= _ShowBlue;
				col.rg *= _ShowRedGreen;
				fixed4 brushColor = fixed4(0, 0.5f * (1 - _ShowRedGreen), 0.9f * (1 - _ShowBlue), 1);
				fixed brush = step(brushSize, 1) * saturate(SmoothStep(0, _BrushStrange, 1 - brushSize));

				half2 arrowUV = fmod(i.uv * 100, 1);
				fixed2 flowUV = tex2D(_MainTex, i.uv).rg * 2 - 1;
				half angle = atan2(flowUV.x, flowUV.y);
				half cosA = cos(angle);
				half sinA = sin(angle);
				half2x2 rotMatrix = half2x2(cosA, -sinA, sinA, cosA);
				arrowUV = mul(arrowUV, rotMatrix);
				fixed4 arrow = tex2D(ArrowTex, arrowUV * 2);

				col += step(0.01, abs(flowUV.x) + abs(flowUV.y)) * arrow * 0.5f;

				col = lerp(col, brushColor, brush);
				return col;
			}
			ENDCG
		}
	}
}
