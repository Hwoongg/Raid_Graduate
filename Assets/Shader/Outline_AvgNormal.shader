// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Outline_AvgNormal"
{
	Properties
	{
		_TintColor("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_NormalTex("Normal", 2D) = "bump" {}
		_RampTex("Ramp", 2D) = "white" {}
		_OutlineWidth("Outline Width", float) = 0.5
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
	}

	SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		Cull front
		pass
		{
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vertLine
			#pragma fragment fragLine
			#pragma target 3.0

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float3 vertexColor : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			half _OutlineWidth;
			half4 _OutlineColor;

			v2f vertLine(appdata_full v)
			{
				v2f o;
				o.vertexColor = v.color;
				o.texcoord = v.texcoord;
				o.normal = v.normal;
				//o.vertex = UnityObjectToClipPos(v.vertex + v.normal * _OutlineWidth);
				o.vertex = UnityObjectToClipPos(v.vertex);

				float3 clipNormal = mul((float3x3)UNITY_MATRIX_MVP, o.normal).xyz;
				float2 offset = normalize(clipNormal.xy) / _ScreenParams.xy * _OutlineWidth * o.vertex.w * 0.5f;
				o.vertex.xy += offset;
				return o;
			}

			float4 fragLine(v2f i) : SV_Target
			{
				float4 col;
				col.rgb = (i.vertex.w - 1) * 0.01f * _OutlineColor.rgb;
				col.a = _OutlineColor.a;
				return col;
			}
			ENDCG
		}

		Tags {"RenderType" = "Opaque"}
		Cull back
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma shader_feature __NORMAL_ON__
		#pragma surface surf ToonRamp fullforwardshadows 
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _NormalTex;
		sampler2D _RampTex;
		fixed4 _TintColor;
		uniform float4 _NormalTex_ST;

		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		#pragma lighting ToonRamp exclude_path::prepass
		inline float4 LightingToonRamp(SurfaceOutput s, half3 lightDir, half atten)
		{
			//lightDir = normalize(lightDir);
			half d = max(dot(s.Normal, lightDir), 0.0f) * 0.5f + 0.5f;
			half3 ramp = tex2D(_RampTex, half2(d, d)) * 1.2f;

			half4 col;
			col.rgb = s.Albedo * _LightColor0.rgb * ramp; //* (atten * 2.0f);
			col.a = 0;
			return col;
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _TintColor;
			o.Albedo = c.rgb;

			o.Normal = UnpackNormal(tex2D(_NormalTex, IN.uv_MainTex * _NormalTex_ST));
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
