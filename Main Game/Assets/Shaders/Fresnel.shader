// Adapted from this tutorial from Kyle Halladay: http://kylehalladay.com/blog/tutorial/2014/02/18/Fresnel-Shaders-From-The-Ground-Up.html
Shader "Custom/Fresnel"
{
    Properties
    {
        // Main color for the shader
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _Glossiness ("Smoothness", Range(0,1)) = 0.5

        _Emission ("Emission", Range(0,1)) = 0.5
        _EmissionColor ("Emission Color", Color) = (1,1,1,1)
        _FresnelExponent ("Fresnel Exponent", Range(0,3)) = 0.5
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        LOD 200
        Lighting on
        ZWrite off
        Cull back
        Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 viewDir;
            INTERNAL_DATA
        };

        fixed4 _Color;
        half _Glossiness;
        half _Emission;
        fixed4 _EmissionColor;
        half _FresnelExponent;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            // Metallic and smoothness come from slider variables
            o.Smoothness = _Glossiness;

            float fresnel = dot(IN.worldNormal, IN.viewDir);

            fresnel = saturate(1 - fresnel);
            fresnel = pow(fresnel, _FresnelExponent);

            float3 fresnelColor = fresnel * _EmissionColor;
            o.Emission = _Emission + fresnelColor;
        }
        ENDCG
    }
    FallBack "Simple Transparent"
}
