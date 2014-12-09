Shader "Custom/toonWTransparency" {
	Properties {
		_MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Transparent" }
		//LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
