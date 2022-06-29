// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Mtree/Leaves Outline"
{
	Properties
	{
		[Header(Albedo Texture)]_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		[Enum(Off,0,Front,1,Back,2)]_CullMode("Cull Mode", Int) = 0
		[Enum(Flip,0,Mirror,1,None,2)]_DoubleSidedNormalMode("Double Sided Normal Mode", Int) = 0
		_Cutoff("Cutoff", Range( 0 , 1)) = 0.5
		[Header(Outline)]_ToonRamp("Toon Ramp", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,0)
		_ToonStyle("Toon Style", Range( 0 , 1)) = 1
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
		[Header(Translucency)]
		_Translucency("Strength", Range( 0 , 50)) = 1
		_TransNormalDistortion("Normal Distortion", Range( 0 , 1)) = 0.1
		_TransScattering("Scaterring Falloff", Range( 1 , 50)) = 2
		_TransDirect("Direct", Range( 0 , 1)) = 1
		_TransAmbient("Ambient", Range( 0 , 1)) = 0.2
		_TransShadow("Shadow", Range( 0 , 1)) = 0.9
		[HDR]_TranslucencyTint("Translucency Tint", Color) = (0.2364577,0.3301887,0,1)
		[HDR]_TransmissionTint("Transmission Tint", Color) = (0.2352941,0.3294118,0,1)
		[Header(Wind)]_GlobalWindInfluence("Global Wind Influence", Range( 0 , 1)) = 1
		_GlobalTurbulenceInfluence("Global Turbulence Influence", Range( 0 , 1)) = 1
		[Enum(Leaves,0,Palm,1,Grass,2,Off,3)]_WindModeLeaves("Wind Mode Leaves", Int) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
		[Header(Forward Rendering Options)]
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Reflections", Float) = 1.0
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Geometry+0" }
		Cull [_CullMode]
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#pragma target 3.0
		#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
		#pragma shader_feature _GLOSSYREFLECTIONS_OFF
		#pragma multi_compile __ LOD_FADE_CROSSFADE
		#pragma surface surf StandardCustom keepalpha addshadow fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			half ASEVFace : VFACE;
			float4 vertexColor : COLOR;
			float3 worldNormal;
			INTERNAL_DATA
			float3 worldPos;
			float4 screenPosition;
		};

		struct SurfaceOutputStandardCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half Metallic;
			half Smoothness;
			half Occlusion;
			half Alpha;
			half3 Transmission;
			half3 Translucency;
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
		uniform float4 _OutlineColor;
		uniform float4 _MainTex_TexelSize;
		uniform float _ToonStyle;
		uniform float _Saturation;
		uniform float _Value;
		uniform int _ColorShifting;
		uniform sampler2D _ToonRamp;
		uniform float _Metallic;
		uniform float _Glossiness;
		uniform half _OcclusionStrength;
		uniform float4 _TransmissionTint;
		uniform half _Translucency;
		uniform half _TransNormalDistortion;
		uniform half _TransScattering;
		uniform half _TransDirect;
		uniform half _TransAmbient;
		uniform half _TransShadow;
		uniform float4 _TranslucencyTint;
		uniform half _Cutoff;


		float2 DirectionalEquation( float _WindDirection )
		{
			float d = _WindDirection * 0.0174532924;
			float xL = cos(d) + 1 / 2;
			float zL = sin(d) + 1 / 2;
			return float2(zL,xL);
		}


		float3 If252_g337( int m_Switch , float3 m_Leaves , float3 m_Palm , float3 m_Grass , float3 m_None )
		{
			float3 Output = m_None;
			if(m_Switch == 0){Output = m_Leaves;}
			if(m_Switch == 1){Output = m_Palm;}
			if(m_Switch == 2){Output = m_Grass;}
			if(m_Switch == 3){Output = m_None;}
			return Output;
		}


		float3 If4_g339( float Mode , float Cull , float3 Flip , float3 Mirror , float3 None )
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
			int m_Switch252_g337 = _WindModeLeaves;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 VAR_VertexPosition21_g337 = mul( unity_ObjectToWorld, float4( ase_vertex3Pos , 0.0 ) ).xyz;
			float3 break109_g337 = VAR_VertexPosition21_g337;
			float VAR_WindStrength43_g337 = ( _WindStrength * _GlobalWindInfluence );
			float4 transform37_g337 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float2 appendResult38_g337 = (float2(transform37_g337.x , transform37_g337.z));
			float dotResult2_g338 = dot( appendResult38_g337 , float2( 12.9898,78.233 ) );
			float lerpResult8_g338 = lerp( 0.8 , ( ( _RandomWindOffset / 2.0 ) + 0.9 ) , frac( ( sin( dotResult2_g338 ) * 43758.55 ) ));
			float VAR_RandomTime16_g337 = ( _Time.x * lerpResult8_g338 );
			float FUNC_Turbulence36_g337 = ( sin( ( ( VAR_RandomTime16_g337 * 40.0 ) - ( VAR_VertexPosition21_g337.z / 15.0 ) ) ) * 0.5 );
			float VAR_WindPulse274_g337 = _WindPulse;
			float FUNC_Angle73_g337 = ( VAR_WindStrength43_g337 * ( 1.0 + sin( ( ( ( ( VAR_RandomTime16_g337 * 2.0 ) + FUNC_Turbulence36_g337 ) - ( VAR_VertexPosition21_g337.z / 50.0 ) ) - ( v.color.r / 20.0 ) ) ) ) * sqrt( v.color.r ) * 0.2 * VAR_WindPulse274_g337 );
			float VAR_SinA80_g337 = sin( FUNC_Angle73_g337 );
			float VAR_CosA78_g337 = cos( FUNC_Angle73_g337 );
			float _WindDirection164_g337 = _WindDirection;
			float2 localDirectionalEquation164_g337 = DirectionalEquation( _WindDirection164_g337 );
			float2 break165_g337 = localDirectionalEquation164_g337;
			float VAR_xLerp83_g337 = break165_g337.x;
			float lerpResult118_g337 = lerp( break109_g337.x , ( ( break109_g337.y * VAR_SinA80_g337 ) + ( break109_g337.x * VAR_CosA78_g337 ) ) , VAR_xLerp83_g337);
			float3 break98_g337 = VAR_VertexPosition21_g337;
			float3 break105_g337 = VAR_VertexPosition21_g337;
			float VAR_zLerp95_g337 = break165_g337.y;
			float lerpResult120_g337 = lerp( break105_g337.z , ( ( break105_g337.y * VAR_SinA80_g337 ) + ( break105_g337.z * VAR_CosA78_g337 ) ) , VAR_zLerp95_g337);
			float3 appendResult122_g337 = (float3(lerpResult118_g337 , ( ( break98_g337.y * VAR_CosA78_g337 ) - ( break98_g337.z * VAR_SinA80_g337 ) ) , lerpResult120_g337));
			float3 FUNC_vertexPos123_g337 = appendResult122_g337;
			float3 break236_g337 = FUNC_vertexPos123_g337;
			half FUNC_SinFunction195_g337 = sin( ( ( VAR_RandomTime16_g337 * 200.0 * ( 0.2 + v.color.g ) ) + ( v.color.g * 10.0 ) + FUNC_Turbulence36_g337 + ( VAR_VertexPosition21_g337.z / 2.0 ) ) );
			float VAR_GlobalWindTurbulence194_g337 = ( _WindTurbulence * _GlobalTurbulenceInfluence );
			float3 appendResult237_g337 = (float3(break236_g337.x , ( break236_g337.y + ( FUNC_SinFunction195_g337 * v.color.b * ( FUNC_Angle73_g337 + ( VAR_WindStrength43_g337 / 200.0 ) ) * VAR_GlobalWindTurbulence194_g337 ) ) , break236_g337.z));
			float3 OUT_Leafs_Standalone244_g337 = appendResult237_g337;
			float3 m_Leaves252_g337 = OUT_Leafs_Standalone244_g337;
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float3 appendResult234_g337 = (float3(( ase_normWorldNormal.x * v.color.g ) , ( ase_normWorldNormal.y / v.color.r ) , ( ase_normWorldNormal.z * v.color.g )));
			float3 OUT_Palm_Standalone243_g337 = ( ( ( FUNC_SinFunction195_g337 * v.color.b * ( FUNC_Angle73_g337 + ( VAR_WindStrength43_g337 / 200.0 ) ) * VAR_GlobalWindTurbulence194_g337 ) * appendResult234_g337 ) + FUNC_vertexPos123_g337 );
			float3 m_Palm252_g337 = OUT_Palm_Standalone243_g337;
			float3 break221_g337 = FUNC_vertexPos123_g337;
			float temp_output_202_0_g337 = ( FUNC_SinFunction195_g337 * v.color.b * ( FUNC_Angle73_g337 + ( VAR_WindStrength43_g337 / 200.0 ) ) );
			float lerpResult203_g337 = lerp( 0.0 , temp_output_202_0_g337 , VAR_xLerp83_g337);
			float lerpResult196_g337 = lerp( 0.0 , temp_output_202_0_g337 , VAR_zLerp95_g337);
			float3 appendResult197_g337 = (float3(( break221_g337.x + lerpResult203_g337 ) , break221_g337.y , ( break221_g337.z + lerpResult196_g337 )));
			float3 OUT_Grass_Standalone245_g337 = appendResult197_g337;
			float3 m_Grass252_g337 = OUT_Grass_Standalone245_g337;
			float3 m_None252_g337 = FUNC_vertexPos123_g337;
			float3 localIf252_g337 = If252_g337( m_Switch252_g337 , m_Leaves252_g337 , m_Palm252_g337 , m_Grass252_g337 , m_None252_g337 );
			float3 OUT_Leafs262_g337 = localIf252_g337;
			float3 temp_output_5_0_g337 = mul( unity_WorldToObject, float4( OUT_Leafs262_g337 , 0.0 ) ).xyz;
			float3 OUT_VertexOffset261 = ( temp_output_5_0_g337 - ase_vertex3Pos );
			v.vertex.xyz += OUT_VertexOffset261;
			float4 ase_screenPos = ComputeScreenPos( UnityObjectToClipPos( v.vertex ) );
			o.screenPosition = ase_screenPos;
		}

		inline half4 LightingStandardCustom(SurfaceOutputStandardCustom s, half3 viewDir, UnityGI gi )
		{
			#if !DIRECTIONAL
			float3 lightAtten = gi.light.color;
			#else
			float3 lightAtten = lerp( _LightColor0.rgb, gi.light.color, _TransShadow );
			#endif
			half3 lightDir = gi.light.dir + s.Normal * _TransNormalDistortion;
			half transVdotL = pow( saturate( dot( viewDir, -lightDir ) ), _TransScattering );
			half3 translucency = lightAtten * (transVdotL * _TransDirect + gi.indirect.diffuse * _TransAmbient) * s.Translucency;
			half4 c = half4( s.Albedo * translucency * _Translucency, 0 );

			half3 transmission = max(0 , -dot(s.Normal, gi.light.dir)) * gi.light.color * s.Transmission;
			half4 d = half4(s.Albedo * transmission , 0);

			SurfaceOutputStandard r;
			r.Albedo = s.Albedo;
			r.Normal = s.Normal;
			r.Emission = s.Emission;
			r.Metallic = s.Metallic;
			r.Smoothness = s.Smoothness;
			r.Occlusion = s.Occlusion;
			r.Alpha = s.Alpha;
			return LightingStandard (r, viewDir, gi) + c + d;
		}

		inline void LightingStandardCustom_GI(SurfaceOutputStandardCustom s, UnityGIInput data, inout UnityGI gi )
		{
			#if defined(UNITY_PASS_DEFERRED) && UNITY_ENABLE_REFLECTION_BUFFERS
				gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal);
			#else
				UNITY_GLOSSY_ENV_FROM_SURFACE( g, s, data );
				gi = UnityGlobalIllumination( data, s.Occlusion, s.Normal, g );
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandardCustom o )
		{
			float Mode4_g339 = (float)_DoubleSidedNormalMode;
			float Cull4_g339 = (float)_CullMode;
			float2 uv_BumpMap = i.uv_texcoord * _BumpMap_ST.xy + _BumpMap_ST.zw;
			float3 bump5_g339 = UnpackScaleNormal( tex2D( _BumpMap, uv_BumpMap ), _BumpScale );
			float3 Flip4_g339 = ( bump5_g339 * i.ASEVFace );
			float3 break7_g339 = bump5_g339;
			float3 appendResult11_g339 = (float3(break7_g339.x , break7_g339.y , ( break7_g339.z * i.ASEVFace )));
			float3 Mirror4_g339 = appendResult11_g339;
			float3 None4_g339 = bump5_g339;
			float3 localIf4_g339 = If4_g339( Mode4_g339 , Cull4_g339 , Flip4_g339 , Mirror4_g339 , None4_g339 );
			float3 OUT_Normal255 = localIf4_g339;
			o.Normal = OUT_Normal255;
			float2 uv0_MainTex = i.uv_texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
			float4 tex2DNode296 = tex2D( _MainTex, uv0_MainTex );
			float4 VAR_AlbedoTexture311 = tex2DNode296;
			float2 appendResult284 = (float2(_MainTex_TexelSize.x , 0.0));
			float2 appendResult283 = (float2(0.0 , _MainTex_TexelSize.y));
			float lerpResult291 = lerp( 1.0 , 10.0 , _ToonStyle);
			float4 lerpResult301 = lerp( ( _Color * VAR_AlbedoTexture311 ) , _OutlineColor , ( tex2DNode296.a * ( 1.0 - ( tex2D( _MainTex, ( uv0_MainTex + appendResult284 ) ).a * tex2D( _MainTex, ( uv0_MainTex - appendResult284 ) ).a * tex2D( _MainTex, ( uv0_MainTex + appendResult283 ) ).a * tex2D( _MainTex, ( uv0_MainTex - appendResult283 ) ).a * lerpResult291 ) ) ));
			float4 VAR_Albedo302 = lerpResult301;
			float4 VAR_Albedo18_g332 = VAR_Albedo302;
			float3 hsvTorgb9_g332 = RGBToHSV( VAR_Albedo18_g332.rgb );
			float3 hsvTorgb13_g332 = HSVToRGB( float3(( ( ( i.vertexColor.g - 0.5 ) * _ColorVariation ) + _Hue + hsvTorgb9_g332 ).x,( hsvTorgb9_g332.y * _Saturation ),( hsvTorgb9_g332.z * _Value )) );
			float4 lerpResult19_g332 = lerp( float4( hsvTorgb13_g332 , 0.0 ) , VAR_Albedo18_g332 , (float)_ColorShifting);
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			#if defined(LIGHTMAP_ON) && UNITY_VERSION < 560 //aseld
			float3 ase_worldlightDir = 0;
			#else //aseld
			float3 ase_worldlightDir = normalize( UnityWorldSpaceLightDir( ase_worldPos ) );
			#endif //aseld
			float dotResult271 = dot( ase_worldNormal , ase_worldlightDir );
			float2 temp_cast_7 = (saturate( (dotResult271*0.5 + 0.5) )).xx;
			float4 VAR_ColorRamp276 = tex2D( _ToonRamp, temp_cast_7 );
			float4 OUT_Albedo254 = ( lerpResult19_g332 * VAR_ColorRamp276 );
			o.Albedo = OUT_Albedo254.rgb;
			o.Metallic = _Metallic;
			float lerpResult309 = lerp( 0.0 , VAR_AlbedoTexture311.r , _Glossiness);
			float OUT_Smoothness50 = lerpResult309;
			o.Smoothness = OUT_Smoothness50;
			float lerpResult41 = lerp( 1.0 , i.vertexColor.a , _OcclusionStrength);
			float OUT_AO44 = lerpResult41;
			o.Occlusion = OUT_AO44;
			float4 temp_output_36_0_g336 = VAR_AlbedoTexture311;
			#if defined(LIGHTMAP_ON) && ( UNITY_VERSION < 560 || ( defined(LIGHTMAP_SHADOW_MIXING) && !defined(SHADOWS_SHADOWMASK) && defined(SHADOWS_SCREEN) ) )//aselc
			float4 ase_lightColor = 0;
			#else //aselc
			float4 ase_lightColor = _LightColor0;
			#endif //aselc
			float3 temp_output_37_0_g336 = ( ase_lightColor.rgb * ase_lightColor.a );
			float3 OUT_Transmission316 = ( (( _TransmissionTint * temp_output_36_0_g336 )).rgb * temp_output_37_0_g336 * i.vertexColor.g );
			o.Transmission = OUT_Transmission316;
			float3 OUT_Translucency315 = ( (( _TranslucencyTint * temp_output_36_0_g336 )).rgb * temp_output_37_0_g336 * i.vertexColor.g );
			o.Translucency = OUT_Translucency315;
			clip( lerpResult301.a - _Cutoff);
			float temp_output_41_0_g340 = lerpResult301.a;
			float4 ase_screenPos = i.screenPosition;
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float2 clipScreen45_g340 = ase_screenPosNorm.xy * _ScreenParams.xy;
			float dither45_g340 = Dither8x8Bayer( fmod(clipScreen45_g340.x, 8), fmod(clipScreen45_g340.y, 8) );
			dither45_g340 = step( dither45_g340, unity_LODFade.x );
			#ifdef LOD_FADE_CROSSFADE
				float staticSwitch40_g340 = ( temp_output_41_0_g340 * dither45_g340 );
			#else
				float staticSwitch40_g340 = temp_output_41_0_g340;
			#endif
			float OUT_Alpha306 = staticSwitch40_g340;
			o.Alpha = OUT_Alpha306;
		}

		ENDCG
	}
	Fallback "Diffuse"
}
/*ASEBEGIN
Version=17800
532;85;1008;681;2337.568;-671.5203;2.212302;True;False
Node;AmplifyShaderEditor.CommentaryNode;1;-1846.293,-2939.105;Inherit;False;4536.057;1466.718;;30;307;306;305;304;303;302;301;300;299;298;297;296;295;294;293;292;291;290;289;288;287;286;285;284;283;282;281;280;308;311;Albedo;1,0.1254902,0.1254902,1;0;0
Node;AmplifyShaderEditor.TexturePropertyNode;280;-1760.188,-2790.359;Float;True;Property;_MainTex;Albedo;1;0;Create;False;0;0;False;1;;None;8b0017825887ee44e83ac0cb49ceadf0;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TexelSizeNode;281;-1479.337,-2276.442;Inherit;False;-1;1;0;SAMPLER2D;;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;283;-1123.276,-2134.475;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;282;-1231.4,-2855.684;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;284;-1118.276,-2280.475;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;288;-706.8723,-2082.859;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;285;-721.2034,-1859.944;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;287;-715.4963,-2306.805;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;286;-440.0023,-1689.501;Inherit;False;Property;_ToonStyle;Toon Style;8;0;Create;True;0;0;False;0;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;289;-701.4431,-2511.226;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;266;-1807.739,2402.5;Inherit;False;2082.579;669.7218;;4;276;275;270;267;Color Ramp;1,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;267;-1785.265,2634.178;Inherit;False;540.401;361.8907;;3;271;269;268;N . L;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;291;-129.4505,-1686.403;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;10;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;290;-271.2654,-2330.72;Inherit;True;Property;_TextureSample2;Texture Sample 2;4;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;292;-267.2654,-2110.72;Inherit;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;307;-272.3001,-1891.479;Inherit;True;Property;_TextureSample4;Texture Sample 4;6;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;293;-275.2654,-2541.72;Inherit;True;Property;_TextureSample3;Texture Sample 3;3;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;270;-1096.919,2656.254;Inherit;False;723.599;290;Also know as Lambert Wrap or Half Lambert;3;274;273;272;Diffuse Wrap;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;296;-271.3763,-2755.639;Inherit;True;Property;_TextureSample5;Texture Sample 5;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;294;166.3196,-2167.112;Inherit;False;5;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;268;-1721.265,2842.177;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldNormalVector;269;-1673.265,2682.177;Inherit;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleSubtractOpNode;297;344.9265,-2251.1;Inherit;False;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;311;152.9506,-2709.866;Inherit;False;VAR_AlbedoTexture;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;295;154.5998,-2871.49;Inherit;False;Property;_Color;Color;0;0;Create;True;0;0;False;1;Header(Albedo Texture);1,1,1,1;0.9174442,1,0.8382353,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;271;-1385.265,2746.177;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;272;-1046.919,2831.254;Float;False;Constant;_WrapperValue;Wrapper Value;0;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;298;560.0448,-2616.937;Inherit;False;Property;_OutlineColor;Outline Color;7;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;299;589.0508,-2333.457;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;300;551.7608,-2770.174;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;273;-825.9753,2750.71;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;274;-548.3201,2713.054;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;301;966.4218,-2768.543;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;5;-1839.721,-142.752;Inherit;False;1742.909;463.5325;;7;246;53;108;47;48;37;255;Normal;0,0.627451,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;4;-1825.133,466.5369;Inherit;False;1068.058;483.6455;;5;50;26;309;310;312;Smoothness;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;275;-299.4062,2684.163;Inherit;True;Property;_ToonRamp;Toon Ramp;6;0;Create;True;0;0;False;1;Header(Outline);-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;2;-1846.601,-1273.244;Inherit;False;1230.287;452.9673;;5;254;251;277;323;102;Color Settings;1,0.1254902,0.1254902,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;302;1185.367,-2767.74;Inherit;False;VAR_Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TexturePropertyNode;37;-1789.721,-88.45222;Float;True;Property;_BumpMap;Normal Map;9;0;Create;False;0;0;False;1;Header(Normal Texture);None;bdcffb5968b084b4490b00efe97041e3;True;bump;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;276;0.009857178,2685.038;Inherit;False;VAR_ColorRamp;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-1451.371,133.5333;Half;False;Property;_BumpScale;Normal Strength;10;0;Create;False;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;317;-1786.739,3301.699;Inherit;False;1141.754;243.6982;;3;314;315;316;Translucency / Transmission;1,0.6827586,0,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;310;-1791.125,566.9387;Inherit;False;311;VAR_AlbedoTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.BreakToComponentsNode;304;1159.721,-2648.548;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.GetLocalVarNode;102;-1783.602,-1168.691;Inherit;False;302;VAR_Albedo;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;47;-1538.721,-62.45196;Inherit;True;Property;_TextureSample1;Texture Sample 1;0;0;Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;3;-1822.963,1121.527;Inherit;False;789.6466;355.3238;;4;44;41;31;24;AO;0.5372549,0.3568628,0.3568628,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;303;1100.834,-2380.163;Half;False;Property;_Cutoff;Cutoff;5;0;Create;True;0;0;False;0;0.5;0.015;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;262;-1806.631,1631.042;Inherit;False;556.9164;193.8704;;2;261;259;VertexPos;0,1,0.09019608,1;0;0
Node;AmplifyShaderEditor.BreakToComponentsNode;312;-1563.183,571.7578;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FunctionNode;323;-1544.011,-1166.571;Inherit;False;Mtree Color Shifting;11;;332;4ec4833a692faa04fbef10a6f43e7e28;0;1;15;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;314;-1736.739,3379.092;Inherit;False;311;VAR_AlbedoTexture;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;53;-1213.393,-62.87763;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ClipNode;305;1467.571,-2427.106;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1580.114,820.1908;Inherit;False;Property;_Glossiness;Smoothness;19;0;Create;False;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-1772.963,1353.167;Half;False;Property;_OcclusionStrength;AO strength;17;0;Create;False;0;0;False;1;Header(Other Settings);0.6;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;31;-1789.597,1169.527;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;277;-1478.657,-1096.782;Inherit;False;276;VAR_ColorRamp;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.IntNode;108;-1128.812,130.5641;Inherit;False;Property;_CullMode;Cull Mode;2;1;[Enum];Create;True;3;Off;0;Front;1;Back;2;0;True;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;251;-1045.64,-1169.77;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;308;1716.582,-2425.971;Inherit;False;LOD CrossFade;-1;;340;bbfabe35be0e79d438adaa880ee1b0aa;1,44,1;1;41;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;309;-1273.354,667.2804;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;41;-1462.109,1314.014;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;322;-1377.47,3381.486;Inherit;False;Mtree Translucency;27;;336;7db2d70ac1ae74fb2a61542e62b24b72;0;1;36;COLOR;0,0,0,0;False;2;FLOAT3;0;FLOAT3;39
Node;AmplifyShaderEditor.FunctionNode;259;-1772.578,1705.35;Inherit;False;Mtree Wind;30;;337;d710ffc7589a70c42a3e6c5220c6279d;7,269,0,281,0,272,0,255,1,282,0,280,0,278,0;0;1;FLOAT3;0
Node;AmplifyShaderEditor.FunctionNode;246;-794.7278,-61.63486;Inherit;False;Double Sided Backface Switch;3;;339;243a51f22b364cf4eac05d94dacd3901;0;2;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;315;-891.9844,3351.699;Inherit;False;OUT_Translucency;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;44;-1291.328,1312.619;Inherit;False;OUT_AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;255;-404.5613,-65.86435;Inherit;False;OUT_Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;254;-894.0403,-1173.935;Inherit;False;OUT_Albedo;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;316;-891.9841,3430.397;Inherit;False;OUT_Transmission;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CommentaryNode;263;795.3169,-939.2224;Inherit;False;275;165;;1;51;Variables;1,0,0.7254902,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;261;-1549.085,1701.22;Inherit;False;OUT_VertexOffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;306;2051.847,-2428.56;Inherit;False;OUT_Alpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;50;-1095.773,659.3359;Inherit;False;OUT_Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;265;801.9891,-575.662;Inherit;False;754.5457;739.9922;;10;213;257;222;258;8;256;9;318;319;7;Output;0,0,0,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;319;878.4391,-170.9256;Inherit;False;316;OUT_Transmission;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;258;890.5248,65.68843;Inherit;False;261;OUT_VertexOffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;8;920.6537,-16.99908;Inherit;False;306;OUT_Alpha;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;318;879.9139,-95.72011;Inherit;False;315;OUT_Translucency;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;256;906.4799,-525.662;Inherit;False;254;OUT_Albedo;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;51;845.317,-889.2224;Inherit;False;Constant;_MaskClipValue;Mask Clip Value;14;1;[HideInInspector];Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;9;884.1423,-315.8114;Inherit;False;50;OUT_Smoothness;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;222;851.9891,-387.5882;Inherit;False;Property;_Metallic;Metallic;18;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;257;906.4799,-456.6621;Inherit;False;255;OUT_Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;7;937.2698,-244.5712;Inherit;False;44;OUT_AO;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;213;1278.111,-327.7843;Float;False;True;-1;2;;0;0;Standard;Mtree/Leaves Outline;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.422;True;True;0;True;TransparentCutout;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;20;-1;-1;0;False;0;0;True;108;-1;0;True;51;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;281;0;280;0
WireConnection;283;1;281;2
WireConnection;282;2;280;0
WireConnection;284;0;281;1
WireConnection;288;0;282;0
WireConnection;288;1;283;0
WireConnection;285;0;282;0
WireConnection;285;1;283;0
WireConnection;287;0;282;0
WireConnection;287;1;284;0
WireConnection;289;0;282;0
WireConnection;289;1;284;0
WireConnection;291;2;286;0
WireConnection;290;0;280;0
WireConnection;290;1;287;0
WireConnection;292;0;280;0
WireConnection;292;1;288;0
WireConnection;307;0;280;0
WireConnection;307;1;285;0
WireConnection;293;0;280;0
WireConnection;293;1;289;0
WireConnection;296;0;280;0
WireConnection;296;1;282;0
WireConnection;294;0;293;4
WireConnection;294;1;290;4
WireConnection;294;2;292;4
WireConnection;294;3;307;4
WireConnection;294;4;291;0
WireConnection;297;1;294;0
WireConnection;311;0;296;0
WireConnection;271;0;269;0
WireConnection;271;1;268;0
WireConnection;299;0;296;4
WireConnection;299;1;297;0
WireConnection;300;0;295;0
WireConnection;300;1;311;0
WireConnection;273;0;271;0
WireConnection;273;1;272;0
WireConnection;273;2;272;0
WireConnection;274;0;273;0
WireConnection;301;0;300;0
WireConnection;301;1;298;0
WireConnection;301;2;299;0
WireConnection;275;1;274;0
WireConnection;302;0;301;0
WireConnection;276;0;275;0
WireConnection;304;0;301;0
WireConnection;47;0;37;0
WireConnection;312;0;310;0
WireConnection;323;15;102;0
WireConnection;53;0;47;0
WireConnection;53;1;48;0
WireConnection;305;0;304;3
WireConnection;305;1;304;3
WireConnection;305;2;303;0
WireConnection;251;0;323;0
WireConnection;251;1;277;0
WireConnection;308;41;305;0
WireConnection;309;1;312;0
WireConnection;309;2;26;0
WireConnection;41;1;31;4
WireConnection;41;2;24;0
WireConnection;322;36;314;0
WireConnection;246;1;53;0
WireConnection;246;2;108;0
WireConnection;315;0;322;0
WireConnection;44;0;41;0
WireConnection;255;0;246;0
WireConnection;254;0;251;0
WireConnection;316;0;322;39
WireConnection;261;0;259;0
WireConnection;306;0;308;0
WireConnection;50;0;309;0
WireConnection;213;0;256;0
WireConnection;213;1;257;0
WireConnection;213;3;222;0
WireConnection;213;4;9;0
WireConnection;213;5;7;0
WireConnection;213;6;319;0
WireConnection;213;7;318;0
WireConnection;213;9;8;0
WireConnection;213;11;258;0
ASEEND*/
//CHKSM=2B8C506EA9881DA03B5A5829767C28A4BFB5EA82