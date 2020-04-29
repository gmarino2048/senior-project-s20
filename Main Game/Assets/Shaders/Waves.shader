// Adapted from Catlike Coding wave shader tutorial: https://catlikecoding.com/unity/tutorials/flow/waves/
Shader "Custom/Waves"
{
    Properties
    {
        [Header(Material Properties)]
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Header(Wave Properties)]
        _Wave_0 ("Wave 0 (Dir 2D, Steepness, Wavelength)", Vector) = (1, 0, 0.50, 10)
        _Wave_1 ("Wave 1 (Dir 2D, Steepness, Wavelength)", Vector) = (0, 1, 0.25, 10)
        _Wave_2 ("Wave 1 (Dir 2D, Steepness, Wavelength)", Vector) = (1, 1, 0.15, 10)
        
        _Speed ("Speed Modifier", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma vertex vert
        #pragma surface surf Standard fullforwardshadows addshadow

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        float4 _Wave_0;
        float4 _Wave_1;
        float4 _Wave_2;
        
        float _Speed;
        float2 _Direction;
        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float3 GerstnerWave(float4 wave, float3 p, inout float3 tangent, inout float3 binormal)
        {
            float steepness = wave.z;
            float wavelength = wave.w;
            float2 direction = wave.xy;
            
            float lambda = 2 * UNITY_PI / wavelength;
            float amplitude = steepness / lambda;
            
            float phase_vel = _Speed * 9.8 / lambda;
            float time_element = lambda * (dot(direction, p.xz) - phase_vel * _Time.y);
            
            tangent += float3(
				-direction.x * direction.x * (steepness * sin(time_element)),
				direction.x * (steepness * cos(time_element)),
				-direction.x * direction.y * (steepness * sin(time_element))
			);
			binormal += float3(
				-direction.x * direction.y * (steepness * sin(time_element)),
				direction.y * (steepness * cos(time_element)),
				-direction.y * direction.y * (steepness * sin(time_element))
			);
			
			return float3(
				direction.x * (amplitude * cos(time_element)),
				amplitude * sin(time_element),
				direction.y * (amplitude * cos(time_element))
			);
        }
        
        void vert(inout appdata_full vertexData)
        {
            float3 gridPoint = vertexData.vertex.xyz;
            
			float3 tangent = float3(1, 0, 0);
			float3 binormal = float3(0, 0, 1);
			
			float3 p = gridPoint;
			p += GerstnerWave(_Wave_0, gridPoint, tangent, binormal);
			p += GerstnerWave(_Wave_1, gridPoint, tangent, binormal);
			p += GerstnerWave(_Wave_2, gridPoint, tangent, binormal);
			
			float3 normal = normalize(cross(binormal, tangent));

			vertexData.vertex.xyz = p;
			vertexData.normal = normal;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
