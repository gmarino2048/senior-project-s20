﻿Shader "Custom/Waves"
{
    Properties
    {
        [Header(Material Properties)]
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Header(Wave Properties)]
        _Amplitude ("Wave Amplitude", float) = 1
        _Wavelength ("Wave Period Length", float) = 10
        _Speed ("Wave Ripple Speed", float) = 1
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
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        float _Amplitude;
        float _Wavelength;
        float _Speed;
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

            float lambda = 2 * UNITY_PI / _Wavelength;
            float time_element = p.x - (_Speed * _Time.y);
            float f = lambda * time_element;
			p.y = _Amplitude * sin(f);
			
			float3 tangent = normalize(float3(1, lambda * _Amplitude * cos(f), 0));
			float3 normal = float3(-tangent.y, tangent.x, 0);

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
