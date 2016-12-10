// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/PowerUp"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,0)
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}

		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off ZTest Always

		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 worldNormal  : NORMAL;
				float3 viewDir : TEXCOORD0;
			};

			float4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz);
				return o;
			}
			
			float4 frag (v2f i) : SV_Target
			{
				float ndotv = dot(i.worldNormal, i.viewDir);
				float4 col = float4(i.worldNormal, 1 - ndotv + _Color.w);
				return col;
			}
			ENDCG
		}
	}
}
