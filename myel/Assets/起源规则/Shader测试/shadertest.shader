Shader "Unlit/shadertest"
{
   Properties
   {
		_Color("Color",Color)=(1,1,1,1)
   }
   SubShader
   {
		pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			fixed4 _Color;

			struct appdata{
				float4 vertex:POSITION;
				float2 uv:TEXCOORD;
			};

			struct v2f{
				float4 pos:SV_POSITION;
				float2 uv:TEXCOORD;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.pos=UnityObjectToClipPos(v.vertex);
				o.uv=v.uv;

				return o;
			}

			fixed checker(float2 uv){
				float2 repeatUV=uv*5;
				float2 c=floor(repeatUV)/2;
				float checker= (frac(c.x+c.y)*2)*uv.x;
				return checker;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				fixed col=checker(i.uv);
				return col;
			}
			ENDCG
		}
   }
}
