// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Mtree/Bark Outline"
{
	Properties
	{
		[Header(Albedo Texture)]_Color("Color", Color) = (1,1,1,0)
		_MainTex("Albedo", 2D) = "white" {}
		[Enum(Off,0,Front,1,Back,2)]_CullMode("Cull Mode", Int) = 2
		[Header(Outline)]_ToonRamp("Toon Ramp", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
		_OutlineThickness("Outline Thickness", Range( 0 , 0.5)) = 0
		[Header(Normal Texture)]_BumpMap("Normal", 2D) = "bump" {}
		_BumpScale("Normal Strength", Float) = 1
		[Enum(On,0,Off,1)][Header(Detail Settings)]_BaseDetail("Base Detail", Int) = 1
		_DetailColor("Detail Color", Color) = (1,1,1,0)
		_DetailAlbedoMap("Detail", 2D) = "white" {}
		_DetailNormalMap("Detail Normal", 2D) = "bump" {}
		_Height("Height", Range( 0 , 1)) = 0
		_Smooth("Smooth", Range( 0.01 , 0.5)) = 0.02
		_TextureInfluence("Texture Influence", Range( 0 , 1)) = 0.5
		[Header(Other Settings)]_OcclusionStrength("AO strength", Range( 0 , 1)) = 0.6
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Glossiness("Smoothness", Range( 0 , 1)) = 0
		[Header(Wind)]_GlobalWindInfluence("Global Wind Influence", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
		[Header(Forward Rendering Options)]
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Reflections", Float) = 1.0
	}

	SubShader
	{
		Tags{ }
		Cull Front
		CGPROGRAM
		#pragma target 3.0
		#pragma surface outlineSurf Outline  keepalpha noshadow noambient novertexlights nolightmap nodynlightmap nodirlightmap nometa noforwardadd vertex:outlineVertexDataFunc 
		
		#include "UnityShaderVariables.cginc"
		
		
		struct Input
		{
			half filler;
		};
		uniform float _WindStrength;
		uniform float _GlobalWindInfluence;
		uniform float _RandomWindOffset;
		uniform float _WindPulse;
		uniform float _WindDirection;
		uniform float _OutlineThickness;
		uniform float4 _OutlineColor;
		
		float2 DirectionalEquation( float _WindDirection )
		{
			float d = _WindDirection * 0.0174532924;
			float xL = cos(d) + 1 / 2;
			float zL = sin(d) + 1 / 2;
			return float2(zL,xL);
		}


		void outlineVertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 VAR_VertexPosition21_g9 = mul( unity_ObjectToWorld, float4( ase_vertex3Pos , 0.0 ) ).xyz;
			float3 break109_g9 = VAR_VertexPosition21_g9;
			float VAR_WindStrength43_g9 = ( _WindStrength * _GlobalWindInfluence );
			float4 transform37_g9 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult38_g9 = (float2(transform37_g9.x , transform37_g9.z));
			float dotResult2_g10 = dot( appendResult38_g9 , float2( 12.9898,78.233 ) );
			float lerpResult8_g10 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g10 ) * 43758.55 ) ));
			float VAR_RandomTime16_g9 = ( _Time.x * lerpResult8_g10 );
			float FUNC_Turbulence36_g9 = ( sin( ( ( VAR_RandomTime16_g9 * 40.0 ) - ( VAR_VertexPosition21_g9.z / 15.0 ) ) ) * 0.5 );
			float VAR_WindPulse274_g9 = _WindPulse;
			float FUNC_Angle73_g9 = ( VAR_WindStrength43_g9 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g9 * 2.0 ) + FUNC_Turbulence36_g9 ) - ( VAR_VertexPosition21_g9.z / 50.0 ) ) - ( v.color.r / 20.0 ) ) ) ) * sqrt( v.color.r ) * 0.2 * VAR_WindPulse274_g9 );
			float VAR_SinA80_g9 = sin( FUNC_Angle73_g9 );
			float VAR_CosA78_g9 = cos( FUNC_Angle73_g9 );
			float _WindDirection164_g9 = _WindDirection;
			float2 localDirectionalEquation164_g9 = DirectionalEquation( _WindDirection164_g9 );
			float2 break165_g9 = localDirectionalEquation164_g9;
			float VAR_xLerp83_g9 = break165_g9.x;
			float lerpResult118_g9 = lerp( break109_g9.x , ( ( break109_g9.y * VAR_SinA80_g9 ) + ( break109_g9.x * VAR_CosA78_g9 ) ) , VAR_xLerp83_g9);
			float3 break98_g9 = VAR_VertexPosition21_g9;
			float3 break105_g9 = VAR_VertexPosition21_g9;
			float VAR_zLerp95_g9 = break165_g9.y;
			float lerpResult120_g9 = lerp( break105_g9.z , ( ( break105_g9.y * VAR_SinA80_g9 ) + ( break105_g9.z * VAR_CosA78_g9 ) ) , VAR_zLerp95_g9);
			float3 appendResult122_g9 = (float3(lerpResult118_g9 , ( ( break98_g9.y * VAR_CosA78_g9 ) - ( break98_g9.z * VAR_SinA80_g9 ) ) , lerpResult120_g9));
			float3 FUNC_vertexPos123_g9 = appendResult122_g9;
			float3 temp_output_5_0_g9 = mul( unity_WorldToObject, float4( FUNC_vertexPos123_g9 , 0.0 ) ).xyz;
			float3 temp_output_209_0 = ( temp_output_5_0_g9 - ase_vertex3Pos );
			float3 ase_vertexNormal = v.normal.xyz;
			float3 outlineVar = ( temp_output_209_0 + ( ase_vertexNormal * _OutlineThickness ) );
			v.vertex.xyz += outlineVar;
		}
		inline half4 LightingOutline( SurfaceOutput s, half3 lightDir, half atten ) { return half4 ( 0,0,0, s.Alpha); }
		void outlineSurf( Input i, inout SurfaceOutput o )
		{
			o.Emission = _OutlineColor.rgb;
		}
		ENDCG
		

		Tags{ "RenderType" = "TreeOpaque"  "Queue" = "Geometry+0" }
		Cull [_CullMode]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#pragma target 3.0
		#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
		#pragma shader_feature _GLOSSYREFLECTIONS_OFF
		#pragma multi_compile __ LOD_FADE_CROSSFADE
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
			float4 screenPosition;
		};

		uniform int _CullMode;
		uniform float _WindStrength;
		uniform float _GlobalWindInfluence;
		uniform float _RandomWindOffset;
		uniform float _WindPulse;
		uniform float _WindDirection;
		uniform sampler2D _DetailNormalMap;
		uniform float4 _DetailNormalMap_ST;
		uniform half _BumpScale;
		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform half _Height;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform half _TextureInfluence;
		uniform half _Smooth;
		uniform int _BaseDetail;
		uniform float4 _DetailColor;
		uniform sampler2D _DetailAlbedoMap;
		uniform float4 _DetailAlbedoMap_ST;
		uniform float4 _Color;
		uniform sampler2D _ToonRamp;
		uniform float _Metallic;
		uniform float _Glossiness;
		uniform half _OcclusionStrength;


		float2 DirectionalEquation( float _WindDirection )
		{
			float d = _WindDirection * 0.0174532924;
			float xL = cos(d) + 1 / 2;
			float zL = sin(d) + 1 / 2;
			return float2(zL,xL);
		}


		inline float Dither8x8Bayer( int x, int y )
		{
			const float dither[ 64 ] = {
				 1, 49, 13, 61,  4, 52, 16, 64,
				33, 17, 45, 29, 36, 20, 48, 32,
				 9, 57,  5, 53, 12, 60,  8, 56,
				41, 25, 37, 21, 44, 28, 40, 24,
				 3, 51, 15, 63,  2, 50, 14, 62,
				35, 19, 47, 31, 34, 18, 46, 30,
				11, 59,  7, 55, 10, 58,  6, 54,
				43, 27, 39, 23, 42, 26, 38, 22};
			int r = y * 8 + x;
			return dither[r] / 64; // same # of instructions as pre-dividing due to compiler magic
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 localOutlineOutput252 = ( 0 );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 VAR_VertexPosition21_g9 = mul( unity_ObjectToWorld, float4( ase_vertex3Pos , 0.0 ) ).xyz;
			float3 break109_g9 = VAR_VertexPosition21_g9;
			float VAR_WindStrength43_g9 = ( _WindStrength * _GlobalWindInfluence );
			float4 transform37_g9 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult38_g9 = (float2(transform37_g9.x , transform37_g9.z));
			float dotResult2_g10 = dot( appendResult38_g9 , float2( 12.9898,78.233 ) );
			float lerpResult8_g10 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g10 ) * 43758.55 ) ));
			float VAR_RandomTime16_g9 = ( _Time.x * lerpResult8_g10 );
			float FUNC_Turbulence36_g9 = ( sin( ( ( VAR_RandomTime16_g9 * 40.0 ) - ( VAR_VertexPosition21_g9.z / 15.0 ) ) ) * 0.5 );
			float VAR_WindPulse274_g9 = _WindPulse;
			float FUNC_Angle73_g9 = ( VAR_WindStrength43_g9 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g9 * 2.0 ) + FUNC_Turbulence36_g9 ) - ( VAR_VertexPosition21_g9.z / 50.0 ) ) - ( v.color.r / 20.0 ) ) ) ) * sqrt( v.color.r ) * 0.2 * VAR_WindPulse274_g9 );
			float VAR_SinA80_g9 = sin( FUNC_Angle73_g9 );
			float VAR_CosA78_g9 = cos( FUNC_Angle73_g9 );
			float _WindDirection164_g9 = _WindDirection;
			float2 localDirectionalEquation164_g9 = DirectionalEquation( _WindDirection164_g9 );
			float2 break165_g9 = localDirectionalEquation164_g9;
			float VAR_xLerp83_g9 = break165_g9.x;
			float lerpResult118_g9 = lerp( break109_g9.x , ( ( break109_g9.y * VAR_SinA80_g9 ) + ( break109_g9.x * VAR_CosA78_g9 ) ) , VAR_xLerp83_g9);
			float3 break98_g9 = VAR_VertexPosition21_g9;
			float3 break105_g9 = VAR_VertexPosition21_g9;
			float VAR_zLerp95_g9 = break165_g9.y;
			float lerpResult120_g9 = lerp( break105_g9.z , ( ( break105_g9.y * VAR_SinA80_g9 ) + ( break105_g9.z * VAR_CosA78_g9 ) ) , VAR_zLerp95_g9);
			float3 appendResult122_g9 = (float3(lerpResult118_g9 , ( ( break98_g9.y * VAR_CosA78_g9 ) - ( break98_g9.z * VAR_SinA80_g9 ) ) , lerpResult120_g9));
			float3 FUNC_vertexPos123_g9 = appendResult122_g9;
			float3 temp_output_5_0_g9 = mul( unity_WorldToObject, float4( FUNC_vertexPos123_g9 , 0.0 ) ).xyz;
			float3 temp_output_209_0 = ( temp_output_5_0_g9 - ase_vertex3Pos );
			float3 vertexoffset252 = temp_output_209_0;
			v.vertex.xyz += vertexoffset252;
			float3 OUT_VertexPos212 = localOutlineOutput252;
			v.vertex.xyz += OUT_VertexPos212;
			float4 ase_screenPos = ComputeScreenPos( UnityObjectToClipPos( v.vertex ) );
			o.screenPosition = ase_screenPos;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_DetailNormalMap = i.uv_texcoord * _DetailNormalMap_ST.xy + _DetailNormalMap_ST.zw;
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 VAR_AlbedoTexture132 = tex2D( _MainTex, uv_MainTex );
			float4 break93 = VAR_AlbedoTexture132;
			float clampResult70 = clamp( ( ( ( i.vertexColor.r - _Height ) + ( ( ( break93.r + break93.g + break93.b ) - 0.5 ) * _TextureInfluence ) ) / _Smooth ) , 0.0 , 1.0 );
			float FUNC_BarkDamageBlend137 = clampResult70;
			float3 lerpResult73 = lerp( UnpackScaleNormal( tex2D( _DetailNormalMap, uv_DetailNormalMap ), _BumpScale ) , UnpackScaleNormal( tex2D( _BumpMap, uv_BumpMap ), _BumpScale ) , FUNC_BarkDamageBlend137);
			int VAR_BaseDetail220 = _BaseDetail;
			float3 lerpResult259 = lerp( lerpResult73 , UnpackScaleNormal( tex2D( _BumpMap, uv_BumpMap ), _BumpScale ) , (float)VAR_BaseDetail220);
			float3 OUT_Normal210 = lerpResult259;
			o.Normal = OUT_Normal210;
			float2 uv_DetailAlbedoMap = i.uv_texcoord * _DetailAlbedoMap_ST.xy + _DetailAlbedoMap_ST.zw;
			float4 temp_output_94_0 = ( VAR_AlbedoTexture132 * _Color );
			float4 lerpResult89 = lerp( ( _DetailColor * tex2D( _DetailAlbedoMap, uv_DetailAlbedoMap ) ) , temp_output_94_0 , FUNC_BarkDamageBlend137);
			float4 lerpResult260 = lerp( lerpResult89 , temp_output_94_0 , (float)VAR_BaseDetail220);
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult237 = dot( ase_worldNormal , ase_worldlightDir );
			float2 temp_cast_5 = (saturate( (dotResult237*0.5 + 0.5) )).xx;
			float4 VAR_ColorRamp241 = tex2D( _ToonRamp, temp_cast_5 );
			float4 OUT_Albedo215 = ( lerpResult260 * VAR_ColorRamp241 );
			o.Albedo = OUT_Albedo215.rgb;
			o.Metallic = _Metallic;
			float lerpResult256 = lerp( 0.0 , VAR_AlbedoTexture132.r , _Glossiness);
			float OUT_Smoothness105 = lerpResult256;
			o.Smoothness = OUT_Smoothness105;
			float lerpResult40 = lerp( 1.0 , i.vertexColor.a , _OcclusionStrength);
			float OUT_AO204 = lerpResult40;
			o.Occlusion = OUT_AO204;
			float temp_output_41_0_g11 = VAR_AlbedoTexture132.a;
			float4 ase_screenPos = i.screenPosition;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 clipScreen45_g11 = ase_screenPosNorm.xy * _ScreenParams.xy;
			float dither45_g11 = Dither8x8Bayer( fmod(clipScreen45_g11.x, 8), fmod(clipScreen45_g11.y, 8) );
			dither45_g11 = step( dither45_g11, unity_LODFade.x );
			#ifdef LOD_FADE_CROSSFADE
				float staticSwitch40_g11 = ( temp_output_41_0_g11 * dither45_g11 );
			#else
				float staticSwitch40_g11 = temp_output_41_0_g11;
			#endif
			float OUT_Alpha228 = staticSwitch40_g11;
			o.Alpha = OUT_Alpha228;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=17800
532;85;1008;681;3232.435;-971.1329;3.643838;True;False
Node;AmplifyShaderEditor.CommentaryNode;62;-1910.43,-1397.629;Inherit;False;2338.832;816.1819;;15;215;205;89;94;138;88;75;67;65;78;132;79;80;221;260;Albedo;1,0.125,0.125,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;80;-1884.27,-934.2155;Float;True;Property;_MainTex;Albedo;2;0;Create;False;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;79;-1646.472,-935.0974;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;63;-1914.402,236.3903;Inherit;False;1716.032;775.5377;;14;137;70;66;85;87;84;90;68;83;81;82;92;93;133;Bark Damage Blend;0.5441177,0.3039554,0,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;132;-1337.993,-935.4142;Inherit;False;VAR_AlbedoTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;133;-1876.222,664.4685;Inherit;False;132;VAR_AlbedoTexture;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;93;-1637.486,669.0856;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;92;-1339.968,602.8804;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-1431.689,897.0052;Half;False;Property;_Height;Height;13;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;83;-1403.783,409.6715;Half;False;Property;_TextureInfluence;Texture Influence;15;0;Create;True;0;0;False;0;0.5;0.5;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;82;-1347.507,735.5916;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;81;-1225.52,292.4143;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;231;-1856.786,3619.55;Inherit;False;2082.579;669.7218;;4;241;240;235;232;Color Ramp;1,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;232;-1834.312,3851.228;Inherit;False;540.401;361.8907;;3;237;234;233;N . L;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;90;-1105.822,720.4196;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-1047.45,390.9706;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;234;-1770.312,4059.227;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;233;-1722.312,3899.227;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.CommentaryNode;235;-1143.682,3871.019;Inherit;False;723.599;290;Also know as Lambert Wrap or Half Lambert;3;239;238;236;Diffuse Wrap;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-1224.069,541.4816;Half;False;Property;_Smooth;Smooth;14;0;Create;True;0;0;False;0;0.02;0.02;0.01;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;85;-940.9781,731.3996;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;237;-1434.312,3963.227;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;236;-1093.682,4046.019;Float;False;Constant;_WrapperValue;Wrapper Value;0;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;66;-804.7661,732.3817;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;238;-872.7382,3965.475;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;70;-678.6691,733.6096;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;78;-1769.214,-1178.649;Float;True;Property;_DetailAlbedoMap;Detail;11;0;Create;False;0;0;False;0;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.CommentaryNode;213;-1895.107,2576.229;Inherit;False;2029.788;647.2447;;8;209;250;249;248;246;245;212;251;Vertex Pos;0,1,0.09019608,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;64;-1916.846,-459.413;Inherit;False;1816.454;548.9569;;12;73;139;72;71;76;77;69;74;86;210;222;259;Normals;0,0.6275859,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;223;757.7206,-1396.159;Inherit;False;516.7706;280.4808;;3;176;220;181;Variables;1,0,0.7254902,1;0;0
Node;AmplifyShaderEditor.ColorNode;65;-1734.939,-1352.974;Float;False;Property;_DetailColor;Detail Color;10;0;Create;True;0;0;False;0;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;86;-1815.917,-117.044;Float;True;Property;_BumpMap;Normal;7;0;Create;False;0;0;False;1;Header(Normal Texture);None;None;True;bump;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.NormalVertexDataNode;246;-1565.858,2938.076;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;245;-1648.107,3082.441;Inherit;False;Property;_OutlineThickness;Outline Thickness;6;0;Create;True;0;0;False;0;0;0;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;74;-1829.845,-409.541;Float;True;Property;_DetailNormalMap;Detail Normal;12;0;Create;False;0;0;False;0;None;None;True;bump;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SaturateNode;239;-595.083,3927.819;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;176;806.7206,-1194.485;Inherit;False;Property;_BaseDetail;Base Detail;9;1;[Enum];Create;True;2;On;0;Off;1;0;False;1;Header(Detail Settings);1;0;0;1;INT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;137;-506.3039,736.2026;Inherit;False;FUNC_BarkDamageBlend;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;67;-1531.062,-1175.177;Inherit;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;75;-1291.41,-844.7188;Float;False;Property;_Color;Color;1;0;Create;True;0;0;False;1;Header(Albedo Texture);1,1,1,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;138;-1187.753,-1067.079;Inherit;False;137;FUNC_BarkDamageBlend;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;76;-1573.367,-409.4128;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;69;-1484.952,-209.7666;Half;False;Property;_BumpScale;Normal Strength;8;0;Create;False;0;0;False;0;1;1.22;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;209;-1561.416,2856.577;Inherit;False;Mtree Wind;19;;9;d710ffc7589a70c42a3e6c5220c6279d;7,269,0,281,0,272,0,255,0,282,0,280,0,278,0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;220;1017.491,-1192.678;Inherit;False;VAR_BaseDetail;-1;True;1;0;INT;0;False;1;INT;0
Node;AmplifyShaderEditor.SamplerNode;77;-1575.864,-115.7556;Inherit;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;229;-1889.26,2280.363;Inherit;False;1029.764;229;;4;227;226;228;225;Alpha;1,1,0.534,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;94;-1048.915,-929.5502;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;88;-1101.673,-1346.463;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;109;-1906.097,1217.689;Inherit;False;1288.315;497.5828;;5;105;255;256;257;258;Smoothness;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;240;-348.4541,3901.213;Inherit;True;Property;_ToonRamp;Toon Ramp;4;0;Create;True;0;0;False;1;Header(Outline);-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;248;-1292.166,2939.007;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;255;-1856.183,1338.705;Inherit;False;132;VAR_AlbedoTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;71;-1166.938,-317.9154;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleAddOpNode;249;-1130.107,2785.975;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;139;-1174.484,-402.0046;Inherit;False;137;FUNC_BarkDamageBlend;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;221;-882.6091,-853.9637;Inherit;False;220;VAR_BaseDetail;1;0;OBJECT;;False;1;INT;0
Node;AmplifyShaderEditor.LerpOp;89;-816.1126,-1037.168;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;111;-1894.282,1818.737;Inherit;False;789.0012;362.734;;4;40;38;39;204;AO;0.5367647,0.355212,0.355212,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;226;-1839.26,2331.827;Inherit;False;132;VAR_AlbedoTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;241;-49.03802,3902.088;Inherit;False;VAR_ColorRamp;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;72;-1164.148,-111.1343;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;250;-1210.115,2614.602;Inherit;False;Property;_OutlineColor;Outline Color;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;253;-555.8626,-728.6365;Inherit;False;241;VAR_ColorRamp;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;227;-1588.732,2330.363;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;258;-1639.437,1489.168;Inherit;False;Property;_Glossiness;Smoothness;18;0;Create;False;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;73;-826.826,-264.6131;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-1852.334,2035.402;Half;False;Property;_OcclusionStrength;AO strength;16;0;Create;False;0;0;False;1;Header(Other Settings);0.6;0.682;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;260;-574.0514,-952.1747;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;257;-1597.264,1344.449;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;222;-867.5163,-40.19589;Inherit;False;220;VAR_BaseDetail;1;0;OBJECT;;False;1;INT;0
Node;AmplifyShaderEditor.VertexColorNode;39;-1849.282,1870.071;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OutlineNode;251;-919.4485,2614.229;Inherit;False;2;False;None;0;0;Front;3;0;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;205;-156.0833,-951.1441;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;259;-569.7021,-131.8717;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;225;-1325.531,2407.393;Inherit;False;LOD CrossFade;-1;;11;bbfabe35be0e79d438adaa880ee1b0aa;1,44,1;1;41;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CustomExpressionNode;252;-696.7436,2824.04;Inherit;False;v.vertex.xyz += vertexoffset@;1;True;1;True;vertexoffset;FLOAT3;0,0,0;In;;Float;False;Outline Output;False;False;0;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;256;-1255.889,1446.687;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;40;-1520.743,1946.201;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;105;-1059.674,1442.456;Inherit;False;OUT_Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;210;-373.2967,-138.1565;Inherit;False;OUT_Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;215;40.83933,-956.204;Inherit;False;OUT_Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;212;-355.0546,2819.346;Inherit;False;OUT_VertexPos;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;204;-1330.425,1939.549;Inherit;False;OUT_AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;228;-1083.421,2393.969;Inherit;False;OUT_Alpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;217;760.597,-870.1332;Inherit;False;1099.843;840.2675;;8;216;214;108;195;211;194;230;261;Output;0,0,0,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;230;889.2448,-238.6692;Inherit;False;228;OUT_Alpha;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;108;850.7147,-400.8286;Inherit;False;105;OUT_Smoothness;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;216;889.3597,-624.7513;Inherit;False;215;OUT_Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;214;863.3909,-166.4926;Inherit;False;212;OUT_VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.IntNode;181;811.1584,-1346.159;Inherit;False;Property;_CullMode;Cull Mode;3;1;[Enum];Create;True;3;Off;0;Front;1;Back;2;0;True;0;2;0;0;1;INT;0
Node;AmplifyShaderEditor.RangedFloatNode;195;814.5529,-473.1964;Inherit;False;Property;_Metallic;Metallic;17;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;261;903.587,-324.0891;Inherit;False;204;OUT_AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;211;881.0494,-550.5397;Inherit;False;210;OUT_Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;194;1462.745,-600.17;Float;False;True;-1;2;;0;0;Standard;Mtree/Bark Outline;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;Back;0;False;254;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TreeOpaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;True;181;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;79;0;80;0
WireConnection;132;0;79;0
WireConnection;93;0;133;0
WireConnection;92;0;93;0
WireConnection;92;1;93;1
WireConnection;92;2;93;2
WireConnection;81;0;92;0
WireConnection;90;0;82;1
WireConnection;90;1;68;0
WireConnection;84;0;81;0
WireConnection;84;1;83;0
WireConnection;85;0;90;0
WireConnection;85;1;84;0
WireConnection;237;0;233;0
WireConnection;237;1;234;0
WireConnection;66;0;85;0
WireConnection;66;1;87;0
WireConnection;238;0;237;0
WireConnection;238;1;236;0
WireConnection;238;2;236;0
WireConnection;70;0;66;0
WireConnection;239;0;238;0
WireConnection;137;0;70;0
WireConnection;67;0;78;0
WireConnection;76;0;74;0
WireConnection;220;0;176;0
WireConnection;77;0;86;0
WireConnection;94;0;132;0
WireConnection;94;1;75;0
WireConnection;88;0;65;0
WireConnection;88;1;67;0
WireConnection;240;1;239;0
WireConnection;248;0;246;0
WireConnection;248;1;245;0
WireConnection;71;0;76;0
WireConnection;71;1;69;0
WireConnection;249;0;209;0
WireConnection;249;1;248;0
WireConnection;89;0;88;0
WireConnection;89;1;94;0
WireConnection;89;2;138;0
WireConnection;241;0;240;0
WireConnection;72;0;77;0
WireConnection;72;1;69;0
WireConnection;227;0;226;0
WireConnection;73;0;71;0
WireConnection;73;1;72;0
WireConnection;73;2;139;0
WireConnection;260;0;89;0
WireConnection;260;1;94;0
WireConnection;260;2;221;0
WireConnection;257;0;255;0
WireConnection;251;0;250;0
WireConnection;251;1;249;0
WireConnection;205;0;260;0
WireConnection;205;1;253;0
WireConnection;259;0;73;0
WireConnection;259;1;72;0
WireConnection;259;2;222;0
WireConnection;225;41;227;3
WireConnection;252;0;251;0
WireConnection;252;1;209;0
WireConnection;256;1;257;0
WireConnection;256;2;258;0
WireConnection;40;1;39;4
WireConnection;40;2;38;0
WireConnection;105;0;256;0
WireConnection;210;0;259;0
WireConnection;215;0;205;0
WireConnection;212;0;252;0
WireConnection;204;0;40;0
WireConnection;228;0;225;0
WireConnection;194;0;216;0
WireConnection;194;1;211;0
WireConnection;194;3;195;0
WireConnection;194;4;108;0
WireConnection;194;5;261;0
WireConnection;194;9;230;0
WireConnection;194;11;214;0
ASEEND*/
//CHKSM=1204E11784BE4547FEC45302C52CCE5AC446910A