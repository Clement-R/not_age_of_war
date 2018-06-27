Shader "Hidden/BevelPixel"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BevelTex("Bevel Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _BevelTex;
			float4 _MainTex_TexelSize;

			fixed4 frag (v2f IN) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, IN.uv);
				fixed4 bev = tex2D(_BevelTex, half2((IN.uv.x * _MainTex_TexelSize.z) % 1, (IN.uv.y * _MainTex_TexelSize.w) % 1));
				c = lerp(1 - 2 * (1 - c) * (1 - bev), 2 * c * bev, step(c, 0.5));
				c.rgb *= c.a;
				return c;
			}
			ENDCG
		}
	}
}
