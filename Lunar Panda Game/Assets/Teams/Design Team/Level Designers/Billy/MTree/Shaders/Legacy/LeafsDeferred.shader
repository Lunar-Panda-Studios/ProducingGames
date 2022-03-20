// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Mtree/Leafs Deferred"
{
	Properties
	{
		[Header(Albedo Texture)]_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		[Enum(Off,0,Front,1,Back,2)]_CullMode("Cull Mode", Int) = 0
		[Enum(Flip,0,Mirror,1,None,2)]_DoubleSidedNormalMode("Double Sided Normal Mode", Int) = 0
		_Cutoff("Cutoff", Range( 0 , 1)) = 0.5
		[Header(Normal Texture)]_BumpMap("Normal Map", 2D) = "bump" {}
		_BumpScale("Normal Strength", Float) = 1
		[Enum(On,0,Off,1)][Header(Color Settings)]_ColorShifting("Color Shifting", Int) = 1
		_Hue("Hue", Range( -0.5 , 0.5)) = -0.5
		_Value("Value", Range( 0 , 3)) = 1
		_Saturation("Saturation", Range( 0 , 2)) = 1
		_ColorVariation("Color Variation", Range( 0 , 0.3)) = 0.15
		[Header(Other Settings)]_OcclusionStrength("AO strength", Range( 0 , 1)) = 0.6
		_Metallic("Metallic", Range( 0 , 1)) = 0
		_Glossiness("Smoothness", Range( 0 , 1)) = 0
		[Enum(Off,0,On,1)][Header(Translucency)]_TranslucencyEnum("Translucency", Int) = 1
		_Translucency("Strength", Range( 0 , 50)) = 4
		_TransNormalDistortion("Normal Distortion", Range( 0 , 10)) = 1
		_TransScale("Scale", Range( 0 , 10)) = 1
		_TransScattering("Scattering Falloff", Range( 1 , 50)) = 1
		[HDR]_TranslucencyTint("Translucency Tint", Color) = (1,1,1,0)
		[Enum(Global,0,Custom,1)]_ColorSource("Color Source", Int) = 0
		[Header(Wind)]_GlobalWindInfluence("Global Wind Influence", Range( 0 , 1)) = 1
		_GlobalTurbulenceInfluence("Global Turbulence Influence", Range( 0 , 1)) = 1
		[Enum(Leaves,0,Palm,1,Grass,2,Off,3)]_WindModeLeaves("Wind Mode Leaves", Int) = 0
		[HideInInspector] _texcoord4( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull [_CullMode]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma multi_compile __ LOD_FADE_CROSSFADE
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:forward vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			half ASEVFace : VFACE;
			float4 vertexColor : COLOR;
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float2 uv4_texcoord4;
			float4 screenPosition;
		};

		uniform int _WindModeLeaves;
		uniform float _WindStrength;
		uniform float _GlobalWindInfluence;
		uniform float _RandomWindOffset;
		uniform float _WindPulse;
		uniform float _WindDirection;
		uniform float _WindTurbulence;
		uniform float _GlobalTurbulenceInfluence;
		uniform int _DoubleSidedNormalMode;
		uniform int _CullMode;
		uniform sampler2D _BumpMap;
		uniform float4 _BumpMap_ST;
		uniform half _BumpScale;
		uniform float _ColorVariation;
		uniform half _Hue;
		uniform float4 _Color;
		uniform sampler2D _MainTex;
		uniform float4 _MainTex_ST;
		uniform float _Saturation;
		uniform float _Value;
		uniform int _ColorShifting;
		uniform float _TransScattering;
		uniform float _TransNormalDistortion;
		uniform float _Translucency;
		uniform float _TransScale;
		uniform float4 _TranslucencyTint;
		uniform int _ColorSource;
		uniform int _TranslucencyEnum;
		uniform float _Metallic;
		uniform float _Glossiness;
		uniform half _OcclusionStrength;
		uniform half _Cutoff;


		float2 DirectionalEquation( float _WindDirection )
		{
			float d = _WindDirection * 0.0174532924;
			float xL = cos(d) + 1 / 2;
			float zL = sin(d) + 1 / 2;
			return float2(zL,xL);
		}


		float3 If252_g478( int m_Switch , float3 m_Leaves , float3 m_Palm , float3 m_Grass , float3 m_None )
		{
			float3 Output = m_None;
			if(m_Switch == 0){Output = m_Leaves;}
			if(m_Switch == 1){Output = m_Palm;}
			if(m_Switch == 2){Output = m_Grass;}
			if(m_Switch == 3){Output = m_None;}
			return Output;
		}


		float3 If4_g459( float Mode , float Cull , float3 Flip , float3 Mirror , float3 None )
		{
			float3 OUT = None;
			if(Cull == 0){
			    if(Mode == 0)
			        OUT = Flip;
			    if(Mode == 1)
			        OUT = Mirror;
			    if(Mode == 2)
			        OUT == None;
			}else{
			    OUT = None;
			}
			return OUT;
		}


		float3 HSVToRGB( float3 c )
		{
			float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
			float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
			return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
		}


		float3 RGBToHSV(float3 c)
		{
			float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
			float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
			float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
			float d = q.x - min( q.w, q.y );
			float e = 1.0e-10;
			return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
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
			int m_Switch252_g478 = _WindModeLeaves;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 VAR_VertexPosition21_g478 = mul( unity_ObjectToWorld, float4( ase_vertex3Pos , 0.0 ) ).xyz;
			float3 break109_g478 = VAR_VertexPosition21_g478;
			float VAR_WindStrength43_g478 = ( _WindStrength * _GlobalWindInfluence );
			float4 transform37_g478 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult38_g478 = (float2(transform37_g478.x , transform37_g478.z));
			float dotResult2_g479 = dot( appendResult38_g478 , float2( 12.9898,78.233 ) );
			float lerpResult8_g479 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g479 ) * 43758.55 ) ));
			float VAR_RandomTime16_g478 = ( _Time.x * lerpResult8_g479 );
			float FUNC_Turbulence36_g478 = ( sin( ( ( VAR_RandomTime16_g478 * 40.0 ) - ( VAR_VertexPosition21_g478.z / 15.0 ) ) ) * 0.5 );
			float VAR_WindPulse274_g478 = _WindPulse;
			float FUNC_Angle73_g478 = ( VAR_WindStrength43_g478 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g478 * 2.0 ) + FUNC_Turbulence36_g478 ) - ( VAR_VertexPosition21_g478.z / 50.0 ) ) - ( v.color.r / 20.0 ) ) ) ) * sqrt( v.color.r ) * 0.2 * VAR_WindPulse274_g478 );
			float VAR_SinA80_g478 = sin( FUNC_Angle73_g478 );
			float VAR_CosA78_g478 = cos( FUNC_Angle73_g478 );
			float _WindDirection164_g478 = _WindDirection;
			float2 localDirectionalEquation164_g478 = DirectionalEquation( _WindDirection164_g478 );
			float2 break165_g478 = localDirectionalEquation164_g478;
			float VAR_xLerp83_g478 = break165_g478.x;
			float lerpResult118_g478 = lerp( break109_g478.x , ( ( break109_g478.y * VAR_SinA80_g478 ) + ( break109_g478.x * VAR_CosA78_g478 ) ) , VAR_xLerp83_g478);
			float3 break98_g478 = VAR_VertexPosition21_g478;
			float3 break105_g478 = VAR_VertexPosition21_g478;
			float VAR_zLerp95_g478 = break165_g478.y;
			float lerpResult120_g478 = lerp( break105_g478.z , ( ( break105_g478.y * VAR_SinA80_g478 ) + ( break105_g478.z * VAR_CosA78_g478 ) ) , VAR_zLerp95_g478);
			float3 appendResult122_g478 = (float3(lerpResult118_g478 , ( ( break98_g478.y * VAR_CosA78_g478 ) - ( break98_g478.z * VAR_SinA80_g478 ) ) , lerpResult120_g478));
			float3 FUNC_vertexPos123_g478 = appendResult122_g478;
			float3 break236_g478 = FUNC_vertexPos123_g478;
			half FUNC_SinFunction195_g478 = sin( ( ( VAR_RandomTime16_g478 * 200.0 * ( 0.2 + v.color.g ) ) + ( v.color.g * 10.0 ) + FUNC_Turbulence36_g478 + ( VAR_VertexPosition21_g478.z / 2.0 ) ) );
			float VAR_GlobalWindTurbulence194_g478 = ( _WindTurbulence * _GlobalTurbulenceInfluence );
			float3 appendResult237_g478 = (float3(break236_g478.x , ( break236_g478.y + ( FUNC_SinFunction195_g478 * v.color.b * ( FUNC_Angle73_g478 + ( VAR_WindStrength43_g478 / 200.0 ) ) * VAR_GlobalWindTurbulence194_g478 ) ) , break236_g478.z));
			float3 OUT_Leafs_Standalone244_g478 = appendResult237_g478;
			float3 m_Leaves252_g478 = OUT_Leafs_Standalone244_g478;
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float3 appendResult234_g478 = (float3(( ase_normWorldNormal.x * v.color.g ) , ( ase_normWorldNormal.y / v.color.r ) , ( ase_normWorldNormal.z * v.color.g )));
			float3 OUT_Palm_Standalone243_g478 = ( ( ( FUNC_SinFunction195_g478 * v.color.b * ( FUNC_Angle73_g478 + ( VAR_WindStrength43_g478 / 200.0 ) ) * VAR_GlobalWindTurbulence194_g478 ) * appendResult234_g478 ) + FUNC_vertexPos123_g478 );
			float3 m_Palm252_g478 = OUT_Palm_Standalone243_g478;
			float3 break221_g478 = FUNC_vertexPos123_g478;
			float temp_output_202_0_g478 = ( FUNC_SinFunction195_g478 * v.color.b * ( FUNC_Angle73_g478 + ( VAR_WindStrength43_g478 / 200.0 ) ) );
			float lerpResult203_g478 = lerp( 0.0 , temp_output_202_0_g478 , VAR_xLerp83_g478);
			float lerpResult196_g478 = lerp( 0.0 , temp_output_202_0_g478 , VAR_zLerp95_g478);
			float3 appendResult197_g478 = (float3(( break221_g478.x + lerpResult203_g478 ) , break221_g478.y , ( break221_g478.z + lerpResult196_g478 )));
			float3 OUT_Grass_Standalone245_g478 = appendResult197_g478;
			float3 m_Grass252_g478 = OUT_Grass_Standalone245_g478;
			float3 m_None252_g478 = FUNC_vertexPos123_g478;
			float3 localIf252_g478 = If252_g478( m_Switch252_g478 , m_Leaves252_g478 , m_Palm252_g478 , m_Grass252_g478 , m_None252_g478 );
			float3 OUT_Leafs262_g478 = localIf252_g478;
			float3 temp_output_5_0_g478 = mul( unity_WorldToObject, float4( OUT_Leafs262_g478 , 0.0 ) ).xyz;
			float3 OUT_VertexPos261 = temp_output_5_0_g478;
			v.vertex.xyz = OUT_VertexPos261;
			float4 ase_screenPos = ComputeScreenPos( UnityObjectToClipPos( v.vertex ) );
			o.screenPosition = ase_screenPos;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float Mode4_g459 = (float)_DoubleSidedNormalMode;
			float Cull4_g459 = (float)_CullMode;
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			float3 bump5_g459 = UnpackScaleNormal( tex2D( _BumpMap, uv_BumpMap ), _BumpScale );
			float3 Flip4_g459 = ( bump5_g459 * i.ASEVFace );
			float3 break7_g459 = bump5_g459;
			float3 appendResult11_g459 = (float3(break7_g459.x , break7_g459.y , ( break7_g459.z * i.ASEVFace )));
			float3 Mirror4_g459 = appendResult11_g459;
			float3 None4_g459 = bump5_g459;
			float3 localIf4_g459 = If4_g459( Mode4_g459 , Cull4_g459 , Flip4_g459 , Mirror4_g459 , None4_g459 );
			float3 OUT_Normal255 = localIf4_g459;
			o.Normal = OUT_Normal255;
			float2 uv_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode13 = tex2D( _MainTex, uv_MainTex );
			float4 VAR_AlbedoTexture267 = tex2DNode13;
			float4 VAR_Albedo101 = ( _Color * VAR_AlbedoTexture267 );
			float4 VAR_Albedo18_g472 = VAR_Albedo101;
			float3 hsvTorgb9_g472 = RGBToHSV( VAR_Albedo18_g472.rgb );
			float3 hsvTorgb13_g472 = HSVToRGB( float3(( ( ( i.vertexColor.g - 0.5 ) * _ColorVariation ) + _Hue + hsvTorgb9_g472 ).x,( hsvTorgb9_g472.y * _Saturation ),( hsvTorgb9_g472.z * _Value )) );
			float4 lerpResult19_g472 = lerp( float4( hsvTorgb13_g472 , 0.0 ) , VAR_Albedo18_g472 , (float)_ColorShifting);
			float3 temp_output_39_0_g477 = lerpResult19_g472.rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 VAR_V74_g477 = ase_worldViewDir;
			float3 VAR_L75_g477 = _WorldSpaceLightPos0.xyz;
			float3 VAR_N12_g477 = (WorldNormalVector( i , OUT_Normal255 ));
			float3 normalizeResult97_g477 = normalize( ( VAR_L75_g477 + ( _TransNormalDistortion * VAR_N12_g477 ) ) );
			float3 VAR_H99_g477 = normalizeResult97_g477;
			float dotResult18_g477 = dot( VAR_V74_g477 , -VAR_H99_g477 );
			float VAR_VdotH25_g477 = ( pow( saturate( dotResult18_g477 ) , ( 50.0 - _Translucency ) ) * _TransScale );
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float3 appendResult109_g477 = (float3(_TranslucencyTint.r , _TranslucencyTint.g , _TranslucencyTint.b));
			float3 lerpResult105_g477 = lerp( ase_lightColor.rgb , appendResult109_g477 , (float)_ColorSource);
			float3 VAR_ColorSource106_g477 = lerpResult105_g477;
			float3 VAR_I31_g477 = ( _TransScattering * ( VAR_VdotH25_g477 + VAR_ColorSource106_g477 ) * ( 1.0 - i.uv4_texcoord4.x ) );
			float3 lerpResult112_g477 = lerp( temp_output_39_0_g477 , ( temp_output_39_0_g477 * VAR_I31_g477 ) , (float)_TranslucencyEnum);
			float3 OUT_Albedo254 = lerpResult112_g477;
			o.Albedo = OUT_Albedo254;
			o.Metallic = _Metallic;
			float lerpResult268 = lerp( 0.0 , VAR_AlbedoTexture267.r , _Glossiness);
			float OUT_Smoothness50 = lerpResult268;
			o.Smoothness = OUT_Smoothness50;
			float lerpResult41 = lerp( 1.0 , i.vertexColor.a , _OcclusionStrength);
			float OUT_AO44 = lerpResult41;
			o.Occlusion = OUT_AO44;
			clip( tex2DNode13.a - _Cutoff);
			float temp_output_41_0_g476 = tex2DNode13.a;
			float4 ase_screenPos = i.screenPosition;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 clipScreen45_g476 = ase_screenPosNorm.xy * _ScreenParams.xy;
			float dither45_g476 = Dither8x8Bayer( fmod(clipScreen45_g476.x, 8), fmod(clipScreen45_g476.y, 8) );
			dither45_g476 = step( dither45_g476, unity_LODFade.x );
			#ifdef LOD_FADE_CROSSFADE
				float staticSwitch40_g476 = ( temp_output_41_0_g476 * dither45_g476 );
			#else
				float staticSwitch40_g476 = temp_output_41_0_g476;
			#endif
			float OUT_Alpha46 = staticSwitch40_g476;
			o.Alpha = OUT_Alpha46;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=17800
532;85;1008;681;3007.031;232.3405;3.340669;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-1855.988,-2014.438;Inherit;False;1482.458;558.947;;10;46;32;30;101;14;13;11;10;252;267;Albedo;1,0.1254902,0.1254902,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;5;-1839.721,-142.752;Inherit;False;1742.909;463.5325;;7;246;53;108;47;48;37;255;Normal;0,0.627451,1,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;10;-1819.385,-1751.215;Float;True;Property;_MainTex;Albedo;1;0;Create;False;0;0;False;1;;None;8b0017825887ee44e83ac0cb49ceadf0;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TexturePropertyNode;37;-1789.721,-88.45222;Float;True;Property;_BumpMap;Normal Map;6;0;Create;False;0;0;False;1;Header(Normal Texture);None;bdcffb5968b084b4490b00efe97041e3;True;bump;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;13;-1591.027,-1750.344;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;47;-1538.721,-62.45196;Inherit;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;48;-1451.371,133.5333;Half;False;Property;_BumpScale;Normal Strength;7;0;Create;False;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;-1505.24,-1930.904;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;False;1;Header(Albedo Texture);1,1,1,1;0.9174442,1,0.8382353,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;267;-1232.125,-1749.935;Inherit;False;VAR_AlbedoTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;53;-1213.393,-62.87763;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.IntNode;108;-1128.812,130.5641;Inherit;False;Property;_CullMode;Cull Mode;2;1;[Enum];Create;True;3;Off;0;Front;1;Back;2;0;True;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-947.1429,-1770.289;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;4;-1825.133,466.5369;Inherit;False;1068.058;483.6455;;5;50;26;266;268;269;Smoothness;1,1,1,1;0;0
Node;AmplifyShaderEditor.FunctionNode;246;-794.7278,-61.63486;Inherit;False;Double Sided Backface Switch;3;;459;243a51f22b364cf4eac05d94dacd3901;0;2;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;101;-740.5823,-1777.458;Inherit;False;VAR_Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;2;-1846.601,-1273.244;Inherit;False;1748.792;441.2249;;5;254;363;361;364;102;Color Settings;1,0.1254902,0.1254902,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;102;-1818.028,-1161.422;Inherit;False;101;VAR_Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;266;-1764.856,614.312;Inherit;False;267;VAR_AlbedoTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;255;-404.5613,-65.86435;Inherit;False;OUT_Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;3;-1822.963,1121.527;Inherit;False;789.6466;355.3238;;4;44;41;31;24;AO;0.5372549,0.3568628,0.3568628,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;30;-1569.232,-1554.536;Half;False;Property;_Cutoff;Cutoff;5;0;Create;True;0;0;False;0;0.5;0.422;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1772.963,1353.167;Half;False;Property;_OcclusionStrength;AO strength;14;0;Create;False;0;0;False;1;Header(Other Settings);0.6;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;364;-1525.885,-1160.105;Inherit;False;Mtree Color Shifting;8;;472;4ec4833a692faa04fbef10a6f43e7e28;0;1;15;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;31;-1789.597,1169.527;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;361;-1472.346,-1015.847;Inherit;False;255;OUT_Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;262;-1806.631,1631.042;Inherit;False;602.7547;158.8317;;2;261;259;VertexPos;0,1,0.09019608,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1539.303,755.15;Inherit;False;Property;_Glossiness;Smoothness;16;0;Create;False;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;32;-1202.495,-1601.48;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;269;-1498.865,613.31;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FunctionNode;363;-1178.778,-1166.096;Inherit;False;Mtree Custom Translucency;17;;477;78c59f9806ffb4a6caf518eac19e0615;0;2;39;FLOAT3;0,0,0;False;90;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;259;-1755.631,1684.531;Inherit;False;Mtree Wind;25;;478;d710ffc7589a70c42a3e6c5220c6279d;7,269,1,281,0,272,0,255,1,282,0,280,0,278,0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;41;-1468.109,1240.014;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;268;-1198.89,611.662;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;252;-928.3115,-1600.964;Inherit;False;LOD CrossFade;-1;;476;bbfabe35be0e79d438adaa880ee1b0aa;1,44,1;1;41;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;261;-1463.068,1679.312;Inherit;False;OUT_VertexPos;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;50;-1005.773,606.3359;Inherit;False;OUT_Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;46;-661.6603,-1605.202;Inherit;False;OUT_Alpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;263;810.3169,-1055.222;Inherit;False;393;270;;1;51;Variables;1,0,0.7254902,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;44;-1286.328,1235.619;Inherit;False;OUT_AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;265;791.8203,-575.662;Inherit;False;757.7145;754.4375;;8;213;222;257;256;9;258;8;357;Output;0,0,0,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;254;-419.4247,-1171.265;Inherit;False;OUT_Albedo;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;8;916.0649,-6.583639;Inherit;False;46;OUT_Alpha;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;258;895.3975,72.05692;Inherit;False;261;OUT_VertexPos;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;357;913.8409,-241.9332;Inherit;False;44;OUT_AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;823.317,-1008.222;Inherit;False;Constant;_MaskClipValue;Mask Clip Value;14;1;[HideInInspector];Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;257;906.4799,-456.6621;Inherit;False;255;OUT_Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;256;906.4799,-525.662;Inherit;False;254;OUT_Albedo;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;9;876.04,-315.8114;Inherit;False;50;OUT_Smoothness;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;222;809.1625,-387.5882;Inherit;False;Property;_Metallic;Metallic;15;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;213;1279.325,-379.9919;Float;False;True;-1;2;;0;0;Standard;Mtree/Leafs Deferred;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;Off;0;False;271;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.422;True;True;0;True;TransparentCutout;;Geometry;DeferredOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;;-1;-1;-1;-1;0;False;0;0;True;108;-1;0;True;51;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;13;0;10;0
WireConnection;47;0;37;0
WireConnection;267;0;13;0
WireConnection;53;0;47;0
WireConnection;53;1;48;0
WireConnection;14;0;11;0
WireConnection;14;1;267;0
WireConnection;246;1;53;0
WireConnection;246;2;108;0
WireConnection;101;0;14;0
WireConnection;255;0;246;0
WireConnection;364;15;102;0
WireConnection;32;0;13;4
WireConnection;32;1;13;4
WireConnection;32;2;30;0
WireConnection;269;0;266;0
WireConnection;363;39;364;0
WireConnection;363;90;361;0
WireConnection;41;1;31;4
WireConnection;41;2;24;0
WireConnection;268;1;269;0
WireConnection;268;2;26;0
WireConnection;252;41;32;0
WireConnection;261;0;259;0
WireConnection;50;0;268;0
WireConnection;46;0;252;0
WireConnection;44;0;41;0
WireConnection;254;0;363;0
WireConnection;213;0;256;0
WireConnection;213;1;257;0
WireConnection;213;3;222;0
WireConnection;213;4;9;0
WireConnection;213;5;357;0
WireConnection;213;9;8;0
WireConnection;213;11;258;0
ASEEND*/
//CHKSM=A1160B325D40CBD595A8862955762611B3AE0CC6