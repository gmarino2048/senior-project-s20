// Swaying Grass Shader adapted from https://roystan.net/articles/grass-shader.html
Shader "Custom/Grass"
{
    Properties
    {
		[Header(Shading)]
        _TopColor("Top Color", Color) = (1,1,1,1)
		_BottomColor("Bottom Color", Color) = (1,1,1,1)
		_TranslucentGain("Translucent Gain", Range(0,1)) = 0.5
		
		_BendRotationRandom("Bend Rotation Random", Range(0,1)) = 0.2
		
		_BladeWidth("Blade Width", Range(0,1)) = 0.05
		_BladeWidthRandom("Blade Width Random", Range(0,1)) = 0.02
		
		_BladeHeight("Blade Height", Range(0,1)) = 0.5
		_BladeHeightRandom("Blade Height Random", Range(0,1)) = 0.3
		
		_TessellationUniform("Tessellation Uniform", Range(1, 64)) = 1
		
		_WindDistortionMap("Wind Distortion Map", 2D) = "white" {}
		_WindFrequency("Wind Frequency", Vector) = (0.05, 0.05, 0, 0)
		_WindStrength("Wind Strength", Float) = 1
    }

	CGINCLUDE
    // Upgrade NOTE: excluded shader from DX11 because it uses wrong array syntax (type[size] name)
    #pragma exclude_renderers d3d11
	#include "UnityCG.cginc"
	#include "Autolight.cginc"
	#include "CustomTessellation.cginc"
	
	float _BendRotationRandom;
	
	float _BladeWidth;
	float _BladeWidthRandom;
	
	float _BladeHeight;
	float _BladeHeightRandom;
	
	sampler2D _WindDistortionMap;
    float4 _WindDistortionMap_ST;

    float2 _WindFrequency;
    
    float _WindStrength;

	// Simple noise function, sourced from http://answers.unity.com/answers/624136/view.html
	// Extended discussion on this function can be found at the following link:
	// https://forum.unity.com/threads/am-i-over-complicating-this-random-function.454887/#post-2949326
	// Returns a number in the 0...1 range.
	float rand(float3 co)
	{
		return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 53.539))) * 43758.5453);
	}
	
	// Construct a rotation matrix that rotates around the provided axis, sourced from:
	// https://gist.github.com/keijiro/ee439d5e7388f3aafc5296005c8c3f33
	float3x3 AngleAxis3x3(float angle, float3 axis)
	{
		float c, s;
		sincos(angle, s, c);

		float t = 1 - c;
		float x = axis.x;
		float y = axis.y;
		float z = axis.z;

		return float3x3(
			t * x * x + c, t * x * y - s * z, t * x * z + s * y,
			t * x * y + s * z, t * y * y + c, t * y * z - s * x,
			t * x * z - s * y, t * y * z + s * x, t * z * z + c
			);
	}
	
	// Geometry shader output structure
	struct geometryOutput
	{
	    float4 pos : SV_POSITION;
	    float2 uv : TEXCOORD0;
	};
	
	geometryOutput VertexOutput(float3 pos, float2 uv)
	{
	    geometryOutput o;
	    o.pos = UnityObjectToClipPos(pos);
	    o.uv = uv;
	    return o;
	}
	
	// Geometry shader function
	[maxvertexcount(3)]
	void geo(triangle vertexOutput IN[3], inout TriangleStream<geometryOutput> triStream)
	{
	    float3 vPos = IN[0].vertex;
	    float3 vNormal = IN[0].normal;
	    float4 vTangent = IN[0].tangent;
	    
	    // Calculate the binormal vector
	    float3 vBinormal = cross(vNormal, vTangent) * vTangent.w;
	    
	    // Declare the three vectors
	    float3x3 tangentToLocal = float3x3(
	        vTangent.x, vBinormal.x, vNormal.x,
	        vTangent.y, vBinormal.y, vNormal.y,
	        vTangent.z, vBinormal.z, vNormal.z);
	        
	    float3x3 facingRotationMatrix = AngleAxis3x3(rand(vPos) * UNITY_TWO_PI, float3(0, 0, 1));
	    float3x3 bendRotationMatrix = AngleAxis3x3(
	        rand(vPos.xxy) * _BendRotationRandom * UNITY_PI * 0.5, float3(-1, 0, 0));
	        
	    float2 uv = vPos.xz * _WindDistortionMap_ST.xy + _WindDistortionMap_ST.zw
	        + _WindFrequency * _Time.y;
	    
	    float2 windSample = (tex2Dlod(_WindDistortionMap, float4(uv, 0, 0)).xy * 2 - 1) * _WindStrength;
	    float3 wind = normalize(float3(windSample.x, windSample.y, 0));
	    float3x3 windRotation = AngleAxis3x3(UNITY_PI * windSample, wind);
	        
	    float width = (rand(vPos.zyx) * 2 - 1) * _BladeWidthRandom + _BladeWidth;
	    float height = (rand(vPos.xzy) * 2 - 1) * _BladeHeightRandom + _BladeHeight;
	    
	    float3x3 transformationMatrix = 
            mul(
                mul(
                    mul(tangentToLocal, windRotation),
                    facingRotationMatrix),
                bendRotationMatrix);
	        
	    float3 posX = float3(width, 0, 0);
	    float3 negX = float3(-width, 0, 0);
	    float3 posZ = float3(0, 0, height);
	        
        triStream.Append(VertexOutput(
            vPos + mul(transformationMatrix, posX),
            float2(0, 0)));
        triStream.Append(VertexOutput(
            vPos + mul(transformationMatrix, negX),
            float2(0, 1)));
        triStream.Append(VertexOutput(
            vPos + mul(transformationMatrix, posZ),
            float2(0.5, 1)));
	}
	
	ENDCG

    SubShader
    {
		Cull Off

        Pass
        {
			Tags
			{
				"RenderType" = "Opaque"
				"LightMode" = "ForwardBase"
			}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geo
			#pragma target 4.6
			#pragma hull hull
			#pragma domain domain
            
			#include "Lighting.cginc"

			float4 _TopColor;
			float4 _BottomColor;
			float _TranslucentGain;

			float4 frag (geometryOutput i, fixed facing : VFACE) : SV_Target
            {	
				return lerp(_BottomColor, _TopColor, i.uv.y);
            }
            
            ENDCG
        }
    }
}