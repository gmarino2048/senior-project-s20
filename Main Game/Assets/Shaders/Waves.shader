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
        _Steepness ("Steepness", Range(0, 1)) = 0.5
        _Wavelength ("Wave Length", float) = 10
        _Direction ("Direction(2D)", Vector) = (1, 0, 0, 0)
        _Speed ("Speed Modifier", Range(0, 1)) = 1
        _WaveTexture ("Wave Texture", 2D) = "white" {}
        _WaveFrequency("Wave Frequency", Vector) = (0.05, 0.05, 0, 0)
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

        float _Steepness;
        float _Wavelength;
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
        
        void vert(inout appdata_full vertexData)
        {
            float3 p = vertexData.vertex.xyz;
            
            float2 dir = normalize(_Direction);

            float lambda = 2 * UNITY_PI / _Wavelength;
            float amplitude = _Steepness / lambda;
            
            float phase_vel = _Speed * 9.8 / lambda;
            float time_element = lambda * (dot(dir, p.xz) - phase_vel * _Time.y);
            
            p.x += dir.x * (amplitude * cos(time_element));
            p.y = amplitude * sin(time_element);
            p.z += dir.y * (amplitude * cos(time_element));
			
			float3 tangent = float3(
			    1 - dir.x * dir.x * (_Steepness * sin(time_element)), 
			    dir.y * (_Steepness * cos(time_element)), 
			    -dir.x * dir.y * (_Steepness * sin(time_element)));
			float3 binormal = float3(
			    -dir.x * dir.y * (_Steepness * sin(time_element)),
			    dir.y * (_Steepness * cos(time_element)),
			    1 - dir.y * dir.y * (_Steepness * sin(time_element)));
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
