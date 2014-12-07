Shader "Custom/toonOutline" {
	Properties {
		//_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Diffuse Material Color", Color)=(1,1,1,1)
	}
	SubShader {
		//Tags { "RenderType"="Opaque" }
		//LOD 200
		
		Cull Back
		Pass{
		tags{"LightMode" = "ForwardBase"}
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

//		sampler2D _MainTex;

		struct vertexInput{
			float4 vertex: POSITION;
			float3 normal: NORMAL;
			};
			
		struct vertexOutput{
		float3 normalWorld: TEXCOORD2;
		float4 positionScreen:SV_POSITION;
		float4 positionWorld: TEXCOORD3;
		};
		
//		struct Input {
//			float2 uv_MainTex;
//		};
		uniform float4 _Color;

//		void surf (Input IN, inout SurfaceOutput o) {
//			half4 c = tex2D (_MainTex, IN.uv_MainTex);
//			o.Albedo = c.rgb;
//			o.Alpha = c.a;
//		}

		vertexOutput vert (vertexInput input)
		{
			vertexOutput output;
			output.positionScreen = mul(UNITY_MATRIX_MVP, input.vertex);
			output.positionWorld= mul(_Object2World, input.vertex);
			float4 temp= mul(float4(input.normal,0.0), _World2Object);
			output.normalWorld = normalize(float3(temp.xyz));		
			return output;	
		}
		
		float4 frag(vertexOutput input): COLOR
		{
			float3 lightDirection=normalize(float3(_WorldSpaceLightPos0.xyz));
			float4 temp = normalize(float4(_WorldSpaceCameraPos, 1.0)-input.positionWorld);
			float3 viewDir = float3(temp.xyz);
			float4 color= _Color;
			if (dot(viewDir,input.normalWorld)<0.5)
			{ color=float4(0,0,0,0);
			}
			else if (dot(input.normalWorld, lightDirection)<0.1)
			{
			  color =0.5*_Color;
			}	
			return color;
		}
		ENDCG
	} 
	//FallBack "Diffuse"
	}
	}