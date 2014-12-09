Shader "tryout" {
	Properties {
		_DiffColor ("Diffuse colour", COLOR) = (1,1,1,1)
		_DiffMap2 ("Diffuse map 2", 2D) = "white" {}
		_DiffMap3 ("Diffuse map 3", 2D) = "white" {}
		_DiffMap4 ("Diffuse map 4", 2D) = "white" {}

		_Threshold1 ("Threshold 1", Range(-1,1)) = 0.6
		_Threshold2 ("Threshold 2", Range(-1,1)) = 0.2
		_Threshold3 ("Threshold 3", Range(-1,1)) = -0.3

		_Overlap1 ("Overlap length 1", Range(0, 0.5)) = 0.1
		_Overlap2 ("Overlap length 2", Range(0, 0.5)) = 0.1
		_Overlap3 ("Overlap length 3", Range(0, 0.5)) = 0.1

		_EdgeSize ("Edge size", FLOAT) = 0.33
	}
	SubShader {
		Pass {
		
		//	tags{"LightMode" = "ForwardBase"}
			Fog {Mode OFF}
			
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag

			#pragma target 3.0

			#include "UnityCG.cginc"

			uniform float4 _DiffColor;
			uniform sampler2D _DiffMap2;
			uniform sampler2D _DiffMap3;
			uniform sampler2D _DiffMap4;
			uniform float _Threshold1;
			uniform float _Threshold2;
			uniform float _Threshold3;
			uniform float _Overlap1;
			uniform float _Overlap2;
			uniform float _Overlap3;

			uniform float _EdgeSize;

			struct vIn 
			{
				float4 pos : POSITION;
				float4 normal : NORMAL;
				float4 tex : TEXCOORD0;
			};

			struct v2f 
			{
				float4 pos : SV_POSITION;
				float4 normal : TEXCOORD0;
				float4 tex : TEXCOORD2;
				//float2 pos2 : TEXCOORD4; // Used for drawing texture relative to screen rather than world space
				float4 lightDirection : TEXCOORD1;
				float3 viewDir : TEXCOORD3;
			};

			v2f vert(vIn input)
			{
				v2f output;

				output.pos = mul(UNITY_MATRIX_MVP, input.pos);
				output.normal = mul(input.normal, _World2Object);
				
				output.tex = input.tex;
				//output.pos2 = float2(output.pos.x / output.pos.w, output.pos.y / output.pos.w);

				output.lightDirection = normalize(_WorldSpaceLightPos0);
				float4 temp = float4(_WorldSpaceCameraPos, 1.0)- mul(_Object2World, input.pos);
				output.viewDir = normalize(float3(temp.xyz));

				return output;
			}

			float4 frag(v2f input) : COLOR
			{
				float dotProduct = dot(input.normal, input.lightDirection);

				if(dot(input.viewDir, input.normal) < _EdgeSize)
				{
					return float4(0,0,0,0);
				} 
				else if(dotProduct > _Threshold1)
				{
					return 	_DiffColor;
				}
				else if(dotProduct > (_Threshold1 - _Overlap1))
				{
					float mix = (_Threshold1 - dotProduct) / _Overlap1; // Goes from 0 to 1
					return 	_DiffColor * lerp( float4(1,1,1,1), tex2D(_DiffMap2, float2(input.tex.xy)), mix);
				}
				else if(dotProduct > _Threshold2)
				{
					return _DiffColor*tex2D(_DiffMap2, float2(input.tex.xy));
				}
				else if(dotProduct > (_Threshold2 - _Overlap2))
				{
					float mix = (_Threshold2 - dotProduct) / _Overlap2; 
					return 						 _DiffColor 
						 					   * tex2D(_DiffMap2, float2(input.tex.xy)) 
						* lerp( float4(1,1,1,1), tex2D(_DiffMap3, float2(input.tex.xy)), mix);
				}
				else if(dotProduct > _Threshold3)
				{
					return _DiffColor 
						 * tex2D(_DiffMap2, float2(input.tex.xy)) 
						 * tex2D(_DiffMap3, float2(input.tex.xy));
				}
				else if(dotProduct > (_Threshold3 - _Overlap3))
				{
					float mix = (_Threshold3 - dotProduct) / _Overlap3; 
					return 						 _DiffColor 
						 					   * tex2D(_DiffMap2, float2(input.tex.xy))
						 					   * tex2D(_DiffMap3, float2(input.tex.xy)) 
						* lerp( float4(1,1,1,1), tex2D(_DiffMap4, float2(input.tex.xy)), mix);
				}
				else
				{
					return _DiffColor 
						 * tex2D(_DiffMap2, float2(input.tex.xy)) 
						 * tex2D(_DiffMap3, float2(input.tex.xy)) 
						 * tex2D(_DiffMap4, float2(input.tex.xy));
				}
			}
		
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
