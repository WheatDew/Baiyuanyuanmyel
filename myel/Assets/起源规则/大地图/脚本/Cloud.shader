﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Cloud"
{
	Properties 
	{
		_AtmosphereColor ("Atmosphere Color", Color) = (0.1, 0.35, 1.0, 1.0)
		_AtmospherePow ("Atmosphere Power", Range(1.5, 8)) = 2
		_AtmosphereMultiply ("Atmosphere Multiply", Range(1, 3)) = 1.5
		_Cutoff ("Alpha Cutoff", Range(0, 1)) = 0.5

		_DiffuseTex("Diffuse", 2D) = "white" {}
		
		_CloudAndNightTex("Cloud And Night", 2D) = "black" {}

		//_LightDir("Light Dir", Vector) = (-1,0,0,1)
	}

	SubShader 
	{

	Tags {"Queue"="AlphaTest" "IgnoreProjector"="True" "RenderType"="TransparentCutout"}
		ZWrite On
		ZTest LEqual

		pass
		{
		CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert 
			#pragma fragment frag
			
			sampler2D _DiffuseTex;
			sampler2D _CloudAndNightTex;

			float4 _AtmosphereColor;
			float _AtmospherePow;
			float _AtmosphereMultiply;
			fixed _Cutoff;

			//float4 _LightDir;

			struct vertexInput 
			{
				float4 pos				: POSITION;
				float3 normal			: NORMAL;
				float2 uv				: TEXCOORD0;
			};

			struct vertexOutput 
			{
				float4 pos			: POSITION;
				float2 uv			: TEXCOORD0;
				half diffuse		: TEXCOORD1;
				half night			: TEXCOORD2;
				half3 atmosphere	: TEXCOORD3;
			};

			void clip(float4 x)
			{
				if(any(x<0))
				discard;
			}
			
			vertexOutput vert(vertexInput input) 
			{
				vertexOutput output;
				output.pos = UnityObjectToClipPos(input.pos);
				output.uv = input.uv;

				float3 localLightDir = normalize(ObjSpaceLightDir(input.pos));
				output.diffuse = saturate(dot(localLightDir, input.normal) * 1.2);
				output.night = 1 - saturate(output.diffuse * 2);

				half3 viewDir = normalize(ObjSpaceViewDir(input.pos));
				half3 normalDir = input.normal;
				output.atmosphere = output.diffuse * _AtmosphereColor.rgb * pow(1 - saturate(dot(viewDir, normalDir)), _AtmospherePow) * _AtmosphereMultiply;

				return output;
			}

			half4 frag(vertexOutput input) : Color
			{
				half4 colorSample = tex2D(_DiffuseTex, input.uv);
				clip (colorSample.a - _Cutoff);

				half4 cloudAndNightSample = tex2D(_CloudAndNightTex, input.uv);

				half3 nightSample = cloudAndNightSample.rgb;
				/*half cloudSample = cloudAndNightSample.r;*/

				half4 result;
				/*result.rgb = (colorSample + cloudSample) * input.diffuse + nightSample * input.night + input.atmosphere;*/
				result.rgb = colorSample  * input.diffuse + nightSample * input.night + input.atmosphere;

				result.a = 1;
				return result;
			}
		ENDCG
		}
	}
	
	Fallback "Diffuse"
}
