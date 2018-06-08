Shader "Retro/Pixel"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
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
			#pragma target 5.0
			#include "UnityCG.cginc"

			struct v2f
			{
				float4 vertex : SV_POSITION;
				fixed4 color :COLOR;
			};

			StructuredBuffer<float2> points;
			StructuredBuffer<float4> colors;

			float2 imgsize;
			
			v2f vert (uint id : SV_VertexID)
			{
				v2f o;
				float2 p = (points[id] +0.5) / imgsize;
				p = p*2-1;
				o.vertex = float4(p,0.5,1);
				o.color = colors[id];
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return i.color;
			}
			ENDCG
		}
	}
}
